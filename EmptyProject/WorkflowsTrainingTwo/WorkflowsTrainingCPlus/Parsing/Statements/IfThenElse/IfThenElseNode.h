#pragma once

#include "../Interfaces/IStatementNode.h"
#include "../../Expressions/Nodes/BinaryExpressions/BooleanExpressionNode.h"
#include "../../ProgramNode.h"
#include <optional>

using namespace WorkflowsTraining::Parsing;
using namespace WorkflowsTraining::Parsing::Statements::Interfaces;
using namespace WorkflowsTraining::Parsing::Expressions::Nodes::BinaryExpressions;

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Statements {
            namespace IfThenElse {

                class IfThenElseNode : public IStatementNode {
                private:
                    std::unique_ptr<BooleanExpressionNode> if_condition_;
                    std::unique_ptr<ProgramNode> then_block_;

                    std::optional<std::unique_ptr<WorkflowsTraining::Parsing::ProgramNode>> else_block_;

                public:
                    IfThenElseNode(
                        std::unique_ptr<BooleanExpressionNode> ifCondition,
                        std::unique_ptr<ProgramNode> thenBlock,
                        std::optional<std::unique_ptr<ProgramNode>> elseBlock
                    );

                    void Execute() override;
                };
            }
        }
    }
}