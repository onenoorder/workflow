using WorkflowsTraining.Lexing;
using WorkflowsTraining.Parsing.Statements;
using WorkflowsTraining.Parsing.Statements.Interfaces;

namespace WorkflowsTraining.Parsing;

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