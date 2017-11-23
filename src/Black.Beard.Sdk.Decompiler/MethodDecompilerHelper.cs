using Bb.Sdk.Decompiler.IlParser;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace Bb.Sdk
{

    public static class MethodDecompilerHelper
    {

        /// <summary>
        /// Gets the source code of the method in CodeDom tree.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static CodeMemberMethod GetSourceCode(this System.Reflection.MethodInfo method)
        {

            var ilReader = ILReaderFactory.Create(method, 0);

            var visitor = new ReadableILVisitor();
            ilReader.Parse(visitor);
            CodeMemberMethod r = visitor.GetMethod();

            return r;
        }

    }

}
