// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeBinaryOperatorExpression : CodeExpression
    {
        public CodeBinaryOperatorExpression(ILInstruction inline) : base(inline) { }

        public CodeBinaryOperatorExpression(ILInstruction inline, CodeExpression left, CodeBinaryOperatorType op, CodeExpression right) : base(inline)
        {
            Right = right;
            Operator = op;
            Left = left;
        }

        public CodeExpression Right { get; set; }

        public CodeExpression Left { get; set; }

        public CodeBinaryOperatorType Operator { get; set; }
    }
}
