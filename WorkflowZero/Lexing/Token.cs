namespace WorkflowZero.Lexing;

public record Token(string Value, TokenType Type, int LineIndex, int CharIndex);