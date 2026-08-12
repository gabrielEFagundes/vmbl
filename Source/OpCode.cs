namespace vmbl.Source;

public enum OpCode : byte
{
    DEF = 0x01,
    QUR = 0x02,
    NOD = 0x30,
    DEV = 0x31,
    NEX = 0x32,
    PAT = 0x33,
    HALT = 0x00, //end
    ERR = 0xFF
}

public record Instruction(
    OpCode Code,
    object[]? Params
){}