using WorkflowZero.Parsing.Expressions.Nodes.BinaryExpressions;
using WorkflowZero.Parsing.Statements.Interfaces;

namespace WorkflowZero.Parsing.Statements.IfThenElse;

public class IfThenElseNode(BooleanExpressionNode ifCondition, ProgramNode thenBlock, ProgramNode? elseBlock)
    : IStatementNode
{
    private BooleanExpressionNode IfCondition { get; } = ifCondition;
    private ProgramNode Then { get; } = thenBlock;
    private ProgramNode? Else { get; } = elseBlock;

    public void Execute()
    {
        if ((bool)IfCondition.Resolve())
        {
            Then.Execute();
        }
        else
        {
            Else?.Execute();
        }
    }
}