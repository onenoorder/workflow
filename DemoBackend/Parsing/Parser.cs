using DemoBackend.Lexing;
using DemoBackend.Parsing.Statements;
using DemoBackend.Parsing.Statements.Interfaces;

namespace DemoBackend.Parsing;

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