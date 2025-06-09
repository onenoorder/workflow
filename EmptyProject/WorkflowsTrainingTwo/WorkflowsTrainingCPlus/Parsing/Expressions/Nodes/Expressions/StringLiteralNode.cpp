#include "StringLiteralNode.h"
#include <iostream>

namespace WorkflowsTraining::Parsing::Expressions::Nodes::Expressions {
    StringLiteralNode::StringLiteralNode(std::string value)
        : value_(std::move(value)) {
    }

    std::variant<bool, int, double, std::string> StringLiteralNode::Resolve() {
        std::cout << "StringLiteralNode: Resolving to \"" << value_ << "\"" << std::endl;
        return value_;
    }
}