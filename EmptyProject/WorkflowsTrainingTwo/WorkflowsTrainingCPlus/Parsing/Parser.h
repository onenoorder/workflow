#pragma once

#include "ProgramNode.h"
#include "../Lexing/TokenStream.h"
#include "../Lexing/Lexer.h"
#include "Statements/Interfaces/IStatementNode.h"
#include "Statements/StatementParser.h"

using namespace WorkflowsTraining::Lexing;

namespace WorkflowsTraining {
    namespace Parsing {
        class Parser {
        private:
            TokenStream _stream;

        public:
            Parser(std::ifstream& sourceCodeStream);

            ProgramNode Parse();
        };
    }
}