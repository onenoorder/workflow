#pragma once

#include <string>
#include <variant>
#include <vector>

#include "../../Interfaces/IExpressionNode.h"
#include "IdentifierNode.h"

using namespace WorkflowsTraining::Parsing::Expressions::Interfaces;

namespace WorkflowsTraining::Parsing::Expressions::Nodes::Expressions {

    class MemberAccessNode : public Interfaces::IExpressionNode {
    private:
        std::unique_ptr<IdentifierNode> identifier_;
        std::unique_ptr<IdentifierNode> member_identifier_;

    public:
        MemberAccessNode(std::unique_ptr<IdentifierNode> identifier,
            std::unique_ptr<IdentifierNode> memberIdentifier);

        const IdentifierNode& GetIdentifier() const;
        const IdentifierNode& GetMemberIdentifier() const;

        /*std::variant<bool, int, double, std::string,
            std::unique_ptr<WorkflowsTraining::Helpers::Users::User>,
            std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>> Resolve() override;*/
    };
}