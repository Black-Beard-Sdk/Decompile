using System;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineBrTargetInstruction
    /// </summary>
    public class InlineBrTargetInstruction : ILInstruction
    {

        internal InlineBrTargetInstruction(int offset, OpCode opCode, int delta) : base(offset, opCode)
        {
            this._delta = delta;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(AbstractILInstructionVisitor visitor)
        {
            visitor.VisitInlineBrTargetInstruction(this);
        }

        /// <summary>
        /// Gets the delta.
        /// </summary>
        /// <value>
        /// The delta.
        /// </value>

        /// <summary>
        /// Gets the target offset.
        /// </summary>
        /// <value>
        /// The target offset.
        /// </value>
        public int TargetOffset { get { return (((base._offset + this._delta) + 1) + 4); } }

        public ILInstruction TargetInstruction { get; internal set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is InlineBrTargetInstruction i)
                if (i.OpCode == this.OpCode)
                    return i._delta == this._delta;
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
            return this.OpCode.GetHashCode() ^ (int)this._delta;
        }

        private int _delta;

    }
}
