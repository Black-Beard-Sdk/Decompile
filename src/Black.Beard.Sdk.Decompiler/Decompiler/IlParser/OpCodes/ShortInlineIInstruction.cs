using System;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ShortInlineIInstruction
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{OpCode} {Byte}")]
    public class ShortInlineIInstruction : ILInstruction
    {

        internal ShortInlineIInstruction(int offset, OpCode opCode, byte value) : base(offset, opCode)
        {
            
            this._int8 = value;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(AbstractILInstructionVisitor visitor)
        {
            visitor.VisitShortInlineIInstruction(this);
        }

        /// <summary>
        /// Gets the byte.
        /// </summary>
        /// <value>
        /// The byte.
        /// </value>
        public byte Byte { get { return this._int8; } }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is ShortInlineIInstruction i)
                if (i.OpCode == this.OpCode)
                    return i._int8 == this._int8;
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
            return this.OpCode.GetHashCode() ^ this._int8;
        }

        private byte _int8;

    }
}
