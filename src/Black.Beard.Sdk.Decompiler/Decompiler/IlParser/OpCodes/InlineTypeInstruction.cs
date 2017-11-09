using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineTypeInstruction
    /// </summary>
    public class InlineTypeInstruction : ILInstruction
    {

        internal InlineTypeInstruction(int offset, OpCode opCode, int token, ITokenResolver resolver) : base(offset, opCode)
        {
            this.m_resolver = resolver;
            this.m_token = token;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(ILInstructionVisitor visitor)
        {
            visitor.VisitInlineTypeInstruction(this);
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public int Token { get { return this.m_token; } }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public System.Type Type
        {
            get
            {
                if (this.m_type == null)
                    this.m_type = this.m_resolver.AsType(this.m_token);
                return this.m_type;
            }
        }

        private ITokenResolver m_resolver;
        private int m_token;
        private System.Type m_type;

    }
}
