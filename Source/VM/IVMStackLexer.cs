namespace vmbl.Source.VM;

public interface IVMStackLexer
{
    abstract char Peek(int cursor);

    abstract Token LexInstruct(char character, ref int cursor);

    abstract List<Token> LexLoop();

    abstract Token LexDigit(char character, ref int cursor);

    abstract Token LexIdent(ref int cursor);

    abstract Token LexString(ref int cursor);

    abstract Token LexNumber(ref int cursor);

    abstract Token LexRecursive(string source, ref int cursor);
}