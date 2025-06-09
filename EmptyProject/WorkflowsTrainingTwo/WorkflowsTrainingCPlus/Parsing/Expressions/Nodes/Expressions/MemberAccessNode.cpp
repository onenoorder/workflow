#include "MemberAccessNode.h"
#include <iostream>

namespace WorkflowsTraining::Parsing::Expressions::Nodes::Expressions {

    MemberAccessNode::MemberAccessNode(std::unique_ptr<IdentifierNode> identifier,
        std::unique_ptr<IdentifierNode> memberIdentifier)
        : identifier_(std::move(identifier)),
        member_identifier_(std::move(memberIdentifier)) {
    }

    const IdentifierNode& MemberAccessNode::GetIdentifier() const { return *identifier_; }
    const IdentifierNode& MemberAccessNode::GetMemberIdentifier() const { return *member_identifier_; }

    std::variant<bool, int, double, std::string,
        std::unique_ptr<WorkflowsTraining::Helpers::Users::User>,
        std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>> MemberAccessNode::Resolve() {
        std::cout << "MemberAccessNode: Resolving '" << identifier_->GetName() << "." << member_identifier_->GetName() << "'..." << std::endl;

        std::variant<bool, int, double, std::string,
            std::unique_ptr<WorkflowsTraining::Helpers::Users::User>,
            std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>> baseObjectVariant = identifier_->Resolve();

        std::variant<bool, int, double, std::string,
            std::unique_ptr<WorkflowsTraining::Helpers::Users::User>,
            std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>> resolvedMemberValue;

        std::visit([&](auto&& arg) {
            using T = std::decay_t<decltype(arg)>;

            if constexpr (std::is_same_v<T, std::unique_ptr<WorkflowsTraining::Helpers::Users::User>>) {
                if (!arg) {
                    throw std::runtime_error("Attempted member access on a null User object.");
                }
                const auto& user = *arg;
                const std::string& memberName = member_identifier_->GetName();

                if (memberName == "UserId") {
                    resolvedMemberValue = user.UserId;
                }
                else if (memberName == "Username") {
                    resolvedMemberValue = user.Username;
                }
                else if (memberName == "Email") {
                    resolvedMemberValue = user.Email;
                }
                else if (memberName == "AccessLevel") {
                    resolvedMemberValue = user.AccessLevel;
                }
                else {
                    throw std::runtime_error("Tried accessing non-existing member '" + memberName + "' on type User.");
                }
            }
            else if constexpr (std::is_same_v<T, std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>>) {
                if (!arg) {
                    throw std::runtime_error("Attempted member access on a null Client object.");
                }
                const auto& client = *arg;
                const std::string& memberName = member_identifier_->GetName();

                if (memberName == "Id") {
                    resolvedMemberValue = client.Id;
                }
                else if (memberName == "Name") {
                    resolvedMemberValue = client.Name;
                }
                else if (memberName == "Age") {
                    resolvedMemberValue = client.Age;
                }
                else if (memberName == "IsActive") {
                    resolvedMemberValue = client.IsActive;
                }
                else {
                    throw std::runtime_error("Tried accessing non-existing member '" + memberName + "' on type Client.");
                }
            }
            else {
                throw std::runtime_error("Tried accessing member '" + member_identifier_->GetName() +
                    "' on an unsupported type for member access. Only User and Client are supported.");
            }
            }, baseObjectVariant);

        return resolvedMemberValue;
    }
}