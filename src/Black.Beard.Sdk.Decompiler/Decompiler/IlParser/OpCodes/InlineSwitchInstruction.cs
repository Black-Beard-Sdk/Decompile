using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{    

    /// <summary>
    /// InlineSwitchInstruction
    /// </summary>
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
            this.m_deltas = deltas;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(ILInstructionVisitor visitor)
        {
            visitor.VisitInlineSwitchInstruction(this);
        }

        /// <summary>
        /// Gets the deltas.
        /// </summary>
        /// <value>
        /// The deltas.
        /// </value>
        public int[] Deltas        {            get            {                return (int[]) this.m_deltas.Clone();            }        }

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
                if (this.m_targetOffsets == null)
                {
                    int length = this.m_deltas.Length;
                    int num2 = 5 + (4 * length);
                    this.m_targetOffsets = new int[length];
                    for (int i = 0; i < length; i++)
                        this.m_targetOffsets[i] = (base.m_offset + this.m_deltas[i]) + num2;
                }
                return this.m_targetOffsets;
            }
        }

        private int[] m_deltas;
        private int[] m_targetOffsets;

    }
}
