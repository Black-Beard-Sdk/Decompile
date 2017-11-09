using System;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineIInstruction
    /// </summary>
    public class InlineIInstruction : ILInstruction
    {

        internal InlineIInstruction(int offset, OpCode opCode, int value) : base(offset, opCode)
        {
            this.m_int32 = value;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(ILInstructionVisitor visitor)
        {
            visitor.VisitInlineIInstruction(this);
        }

        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <value>
        /// The int32.
        /// </value>
        public int Int32 { get { return this.m_int32; } }

        private int m_int32;

    }
}
