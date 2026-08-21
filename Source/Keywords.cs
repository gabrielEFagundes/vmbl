using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace vmbl.Source;

public enum TokenTypes
{
    DEFINE, //defines something lol
    QUERY, //queries something lol
    NODE, //used with query/define to query or define a node
    OBJ, //used with query/define to query or define an object that is not part of the graph
    NEXT, //used with query for next project
    PATH, //used with query for steps until next project
    TO, //used with path, pretty print
    HALT, //end
    UNKNOWN, //DEPRECATED - unknown keyword, not even used
    COMMA, //,
    SEMICOLON, //;
    OPENPARENTS, //(
    CLOSEPARENTS, //)
    OPENBRACKET, //[
    CLOSEBRACKET, //]
    EQUALS, //=
    MINUS, //-
    QUOTATION, // "
    STRING, //string
    INT, //integer
    DOUBLE, //floating point
    IDENT //identifier
}

/// <summary>
/// A well defined struct that can hold the following types:
/// <code>
///    string
///    int
///    double
///    List of Value itself
/// </code>
/// </summary>
public struct Value : IEquatable<Value>
{
    private object _value;
    
    private Value(string v) => _value = v;
    private Value(int v) => _value = v;
    private Value(double v) => _value = v;
    private Value(List<Value> v) => _value = v;

    public static implicit operator Value(string v) => new(v);
    public static implicit operator Value(int v) => new(v);
    public static implicit operator Value(double v) => new(v);

    public static implicit operator Value(List<Value> av) => new(av);

    public override string ToString(){
        if(_value is List<Value> list)
            return string.Join(", ", list);

        return _value.ToString() ?? string.Empty;
    }

    public bool Equals(Value other)
    {
        return _value.Equals(other._value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Value v && Equals(v);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public object AsObject => _value;

    public new Type GetType() => _value.GetType();
}

/// <param name="TokenT">The type of the Token</param>
/// <param name="Content">The content of that Token</param>
public record Token
(
    TokenTypes TokenT,
    Value Content
)
{
    /// <summary> Holds the list of the reserved keywords for VMBL. </summary>
    public static readonly FrozenDictionary<string, TokenTypes> ReservedWords = new Dictionary<string, TokenTypes>
    {
        { "DEFINE", TokenTypes.DEFINE },
        { "QUERY", TokenTypes.QUERY },
        { "NODE", TokenTypes.NODE },
        { "OBJ", TokenTypes.OBJ },
        { "NEXT", TokenTypes.NEXT },
        { "PATH", TokenTypes.PATH },
        { "TO", TokenTypes.TO }
    }.ToFrozenDictionary();

    /// <summary> Holds the list of the reserved digits, such as parenthesis and brackets for VMBL </summary>
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