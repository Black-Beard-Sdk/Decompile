// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeLabeledStatement : CodeStatement
    {
        private string _label;

        public CodeLabeledStatement(ILInstruction inline) : base(inline) { }

        public CodeLabeledStatement(ILInstruction inline, string label) : base(inline)
        {
            _label = label;
        }

        public CodeLabeledStatement(ILInstruction inline, string label, CodeStatement statement) : base(inline)
        {
            _label = label;
            Statement = statement;
        }

        public string Label
        {
            get { return _label ?? string.Empty; }
            set { _label = value; }
        }

        public CodeStatement Statement { get; set; }
    }
}
