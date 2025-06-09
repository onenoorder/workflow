#include <iostream>
#include <stdexcept>
#include "BooleanExpressionNode.h"

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Expressions {
            namespace Nodes {
                namespace BinaryExpressions {

                    BooleanExpressionNode::BooleanExpressionNode(std::string operatorString,
                        std::unique_ptr<IExpressionNode> left,
                        std::unique_ptr<IExpressionNode> right)
                        : _operator_string(std::move(operatorString)),
                        _left(std::move(left)),
                        _right(std::move(right)) {
                    }

                    std::variant<bool, int, double, std::string> BooleanExpressionNode::Resolve() {
                        return ResolveBool();
                    }

                    bool BooleanExpressionNode::ResolveBool() {
                        std::cout << "BooleanExpressionNode: Resolving expression with operator '" << _operator_string << "'..." << std::endl;

                        std::variant<bool, int, double, std::string> leftValue = _left->Resolve();
                        std::variant<bool, int, double, std::string> rightValue = _right->Resolve();

                        if (_operator_string == "equals") {
                            if (leftValue.index() != rightValue.index()) {
                                throw std::runtime_error("Type mismatch in 'equals' comparison.");
                            }
                            if (auto b_val = std::get_if<bool>(&leftValue)) return *b_val == std::get<bool>(rightValue);
                            if (auto i_val = std::get_if<int>(&leftValue)) return *i_val == std::get<int>(rightValue);
                            if (auto s_val = std::get_if<std::string>(&leftValue)) return *s_val == std::get<std::string>(rightValue);

                        }
                        else if (_operator_string == ">") {
                            if (std::holds_alternative<int>(leftValue) && std::holds_alternative<int>(rightValue)) {
                                return std::get<int>(leftValue) > std::get<int>(rightValue);
                            }
                            throw std::runtime_error("Type mismatch: Expected integer values for '>' operator.");

                        }
                        else if (_operator_string == "<") {
                            if (std::holds_alternative<int>(leftValue) && std::holds_alternative<int>(rightValue)) {
                                return std::get<int>(leftValue) < std::get<int>(rightValue);
                            }
                            throw std::runtime_error("Type mismatch: Expected integer values for '<' operator.");

                        }
                        else {
                            throw std::runtime_error("Unexpected operator '" + _operator_string + "' while evaluating boolean expression.");
                        }

                        return false;
                    }
                }
            }
        }
    }
}