// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeAttachEventStatement : CodeStatement
    {

        private CodeEventReferenceExpression _eventRef;

        public CodeAttachEventStatement(ILInstruction inline) : base(inline) { }

        public CodeAttachEventStatement(ILInstruction inline, CodeEventReferenceExpression eventRef, CodeExpression listener) : base(inline)
        {
            _eventRef = eventRef;
            Listener = listener;
        }

        public CodeAttachEventStatement(ILInstruction inline, CodeExpression targetObject, string eventName, CodeExpression listener) :
            this(inline, new CodeEventReferenceExpression(inline, targetObject, eventName), listener)
        {
        }

        public CodeEventReferenceExpression Event
        {
            get { return _eventRef ?? (_eventRef = new CodeEventReferenceExpression(this.ILInstruction)); }
            set { _eventRef = value; }
        }

        public CodeExpression Listener { get; set; }
    }
}
