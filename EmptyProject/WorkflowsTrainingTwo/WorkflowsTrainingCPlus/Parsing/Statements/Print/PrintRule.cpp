#include "PrintRule.h"
#include <iostream>
#include "PrintNode.h"
#include "../../Expressions/Nodes/Expressions/StringLiteralNode.h"
#include "../../Expressions/ExpressionParser.h"

using namespace WorkflowsTraining::Parsing::Expressions;
using namespace WorkflowsTraining::Parsing::Expressions::Nodes::Expressions;

namespace WorkflowsTraining::Parsing::Statements::Print {
    
    bool PrintRule::CanParse(TokenStream& stream) const {
        return stream.Peek().Type == Lexing::TokenType::Print;
    }

    std::unique_ptr<IStatementNode> PrintRule::Parse(TokenStream& stream) const {
        std::cout << "PrintRule: Parsing 'print' statement...\n";
        stream.Eat();

        std::vector<std::unique_ptr<Expressions::Interfaces::IExpressionNode>> arguments =
            ExpressionParser::ParseArguments(stream);

        if (arguments.size() == 1) {
            std::cout << "PrintRule: Found single argument. Creating PrintNode.\n";
            return std::make_unique<PrintNode>(std::move(arguments[0]));
        }
        else if (arguments.empty()) {
            std::cout << "PrintRule: No arguments found. Creating PrintNode with empty string literal.\n";
            return std::make_unique<PrintNode>(std::make_unique<StringLiteralNode>(""));
        }
        else {
            std::cout << "PrintRule: Found multiple arguments (" << arguments.size() << "). Using the first one.\n";
            return std::make_unique<PrintNode>(std::move(arguments[0]));
        }
    }

}