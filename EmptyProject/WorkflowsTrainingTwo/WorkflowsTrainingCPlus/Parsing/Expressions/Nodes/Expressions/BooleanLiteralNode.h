#pragma once
#include <string>
#include <variant>
#include <vector>

#include "../../Interfaces/IExpressionNode.h"

using namespace WorkflowsTraining::Parsing::Expressions::Interfaces;

namespace WorkflowsTraining::Parsing::Expressions::Nodes::Expressions {
    class BooleanLiteralNode : public IExpressionNode {
    private:
        bool value_;

    public:
        BooleanLiteralNode(bool value);

        std::variant<bool, int, double, std::string> Resolve() override;
    };

}