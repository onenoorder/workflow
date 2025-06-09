#pragma once

#include <string>
#include "TokenType.h"

namespace WorkflowsTraining {
    namespace Lexing {
        class Token {
        public:
            std::string Value;
            TokenType Type;
            int LineIndex;
            int CharIndex;

            Token(std::string value, TokenType type, int lineIndex, int charIndex);

            Token();

            bool operator==(const Token& other) const;

            bool operator!=(const Token& other) const;

            std::string ToString() const;
        };
    }
}