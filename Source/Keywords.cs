using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

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