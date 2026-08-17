```json
DefineStmt {   
    TypeStatement = DEFINE, 
    TypeToCreate = NODE, 
    Attributes = Attribution { 
        Key = name, 
        Value = node 
    },
    Attribution { 
        Key = difficulty, 
        Value = 5.5 
    },
    Attribution { 
        Key = hours, 
        Value = 20 
    },
    Attribution { 
        Key = req_skills, 
        Value = Skill, Skill, Skill 
    },
    Attribution { 
        Key = gain_skills, 
        Value = Skill, Skill 
    }
}
```

```
=== HEADER ===
0x4B 0x53              ← magic number ("KS")
0x01                   ← version 1
0x00 0x04              ← constant count: 4

=== CONSTANTS TABLE ===

Entry 0: "name"
0x01                   ← type: string
0x00 0x04              ← string length: 4 bytes
0x6E 0x61 0x6D 0x65    ← "name" in UTF-8

Entry 1: "node"
0x01                   ← type: string
0x00 0x04              ← string length: 4 bytes
0x6E 0x6F 0x64 0x65    ← "node" in UTF-8

Entry 2: "difficulty"
0x01                   ← type: string
0x00 0x0A              ← string length: 10 bytes
0x64 0x69 0x66 0x66 0x69 0x63 0x75 0x6C 0x74 0x79
                       ← "difficulty" in UTF-8

Entry 3: 5
0x02                   ← type: int
0x00 0x00 0x00 0x05    ← value: 5 (4 bytes, big-endian)

=== BYTECODE ===

PUSH_CONST 0           ← push "name"
0x10 0x00 0x00

PUSH_CONST 1           ← push "node"
0x10 0x00 0x01

PUSH_CONST 2           ← push "difficulty"
0x10 0x00 0x02

PUSH_CONST 3           ← push 5
0x10 0x00 0x03

DEFINE_NODE 2          ← pop 2 key/value pairs, build node
0x20 0x02

HALT
0xFF
```

```json
foreach statement in program
    foreach attribute in statement.Attributes
        if attribute.Value is array
            foreach element in array
                emit PUSH_CONST for element
            emit MAKE_ARRAY
        else
            emit PUSH_CONST for value
        emit PUSH_CONST for key
    emit DEFINE_NODE / DEFINE_OBJ
```