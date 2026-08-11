namespace vmbl.Source.VM;

public interface IVMStackCompiler
{
    int Cursor { get; set; }

    static abstract void Expect(Type type);

    static abstract object Peek();

    static abstract void Dispose();

    static abstract void Execute(IVMStackCompiler compiler, Instruction[] instructions);
}