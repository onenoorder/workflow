using WorkflowsTraining.Parsing.Expressions.Interfaces;

namespace WorkflowsTraining.Parsing.Expressions.Nodes.Expressions;

public class StringConcatenationNode(IList<IExpressionNode> strings) : IExpressionNode
{
    private IList<IExpressionNode> Strings { get; } = strings;

    public object Resolve()
    {
        return Strings.Aggregate("", (current, stringNode) => current + stringNode.Resolve());
    }
}