// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeAssignStatement : CodeStatement
    {
        public CodeAssignStatement(ILInstruction inline) : base(inline) { }

        public CodeAssignStatement(ILInstruction inline, CodeExpression left, CodeExpression right) : base(inline)
        {
            Left = left;
            Right = right;
        }

        public CodeExpression Left { get; set; }

        public CodeExpression Right { get; set; }
    }
}
