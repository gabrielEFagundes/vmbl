namespace vmbl.Source;

/// <param name="TypeStatement">Type of the Statement</param>
public record Statement(
    TokenTypes TypeStatement
){}

/// <param name="Key">The key (e.g name, id, title)</param>
/// <param name="Value">The value for the key (e.g "Gabriel", 1, "Learning C#"</param>
public record Attribution(
    string Key,
    Value Value
){}

/// <param name="TypeStatement">Type of the Statement</param>
/// <param name="TypeToCreate">Type to define (NODE, OBJ)</param>
/// <param name="Attributes">Attributes composing the DefineStatement</param>
public record DefineStmt(
    TokenTypes TypeStatement,
    TokenTypes TypeToCreate,
    List<Attribution> Attributes
) : Statement(TypeStatement)
{
    public override string ToString()
    => $"DefineStmt {{ TypeStatement = {TypeStatement}, TypeToCreate = {TypeToCreate}, Attributes = {string.Join(",", Attributes)} }}";
}

/// <param name="TypeStatement">Type of the Statement</param>
/// <param name="TypeToQuery">Type to query (NODE, OBJ, PATH, NEXT)</param>
/// <param name="ValueToQuery">The value it'll query (name="Gabriel", id=1)</param>
public record QueryStmt(
    TokenTypes TypeStatement,
    TokenTypes TypeToQuery,
    Attribution ValueToQuery
) : Statement(TypeStatement){}