#include "ClientsNode.h"
#include <iostream>

namespace WorkflowsTraining::Parsing::Expressions::Nodes::Expressions {

    /*static std::string ClientsNode::VariantToString(const std::variant<bool, int, double, std::string,
        std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>,
        std::vector<std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>>
    >& val) {
        std::stringstream ss;
        std::visit([&](auto&& arg) {
            using T = std::decay_t<decltype(arg)>;
            if constexpr (std::is_same_v<T, bool>) ss << (arg ? "true" : "false");
            else if constexpr (std::is_same_v<T, int>) ss << arg;
            else if constexpr (std::is_same_v<T, double>) ss << arg;
            else if constexpr (std::is_same_v<T, std::string>) ss << "\"" << arg << "\"";
            else if constexpr (std::is_same_v<T, std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>>) {
                if (arg) ss << "[Client: " << arg->ToString() << "]"; else ss << "[Null Client]";
            }
            else if constexpr (std::is_same_v<T, std::vector<std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>>>) {
                ss << "[Clients list of " << arg.size() << " clients]";
            }
            else ss << "UNKNOWN_TYPE";
            }, val);
        return ss.str();
    }*/

    ClientsNode::ClientsNode(std::unique_ptr<IdentifierNode> memberIdentifier,
        std::vector<std::unique_ptr<Interfaces::IExpressionNode>>&& parameters)
        : member_identifier_(std::move(memberIdentifier)),
        parameters_(std::move(parameters)) {
    }

    /*std::variant<bool, int, double, std::string,
        std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>,
        std::vector<std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>>
    > ClientsNode::Resolve() {
        std::cout << "ClientsNode: Resolving Clients." << member_identifier_->GetName() << "..." << std::endl;

        std::vector<std::variant<bool, int, double, std::string,
            std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>,
            std::vector<std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>>
            >> parameterValues;

        for (const auto& parameter : parameters_) {
            parameterValues.push_back(parameter->Resolve());
        }

        std::variant<bool, int, double, std::string,
            std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>,
            std::vector<std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>>
        > returnValue;

        const std::string& methodName = member_identifier_->GetName();

        if (methodName == "Find") {
            if (parameterValues.size() != 1 || !std::holds_alternative<std::string>(parameterValues[0])) {
                throw std::runtime_error("Clients.Find expects exactly one string parameter.");
            }
            std::string clientId = std::get<std::string>(parameterValues[0]);
            returnValue = WorkflowsTraining::Helpers::Clients::ClientActions::Find(clientId);
        }
        else if (methodName == "All") {
            if (!parameterValues.empty()) {
                throw std::runtime_error("Clients.All expects no parameters.");
            }
            returnValue = WorkflowsTraining::Helpers::Clients::ClientActions::All();
        }
        else {
            std::stringstream ss;
            ss << "Tried executing Clients." << methodName << " with parameters (";
            for (size_t i = 0; i < parameterValues.size(); ++i) {
                ss << VariantToString(parameterValues[i]);
                if (i < parameterValues.size() - 1) {
                    ss << ", ";
                }
            }
            ss << ") but method is not recognized.";
            throw std::runtime_error(ss.str());
        }

        bool is_null_client_ptr = false;
        if (std::holds_alternative<std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>>(returnValue)) {
            if (!std::get<std::unique_ptr<WorkflowsTraining::Helpers::Clients::Client>>(returnValue)) {
                is_null_client_ptr = true;
            }
        }

        if (is_null_client_ptr) {
            std::stringstream ss;
            ss << "Expected to find value for Clients." << methodName << " with parameters (";
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
    }*/
}