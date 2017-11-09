namespace Bb.Sdk.Decompiler.IlParser
{
    using System;

    /// <summary>
    /// IFormatProvider
    /// </summary>
    public interface IFormatProvider
    {

        /// <summary>
        /// Arguments the specified ordinal.
        /// </summary>
        /// <param name="ordinal">The ordinal.</param>
        /// <returns></returns>
        string Argument(int ordinal);

        /// <summary>
        /// Escapeds the string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        string EscapedString(string str);

        /// <summary>
        /// Int16s to hexadecimal.
        /// </summary>
        /// <param name="int16">The int16.</param>
        /// <returns></returns>
        string Int16ToHex(int int16);

        /// <summary>
        /// Int32s to hexadecimal.
        /// </summary>
        /// <param name="int32">The int32.</param>
        /// <returns></returns>
        string Int32ToHex(int int32);

        /// <summary>
        /// Int8s to hexadecimal.
        /// </summary>
        /// <param name="int8">The int8.</param>
        /// <returns></returns>
        string Int8ToHex(int int8);

        /// <summary>
        /// Labels the specified offset.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        string Label(int offset);

        /// <summary>
        /// Multiples the labels.
        /// </summary>
        /// <param name="offsets">The offsets.</param>
        /// <returns></returns>
        string MultipleLabels(int[] offsets);

        /// <summary>
        /// Sigs the byte array to string.
        /// </summary>
        /// <param name="sig">The sig.</param>
        /// <returns></returns>
        string SigByteArrayToString(byte[] sig);

    }
}
