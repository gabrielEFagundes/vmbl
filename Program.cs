using vmbl.Source;

if(args.Length == 0 || args.Length > 1)
{
    Console.WriteLine("Please, provide the script's file path");
    return;
}

string content = File.ReadAllText(args[0]);

Console.WriteLine(content);