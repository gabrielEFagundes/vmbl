namespace vmbl.Source;

// maybe in the future make the nodes a bit more... flexible?

public record Statement(
    TokenTypes TypeStatement
){}

public record Attribution(
    TokenTypes Key,
    string Value
) : Statement(Key){}

public record DefineStmt(
    TokenTypes TypeStatement,
    TokenTypes TypeToCreate,
    params Dictionary<string, Value>[] Values
) : Statement(TypeStatement){}

public record QueryStmt(
    TokenTypes TypeStatement,
    string ValueToQuery
) : Statement(TypeStatement){}