#pragma once

#include <variant>
#include <string>

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Expressions {
            namespace Interfaces {

                class IExpressionNode {
                public:
                    virtual ~IExpressionNode() = default;

                    virtual std::variant<bool, int, double, std::string> Resolve() = 0;
                };
            }
        }
    }
}