namespace vmbl.Source;

public enum OpCode
{
    DEFINE, //defines something lol
    QUERY,
    NODE, //used with query/define to query or define a node
    DEV, //used with query/define to query or define a dev
    NEXT, //used with query for next project
    PATH, //used with query for steps until next project
    HALT //end
}

public enum Keywords
{
    COLON, //,
    SEMICOLON, //;
    OPENPARENTS, //(
    CLOSEPARENTS, //)
    OPENBRACKET, //[
    CLOSEBRACKET, //]
    EQUALS, //=
    MINUS, //-
    IDENT
}

public record Token
(
    OpCode Code,
    Keywords? Keyword,
    string Content
){}

public record Instruction(
    Type Code,
    object[]? Params
){}

public struct Type
{
    public OpCode? Code;
    public Keywords? Keyword;
}