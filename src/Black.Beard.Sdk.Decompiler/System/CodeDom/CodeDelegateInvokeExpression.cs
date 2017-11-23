// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeDelegateInvokeExpression : CodeExpression
    {
        public CodeDelegateInvokeExpression(ILInstruction inline) : base(inline) { }

        public CodeDelegateInvokeExpression(ILInstruction inline, CodeExpression targetObject) : base(inline)
        {
            TargetObject = targetObject;
        }

        public CodeDelegateInvokeExpression(ILInstruction inline, CodeExpression targetObject, params CodeExpression[] parameters) : base(inline)
        {
            TargetObject = targetObject;
            Parameters.AddRange(parameters);
        }

        public CodeExpression TargetObject { get; set; }

        public CodeExpressionCollection Parameters { get; } = new CodeExpressionCollection();
    }
}
