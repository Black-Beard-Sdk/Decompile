using System;
using System.CodeDom;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ILInstruction
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{OpCode}")]
    public abstract class ILInstruction
    {

        internal ILInstruction(int offset, OpCode opCode)
        {
            this._offset = offset;
            this._opCode = opCode;
        }

        public virtual int GetSize()
        {

            int size = 0;
            var index = ((ushort)this.OpCode.Value);
            if (index < 0x100)
                size = 1;
            else
                size = 2;

            switch (this.OpCode.OperandType)
            {

                //case OperandType.InlineSwitch:
                //    return size + (1 + ((ILInstruction[])operand).Length) * 4;

                case OperandType.InlineI8:
                case OperandType.InlineR:
                    return size + 8;
                case OperandType.InlineBrTarget:
                case OperandType.InlineField:
                case OperandType.InlineI:
                case OperandType.InlineMethod:
                case OperandType.InlineString:
                case OperandType.InlineTok:
                case OperandType.InlineType:
                case OperandType.ShortInlineR:
                case OperandType.InlineSig:
                    return size + 4;
                //case OperandType.InlineArg:
                case OperandType.InlineVar:
                    return size + 2;
                case OperandType.ShortInlineBrTarget:
                case OperandType.ShortInlineI:
                //case OperandType.ShortInlineArg:
                case OperandType.ShortInlineVar:
                    return size + 1;



                case OperandType.InlineNone:
                default:
                    return size;
            }

        }


        OperandType OperandType { get { return this.OperandType; } }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public abstract void Accept(AbstractILInstructionVisitor visitor);

        /// <summary>
        /// Gets the offset.
        /// </summary>
        /// <value>
        /// The offset.
        /// </value>
        public int Offset { get { return this._offset; } }

        /// <summary>
        /// Gets the op code.
        /// </summary>
        /// <value>
        /// The op code.
        /// </value>
        public OpCode OpCode { get { return this._opCode; } }

        public CodeObject Expression { get; set; }
        public ILInstruction[] TargetInstructions { get; internal set; }

        public bool IsBranch { get; internal set; }

        public Interval ILRange { get; internal set; }

        public ILInstruction Previous { get; internal set; }
        public ILInstruction Next { get; internal set; }

        internal virtual int GetEndOffset()
        {
            return this._offset + GetSize();
        }

        //[Conditional("DEBUG")]
        //internal virtual void CheckInvariant(ILPhase phase)
        //{
        //    //foreach (var child in Children)
        //    //{
        //    //    Debug.Assert(child.Parent == this);
        //    //    Debug.Assert(this.GetChild(child.ChildIndex) == child);
        //    //    // if child flags are invalid, parent flags must be too
        //    //    // exception: nested ILFunctions (lambdas)
        //    //    Debug.Assert(this is ILFunction || child.flags != invalidFlags || this.flags == invalidFlags);
        //    //    Debug.Assert(child.IsConnected == this.IsConnected);
        //    //    child.CheckInvariant(phase);
        //    //}
        //    //Debug.Assert((this.DirectFlags & ~this.Flags) == 0, "All DirectFlags must also appear in this.Flags");
        //}

        ///// <summary>
        ///// Gets the length of the Opcode.Value. If the value id 0x100 the Opcode is 1 otherelse is 2
        ///// </summary>
        ///// <value>
        ///// The length of the op code.
        ///// </value>
        //public short OpCodeLength { get; }

        ///// <summary>
        ///// Gets the total length (OpCodeLength + ArgumentLength).
        ///// </summary>
        ///// <value>
        ///// The length.
        ///// </value>
        //public int Length { get; }


        /// <summary>
        /// The m_offset
        /// </summary>
        protected int _offset;

        /// <summary>
        /// The m_op code
        /// </summary>
        protected OpCode _opCode;


    }
}
