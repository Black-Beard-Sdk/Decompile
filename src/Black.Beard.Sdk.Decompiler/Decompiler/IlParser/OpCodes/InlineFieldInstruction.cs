using System.Reflection;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineFieldInstruction
    /// </summary>
    public class InlineFieldInstruction : ILInstruction
    {

        internal InlineFieldInstruction(ITokenResolver resolver, int offset, OpCode opCode, int token) : base(offset, opCode)
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
            visitor.VisitInlineFieldInstruction(this);
        }

        /// <summary>
        /// Gets the field.
        /// </summary>
        /// <value>
        /// The field.
        /// </value>
        public FieldInfo Field
        {
            get
            {
                if (this.m_field == null)
                    this.m_field = this.m_resolver.AsField(this.m_token);
                return this.m_field;
            }
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public int Token        {            get            {                return this.m_token;            }        }

        private FieldInfo m_field;
        private ITokenResolver m_resolver;
        private int m_token;

    }
}
