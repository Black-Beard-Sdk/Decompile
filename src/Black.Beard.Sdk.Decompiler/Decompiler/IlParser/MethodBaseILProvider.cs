namespace Bb.Sdk.Decompiler.IlParser
{
    using System;
    using System.Reflection;

    /// <summary>
    /// MethodBaseILProvider
    /// </summary>
    public class MethodBaseILProvider : IILProvider
    {
     
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodBaseILProvider"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        public MethodBaseILProvider(MethodBase method)
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
                MethodBody methodBody = this.m_method.GetMethodBody();
                this.m_byteArray = (methodBody == null) ? new byte[0] : methodBody.GetILAsByteArray();
            }
            return this.m_byteArray;
        }

        private byte[] m_byteArray;
        private MethodBase m_method;


    }
}
