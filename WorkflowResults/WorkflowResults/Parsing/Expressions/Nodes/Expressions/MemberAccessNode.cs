using System.Reflection;
using WorkflowResults.Helpers.Users;
using WorkflowResults.Parsing.Expressions.Interfaces;

namespace WorkflowResults.Parsing.Expressions.Nodes.Expressions;

public class MemberAccessNode(IdentifierNode identifier, IdentifierNode memberIdentifier) : IExpressionNode
{
    public IdentifierNode Identifier { get; } = identifier;
    public IdentifierNode MemberIdentifier { get; } = memberIdentifier;

    public object Resolve()
    {
        object identifier = Identifier.Resolve();
        object? value;

        if (identifier.GetType() == typeof(User))
        {
            PropertyInfo? propertyInfo = typeof(User).GetProperty(MemberIdentifier.Name);
            if (propertyInfo != null)
            {
                value = propertyInfo.GetValue(identifier);
            }
            else
            {
                throw new Exception(
                    $"Tried accessing non existing member {MemberIdentifier.Name} on type User");
            }
        }
        else
        {
            throw new Exception(
                $"Tried accessing member {MemberIdentifier.Name} on {identifier.GetType().Name}, only supported for type User");
        }

        if (value == null)
        {
            throw new Exception(
                $"Expected to find value for {Identifier.Name}.{MemberIdentifier.Name} but did not find it.");
        }

        return value;
    }
}