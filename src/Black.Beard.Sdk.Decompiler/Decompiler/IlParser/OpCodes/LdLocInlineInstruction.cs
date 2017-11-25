using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Bb.Sdk.Decompiler.IlParser
{

    public class LdLocInlineInstruction : ILInstruction
    {


        public LdLocInlineInstruction(ILVariable variable, int offset, OpCode opCode, UInt16 index) : base(offset, opCode)
        {

            this.Variable = variable;
            this.Index = index;
        }

        public ILVariable Variable { get; }

        public UInt16 Index { get; }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(AbstractILInstructionVisitor visitor)
        {
            visitor.VisitLdLoc(this);
        }

    }
}
