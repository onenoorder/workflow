using WorkflowZero.Helpers.Users;
using WorkflowZero.Parsing.Expressions.Interfaces;

namespace WorkflowZero.Parsing.Expressions.Nodes.Expressions;

public class UsersNode(IdentifierNode memberIdentifier, IExpressionNode parameter) : IExpressionNode
{
    private IdentifierNode MemberIdentifier { get; } = memberIdentifier;
    private IExpressionNode? Parameter { get; } = parameter;

    public object Resolve()
    {
        object? returnValue = null;
        object? parameter = Parameter?.Resolve();

        returnValue = MemberIdentifier.Name switch
        {
            "Find" => UsersActions.Find((string)parameter),
            "All" => UsersActions.All(),
            _ => returnValue
        };

        return returnValue ??
               throw new Exception(
                   $"Tried executing Users.{MemberIdentifier.Name} with parameter {parameter} but got null");
    }
}