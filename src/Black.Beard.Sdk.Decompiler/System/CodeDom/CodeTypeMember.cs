// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.CodeDom.Compiler;
using System.IO;

namespace System.CodeDom
{
    public class CodeTypeMember : CodeObject
    {


        public CodeTypeMember(int offset) : base(offset)
        {

        }

        private string _name;
        private CodeAttributeDeclarationCollection _customAttributes = null;

        public string Name
        {
            get { return _name ?? string.Empty; }
            set { _name = value; }
        }

        public MemberAttributes Attributes { get; set; } = MemberAttributes.Private | MemberAttributes.Final;

        public CodeAttributeDeclarationCollection CustomAttributes
        {
            get { return _customAttributes ?? (_customAttributes = new CodeAttributeDeclarationCollection()); }
            set { _customAttributes = value; }
        }




        public override string ToString()
        {

            var w = new StringWriter();
            var o = new CodeGeneratorOptions();

            var provider = new Microsoft.CSharp.CSharpCodeProvider();
            provider.GenerateCodeFromMember(this, w, o);
            return w.ToString();
        }

    }
}
