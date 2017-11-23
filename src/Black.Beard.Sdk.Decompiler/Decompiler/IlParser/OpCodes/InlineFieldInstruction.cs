using System.Reflection;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// InlineFieldInstruction
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{OpCode} {Field.Name}")]
    public class InlineFieldInstruction : ILInstruction
    {

        internal InlineFieldInstruction(ITokenResolver resolver, int offset, OpCode opCode, int token) : base(offset, opCode)
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
                if (this._field == null)
                    this._field = this._resolver.AsField(this._token);
                return this._field;
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
            if (obj is InlineFieldInstruction i)
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
            return this.OpCode.GetHashCode() ^ (int)this._token;
        }

        private FieldInfo _field;
        private ITokenResolver _resolver;
        private int _token;

    }
}
