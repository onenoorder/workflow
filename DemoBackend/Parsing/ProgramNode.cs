using DemoBackend.Helpers.Output;
using DemoBackend.Parsing.Interfaces;
using DemoBackend.Parsing.Statements.Interfaces;

namespace DemoBackend.Parsing;

public class ProgramNode(IList<IStatementNode> statements) : IAstNode
{
    private IList<IStatementNode> Statements { get; } = statements;

    public void Execute(Output output)
    {
        foreach (IStatementNode statement in Statements)
        {
            statement.Execute(output);
        }
    }
}