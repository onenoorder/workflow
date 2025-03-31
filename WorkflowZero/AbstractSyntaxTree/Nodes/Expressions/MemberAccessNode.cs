using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;

namespace WorkflowZero.AbstractSyntaxTree.Nodes.Expressions;

public class UsersNode(IdentifierNode memberIdentifier, IExpressionNode parameter) : IExpressionNode
{
    public IdentifierNode MemberIdentifier { get; } = memberIdentifier;
    public IExpressionNode? Parameter { get; } = parameter;
}