using WorkflowResults.Parsing.Interfaces;
using WorkflowResults.Parsing.Statements.Interfaces;

namespace WorkflowResults.Parsing;

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