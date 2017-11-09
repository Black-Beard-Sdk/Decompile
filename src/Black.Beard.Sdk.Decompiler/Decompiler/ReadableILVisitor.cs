using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bb.Sdk.Decompiler.IlParser;
using System.Reflection.Metadata;

namespace Bb.Sdk.Decompiler.Exceptions
{
    
    class ReadableILVisitor : ILInstructionVisitor
    {

        private MethodDefinition method;

        public static ReadableILVisitor Create(MethodBase method, int offset)
        {
            ReadableILVisitor visitor = new ReadableILVisitor();

            var m = (method as MethodInfo);
            var type = m.ReturnType;

            var attributes = (MethodAttributes)(int)method.Attributes;

            visitor.method = method.Create();

            ILReaderFactory.Create(method, offset)
                .Accept(visitor);

            return visitor;

        }

        /// <summary>
        /// Visits the inline br target instruction.
        /// </summary>
        /// <param name="inlineBrTargetInstruction">The inline br target instruction.</param>
        public override void VisitInlineBrTargetInstruction(InlineBrTargetInstruction inlineBrTargetInstruction)
        {
        }

        /// <summary>
        /// Visits the inline field instruction.
        /// </summary>
        /// <param name="inlineFieldInstruction">The inline field instruction.</param>
        public override void VisitInlineFieldInstruction(InlineFieldInstruction inlineFieldInstruction)
        {
        }

        /// <summary>
        /// Visits the inline i8 instruction.
        /// </summary>
        /// <param name="inlineI8Instruction">The inline i8 instruction.</param>
        public override void VisitInlineI8Instruction(InlineI8Instruction inlineI8Instruction)
        {
        }

        /// <summary>
        /// Visits the inline i instruction.
        /// </summary>
        /// <param name="inlineIInstruction">The inline i instruction.</param>
        public override void VisitInlineIInstruction(InlineIInstruction inlineIInstruction)
        {
        }

        /// <summary>
        /// Visits the inline method instruction.
        /// </summary>
        /// <param name="inlineMethodInstruction">The inline method instruction.</param>
        public override void VisitInlineMethodInstruction(InlineMethodInstruction inlineMethodInstruction)
        {
        }

        /// <summary>
        /// Visits the inline none instruction.
        /// </summary>
        /// <param name="inlineNoneInstruction">The inline none instruction.</param>
        public override void VisitInlineNoneInstruction(InlineNoneInstruction inlineNoneInstruction)
        {
        }

        /// <summary>
        /// Visits the inline r instruction.
        /// </summary>
        /// <param name="inlineRInstruction">The inline r instruction.</param>
        public override void VisitInlineRInstruction(InlineRInstruction inlineRInstruction)
        {
        }

        /// <summary>
        /// Visits the inline sig instruction.
        /// </summary>
        /// <param name="inlineSigInstruction">The inline sig instruction.</param>
        public override void VisitInlineSigInstruction(InlineSigInstruction inlineSigInstruction)
        {
        }

        /// <summary>
        /// Visits the inline string instruction.
        /// </summary>
        /// <param name="inlineStringInstruction">The inline string instruction.</param>
        public override void VisitInlineStringInstruction(InlineStringInstruction inlineStringInstruction)
        {
        }

        /// <summary>
        /// Visits the inline switch instruction.
        /// </summary>
        /// <param name="inlineSwitchInstruction">The inline switch instruction.</param>
        public override void VisitInlineSwitchInstruction(InlineSwitchInstruction inlineSwitchInstruction)
        {
        }

        /// <summary>
        /// Visits the inline tok instruction.
        /// </summary>
        /// <param name="inlineTokInstruction">The inline tok instruction.</param>
        public override void VisitInlineTokInstruction(InlineTokInstruction inlineTokInstruction)
        {
        }

        /// <summary>
        /// Visits the inline type instruction.
        /// </summary>
        /// <param name="inlineTypeInstruction">The inline type instruction.</param>
        public override void VisitInlineTypeInstruction(InlineTypeInstruction inlineTypeInstruction)
        {
        }

        /// <summary>
        /// Visits the inline variable instruction.
        /// </summary>
        /// <param name="inlineVarInstruction">The inline variable instruction.</param>
        public override void VisitInlineVarInstruction(InlineVarInstruction inlineVarInstruction)
        {
        }

        /// <summary>
        /// Visits the short inline br target instruction.
        /// </summary>
        /// <param name="shortInlineBrTargetInstruction">The short inline br target instruction.</param>
        public override void VisitShortInlineBrTargetInstruction(ShortInlineBrTargetInstruction shortInlineBrTargetInstruction)
        {
        }

        /// <summary>
        /// Visits the short inline i instruction.
        /// </summary>
        /// <param name="shortInlineIInstruction">The short inline i instruction.</param>
        public override void VisitShortInlineIInstruction(ShortInlineIInstruction shortInlineIInstruction)
        {
        }

        /// <summary>
        /// Visits the short inline r instruction.
        /// </summary>
        /// <param name="shortInlineRInstruction">The short inline r instruction.</param>
        public override void VisitShortInlineRInstruction(ShortInlineRInstruction shortInlineRInstruction)
        {
        }

        /// <summary>
        /// Visits the short inline variable instruction.
        /// </summary>
        /// <param name="shortInlineVarInstruction">The short inline variable instruction.</param>
        public override void VisitShortInlineVarInstruction(ShortInlineVarInstruction shortInlineVarInstruction)
        {
        }

    }

}
