// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeThrowExceptionStatement : CodeStatement
    {
        public CodeThrowExceptionStatement(ILInstruction inline) : base(inline) { }

        public CodeThrowExceptionStatement(ILInstruction inline, CodeExpression toThrow) : base(inline)
        {
            ToThrow = toThrow;
        }

        public CodeExpression ToThrow { get; set; }
    }
}
