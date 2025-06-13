using WorkflowResults.Lexing;
using WorkflowResults.Parsing.Expressions.Interfaces;
using WorkflowResults.Parsing.Expressions.Nodes.BinaryExpressions;
using WorkflowResults.Parsing.Expressions.Nodes.Expressions;

namespace WorkflowResults.Parsing.Expressions;

public static class ExpressionParser
{
    public static IExpressionNode ParseExpression(Token token, TokenStream stream)
    {
        IExpressionNode expressionNode = ParsePrimaryExpression(token, stream);

        return stream.Peek().Type switch
        {
            TokenType.ArithmeticOperator => ParseArithmeticOperatorExpression(expressionNode, stream),
            TokenType.ComparisonOperator => ParseComparisonOperatorExpression(expressionNode, stream),
            TokenType.StringConcatenation => ParseStringConcatenation(expressionNode, stream),
            _ => expressionNode
        };
    }

    public static IList<IExpressionNode> ParseArguments(TokenStream stream)
    {
        stream.Expect(TokenType.OpenParenthesis);
        IList<IExpressionNode> arguments = [];

        if (stream.Peek().Type != TokenType.CloseParenthesis)
        {
            while (stream.Peek().Type != TokenType.CloseParenthesis)
            {
                arguments.Add(ParseExpression(stream.Eat(), stream));
                if (stream.Peek().Type == TokenType.Comma)
                {
                    stream.Eat();
                }
            }
        }

        stream.Expect(TokenType.CloseParenthesis);

        return arguments;
    }

    public static IExpressionNode ParseIdentifierExpression(Token token, TokenStream stream)
    {
        IdentifierNode identifier = new(token.Value);

        return stream.Peek().Type is not TokenType.MemberAccessOperator
            ? identifier
            : ParseMemberAccess(identifier, stream);
    }

    private static IExpressionNode ParseArithmeticOperatorExpression(IExpressionNode leftNode, TokenStream stream)
    {
        while (stream.Peek().Value is "+" or "-" or "/" or "*")
        {
            string operatorValue = stream.Eat().Value;
            IExpressionNode rightNode = ParseMultiplicativeExpression(stream.Eat(), stream);
            leftNode = new ArithmeticExpressionNode(operatorValue, leftNode, rightNode);
        }

        return leftNode;
    }


    private static IExpressionNode ParseMultiplicativeExpression(Token token, TokenStream stream)
    {
        IExpressionNode leftNode = ParsePrimaryExpression(token, stream);

        while (stream.Peek().Value is "/" or "*")
        {
            string operatorValue = stream.Eat().Value;
            IExpressionNode rightNode = ParsePrimaryExpression(stream.Eat(), stream);
            leftNode = new ArithmeticExpressionNode(operatorValue, leftNode, rightNode);
        }

        return leftNode;
    }

    private static IExpressionNode ParseComparisonOperatorExpression(IExpressionNode leftNode, TokenStream stream)
    {
        string operatorValue = stream.Eat().Value;
        IExpressionNode rightNode = ParsePrimaryExpression(stream.Eat(), stream);

        return new BooleanExpressionNode(operatorValue, leftNode, rightNode);
    }

    private static IExpressionNode ParseStringConcatenation(IExpressionNode expressionNode, TokenStream stream)
    {
        IList<IExpressionNode> strings = [expressionNode];
        while (stream.Peek().Type is TokenType.StringConcatenation)
        {
            stream.Eat();
            strings.Add(ParseExpression(stream.Eat(), stream));

        }
        return new StringConcatenationNode(strings);
    }


    private static IExpressionNode ParsePrimaryExpression(Token token, TokenStream stream)
    {
        return token.Type switch
        {
            TokenType.String => new StringLiteralNode(token.Value),
            TokenType.Number => new NumberLiteral(int.Parse(token.Value)),
            TokenType.Identifier => ParseIdentifierExpression(token, stream),
            TokenType.Bool => new BooleanLiteralNode(token.Value == "true"),
            _ => throw new Exception($"Unexpected {token.Value} at line {token.LineIndex}")
        };
    }

    private static IExpressionNode ParseMemberAccess(IdentifierNode identifierNode, TokenStream stream)
    {
        stream.Expect(TokenType.MemberAccessOperator);
        Token memberIdentifier = stream.Eat();

        if (memberIdentifier.Type is not TokenType.Identifier)
            throw new Exception(
                $"Expected {TokenType.Identifier.ToString()} at line {memberIdentifier.LineIndex} but found {memberIdentifier.Type.ToString()}");

        IdentifierNode memberNode = new(memberIdentifier.Value);

        if (stream.Peek().Type is not TokenType.OpenParenthesis)
            return new MemberAccessNode(identifierNode, memberNode);
        if (identifierNode.Name.Equals("Clients"))
        {
            return new ClientsNode(memberNode, ParseArguments(stream));
        }
        return new UsersNode(memberNode, ParseArguments(stream));
    }
}