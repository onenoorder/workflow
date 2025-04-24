using WorkflowZero.Parsing.Expressions.Interfaces;

namespace WorkflowZero.Parsing.Expressions.Nodes.Expressions;

public class BooleanLiteralNode(bool value) : IExpressionNode
{
    private bool Value { get; } = value;

    public object Resolve()
    {
        return Value;
    }
}