namespace WorkflowZero.Lexing;

public class TokenStream
{
    private Queue<Token> tokens;
    private Token current;

    public TokenStream(Queue<Token> tokens)
    {
        this.tokens = tokens;
        current = tokens.Dequeue();
    }

    public Token Peek()
    {
        return current;
    }

    public Token PeekNext()
    {
        return tokens.Peek();
    }

    public Token Eat()
    {
        Token eatenToken = current;
        current = tokens.Dequeue();
        return eatenToken;
    }


    public void Expect(TokenType type)
    {
        Token nextToken = Eat();

        if (nextToken.Type != type)
            throw new Exception(
                $"Expected {type.ToString()} at line {nextToken.LineIndex} but found {nextToken.Type.ToString()}");
    }


    public bool EndOfFile()
    {
        return Peek().Type == TokenType.Eof;
    }
}