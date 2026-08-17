namespace vmbl.Source;

public enum HeaderBytes : ushort
{
    MGIC_VAL = 0xDEAF,
    VERSION = 0xA01,
    CONST_COUNT = 0xCC
}

public enum OpCode : byte
{
    DEFINE = 0x01,
    QUERY = 0x02,
    NODE = 0x30,
    OBJ = 0x31,

    INDEX = 0x40,
    PUSH = 0x50,
    POP = 0x60,
    MK_ARRAY = 0x70,
    HALT = 0x00, //end
    ERR = 0xFF
}