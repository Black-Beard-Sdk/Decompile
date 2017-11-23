// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodePropertyReferenceExpression : CodeExpression
    {

        private string _propertyName;

        public CodePropertyReferenceExpression(ILInstruction inline) : base(inline) { }

        public CodePropertyReferenceExpression(ILInstruction inline, CodeExpression targetObject, string propertyName) : base(inline)
        {
            TargetObject = targetObject;
            PropertyName = propertyName;
        }

        public CodeExpression TargetObject { get; set; }

        public string PropertyName
        {
            get { return _propertyName ?? string.Empty; }
            set { _propertyName = value; }
        }
    }
}
