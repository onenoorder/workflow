#pragma once

#include <string>
#include <memory>

#include "../../Interfaces/IBinaryExpressionNode.h"

using namespace WorkflowsTraining::Parsing::Expressions::Interfaces;

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Expressions {
            namespace Nodes {
                namespace BinaryExpressions {

                    class ArithmeticExpressionNode : public IBinaryExpressionNode {
                    private:
                        std::string operator_string_;
                        std::unique_ptr<Interfaces::IExpressionNode> left_;
                        std::unique_ptr<Interfaces::IExpressionNode> right_;

                    public:
                        ArithmeticExpressionNode(std::string operatorString,
                            std::unique_ptr<Interfaces::IExpressionNode> left,
                            std::unique_ptr<Interfaces::IExpressionNode> right);

                        std::variant<bool, int, double, std::string> Resolve() override;
                    };
                }
            }
        }
    }
}