using System;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ILInstructionVisitor
    /// </summary>
    public abstract class ILInstructionVisitor
    {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ILInstructionVisitor"/> class.
        /// </summary>
        protected ILInstructionVisitor()
        {
        }

        /// <summary>
        /// Visits the inline br target instruction.
        /// </summary>
        /// <param name="inlineBrTargetInstruction">The inline br target instruction.</param>
        public virtual void VisitInlineBrTargetInstruction(InlineBrTargetInstruction inlineBrTargetInstruction)
        {
        }

        /// <summary>
        /// Visits the inline field instruction.
        /// </summary>
        /// <param name="inlineFieldInstruction">The inline field instruction.</param>
        public virtual void VisitInlineFieldInstruction(InlineFieldInstruction inlineFieldInstruction)
        {
        }

        /// <summary>
        /// Visits the inline i8 instruction.
        /// </summary>
        /// <param name="inlineI8Instruction">The inline i8 instruction.</param>
        public virtual void VisitInlineI8Instruction(InlineI8Instruction inlineI8Instruction)
        {
        }

        /// <summary>
        /// Visits the inline i instruction.
        /// </summary>
        /// <param name="inlineIInstruction">The inline i instruction.</param>
        public virtual void VisitInlineIInstruction(InlineIInstruction inlineIInstruction)
        {
        }

        /// <summary>
        /// Visits the inline method instruction.
        /// </summary>
        /// <param name="inlineMethodInstruction">The inline method instruction.</param>
        public virtual void VisitInlineMethodInstruction(InlineMethodInstruction inlineMethodInstruction)
        {
        }

        /// <summary>
        /// Visits the inline none instruction.
        /// </summary>
        /// <param name="inlineNoneInstruction">The inline none instruction.</param>
        public virtual void VisitInlineNoneInstruction(InlineNoneInstruction inlineNoneInstruction)
        {
        }

        /// <summary>
        /// Visits the inline r instruction.
        /// </summary>
        /// <param name="inlineRInstruction">The inline r instruction.</param>
        public virtual void VisitInlineRInstruction(InlineRInstruction inlineRInstruction)
        {
        }

        /// <summary>
        /// Visits the inline sig instruction.
        /// </summary>
        /// <param name="inlineSigInstruction">The inline sig instruction.</param>
        public virtual void VisitInlineSigInstruction(InlineSigInstruction inlineSigInstruction)
        {
        }

        /// <summary>
        /// Visits the inline string instruction.
        /// </summary>
        /// <param name="inlineStringInstruction">The inline string instruction.</param>
        public virtual void VisitInlineStringInstruction(InlineStringInstruction inlineStringInstruction)
        {
        }

        /// <summary>
        /// Visits the inline switch instruction.
        /// </summary>
        /// <param name="inlineSwitchInstruction">The inline switch instruction.</param>
        public virtual void VisitInlineSwitchInstruction(InlineSwitchInstruction inlineSwitchInstruction)
        {
        }

        /// <summary>
        /// Visits the inline tok instruction.
        /// </summary>
        /// <param name="inlineTokInstruction">The inline tok instruction.</param>
        public virtual void VisitInlineTokInstruction(InlineTokInstruction inlineTokInstruction)
        {
        }

        /// <summary>
        /// Visits the inline type instruction.
        /// </summary>
        /// <param name="inlineTypeInstruction">The inline type instruction.</param>
        public virtual void VisitInlineTypeInstruction(InlineTypeInstruction inlineTypeInstruction)
        {
        }

        /// <summary>
        /// Visits the inline variable instruction.
        /// </summary>
        /// <param name="inlineVarInstruction">The inline variable instruction.</param>
        public virtual void VisitInlineVarInstruction(InlineVarInstruction inlineVarInstruction)
        {
        }

        /// <summary>
        /// Visits the short inline br target instruction.
        /// </summary>
        /// <param name="shortInlineBrTargetInstruction">The short inline br target instruction.</param>
        public virtual void VisitShortInlineBrTargetInstruction(ShortInlineBrTargetInstruction shortInlineBrTargetInstruction)
        {
        }

        /// <summary>
        /// Visits the short inline i instruction.
        /// </summary>
        /// <param name="shortInlineIInstruction">The short inline i instruction.</param>
        public virtual void VisitShortInlineIInstruction(ShortInlineIInstruction shortInlineIInstruction)
        {
        }

        /// <summary>
        /// Visits the short inline r instruction.
        /// </summary>
        /// <param name="shortInlineRInstruction">The short inline r instruction.</param>
        public virtual void VisitShortInlineRInstruction(ShortInlineRInstruction shortInlineRInstruction)
        {
        }

        /// <summary>
        /// Visits the short inline variable instruction.
        /// </summary>
        /// <param name="shortInlineVarInstruction">The short inline variable instruction.</param>
        public virtual void VisitShortInlineVarInstruction(ShortInlineVarInstruction shortInlineVarInstruction)
        {
        }
    }
}
