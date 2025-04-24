using WorkflowZero.Lexing;
using WorkflowZero.Parsing.Expressions;
using WorkflowZero.Parsing.Expressions.Interfaces;
using WorkflowZero.Parsing.Expressions.Nodes.BinaryExpressions;
using WorkflowZero.Parsing.Statements.Interfaces;

namespace WorkflowZero.Parsing.Statements.IfThenElse;

public class IfThenElseRule : IParserRule
{
    public bool CanParse(TokenStream stream)
    {
        return stream.Peek().Type.Equals(TokenType.If);
    }

    public IStatementNode Parse(TokenStream stream)
    {
        stream.Eat();
        Token nextToken = stream.Eat();
        IExpressionNode ifCondition = ExpressionParser.ParseExpression(nextToken, stream);

        if (ifCondition is not BooleanExpressionNode)
        {
            throw new Exception($"Expected boolean value at line {nextToken.LineIndex}");
        }

        ProgramNode thenNode = ParseThenBlock(stream);
        ProgramNode? elseNode = ParseElseBlock(stream);
        stream.Expect(TokenType.EndIf);

        return new IfThenElseNode((BooleanExpressionNode)ifCondition, thenNode, elseNode);
    }


    private ProgramNode ParseThenBlock(TokenStream stream)
    {
        stream.Expect(TokenType.Then);
        TokenType nextTokenType = stream.Peek().Type;
        IList<IStatementNode> then = [];

        while (nextTokenType != TokenType.Else && nextTokenType != TokenType.EndIf)
        {
            then.Add(StatementParser.ParseStatement(stream));
            nextTokenType = stream.Peek().Type;
        }

        return new ProgramNode(then);
    }

    private ProgramNode? ParseElseBlock(TokenStream stream)
    {
        ProgramNode? elseNode = null;

        if (stream.Peek().Type == TokenType.Else)
        {
            stream.Expect(TokenType.Else);
            IList<IStatementNode> elseBlock = [];

            while (stream.Peek().Type != TokenType.EndIf)
            {
                elseBlock.Add(StatementParser.ParseStatement(stream));
            }

            elseNode = new ProgramNode(elseBlock);
        }

        return elseNode;
    }
}