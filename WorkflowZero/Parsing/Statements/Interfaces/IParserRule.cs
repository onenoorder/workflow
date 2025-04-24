using WorkflowZero.Lexing;

namespace WorkflowZero.Parsing.Statements.Interfaces;

public interface IParserRule
{
    bool CanParse(TokenStream stream);
    IStatementNode Parse(TokenStream stream);
}