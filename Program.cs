using vmbl.Source;
using vmbl.Source.Compiler;
using vmbl.Source.VM;

if (args.Length == 0 || args.Length > 1)
{
    Console.WriteLine("Please, provide the script's file path");
    return;
}

string content = File.ReadAllText(args[0]) + '\0';

// TODO: enhance this lexer, it's kinda bad
IVMStackLexer lexer = new VMStackLexer(content);
List<Token> tokens = lexer.LexLoop();

// DEBUG only
//foreach(var t in tokens) Console.WriteLine(t.ToString());

IVMStackParser parser = new VMStackParser([.. tokens]);
List<Statement> statements = [];
try
{
    statements = parser.Execute();
}catch(Exception e)
{
    Console.WriteLine(e);
}

// DEBUG only
//foreach(var s in statements) Console.WriteLine(s.ToString());

IStackCompiler compiler = new StackCompiler(statements);
compiler.Compile();