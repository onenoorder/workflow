#pragma once

#include "../Interfaces/IParserRule.h"
#include "../../Expressions/Interfaces/IExpressionNode.h"

using namespace WorkflowsTraining::Parsing;
using namespace WorkflowsTraining::Parsing::Statements::Interfaces;
using namespace WorkflowsTraining::Parsing::Expressions::Interfaces;

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Statements {
            namespace Print {

                class PrintRule : public IParserRule {
                public:
                    bool CanParse(TokenStream& stream) const override;

                    std::unique_ptr<IStatementNode> Parse(TokenStream& stream) const override;
                };
            }
        }
    }
}