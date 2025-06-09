#pragma once

#include "../Interfaces/IParserRule.h"
#include "../../ProgramNode.h"
#include "IfThenElseNode.h"

using namespace WorkflowsTraining::Parsing::Statements::Interfaces;

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Statements {
            namespace IfThenElse {

                class IfThenElseRule : public IParserRule {
                public:
                    bool CanParse(TokenStream& stream) const override;

                    std::unique_ptr<IStatementNode> Parse(TokenStream& stream) const override;

                private:
                    std::unique_ptr<ProgramNode> ParseThenBlock(TokenStream& stream) const;

                    std::optional<std::unique_ptr<ProgramNode>> ParseElseBlock(TokenStream& stream) const;
                };
            }
        }
    }
}