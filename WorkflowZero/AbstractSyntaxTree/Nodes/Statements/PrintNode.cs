using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;

namespace WorkflowZero.AbstractSyntaxTree.Nodes.Statements;

public class PrintNode(IExpressionNode value) : IStatementNode
{
    public IExpressionNode Value { get; } = value;
}