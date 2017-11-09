using System;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// IILStringCollector
    /// </summary>
    public interface IILStringCollector
    {
        /// <summary>
        /// Processes the specified il instruction.
        /// </summary>
        /// <param name="ilInstruction">The il instruction.</param>
        /// <param name="operandString">The operand string.</param>
        void Process(ILInstruction ilInstruction, string operandString);
    }

}
