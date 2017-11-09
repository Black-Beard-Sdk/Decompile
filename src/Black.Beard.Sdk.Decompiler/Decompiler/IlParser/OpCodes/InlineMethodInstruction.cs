using System.Reflection;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineMethodInstruction
    /// </summary>
    public class InlineMethodInstruction : ILInstruction
    {

        internal InlineMethodInstruction(int offset, OpCode opCode, int token, ITokenResolver resolver) : base(offset, opCode)
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
            visitor.VisitInlineMethodInstruction(this);
        }

        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public MethodBase Method
        {
            get
            {
                if (this.m_method == null)
                    this.m_method = this.m_resolver.AsMethod(this.m_token);
                return this.m_method;
            }
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public int Token { get { return this.m_token; } }

        private MethodBase m_method;
        private ITokenResolver m_resolver;
        private int m_token;

    }
}
