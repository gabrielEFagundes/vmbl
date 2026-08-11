namespace vmbl.Source.VM;

public class VMStackLexer : IVMStackLexer
{
    int IVMStackLexer.Cursor { get; set; } = 0;
    public static string Content = null!;

    // oh god
    public VMStackLexer(string content)
    {
        Content = content;
    }

    public static Instruction? LexInstruct(IVMStackLexer lexer)
    {
        
        return null;
    }

    public static Instruction[] LexLoop(IVMStackLexer lexer)
    {
        var instructions = new List<Instruction>();
        while(lexer.Cursor < Content.Length)
        {
            
        }

        return instructions;
    }
}