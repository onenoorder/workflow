namespace WorkflowZero.Lexing;

public static class Lexer
{
    private static readonly Dictionary<string, TokenType> Keywords = new()
    {
        { "if", TokenType.If },
        { "else", TokenType.Else },
        { "then", TokenType.Then },
        { "endif", TokenType.EndIf },
        { "print", TokenType.Print },
        { "equals", TokenType.ComparisonOperator },
        { "true", TokenType.Bool },
        { "false", TokenType.Bool },
        { "Users", TokenType.Identifier },
        { "loop", TokenType.Loop },
        { "endloop", TokenType.EndLoop },
    };

    public static Queue<Token> Tokenize(StreamReader sourceCodeStream)
    {
        Queue<Token> tokenQueue = new();
        string? line = sourceCodeStream.ReadLine();
        int lineIndex = 1;
        while (line != null)
        {
            AddLineTokensToQueue(line, lineIndex, tokenQueue);
            line = sourceCodeStream.ReadLine();
            lineIndex++;
        }
        sourceCodeStream.Close();
        tokenQueue.Enqueue(new Token("EOF",TokenType.Eof, lineIndex,0));
        return tokenQueue;
    }

    private static void AddLineTokensToQueue(string line, int lineIndex, Queue<Token> tokenQueue)
    {
        for (int charIndex = 0; charIndex < line.Length; charIndex++)
        {
            char character = line[charIndex];
            if (character is ' ')
            {
                continue;
            }
            if (TrySingleCharacterToken(character, lineIndex, charIndex, out Token token))
            {
                tokenQueue.Enqueue(token);
            } else
            {
                if (character is '"')
                {
                    string? value = GetValue(line, charIndex + 1, ch => ch != '"');
                    tokenQueue.Enqueue(new Token(value, TokenType.String, lineIndex, charIndex));
                    charIndex+= value.Length + 1;
                }
                else if (char.IsDigit(character)){
                    string? value = GetValue(line, charIndex, char.IsDigit);
                    tokenQueue.Enqueue(new Token(value, TokenType.Number, lineIndex, charIndex));
                    charIndex+= value.Length - 1;
                }
                else if (char.IsLetter(character)){
                    string? value = GetValue(line, charIndex, char.IsLetter);
                    TokenType type = TokenType.Identifier;
                    if (Keywords.TryGetValue(value, out TokenType keyword))
                    {
                        type = keyword;
                    }
                    tokenQueue.Enqueue(new Token(value, type, lineIndex, charIndex));
                    charIndex+= value.Length - 1;
                }
            }
        }
    }

    private static string? GetValue(string line, int startIndex, Func<char, bool> condition)
    {
        string? value = "";
        while (startIndex < line.Length && condition(line[startIndex]))
        {
            value += line[startIndex];
            startIndex++;
        }
        return value;
    }


    private static bool TrySingleCharacterToken(char character, int lineIndex, int charIndex, out Token token)
    {
        token = null;
        bool result = false;
        switch (character)
        {
            case '(':
                token = new Token(character.ToString(), TokenType.OpenParenthesis, lineIndex, charIndex);
                result =  true;
                break;
            case ')':
                token = new Token(character.ToString(), TokenType.CloseParenthesis, lineIndex, charIndex);
                result =  true;
                break;
            case '.':
                token = new Token(character.ToString(), TokenType.MemberAccessOperator, lineIndex, charIndex);
                result =  true;
                break;
            case '=':
                token = new Token(character.ToString(), TokenType.EqualSign, lineIndex, charIndex);
                result =  true;
                break;
            case '>':
            case '<':
                token = new Token(character.ToString(), TokenType.ComparisonOperator, lineIndex, charIndex);
                result =  true;
                break;
            case '+':
            case '-':
                token = new Token(character.ToString(), TokenType.ArithmeticOperator, lineIndex, charIndex);
                result =  true;
                break;
        }
        return result;
    }
    
}