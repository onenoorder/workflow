using WorkflowZero.Lexing;
using WorkflowZero.Parsing.Statements.IfThenElse;
using WorkflowZero.Parsing.Statements.Interfaces;
using WorkflowZero.Parsing.Statements.Loop;
using WorkflowZero.Parsing.Statements.Print;
using WorkflowZero.Parsing.Statements.VariableAssignment;

namespace WorkflowZero.Parsing.Statements;

public static class StatementParser
{
    private static readonly List<IParserRule> Rules =
    [
        new VariableAssignmentRule(),
        new PrintRule(),
        new IfThenElseRule(),
        new LoopRule()
    ];

    public static IStatementNode ParseStatement(TokenStream stream)
    {
        IParserRule? rule = Rules.FirstOrDefault(r => r.CanParse(stream));
        if (rule == null)
            throw new Exception($"Unexpected token: {stream.Peek().Type}");

        return rule.Parse(stream);
    }
}