using System;
using System.Reflection;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ILInstructionVisitor
    /// </summary>
    public abstract class AbstractILInstructionVisitor
    {

        protected MethodBase Method;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractILInstructionVisitor"/> class.
        /// </summary>
        protected AbstractILInstructionVisitor()
        {
        }

        /// <summary>
        /// Visits the inline br target instruction.
        /// </summary>
        /// <param name="inline">The inline br target instruction.</param>
        public virtual void VisitInlineBrTargetInstruction(InlineBrTargetInstruction inline)
        {
        }

        public virtual void VisitInlineStElemInstruction(InlineStElemInstruction inline)
        {            
        }

        /// <summary>
        /// Visits the inline ld elem instruction.
        /// </summary>
        /// <param name="inline">The inline.</param>
        public virtual void VisitInlineLdElemInstruction(InlineLdElemInstruction inline)
        {
            
        }

        /// <summary>
        /// Visits the inline ld argument instruction.
        /// </summary>
        /// <param name="inline">The inline.</param>
        public virtual void VisitInlineLdArgInstruction(InlineLdArgInstruction inline)
        {
            
        }

        /// <summary>
        /// Visits the inline condition instruction.
        /// </summary>
        /// <param name="inline">The inline.</param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void VisitInlineConditionInstruction(InlineConditionInstruction inline)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Visits the inline convertion instruction.
        /// </summary>
        /// <param name="inline">The inline.</param>
        public virtual void VisitInlineConvertionInstruction(InlineConvertionInstruction inline)
        {
            
        }

        /// <summary>
        /// Visits the inline operator loc instruction.
        /// </summary>
        /// <param name="inline">The inline operator loc instruction.</param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void VisitInlineOperatorInstruction(InlineOperatorInstruction inline)
        {
            
        }        

        /// <summary>
        /// Visits the inline ld i4 s instruction.
        /// </summary>
        /// <param name="inline">The inline.</param>
        public virtual void VisitInlineLdI4Instruction(InlineLdI4Instruction inline)
        {

        }

        /// <summary>
        /// Visits the inline ld loc s instruction.
        /// </summary>
        /// <param name="inline">The inline.</param>
        public virtual void VisitInlineLdLocSInstruction(InlineLdLocSInstruction inline)
        {
        }

        /// <summary>
        /// Visits the inline field instruction.
        /// </summary>
        /// <param name="inline">The inline field instruction.</param>
        public virtual void VisitInlineFieldInstruction(InlineFieldInstruction inline)
        {
        }

        /// <summary>
        /// Visits the inline i8 instruction.
        /// </summary>
        /// <param name="inline">The inline i8 instruction.</param>
        public virtual void VisitInlineI8Instruction(InlineI8Instruction inline)
        {
        }

        /// <summary>
        /// Visits the inline i instruction.
        /// </summary>
        /// <param name="inline">The inline i instruction.</param>
        public virtual void VisitInlineIInstruction(InlineIInstruction inline)
        {
        }

        /// <summary>
        /// Visits the inline st loc instruction.
        /// </summary>
        /// <param name="inline">The inline.</param>
        public virtual void VisitInlineStLocInstruction(InlineStLocInstruction inline)
        {
            
        }

        /// <summary>
        /// Visits the inline method instruction.
        /// </summary>
        /// <param name="inline">The inline method instruction.</param>
        public virtual void VisitInlineMethodInstruction(InlineMethodInstruction inline)
        {
        }

        /// <summary>
        /// Inlines the ld loc instruction.
        /// </summary>
        /// <param name="inline">The inline.</param>
        public virtual void VisitInlineLdLocInstruction(InlineLdLocInstruction inline)
        {
        }

        /// <summary>
        /// Visits the inline st loc s instruction.
        /// </summary>
        /// <param name="inline">The inline.</param>
        public virtual void VisitInlineStLocSInstruction(InlineStLocSInstruction inline)
        {
        }

        /// <summary>
        /// Visits the inline none instruction.
        /// </summary>
        /// <param name="inline">The inline none instruction.</param>
        public virtual void VisitInlineNoneInstruction(InlineNoneInstruction inline)
        {
        }

        /// <summary>
        /// Visits the inline r instruction.
        /// </summary>
        /// <param name="inline">The inline r instruction.</param>
        public virtual void VisitInlineRInstruction(InlineRInstruction inline)
        {
        }

        /// <summary>
        /// Visits the inline sig instruction.
        /// </summary>
        /// <param name="inline">The inline sig instruction.</param>
        public virtual void VisitInlineSigInstruction(InlineSigInstruction inline)
        {
        }

        /// <summary>
        /// Visits the inline string instruction.
        /// </summary>
        /// <param name="inline">The inline string instruction.</param>
        public virtual void VisitInlineStringInstruction(InlineStringInstruction inline)
        {
        }

        internal void SetMethod(MethodBase method)
        {
            this.Method = method;
        }

        /// <summary>
        /// Visits the inline switch instruction.
        /// </summary>
        /// <param name="inline">The inline switch instruction.</param>
        public virtual void VisitInlineSwitchInstruction(InlineSwitchInstruction inline)
        {
        }

        /// <summary>
        /// Visits the inline tok instruction.
        /// </summary>
        /// <param name="inline">The inline tok instruction.</param>
        public virtual void VisitInlineTokInstruction(InlineTokInstruction inline)
        {
        }

        /// <summary>
        /// Visits the inline type instruction.
        /// </summary>
        /// <param name="inline">The inline type instruction.</param>
        public virtual void VisitInlineTypeInstruction(InlineTypeInstruction inline)
        {
        }

        /// <summary>
        /// Visits the inline variable instruction.
        /// </summary>
        /// <param name="inline">The inline variable instruction.</param>
        public virtual void VisitInlineVarInstruction(InlineVarInstruction inline)
        {
        }

        /// <summary>
        /// Visits the short inline br target instruction.
        /// </summary>
        /// <param name="inline">The short inline br target instruction.</param>
        public virtual void VisitShortInlineBrTargetInstruction(ShortInlineBrTargetInstruction inline)
        {
        }

        /// <summary>
        /// Visits the short inline i instruction.
        /// </summary>
        /// <param name="inline">The short inline i instruction.</param>
        public virtual void VisitShortInlineIInstruction(ShortInlineIInstruction inline)
        {
        }

        /// <summary>
        /// Visits the short inline r instruction.
        /// </summary>
        /// <param name="inline">The short inline r instruction.</param>
        public virtual void VisitShortInlineRInstruction(ShortInlineRInstruction inline)
        {
        }

        /// <summary>
        /// Visits the short inline variable instruction.
        /// </summary>
        /// <param name="inline">The short inline variable instruction.</param>
        public virtual void VisitShortInlineVarInstruction(ShortInlineVarInstruction inline)
        {
        }
    }
}
