using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{
	public partial class InlineConditionInstruction : InlineNoneInstruction
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineConditionInstruction"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="opCode">The op code.</param>
        public InlineConditionInstruction(int offset, OpCode opCode) 
            : base (offset, opCode) 
        {

        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(AbstractILInstructionVisitor visitor)
        {
            visitor.VisitInlineConditionInstruction(this);
        }
        
    }
}
