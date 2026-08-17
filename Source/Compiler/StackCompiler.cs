using vmbl.Source.Utils;

namespace vmbl.Source.Compiler;

public class StackCompiler(List<Statement> statements) : IStackCompiler
{
    public Statement[] statements = [.. statements];
    private static readonly Stream _stream = new FileStream("compiled.ksc", FileMode.Create, FileAccess.ReadWrite);
    private readonly BinaryWriter _binaryWriter = new(_stream);
    private static readonly ushort[] _headers = [(ushort)HeaderBytes.MGIC_VAL, (ushort)HeaderBytes.VERSION, (ushort)HeaderBytes.CONST_COUNT, 0];
    private static HashSet<Value> _constants = [];

    public void MountConstantsTable(Statement statement)
    {
        if(statement is DefineStmt def)
        {
            foreach(var att in def.Attributes)
            {
                _constants.Add(att.Key); 
                _constants.Add(att.Value);
            }
        }

        if(statement is QueryStmt qry)
        {
            _constants.Add(qry.ValueToQuery.Key);
            _constants.Add(qry.ValueToQuery.Value);
        }

        _headers[3] = (ushort) _constants.Count;
        CompilerUts.Write(_binaryWriter, _headers);

        foreach(var c in _constants)
        {
            CompilerUts.WriteValue(_binaryWriter, c);
        }

        _constants.Clear();
        _constants.TrimExcess();
        _constants = null!;
    }

    public void EmitPush(Value v)
    {
        //CompilerUts.Write(_binaryWriter, OpCode.PUSH);
    }

    public void Emit(Statement statement)
    {
        if(statement is DefineStmt define)
            foreach(var d in define.Attributes)
                if(d.Value.AsObject is List<Value> ls)
                    foreach(var l in ls)
                        EmitPush(l);
        
    }

    public void Dispose()
    {
        _stream.Dispose();
        _binaryWriter.Dispose();
    }

    public void Compile()
    {
        foreach(var statement in statements)
            MountConstantsTable(statement);

        foreach(var statement in statements)
            Emit(statement);

        Dispose();
    }
}