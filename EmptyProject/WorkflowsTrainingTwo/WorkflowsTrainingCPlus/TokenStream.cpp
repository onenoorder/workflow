#include <queue>
#include <string>
#include <stdexcept>
#include <utility>

#include "TokenType.cpp"
#include "Token.cpp"

namespace WorkflowsTraining {
    namespace Lexing {

        class TokenStream {
        private:
            std::queue<Token> tokens;
            Token current;

        public:
            TokenStream(std::queue<Token> initialTokens)
                : tokens(std::move(initialTokens)) {
                if (!tokens.empty()) {
                    current = tokens.front();
                    tokens.pop();
                }
                else {
                    current = Token("EOF", TokenType::Eof, 0, 0);
                }
            }

            const Token& Peek() const {
                return current;
            }

            const Token& PeekNext() const {
                if (tokens.empty()) {
                    static Token eofToken("EOF", TokenType::Eof, current.LineIndex, current.CharIndex + 1);
                    return eofToken;
                }
                return tokens.front();
            }

            Token Eat() {
                Token eatenToken = current;

                if (!tokens.empty()) {
                    current = tokens.front();
                    tokens.pop();
                }
                else {
                    current = Token("EOF", TokenType::Eof, eatenToken.LineIndex, eatenToken.CharIndex + eatenToken.Value.length());
                }
                return eatenToken;
            }

            void Expect(TokenType type) {
                Token nextToken = Eat();

                if (nextToken.Type != type) {
                    throw std::runtime_error("Expected " + std::to_string(static_cast<int>(type)) +
                        " at line " + std::to_string(nextToken.LineIndex) +
                        " but found " + std::to_string(static_cast<int>(nextToken.Type)));
                }
            }

            bool EndOfFile() const {
                return Peek().Type == TokenType::Eof;
            }
        };
    }
}