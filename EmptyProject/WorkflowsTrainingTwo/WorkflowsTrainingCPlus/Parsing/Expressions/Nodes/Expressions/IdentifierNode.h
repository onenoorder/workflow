#pragma once
#include <string>
#include <variant>

#include "../../Interfaces/IExpressionNode.h"

using namespace WorkflowsTraining::Parsing::Expressions::Interfaces;

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Expressions {
            namespace Nodes {
                namespace Expressions {

                    class IdentifierNode : public IExpressionNode {
                    private:
                        std::string _name;

                    public:
                        IdentifierNode(std::string name);

                        std::string GetName() const;

                        std::variant<bool, int, double, std::string> Resolve() override;
                    };
                }
            }
        }
    }
}