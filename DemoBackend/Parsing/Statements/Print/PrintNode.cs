using DemoBackend.Helpers.Output;
using DemoBackend.Parsing.Expressions.Interfaces;
using DemoBackend.Parsing.Statements.Interfaces;

namespace DemoBackend.Parsing.Statements.Print;

public class PrintNode(IExpressionNode value) : IStatementNode
{
    private IExpressionNode Value { get; } = value;

    public void Execute(Output output)
    {
        output.WriteLine(Value.Resolve().ToString() ?? string.Empty);
    }
}