// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Specialized;
using System.Reflection;

namespace System.CodeDom.Compiler
{
    public partial class CompilerResults
    {
        private readonly StringCollection _output = new StringCollection();
        private Assembly _compiledAssembly;
        private TempFileCollection _tempFiles;

        public CompilerResults(TempFileCollection tempFiles)
        {
            _tempFiles = tempFiles;
        }

        public TempFileCollection TempFiles
        {
            get
            {
                return _tempFiles;
            }
            set
            {
                _tempFiles = value;
            }
        }

        public StringCollection Output => _output;

        public string PathToAssembly { get; set; }

        public int NativeCompilerReturnValue { get; set; }
    }
}