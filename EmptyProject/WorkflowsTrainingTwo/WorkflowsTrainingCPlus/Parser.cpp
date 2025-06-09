#include "TokenStream.cpp"
#include "Lexer.cpp"
#include "ProgramNode.cpp"

namespace WorkflowsTraining {
    namespace Parsing {
        class Parser {
        private:
            WorkflowsTraining::Lexing::TokenStream _stream;

        public:
            Parser(std::ifstream& sourceCodeStream)
                : _stream(WorkflowsTraining::Lexing::Lexer::Tokenize(sourceCodeStream)) {
            }

            ProgramNode Parse() {
                std::vector<std::unique_ptr<Statements::IStatementNode>> statements;

                while (!_stream.EndOfFile()) {
                    statements.push_back(Statements::StatementParser::ParseStatement(_stream));
                }

                return ProgramNode(std::move(statements));
            }
        };
    }
}