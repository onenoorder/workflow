#include "UsersNode.h"
#include <iostream>

namespace WorkflowsTraining::Parsing::Expressions::Nodes::Expressions {
    static std::string UsersNode::VariantToString(const std::variant<bool, int, double, std::string,
        std::unique_ptr<WorkflowsTraining::Helpers::Users::User>,
        std::vector<std::unique_ptr<WorkflowsTraining::Helpers::Users::User>>
    >& val) {
        std::stringstream ss;
        std::visit([&](auto&& arg) {
            using T = std::decay_t<decltype(arg)>;
            if constexpr (std::is_same_v<T, bool>) ss << (arg ? "true" : "false");
            else if constexpr (std::is_same_v<T, int>) ss << arg;
            else if constexpr (std::is_same_v<T, double>) ss << arg;
            else if constexpr (std::is_same_v<T, std::string>) ss << "\"" << arg << "\"";
            else if constexpr (std::is_same_v<T, std::unique_ptr<WorkflowsTraining::Helpers::Users::User>>) {
                if (arg) ss << "[User: " << arg->ToString() << "]"; else ss << "[Null User]";
            }
            else if constexpr (std::is_same_v<T, std::vector<std::unique_ptr<WorkflowsTraining::Helpers::Users::User>>>) {
                ss << "[Users list of " << arg.size() << " users]";
            }
            else ss << "UNKNOWN_TYPE";
            }, val);
        return ss.str();
    }

    UsersNode::UsersNode(std::unique_ptr<IdentifierNode> memberIdentifier,
        std::vector<std::unique_ptr<Interfaces::IExpressionNode>>&& parameters)
        : member_identifier_(std::move(memberIdentifier)),
        parameters_(std::move(parameters)) {
    }

    std::variant<bool, int, double, std::string,
        std::unique_ptr<WorkflowsTraining::Helpers::Users::User>,
        std::vector<std::unique_ptr<WorkflowsTraining::Helpers::Users::User>>
    > UsersNode::Resolve() override {
        std::cout << "UsersNode: Resolving Users." << member_identifier_->GetName() << "..." << std::endl;

        std::vector<std::variant<bool, int, double, std::string,
            std::unique_ptr<WorkflowsTraining::Helpers::Users::User>,
            std::vector<std::unique_ptr<WorkflowsTraining::Helpers::Users::User>>
            >> parameterValues;

        for (const auto& parameter : parameters_) {
            parameterValues.push_back(parameter->Resolve());
        }

        std::variant<bool, int, double, std::string,
            std::unique_ptr<WorkflowsTraining::Helpers::Users::User>,
            std::vector<std::unique_ptr<WorkflowsTraining::Helpers::Users::User>>
        > returnValue;

        const std::string& methodName = member_identifier_->GetName();

        if (methodName == "Find") {
            if (parameterValues.size() != 1 || !std::holds_alternative<std::string>(parameterValues[0])) {
                throw std::runtime_error("Users.Find expects exactly one string parameter.");
            }
            std::string userId = std::get<std::string>(parameterValues[0]);
            returnValue = WorkflowsTraining::Helpers::Users::UsersActions::Find(userId);
        }
        else if (methodName == "All") {
            if (!parameterValues.empty()) {
                throw std::runtime_error("Users.All expects no parameters.");
            }
            returnValue = WorkflowsTraining::Helpers::Users::UsersActions::All();
        }
        else {
            std::stringstream ss;
            ss << "Tried executing Users." << methodName << " with parameters (";
            for (size_t i = 0; i < parameterValues.size(); ++i) {
                ss << VariantToString(parameterValues[i]);
                if (i < parameterValues.size() - 1) {
                    ss << ", ";
                }
            }
            ss << ") but method is not recognized.";
            throw std::runtime_error(ss.str());
        }

        bool is_null_user_ptr = false;
        if (std::holds_alternative<std::unique_ptr<WorkflowsTraining::Helpers::Users::User>>(returnValue)) {
            if (!std::get<std::unique_ptr<WorkflowsTraining::Helpers::Users::User>>(returnValue)) {
                is_null_user_ptr = true;
            }
        }

        if (is_null_user_ptr) {
            std::stringstream ss;
            ss << "Expected to find value for Users." << methodName << " with parameters (";
            for (size_t i = 0; i < parameterValues.size(); ++i) {
                ss << VariantToString(parameterValues[i]);
                if (i < parameterValues.size() - 1) {
                    ss << ", ";
                }
            }
            ss << ") but got null.";
            throw std::runtime_error(ss.str());
        }

        return returnValue;
    }
}