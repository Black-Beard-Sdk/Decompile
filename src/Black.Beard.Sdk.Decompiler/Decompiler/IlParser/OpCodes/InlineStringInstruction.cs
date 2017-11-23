using System;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineStringInstruction
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{OpCode} {String}")]
    public class InlineStringInstruction : ILInstruction
    {


        internal InlineStringInstruction(int offset, OpCode opCode, int token, ITokenResolver resolver) : base(offset, opCode)
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
                if (this._string == null)
                    this._string = this._resolver.AsString(this.Token);
                return this._string;
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
            if (obj is InlineStringInstruction i)
                if (i.OpCode == this.OpCode)
                    return i.String == this.String;
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
            return this.OpCode.GetHashCode() ^ this.String.GetHashCode();
        }
        private ITokenResolver _resolver;
        private string _string;
        private int _token;

    }
}
