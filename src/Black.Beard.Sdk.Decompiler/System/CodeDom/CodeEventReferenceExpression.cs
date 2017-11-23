// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeEventReferenceExpression : CodeExpression
    {
        private string _eventName;

        public CodeEventReferenceExpression(ILInstruction inline) : base(inline) { }

        public CodeEventReferenceExpression(ILInstruction inline, CodeExpression targetObject, string eventName) : base(inline)
        {
            TargetObject = targetObject;
            _eventName = eventName;
        }

        public CodeExpression TargetObject { get; set; }

        public string EventName
        {
            get { return _eventName ?? string.Empty; }
            set { _eventName = value; }
        }
    }
}
