using WorkflowsTraining.Helpers.Clients;
using WorkflowsTraining.Helpers.Storage;
using WorkflowsTraining.Helpers.Users;
using WorkflowsTraining.Parsing.Expressions.Interfaces;
using WorkflowsTraining.Parsing.Expressions.Nodes.Expressions;
using WorkflowsTraining.Parsing.Statements.Interfaces;

namespace WorkflowsTraining.Parsing.Statements.Loop;

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
        else if (value is IList<User> users)
        {
            foreach (User loopItem in users)
            {
                if (loopItemIdentifier != null)
                {
                    VariableStorage.StoreVariable(loopItemIdentifier, loopItem);
                }

                Loop.Execute();
            }
        } else if (value is IList<Client> clients) {
            foreach (Client loopItem in clients) {
                if (loopItemIdentifier != null) {
                    VariableStorage.StoreVariable(loopItemIdentifier, loopItem);
                }

                Loop.Execute();
            }
        } else
        {
            throw new Exception($"Could not loop {value}");
        }
    }
}