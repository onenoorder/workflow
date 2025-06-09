using System.Reflection;
using WorkflowsTraining.Helpers.Storage;
using WorkflowsTraining.Helpers.Users;
using WorkflowsTraining.Parsing.Expressions.Interfaces;
using WorkflowsTraining.Parsing.Expressions.Nodes.Expressions;
using WorkflowsTraining.Parsing.Statements.Interfaces;

namespace WorkflowsTraining.Parsing.Statements.VariableAssignment;

public class VariableAssignmentNode(IExpressionNode identifier, IExpressionNode value) : IStatementNode
{
    private IExpressionNode Identifier { get; } = identifier;
    private IExpressionNode Value { get; } = value;

    public void Execute()
    {
        object value = Value.Resolve();

        switch (Identifier)
        {
            case MemberAccessNode node:
                AssignMemberVariable(node, value);
                break;
            case IdentifierNode node:
                VariableStorage.StoreVariable(node.Name, value);
                break;
            default:
                throw new Exception("Could not assign variable.");
        }
    }

    private void AssignMemberVariable(MemberAccessNode memberAccessNode, object value)
    {
        object identifier = memberAccessNode.Identifier.Resolve();

        if (identifier.GetType() == typeof(User))
        {
            PropertyInfo? propertyInfo = typeof(User).GetProperty(memberAccessNode.MemberIdentifier.Name);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(identifier, value);
            }
            else
            {
                throw new Exception(
                    $"Tried accessing non existing member {memberAccessNode.MemberIdentifier.Name} on type User");
            }
        }
        else
        {
            throw new Exception(
                $"Tried accessing member {memberAccessNode.MemberIdentifier.Name} on {identifier.GetType().Name}, only supported for type User");
        }
    }
}