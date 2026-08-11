# vmbl
Virtual Machine Based Language... I'm terrible with names

DSL for Kalopsia interaction.

## Brief example:

```plaintext
DEFINE NODE (name="node"; difficulty=5, hours=20; req_skills=["", "", ""]; gain_skills=["", ""]);
DEFINE DEV (skills=["", "", ""]);

QUERY NODE name="node"; -queries every single node with this name out there
```

## Build executable:

```bash
dotnet publish -c kalopsia -r win-x64 --self-contained false # windows

---

dotnet publish -c kalopsia -r linux-x64 --self-contained true -p:PublishSingleFile=true # linux
```