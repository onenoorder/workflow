using WorkflowsTraining.Parsing.Expressions.Interfaces;
using WorkflowsTraining.Parsing.Statements.Interfaces;

namespace WorkflowsTraining.Parsing.Statements.Print;

public class PrintNode(IExpressionNode value) : IStatementNode
{
    private IExpressionNode Value { get; } = value;

    public void Execute()
    {
        Console.WriteLine(Value.Resolve());
    }
}