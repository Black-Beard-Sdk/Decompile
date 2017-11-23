namespace Bb.Sdk.Decompiler.IlParser
{
    using System;
    using System.Text;

    /// <summary>
    /// ReadableILStringVisitor
    /// </summary>
    public class ReadableILStringVisitor : AbstractILInstructionVisitor
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadableILStringVisitor"/> class.
        /// </summary>
        /// <param name="collector">The collector.</param>
        public ReadableILStringVisitor(IILStringCollector collector) : this(collector, DefaultFormatProvider.Instance)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadableILStringVisitor"/> class.
        /// </summary>
        /// <param name="collector">The collector.</param>
        /// <param name="formatProvider">The format provider.</param>
        public ReadableILStringVisitor(IILStringCollector collector, Bb.Sdk.Decompiler.IlParser.IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
            this.collector = collector;
        }

        public override void VisitInlineConditionInstruction(InlineConditionInstruction inline)
        {
            this.collector.Process(inline, string.Empty);
        }

        public override void VisitInlineConvertionInstruction(InlineConvertionInstruction inline)
        {
            this.collector.Process(inline, string.Empty);
        }

        public override void VisitInlineLdArgInstruction(InlineLdArgInstruction inline)
        {
            this.collector.Process(inline, string.Empty);
        }

        public override void VisitInlineLdElemInstruction(InlineLdElemInstruction inline)
        {
            this.collector.Process(inline, string.Empty);
        }

        public override void VisitInlineLdI4Instruction(InlineLdI4Instruction inline)
        {
            this.collector.Process(inline, string.Empty);
        }

        public override void VisitInlineLdLocInstruction(InlineLdLocInstruction inline)
        {
            this.collector.Process(inline, string.Empty);
        }

        public override void VisitInlineLdLocSInstruction(InlineLdLocSInstruction inline)
        {
            this.collector.Process(inline, this.formatProvider.Argument(inline.Ordinal));
        }

        public override void VisitInlineOperatorInstruction(InlineOperatorInstruction inline)
        {
            this.collector.Process(inline, string.Empty);
        }

        public override void VisitInlineStElemInstruction(InlineStElemInstruction inline)
        {
            this.collector.Process(inline, string.Empty);
        }

        public override void VisitInlineStLocInstruction(InlineStLocInstruction inline)
        {
            this.collector.Process(inline, string.Empty);
        }

        public override void VisitInlineStLocSInstruction(InlineStLocSInstruction inline)
        {
            this.collector.Process(inline, this.formatProvider.Argument(inline.Ordinal));
        }

        /// <summary>
        /// Visits the inline br target instruction.
        /// </summary>
        /// <param name="inlineBrTargetInstruction">The inline br target instruction.</param>
        public override void VisitInlineBrTargetInstruction(InlineBrTargetInstruction inlineBrTargetInstruction)
        {
            this.collector.Process(inlineBrTargetInstruction, this.formatProvider.Label(inlineBrTargetInstruction.TargetOffset));
        }

        /// <summary>
        /// Visits the inline field instruction.
        /// </summary>
        /// <param name="inlineFieldInstruction">The inline field instruction.</param>
        public override void VisitInlineFieldInstruction(InlineFieldInstruction inlineFieldInstruction)
        {
            string str;
            try
            {
                str = inlineFieldInstruction.Field + "/" + inlineFieldInstruction.Field.DeclaringType;
            }
            catch (Exception exception)
            {
                str = "!" + exception.Message + "!";
            }
            this.collector.Process(inlineFieldInstruction, str);
        }

        /// <summary>
        /// Visits the inline i8 instruction.
        /// </summary>
        /// <param name="inlineI8Instruction">The inline i8 instruction.</param>
        public override void VisitInlineI8Instruction(InlineI8Instruction inlineI8Instruction)
        {
            this.collector.Process(inlineI8Instruction, inlineI8Instruction.Int64.ToString());
        }

        /// <summary>
        /// Visits the inline i instruction.
        /// </summary>
        /// <param name="inlineIInstruction">The inline i instruction.</param>
        public override void VisitInlineIInstruction(InlineIInstruction inlineIInstruction)
        {
            this.collector.Process(inlineIInstruction, inlineIInstruction.Int32.ToString());
        }

        /// <summary>
        /// Visits the inline method instruction.
        /// </summary>
        /// <param name="inlineMethodInstruction">The inline method instruction.</param>
        public override void VisitInlineMethodInstruction(InlineMethodInstruction inlineMethodInstruction)
        {
            string str;
            try
            {
                str = string.Format("{0} {1}", getMode(inlineMethodInstruction.Method.IsStatic), Serialize(inlineMethodInstruction.Method));
            }
            catch (Exception exception)
            {
                str = "!" + exception.Message + "!";
            }
            this.collector.Process(inlineMethodInstruction, str);
        }


        /// <summary>
        /// Visits the inline none instruction.
        /// </summary>
        /// <param name="inlineNoneInstruction">The inline none instruction.</param>
        public override void VisitInlineNoneInstruction(InlineNoneInstruction inlineNoneInstruction)
        {
            this.collector.Process(inlineNoneInstruction, string.Empty);
        }

        /// <summary>
        /// Visits the inline r instruction.
        /// </summary>
        /// <param name="inlineRInstruction">The inline r instruction.</param>
        public override void VisitInlineRInstruction(InlineRInstruction inlineRInstruction)
        {
            this.collector.Process(inlineRInstruction, inlineRInstruction.Double.ToString());
        }

        /// <summary>
        /// Visits the inline sig instruction.
        /// </summary>
        /// <param name="inlineSigInstruction">The inline sig instruction.</param>
        public override void VisitInlineSigInstruction(InlineSigInstruction inlineSigInstruction)
        {
            this.collector.Process(inlineSigInstruction, this.formatProvider.SigByteArrayToString(inlineSigInstruction.Sign));
        }

        /// <summary>
        /// Visits the inline string instruction.
        /// </summary>
        /// <param name="inlineStringInstruction">The inline string instruction.</param>
        public override void VisitInlineStringInstruction(InlineStringInstruction inlineStringInstruction)
        {
            this.collector.Process(inlineStringInstruction, this.formatProvider.EscapedString(inlineStringInstruction.String));
        }

        /// <summary>
        /// Visits the inline switch instruction.
        /// </summary>
        /// <param name="inlineSwitchInstruction">The inline switch instruction.</param>
        public override void VisitInlineSwitchInstruction(InlineSwitchInstruction inlineSwitchInstruction)
        {
            this.collector.Process(inlineSwitchInstruction, this.formatProvider.MultipleLabels(inlineSwitchInstruction.TargetOffsets));
        }

        /// <summary>
        /// Visits the inline tok instruction.
        /// </summary>
        /// <param name="inlineTokInstruction">The inline tok instruction.</param>
        public override void VisitInlineTokInstruction(InlineTokInstruction inlineTokInstruction)
        {
            string str;
            try
            {
                str = inlineTokInstruction.Member + "/" + inlineTokInstruction.Member.DeclaringType;

                //str = string.Format("{0} {1}.{2}", getMode(inlineMethodInstruction.Method.IsStatic), inlineMethodInstruction.Method.DeclaringType.FullName, inlineMethodInstruction.Method);

            }
            catch (Exception exception)
            {
                str = "!" + exception.Message + "!";
            }
            this.collector.Process(inlineTokInstruction, str);
        }

        /// <summary>
        /// Visits the inline type instruction.
        /// </summary>
        /// <param name="inlineTypeInstruction">The inline type instruction.</param>
        public override void VisitInlineTypeInstruction(InlineTypeInstruction inlineTypeInstruction)
        {
            string name;
            try
            {
                name = inlineTypeInstruction.Type.FullName;
            }
            catch (Exception exception)
            {
                name = "!" + exception.Message + "!";
            }
            this.collector.Process(inlineTypeInstruction, name);
        }

        /// <summary>
        /// Visits the inline variable instruction.
        /// </summary>
        /// <param name="inlineVarInstruction">The inline variable instruction.</param>
        public override void VisitInlineVarInstruction(InlineVarInstruction inlineVarInstruction)
        {
            this.collector.Process(inlineVarInstruction, this.formatProvider.Argument(inlineVarInstruction.Ordinal));
        }

        /// <summary>
        /// Visits the short inline br target instruction.
        /// </summary>
        /// <param name="shortInlineBrTargetInstruction">The short inline br target instruction.</param>
        public override void VisitShortInlineBrTargetInstruction(ShortInlineBrTargetInstruction shortInlineBrTargetInstruction)
        {
            this.collector.Process(shortInlineBrTargetInstruction, this.formatProvider.Label(shortInlineBrTargetInstruction.TargetOffset));
        }

        /// <summary>
        /// Visits the short inline i instruction.
        /// </summary>
        /// <param name="shortInlineIInstruction">The short inline i instruction.</param>
        public override void VisitShortInlineIInstruction(ShortInlineIInstruction shortInlineIInstruction)
        {
            this.collector.Process(shortInlineIInstruction, shortInlineIInstruction.Byte.ToString());
        }

        /// <summary>
        /// Visits the short inline r instruction.
        /// </summary>
        /// <param name="shortInlineRInstruction">The short inline r instruction.</param>
        public override void VisitShortInlineRInstruction(ShortInlineRInstruction shortInlineRInstruction)
        {
            this.collector.Process(shortInlineRInstruction, shortInlineRInstruction.Single.ToString());
        }

        /// <summary>
        /// Visits the short inline variable instruction.
        /// </summary>
        /// <param name="shortInlineVarInstruction">The short inline variable instruction.</param>
        public override void VisitShortInlineVarInstruction(ShortInlineVarInstruction shortInlineVarInstruction)
        {
            this.collector.Process(shortInlineVarInstruction, this.formatProvider.Argument(shortInlineVarInstruction.Ordinal));
        }




        private static string getMode(bool isstatic)
        {
            return isstatic ? "static" : "instance";
        }

        private static string Serialize(System.Reflection.MethodBase methodBase)
        {

            StringBuilder st = new StringBuilder();

            st.Append(Serialize(methodBase.DeclaringType));

            st.Append(" ");
            st.Append(methodBase.Name);

            st.Append(SerializeGeneric(methodBase.GetGenericArguments()));
            st.Append("(");

            bool t = false;
            foreach (var item in methodBase.GetParameters())
            {
                if (t)
                    st.Append(", ");
                st.Append(Serialize(item.ParameterType));
                t = true;
            }

            st.Append(")");
            return st.ToString();
        }

        private static string SerializeGeneric(Type[] types)
        {
            StringBuilder st = new StringBuilder();

            if (types.Length > 0)
            {
                st.Append("<");

                bool t = false;
                foreach (var type in types)
                {
                    if (t)
                        st.Append(", ");
                    st.Append(Serialize(type));
                    t = true;
                }
                st.Append(">");
            }

            return st.ToString();
        }

        private static string Serialize(Type type)
        {

            StringBuilder st = new StringBuilder();

            if (type.IsNested)
            {
                st.Append(Serialize(type.DeclaringType));
                st.Append("+");

                st.Append(SerializeTypeName(type.Name));

            }
            else
            {

                if (!string.IsNullOrEmpty(type.Namespace))
                {
                    st.Append(type.Namespace);
                    st.Append(".");
                }

                st.Append(SerializeTypeName(type.Name));

            }
            return st.ToString();

        }

        private static string SerializeTypeName(string p)
        {
            return "<span class=\"simpleType\">" + p + "</span>";
        }


        /// <summary>
        /// The collector
        /// </summary>
        protected IILStringCollector collector;
        /// <summary>
        /// The format provider
        /// </summary>
        protected IFormatProvider formatProvider;


    }
}
