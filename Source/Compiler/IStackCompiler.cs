namespace vmbl.Source.Compiler;

public interface IStackCompiler
{
    abstract void MountConstantsTable(Statement statement);

    abstract void EmitPush(int index);

    abstract void EmitMkArray(int amount);

    abstract void EmitDefine(int amount, DefineStmt type);

    abstract void EmitQuery(int amount, QueryStmt type);

    abstract void Emit(Statement statement);

    abstract void Dispose();

    abstract void Compile();
}