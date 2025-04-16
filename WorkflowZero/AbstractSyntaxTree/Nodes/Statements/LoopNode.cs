using WorkflowZero.AbstractSyntaxTree.Nodes.Expressions;
using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;

namespace WorkflowZero.AbstractSyntaxTree.Nodes.Statements;

public class LoopNode(IExpressionNode expressionNode, ProgramNode loopBlock, IdentifierNode? loopItemIdentifier) : IStatementNode
{
    public IExpressionNode Times { get; } = expressionNode;
    public ProgramNode Loop { get; } = loopBlock;
    public IdentifierNode? LoopItemIdentifier { get; } = loopItemIdentifier;
}