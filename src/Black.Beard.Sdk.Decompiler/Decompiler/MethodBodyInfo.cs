using Bb.Sdk.Decompiler.IlParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Sdk.Exceptions
{

    /// <summary>
    /// MethodBodyInfo
    /// </summary>
    [Serializable]
    public class MethodBodyInfo
    {

        // Fields
        private List<string> m_instructions = new List<string>();

        // Methods
        internal void AddInstruction(string inst)
        {
            this.m_instructions.Add(inst);
        }

        /// <summary>
        /// Creates the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public static MethodBodyInfo Create(MethodBase method, int offset)
        {
            MethodBodyInfo mbi = new MethodBodyInfo
            {
                Identity = method.GetHashCode(),
                TypeName = method.GetType().Name,
                MethodToString = method.ToString()
            };

            ReadableILStringVisitor visitor = new ReadableILStringVisitor(new MethodBodyInfoBuilder(mbi, offset), DefaultFormatProvider.Instance);
            ILReaderFactory.Create(method, offset).Accept(visitor);

            return mbi;
        }

        // Properties
        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        /// <value>
        /// The identity.
        /// </value>
        public int Identity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the instructions.
        /// </summary>
        /// <value>
        /// The instructions.
        /// </value>
        public List<string> Instructions
        {
            get
            {
                return this.m_instructions;
            }
        }

        /// <summary>
        /// Gets or sets the method to string.
        /// </summary>
        /// <value>
        /// The method to string.
        /// </value>
        public string MethodToString
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        /// <value>
        /// The name of the type.
        /// </value>
        public string TypeName
        {
            get
           ;
            set
           ;
        }

        
    }

    // Nested Types
    class MethodBodyInfoBuilder : IILStringCollector
    {

        // Fields
        private MethodBodyInfo m_mbi;
        private int offset;

        // Methods
        public MethodBodyInfoBuilder(MethodBodyInfo mbi, int offset)
        {
            this.m_mbi = mbi;
            this.offset = offset;
        }

        public void Process(ILInstruction instruction, string operandString)
        {

            string txt = string.Empty;

            if (instruction.Offset == offset)
                txt = "<span class=\"inLine\">";

            txt += string.Format("<span class=\"il\">{0:x4}</span>&nbsp;&nbsp;&nbsp; {1,-10} {2}", instruction.Offset, instruction.OpCode.Name, operandString);

            if (instruction.Offset == offset)
                txt += "</span>";

            this.m_mbi.AddInstruction(txt);

        }
    }


}
