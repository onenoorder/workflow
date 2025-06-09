#include "NumberLiteralNode.h"
#include <iostream>

namespace WorkflowsTraining::Parsing::Expressions::Nodes::Expressions {
    NumberLiteralNode::NumberLiteralNode(int value)
        : value_(value) {
    }

    std::variant<bool, int, double, std::string> NumberLiteralNode::Resolve() {
        std::cout << "NumberLiteral: Resolving to " << value_ << std::endl;
        return value_;
    }
}