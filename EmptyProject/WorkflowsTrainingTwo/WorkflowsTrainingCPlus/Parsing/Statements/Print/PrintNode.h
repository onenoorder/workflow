#pragma once

#include "../Interfaces/IStatementNode.h"
#include "../../Expressions/Interfaces/IExpressionNode.h"

using namespace WorkflowsTraining::Parsing;
using namespace WorkflowsTraining::Parsing::Statements::Interfaces;
using namespace WorkflowsTraining::Parsing::Expressions::Interfaces;

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Statements {
            namespace Print {

                class PrintNode : public IStatementNode {
                private:
                    std::unique_ptr<IExpressionNode> value_;

                public:
                    PrintNode(std::unique_ptr<IExpressionNode> value);

                    void Execute() override;
                };
            }
        }
    }
}