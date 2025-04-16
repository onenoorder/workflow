using WorkflowZero.AbstractSyntaxTree.Nodes;
using WorkflowZero.AbstractSyntaxTree.Nodes.BinaryExpressions;
using WorkflowZero.AbstractSyntaxTree.Nodes.Expressions;
using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;
using WorkflowZero.AbstractSyntaxTree.Nodes.Statements;
using WorkflowZero.Lexing;

namespace WorkflowZero.Parsing;

public class Parser(StreamReader sourceCodeStream)
{
    private readonly Queue<Token> _tokens = Lexer.Tokenize(sourceCodeStream);

    public ProgramNode Parse()
    {
        IList<IStatementNode> statements = [];

        while (NotAtEndOfFile())
        {
            statements.Add(ParseStatement());
        }

        return new ProgramNode(statements);
    }

    private bool NotAtEndOfFile()
    {
        return _tokens.Peek().Type != TokenType.Eof;
    }

    private Token GetNextToken()
    {
        Token token = _tokens.Dequeue();

        if (token.Type == TokenType.Eof)
        {
            throw new Exception($"Unexpected EOF at line:{token.LineIndex} char: {token.CharIndex}");
        }

        return token;
    }

    private Token PeakNextToken()
    {
        return _tokens.Peek();
    }

    private IStatementNode ParseStatement()
    {
        Token currentToken = GetNextToken();

        if ((PeakNextToken().Type is TokenType.EqualSign || 
            PeakNextToken().Type is TokenType.MemberAccessOperator) &&
            currentToken.Type is TokenType.Identifier)
        {
            return ParseVariableAssignment(currentToken);
        }

        return currentToken.Type switch
        {
            TokenType.Print => new PrintNode(ParseArgument() ?? new StringLiteralNode("")),
            TokenType.If => ParseIfThenElse(),
            TokenType.Loop => ParseLoop(),
            _ => ParseExpression(currentToken)
        };
    }

    private IStatementNode ParseLoop()
    {
        Token nextToken = GetNextToken();
        IExpressionNode times = ParseExpression(nextToken);

        if (times is not NumberLiteral and not UsersNode)
        {
            throw new Exception($"Cannot loop {nextToken}");
        }

        TokenType nextTokenType = PeakNextToken().Type;
        IdentifierNode? loopItemIdentifier = null;

        if (nextTokenType == TokenType.Identifier)
        {
            loopItemIdentifier = new IdentifierNode(GetNextToken().Value);
            nextTokenType = PeakNextToken().Type;
        }

        IList<IStatementNode> loopBlock = [];

        while (nextTokenType != TokenType.EndLoop)
        {
            loopBlock.Add(ParseStatement());
            nextTokenType = PeakNextToken().Type;
        }

        ExpectToken(TokenType.EndLoop);

        return new LoopNode(times,  new ProgramNode(loopBlock), loopItemIdentifier);
    }

    private IStatementNode ParseIfThenElse()
    {
        Token nextToken = GetNextToken();
        IExpressionNode ifCondition = ParseExpression(nextToken);

        if (ifCondition is not BooleanExpressionNode)
        {
            throw new Exception($"Expected boolean value at line {nextToken.LineIndex}");
        }

        ProgramNode thenNode = ParseThenBlock();
        ProgramNode? elseNode = ParseElseBlock();
        ExpectToken(TokenType.EndIf);

        return new IfThenElseNode((BooleanExpressionNode)ifCondition, thenNode, elseNode);
    }

    private ProgramNode ParseThenBlock()
    {
        ExpectToken(TokenType.Then);
        TokenType nextTokenType = PeakNextToken().Type;
        IList<IStatementNode> then = [];

        while (nextTokenType != TokenType.Else && nextTokenType != TokenType.EndIf)
        {
            then.Add(ParseStatement());
            nextTokenType = PeakNextToken().Type;
        }

        return new ProgramNode(then);
    }

    private ProgramNode? ParseElseBlock()
    {
        ProgramNode? elseNode = null;

        if (PeakNextToken().Type == TokenType.Else)
        {
            ExpectToken(TokenType.Else);
            IList<IStatementNode> elseBlock = [];

            while (PeakNextToken().Type != TokenType.EndIf)
            {
                elseBlock.Add(ParseStatement());
            }

            elseNode = new ProgramNode(elseBlock);
        }

        return elseNode;
    }


    private IStatementNode ParseVariableAssignment(Token token)
    {
        IExpressionNode returnNode = ParseIdentifierExpression(token);
        ExpectToken(TokenType.EqualSign);
        IExpressionNode value = ParseExpression(GetNextToken());

        return new VariableAssignmentNode(returnNode, value);
    }

    private IExpressionNode ParseExpression(Token token)
    {
        IExpressionNode expressionNode = ParsePrimaryExpression(token);

        return PeakNextToken().Type switch
        {
            TokenType.ArithmeticOperator => ParseArithmeticOperatorExpression(expressionNode),
            TokenType.ComparisonOperator => ParseComparisonOperatorExpression(expressionNode),
            _ => expressionNode
        };
    }
    
    
    
    private IExpressionNode ParsePrimaryExpression(Token token)
    {
        return token.Type switch
        {
            TokenType.String => new StringLiteralNode(token.Value),
            TokenType.Number => new NumberLiteral(int.Parse(token.Value)),
            TokenType.Identifier => ParseIdentifierExpression(token),
            TokenType.Bool => new BooleanLiteralNode(token.Value == "true"),
            _ => throw new Exception($"Unexpected {token.Value} at line {token.LineIndex}")
        };
    }

    private IExpressionNode ParseIdentifierExpression(Token token)
    {
        IdentifierNode identifier = new(token.Value);

        return PeakNextToken().Type is not TokenType.MemberAccessOperator ? 
            identifier : ParseMemberAccess(identifier);
    }

    private IExpressionNode ParseMemberAccess(IdentifierNode identifierNode)
    {
        ExpectToken(TokenType.MemberAccessOperator);
        Token memberIdentifier = GetNextToken();

        if (memberIdentifier.Type is not TokenType.Identifier)
            throw new Exception(
                $"Expected {TokenType.Identifier.ToString()} at line {memberIdentifier.LineIndex} but found {memberIdentifier.Type.ToString()}");

        IdentifierNode memberNode = new(memberIdentifier.Value);

        if (PeakNextToken().Type is not TokenType.OpenParenthesis)
            return new MemberAccessNode(identifierNode, memberNode);

        return new UsersNode(memberNode, ParseArgument());
    }


    private IExpressionNode ParseArithmeticOperatorExpression(IExpressionNode leftNode)
    {
        while (PeakNextToken().Value is "+" or "-")
        {
            string operatorValue = GetNextToken().Value;
            IExpressionNode rightNode = ParseMultiplicativeExpression(GetNextToken());
            leftNode = new ArithmeticExpressionNode(operatorValue, leftNode, rightNode);
        }

        return leftNode;
    }

    private IExpressionNode ParseMultiplicativeExpression(Token token)
    {
        IExpressionNode leftNode = ParsePrimaryExpression(token);

        while (PeakNextToken().Value is "/" or "*")
        {
            string operatorValue = GetNextToken().Value;
            IExpressionNode rightNode = ParsePrimaryExpression(GetNextToken());
            leftNode = new ArithmeticExpressionNode(operatorValue, leftNode, rightNode);
        }

        return leftNode;
    }


    private IExpressionNode ParseComparisonOperatorExpression(IExpressionNode leftNode)
    {
        string operatorValue = GetNextToken().Value;
        IExpressionNode rightNode = ParsePrimaryExpression(GetNextToken());

        return new BooleanExpressionNode(operatorValue, leftNode, rightNode);
    }


    private IExpressionNode? ParseArgument()
    {
        ExpectToken(TokenType.OpenParenthesis);
        IExpressionNode? argument = null;

        if (PeakNextToken().Type != TokenType.CloseParenthesis)
        {
            argument = ParseExpression(GetNextToken());
        }

        ExpectToken(TokenType.CloseParenthesis);

        return argument;
    }

    private void ExpectToken(TokenType type)
    {
        Token nextToken = GetNextToken();

        if (nextToken.Type != type)
            throw new Exception(
                $"Expected {type.ToString()} at line {nextToken.LineIndex} but found {nextToken.Type.ToString()}");
    }
}