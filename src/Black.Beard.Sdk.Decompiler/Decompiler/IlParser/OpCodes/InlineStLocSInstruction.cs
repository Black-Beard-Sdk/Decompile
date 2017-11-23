﻿using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{
	public partial class InlineStLocSInstruction : ShortInlineVarInstruction
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineStLocInstruction"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="opCode">The op code.</param>
        public InlineStLocSInstruction(int offset, OpCode opCode, byte ordinal) 
            : base (offset, opCode, ordinal) 
        {

        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(AbstractILInstructionVisitor visitor)
        {
            visitor.VisitInlineStLocSInstruction(this);
        }
        
    }
}