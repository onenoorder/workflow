#pragma once
#include <vector>
#include <memory>
#include "../../Lexing/TokenStream.h"
#include "Interfaces/IStatementNode.h"
#include "Interfaces/IParserRule.h"
#include "IfThenElse/IfThenElseRule.h"

using namespace WorkflowsTraining::Lexing;
using namespace WorkflowsTraining::Parsing::Statements::IfThenElse;

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Statements {

            class StatementParser {
            private:
                static const std::vector<std::unique_ptr<IParserRule>> Rules;

                static std::vector<std::unique_ptr<IParserRule>> InitRules() {
                    std::vector<std::unique_ptr<IParserRule>> rules;
                    rules.push_back(std::make_unique<VariableAssignmentRule>());
                    rules.push_back(std::make_unique<PrintRule>());
                    rules.push_back(std::make_unique<IfThenElseRule>());
                    rules.push_back(std::make_unique<LoopRule>());
                    return rules;
                }

            public:
                static std::unique_ptr<IStatementNode> ParseStatement(TokenStream& stream) {
                    auto it = std::find_if(Rules.begin(), Rules.end(),
                        [&](const std::unique_ptr<IParserRule>& rule) {
                            return rule->CanParse(stream);
                        });

                    if (it == Rules.end()) {
                        throw std::runtime_error("Unexpected token: " + stream.Peek().ToString() +
                            " at line " + std::to_string(stream.Peek().LineIndex));
                    }

                    return (*it)->Parse(stream);
                }
            };
        }
    }
}
