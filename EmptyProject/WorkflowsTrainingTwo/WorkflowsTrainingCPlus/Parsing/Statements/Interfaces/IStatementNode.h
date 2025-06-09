#pragma once

#include "../../Interfaces/IAstNode.h"

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Statements {
            namespace Interfaces {

                class IStatementNode : public WorkflowsTraining::Parsing::Interfaces::IAstNode {
                public:
                    virtual ~IStatementNode() = default;

                    virtual void Execute() = 0;
                };
            }
        }
    }
}