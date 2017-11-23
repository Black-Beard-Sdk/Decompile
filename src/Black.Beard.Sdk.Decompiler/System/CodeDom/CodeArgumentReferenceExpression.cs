// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeArgumentReferenceExpression : CodeExpression
    {
        private string _parameterName;

        public CodeArgumentReferenceExpression(ILInstruction inline) : base(inline) { }

        public CodeArgumentReferenceExpression(ILInstruction inline, string parameterName) : base(inline)
        {
            _parameterName = parameterName;
        }

        public string ParameterName
        {
            get { return _parameterName ?? string.Empty; }
            set { _parameterName = value; }
        }
    }
}
