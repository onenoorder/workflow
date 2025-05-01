using DemoBackend.Helpers.Output;
using DemoBackend.Helpers.Storage;
using DemoBackend.Helpers.Users;
using DemoBackend.Parsing.Expressions.Interfaces;
using DemoBackend.Parsing.Expressions.Nodes.Expressions;
using DemoBackend.Parsing.Statements.Interfaces;

namespace DemoBackend.Parsing.Statements.Loop;

public class LoopNode(IExpressionNode expressionNode, ProgramNode loopBlock, IdentifierNode? loopItemIdentifier)
    : IStatementNode
{
    private IExpressionNode Times { get; } = expressionNode;
    private ProgramNode Loop { get; } = loopBlock;
    private IdentifierNode? LoopItemIdentifier { get; } = loopItemIdentifier;

    public void Execute(Output output)
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

                Loop.Execute(output);
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

                Loop.Execute(output);
            }
        }
        else
        {
            throw new Exception($"Could not loop {value}");
        }
    }
}