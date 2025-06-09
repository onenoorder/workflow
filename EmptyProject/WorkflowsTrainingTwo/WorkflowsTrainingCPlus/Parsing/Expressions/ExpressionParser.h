#pragma once
#include <memory>
#include <vector>
#include "../../Lexing/TokenStream.h"
#include "../../Lexing/Token.h"
#include "Interfaces/IExpressionNode.h"
#include "Nodes/Expressions/IdentifierNode.h"

using namespace WorkflowsTraining::Lexing;
using namespace WorkflowsTraining::Parsing::Expressions::Interfaces;
using namespace WorkflowsTraining::Parsing::Expressions::Nodes::Expressions;


namespace WorkflowsTraining {
    namespace Parsing {
        namespace Expressions {

            class ExpressionParser {
            public:
                static std::unique_ptr<IExpressionNode> ParseExpression(Token token, TokenStream& stream);

                static std::vector<std::unique_ptr<Interfaces::IExpressionNode>> ParseArguments(TokenStream& stream);

                static std::unique_ptr<Interfaces::IExpressionNode> ParseIdentifierExpression(Token token, TokenStream& stream);

            private:
                static std::unique_ptr<Interfaces::IExpressionNode> ParseArithmeticOperatorExpression(std::unique_ptr<Interfaces::IExpressionNode> leftNode, TokenStream& stream);

                static std::unique_ptr<Interfaces::IExpressionNode> ParseMultiplicativeExpression(Token token, TokenStream& stream);

                static std::unique_ptr<Interfaces::IExpressionNode> ParseComparisonOperatorExpression(std::unique_ptr<IExpressionNode> leftNode, TokenStream& stream);

                static std::unique_ptr<Interfaces::IExpressionNode> ParseStringConcatenation(std::unique_ptr<IExpressionNode> expressionNode, TokenStream& stream);

                static std::unique_ptr<Interfaces::IExpressionNode> ParsePrimaryExpression(Token token, TokenStream& stream);

                //static std::unique_ptr<Interfaces::IExpressionNode> ParseMemberAccess(std::unique_ptr<IdentifierNode> identifierNode, TokenStream& stream);
            };
        }
    }
}