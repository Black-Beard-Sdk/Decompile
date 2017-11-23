namespace Bb.Sdk.Decompiler.IlParser
{
    using System;
    using System.Reflection.Emit;

    /// <summary>
    /// InlineVarInstruction
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{OpCode} var{Ordinal}")]
    public class InlineVarInstruction : ILInstruction
    {

        internal InlineVarInstruction(int offset, OpCode opCode, ushort ordinal) : base(offset, opCode)
        {
            this._ordinal = ordinal;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(AbstractILInstructionVisitor visitor)
        {
            visitor.VisitInlineVarInstruction(this);
        }

        /// <summary>
        /// Gets the ordinal.
        /// </summary>
        /// <value>
        /// The ordinal.
        /// </value>
        public ushort Ordinal { get { return this._ordinal; } }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is InlineVarInstruction i)
                if (i.OpCode == this.OpCode)
                    return i._ordinal == this._ordinal;
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
            return this.OpCode.GetHashCode() ^ this._ordinal;
        }

        private ushort _ordinal;

    }
}
