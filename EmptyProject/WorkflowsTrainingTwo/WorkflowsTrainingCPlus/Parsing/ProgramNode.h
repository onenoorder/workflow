#pragma once

#include <vector>
#include <memory>

#include "Statements/Interfaces/IStatementNode.h"
#include "Interfaces/IAstNode.h"

namespace WorkflowsTraining {
    namespace Parsing {
        class ProgramNode : public WorkflowsTraining::Parsing::Interfaces::IAstNode {
        private:
            std::vector<std::unique_ptr<IStatementNode>> statements_;

        public:
            ProgramNode(std::vector<std::unique_ptr<IStatementNode>>&& statements);

            void Execute();

            const std::vector<std::unique_ptr<IStatementNode>>& GetStatements() const;
        };
    }
}