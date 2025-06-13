using WorkflowResults.Lexing;

namespace WorkflowResults.Parsing.Statements.Interfaces;

public interface IParserRule
{
    bool CanParse(TokenStream stream);
    IStatementNode Parse(TokenStream stream);
}