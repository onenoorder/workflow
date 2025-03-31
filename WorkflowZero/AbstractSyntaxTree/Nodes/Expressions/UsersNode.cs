using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;

namespace WorkflowZero.AbstractSyntaxTree.Nodes.Expressions;

public class MemberAccessNode(IdentifierNode identifier, IdentifierNode memberIdentifier) : IExpressionNode
{
    public IdentifierNode Identifier { get; } = identifier;
    public IdentifierNode MemberIdentifier { get; } = memberIdentifier;
}