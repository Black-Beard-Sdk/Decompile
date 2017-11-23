using System;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineSigInstruction
    /// </summary>
    public class InlineSigInstruction : ILInstruction
    {

        internal InlineSigInstruction(int offset, OpCode opCode, int token, ITokenResolver resolver) : base(offset, opCode)
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
            visitor.VisitInlineSigInstruction(this);
        }

        /// <summary>
        /// Gets the signature.
        /// </summary>
        /// <value>
        /// The signature.
        /// </value>
        public byte[] Sign
        {
            get
            {
                if (this._sign == null)
                    this._sign = this._resolver.AsSign(this._token);
                return this._sign;
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
            if (obj is InlineSigInstruction i)
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

        private ITokenResolver _resolver;
        private byte[] _sign;
        private int _token;

    }
}
