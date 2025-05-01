using DemoBackend.Lexing;
using DemoBackend.Parsing.Expressions;
using DemoBackend.Parsing.Expressions.Interfaces;
using DemoBackend.Parsing.Expressions.Nodes.Expressions;
using DemoBackend.Parsing.Statements.Interfaces;

namespace DemoBackend.Parsing.Statements.Loop;

public class LoopRule : IParserRule
{
    public bool CanParse(TokenStream stream)
    {
        return stream.Peek().Type.Equals(TokenType.Loop);
    }

    public IStatementNode Parse(TokenStream stream)
    {
        stream.Eat();
        Token nextToken = stream.Eat();
        IExpressionNode times = ExpressionParser.ParseExpression(nextToken, stream);

        if (times is not NumberLiteral and not UsersNode)
        {
            throw new Exception($"Cannot loop {nextToken}");
        }

        TokenType nextTokenType = stream.Peek().Type;
        IdentifierNode? loopItemIdentifier = null;

        if (nextTokenType == TokenType.Identifier)
        {
            loopItemIdentifier = new IdentifierNode(stream.Eat().Value);
            nextTokenType = stream.Peek().Type;
        }

        IList<IStatementNode> loopBlock = [];

        while (nextTokenType != TokenType.EndLoop)
        {
            loopBlock.Add(StatementParser.ParseStatement(stream));
            nextTokenType = stream.Peek().Type;
        }

        stream.Expect(TokenType.EndLoop);

        return new LoopNode(times, new ProgramNode(loopBlock), loopItemIdentifier);
    }
}