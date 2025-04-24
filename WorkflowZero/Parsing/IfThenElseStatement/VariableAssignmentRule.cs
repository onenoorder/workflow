using WorkflowZero.AbstractSyntaxTree.Nodes;
using WorkflowZero.AbstractSyntaxTree.Nodes.BinaryExpressions;
using WorkflowZero.AbstractSyntaxTree.Nodes.Expressions;
using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;
using WorkflowZero.AbstractSyntaxTree.Nodes.Statements;
using WorkflowZero.Lexing;
using WorkflowZero.Parsing.Interfaces;

namespace WorkflowZero.Parsing.IfThenElseStatement;

public class VariableAssignmentRule : IParserRule
{
    public bool CanParse(TokenStream stream)
    {
        return stream.PeekNext().Type is TokenType.EqualSign or TokenType.MemberAccessOperator &&
         stream.Peek().Type is TokenType.Identifier;
    }

    public IStatementNode Parse(TokenStream stream)
    {
        IExpressionNode returnNode = ExpressionParser.ParseIdentifierExpression(stream.Eat(), stream);
        stream.Expect(TokenType.EqualSign);
        IExpressionNode value = ExpressionParser.ParseExpression(stream.Eat(),stream);

        return new VariableAssignmentNode(returnNode, value);
    }
}