using WorkflowZero.Lexing;
using WorkflowZero.Parsing.Expressions;
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
        return new PrintNode(ExpressionParser.ParseArgument(stream) ?? new StringLiteralNode(""));
    }
}