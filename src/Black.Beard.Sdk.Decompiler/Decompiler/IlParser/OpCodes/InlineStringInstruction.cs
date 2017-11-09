using System;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineStringInstruction
    /// </summary>
    public class InlineStringInstruction : ILInstruction
    {

        internal InlineStringInstruction(int offset, OpCode opCode, int token, ITokenResolver resolver) : base(offset, opCode)
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
            visitor.VisitInlineStringInstruction(this);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <value>
        /// The string.
        /// </value>
        public string String
        {
            get
            {
                if (this.m_string == null)
                    this.m_string = this.m_resolver.AsString(this.Token);
                return this.m_string;
            }
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public int Token        {            get            {                return this.m_token;            }        }

        private ITokenResolver m_resolver;
        private string m_string;
        private int m_token;

    }
}
