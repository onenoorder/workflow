using WorkflowsTraining.Parsing.Interfaces;

namespace WorkflowsTraining.Parsing.Statements.Interfaces;

public interface IStatementNode : IAstNode
{
    public void Execute();
}