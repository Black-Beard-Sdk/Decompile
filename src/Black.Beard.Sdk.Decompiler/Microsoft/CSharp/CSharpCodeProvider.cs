// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace Microsoft.CSharp
{

    public class CSharpCodeProvider //: CodeDomProvider
    {


        public CSharpCodeProvider()
        {
            _generator = new CSharpCodeGenerator();
        }

        public CSharpCodeProvider(IDictionary<string, string> providerOptions)
        {
            if (providerOptions == null)
                throw new ArgumentNullException(nameof(providerOptions));

            _generator = new CSharpCodeGenerator(providerOptions);
        }

        public ICodeGenerator CreateGenerator() => _generator;
        
        public void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options) =>
            _generator.GenerateCodeFromMember(member, writer, options);


        private readonly CSharpCodeGenerator _generator;

    }
}
