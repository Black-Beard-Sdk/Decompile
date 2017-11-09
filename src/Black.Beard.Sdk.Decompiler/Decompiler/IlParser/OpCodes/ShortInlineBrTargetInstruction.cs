using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ShortInlineBrTargetInstruction
    /// </summary>
    public class ShortInlineBrTargetInstruction : ILInstruction
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortInlineBrTargetInstruction"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="opCode">The op code.</param>
        /// <param name="delta">The delta.</param>
        internal ShortInlineBrTargetInstruction(int offset, OpCode opCode, sbyte delta) : base(offset, opCode)
        {
            this.m_delta = delta;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(ILInstructionVisitor visitor)
        {
            visitor.VisitShortInlineBrTargetInstruction(this);
        }

        /// <summary>
        /// Gets the delta.
        /// </summary>
        /// <value>
        /// The delta.
        /// </value>
        public sbyte Delta { get { return this.m_delta; } }

        /// <summary>
        /// Gets the target offset.
        /// </summary>
        /// <value>
        /// The target offset.
        /// </value>
        public int TargetOffset { get { return (((base.m_offset + this.m_delta) + 1) + 1); } }

        private sbyte m_delta;

    }
}
