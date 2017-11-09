using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ShortInlineVarInstruction
    /// </summary>
    public class ShortInlineVarInstruction : ILInstruction
    {

        internal ShortInlineVarInstruction(int offset, OpCode opCode, byte ordinal) : base(offset, opCode)
        {
            this.m_ordinal = ordinal;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(ILInstructionVisitor visitor)
        {
            visitor.VisitShortInlineVarInstruction(this);
        }

        /// <summary>
        /// Gets the ordinal.
        /// </summary>
        /// <value>
        /// The ordinal.
        /// </value>
        public byte Ordinal { get { return this.m_ordinal; } }

        private byte m_ordinal;

    }
}
