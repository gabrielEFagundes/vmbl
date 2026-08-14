namespace vmbl.Source.VM;

public interface IVMStackParser
{
    abstract TokenTypes Expect(TokenTypes type);

    abstract TokenTypes ExpectEither(params TokenTypes[] types);

    abstract Token Peek();

    abstract Value ParseValues();

    abstract Attribution ParseAttribute();

    abstract Statement ParseDefineNodeStmt();

    abstract Statement ParseDefineObjStmt();
    
    abstract Statement ParseQueryStmt();

    abstract Statement ParseLoop(Token token);

    abstract List<Statement> Execute();
}