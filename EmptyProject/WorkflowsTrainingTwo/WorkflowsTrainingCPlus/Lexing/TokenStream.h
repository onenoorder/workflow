#pragma once

#include <queue>
#include <string>
#include <stdexcept>
#include <utility>

#include "TokenType.h"
#include "Token.h"

namespace WorkflowsTraining {
    namespace Lexing {

        class TokenStream {
        private:
            std::queue<Token> tokens;
            Token current;

        public:
            TokenStream(std::queue<Token> initialTokens);

            const Token& Peek() const;

            const Token& PeekNext() const;

            Token Eat();

            void Expect(TokenType type);

            bool EndOfFile() const;
        };
    }
}