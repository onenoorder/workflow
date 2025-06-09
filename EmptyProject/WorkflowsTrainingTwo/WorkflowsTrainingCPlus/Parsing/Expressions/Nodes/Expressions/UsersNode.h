#pragma once
#include <string>
#include <variant>
#include <vector>

#include "../../Interfaces/IExpressionNode.h"
#include <memory>

using namespace WorkflowsTraining::Parsing::Expressions::Interfaces;

namespace WorkflowsTraining::Parsing::Expressions::Nodes::Expressions {

    class UsersNode : public Interfaces::IExpressionNode {
    private:
        std::unique_ptr<IdentifierNode> member_identifier_;
        std::vector<std::unique_ptr<Interfaces::IExpressionNode>> parameters_;

        /*/static std::string VariantToString(const std::variant<bool, int, double, std::string,
            std::unique_ptr<WorkflowsTraining::Helpers::Users::User>,
            std::vector<std::unique_ptr<WorkflowsTraining::Helpers::Users::User>>
        >& val);*/

    public:
        UsersNode(std::unique_ptr<IdentifierNode> memberIdentifier,
            std::vector<std::unique_ptr<Interfaces::IExpressionNode>>&& parameters);

        /*std::variant<bool, int, double, std::string,
            std::unique_ptr<WorkflowsTraining::Helpers::Users::User>,
            std::vector<std::unique_ptr<WorkflowsTraining::Helpers::Users::User>>
        > Resolve() override;*/
    };

}