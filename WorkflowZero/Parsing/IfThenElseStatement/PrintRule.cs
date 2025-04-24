using WorkflowZero.AbstractSyntaxTree.Nodes;
using WorkflowZero.AbstractSyntaxTree.Nodes.BinaryExpressions;
using WorkflowZero.AbstractSyntaxTree.Nodes.Expressions;
using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;
using WorkflowZero.AbstractSyntaxTree.Nodes.Statements;
using WorkflowZero.Lexing;
using WorkflowZero.Parsing.Interfaces;

namespace WorkflowZero.Parsing.IfThenElseStatement;

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