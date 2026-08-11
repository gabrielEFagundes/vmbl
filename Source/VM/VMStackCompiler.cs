namespace vmbl.Source.VM;

internal class VMStackCompiler(string content) : IVMStackCompiler
{
    int IVMStackCompiler.Cursor { get; set; } = 0;

    public static void Expect(Type type)
    {
        throw new NotImplementedException();
    }

    public static object Peek()
    {
        throw new NotImplementedException();
    }

    public static void Dispose()
    {
        throw new NotImplementedException();
    }

    public static void Execute(IVMStackCompiler compiler, Instruction[] instructions)
    {
        while(compiler.Cursor < instructions.Length)
        {

            compiler.Cursor++;
        }
    }
}