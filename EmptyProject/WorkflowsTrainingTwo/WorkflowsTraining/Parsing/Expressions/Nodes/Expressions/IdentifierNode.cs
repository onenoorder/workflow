using WorkflowsTraining.Helpers.Storage;
using WorkflowsTraining.Parsing.Expressions.Interfaces;

namespace WorkflowsTraining.Parsing.Expressions.Nodes.Expressions;

public class IdentifierNode(string name) : IExpressionNode
{
    public string Name { get; } = name;

    public object Resolve()
    {
        return VariableStorage.GetVariable(Name);
    }
}