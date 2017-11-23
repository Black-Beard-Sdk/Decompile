// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeArrayIndexerExpression : CodeExpression
    {
        private CodeExpressionCollection _indices;

        public CodeArrayIndexerExpression(ILInstruction inline) : base(inline) { }

        public CodeArrayIndexerExpression(ILInstruction inline, CodeExpression targetObject, params CodeExpression[] indices) : base(inline)
        {
            TargetObject = targetObject;
            Indices.AddRange(indices);
        }

        public CodeExpression TargetObject { get; set; }

        public CodeExpressionCollection Indices => _indices ?? (_indices = new CodeExpressionCollection());
    }
}
