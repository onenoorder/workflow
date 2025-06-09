#pragma once

namespace WorkflowsTraining {
    namespace Lexing {
        enum class TokenType {
            If,
            Else,
            Then,
            EndIf,
            Print,
            ComparisonOperator, // For 'equals', '>', '<'
            Bool,               // For 'true', 'false'
            Identifier,         // For 'Users', 'Clients'
            Loop,
            EndLoop,
            OpenParenthesis,    // (
            CloseParenthesis,   // )
            MemberAccessOperator, // .
            EqualSign,          // =
            ArithmeticOperator, // +, -, /, *
            StringConcatenation, // ~
            String,             // "..."
            Number,
            Eof,                // End of File
            Comma,              // ,
            Unknown             // For unhandled characters/tokens
        };
    }
}