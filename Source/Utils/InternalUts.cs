namespace vmbl.Source.Utils;

internal class InternalUts
{
    /// <summary>
    /// Homologation method (creates the dir on the project's bin folder)
    /// </summary>
    public static void CreateTargetOutput()
        => Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "out"));

    public static void CreateBufferedWriter(string toWrite)
    {
        using Stream outbuffered = Console.OpenStandardOutput();
        using StreamWriter writer = new(outbuffered);
        writer.AutoFlush = false;
        writer.WriteLine(toWrite);
    }
}