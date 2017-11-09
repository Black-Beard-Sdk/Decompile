using System;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineI8Instruction
    /// </summary>
    public class InlineI8Instruction : ILInstruction
    {

        internal InlineI8Instruction(int offset, OpCode opCode, long value) : base(offset, opCode)
        {
            this.m_int64 = value;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(ILInstructionVisitor visitor)
        {
            visitor.VisitInlineI8Instruction(this);
        }

        /// <summary>
        /// Gets the int64.
        /// </summary>
        /// <value>
        /// The int64.
        /// </value>
        public long Int64 { get { return this.m_int64; } }

        private long m_int64;

    }
}
