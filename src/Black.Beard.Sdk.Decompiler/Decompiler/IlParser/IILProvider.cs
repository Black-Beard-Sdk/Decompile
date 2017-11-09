using System;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// IILProvider
    /// </summary>
    public interface IILProvider
    {
        /// <summary>
        /// Gets the byte array.
        /// </summary>
        /// <returns></returns>
        byte[] GetByteArray();
    }

}
