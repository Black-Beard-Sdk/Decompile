// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeDirectionExpression : CodeExpression
    {
        public CodeDirectionExpression(ILInstruction inline) : base(inline) { }

        public CodeDirectionExpression(ILInstruction inline, FieldDirection direction, CodeExpression expression) : base(inline)
        {
            Expression = expression;
            Direction = direction;
        }

        public CodeExpression Expression { get; set; }

        public FieldDirection Direction { get; set; }
    }
}
