using WorkflowResults.Parsing.Interfaces;

namespace WorkflowResults.Parsing.Statements.Interfaces;

public interface IStatementNode : IAstNode
{
    public void Execute();
}