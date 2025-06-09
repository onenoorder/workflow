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
                    class StringLiteralNode : public IExpressionNode {
                    private:
                        std::string value_;

                    public:
                        StringLiteralNode(std::string value);

                        std::variant<bool, int, double, std::string> Resolve() override;
                    };
                }
            }
        }
    }
}