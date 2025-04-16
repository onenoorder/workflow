using System.Reflection;
using WorkflowZero.AbstractSyntaxTree.Nodes;
using WorkflowZero.AbstractSyntaxTree.Nodes.BinaryExpressions;
using WorkflowZero.AbstractSyntaxTree.Nodes.Expressions;
using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;
using WorkflowZero.AbstractSyntaxTree.Nodes.Statements;
using WorkflowZero.Storage;
using WorkflowZero.Users;

namespace WorkflowZero.Interpreting;

public class Interpreter
{
    public void ExecuteProgram(ProgramNode programNode)
    {
        foreach (IStatementNode statement in programNode.Statements)
        {
            ExecuteStatement(statement);
        }
    }

    private void ExecuteStatement(IStatementNode statementNode)
    {
        switch (statementNode)
        {
            case IfThenElseNode ifThenElse:
                EvaluateIfThenElse(ifThenElse);
                break;
            case LoopNode loop:
                EvaluateLoop(loop);
                break;
            case PrintNode printNode:
                Console.WriteLine(EvaluateExpression(printNode.Value));
                break;
            case VariableAssignmentNode variableAssignment:
                EvaluateVariableAssignment(variableAssignment);
                break;
        }
    }

    private void EvaluateLoop(LoopNode loop)
    {
        object value = EvaluateExpression(loop.Times);
        string? loopItemIdentifier = loop.LoopItemIdentifier?.Name;

        if (value is int numberOfTimes)
        {
            for (int i = 0; i < numberOfTimes; i++)
            {
                if (loopItemIdentifier != null)
                {
                    VariableStorage.StoreVariable(loopItemIdentifier, i );
                }

                ExecuteProgram(loop.Loop);
            }
        }
        else if (value is IList<User> list)
        {
            foreach (var loopItem in list)
            {
                if (loopItemIdentifier != null)
                {
                    VariableStorage.StoreVariable(loopItemIdentifier, loopItem );
                }

                ExecuteProgram(loop.Loop);
            }
        }
        else
        {
            throw new Exception($"Could not loop {value}");
        }
    }

    private void EvaluateVariableAssignment(VariableAssignmentNode variableAssignment)
    {
        object value = EvaluateExpression(variableAssignment.Value);

        switch (variableAssignment.Identifier)
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
        object identifier = EvaluateExpression(memberAccessNode.Identifier);

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

    private void EvaluateIfThenElse(IfThenElseNode ifThenElse)
    {
        if (EvaluateBooleanExpression(ifThenElse.IfCondition))
        {
            ExecuteProgram(ifThenElse.Then);
        }
        else
        {
            if (ifThenElse.Else != null)
            {
                ExecuteProgram(ifThenElse.Else);
            }
        }
    }

    private bool EvaluateBooleanExpression(BooleanExpressionNode expressionNode)
    {
        object leftValue = EvaluateExpression(expressionNode.Left);
        object rightValue = EvaluateExpression(expressionNode.Right);

        return expressionNode.OperatorString switch
        {
            "equals" => leftValue == rightValue,
            ">" => (int)leftValue > (int)rightValue,
            "<" => (int)leftValue < (int)rightValue,
            _ => throw new Exception($"Unexpected value while evaluating boolean expression")
        };
    }

    private object EvaluateExpression(IExpressionNode expressionNode)
    {
        return expressionNode switch
        {
            ArithmeticExpressionNode arithmeticExpressionNode => EvaluateArithmeticExpression(arithmeticExpressionNode),
            StringLiteralNode stringLiteralNode => stringLiteralNode.Value,
            NumberLiteral numberLiteral => numberLiteral.Value,
            MemberAccessNode memberAccessNode => EvaluateMemberAccess(memberAccessNode),
            UsersNode usersNode => EvaluateUsersClassAction(usersNode),
            IdentifierNode identifierNode => VariableStorage.GetVariable(identifierNode.Name),
            _ => throw new Exception($"Unexpected value {expressionNode}")
        };
    }

    private object EvaluateUsersClassAction(UsersNode usersNode)
    {
        object? returnValue = null;
        object? parameter = usersNode.Parameter != null ? EvaluateExpression(usersNode.Parameter) : null;

        if (usersNode.MemberIdentifier.Name == "Find")
        {
            returnValue = UsersActions.Find((string)parameter);
        }

        if (usersNode.MemberIdentifier.Name == "All")
        {
            returnValue = UsersActions.All();
        }

        return returnValue ??
               throw new Exception(
                   $"Tried executing Users.{usersNode.MemberIdentifier.Name} with parameter {parameter} but got null");
    }

    private object EvaluateMemberAccess(MemberAccessNode memberAccessNode)
    {
        object identifier = EvaluateExpression(memberAccessNode.Identifier);
        object? value;

        if (identifier.GetType() == typeof(User))
        {
            PropertyInfo? propertyInfo = typeof(User).GetProperty(memberAccessNode.MemberIdentifier.Name);
            if (propertyInfo != null)
            {
                value = propertyInfo.GetValue(identifier);
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

        if (value == null)
        {
            throw new Exception(
                $"Expected to find value for {memberAccessNode.Identifier.Name}.{memberAccessNode.MemberIdentifier.Name} but did not find it.");
        }

        return value;
    }

    private object EvaluateArithmeticExpression(ArithmeticExpressionNode arithmeticExpressionNode)
    {
        int leftValue = (int)EvaluateExpression(arithmeticExpressionNode.Left);
        int rightValue = (int)EvaluateExpression(arithmeticExpressionNode.Right);

        return arithmeticExpressionNode.OperatorString switch
        {
            "+" => leftValue + rightValue,
            "-" => leftValue - rightValue,
            "/" => leftValue / rightValue,
            "*" => leftValue * rightValue,
            _ => throw new Exception($"Unexpected value while evaluating arithmetic expression")
        };
    }
}