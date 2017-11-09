using System;
using System.IO;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ReadableILStringToTextWriter
    /// </summary>
    public class ReadableILStringToTextWriter : IILStringCollector
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadableILStringToTextWriter"/> class.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public ReadableILStringToTextWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Processes the specified il instruction.
        /// </summary>
        /// <param name="ilInstruction">The il instruction.</param>
        /// <param name="operandString">The operand string.</param>
        public virtual void Process(ILInstruction ilInstruction, string operandString)
        {
            this.writer.WriteLine(Constants.ILMask, ilInstruction.Offset, ilInstruction.OpCode.Name, operandString);
        }

        /// <summary>
        /// The writer
        /// </summary>
        protected TextWriter writer;

    }
}
