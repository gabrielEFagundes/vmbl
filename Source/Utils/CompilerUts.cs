using System.Runtime.InteropServices;

namespace vmbl.Source.Utils;

public class CompilerUts
{
    public static bool IsOutOfBounds(int x, List<Value> iterable) => x < iterable.Count;

    public static void WriteValue(BinaryWriter writer, Value value)
    {
        if (value.AsObject is string s) writer.Write(s);
        else if (value.AsObject is int i) writer.Write(i);
        else if (value.AsObject is double d) writer.Write(d);
        else if (value.AsObject is List<Value> values)
        {
            HashSet<Value> nonRepeatingVals = [];
            foreach(var v in values)
            {
                if(nonRepeatingVals.Add(v))
                    WriteValue(writer, v);
            }
        }
        else
            throw new Exception("compiler found an invalid type value when compiling (VMBL@1)");
    }
    
    public static void Write(BinaryWriter writer, ushort[] bytes)
    {
        ReadOnlySpan<byte> byteSpan = MemoryMarshal.Cast<ushort, byte>(bytes);
        writer.Write(byteSpan);
    }
}