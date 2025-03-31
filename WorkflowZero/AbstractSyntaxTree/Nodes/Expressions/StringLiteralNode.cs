
using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;

namespace WorkflowZero.AbstractSyntaxTree.Nodes.Expressions;

public class StringLiteralNode (string value) : IExpressionNode
{
    public string Value { get; } = value;
}