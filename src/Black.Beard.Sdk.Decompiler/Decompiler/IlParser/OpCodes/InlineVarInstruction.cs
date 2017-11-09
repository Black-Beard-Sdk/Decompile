namespace Bb.Sdk.Decompiler.IlParser
{
    using System;
    using System.Reflection.Emit;

    /// <summary>
    /// InlineVarInstruction
    /// </summary>
    public class InlineVarInstruction : ILInstruction
    {
        private ushort m_ordinal;

        internal InlineVarInstruction(int offset, OpCode opCode, ushort ordinal) : base(offset, opCode)
        {
            this.m_ordinal = ordinal;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(ILInstructionVisitor visitor)
        {
            visitor.VisitInlineVarInstruction(this);
        }

        /// <summary>
        /// Gets the ordinal.
        /// </summary>
        /// <value>
        /// The ordinal.
        /// </value>
        public ushort Ordinal { get { return this.m_ordinal; } }

    }
}
