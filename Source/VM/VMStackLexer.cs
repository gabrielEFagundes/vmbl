using System.Diagnostics;
using vmbl.Source.Utils;

namespace vmbl.Source.VM;

public class VMStackLexer(string Content) : IVMStackLexer
{
    internal bool IsCursorNotForwardLength(int cursor) => cursor+1 < Content.Length;

    public char Peek(int cursor)
        => cursor+1 < Content.Length ? Content[cursor+1] : '\0';

    public Token LexDigit(char c, ref int cursor)
    {
        if(Token.ReservedDigits.TryGetValue(c, out var cType))
        {
            if(cType == TokenTypes.MINUS && Peek(cursor) == '-')
            {
                // consume all until next \n
                while(IsCursorNotForwardLength(cursor) && (Content[cursor] != '\n'))
                    cursor++;
                return LexInstruct(Content[cursor], ref cursor);
            }
            cursor++;
            return new Token(cType, c.ToString());
        }

        cursor++;
        return new Token(TokenTypes.UNKNOWN, "\0");
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

        return new Token(TokenTypes.IDENT, word);
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
        return new Token(TokenTypes.STRING, word);
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
            return new Token(TokenTypes.INT, n);

        else if(double.TryParse(number, out double d))
            return new Token(TokenTypes.DOUBLE, d);

        else
            return new Token(TokenTypes.UNKNOWN, "\0");
    }

    public Token LexHalt(ref int cursor)
    {
        cursor++;
        return new Token(TokenTypes.HALT, "\0");
    }

    public Token LexRecursive(ref int cursor)
    {
        cursor++;
        return IsCursorNotForwardLength(cursor)? 
            LexInstruct(Content[cursor], ref cursor) 
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
            var c when LexUts.IsNonCharacter(c) => LexRecursive(ref cursor),
            '\0' => LexHalt(ref cursor),

            _ => throw new UnreachableException($"lexer reached an impossible state on char {character} (VMBL#1)")
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