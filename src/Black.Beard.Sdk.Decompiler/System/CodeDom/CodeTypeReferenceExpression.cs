// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeTypeReferenceExpression : CodeExpression
    {
        private CodeTypeReference _type;

        public CodeTypeReferenceExpression(ILInstruction inline) : base(inline) { }

        public CodeTypeReferenceExpression(ILInstruction inline, CodeTypeReference type) : base(inline)
        {
            Type = type;
        }

        public CodeTypeReferenceExpression(ILInstruction inline, string type) : base(inline)
        {
            Type = new CodeTypeReference(type);
        }

        public CodeTypeReferenceExpression(ILInstruction inline, Type type) : base(inline)
        {
            Type = new CodeTypeReference(type);
        }

        public CodeTypeReference Type
        {
            get { return _type ?? (_type = new CodeTypeReference("")); }
            set { _type = value; }
        }
    }
}
