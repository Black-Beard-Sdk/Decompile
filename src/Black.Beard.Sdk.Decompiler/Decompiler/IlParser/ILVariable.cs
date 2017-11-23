using System;
using System.Collections.Generic;

namespace Bb.Sdk.Decompiler.IlParser
{

    public class ILVariable
    {

        VariableKind kind;

        public VariableKind Kind
        {
            get
            {
                return kind;
            }
            internal set
            {
                if (kind == VariableKind.Parameter)
                    throw new InvalidOperationException("Kind=Parameter cannot be changed!");
                kind = value;
            }
        }

        public readonly StackType StackType;

        Type type;
        public Type Type
        {
            get
            {
                return type;
            }
            internal set
            {
                if (value.GetStackType() != StackType)
                    throw new ArgumentException($"Expected stack-type: {StackType} may not be changed. Found: {value.GetStackType()}");
                type = value;
            }
        }

        /// <summary>
        /// The index of the local variable or parameter (depending on Kind)
        /// </summary>

        public string Name { get; set; }

        public bool HasGeneratedName { get; set; }

        ///// <summary>
        ///// Gets the function in which this variable is declared.
        ///// </summary>
        ///// <remarks>
        ///// This property is set automatically when the variable is added to the <c>ILFunction.Variables</c> collection.
        ///// </remarks>
        //public ILFunction Function { get; internal set; }

        ///// <summary>
        ///// Gets the block container in which this variable is captured.
        ///// For captured variables declared inside the loop, the capture scope is the BlockContainer of the loop.
        ///// For captured variables declared outside of the loop, the capture scope is the BlockContainer of the parent. 
        ///// </summary>
        ///// <remarks>
        ///// This property returns null for variables that are not captured.
        ///// </remarks>
        //public BlockContainer CaptureScope { get; internal set; }

        /// <summary>
        /// Gets the index of this variable within the <c>Function.Variables</c> collection.
        /// </summary>
        /// <remarks>
        /// This property is set automatically when the variable is added to the <c>VariableScope.Variables</c> collection.
        /// It may change if an item with a lower index is removed from the collection.
        /// </remarks>
        public int IndexInFunction { get; internal set; }

        ///// <summary>
        ///// Number of ldloc instructions referencing this variable.
        ///// </summary>
        ///// <remarks>
        ///// This variable is automatically updated when adding/removing ldloc instructions from the ILAst.
        ///// </remarks>
        //public int LoadCount => LoadInstructions.Count;

        //readonly List<LdLoc> loadInstructions = new List<LdLoc>();

        ///// <summary>
        ///// List of ldloc instructions referencing this variable.
        ///// </summary>
        ///// <remarks>
        ///// This list is automatically updated when adding/removing ldloc instructions from the ILAst.
        ///// </remarks>
        //public IReadOnlyList<LdLoc> LoadInstructions => loadInstructions;

        ///// <summary>
        ///// Number of store instructions referencing this variable,
        ///// plus 1 if HasInitialValue.
        ///// 
        ///// Stores are:
        ///// <list type="item">
        ///// <item>stloc</item>
        ///// <item>TryCatchHandler (assigning the exception variable)</item>
        ///// <item>PinnedRegion (assigning the pointer variable)</item>
        ///// <item>initial values (<see cref="HasInitialValue"/>)</item>
        ///// </list>
        ///// </summary>
        ///// <remarks>
        ///// This variable is automatically updated when adding/removing stores instructions from the ILAst.
        ///// </remarks>
        //public int StoreCount => (hasInitialValue ? 1 : 0) + StoreInstructions.Count;

        //readonly List<IStoreInstruction> storeInstructions = new List<IStoreInstruction>();

        ///// <summary>
        ///// List of store instructions referencing this variable.
        ///// 
        ///// Stores are:
        ///// <list type="item">
        ///// <item>stloc</item>
        ///// <item>TryCatchHandler (assigning the exception variable)</item>
        ///// <item>PinnedRegion (assigning the pointer variable)</item>
        ///// <item>initial values (<see cref="HasInitialValue"/>) -- however, there is no instruction for
        /////       the initial value, so it is not contained in the store list.</item>
        ///// </list>
        ///// </summary>
        ///// <remarks>
        ///// This list is automatically updated when adding/removing stores instructions from the ILAst.
        ///// </remarks>
        //public IReadOnlyList<IStoreInstruction> StoreInstructions => storeInstructions;

        ///// <summary>
        ///// Number of ldloca instructions referencing this variable.
        ///// </summary>
        ///// <remarks>
        ///// This variable is automatically updated when adding/removing ldloca instructions from the ILAst.
        ///// </remarks>
        //public int AddressCount => AddressInstructions.Count;

        //readonly List<LdLoca> addressInstructions = new List<LdLoca>();

        ///// <summary>
        ///// List of ldloca instructions referencing this variable.
        ///// </summary>
        ///// <remarks>
        ///// This list is automatically updated when adding/removing ldloca instructions from the ILAst.
        ///// </remarks>
        //public IReadOnlyList<LdLoca> AddressInstructions => addressInstructions;

        //internal void AddLoadInstruction(LdLoc inst) => inst.IndexInLoadInstructionList = AddInstruction(loadInstructions, inst);
        //internal void AddStoreInstruction(IStoreInstruction inst) => inst.IndexInStoreInstructionList = AddInstruction(storeInstructions, inst);
        //internal void AddAddressInstruction(LdLoca inst) => inst.IndexInAddressInstructionList = AddInstruction(addressInstructions, inst);

        //internal void RemoveLoadInstruction(LdLoc inst) => RemoveInstruction(loadInstructions, inst.IndexInLoadInstructionList, inst);
        //internal void RemoveStoreInstruction(IStoreInstruction inst) => RemoveInstruction(storeInstructions, inst.IndexInStoreInstructionList, inst);
        //internal void RemoveAddressInstruction(LdLoca inst) => RemoveInstruction(addressInstructions, inst.IndexInAddressInstructionList, inst);

        //int AddInstruction<T>(List<T> list, T inst) where T : class, IInstructionWithVariableOperand
        //{
        //    list.Add(inst);
        //    return list.Count - 1;
        //}

        //void RemoveInstruction<T>(List<T> list, int index, T inst) where T : class, IInstructionWithVariableOperand
        //{
        //    Debug.Assert(list[index] == inst);
        //    int indexToMove = list.Count - 1;
        //    list[index] = list[indexToMove];
        //    list[index].IndexInVariableInstructionMapping = index;
        //    list.RemoveAt(indexToMove);
        //}

        bool hasInitialValue;

        /// <summary>
        /// Gets/Sets whether the variable has an initial value.
        /// This is always <c>true</c> for parameters (incl. <c>this</c>).
        /// 
        /// Normal variables have an initial value if the function uses ".locals init"
        /// and that initialization is not a dead store.
        /// </summary>
        /// <remarks>
        /// An initial value is counted as a store (adds 1 to StoreCount)
        /// </remarks>
        public bool HasInitialValue
        {
            get { return hasInitialValue; }
            set
            {
                if (Kind == VariableKind.Parameter && !value)
                    throw new InvalidOperationException("Cannot remove HasInitialValue from parameters");
                hasInitialValue = value;
            }
        }

        //public bool IsSingleDefinition
        //{
        //    get
        //    {
        //        return StoreCount == 1 && AddressCount == 0;
        //    }
        //}

        ///// <summary>
        ///// The field which was converted to a local variable.
        ///// Set when the variable is from a 'yield return' or 'async' state machine.
        ///// </summary>
        //public IField StateMachineField;

        public ILVariable(VariableKind kind, Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            this.Kind = kind;
            this.type = type;
            this.StackType = type.GetStackType();
            if (kind == VariableKind.Parameter)
                this.HasInitialValue = true;
        }

        public ILVariable(VariableKind kind, Type type, StackType stackType)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            this.Kind = kind;
            this.type = type;
            this.StackType = stackType;
            if (kind == VariableKind.Parameter)
                this.HasInitialValue = true;
        }

        public override string ToString()
        {
            return Name;
        }

        //internal void WriteDefinitionTo(ITextOutput output)
        //{
        //    switch (Kind)
        //    {
        //        case VariableKind.Local:
        //            output.Write("local ");
        //            break;
        //        case VariableKind.PinnedLocal:
        //            output.Write("pinned local ");
        //            break;
        //        case VariableKind.Parameter:
        //            output.Write("param ");
        //            break;
        //        case VariableKind.Exception:
        //            output.Write("exception ");
        //            break;
        //        case VariableKind.StackSlot:
        //            output.Write("stack ");
        //            break;
        //        case VariableKind.InitializerTarget:
        //            output.Write("initializer ");
        //            break;
        //        case VariableKind.ForeachLocal:
        //            output.Write("foreach ");
        //            break;
        //        case VariableKind.UsingLocal:
        //            output.Write("using ");
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //    output.WriteDefinition(this.Name, this, isLocal: true);
        //    output.Write(" : ");
        //    Type.WriteTo(output);
        //    output.Write('(');
        //    if (Kind == VariableKind.Parameter || Kind == VariableKind.Local || Kind == VariableKind.PinnedLocal)
        //        output.Write("Index={0}, ", Index);
        //    output.Write("LoadCount={0}, AddressCount={1}, StoreCount={2})", LoadCount, AddressCount, StoreCount);
        //    if (hasInitialValue && Kind != VariableKind.Parameter)
        //        output.Write(" init");
        //    if (CaptureScope != null)
        //        output.Write(" captured in " + CaptureScope.EntryPoint.Label);
        //    if (StateMachineField != null)
        //        output.Write(" from state-machine");
        //}

        //internal void WriteTo(ITextOutput output)
        //{
        //    output.WriteReference(this.Name, this, isLocal: true);
        //}

        ///// <summary>
        ///// Gets whether this variable occurs within the specified instruction.
        ///// </summary>
        //internal bool IsUsedWithin(ILInstruction inst)
        //{
        //    if (inst is IInstructionWithVariableOperand iwvo && iwvo.Variable == this)
        //        return true;
        //    foreach (var child in inst.Children)
        //        if (IsUsedWithin(child))
        //            return true;
        //    return false;
        //}

    }


    public enum VariableKind
    {
        /// <summary>
        /// A local variable.
        /// </summary>
        Local,
        /// <summary>
        /// A pinned local variable
        /// </summary>
        PinnedLocal,
        /// <summary>
        /// A local variable used as using-resource variable.
        /// </summary>
        UsingLocal,
        /// <summary>
        /// A local variable used as foreach variable.
        /// </summary>
        ForeachLocal,
        /// <summary>
        /// A local variable used inside an array, collection or
        /// object initializer block to denote the object being initialized.
        /// </summary>
        InitializerTarget,
        /// <summary>
        /// A parameter.
        /// </summary>
        Parameter,
        /// <summary>
        /// Variable created for exception handler
        /// </summary>
        Exception,
        /// <summary>
        /// Variable created from stack slot.
        /// </summary>
        StackSlot
    }

    /// <summary>
    /// A type for the purpose of stack analysis.
    /// </summary>
    public enum StackType : byte
    {
        // Note: the numeric of these enum members is relevant for ILReader.MergeStacks:
        // when two branches meet where a stack slot has different types, the type after
        // the branch is the one with the higher numeric value.

        /// <summary>
        /// The stack type is unknown; for example a call returning an unknown type
        /// because an assembly reference isn't loaded.
        /// Can also occur with invalid IL.
        /// </summary>
        Unknown,
        /// <summary>32-bit integer</summary>
        /// <remarks>
        /// Used for C# <c>int</c>, <c>uint</c>,
        /// C# small integer types <c>byte</c>, <c>sbyte</c>, <c>short</c>, <c>ushort</c>,
        /// <c>bool</c> and <c>char</c>,
        /// and any enums with one of the above as underlying type.
        /// </remarks>
        I4,
        /// <summary>native-size integer, or unmanaged pointer</summary>
        /// <remarks>
        /// Used for C# <c>IntPtr</c>, <c>UIntPtr</c> and any native pointer types (<c>void*</c> etc.)
        /// Also used for IL function pointer types.
        /// </remarks>
        I,
        /// <summary>64-bit integer</summary>
        /// <remarks>
        /// Used for C# <c>long</c>, <c>ulong</c>,
        /// and any enums with one of the above as underlying type.
        /// </remarks>
        I8,
        /// <summary>Floating point number</summary>
        /// <remarks>
        /// Used for C# <c>float</c> and <c>double</c>.
        /// </remarks>
        F,
        /// <summary>Another stack type. Includes objects, value types, ...</summary>
        O,
        /// <summary>A managed pointer</summary>
        Ref,
        /// <summary>Represents the lack of a stack slot</summary>
        Void
    }

}