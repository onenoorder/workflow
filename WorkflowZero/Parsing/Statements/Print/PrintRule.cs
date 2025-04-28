using WorkflowZero.Lexing;
using WorkflowZero.Parsing.Expressions;
using WorkflowZero.Parsing.Expressions.Interfaces;
using WorkflowZero.Parsing.Expressions.Nodes.Expressions;
using WorkflowZero.Parsing.Statements.Interfaces;

namespace WorkflowZero.Parsing.Statements.Print;

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