using System.Diagnostics;

namespace vmbl.Source.VM;

internal class VMStackCompiler(Token[] Tokens) : IVMStackCompiler
{
    private int _cursor = 0;

    internal bool IsCursorNotOutBounds() => _cursor+1 < Tokens.Length;
    
    public void Expect(TokenTypes keyword)
    {
        if(Peek().TokenT == keyword) _cursor++;
        throw new Exception($"expected {keyword}, found something else.");
    }

    public Token Peek()
    {
        return IsCursorNotOutBounds()?
            Tokens[_cursor+1]
            : Tokens[_cursor];
    }

    public Statement ParseValues()
    {
        Expect(TokenTypes.IDENT);
        Expect(TokenTypes.EQUALS);
        // uh lowk
    }

    public Statement ParseDefineStmt()
    {
        if(Peek().TokenT != TokenTypes.NODE) Expect(TokenTypes.OBJ);
        else Expect(TokenTypes.NODE);

        Expect(TokenTypes.OPENPARENTS);

        while(Tokens[_cursor].TokenT != TokenTypes.CLOSEPARENTS)
        {
            ParseValues();
        }
    }

    public Statement ParseQueryStmt()
    {
        
    }

    public Statement ParseLoop(Token token)
    {
        return token.TokenT switch
        {
            TokenTypes.DEFINE => ParseDefineStmt(),
            TokenTypes.QUERY => ParseQueryStmt(),

            _ => throw new UnreachableException($"parser reached an impossible state on token {token} (VMBL#2)")
        };
    }

    public void Execute(Token[] tokens)
    {
        while (_cursor < tokens.Length)
        {
            ParseLoop(tokens[_cursor]);
        }
    }
}