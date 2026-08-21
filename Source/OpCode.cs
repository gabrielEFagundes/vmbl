namespace vmbl.Source;

/// <summary> The header bytes for the bytecode file, used to identify if the compiled bytecode is or isn't a VMBL source. </summary>
public enum HeaderBytes : ushort
{
    MGIC_VAL = 0xDEAF,
    VERSION = 0xA01
}

/// <summary> The defined bytes used for each instruction on the bytecode. </summary>
public enum OpCode : byte
{
    DEFINE = 0x01,
    QUERY = 0x02,
    NODE = 0x30,
    OBJ = 0x31,
    PATH = 0x32,
    NEXT = 0x33,

    STRING = 0x11,
    INT = 0x22,
    DOUBLE = 0x33,

    PUSH = 0x50,
    POP = 0x60,
    MK_ARRAY = 0x70,
    PLACEHOLDER = 0xAC,
    ERR = 0xFF // probably deprecated, no use at all for now.
}