using DemoBackend.Helpers.Output;
using DemoBackend.Parsing.Expressions.Nodes.BinaryExpressions;
using DemoBackend.Parsing.Statements.Interfaces;

namespace DemoBackend.Parsing.Statements.IfThenElse;

public class IfThenElseNode(BooleanExpressionNode ifCondition, ProgramNode thenBlock, ProgramNode? elseBlock)
    : IStatementNode
{
    private BooleanExpressionNode IfCondition { get; } = ifCondition;
    private ProgramNode Then { get; } = thenBlock;
    private ProgramNode? Else { get; } = elseBlock;

    public void Execute(Output output)
    {
        if ((bool)IfCondition.Resolve())
        {
            Then.Execute(output);
        }
        else
        {
            Else?.Execute(output);
        }
    }
}