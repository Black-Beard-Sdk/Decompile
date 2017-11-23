using System;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineIInstruction
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{OpCode} {Int32}")]
    public class InlineIInstruction : ILInstruction
    {

        internal InlineIInstruction(int offset, OpCode opCode, int value) : base(offset, opCode)
        {
            this._int32 = value;
        }


        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(AbstractILInstructionVisitor visitor)
        {
            visitor.VisitInlineIInstruction(this);
        }

        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <value>
        /// The int32.
        /// </value>
        public int Int32 { get { return this._int32; } }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is InlineIInstruction i)
                if (i.OpCode == this.OpCode)
                    return i._int32 == this._int32;
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
            return this.OpCode.GetHashCode() ^ this._int32;
        }

        private int _int32;

    }
}
