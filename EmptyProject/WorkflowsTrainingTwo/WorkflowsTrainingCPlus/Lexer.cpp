#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <map>
#include <queue>
#include <functional>
#include <cctype>

#include "TokenType.cpp"
#include "Token.cpp"
#include "TokenStream.cpp"

namespace WorkflowsTraining {
    namespace Lexing {
        class Lexer {
        private:
            static const std::map<std::string, TokenType> Keywords;

            std::map<std::string, TokenType> InitKeywords = {
                { "if", TokenType::If },
                { "else", TokenType::Else },
                { "then", TokenType::Then },
                { "endif", TokenType::EndIf },
                { "print", TokenType::Print },
                { "equals", TokenType::ComparisonOperator},
                { "true", TokenType::Bool },
                { "false", TokenType::Bool },
                { "Users", TokenType::Identifier },
                { "Clients", TokenType::Identifier },
                { "loop", TokenType::Loop },
                { "endloop", TokenType::EndLoop }
            };

        public:
            static TokenStream Tokenize(std::ifstream& sourceCodeStream) {
                std::queue<Token> tokenQueue;
                std::string line;
                int lineIndex = 1;

                while (std::getline(sourceCodeStream, line)) {
                    AddLineTokensToQueue(line, lineIndex, tokenQueue);
                    lineIndex++;
                }

                tokenQueue.emplace("EOF", TokenType::Eof, lineIndex, 0);
                return TokenStream(std::move(tokenQueue));
            }

        private:
            static void AddLineTokensToQueue(const std::string& line, int lineIndex, std::queue<Token>& tokenQueue) {
                for (int charIndex = 0; charIndex < line.length(); ++charIndex) {
                    char character = line[charIndex];

                    if (character == ' ') {
                        continue;
                    }

                    Token singleCharToken("", TokenType::Unknown, 0, 0);
                    if (TrySingleCharacterToken(character, lineIndex, charIndex, singleCharToken)) {
                        tokenQueue.emplace(singleCharToken);
                    }
                    else {
                        if (character == '"') {
                            std::string value = GetValue(line, charIndex + 1, [](char ch) { return ch != '"'; });
                            tokenQueue.emplace(value, TokenType::String, lineIndex, charIndex);
                            charIndex += value.length() + 1;
                        }
                        else if (std::isdigit(character)) {
                            std::string value = GetValue(line, charIndex, [](char ch) { return std::isdigit(ch); });
                            tokenQueue.emplace(value, TokenType::Number, lineIndex, charIndex);
                            charIndex += value.length() - 1;
                        }
                        else if (std::isalpha(character)) {
                            std::string value = GetValue(line, charIndex, [](char ch) { return std::isalpha(ch); });
                            TokenType type = TokenType::Identifier;

                            auto it = Keywords.find(value);
                            if (it != Keywords.end()) {
                                type = it->second;
                            }
                            tokenQueue.emplace(value, type, lineIndex, charIndex);
                            charIndex += value.length() - 1;
                        }
                    }
                }
            }

            static std::string GetValue(const std::string& line, int startIndex, std::function<bool(char)> condition) {
                std::string value = "";

                while (startIndex < line.length() && condition(line[startIndex])) {
                    value += line[startIndex];
                    startIndex++;
                }
                return value;
            }

            static bool TrySingleCharacterToken(char character, int lineIndex, int charIndex, Token& outToken) {
                bool result = false;
                switch (character) {
                case '(':
                    outToken = Token(std::string(1, character), TokenType::OpenParenthesis, lineIndex, charIndex);
                    result = true;
                    break;
                case ')':
                    outToken = Token(std::string(1, character), TokenType::CloseParenthesis, lineIndex, charIndex);
                    result = true;
                    break;
                case '.':
                    outToken = Token(std::string(1, character), TokenType::MemberAccessOperator, lineIndex, charIndex);
                    result = true;
                    break;
                case '=':
                    outToken = Token(std::string(1, character), TokenType::EqualSign, lineIndex, charIndex);
                    result = true;
                    break;
                case '>':
                case '<':
                    outToken = Token(std::string(1, character), TokenType::ComparisonOperator, lineIndex, charIndex);
                    result = true;
                    break;
                case '+':
                case '-':
                case '/':
                case '*':
                    outToken = Token(std::string(1, character), TokenType::ArithmeticOperator, lineIndex, charIndex);
                    result = true;
                    break;
                case '~':
                    outToken = Token(std::string(1, character), TokenType::StringConcatenation, lineIndex, charIndex);
                    result = true;
                    break;
                }
                return result;
            }
        };
    }
}