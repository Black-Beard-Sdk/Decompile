// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeMethodReturnStatement : CodeStatement
    {
        public CodeMethodReturnStatement(ILInstruction inline) : base(inline) { }

        public CodeMethodReturnStatement(ILInstruction inline, CodeExpression expression) : base(inline)
        {
            Expression = expression;
        }

        public CodeExpression Expression { get; set; }
    }
}
