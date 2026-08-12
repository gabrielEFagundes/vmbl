using vmbl.Source;
using vmbl.Source.VM;

if (args.Length == 0 || args.Length > 1)
{
    Console.WriteLine("Please, provide the script's file path");
    return;
}

string content = File.ReadAllText(args[0]);

IVMStackLexer lexer = new VMStackLexer(content);
List<Token> tokens = lexer.LexLoop();

foreach(var token in tokens) Console.WriteLine(token.ToString());