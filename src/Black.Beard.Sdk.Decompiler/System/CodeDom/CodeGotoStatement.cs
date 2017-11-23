// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeGotoStatement : CodeStatement
    {

        public CodeGotoStatement(ILInstruction inline, Marker marker)
            : base(inline)
        {
            this.Marker = marker;
        }

        public Marker Marker { get; set; }

        public string Label { get { return this.Marker.Label; } }

    }
}
