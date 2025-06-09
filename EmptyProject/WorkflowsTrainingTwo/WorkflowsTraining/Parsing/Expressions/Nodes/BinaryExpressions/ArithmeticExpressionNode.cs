using WorkflowsTraining.Parsing.Expressions.Interfaces;

namespace WorkflowsTraining.Parsing.Expressions.Nodes.BinaryExpressions;

public class ArithmeticExpressionNode(string operatorString, IExpressionNode left, IExpressionNode right)
    : IBinaryExpressionNode
{
    private string OperatorString { get; } = operatorString;
    private IExpressionNode Left { get; } = left;
    private IExpressionNode Right { get; } = right;

    public object Resolve()
    {
        int leftValue = (int)Left.Resolve();
        int rightValue = (int)Right.Resolve();

        return OperatorString switch
        {
            "+" => leftValue + rightValue,
            "-" => leftValue - rightValue,
            "/" => leftValue / rightValue,
            "*" => leftValue * rightValue,
            _ => throw new Exception($"Unexpected value while evaluating arithmetic expression")
        };
    }
}