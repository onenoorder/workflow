#pragma once

#include <string>
#include <variant>
#include <vector>

#include "../../Interfaces/IExpressionNode.h"

using namespace WorkflowsTraining::Parsing::Expressions::Interfaces;

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Expressions {
            namespace Nodes {
                namespace Expressions {

                    class StringConcatenationNode : public IExpressionNode {
                    private:
                        std::vector<std::unique_ptr<Interfaces::IExpressionNode>> _strings;

                    public:
                        StringConcatenationNode(std::vector<std::unique_ptr<IExpressionNode>>&& strings);

                        std::variant<bool, int, double, std::string> Resolve() override;
                    };
                }
            }
        }
    }
}