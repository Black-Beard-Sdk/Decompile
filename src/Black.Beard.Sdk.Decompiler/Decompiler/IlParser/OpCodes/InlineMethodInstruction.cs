using System.Reflection;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineMethodInstruction
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{OpCode} {Method.ToString()}")]
    public class InlineMethodInstruction : ILInstruction
    {

        internal InlineMethodInstruction(int offset, OpCode opCode, int token, ITokenResolver resolver) : base(offset, opCode)
        {
            this._resolver = resolver;
            this._token = token;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(AbstractILInstructionVisitor visitor)
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
                if (this._method == null)
                    this._method = this._resolver.AsMethod(this._token);
                return this._method;
            }
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public int Token { get { return this._token; } }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is InlineMethodInstruction i)
                if (i.OpCode == this.OpCode)
                    return i._token == this._token;
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
            return this.OpCode.GetHashCode() ^ this._token;
        }

        private MethodBase _method;
        private ITokenResolver _resolver;
        private int _token;

    }
}
