using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;
using WorkflowZero.Lexing;
using WorkflowZero.Parsing.IfThenElseStatement;
using WorkflowZero.Parsing.Interfaces;

namespace WorkflowZero.Parsing;

public static class StatementParser
{
    private static readonly List<IParserRule> _rules =
    [
        new VariableAssignmentRule(),
        new PrintRule(),
        new IfThenElseRule(),
        new LoopRule()
    ];
    public static IStatementNode ParseStatement(TokenStream stream)
    {
        IParserRule? rule = _rules.FirstOrDefault(r => r.CanParse(stream));
        if (rule == null)
            throw new Exception($"Unexpected token: {stream.Peek().Type}");

        return rule.Parse(stream);
    }
}