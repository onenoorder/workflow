#include "PrintNode.h"
#include <iostream>

namespace WorkflowsTraining::Parsing::Statements::Print {
    PrintNode::PrintNode(std::unique_ptr<Expressions::Interfaces::IExpressionNode> value)
        : value_(std::move(value)) {
    }

    void PrintNode::Execute() {
        std::cout << "Executing PrintNode: ";

        auto resolvedValue = value_->Resolve();

        std::visit([](auto&& arg) {
            using T = std::decay_t<decltype(arg)>;

            if constexpr (std::is_same_v<T, bool>) {
                std::cout << (arg ? "true" : "false");
            }
            else if constexpr (std::is_same_v<T, int>) {
                std::cout << arg;
            }
            else if constexpr (std::is_same_v<T, double>) {
                std::cout << arg;
            }
            else if constexpr (std::is_same_v<T, std::string>) {
                std::cout << arg;
            }
            else {
                std::cout << "[Unsupported type for print]";
            }
            }, resolvedValue);
        std::cout << std::endl;
    }
}