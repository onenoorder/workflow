using WorkflowsTraining.Lexing;
using WorkflowsTraining.Parsing.Statements.IfThenElse;
using WorkflowsTraining.Parsing.Statements.Interfaces;
using WorkflowsTraining.Parsing.Statements.Loop;
using WorkflowsTraining.Parsing.Statements.Print;
using WorkflowsTraining.Parsing.Statements.VariableAssignment;

namespace WorkflowsTraining.Parsing.Statements;

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