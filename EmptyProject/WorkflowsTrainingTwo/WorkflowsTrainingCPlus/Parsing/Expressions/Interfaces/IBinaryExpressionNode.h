#pragma once

#include <variant>
#include <string>

#include "IExpressionNode.h"

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Expressions {
            namespace Interfaces {

                class IBinaryExpressionNode : public IExpressionNode {
                public:
                    virtual ~IBinaryExpressionNode() = default;
                };
            }
        }
    }
}