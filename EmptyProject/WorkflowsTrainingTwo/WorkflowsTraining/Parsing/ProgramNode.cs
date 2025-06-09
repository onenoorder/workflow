using WorkflowsTraining.Parsing.Interfaces;
using WorkflowsTraining.Parsing.Statements.Interfaces;

namespace WorkflowsTraining.Parsing;

public class ProgramNode(IList<IStatementNode> statements) : IAstNode
{
    private IList<IStatementNode> Statements { get; } = statements;

    public void Execute()
    {
        foreach (IStatementNode statement in Statements)
        {
            statement.Execute();
        }
    }
}