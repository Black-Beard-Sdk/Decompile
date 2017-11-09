using System;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineRInstruction
    /// </summary>
    public class InlineRInstruction : ILInstruction
    {

        internal InlineRInstruction(int offset, OpCode opCode, double value) : base(offset, opCode)
        {
            this.m_value = value;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(ILInstructionVisitor visitor)
        {
            visitor.VisitInlineRInstruction(this);
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <value>
        /// The double.
        /// </value>
        public double Double        {            get            {                return this.m_value;            }        }

        private double m_value;

    }
}
