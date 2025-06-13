using WorkflowResults.Parsing.Expressions.Nodes.BinaryExpressions;
using WorkflowResults.Parsing.Statements.Interfaces;

namespace WorkflowResults.Parsing.Statements.IfThenElse;

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