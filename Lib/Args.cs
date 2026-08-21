using vmbl.Source.Utils;

namespace vmbl.Lib;

internal class Args
{
    private string[] _inputArgs { get; set; } = [];

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

    public string GetPathOrDoSomething()
    {
        
        for(int i = 0; i < _inputArgs.Length; i++)
        {
            switch (_inputArgs[i])
            {
                case "-v":
                case "--version":
                    InternalUts.CreateBufferedWriter($"VMBL version {Defaults.Version}\nby {Defaults.Author}");
                    Environment.Exit(0);
                    break;

                case "-h":
                case "--help":
                case "?":
                    InternalUts.CreateBufferedWriter("VMBL\n\t-h / --help: help\n\t-v / --version: current installed version");
                    Environment.Exit(0);
                    break;
            }
        }
        return _inputArgs[0]; // always the script's path
    }
}