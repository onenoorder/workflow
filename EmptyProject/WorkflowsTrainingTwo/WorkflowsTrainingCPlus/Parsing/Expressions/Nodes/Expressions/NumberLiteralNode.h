#pragma once

#include "../../Interfaces/IExpressionNode.h"
#include "IdentifierNode.h"

using namespace WorkflowsTraining::Parsing::Expressions::Interfaces;

namespace WorkflowsTraining::Parsing::Expressions::Nodes::Expressions {
    class NumberLiteralNode : public IExpressionNode {
    private:
        int value_;

    public:
        NumberLiteralNode(int value);

        std::variant<bool, int, double, std::string> Resolve() override;
    };
}