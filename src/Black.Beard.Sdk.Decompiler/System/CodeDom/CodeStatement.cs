// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;
using System.CodeDom.Compiler;
using System.IO;

namespace System.CodeDom
{
    public class CodeStatement : CodeObject
    {

        public CodeStatement(ILInstruction inline) : base(inline.Offset)
        {
            this.ILInstruction = inline;
        }

        public ILInstruction ILInstruction { get; }

        public override string ToString()
        {

            var w = new StringWriter();
            var o = new CodeGeneratorOptions();

            var provider = new Microsoft.CSharp.CSharpCodeProvider();
            provider.CreateGenerator().GenerateCodeFromStatement(this, w, o);
            return w.ToString();
        }


    }
}
