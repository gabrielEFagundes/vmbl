namespace vmbl.Source.Utils;

internal class InternalUts
{
    public static void CreateTargetOutput()
        => Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "out"));
}