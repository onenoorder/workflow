using WorkflowResults.Parsing.Expressions.Interfaces;
using WorkflowResults.Parsing.Statements.Interfaces;

namespace WorkflowResults.Parsing.Statements.Print;

public class PrintNode(IExpressionNode value) : IStatementNode
{
    private IExpressionNode Value { get; } = value;

    public void Execute()
    {
        Console.WriteLine(Value.Resolve());
    }
}