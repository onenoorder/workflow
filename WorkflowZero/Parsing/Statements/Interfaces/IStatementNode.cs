using WorkflowZero.Parsing.Interfaces;

namespace WorkflowZero.Parsing.Statements.Interfaces;

public interface IStatementNode : IAstNode
{
    public void Execute();
}