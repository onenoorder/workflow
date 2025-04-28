using WorkflowZero.Parsing.Expressions.Interfaces;

namespace WorkflowZero.Parsing.Expressions.Nodes.BinaryExpressions;

public class BooleanExpressionNode(string operatorString, IExpressionNode left, IExpressionNode right)
    : IBinaryExpressionNode
{
    private string OperatorString { get; } = operatorString;
    private IExpressionNode Left { get; } = left;
    private IExpressionNode Right { get; } = right;

    public object Resolve()
    {
        object leftValue = Left.Resolve();
        object rightValue = Right.Resolve();

        return OperatorString switch
        {
            "equals" => leftValue.Equals(rightValue),
            ">" => (int)leftValue > (int)rightValue,
            "<" => (int)leftValue < (int)rightValue,
            _ => throw new Exception($"Unexpected value while evaluating boolean expression")
        };
    }
}