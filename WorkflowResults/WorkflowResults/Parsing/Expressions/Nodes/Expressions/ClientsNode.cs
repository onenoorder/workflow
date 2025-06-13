using WorkflowResults.Helpers.Clients;
using WorkflowResults.Helpers.Users;
using WorkflowResults.Parsing.Expressions.Interfaces;

namespace WorkflowResults.Parsing.Expressions.Nodes.Expressions;

public class ClientsNode(IdentifierNode memberIdentifier, IList<IExpressionNode> parameters) : IExpressionNode
{
    private IdentifierNode MemberIdentifier { get; } = memberIdentifier;
    private IList<IExpressionNode> Parameters { get; } = parameters;

    public object Resolve()
    {
        object? returnValue = null;
        IList<object>? parameterValues = [];
        foreach (IExpressionNode parameter in Parameters)
        {
            parameterValues.Add(parameter.Resolve());
        }

        returnValue = MemberIdentifier.Name switch
        {
            "Find" => ClientActions.Find((string)parameterValues[0]),
            "All" => ClientActions.All(),
            "Add" => ClientActions.Add((string)parameterValues[0]),
            _ => returnValue
        };

        return returnValue ??
               throw new Exception(
                   $"Tried executing Users.{MemberIdentifier.Name} with parameter {string.Join(", ", parameterValues)} but got null");
    }
}