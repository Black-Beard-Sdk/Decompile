// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.CodeDom
{
    public class CodeMemberField : CodeTypeMember
    {
        
        private CodeTypeReference _type;

        public CodeMemberField(int offset) : base(offset) { }

        public CodeMemberField(int offset, CodeTypeReference type, string name) : base(offset)
        {
            Type = type;
            Name = name;
        }

        public CodeMemberField(int offset, string type, string name) : base(offset)
        {
            Type = new CodeTypeReference(type);
            Name = name;
        }

        public CodeMemberField(int offset, Type type, string name) : base(offset)
        {
            Type = new CodeTypeReference(type);
            Name = name;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public CodeTypeReference Type
        {
            get { return _type ?? (_type = new CodeTypeReference("")); }
            set { _type = value; }
        }

        public CodeExpression InitExpression { get; set; }
    }
}
