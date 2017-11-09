using System;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// DynamicMethodILProvider
    /// </summary>
    public class DynamicMethodILProvider : IILProvider
    {

        // Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicMethodILProvider"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        public DynamicMethodILProvider(DynamicMethod method)
        {
            this.m_method = method;
        }

        /// <summary>
        /// Gets the byte array.
        /// </summary>
        /// <returns></returns>
        public byte[] GetByteArray()
        {
            if (this.m_byteArray == null)
            {
                ILGenerator iLGenerator = this.m_method.GetILGenerator();
                try
                {
                    this.m_byteArray = (byte[])s_miBakeByteArray.Invoke(iLGenerator, null);
                    if (this.m_byteArray == null)
                    {
                        this.m_byteArray = new byte[0];
                    }
                }
                catch (TargetInvocationException)
                {
                    int length = (int)s_fiLen.GetValue(iLGenerator);
                    this.m_byteArray = new byte[length];
                    Array.Copy((byte[])s_fiStream.GetValue(iLGenerator), this.m_byteArray, length);
                }
            }
            return this.m_byteArray;
        }

        // Fields
        private byte[] m_byteArray;
        private DynamicMethod m_method;
        private static FieldInfo s_fiLen = typeof(ILGenerator).GetField("m_length", BindingFlags.NonPublic | BindingFlags.Instance);
        private static FieldInfo s_fiStream = typeof(ILGenerator).GetField("m_ILStream", BindingFlags.NonPublic | BindingFlags.Instance);
        private static MethodInfo s_miBakeByteArray = typeof(ILGenerator).GetMethod("BakeByteArray", BindingFlags.NonPublic | BindingFlags.Instance);

    }


}
