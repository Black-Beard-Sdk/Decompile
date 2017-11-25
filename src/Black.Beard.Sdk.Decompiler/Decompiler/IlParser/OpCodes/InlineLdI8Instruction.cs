
using System.Diagnostics;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{
	public partial class InlineLdI8Instruction : InlineNoneInstruction
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineLdI8Instruction"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="opCode">The op code.</param>
        public InlineLdI8Instruction(int offset, OpCode opCode, long value) 
            : base (offset, opCode) 
        {
            this.Value = value;
        }

        public long Value { get; }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(AbstractILInstructionVisitor visitor)
        {
            visitor.VisitInlineLdI8Instruction(this);
        }
        
    }
}
