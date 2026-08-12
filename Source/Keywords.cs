using System.Collections.Frozen;

namespace vmbl.Source;

public enum Keywords
{
    DEFINE, //defines something lol
    QUERY,
    NODE, //used with query/define to query or define a node
    DEV, //used with query/define to query or define a dev
    NEXT, //used with query for next project
    PATH, //used with query for steps until next project
    HALT, //end
    UNKNOWN,
    COMMA, //,
    SEMICOLON, //;
    OPENPARENTS, //(
    CLOSEPARENTS, //)
    OPENBRACKET, //[
    CLOSEBRACKET, //]
    EQUALS, //=
    MINUS, //-
    QUOTATION, // "
    STRING,
    IDENT
}

public record Token
(
    Keywords Keyword,
    string Content
)
{
    public static readonly FrozenDictionary<string, Keywords> ReservedWords = new Dictionary<string, Keywords>
    {
        { "DEFINE", Keywords.DEFINE },
        { "QUERY", Keywords.QUERY },
        { "NODE", Keywords.QUERY },
        { "DEV", Keywords.DEV },
        { "NEXT", Keywords.NEXT },
        { "PATH", Keywords.PATH }
    }.ToFrozenDictionary();

    public static readonly FrozenDictionary<char, Keywords> ReservedDigits = new Dictionary<char, Keywords>
    {
        { '(', Keywords.OPENPARENTS },
        { ')', Keywords.CLOSEPARENTS },
        { '[', Keywords.OPENBRACKET },
        { ']', Keywords.CLOSEBRACKET },
        { ',', Keywords.COMMA },
        { ';', Keywords.SEMICOLON },
        { '=', Keywords.EQUALS },
        { '-', Keywords.MINUS },
    }.ToFrozenDictionary();
}