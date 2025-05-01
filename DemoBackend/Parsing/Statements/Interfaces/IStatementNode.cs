using DemoBackend.Helpers.Output;
using DemoBackend.Parsing.Interfaces;

namespace DemoBackend.Parsing.Statements.Interfaces;

public interface IStatementNode : IAstNode
{
    public void Execute(Output output);
}