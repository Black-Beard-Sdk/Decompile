// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeObjectCreateExpression : CodeExpression
    {
        private CodeTypeReference _createType;

        public CodeObjectCreateExpression(ILInstruction inline) : base(inline) { }

        public CodeObjectCreateExpression(ILInstruction inline, CodeTypeReference createType, params CodeExpression[] parameters) : base(inline)
        {
            CreateType = createType;
            Parameters.AddRange(parameters);
        }

        public CodeObjectCreateExpression(ILInstruction inline, string createType, params CodeExpression[] parameters) : base(inline)
        {
            CreateType = new CodeTypeReference(createType);
            Parameters.AddRange(parameters);
        }

        public CodeObjectCreateExpression(ILInstruction inline, Type createType, params CodeExpression[] parameters) : base(inline)
        {
            CreateType = new CodeTypeReference(createType);
            Parameters.AddRange(parameters);
        }

        public CodeTypeReference CreateType
        {
            get { return _createType ?? (_createType = new CodeTypeReference(string.Empty)); }
            set { _createType = value; }
        }

        public CodeExpressionCollection Parameters { get; } = new CodeExpressionCollection();
    }
}
