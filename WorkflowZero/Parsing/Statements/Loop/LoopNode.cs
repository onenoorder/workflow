using WorkflowZero.Helpers.Storage;
using WorkflowZero.Helpers.Users;
using WorkflowZero.Parsing.Expressions.Interfaces;
using WorkflowZero.Parsing.Expressions.Nodes.Expressions;
using WorkflowZero.Parsing.Statements.Interfaces;

namespace WorkflowZero.Parsing.Statements.Loop;

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