namespace vmbl.Source.Utils;

public class LexUts
{
    /// <returns>True if the current char is an ascii punctuation, false otherwise</returns>
    public static bool AsciiPunct(char punctuation)
        => punctuation is '(' or ')' or '[' or ']' or ',' or ';' or '=' or '-';

    /// <returns>True if the current char is a letter or a valid underscore, false otherwise</returns>
    public static bool IsValidIdent(char letter)
        => (letter >= 'a' && letter <= 'z') 
        || (letter >= 'A' && letter <= 'Z')
        || letter == '_';

    /// <returns>True if the current char is a number (integer or double), false otherwise</returns>
    public static bool IsIntOrDouble(char number)
        => char.IsNumber(number) || number == '.';

    /// <returns>True if the current char is a non-ascii readable character, false otherwise</returns>
    public static bool IsNonCharacter(char character) 
        => character == '\n' || character == '\t' || character == ' ';
    
}