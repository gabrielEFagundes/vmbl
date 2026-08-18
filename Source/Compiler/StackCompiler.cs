using System.Diagnostics;
using vmbl.Source.Utils;

namespace vmbl.Source.Compiler;

public class StackCompiler(List<Statement> statements) : IStackCompiler
{
    public Statement[] statements = [.. statements];
    private static readonly Stream _stream = new FileStream("compiled.ksc", FileMode.Create, FileAccess.ReadWrite);
    private readonly BinaryWriter _binaryWriter = new(_stream);
    private static readonly ushort[] _headers = [(ushort)HeaderBytes.MGIC_VAL, (ushort)HeaderBytes.VERSION, (ushort)OpCode.PLACEHOLDER];
    private static Dictionary<Value, int> _constants = [];

    public void MountConstantsTable(Statement statement)
    {
        if(statement is DefineStmt def)
        {
            foreach(var att in def.Attributes)
            {
                if(!_constants.ContainsKey(att.Value))
                {
                    if(att.Value.AsObject is List<Value> list)
                    {
                        CompilerUts.RegisterConst(ref _constants, att.Key);
                        foreach(var el in list)
                            CompilerUts.RegisterConst(ref _constants, el);
                        
                        continue;
                    }

                    CompilerUts.RegisterConst(ref _constants, att.Key);
                    CompilerUts.RegisterConst(ref _constants, att.Value);
                }
            }
        }
        else if(statement is QueryStmt qry 
            && (!_constants.ContainsKey(qry.ValueToQuery.Value) || !_constants.ContainsKey(qry.ValueToQuery.Key)))
        {
            CompilerUts.RegisterConst(ref _constants, qry.ValueToQuery.Key);
            CompilerUts.RegisterConst(ref _constants, qry.ValueToQuery.Value);
        }
    }

    public void EmitPush(int index)
        => CompilerUts.Write(_binaryWriter, [(ushort)OpCode.PUSH, (ushort)index]);

    public void EmitMkArray(int amount)
        => CompilerUts.Write(_binaryWriter, [(ushort)OpCode.MK_ARRAY, (ushort)amount]);

    public void EmitDefine(int amount, DefineStmt type)
    {
        CompilerUts.Write(_binaryWriter, [(ushort)OpCode.DEFINE]);
        if(type.TypeToCreate == TokenTypes.NODE)
            CompilerUts.Write(_binaryWriter, [(ushort)OpCode.NODE, (ushort)amount]);
        else
            CompilerUts.Write(_binaryWriter, [(ushort)OpCode.OBJ, (ushort)amount]);
    }

    public void EmitQuery(int amount, QueryStmt type)
    {
        CompilerUts.Write(_binaryWriter, [(ushort)OpCode.QUERY]);
        if(type.TypeToQuery == TokenTypes.NODE)
            CompilerUts.Write(_binaryWriter, [(ushort)OpCode.NODE, (ushort)amount]);
        else
            CompilerUts.Write(_binaryWriter, [(ushort)OpCode.OBJ, (ushort)amount]);
    }

    public void Emit(Statement statement)
    {
        if(statement is DefineStmt define)
        {
            foreach(var d in define.Attributes)
                if(d.Value.AsObject is List<Value> ls)
                {
                    EmitPush(_constants[d.Key]);
                    foreach(var l in ls)
                        EmitPush(_constants[l]);
                    EmitMkArray(ls.Count);
                }
                else
                {
                    EmitPush(_constants[d.Key]);
                    EmitPush(_constants[d.Value]);
                }
            EmitDefine(define.Attributes.Count, define);

        }else if(statement is QueryStmt query)
        {
            EmitPush(_constants[query.ValueToQuery.Key]);
            EmitPush(_constants[query.ValueToQuery.Value]);
            EmitQuery(1, query);
        }else
            throw new UnreachableException($"compiler reached an impossible state at {statement} (VMBL#3)");
        
    }

    public void Dispose()
    {
        CompilerUts.DisposeDataStruct(ref _constants);
        _stream.Dispose();
        _binaryWriter.Dispose();
    }

    public void Compile()
    {
        CompilerUts.Write(_binaryWriter, _headers);

        foreach(var statement in statements)
            MountConstantsTable(statement);

        foreach(var c in _constants)
        {
            CompilerUts.WriteValue(_binaryWriter, c.Key);
            Console.WriteLine(c.ToString());
        }

        CompilerUts.WriteAt(_binaryWriter, 4, [(ushort)_constants.Count]);

        foreach(var statement in statements)
            Emit(statement);

        Dispose();
    }
}