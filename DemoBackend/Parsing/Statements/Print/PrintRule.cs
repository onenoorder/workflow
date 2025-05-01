using DemoBackend.Lexing;
using DemoBackend.Parsing.Expressions;
using DemoBackend.Parsing.Expressions.Interfaces;
using DemoBackend.Parsing.Expressions.Nodes.Expressions;
using DemoBackend.Parsing.Statements.Interfaces;

namespace DemoBackend.Parsing.Statements.Print;

public class PrintRule : IParserRule
{
    public bool CanParse(TokenStream stream)
    {
        return stream.Peek().Type.Equals(TokenType.Print);
    }

    public IStatementNode Parse(TokenStream stream)
    {
        stream.Eat();
        IList<IExpressionNode> arguments = ExpressionParser.ParseArguments(stream);
        return new PrintNode(arguments.Count == 1 ? arguments[0] : new StringLiteralNode(""));
    }
}