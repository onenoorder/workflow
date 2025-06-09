namespace WorkflowsTraining.Lexing;

public enum TokenType
{
    Print,
    If,
    Then,
    Else,
    EndIf,
    String,
    Number,
    Bool,
    Identifier,
    OpenParenthesis,
    CloseParenthesis,
    EqualSign,
    ArithmeticOperator,
    ComparisonOperator,
    MemberAccessOperator,
    Loop,
    EndLoop,
    StringConcatenation,
    Comma,
    Eof
}