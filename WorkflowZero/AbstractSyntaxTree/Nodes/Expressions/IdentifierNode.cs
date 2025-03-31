using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;

namespace WorkflowZero.AbstractSyntaxTree.Nodes.Expressions;

public class IdentifierNode(string name) : IExpressionNode
{
    public string Name { get;} = name;
}