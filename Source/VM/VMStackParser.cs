using System.Diagnostics;

namespace vmbl.Source.VM;

internal class VMStackParser(Token[] Tokens) : IVMStackParser
{
    private int _cursor = 0;

    internal bool IsCursorNotOutBounds() => _cursor+1 < Tokens.Length;
    internal TokenTypes CurrTknType() => Tokens[_cursor].TokenT;
    
    public TokenTypes Expect(TokenTypes keyword)
    {
        TokenTypes next = Peek().TokenT;
        //Console.WriteLine(keyword.ToString());
        if(next == keyword)
        {
            _cursor++;
            return next;
        }
        throw new Exception($"expected {keyword}, found {next}");
    }

    public TokenTypes ExpectEither(params TokenTypes[] types)
    {
        TokenTypes next = Peek().TokenT;
        //Console.WriteLine(next.ToString());
        if(types.Contains(next))
        {
            _cursor++;
            return Tokens[_cursor].TokenT;
        }
        throw new Exception($"expected either {string.Join(',', types)}, found {next}");
    }

    public Token Peek()
    {
        return IsCursorNotOutBounds()?
            Tokens[_cursor+1]
            : Tokens[_cursor];
    }

    public Value ParseValues()
    {
        Value value;
        TokenTypes type = ExpectEither(TokenTypes.STRING, TokenTypes.INT, TokenTypes.DOUBLE, TokenTypes.OPENBRACKET);
        if(type == TokenTypes.OPENBRACKET)
        {
            var values = new List<Value>();
            TokenTypes tmpT;
            while(CurrTknType() != TokenTypes.CLOSEBRACKET)
            {
                values.Add(ParseValues());
                tmpT = ExpectEither(TokenTypes.COMMA, TokenTypes.CLOSEBRACKET);
                if(tmpT == TokenTypes.CLOSEBRACKET) break;
            }
            value = values;
        }else value = Tokens[_cursor].Content;

        return value;
    }

    public Attribution ParseAttribute()
    {
        Expect(TokenTypes.IDENT);
        string key = Tokens[_cursor].Content.ToString();

        Expect(TokenTypes.EQUALS);
        Value value = ParseValues();

        if(Peek().TokenT != TokenTypes.CLOSEPARENTS)
            Expect(TokenTypes.SEMICOLON);
        
        return new Attribution(key, value);
    }

    public Statement ParseDefineNodeStmt()
    {
        Expect(TokenTypes.NODE);
        Expect(TokenTypes.OPENPARENTS);
        List<Attribution> attributes = [];

        while(Peek().TokenT != TokenTypes.CLOSEPARENTS)
        {
            attributes.Add(ParseAttribute());
        }
        Expect(TokenTypes.CLOSEPARENTS);
        Expect(TokenTypes.SEMICOLON);

        _cursor++;
        return new DefineStmt(TokenTypes.DEFINE, TokenTypes.NODE, attributes);
    }

    public Statement ParseDefineObjStmt()
    {
        Expect(TokenTypes.OBJ);
        Expect(TokenTypes.OPENPARENTS);
        List<Attribution> attributes = [];

        while(Peek().TokenT != TokenTypes.CLOSEPARENTS)
        {
            attributes.Add(ParseAttribute());
        }
        Expect(TokenTypes.CLOSEPARENTS);
        Expect(TokenTypes.SEMICOLON);

        _cursor++;
        return new DefineStmt(TokenTypes.DEFINE, TokenTypes.OBJ, attributes);
    }

    public Statement ParseQueryStmt()
    {
        TokenTypes type = ExpectEither(TokenTypes.NODE, TokenTypes.OBJ);
        Attribution value = ParseAttribute();
        
        _cursor++;
        return new QueryStmt(TokenTypes.QUERY, type, value);
    }

    public Statement ParseLoop(Token token)
    {
        return token.TokenT switch
        {
            var t when t == TokenTypes.DEFINE 
                && Peek().TokenT == TokenTypes.NODE => ParseDefineNodeStmt(),

            var t when t == TokenTypes.DEFINE 
                && Peek().TokenT == TokenTypes.OBJ => ParseDefineObjStmt(),

            TokenTypes.QUERY => ParseQueryStmt(),

            _ => throw new UnreachableException($"parser reached an impossible state on token {token} (VMBL#2)")
        };
    }

    public List<Statement> Execute()
    {
        List<Statement> ast = [];
        while (CurrTknType() != TokenTypes.HALT)
            ast.Add(ParseLoop(Tokens[_cursor]));

        return ast;
    }
}