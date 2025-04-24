using WorkflowZero.AbstractSyntaxTree.Nodes;
using WorkflowZero.AbstractSyntaxTree.Nodes.Interfaces;
using WorkflowZero.Lexing;

namespace WorkflowZero.Parsing;

public class ParserNew(StreamReader sourceCodeStream)
{
    private readonly TokenStream _stream = new(Lexer.Tokenize(sourceCodeStream));

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