#include <string>
#include "TokenType.cpp"

namespace WorkflowsTraining {
    namespace Lexing {
        class Token {
        public:
            std::string Value;
            TokenType Type;
            int LineIndex;
            int CharIndex;

            Token(std::string value, TokenType type, int lineIndex, int charIndex)
                : Value(std::move(value)), Type(type), LineIndex(lineIndex), CharIndex(charIndex) {
            }

            Token() : Value(""), Type(TokenType::Unknown), LineIndex(0), CharIndex(0) {}

            bool operator==(const Token& other) const {
                return Value == other.Value &&
                    Type == other.Type &&
                    LineIndex == other.LineIndex &&
                    CharIndex == other.CharIndex;
            }

            bool operator!=(const Token& other) const {
                return !(*this == other);
            }

            std::string ToString() const {
                return "Token { Value: \"" + Value + "\", Type: " + std::to_string(static_cast<int>(Type)) +
                    ", Line: " + std::to_string(LineIndex) + ", Char: " + std::to_string(CharIndex) + " }";
            }
        };
    }
}
