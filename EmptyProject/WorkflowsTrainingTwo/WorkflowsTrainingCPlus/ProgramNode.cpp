#include <vector>
#include <memory>

namespace WorkflowsTraining {
    namespace Parsing {
        class ProgramNode : public Interfaces::IAstNode {
        private:
            std::vector<std::unique_ptr<Statements::IStatementNode>> statements_;

        public:
            ProgramNode(std::vector<std::unique_ptr<Statements::IStatementNode>>&& statements)
                : statements_(std::move(statements)) {
            }

            void Execute() override {
                for (const auto& statement : statements_) {
                    statement->Execute();
                }
            }

            const std::vector<std::unique_ptr<Statements::IStatementNode>>& GetStatements() const {
                return statements_;
            }
        };
    }
}