#include "IfThenElseRule.h"
#include <iostream>
#include "../../Expressions/ExpressionParser.h"
#include "../../Statements/StatementParser.h"

using namespace WorkflowsTraining::Parsing::Expressions;
using namespace WorkflowsTraining::Parsing::Statements;

namespace WorkflowsTraining::Parsing::Statements::IfThenElse {

    bool IfThenElseRule::CanParse(TokenStream& stream) const {
        return stream.Peek().Type == TokenType::If;
    }

    std::unique_ptr<IStatementNode> IfThenElseRule::Parse(TokenStream& stream) const {
        std::cout << "IfThenElseRule: Parsing an 'if' statement." << std::endl;

        // 1. Eat 'if' keyword
        stream.Expect(TokenType::If);

        // 2. Parse the condition
        Token nextToken = stream.Eat();
        std::unique_ptr<IExpressionNode> ifCondition = ExpressionParser::ParseExpression(nextToken, stream);

        // 3. Type check the condition (must be a BooleanExpressionNode)
        WorkflowsTraining::Parsing::Expressions::Nodes::BinaryExpressions::BooleanExpressionNode* boolCondition =
            dynamic_cast<WorkflowsTraining::Parsing::Expressions::Nodes::BinaryExpressions::BooleanExpressionNode*>(ifCondition.get());

        if (boolCondition == nullptr) {
            throw std::runtime_error("Expected boolean value for 'if' condition at line " +
                std::to_string(nextToken.LineIndex) +
                ". Found expression type: " + typeid(*ifCondition).name());
        }
        std::unique_ptr<WorkflowsTraining::Parsing::Expressions::Nodes::BinaryExpressions::BooleanExpressionNode> ownedBoolCondition(boolCondition);
        ifCondition.release();

        // 4. Parse the 'then' block
        std::unique_ptr<WorkflowsTraining::Parsing::ProgramNode> thenNode = ParseThenBlock(stream);

        // 5. Parse the 'else' block (optional)
        std::optional<std::unique_ptr<WorkflowsTraining::Parsing::ProgramNode>> elseNode = ParseElseBlock(stream);

        // 6. Expect 'endif' keyword
        stream.Expect(WorkflowsTraining::Lexing::TokenType::EndIf);

        // 7. Return the IfThenElseNode
        return std::make_unique<IfThenElseNode>(
            std::move(ownedBoolCondition),
            std::move(thenNode),
            std::move(elseNode)
        );
    }

    std::unique_ptr<ProgramNode> IfThenElseRule::ParseThenBlock(TokenStream& stream) const {
        std::cout << "IfThenElseRule: Parsing THEN block." << std::endl;
        stream.Expect(TokenType::Then);

        std::vector<std::unique_ptr<IStatementNode>> thenStatements;
        TokenType nextTokenType = stream.Peek().Type;

        // Loop until 'else' or 'endif' is encountered
        while (nextTokenType != TokenType::Else &&
            nextTokenType != TokenType::EndIf &&
            nextTokenType != TokenType::Eof)
        {
            std::unique_ptr<Interfaces::IStatementNode> statement = StatementParser::ParseStatement(stream);
            if (statement) {
                thenStatements.push_back(std::move(statement));
            }
            else {
                break;
            }
            nextTokenType = stream.Peek().Type;
        }

        return std::make_unique<ProgramNode>(std::move(thenStatements));
    }

    std::optional<std::unique_ptr<ProgramNode>> IfThenElseRule::ParseElseBlock(TokenStream& stream) const {
        if (stream.Peek().Type == TokenType::Else) {
            std::cout << "IfThenElseRule: Parsing ELSE block." << std::endl;
            stream.Expect(TokenType::Else);

            std::vector<std::unique_ptr<IStatementNode>> elseStatements;

            while (stream.Peek().Type != TokenType::EndIf &&
                stream.Peek().Type != TokenType::Eof)
            {
                std::unique_ptr<IStatementNode> statement = StatementParser::ParseStatement(stream);
                if (statement) {
                    elseStatements.push_back(std::move(statement));
                }
                else {
                    break;
                }
            }
            return std::make_optional(std::make_unique<ProgramNode>(std::move(elseStatements)));
        }

        return std::nullopt;
    }
}