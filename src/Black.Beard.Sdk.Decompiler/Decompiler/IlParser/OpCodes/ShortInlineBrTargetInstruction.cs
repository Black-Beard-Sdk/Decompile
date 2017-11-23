using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ShortInlineBrTargetInstruction
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{OpCode} {Delta}")]
    public class ShortInlineBrTargetInstruction : ILInstruction
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortInlineBrTargetInstruction"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="opCode">The op code.</param>
        /// <param name="delta">The delta.</param>
        internal ShortInlineBrTargetInstruction(int offset, OpCode opCode, sbyte delta) : base(offset, opCode)
        {
            this._delta = delta;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(AbstractILInstructionVisitor visitor)
        {
            visitor.VisitShortInlineBrTargetInstruction(this);
        }

        /// <summary>
        /// Gets the delta.
        /// </summary>
        /// <value>
        /// The delta.
        /// </value>
        public sbyte Delta { get { return this._delta; } }

        /// <summary>
        /// Gets the target offset.
        /// </summary>
        /// <value>
        /// The target offset.
        /// </value>
        public int TargetOffset { get { return (((base._offset + this._delta) + 1) + 1); } }

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
            if (obj is ShortInlineBrTargetInstruction i)
                if (i.OpCode == this.OpCode)
                    return i._delta == this._delta;
            return false;
        }

        public override int GetHashCode()
        {
            return this.OpCode.GetHashCode() ^ this._delta;
        }

        private sbyte _delta;

    }
}
