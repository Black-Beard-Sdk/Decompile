using System;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ILInstruction
    /// </summary>
    public abstract class ILInstruction
    {

        internal ILInstruction(int offset, System.Reflection.Emit.OpCode opCode)
        {
            this.m_offset = offset;
            this.m_opCode = opCode;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public abstract void Accept(ILInstructionVisitor visitor);

        /// <summary>
        /// Gets the offset.
        /// </summary>
        /// <value>
        /// The offset.
        /// </value>
        public int Offset { get { return this.m_offset; } }

        /// <summary>
        /// Gets the op code.
        /// </summary>
        /// <value>
        /// The op code.
        /// </value>
        public System.Reflection.Emit.OpCode OpCode { get { return this.m_opCode; } }

        /// <summary>
        /// The m_offset
        /// </summary>
        protected int m_offset;

        /// <summary>
        /// The m_op code
        /// </summary>
        protected System.Reflection.Emit.OpCode m_opCode;

    }
}
