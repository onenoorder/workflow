#pragma once

#include <memory>
#include "../../Interfaces/IBinaryExpressionNode.h"

using namespace WorkflowsTraining::Parsing::Expressions::Interfaces;

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Expressions {
            namespace Nodes {
                namespace BinaryExpressions {

                    class BooleanExpressionNode : public IBinaryExpressionNode {
                    private:
                        std::string _operator_string;
                        std::unique_ptr<IExpressionNode> _left;
                        std::unique_ptr<IExpressionNode> _right;

                    public:
                        BooleanExpressionNode(std::string operatorString,
                            std::unique_ptr<IExpressionNode> left,
                            std::unique_ptr<IExpressionNode> right);

                        std::variant<bool, int, double, std::string> Resolve() override;
                        
                        bool ResolveBool();
                    };
                }
            }
        }
    }
}