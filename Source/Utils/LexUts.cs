namespace vmbl.Source.Utils;

public class LexUts
{
    public static bool AsciiPunct(char punctuation)
    {
        return punctuation is '(' or ')' or '[' or ']' or ',' or ';' or '=' or '-';
    }

    public static bool IsValidIdent(char letter)
    {
        return (letter >= 'a' && letter <= 'z') 
            || (letter >= 'A' && letter <= 'Z')
            || letter == '_';
    }

    public static bool IsIntOrDouble(char number)
    {
        return char.IsNumber(number) || number == '.';
    }
}