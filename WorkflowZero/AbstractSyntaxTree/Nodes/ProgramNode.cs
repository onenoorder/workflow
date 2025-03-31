using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;

namespace WorkflowZero.AbstractSyntaxTree.Nodes;

public class ProgramNode(IList<IStatementNode> statements) : IAstNode
{
    public IList<IStatementNode> Statements { get; set; } = statements;
}