using WorkflowZero.Helpers.Users;
using WorkflowZero.Parsing.Expressions.Interfaces;

namespace WorkflowZero.Parsing.Expressions.Nodes.Expressions;

public class UsersNode(IdentifierNode memberIdentifier, IList<IExpressionNode> parameters) : IExpressionNode
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
            "Find" => UsersActions.Find((string)parameterValues[0]),
            "All" => UsersActions.All(),
            "Add" => UsersActions.Add((string)parameterValues[0], (string)parameterValues[1], (int)parameterValues[2]),
            "GetInitial" => UsersActions.GetInitial((User) parameterValues[0]),
            _ => returnValue
        };

        return returnValue ??
               throw new Exception(
                   $"Tried executing Users.{MemberIdentifier.Name} with parameter {string.Join(", ", parameterValues)} but got null");
    }
}