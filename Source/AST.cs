namespace vmbl.Source;

// maybe in the future make the nodes a bit more... flexible?

public record Statement(
    TokenTypes TypeStatement
){}

public record Attribution(
    string Key,
    Value Value
){}

public record DefineStmt(
    TokenTypes TypeStatement,
    TokenTypes TypeToCreate,
    List<Attribution> Attributes
) : Statement(TypeStatement)
{
    public override string ToString()
    => $"DefineStmt {{ TypeStatement = {TypeStatement}, TypeToCreate = {TypeToCreate}, Attributes = {string.Join(",", Attributes)} }}";
}

public record QueryStmt(
    TokenTypes TypeStatement,
    TokenTypes TypeToQuery,
    Attribution ValueToQuery
) : Statement(TypeStatement){}