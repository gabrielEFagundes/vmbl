using vmbl.Source.Utils;

namespace vmbl.Lib;

internal class Args
{
    private string[]? _inputArgs { get; set; }
    private static string[] _internalArgs = ["-o", "-h", "--help", "-v", "--version"];

    public Args ParseArgs(string[] args)
    {
        if(args.Length < 1) 
        {
            InternalUts.CreateBufferedWriter("Please, provide the script's file path.\nType 'vmbl ?' or 'vmbl -h/--help' for help");
            Environment.Exit(1);
        }

        _inputArgs = args;
        return this;
    }

    public void DoSomething()
    {
        if(_inputArgs != null && _inputArgs.FirstOrDefault(arg => _internalArgs.Contains(arg)) != null)
            for(int i = 0; i < _inputArgs.Length; i++)
            {
                // I'm tired
                _ = _inputArgs[i] switch
                {
                    "-h" => null,
                    _ => default
                };
            }
    }
}