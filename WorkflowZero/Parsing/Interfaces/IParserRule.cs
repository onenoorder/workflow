using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;
using WorkflowZero.Lexing;

namespace WorkflowZero.Parsing.Interfaces;

public interface IParserRule
{
    bool CanParse(TokenStream stream);
    IStatementNode Parse(TokenStream stream);
}