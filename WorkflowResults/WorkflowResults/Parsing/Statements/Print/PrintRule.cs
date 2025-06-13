using WorkflowResults.Lexing;
using WorkflowResults.Parsing.Expressions;
using WorkflowResults.Parsing.Expressions.Interfaces;
using WorkflowResults.Parsing.Expressions.Nodes.Expressions;
using WorkflowResults.Parsing.Statements.Interfaces;

namespace WorkflowResults.Parsing.Statements.Print;

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