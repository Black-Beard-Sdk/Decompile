// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bb.Sdk.Decompiler.IlParser;

namespace System.CodeDom
{
    public class CodeConditionStatement : CodeStatement
    {

        public CodeConditionStatement(ILInstruction inline) : base(inline) { }

        public CodeConditionStatement(ILInstruction inline, CodeExpression condition, params CodeStatement[] trueStatements) : base(inline)
        {
            Condition = condition;
            TrueStatements.AddRange(trueStatements);
        }

        public CodeConditionStatement(ILInstruction inline, CodeExpression condition, CodeStatement[] trueStatements, CodeStatement[] falseStatements) : base(inline)
        {
            Condition = condition;
            TrueStatements.AddRange(trueStatements);
            FalseStatements.AddRange(falseStatements);
        }

        public CodeExpression Condition { get; set; }

        public CodeStatementCollection TrueStatements { get; } = new CodeStatementCollection();

        public CodeStatementCollection FalseStatements { get; } = new CodeStatementCollection();

    }
}
