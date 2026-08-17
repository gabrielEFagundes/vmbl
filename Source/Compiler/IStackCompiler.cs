namespace vmbl.Source.Compiler;

public interface IStackCompiler
{
    abstract void MountConstantsTable(Statement statement);

    abstract void Dispose();

    abstract void Compile();
}