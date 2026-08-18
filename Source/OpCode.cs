namespace vmbl.Source;

public enum HeaderBytes : ushort
{
    MGIC_VAL = 0xDEAF,
    VERSION = 0xA01
}

public enum OpCode : byte
{
    DEFINE = 0x01,
    QUERY = 0x02,
    NODE = 0x30,
    OBJ = 0x31,

    STRING = 0x11,
    INT = 0x22,
    DOUBLE = 0x33,

    INDEX = 0x40,
    PUSH = 0x50,
    POP = 0x60,
    MK_ARRAY = 0x70,
    PLACEHOLDER = 0xAC,
    HALT = 0x00, //end..? might remove
    ERR = 0xFF
}