using WorkflowZero.Parsing.Interfaces;
using WorkflowZero.Parsing.Statements.Interfaces;

namespace WorkflowZero.Parsing;

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