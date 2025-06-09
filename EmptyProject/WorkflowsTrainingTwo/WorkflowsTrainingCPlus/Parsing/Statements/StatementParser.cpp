#include "StatementParser.h"
#include "IfThenElse/IfThenElseRule.h"

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Statements {

            std::vector<std::unique_ptr<IParserRule>> StatementParser::InitRules() {
                std::vector<std::unique_ptr<IParserRule>> rules;
                rules.push_back(std::make_unique<VariableAssignmentRule>());
                rules.push_back(std::make_unique<PrintRule>());
                rules.push_back(std::make_unique<IfThenElseRule>());
                rules.push_back(std::make_unique<LoopRule>());
                return rules;
            }

            std::unique_ptr<IStatementNode> StatementParser::ParseStatement(TokenStream& stream) {
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
        }
    }
}