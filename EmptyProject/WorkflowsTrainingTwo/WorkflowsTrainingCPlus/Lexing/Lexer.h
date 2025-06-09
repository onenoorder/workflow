#pragma once

#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <map>
#include <queue>
#include <functional>
#include <cctype>
#include "TokenType.h"
#include "TokenStream.h"

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
            static TokenStream Tokenize(std::ifstream& sourceCodeStream);

        private:
            static void AddLineTokensToQueue(const std::string& line, int lineIndex, std::queue<Token>& tokenQueue);

            static std::string GetValue(const std::string& line, int startIndex, std::function<bool(char)> condition);

            static bool TrySingleCharacterToken(char character, int lineIndex, int charIndex, Token& outToken);
        };
    }
}