using WorkflowResults.Helpers.Storage;
using WorkflowResults.Helpers.Users;
using WorkflowResults.Parsing.Expressions.Interfaces;
using WorkflowResults.Parsing.Expressions.Nodes.Expressions;
using WorkflowResults.Parsing.Statements.Interfaces;

namespace WorkflowResults.Parsing.Statements.Loop;

public class LoopNode(IExpressionNode expressionNode, ProgramNode loopBlock, IdentifierNode? loopItemIdentifier)
    : IStatementNode
{
    private IExpressionNode Times { get; } = expressionNode;
    private ProgramNode Loop { get; } = loopBlock;
    private IdentifierNode? LoopItemIdentifier { get; } = loopItemIdentifier;

    public void Execute()
    {
        object value = Times.Resolve();
        string? loopItemIdentifier = LoopItemIdentifier?.Name;

        if (value is int numberOfTimes)
        {
            for (int i = 0; i < numberOfTimes; i++)
            {
                if (loopItemIdentifier != null)
                {
                    VariableStorage.StoreVariable(loopItemIdentifier, i);
                }

                Loop.Execute();
            }
        }
        else if (value is IList<User> list)
        {
            foreach (User loopItem in list)
            {
                if (loopItemIdentifier != null)
                {
                    VariableStorage.StoreVariable(loopItemIdentifier, loopItem);
                }

                Loop.Execute();
            }
        }
        else
        {
            throw new Exception($"Could not loop {value}");
        }
    }
}