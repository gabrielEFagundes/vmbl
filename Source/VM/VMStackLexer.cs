using System.Diagnostics;
using vmbl.Source.Utils;

namespace vmbl.Source.VM;

public class VMStackLexer(string Content) : IVMStackLexer
{
    internal bool IsCursorNotForwardLength(int cursor) => cursor+1 < Content.Length;

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
                while(IsCursorNotForwardLength(cursor) && (Content[cursor] != '\n' || Content[cursor] != '\0'))
                    cursor++;
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

        while(IsCursorNotForwardLength(cursor) && LexUts.IsValidIdent(Content[cursor]))
        {
            word += Content[cursor];
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

    public Token LexString(ref int cursor)
    {
        cursor++; // consume start quote
        string word = "";

        while(Content[cursor] != '"')
        {
            word += Content[cursor];
            cursor++;
        }

        cursor++;
        return new Token(Keywords.STRING, word);
    }

    public Token LexNumber(ref int cursor)
    {
        string number = "";
        while (LexUts.IsIntOrDouble(Content[cursor]))
        {
            number += Content[cursor];
            cursor++;
        }
        
        if(int.TryParse(number, out int n)) 
            return new Token(Keywords.INT, n);

        else if(double.TryParse(number, out double d))
            return new Token(Keywords.DOUBLE, d);

        else
            return new Token(Keywords.UNKNOWN, "\0");
    }

    public Token LexRecursive(string content, ref int cursor)
    {
        cursor++;
        return IsCursorNotForwardLength(cursor)? 
            LexInstruct(content[cursor], ref cursor) 
            : LexInstruct('\0', ref cursor);
    }

    public Token LexInstruct(char character, ref int cursor)
    {
        return character switch
        {
            var c when char.IsAsciiLetter(c) => LexIdent(ref cursor),
            var c when char.IsNumber(c) => LexNumber(ref cursor),
            var c when LexUts.AsciiPunct(c) => LexDigit(c, ref cursor),
            var c when c == '"' => LexString(ref cursor),
            var c when LexUts.IsNonCharacter(c) => LexRecursive(Content, ref cursor),
            '\0' => new Token(Keywords.HALT, "\0"),

            _ => throw new UnreachableException($"Lexer reached an impossible state on char {character} (VMBL01)")
        };
    }

    public List<Token> LexLoop()
    {
        var tokens = new List<Token>();
        int cursor = 0;

        while(cursor < Content.Length)
            tokens.Add(LexInstruct(Content[cursor], ref cursor));

        return tokens;
    }
}