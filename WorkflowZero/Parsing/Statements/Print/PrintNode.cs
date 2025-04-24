using WorkflowZero.Parsing.Expressions.Interfaces;
using WorkflowZero.Parsing.Statements.Interfaces;

namespace WorkflowZero.Parsing.Statements.Print;

public class PrintNode(IExpressionNode value) : IStatementNode
{
    private IExpressionNode Value { get; } = value;

    public void Execute()
    {
        Console.WriteLine(Value.Resolve());
    }
}