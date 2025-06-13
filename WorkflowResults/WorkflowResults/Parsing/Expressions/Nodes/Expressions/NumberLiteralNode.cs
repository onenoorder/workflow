using WorkflowResults.Parsing.Expressions.Interfaces;

namespace WorkflowResults.Parsing.Expressions.Nodes.Expressions;

public class NumberLiteral(int value) : IExpressionNode
{
    private int Value { get; } = value;

    public object Resolve()
    {
        return Value;
    }
}