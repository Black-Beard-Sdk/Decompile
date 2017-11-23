// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeVariableReferenceExpression : CodeExpression
    {
        private string _variableName;

        public CodeVariableReferenceExpression(ILInstruction inline) : base(inline) { }

        public CodeVariableReferenceExpression(ILInstruction inline, string variableName) : base(inline)
        {

            if (string.IsNullOrEmpty(variableName))
                throw new ArgumentException("message", nameof(variableName));

            _variableName = variableName;

        }

        public string VariableName
        {
            get { return _variableName ?? string.Empty; }
            set { _variableName = value; }
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

            if (obj is CodeVariableReferenceExpression p)
                return p.VariableName == this.VariableName;

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
            return this._variableName.GetHashCode();
        }

    }
}
