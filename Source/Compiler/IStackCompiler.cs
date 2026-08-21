namespace vmbl.Source.Compiler;

public interface IStackCompiler
{
    /// <summary>
    /// Mounts the constants table on a scoped list.
    /// </summary>
    /// 
    /// <param name="statement">The current statement of the AST</param>
    /// <seealso cref="IStackCompiler"/>
    abstract void MountConstantsTable(Statement statement);

    /// <summary>
    /// Emits a PUSH instruction, with the index of the instruction from the constants table.
    /// </summary>
    /// 
    /// <param name="index">The index from the constant inside the table</param>
    /// <seealso cref="IStackCompiler"/>
    abstract void EmitPush(int index);

    /// <summary>
    /// Emits a MK_ARRAY instruction, with the amount of PUSH the VM should pop from the stack.
    /// </summary>
    /// 
    /// <param name="amount">The amount of PUSH instructions to be popped</param>
    /// <seealso cref="IStackCompiler"/>
    abstract void EmitMkArray(int amount);

    /// <summary>
    /// Emits a DEFINE NODE or DEFINE OBJ, with the amount of PUSH the VM should pop from the stack.
    /// </summary>
    /// 
    /// <param name="amount">The amount of PUSH instructions to be popped</param>
    /// <param name="type">The type of the statement, used to make sure it's either a DEFINE NODE or a DEFINE OBJ</param>
    /// <seealso cref="IStackCompiler"/>
    abstract void EmitDefine(int amount, DefineStmt type);

    /// <summary>
    /// Emits a QUERY NODE, OBJ, PATH or NEXT instruction, with the amount of PUSH the VM should pop from the stack.
    /// <para/>
    /// Usually, for queries, the VM will either pop 1 or 0. That will change as updates happen.
    /// </summary>
    /// 
    /// <param name="amount">The amount of PUSH instructions to be popped</param>
    /// <param name="type">The type of the statement, used to differentiate QUERY NODE from OBJ, PATH and NEXT</param>
    /// <seealso cref="IStackCompiler"/>
    abstract void EmitQuery(int amount, QueryStmt type);

    /// <summary>
    /// Main method to emit the bytecode.
    /// </summary>
    /// 
    /// <param name="statement">The next statement from the AST</param>
    /// <seealso cref="IStackCompiler"/>
    abstract void Emit(Statement statement);

    /// <summary>
    /// Used to dispose of the stream, binary writer and all the data structures mounted throughout the code's runtime.
    /// </summary>
    /// <seealso cref="IStackCompiler"/>
    abstract void Dispose();

    /// <summary>
    /// Main method called to start the Compiling step.
    /// </summary>
    /// <seealso cref="StackCompiler"/>
    abstract void Compile();
}