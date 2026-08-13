namespace vmbl.Source.Utils;

public class LexUts
{
    public static bool AsciiPunct(char punctuation)
        => punctuation is '(' or ')' or '[' or ']' or ',' or ';' or '=' or '-';

    public static bool IsValidIdent(char letter)
        => (letter >= 'a' && letter <= 'z') 
        || (letter >= 'A' && letter <= 'Z')
        || letter == '_';

    public static bool IsIntOrDouble(char number)
        => char.IsNumber(number) || number == '.';

    public static bool IsNonCharacter(char character) 
        => character == '\n' || character == '\t' || character == ' ';
    
}