#include "BooleanLiteralNode.h"
#include <iostream>

namespace WorkflowsTraining::Parsing::Expressions::Nodes::Expressions {
    BooleanLiteralNode::BooleanLiteralNode(bool value)
        : value_(value) {
    }

    std::variant<bool, int, double, std::string> BooleanLiteralNode::Resolve() {
        std::cout << "BooleanLiteralNode: Resolving to " << (value_ ? "true" : "false") << std::endl;
        return value_;
    }
}