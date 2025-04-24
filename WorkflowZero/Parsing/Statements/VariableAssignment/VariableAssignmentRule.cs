using WorkflowZero.Lexing;
using WorkflowZero.Parsing.Expressions;
using WorkflowZero.Parsing.Expressions.Interfaces;
using WorkflowZero.Parsing.Statements.Interfaces;

namespace WorkflowZero.Parsing.Statements.VariableAssignment;

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
        IExpressionNode value = ExpressionParser.ParseExpression(stream.Eat(), stream);

        return new VariableAssignmentNode(returnNode, value);
    }
}