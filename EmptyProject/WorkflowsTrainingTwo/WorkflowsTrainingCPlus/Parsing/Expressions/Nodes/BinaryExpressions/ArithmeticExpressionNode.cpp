#include "ArithmeticExpressionNode.h"
#include <iostream>

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Expressions {
            namespace Nodes {
                namespace BinaryExpressions {

                    ArithmeticExpressionNode::ArithmeticExpressionNode(std::string operatorString,
                        std::unique_ptr<Interfaces::IExpressionNode> left,
                        std::unique_ptr<Interfaces::IExpressionNode> right)
                        : operator_string_(std::move(operatorString)),
                        left_(std::move(left)),
                        right_(std::move(right)) {
                    }

                    std::variant<bool, int, double, std::string> ArithmeticExpressionNode::Resolve() {
                        std::cout << "ArithmeticExpressionNode: Resolving expression with operator '" << operator_string_ << "'..." << std::endl;

                        std::variant<bool, int, double, std::string> leftValueVariant = left_->Resolve();
                        std::variant<bool, int, double, std::string> rightValueVariant = right_->Resolve();

                        int leftValue;
                        int rightValue;

                        try {
                            leftValue = std::get<int>(leftValueVariant);
                            rightValue = std::get<int>(rightValueVariant);
                        }
                        catch (const std::bad_variant_access& e) {
                            throw std::runtime_error("Type error: Arithmetic operations require integer operands. " + std::string(e.what()));
                        }

                        if (operator_string_ == "+") {
                            return leftValue + rightValue;
                        }
                        else if (operator_string_ == "-") {
                            return leftValue - rightValue;
                        }
                        else if (operator_string_ == "/") {
                            if (rightValue == 0) {
                                throw std::runtime_error("Division by zero in arithmetic expression.");
                            }
                            return leftValue / rightValue;
                        }
                        else if (operator_string_ == "*") {
                            return leftValue * rightValue;
                        }
                        else {
                            throw std::runtime_error("Unexpected operator '" + operator_string_ + "' while evaluating arithmetic expression.");
                        }
                    }
                }
            }
        }
    }
}