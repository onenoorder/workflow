using DemoBackend.Lexing;

namespace DemoBackend.Parsing.Statements.Interfaces;

public interface IParserRule
{
    bool CanParse(TokenStream stream);
    IStatementNode Parse(TokenStream stream);
}