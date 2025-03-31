
using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;

namespace WorkflowZero.AbstractSyntaxTree.Nodes.Expressions;

public class BooleanLiteralNode (bool value) : IExpressionNode
{
    public bool Value { get; } = value;
}