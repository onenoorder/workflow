#include "Token.h"

namespace WorkflowsTraining {
    namespace Lexing {

        Token::Token(std::string value, TokenType type, int lineIndex, int charIndex)
            : Value(std::move(value)), Type(type), LineIndex(lineIndex), CharIndex(charIndex) {
        }

        Token::Token() : Value(""), Type(TokenType::Unknown), LineIndex(0), CharIndex(0) {}

        bool Token::operator==(const Token& other) const {
            return Value == other.Value &&
                Type == other.Type &&
                LineIndex == other.LineIndex &&
                CharIndex == other.CharIndex;
        }

        bool Token::operator!=(const Token& other) const {
            return !(*this == other);
        }

        std::string Token::ToString() const {
            return "Token { Value: \"" + Value + "\", Type: " + std::to_string(static_cast<int>(Type)) +
                ", Line: " + std::to_string(LineIndex) + ", Char: " + std::to_string(CharIndex) + " }";
        }
    }
}
