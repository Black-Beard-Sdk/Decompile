// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{


    [System.Diagnostics.DebuggerDisplay("{Label}")]
    public class Marker : CodeObject
    {
        

        public Marker(ILInstruction initialInline, int targetOffset, blockType kind) : base(initialInline.Offset)
        {
            this.Inline = initialInline;
            this.TargetOffset = targetOffset;
            this.Kind = kind;
        }

        public ILInstruction Inline { get; }

        public int TargetOffset { get; }
        public blockType Kind { get; }

        public string Label { get { return string.Format("IL_{0:x4}", this.TargetOffset); } }

    }


    public enum blockType
        {
            Undefined,
            IfTrue,
            IfFalse,
    }

}