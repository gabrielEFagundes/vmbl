namespace vmbl.Source.VM;

public interface IVMStackCompiler
{
    abstract void Expect(TokenTypes type);

    abstract Token Peek();

    abstract void ParseValues();

    abstract void ParseDefineStmt();
    
    abstract void ParseQueryStmt();

    abstract void ParseLoop(Token token);

    abstract void Execute(Token[] tokens);
}