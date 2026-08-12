namespace vmbl.Source.VM;

public class VMStackLexer(string Content) : IVMStackLexer
{

    public static bool AsciiPunct(char punctuation)
    {
        return punctuation is '(' or ')' or '[' or ']' or ',' or ';' or '=' or '-';
    }

    public char Peek(int cursor)
    {
        return cursor+1 < Content.Length ? Content[cursor+1] : '\0';
    }

    public Token LexDigit(char c, ref int cursor)
    {
        if(Token.ReservedDigits.TryGetValue(c, out var cType))
        {
            if(cType == Keywords.MINUS && Peek(cursor) == '-')
            {
                // consume all until next \n
                while(Content[cursor] != '\n')
                {
                    cursor++;
                }
                return LexInstruct(Content[cursor], ref cursor);
            }
            cursor++;
            return new Token(cType, c.ToString());
        }

        cursor++;
        return new Token(Keywords.UNKNOWN, "\0");
    }

    public Token LexIdent(ref int cursor)
    {
        string word = "";
        char curr = Content[cursor];

        word += curr; // include the first char
        while(curr != '\0')
        {
            curr = Peek(cursor);            
            word += curr;
            cursor++;
        }

        if (Token.ReservedWords.TryGetValue(word, out var keywordType))
        {
            cursor++;
            return new Token(keywordType, word);
        }

        cursor++;
        return new Token(Keywords.IDENT, word);
    }

    public Token LexQuotes(ref int cursor)
    {
        cursor++; // consume start quote
        char startPoint = Content[cursor];
        string word = "";

        while(startPoint != '"')
        {
            word += Content[cursor];
            cursor++;
        }

        cursor++;
        return new Token(Keywords.STRING, word);
    }

    public Token LexRecursive(string content, ref int cursor)
    {
        cursor++;
        return LexInstruct(content[cursor], ref cursor);
    }

    public Token LexInstruct(char character, ref int cursor)
    {
        return character switch
        {
            var c when char.IsAsciiLetter(c) => LexIdent(ref cursor),
            var c when AsciiPunct(c) => LexDigit(c, ref cursor),
            var c when c == ' ' || c == '\n' || c == '\t' => LexRecursive(Content, ref cursor),
            _ => new Token(Keywords.HALT, "\0")
        };
    }

    public List<Token> LexLoop()
    {
        var tokens = new List<Token>();
        int cursor = 0;

        while(cursor < Content.Length)
        {
            tokens.Add(LexInstruct(Content[cursor], ref cursor));
            Console.WriteLine(cursor);
        }

        return tokens;
    }
}