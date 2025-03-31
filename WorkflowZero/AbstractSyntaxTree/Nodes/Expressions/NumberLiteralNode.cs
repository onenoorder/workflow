
using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;

namespace WorkflowZero.AbstractSyntaxTree.Nodes.Expressions;

public class NumberLiteral (int value) : IExpressionNode
{
    public int Value { get; } = value;
}