using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;

namespace WorkflowZero.AbstractSyntaxTree.Nodes.Statements;

public class VariableAssignmentNode(IExpressionNode identifier, IExpressionNode value) : IStatementNode
{
    public IExpressionNode Identifier { get; } = identifier;
    public IExpressionNode Value { get; } = value;
}