using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineSwitchInstruction
    /// </summary>
    //[System.Diagnostics.DebuggerDisplay("{}")]
    public class InlineSwitchInstruction : ILInstruction
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineSwitchInstruction"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="opCode">The op code.</param>
        /// <param name="deltas">The deltas.</param>
        internal InlineSwitchInstruction(int offset, OpCode opCode, int[] deltas) : base(offset, opCode)
        {
            this._deltas = deltas;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(AbstractILInstructionVisitor visitor)
        {
            visitor.VisitInlineSwitchInstruction(this);
        }

        /// <summary>
        /// Gets the deltas.
        /// </summary>
        /// <value>
        /// The deltas.
        /// </value>
        public int[] Deltas { get { return (int[])this._deltas.Clone(); } }

        /// <summary>
        /// Gets the target offsets.
        /// </summary>
        /// <value>
        /// The target offsets.
        /// </value>
        public int[] TargetOffsets
        {
            get
            {
                if (this._targetOffsets == null)
                {
                    int length = this._deltas.Length;
                    int num2 = 5 + (4 * length);
                    this._targetOffsets = new int[length];
                    for (int i = 0; i < length; i++)
                        this._targetOffsets[i] = (base._offset + this._deltas[i]) + num2;
                }
                return this._targetOffsets;
            }
        }

        public override int GetSize()
        {

            int size = 0;
            var index = ((ushort)this.OpCode.Value);
            if (index < 0x100)
                size = 1;
            else
                size = 2;

            return size + (1 + ((int[])Deltas).Length) * 4;

        }

        ///// <summary>
        ///// Returns a hash code for this instance.
        ///// </summary>
        ///// <returns>
        ///// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        ///// </returns>
        //public override int GetHashCode()
        //{
        //    return this.OpCode.GetHashCode() ^ this._ordinal;
        //}

        ///// <summary>
        ///// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        ///// </summary>
        ///// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        ///// <returns>
        /////   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        ///// </returns>
        //public override bool Equals(object obj)
        //{
        //    if (obj is InlineSwitchInstruction i)
        //        if (i.OpCode == this.OpCode)
        //            return i.String == this.String;
        //    return false;
        //}


        private int[] _deltas;
        private int[] _targetOffsets;

    }
}
