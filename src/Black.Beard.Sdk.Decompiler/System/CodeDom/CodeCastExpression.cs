// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeCastExpression : CodeExpression
    {
        private CodeTypeReference _targetType;

        public CodeCastExpression(ILInstruction inline) : base(inline) { }

        public CodeCastExpression(ILInstruction inline, CodeTypeReference targetType, CodeExpression expression) : base(inline)
        {
            TargetType = targetType;
            Expression = expression;
        }

        public CodeCastExpression(ILInstruction inline, string targetType, CodeExpression expression) : base(inline)
        {
            TargetType = new CodeTypeReference(targetType);
            Expression = expression;
        }

        public CodeCastExpression(ILInstruction inline, Type targetType, CodeExpression expression) : base(inline)
        {
            TargetType = new CodeTypeReference(targetType);
            Expression = expression;
        }

        public CodeTypeReference TargetType
        {
            get { return _targetType ?? (_targetType = new CodeTypeReference("")); }
            set { _targetType = value; }
        }

        public CodeExpression Expression { get; set; }
    }
}
