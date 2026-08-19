using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace vmbl.Source.Utils;

public class CompilerUts
{
    public static Value WriteValue(BinaryWriter writer, Value value)
    {
        if (value.AsObject is string s) { writer.Write((byte)OpCode.STRING); writer.Write(s); }
        else if (value.AsObject is int i) { writer.Write((byte)OpCode.INT); writer.Write(i); }
        else if (value.AsObject is double d) { writer.Write((byte)OpCode.DOUBLE); writer.Write(d); }
        else
            throw new Exception("compiler found an invalid type value when compiling (VMBL@1)");

        return value;
    }

    public static void Write(BinaryWriter writer, uint offset, ushort[] buffer)
    {
        ReadOnlySpan<byte> byteSpan = MemoryMarshal.Cast<ushort, byte>(buffer);
        writer.Seek((int)offset, SeekOrigin.Begin);
        writer.Write(byteSpan);
        writer.Seek(0, SeekOrigin.End);
    }
    
    public static void Write(BinaryWriter writer, Span<byte> buffer)
        => writer.Write(buffer);

    public static int RegisterConst(ref Dictionary<Value, int> consts, Value value)
    {
        if(consts.TryGetValue(value, out int index))
            return index;
        index = consts.Count;
        consts[value] = index;
        return index;
    }

    public static int RegisterConst(ref Dictionary<Value, int> consts, string value)
    {
        if(consts.TryGetValue(value, out int index))
            return index;
        index = consts.Count;
        consts[value] = index;
        return index;
    }

    public static void DisposeDataStruct<TK, TV>(ref Dictionary<TK, TV> list) where TK : notnull
    {
        list.Clear();
        list.TrimExcess();
        list = null!;
    }
}