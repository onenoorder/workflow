#include "ProgramNode.h"
#include "Statements/IfThenElse/IfThenElseNode.h"

using namespace WorkflowsTraining::Parsing;
using namespace WorkflowsTraining::Parsing::Statements;

ProgramNode::ProgramNode(std::vector<std::unique_ptr<IStatementNode>>&& statements)
    : statements_(std::move(statements)) {
}

void ProgramNode::Execute() {
    for (const auto& statement : statements_) {
        statement->Execute();
    }
}

const std::vector<std::unique_ptr<IStatementNode>>& ProgramNode::GetStatements() const {
    return statements_;
}