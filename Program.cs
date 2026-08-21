using System.Diagnostics;
using vmbl;
using vmbl.Lib;
using vmbl.Source;
using vmbl.Source.Compiler;
using vmbl.Source.Utils;
using vmbl.Source.VM;

Stopwatch stopwatch = Stopwatch.StartNew();

try{
    _ = new Defaults(); // initialize configurations
    string file = new Args().ParseArgs(args).GetPathOrDoSomething();

    string content = File.ReadAllText(file) + '\0';

    IVMStackLexer lexer = new VMStackLexer(content);
    List<Token> tokens = lexer.LexLoop();

    IVMStackParser parser = new VMStackParser([.. tokens]);
    List<Statement> statements = [];
    statements = parser.Execute();

    InternalUts.CreateTargetOutput();

    IStackCompiler compiler = new StackCompiler(statements);
    compiler.Compile();

    stopwatch.Stop();
    InternalUts.CreateBufferedWriter($"build finished in {stopwatch.ElapsedMilliseconds}ms");
}catch(Exception e)
{
    stopwatch.Stop();
    InternalUts.CreateBufferedWriter($"build failed: {e.Message}");
    Environment.FailFast($"in {stopwatch.ElapsedMilliseconds}ms");
}