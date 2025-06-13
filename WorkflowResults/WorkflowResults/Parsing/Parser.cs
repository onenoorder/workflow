using WorkflowResults.Lexing;
using WorkflowResults.Parsing.Statements;
using WorkflowResults.Parsing.Statements.Interfaces;

namespace WorkflowResults.Parsing;

public class Parser(StreamReader sourceCodeStream)
{
    private readonly TokenStream _stream = Lexer.Tokenize(sourceCodeStream);

    public ProgramNode Parse()
    {
        IList<IStatementNode> statements = [];
        while (!_stream.EndOfFile())
        {
            statements.Add(StatementParser.ParseStatement(_stream));
        }

        return new ProgramNode(statements);
    }
}