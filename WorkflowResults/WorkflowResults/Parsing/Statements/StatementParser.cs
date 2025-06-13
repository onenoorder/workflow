using WorkflowResults.Lexing;
using WorkflowResults.Parsing.Statements.IfThenElse;
using WorkflowResults.Parsing.Statements.Interfaces;
using WorkflowResults.Parsing.Statements.Loop;
using WorkflowResults.Parsing.Statements.Print;
using WorkflowResults.Parsing.Statements.VariableAssignment;

namespace WorkflowResults.Parsing.Statements;

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