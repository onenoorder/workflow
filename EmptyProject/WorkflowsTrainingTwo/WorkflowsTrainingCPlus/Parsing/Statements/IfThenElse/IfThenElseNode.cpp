#include "IfThenElseNode.h"
#include <iostream>

using namespace WorkflowsTraining::Parsing;
using namespace WorkflowsTraining::Parsing::Expressions::Nodes::BinaryExpressions;

namespace WorkflowsTraining::Parsing::Statements::IfThenElse {
    IfThenElseNode::IfThenElseNode(
        std::unique_ptr<BooleanExpressionNode> ifCondition,
        std::unique_ptr<ProgramNode> thenBlock,
        std::optional<std::unique_ptr<ProgramNode>> elseBlock = std::nullopt
    ) : if_condition_(std::move(ifCondition)),
        then_block_(std::move(thenBlock)),
        else_block_(std::move(elseBlock)) {
    }

    void IfThenElseNode::Execute() {
        std::cout << "IfThenElseNode: Checking condition..." << std::endl;
        if (if_condition_->ResolveBool()) {
            std::cout << "IfThenElseNode: Condition is TRUE. Executing THEN block." << std::endl;
            then_block_->Execute();
        }
        else {
            std::cout << "IfThenElseNode: Condition is FALSE." << std::endl;
            if (else_block_.has_value()) {
                std::cout << "IfThenElseNode: Executing ELSE block." << std::endl;
                else_block_.value()->Execute();
            }
            else {
                std::cout << "IfThenElseNode: No ELSE block to execute." << std::endl;
            }
        }
    }
}