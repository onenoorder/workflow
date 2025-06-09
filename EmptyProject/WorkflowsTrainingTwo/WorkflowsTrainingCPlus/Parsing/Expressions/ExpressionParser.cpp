#include "ExpressionParser.h"
#include <iostream>
#include "Nodes/BinaryExpressions/ArithmeticExpressionNode.h"
#include "Nodes/BinaryExpressions/BooleanExpressionNode.h"
#include "Nodes/Expressions/StringConcatenationNode.h"
#include "Nodes/Expressions/StringLiteralNode.h"
#include "Nodes/Expressions/BooleanLiteralNode.h"
#include "Nodes/Expressions/MemberAccessNode.h"
#include "Nodes/Expressions/NumberLiteralNode.h"
#include "Nodes/Expressions/ClientsNode.h"
#include "Nodes/Expressions/UsersNode.h"

using namespace WorkflowsTraining::Parsing::Expressions::Nodes::BinaryExpressions;

namespace WorkflowsTraining::Parsing::Expressions {

    std::unique_ptr<IExpressionNode> ExpressionParser::ParseExpression(Token token, TokenStream& stream) {
        std::cout << "ExpressionParser: Parsing expression starting with " << token.ToString() << std::endl;

        std::unique_ptr<IExpressionNode> expressionNode = ParsePrimaryExpression(token, stream);

        TokenType peekType = stream.Peek().Type;

        if (peekType == TokenType::ArithmeticOperator) {
            return ParseArithmeticOperatorExpression(std::move(expressionNode), stream);
        }
        else if (peekType == TokenType::ComparisonOperator) {
            return ParseComparisonOperatorExpression(std::move(expressionNode), stream);
        }
        else if (peekType == TokenType::StringConcatenation) {
            return ParseStringConcatenation(std::move(expressionNode), stream);
        }
        else {
            return expressionNode;
        }
    }

    std::vector<std::unique_ptr<IExpressionNode>> ExpressionParser::ParseArguments(TokenStream& stream) {
        std::cout << "ExpressionParser: Parsing arguments." << std::endl;
        stream.Expect(TokenType::OpenParenthesis);
        std::vector<std::unique_ptr<IExpressionNode>> arguments;

        if (stream.Peek().Type != TokenType::CloseParenthesis) {
            while (stream.Peek().Type != TokenType::CloseParenthesis && !stream.EndOfFile()) {
                arguments.push_back(ParseExpression(stream.Eat(), stream));
                if (stream.Peek().Type == TokenType::Comma) {
                    stream.Eat();
                }
                else if (stream.Peek().Type != TokenType::CloseParenthesis && !stream.EndOfFile()) {
                    throw std::runtime_error("Expected comma or closing parenthesis in arguments list at line " +
                        std::to_string(stream.Peek().LineIndex));
                }
            }
        }

        stream.Expect(TokenType::CloseParenthesis);
        return arguments;
    }

    std::unique_ptr<IExpressionNode> ExpressionParser::ParseIdentifierExpression(Token token, TokenStream& stream) {
        std::cout << "ExpressionParser: Parsing identifier expression: " << token.Value << std::endl;
        auto identifier = std::make_unique<IdentifierNode>(token.Value);

        if (stream.Peek().Type != TokenType::MemberAccessOperator) {
            return identifier;
        }
        else {
            //return ParseMemberAccess(std::move(identifier), stream);
        }
    }

    std::unique_ptr<IExpressionNode> ExpressionParser::ParseArithmeticOperatorExpression(std::unique_ptr<IExpressionNode> leftNode, TokenStream& stream) {
        std::cout << "ExpressionParser: Parsing arithmetic operator expression." << std::endl;

        while (stream.Peek().Value == "+" || stream.Peek().Value == "-") {
            std::string operatorValue = stream.Eat().Value;
            std::unique_ptr<IExpressionNode> rightNode = ParseMultiplicativeExpression(stream.Eat(), stream);
            leftNode = std::make_unique<ArithmeticExpressionNode>(
                operatorValue,
                std::move(leftNode),
                std::move(rightNode)
            );
        }
        return leftNode;
    }

    std::unique_ptr<IExpressionNode> ExpressionParser::ParseMultiplicativeExpression(Token token, TokenStream& stream) {
        std::cout << "ExpressionParser: Parsing multiplicative expression." << std::endl;
        std::unique_ptr<IExpressionNode> leftNode = ParsePrimaryExpression(token, stream);

        while (stream.Peek().Value == "/" || stream.Peek().Value == "*") {
            std::string operatorValue = stream.Eat().Value;
            std::unique_ptr<IExpressionNode> rightNode = ParsePrimaryExpression(stream.Eat(), stream);
            leftNode = std::make_unique<ArithmeticExpressionNode>(
                operatorValue,
                std::move(leftNode),
                std::move(rightNode)
            );
        }
        return leftNode;
    }

    std::unique_ptr<IExpressionNode> ExpressionParser::ParseComparisonOperatorExpression(std::unique_ptr<IExpressionNode> leftNode, TokenStream& stream) {
        std::cout << "ExpressionParser: Parsing comparison operator expression." << std::endl;
        std::string operatorValue = stream.Eat().Value;
        std::unique_ptr<IExpressionNode> rightNode = ParsePrimaryExpression(stream.Eat(), stream);

        return std::make_unique<BooleanExpressionNode>(
            operatorValue,
            std::move(leftNode),
            std::move(rightNode)
        );
    }

    std::unique_ptr<IExpressionNode> ExpressionParser::ParseStringConcatenation(std::unique_ptr<IExpressionNode> expressionNode, TokenStream& stream) {
        std::cout << "ExpressionParser: Parsing string concatenation." << std::endl;
        std::vector<std::unique_ptr<IExpressionNode>> strings;
        strings.push_back(std::move(expressionNode));

        while (stream.Peek().Type == TokenType::StringConcatenation && !stream.EndOfFile()) {
            stream.Eat();
            strings.push_back(ParseExpression(stream.Eat(), stream));
        }
        return std::make_unique<StringConcatenationNode>(std::move(strings));
    }

    std::unique_ptr<IExpressionNode> ExpressionParser::ParsePrimaryExpression(Token token, TokenStream& stream) {
        std::cout << "ExpressionParser: Parsing primary expression for token: " << token.ToString() << std::endl;
        switch (token.Type) {
        case TokenType::String:
            return std::make_unique<StringLiteralNode>(token.Value);
        case TokenType::Number:
            try {
                return std::make_unique<NumberLiteralNode>(std::stoi(token.Value));
            }
            catch (const std::out_of_range& e) {
                throw std::runtime_error("Number out of range: " + token.Value + " at line " + std::to_string(token.LineIndex));
            }
            catch (const std::invalid_argument& e) {
                throw std::runtime_error("Invalid number format: " + token.Value + " at line " + std::to_string(token.LineIndex));
            }
        case TokenType::Identifier:
            return ParseIdentifierExpression(token, stream);
        case TokenType::Bool:
            return std::make_unique<BooleanLiteralNode>(token.Value == "true");
        default:
            throw std::runtime_error("Unexpected token for primary expression: " + token.ToString() +
                " at line " + std::to_string(token.LineIndex));
        }
    }
    /*
    std::unique_ptr<IExpressionNode> ExpressionParser::ParseMemberAccess(std::unique_ptr<IdentifierNode> identifierNode, TokenStream& stream) {
        std::cout << "ExpressionParser: Parsing member access for base: " << identifierNode->GetName() << std::endl;

        stream.Expect(TokenType::MemberAccessOperator);
        Token memberIdentifierToken = stream.Eat();

        if (memberIdentifierToken.Type != TokenType::Identifier) {
            throw std::runtime_error("Expected for member access at line " + std::to_string(memberIdentifierToken.LineIndex) +
                " but found ");
        }

        auto memberNode = std::make_unique<IdentifierNode>(memberIdentifierToken.Value);

        if (stream.Peek().Type != TokenType::OpenParenthesis) {
            return std::make_unique<MemberAccessNode>(std::move(identifierNode), std::move(memberNode));
        }

        if (identifierNode->GetName() == "Clients") {
            return std::make_unique<ClientsNode>(std::move(memberNode), ParseArguments(stream));
        }
        if (identifierNode->GetName() == "Users") {
            return std::make_unique<UsersNode>(std::move(memberNode), ParseArguments(stream));
        }

        throw std::runtime_error("Unsupported method call for identifier: " + identifierNode->GetName() +
            " at line " + std::to_string(identifierNode->Resolve().index()));
    }*/
}