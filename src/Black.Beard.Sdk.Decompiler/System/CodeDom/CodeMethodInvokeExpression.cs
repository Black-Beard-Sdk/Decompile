// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeMethodInvokeExpression : CodeExpression
    {
        private CodeMethodReferenceExpression _method;

        public CodeMethodInvokeExpression(ILInstruction inline) : base(inline) { }

        public CodeMethodInvokeExpression(ILInstruction inline, CodeMethodReferenceExpression method, params CodeExpression[] parameters) : base(inline)
        {
            _method = method;
            Parameters.AddRange(parameters);
        }

        public CodeMethodInvokeExpression(ILInstruction inline, CodeExpression targetObject, string methodName, params CodeExpression[] parameters) : base(inline)
        {
            _method = new CodeMethodReferenceExpression(inline, targetObject, methodName);
            Parameters.AddRange(parameters);
        }

        public CodeMethodReferenceExpression Method
        {
            get { return _method ?? (_method = new CodeMethodReferenceExpression(this.ILInstruction)); }
            set { _method = value; }
        }

        public CodeExpressionCollection Parameters { get; } = new CodeExpressionCollection();
    }
}
