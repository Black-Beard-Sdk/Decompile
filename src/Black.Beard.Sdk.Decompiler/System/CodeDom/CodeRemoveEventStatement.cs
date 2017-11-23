// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeRemoveEventStatement : CodeStatement
    {
        private CodeEventReferenceExpression _eventRef;

        public CodeRemoveEventStatement(ILInstruction inline) : base(inline) { }

        public CodeRemoveEventStatement(ILInstruction inline, CodeEventReferenceExpression eventRef, CodeExpression listener) : base(inline)
        {
            _eventRef = eventRef;
            Listener = listener;
        }

        public CodeRemoveEventStatement(ILInstruction inline, CodeExpression targetObject, string eventName, CodeExpression listener) : base(inline)
        {
            _eventRef = new CodeEventReferenceExpression(inline, targetObject, eventName);
            Listener = listener;
        }

        public CodeEventReferenceExpression Event
        {
            get { return _eventRef ?? (_eventRef = new CodeEventReferenceExpression(this.ILInstruction)); }
            set { _eventRef = value; }
        }

        public CodeExpression Listener { get; set; }
    }
}
