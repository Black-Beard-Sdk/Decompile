using System.IO;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// RawILStringToTextWriter
    /// </summary>
    public class RawILStringToTextWriter : ReadableILStringToTextWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawILStringToTextWriter"/> class.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public RawILStringToTextWriter(TextWriter writer) : base(writer)
        {
        }

        /// <summary>
        /// Processes the specified il instruction.
        /// </summary>
        /// <param name="ilInstruction">The il instruction.</param>
        /// <param name="operandString">The operand string.</param>
        public override void Process(ILInstruction ilInstruction, string operandString)
        {
            base.writer.WriteLine("IL_{0:x4}: {1,-4}| {2, -8}", ilInstruction.Offset, ilInstruction.OpCode.Value.ToString("x2"), operandString);
        }
    }
}
