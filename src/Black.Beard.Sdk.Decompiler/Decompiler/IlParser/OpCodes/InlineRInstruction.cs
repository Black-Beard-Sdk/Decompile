using System;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineRInstruction
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{OpCode} {Double}")]
    public class InlineRInstruction : ILInstruction
    {

        internal InlineRInstruction(int offset, OpCode opCode, double value) : base(offset, opCode)
        {
            this._value = value;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(AbstractILInstructionVisitor visitor)
        {
            visitor.VisitInlineRInstruction(this);
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <value>
        /// The double.
        /// </value>
        public double Double { get { return this._value; } }


        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is InlineRInstruction i)
                if (i.OpCode == this.OpCode)
                    return i._value == this._value;
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
            return this.OpCode.GetHashCode() ^ (int)this._value;
        }

        private double _value;

    }
}
