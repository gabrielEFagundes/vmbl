using System.Collections.Frozen;

namespace vmbl.Source;

public enum TokenTypes
{
    DEFINE, //defines something lol
    QUERY,
    NODE, //used with query/define to query or define a node
    OBJ, //used with query/define to query or define an object that is not part of the graph
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
    INT,
    DOUBLE,
    IDENT
}

public struct Value
{
    private readonly object _value;
    private Value(object value) => _value = value;

    public static implicit operator Value(string v) => new(v);
    public static implicit operator Value(int v) => new(v);
    public static implicit operator Value(double v) => new(v);

    public static implicit operator Value(List<Value> av) => new(av);

    public override string ToString(){
        if(_value is List<Value> list)
            return string.Join(", ", list);

        return _value.ToString() ?? string.Empty;
    }
    public object AsObject() => _value;
}

public record Token
(
    TokenTypes TokenT,
    Value Content
)
{
    public static readonly FrozenDictionary<string, TokenTypes> ReservedWords = new Dictionary<string, TokenTypes>
    {
        { "DEFINE", TokenTypes.DEFINE },
        { "QUERY", TokenTypes.QUERY },
        { "NODE", TokenTypes.NODE },
        { "OBJ", TokenTypes.OBJ },
        { "NEXT", TokenTypes.NEXT },
        { "PATH", TokenTypes.PATH }
    }.ToFrozenDictionary();

    public static readonly FrozenDictionary<char, TokenTypes> ReservedDigits = new Dictionary<char, TokenTypes>
    {
        { '(', TokenTypes.OPENPARENTS },
        { ')', TokenTypes.CLOSEPARENTS },
        { '[', TokenTypes.OPENBRACKET },
        { ']', TokenTypes.CLOSEBRACKET },
        { ',', TokenTypes.COMMA },
        { ';', TokenTypes.SEMICOLON },
        { '=', TokenTypes.EQUALS },
        { '-', TokenTypes.MINUS },
    }.ToFrozenDictionary();
}