using WorkflowsTraining.Lexing;

namespace WorkflowsTraining.Parsing.Statements.Interfaces;

public interface IParserRule
{
    bool CanParse(TokenStream stream);
    IStatementNode Parse(TokenStream stream);
}