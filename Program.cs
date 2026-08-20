using System.Diagnostics;
using vmbl.Lib;
using vmbl.Source;
using vmbl.Source.Compiler;
using vmbl.Source.Utils;
using vmbl.Source.VM;

Stopwatch stopwatch = Stopwatch.StartNew();

try{
    string[] file = Args.Verify(args);

    string content = File.ReadAllText(file[0]) + '\0';

    IVMStackLexer lexer = new VMStackLexer(content);
    List<Token> tokens = lexer.LexLoop();

    // DEBUG only
    // foreach(var t in tokens) Console.WriteLine(t.ToString());

    IVMStackParser parser = new VMStackParser([.. tokens]);
    List<Statement> statements = [];
    statements = parser.Execute();

    // DEBUG only
    // foreach(var s in statements) Console.WriteLine(s.ToString());

    InternalUts.CreateTargetOutput();

    IStackCompiler compiler = new StackCompiler(statements);
    compiler.Compile();

    stopwatch.Stop();
    InternalUts.CreateBufferedWriter($"build finished in {stopwatch.ElapsedMilliseconds}ms");
}catch(Exception e)
{
    stopwatch.Stop();
    InternalUts.CreateBufferedWriter($"build failed: ${e.StackTrace ?? "No stacktrace available"}");
    Environment.FailFast($"in {stopwatch.ElapsedMilliseconds}ms");
}