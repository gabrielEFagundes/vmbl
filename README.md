# vmbl

Virtual Machine Based Language... I'm terrible with names

Lightning fast DSL for Kalopsia interaction.

## Brief example:

Find them [here](/examples)

## Context

[Kalopsia](https://github.com/gabrielEFagundes/Kalopsia) is a decision-making algorithm written originally to organize my TODO projects.

This is a *DSL* (Design Specific Language) made specifically to interact with Kalopsia's graphs.

With VMBL, you can:
- Create graphs and objects (outside of the graph)
- Query graphs and objects

New features are constantly being developed, so expect a documentation soon.

## How it works

VMBL is compiled to bytecode, which is read by the VM embedded into Kalopsia to execute the instructions.

## How to use

Compile the source code to an executable:
```bash
dotnet publish -r win-x64 -c Release
# or...
dotnet publish -r linux-x64 -c Release
# or even...
dotnet publish -r osx-arm64 -c Release
```

Then, simply run vmbl with the path to your script on the terminal:
```bash
vmbl /path/to/script.vmbl
```

By default, your output folder (with the compiled file) will be on `/out/target.ksc`

You can change that by parsing a flag when compiling your vmbl script:
```
vmbl /path/to/script.vmbl -o /your/own/custom/path
```

To learn more about the available flags, see [the documentation](/docs)

## Developing

Before contributing and sending your PR, you must build the project yourself to test it out:
```bash
dotnet build -o ./dist --no-self-contained -r win-x64
# or...
dotnet build -o ./dist --no-self-contained -r linux-x64
# or even...
dotnet build -o ./dist --no-self-contained -r osx-arm64
```

This will build the project, without DLLs, inside a `dist` folder, on your working directory.