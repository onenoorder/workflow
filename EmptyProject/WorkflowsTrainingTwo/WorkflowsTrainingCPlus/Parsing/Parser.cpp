#include "Parser.h"
#include "Statements/IfThenElse/IfThenElseNode.h"

using namespace WorkflowsTraining::Parsing;
using namespace WorkflowsTraining::Lexing;
using namespace WorkflowsTraining::Parsing::Statements;

Parser::Parser(std::ifstream& sourceCodeStream)
    : _stream(Lexer::Tokenize(sourceCodeStream)) {
}

ProgramNode Parser::Parse() {
    std::vector<std::unique_ptr<IStatementNode>> statements;

    while (!_stream.EndOfFile()) {
        statements.push_back(Statements::StatementParser::ParseStatement(_stream));
    }

    return ProgramNode(std::move(statements));
}