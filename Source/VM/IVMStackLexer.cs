namespace vmbl.Source.VM;

public interface IVMStackLexer
{
    int Cursor { get; set; }

    static abstract Instruction LexInstruct(IVMStackLexer lexer);

    static abstract Instruction[] LexLoop(IVMStackLexer lexer);
}