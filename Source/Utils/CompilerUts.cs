using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace vmbl.Source.Utils;

public class CompilerUts
{
    /// <summary>
    /// Writes a Value in its binary form to a stream, usually a FileStream, 
    /// where the bytecode goes to.
    /// </summary>
    public static Value WriteValue(BinaryWriter writer, Value value)
    {
        if (value.AsObject is string s) { writer.Write((byte)OpCode.STRING); writer.Write(s); }
        else if (value.AsObject is int i) { writer.Write((byte)OpCode.INT); writer.Write(i); }
        else if (value.AsObject is double d) { writer.Write((byte)OpCode.DOUBLE); writer.Write(d); }
        else
            throw new Exception("compiler found an invalid type value when compiling (VMBL@1)");

        return value;
    }

    /// <summary> Writes an array of ushorts to a stream, usually a FileStream. </summary>
    /// <param name="writer">The binary writer</param>
    /// <param name="offset">The offset from the beggining of the stream</param>
    /// <param name="buffer">The array buffer of ushorts</param>
    public static void Write(BinaryWriter writer, uint offset, ushort[] buffer)
    {
        ReadOnlySpan<byte> byteSpan = MemoryMarshal.Cast<ushort, byte>(buffer);
        writer.Seek((int)offset, SeekOrigin.Begin);
        writer.Write(byteSpan);
        writer.Seek(0, SeekOrigin.End);
    }
    
    /// <summary> Writes a Span of bytes to a stream, usually a FileStream. </summary>
    /// <param name="writer">The binary writer</param>
    /// <param name="buffer">The Span (array) buffer of bytes</param>
    public static void Write(BinaryWriter writer, Span<byte> buffer)
        => writer.Write(buffer);

    /// <summary>
    /// Adds a constant to the constants table loaded inside a Dictionary.
    /// Ignores duplicates.
    /// </summary>
    public static int RegisterConst(ref Dictionary<Value, int> consts, Value value)
    {
        if(consts.TryGetValue(value, out int index))
            return index;
        index = consts.Count;
        consts[value] = index;
        return index;
    }

    /// <summary>
    /// Disposes the referentied data structure, on the use case of VMBL, only a Dictionary overload is available.
    /// </summary>
    public static void DisposeDataStruct<TK, TV>(ref Dictionary<TK, TV> list) where TK : notnull
    {
        list.Clear();
        list.TrimExcess();
        list = null!;
    }
}