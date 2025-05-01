using DemoBackend.Helpers.Storage;
using DemoBackend.Parsing.Expressions.Interfaces;

namespace DemoBackend.Parsing.Expressions.Nodes.Expressions;

public class IdentifierNode(string name) : IExpressionNode
{
    public string Name { get; } = name;

    public object Resolve()
    {
        return VariableStorage.GetVariable(Name);
    }
}