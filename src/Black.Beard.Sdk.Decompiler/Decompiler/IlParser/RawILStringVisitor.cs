using System;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// RawILStringVisitor
    /// </summary>
    public class RawILStringVisitor : ReadableILStringVisitor
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RawILStringVisitor"/> class.
        /// </summary>
        /// <param name="collector">The collector.</param>
        public RawILStringVisitor(IILStringCollector collector) : this(collector, DefaultFormatProvider.Instance)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RawILStringVisitor"/> class.
        /// </summary>
        /// <param name="collector">The collector.</param>
        /// <param name="formatProvider">The format provider.</param>
        public RawILStringVisitor(IILStringCollector collector, Bb.Sdk.Decompiler.IlParser.IFormatProvider formatProvider) : base(collector, formatProvider)
        {
        }

        /// <summary>
        /// Visits the inline br target instruction.
        /// </summary>
        /// <param name="inlineBrTargetInstruction">The inline br target instruction.</param>
        public override void VisitInlineBrTargetInstruction(InlineBrTargetInstruction inlineBrTargetInstruction)
        {
            base.collector.Process(inlineBrTargetInstruction, base.formatProvider.Int32ToHex(inlineBrTargetInstruction.Delta));
        }

        /// <summary>
        /// Visits the inline field instruction.
        /// </summary>
        /// <param name="inlineFieldInstruction">The inline field instruction.</param>
        public override void VisitInlineFieldInstruction(InlineFieldInstruction inlineFieldInstruction)
        {
            base.collector.Process(inlineFieldInstruction, base.formatProvider.Int32ToHex(inlineFieldInstruction.Token));
        }

        /// <summary>
        /// Visits the inline method instruction.
        /// </summary>
        /// <param name="inlineMethodInstruction">The inline method instruction.</param>
        public override void VisitInlineMethodInstruction(InlineMethodInstruction inlineMethodInstruction)
        {
            base.collector.Process(inlineMethodInstruction, base.formatProvider.Int32ToHex(inlineMethodInstruction.Token));
        }

        /// <summary>
        /// Visits the inline sig instruction.
        /// </summary>
        /// <param name="inlineSigInstruction">The inline sig instruction.</param>
        public override void VisitInlineSigInstruction(InlineSigInstruction inlineSigInstruction)
        {
            base.collector.Process(inlineSigInstruction, base.formatProvider.Int32ToHex(inlineSigInstruction.Token));
        }

        /// <summary>
        /// Visits the inline string instruction.
        /// </summary>
        /// <param name="inlineStringInstruction">The inline string instruction.</param>
        public override void VisitInlineStringInstruction(InlineStringInstruction inlineStringInstruction)
        {
            base.collector.Process(inlineStringInstruction, base.formatProvider.Int32ToHex(inlineStringInstruction.Token));
        }

        /// <summary>
        /// Visits the inline switch instruction.
        /// </summary>
        /// <param name="inlineSwitchInstruction">The inline switch instruction.</param>
        public override void VisitInlineSwitchInstruction(InlineSwitchInstruction inlineSwitchInstruction)
        {
            base.collector.Process(inlineSwitchInstruction, "...");
        }

        /// <summary>
        /// Visits the inline tok instruction.
        /// </summary>
        /// <param name="inlineTokInstruction">The inline tok instruction.</param>
        public override void VisitInlineTokInstruction(InlineTokInstruction inlineTokInstruction)
        {
            base.collector.Process(inlineTokInstruction, base.formatProvider.Int32ToHex(inlineTokInstruction.Token));
        }

        /// <summary>
        /// Visits the inline type instruction.
        /// </summary>
        /// <param name="inlineTypeInstruction">The inline type instruction.</param>
        public override void VisitInlineTypeInstruction(InlineTypeInstruction inlineTypeInstruction)
        {
            base.collector.Process(inlineTypeInstruction, base.formatProvider.Int32ToHex(inlineTypeInstruction.Token));
        }

        /// <summary>
        /// Visits the inline variable instruction.
        /// </summary>
        /// <param name="inlineVarInstruction">The inline variable instruction.</param>
        public override void VisitInlineVarInstruction(InlineVarInstruction inlineVarInstruction)
        {
            base.collector.Process(inlineVarInstruction, base.formatProvider.Int16ToHex(inlineVarInstruction.Ordinal));
        }

        /// <summary>
        /// Visits the short inline br target instruction.
        /// </summary>
        /// <param name="shortInlineBrTargetInstruction">The short inline br target instruction.</param>
        public override void VisitShortInlineBrTargetInstruction(ShortInlineBrTargetInstruction shortInlineBrTargetInstruction)
        {
            base.collector.Process(shortInlineBrTargetInstruction, base.formatProvider.Int8ToHex(shortInlineBrTargetInstruction.Delta));
        }

        /// <summary>
        /// Visits the short inline variable instruction.
        /// </summary>
        /// <param name="shortInlineVarInstruction">The short inline variable instruction.</param>
        public override void VisitShortInlineVarInstruction(ShortInlineVarInstruction shortInlineVarInstruction)
        {
            base.collector.Process(shortInlineVarInstruction, base.formatProvider.Int8ToHex(shortInlineVarInstruction.Ordinal));
        }

    }
}
