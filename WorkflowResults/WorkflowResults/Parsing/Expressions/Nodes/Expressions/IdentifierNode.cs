using WorkflowResults.Helpers.Storage;
using WorkflowResults.Parsing.Expressions.Interfaces;

namespace WorkflowResults.Parsing.Expressions.Nodes.Expressions;

public class IdentifierNode(string name) : IExpressionNode
{
    public string Name { get; } = name;

    public object Resolve()
    {
        return VariableStorage.GetVariable(Name);
    }
}