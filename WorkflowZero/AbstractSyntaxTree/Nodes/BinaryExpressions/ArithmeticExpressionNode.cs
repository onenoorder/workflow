
using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;

namespace WorkflowZero.AbstractSyntaxTree.Nodes.BinaryExpressions;

public class ArithmeticExpressionNode (string operatorString, IExpressionNode left, IExpressionNode right) : IBinaryExpressionNode
{
    public string OperatorString { get; } = operatorString;
    public IExpressionNode Left { get; } = left;
    public IExpressionNode Right { get; } = right;
}