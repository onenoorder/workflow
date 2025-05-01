using DemoBackend.Lexing;
using DemoBackend.Parsing.Statements.IfThenElse;
using DemoBackend.Parsing.Statements.Interfaces;
using DemoBackend.Parsing.Statements.Loop;
using DemoBackend.Parsing.Statements.Print;
using DemoBackend.Parsing.Statements.VariableAssignment;

namespace DemoBackend.Parsing.Statements;

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