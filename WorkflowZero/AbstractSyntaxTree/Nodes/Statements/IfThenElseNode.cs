using WorkflowZero.AbstractSyntaxTree.Nodes.BinaryExpressions;
using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;

namespace WorkflowZero.AbstractSyntaxTree.Nodes.Statements;

public class IfThenElseNode(BooleanExpressionNode ifCondition, ProgramNode thenBlock, ProgramNode? elseBlock) : IStatementNode
{
    public BooleanExpressionNode IfCondition { get; } = ifCondition;
    public ProgramNode Then { get; } = thenBlock;
    public ProgramNode? Else { get; } = elseBlock;
}