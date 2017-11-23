using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ILReader
    /// </summary>
    public sealed partial class ILInstructionReader : IEnumerable<ILInstruction>, IEnumerable
    {

        #region Ctors

        /// <summary>
        /// Initializes the <see cref="ILInstructionReader"/> class.
        /// </summary>
        static ILInstructionReader()
        {

            foreach (FieldInfo info in typeof(System.Reflection.Emit.OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                OpCode code = (OpCode)info.GetValue(null);
                ushort index = (ushort)code.Value;

                if (index < 0x100)
                    _oneByteOpCodes[index] = code;

                else if ((index & 0xff00) == 0xfe00)
                    _twoByteOpCodes[index & 0xff] = code;

            }

            //foreach (OpCode item in _oneByteOpCodes)
            //    if (item.OperandType != OperandType.InlineNone)
            //        Debug.WriteLine("{0} -> {1}", item.ToString(), item.OperandType.ToString());

            //foreach (OpCode item in _twoByteOpCodes)
            //    if (item.OperandType != OperandType.InlineNone)
            //        Debug.WriteLine("{0} -> {1}", item.ToString(), item.OperandType.ToString());

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ILInstructionReader"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <exception cref="System.ArgumentNullException">method</exception>
        /// <exception cref="System.ArgumentException">method must be RuntimeMethodInfo or RuntimeConstructorInfo for this constructor.</exception>
        internal ILInstructionReader(MethodBase method)
        {

            if (method == null)
                throw new ArgumentNullException("method");

            this.Method = method;

            Type type = method.GetType();
            if ((type != _runtimeMethodInfoType) && (type != _runtimeConstructorInfoType))
                throw new ArgumentException("method must be RuntimeMethodInfo or RuntimeConstructorInfo for this constructor.");

            this._ilProvider = new MethodBaseILProvider(method);
            this._resolver = new ModuleScopeTokenResolver(method);
            this._byteArray = this._ilProvider.GetByteArray();
            this._position = 0;
            this.stackByOffset = new Dictionary<int, ImmutableStack<ILVariable>>();
            this.unionFind = new UnionFind<ILVariable>();
            this.stackMismatchPairs = new List<(ILVariable, ILVariable)>();
            this.currentStack = ImmutableStack<ILVariable>.Empty;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ILInstructionReader"/> class.
        /// </summary>
        /// <param name="ilProvider">The il provider.</param>
        /// <param name="tokenResolver">The token resolver.</param>
        /// <exception cref="System.ArgumentNullException">ilProvider</exception>
        internal ILInstructionReader(IILProvider ilProvider, ITokenResolver tokenResolver)
        {
            this._resolver = tokenResolver;
            this._ilProvider = ilProvider ?? throw new ArgumentNullException("ilProvider");
            this._byteArray = this._ilProvider.GetByteArray();
            this._position = 0;
            this.stackByOffset = new Dictionary<int, ImmutableStack<ILVariable>>();
            this.unionFind = new UnionFind<ILVariable>();
            this.stackMismatchPairs = new List<(ILVariable, ILVariable)>();
            this.currentStack = ImmutableStack<ILVariable>.Empty;

        }

        #endregion Ctors

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <exception cref="System.ArgumentNullException">argument 'visitor' can not be null</exception>
        public void Parse(AbstractILInstructionVisitor visitor)
        {

            visitor.SetMethod(this.Method);

            if (visitor == null)
                throw new ArgumentNullException("argument 'visitor' can not be null");

            foreach (ILInstruction instruction in _instructions)
                instruction.Accept(visitor);

        }

        /// <summary>
        /// Read il code
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <exception cref="System.ArgumentNullException">argument 'visitor' can not be null</exception>
        internal void ReadIl()
        {

            var body = this.Method.GetMethodBody();

            foreach (LocalVariableInfo variable in body.LocalVariables)
            {

                var v = new ILVariable(VariableKind.StackSlot, variable.LocalType, StackType.I);

            }

            ILInstruction[] instructions = this.ToArray();

            foreach (ExceptionHandlingClause eh in body.ExceptionHandlingClauses)
            {
                ImmutableStack<ILVariable> ehStack = null;
                if (eh.Flags == ExceptionHandlingClauseOptions.Clause)
                {
                    var v = new ILVariable(VariableKind.Exception, eh.CatchType)
                    {
                        Name = "E_" + eh.HandlerOffset,
                        HasGeneratedName = true
                    };
                    variableByExceptionHandler.Add(eh, v);
                    ehStack = ImmutableStack.Create(v);
                }
                else if (eh.Flags == ExceptionHandlingClauseOptions.Filter)
                {
                    var v = new ILVariable(VariableKind.Exception, typeof(object))
                    {
                        Name = "E_" + eh.HandlerOffset,
                        HasGeneratedName = true
                    };
                    variableByExceptionHandler.Add(eh, v);
                    ehStack = ImmutableStack.Create(v);
                }
                else
                {
                    ehStack = ImmutableStack<ILVariable>.Empty;
                }

                if (eh.FilterOffset != 0)
                {
                    GetInstruction(instructions, eh.FilterOffset).IsBranch = true;
                    StoreStackForOffset(eh.FilterOffset, ehStack);
                }
                if (eh.HandlerOffset != 0)
                {
                    GetInstruction(instructions, eh.FilterOffset).IsBranch = true;
                    StoreStackForOffset(eh.HandlerOffset, ehStack);
                }
            }

            ResolveBranches(instructions);


            this.nextInstructionIndex = 0;
            while (nextInstructionIndex < instructions.Length)
            {

                ILInstruction currentInstruction = instructions[nextInstructionIndex];
                //int start = currentInstruction.Offset;
                //StoreStackForOffset(start, currentStack);

                ILInstruction decodedInstruction = DecodeInstruction(currentInstruction);

                // if (decodedInstruction.ResultType == StackType.Unknown)
                //      Warn("Unknown result type (might be due to invalid IL)");

                // decodedInstruction.CheckInvariant(ILPhase.InILReader);

                // int end = currentInstruction.GetEndOffset();

                // decodedInstruction.ILRange = new Interval(start, end);

                // UnpackPush(decodedInstruction).ILRange = decodedInstruction.ILRange;
                this._stack_instructions.Push(decodedInstruction);

                //    if (decodedInstruction.HasDirectFlag(InstructionFlags.EndPointUnreachable))
                //        if (!stackByOffset.TryGetValue(end, out currentStack))
                //            currentStack = ImmutableStack<ILVariable>.Empty;
                nextInstructionIndex++;
            }


            if (System.Diagnostics.Debugger.IsAttached)
            {
                MemoryStream txt = new MemoryStream();
                var streamWriter = new StreamWriter(txt);
                ReadableILStringVisitor visitor2 = new ReadableILStringVisitor(new ReadableILStringToTextWriter(streamWriter));
                foreach (ILInstruction instruction in instructions)
                    instruction.Accept(visitor2);
                streamWriter.Flush();
                var ar = txt.ToArray();
                string text = System.Text.Encoding.Default.GetString(ar);
                Debug.WriteLine(text);
            }

        }

        void StoreStackForOffset(int offset, ImmutableStack<ILVariable> stack)
        {
            if (stackByOffset.TryGetValue(offset, out var existing))
            {
                var newStack = MergeStacks(existing, stack);
                if (newStack != existing)
                    stackByOffset[offset] = newStack;
            }
            else
                stackByOffset.Add(offset, stack);
        }

        static bool CheckStackCompatibleWithoutAdjustments(ImmutableStack<ILVariable> a, ImmutableStack<ILVariable> b)
        {
            while (!a.IsEmpty && !b.IsEmpty)
            {
                if (a.Peek().StackType != b.Peek().StackType)
                    return false;
                a = a.Pop();
                b = b.Pop();
            }
            return a.IsEmpty && b.IsEmpty;
        }

        private bool IsValidTypeStackTypeMerge(StackType stackType1, StackType stackType2)
        {
            if (stackType1 == StackType.I && stackType2 == StackType.I4)
                return true;
            if (stackType1 == StackType.I4 && stackType2 == StackType.I)
                return true;
            // allow merging unknown type with any other type
            return stackType1 == StackType.Unknown || stackType2 == StackType.Unknown;
        }

        ImmutableStack<ILVariable> MergeStacks(ImmutableStack<ILVariable> a, ImmutableStack<ILVariable> b)
        {
            if (CheckStackCompatibleWithoutAdjustments(a, b))
            {
                // We only need to union the input variables, but can 
                // otherwise re-use the existing stack.
                ImmutableStack<ILVariable> output = a;
                while (!a.IsEmpty && !b.IsEmpty)
                {
                    Debug.Assert(a.Peek().StackType == b.Peek().StackType);
                    unionFind.Merge(a.Peek(), b.Peek());
                    a = a.Pop();
                    b = b.Pop();
                }
                return output;
            }
            else if (a.Count() != b.Count())
            {
                // Let's not try to merge mismatched stacks.
                //Warn("Incompatible stack heights: " + a.Count() + " vs " + b.Count());
                return a;
            }
            else
            {
                // The more complex case where the stacks don't match exactly.
                var output = new List<ILVariable>();
                while (!a.IsEmpty && !b.IsEmpty)
                {
                    var varA = a.Peek();
                    var varB = b.Peek();
                    if (varA.StackType == varB.StackType)
                    {
                        unionFind.Merge(varA, varB);
                        output.Add(varA);
                    }
                    else
                    {
                        if (!IsValidTypeStackTypeMerge(varA.StackType, varB.StackType))
                        {
                            //Warn("Incompatible stack types: " + varA.StackType + " vs " + varB.StackType);
                        }
                        if (varA.StackType > varB.StackType)
                        {
                            output.Add(varA);
                            // every store to varB should also store to varA
                            stackMismatchPairs.Add((varB, varA));
                        }
                        else
                        {
                            output.Add(varB);
                            // every store to varA should also store to varB
                            stackMismatchPairs.Add((varA, varB));
                        }
                    }
                    a = a.Pop();
                    b = b.Pop();
                }
                // because we built up output by popping from the input stacks, we need to reverse it to get back the original order
                output.Reverse();
                return ImmutableStack.CreateRange(output);
            }
        }


        void ResolveBranches(ILInstruction[] _instructions)
        {
            var size = _instructions.Length;

            for (int i = 0; i < size; i++)
            {
                ILInstruction instruction = _instructions[i];

                switch (instruction)
                {
                    case ShortInlineBrTargetInstruction c1:
                        c1.TargetInstruction = GetInstruction(_instructions, c1.TargetOffset);
                        break;

                    case InlineBrTargetInstruction c1:
                        c1.TargetInstruction = GetInstruction(_instructions, c1.TargetOffset);
                        break;

                    case InlineSwitchInstruction c1:
                        var offsets = (int[])c1.Deltas;
                        var branches = new ILInstruction[offsets.Length];
                        for (int j = 0; j < offsets.Length; j++)
                            branches[j] = GetInstruction(_instructions, offsets[j]);
                        instruction.TargetInstructions = branches;
                        break;

                    default:
                        break;
                }

            }
        }

        ILInstruction GetInstruction(ILInstruction[] _instructions, int offset)
        {
            var size = _instructions.Length;
            if (offset < 0 || offset > _instructions[size - 1].Offset)
                return null;

            int min = 0;
            int max = size - 1;
            while (min <= max)
            {
                int mid = min + ((max - min) / 2);
                var instruction = _instructions[mid];
                var instruction_offset = instruction.Offset;

                if (offset == instruction_offset)
                    return instruction;

                if (offset < instruction_offset)
                    max = mid - 1;
                else
                    min = mid + 1;
            }

            return null;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<ILInstruction> GetEnumerator()
        {

            ILInstruction last = null;
            ILInstruction current;

            while (this._position < this._byteArray.Length)
            {

                current = this.Next();
                current.Previous = last;

                if (last != null)
                    last.Next = current;

                yield return current;

                last = current;

            }

            this._position = 0;

        }

        public MethodBase Method { get; }

        public override string ToString()
        {
            return base.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        #region private

        private byte[] _byteArray;
        private IILProvider _ilProvider;
        private int _position;
        private readonly Dictionary<int, ImmutableStack<ILVariable>> stackByOffset;
        private readonly UnionFind<ILVariable> unionFind;
        private readonly List<(ILVariable, ILVariable)> stackMismatchPairs;
        private ITokenResolver _resolver;
        private static OpCode[] _oneByteOpCodes = new OpCode[0x100];
        private static OpCode[] _twoByteOpCodes = new OpCode[0x100];
        private static Type _runtimeConstructorInfoType = Type.GetType("System.Reflection.RuntimeConstructorInfo");
        private static Type _runtimeMethodInfoType = Type.GetType("System.Reflection.RuntimeMethodInfo");
        Dictionary<ExceptionHandlingClause, ILVariable> variableByExceptionHandler;
        private Stack<ILInstruction> _stack_instructions;
        private List<ILInstruction> _instructions;
        private ImmutableStack<ILVariable> currentStack;
        private int nextInstructionIndex;

        #endregion private



    }
}
