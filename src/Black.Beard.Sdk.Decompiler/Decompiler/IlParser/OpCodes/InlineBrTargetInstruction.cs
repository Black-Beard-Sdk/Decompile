using System;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineBrTargetInstruction
    /// </summary>
    public class InlineBrTargetInstruction : ILInstruction
    {

        internal InlineBrTargetInstruction(int offset, OpCode opCode, int delta) : base(offset, opCode)
        {
            this.m_delta = delta;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(ILInstructionVisitor visitor)
        {
            visitor.VisitInlineBrTargetInstruction(this);
        }

        /// <summary>
        /// Gets the delta.
        /// </summary>
        /// <value>
        /// The delta.
        /// </value>
        public int Delta { get { return this.m_delta; } }

        /// <summary>
        /// Gets the target offset.
        /// </summary>
        /// <value>
        /// The target offset.
        /// </value>
        public int TargetOffset { get { return (((base.m_offset + this.m_delta) + 1) + 4); } }

        private int m_delta;

    }
}
