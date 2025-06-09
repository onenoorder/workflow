#include "StringConcatenationNode.h"
#include <iostream>

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Expressions {
            namespace Nodes {
                namespace Expressions {
                    StringConcatenationNode::StringConcatenationNode(std::vector<std::unique_ptr<IExpressionNode>>&& strings)
                        : _strings(std::move(strings)) {
                    }

                    std::variant<bool, int, double, std::string> StringConcatenationNode::Resolve() {
                        std::cout << "StringConcatenationNode: Resolving..." << std::endl;
                        std::string result_string = "";

                        for (const auto& stringNode : _strings) {
                            std::variant<bool, int, double, std::string> resolvedValue = stringNode->Resolve();

                            std::visit([&](auto&& arg) {
                                using T = std::decay_t<decltype(arg)>;

                                if constexpr (std::is_same_v<T, bool>) {
                                    result_string += (arg ? "true" : "false");
                                }
                                else if constexpr (std::is_same_v<T, int>) {
                                    result_string += std::to_string(arg);
                                }
                                else if constexpr (std::is_same_v<T, double>) {
                                    result_string += std::to_string(arg);
                                }
                                else if constexpr (std::is_same_v<T, std::string>) {
                                    result_string += arg;
                                }
                                else {
                                    throw std::runtime_error("Unsupported type encountered during string concatenation.");
                                }
                                }, resolvedValue);
                        }

                        return result_string;
                    }
                }
            }
        }
    }
}