using System.Reflection;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineTokInstruction
    /// </summary>
    public class InlineTokInstruction : ILInstruction
    {

        internal InlineTokInstruction(int offset, OpCode opCode, int token, ITokenResolver resolver) : base(offset, opCode)
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
            visitor.VisitInlineTokInstruction(this);
        }

        /// <summary>
        /// Gets the member.
        /// </summary>
        /// <value>
        /// The member.
        /// </value>
        public MemberInfo Member
        {
            get
            {
                if (this.m_member == null)
                    this.m_member = this.m_resolver.AsMember(this.Token);
                return this.m_member;
            }
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public int Token { get { return this.m_token; } }

        private MemberInfo m_member;
        private ITokenResolver m_resolver;
        private int m_token;

    }
}
