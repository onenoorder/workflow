using WorkflowsTraining.Lexing;
using WorkflowsTraining.Parsing.Expressions;
using WorkflowsTraining.Parsing.Expressions.Interfaces;
using WorkflowsTraining.Parsing.Expressions.Nodes.Expressions;
using WorkflowsTraining.Parsing.Statements.Interfaces;

namespace WorkflowsTraining.Parsing.Statements.Print;

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