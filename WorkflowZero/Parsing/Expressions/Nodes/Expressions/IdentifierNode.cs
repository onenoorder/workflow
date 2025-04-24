using WorkflowZero.Helpers.Storage;
using WorkflowZero.Parsing.Expressions.Interfaces;

namespace WorkflowZero.Parsing.Expressions.Nodes.Expressions;

public class IdentifierNode(string name) : IExpressionNode
{
    public string Name { get; } = name;

    public object Resolve()
    {
        return VariableStorage.GetVariable(Name);
    }
}