// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeDelegateCreateExpression : CodeExpression
    {
        private CodeTypeReference _delegateType;
        private string _methodName;

        public CodeDelegateCreateExpression(ILInstruction inline) : base(inline) { }

        public CodeDelegateCreateExpression(ILInstruction inline, CodeTypeReference delegateType, CodeExpression targetObject, string methodName): base(inline)
        {
            _delegateType = delegateType;
            TargetObject = targetObject;
            _methodName = methodName;
        }

        public CodeTypeReference DelegateType
        {
            get { return _delegateType ?? (_delegateType = new CodeTypeReference("")); }
            set { _delegateType = value; }
        }

        public CodeExpression TargetObject { get; set; }

        public string MethodName
        {
            get { return _methodName ?? string.Empty; }
            set { _methodName = value; }
        }
    }
}
