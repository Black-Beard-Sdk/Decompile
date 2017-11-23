using System;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineI8Instruction
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{OpCode} {Int64}")]
    public class InlineI8Instruction : ILInstruction
    {

        internal InlineI8Instruction(int offset, OpCode opCode, long value) : base(offset, opCode)
        {
            this._int64 = value;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(AbstractILInstructionVisitor visitor)
        {
            visitor.VisitInlineI8Instruction(this);
        }

        /// <summary>
        /// Gets the int64.
        /// </summary>
        /// <value>
        /// The int64.
        /// </value>
        public long Int64 { get { return this._int64; } }


        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is InlineI8Instruction i)
                if (i.OpCode == this.OpCode)
                    return i._int64 == this._int64;
            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.OpCode.GetHashCode() ^ (int)this._int64;
        }

        private long _int64;

    }
}
