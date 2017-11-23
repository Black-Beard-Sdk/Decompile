// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeArrayCreateExpression : CodeExpression
    {
        private readonly CodeExpressionCollection _initializers = new CodeExpressionCollection();
        private CodeTypeReference _createType;

        public CodeArrayCreateExpression(ILInstruction inline) : base(inline)
        {
        }

        public CodeArrayCreateExpression(ILInstruction inline, CodeTypeReference createType, params CodeExpression[] initializers) : base(inline)
        {
            _createType = createType;
            _initializers.AddRange(initializers);
        }

        public CodeArrayCreateExpression(ILInstruction inline, string createType, params CodeExpression[] initializers) : base(inline)
        {
            _createType = new CodeTypeReference(createType);
            _initializers.AddRange(initializers);
        }

        public CodeArrayCreateExpression(ILInstruction inline, Type createType, params CodeExpression[] initializers) : base(inline)
        {
            _createType = new CodeTypeReference(createType);
            _initializers.AddRange(initializers);
        }

        public CodeArrayCreateExpression(ILInstruction inline, CodeTypeReference createType, int size) : base(inline)
        {
            _createType = createType;
            Size = size;
        }

        public CodeArrayCreateExpression(ILInstruction inline, string createType, int size) : base(inline)
        {
            _createType = new CodeTypeReference(createType);
            Size = size;
        }

        public CodeArrayCreateExpression(ILInstruction inline, Type createType, int size) : base(inline)
        {
            _createType = new CodeTypeReference(createType);
            Size = size;
        }

        public CodeArrayCreateExpression(ILInstruction inline, CodeTypeReference createType, CodeExpression size) : base(inline)
        {
            _createType = createType;
            SizeExpression = size;
        }

        public CodeArrayCreateExpression(ILInstruction inline, string createType, CodeExpression size) : base(inline)
        {
            _createType = new CodeTypeReference(createType);
            SizeExpression = size;
        }

        public CodeArrayCreateExpression(ILInstruction inline, Type createType, CodeExpression size) : base(inline)
        {
            _createType = new CodeTypeReference(createType);
            SizeExpression = size;
        }

        public CodeTypeReference CreateType
        {
            get { return _createType ?? (_createType = new CodeTypeReference("")); }
            set { _createType = value; }
        }

        public CodeExpressionCollection Initializers => _initializers;

        public int Size { get; set; }

        public CodeExpression SizeExpression { get; set; }
    }
}
