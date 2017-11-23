// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodePrimitiveExpression : CodeExpression
    {

        public CodePrimitiveExpression(ILInstruction inline) : base(inline) { }

        public CodePrimitiveExpression(ILInstruction inline, object value) : base(inline)
        {
            Value = value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {

            if (obj is CodePrimitiveExpression p)
                return p.Value.Equals(this.Value);

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
            return this.Value?.GetHashCode() ?? base.GetHashCode();
        }

        public object Value { get; set; }

    }
}
