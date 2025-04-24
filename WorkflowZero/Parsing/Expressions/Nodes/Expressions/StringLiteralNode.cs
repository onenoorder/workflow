using WorkflowZero.Parsing.Expressions.Interfaces;

namespace WorkflowZero.Parsing.Expressions.Nodes.Expressions;

public class StringLiteralNode(string value) : IExpressionNode
{
    private string Value { get; } = value;

    public object Resolve()
    {
        return Value;
    }
}