#include "IdentifierNode.h"
#include <iostream>

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Expressions {
            namespace Nodes {
                namespace Expressions {
                    IdentifierNode::IdentifierNode(std::string name)
                        : _name(std::move(name)) {
                    }

                    std::string IdentifierNode::GetName() const
                    {
                        return _name;
                    }

                    std::variant<bool, int, double, std::string> IdentifierNode::Resolve() {
                        std::cout << "IdentifierNode: Resolving '" << _name << "'..." << std::endl;
                        //return WorkflowsTraining::Helpers::Storage::VariableStorage::GetVariable(_name);
                    }
                }
            }
        }
    }
}