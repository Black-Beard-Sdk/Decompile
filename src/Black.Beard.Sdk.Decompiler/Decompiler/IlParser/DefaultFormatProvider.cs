using System;
using System.Text;

namespace Bb.Sdk.Decompiler.IlParser
{    

    /// <summary>
    /// DefaultFormatProvider
    /// </summary>
    public class DefaultFormatProvider : Bb.Sdk.Decompiler.IlParser.IFormatProvider
    {
        /// <summary>
        /// The instance
        /// </summary>
        public static DefaultFormatProvider Instance = new DefaultFormatProvider();

        /// <summary>
        /// Prevents a default instance of the <see cref="DefaultFormatProvider"/> class from being created.
        /// </summary>
        private DefaultFormatProvider()
        {
        }

        /// <summary>
        /// Arguments the specified ordinal.
        /// </summary>
        /// <param name="ordinal">The ordinal.</param>
        /// <returns></returns>
        public virtual string Argument(int ordinal)
        {
            return string.Format(Constants.MaskVariableTxt, ordinal);
        }

        /// <summary>
        /// Escapeds the string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public virtual string EscapedString(string str)
        {
            int length = str.Length;
            StringBuilder sb = new StringBuilder(length * 2);

            sb.Append(Constants.DblQuoteTxt);

            for (int i = 0; i < length; i++)
            {

                char ch = str[i];

                if (ch == Constants.TabChar)
                    sb.Append(Constants.TabChar);

                else if (ch == Constants.NewLineChar)
                    sb.Append(Constants.NewLineChar);

                else if (ch == Constants.RetournCarrierChar)
                    sb.Append(Constants.RetournCarrierChar);

                else if (ch == Constants.DblQuoteChar)
                    sb.Append("\\\"");

                else if (ch == '\\')
                    sb.Append(@"\");

                else if ((ch < Constants.SpaceChar) || (ch >= '\x007f'))
                    sb.AppendFormat(@"\u{0:x4}", (int) ch);

                else
                    sb.Append(ch);

                }

            sb.Append(Constants.DblQuoteTxt);

            return sb.ToString();

        }

        /// <summary>
        /// Int16s to hexadecimal.
        /// </summary>
        /// <param name="int16">The int16.</param>
        /// <returns></returns>
        public virtual string Int16ToHex(int int16)
        {
            return int16.ToString(Constants.ToStringX4);
        }

        /// <summary>
        /// Int32s to hexadecimal.
        /// </summary>
        /// <param name="int32">The int32.</param>
        /// <returns></returns>
        public virtual string Int32ToHex(int int32)
        {
            return int32.ToString(Constants.ToStringX8);
        }

        /// <summary>
        /// Int8s to hexadecimal.
        /// </summary>
        /// <param name="int8">The int8.</param>
        /// <returns></returns>
        public virtual string Int8ToHex(int int8)
        {
            return int8.ToString("X2");
        }

        /// <summary>
        /// Labels the specified offset.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public virtual string Label(int offset)
        {
            return string.Format(Constants.ILMask2, offset);
        }

        /// <summary>
        /// Multiples the labels.
        /// </summary>
        /// <param name="offsets">The offsets.</param>
        /// <returns></returns>
        public virtual string MultipleLabels(int[] offsets)
        {
            StringBuilder builder = new StringBuilder();
            int length = offsets.Length;
            for (int i = 0; i < length; i++)
            {
                if (i == 0)
                {
                    builder.AppendFormat("(", new object[0]);
                }
                else
                {
                    builder.AppendFormat(", ", new object[0]);
                }
                builder.Append(this.Label(offsets[i]));
            }
            builder.AppendFormat(")", new object[0]);
            return builder.ToString();
        }

        /// <summary>
        /// Sigs the byte array to string.
        /// </summary>
        /// <param name="sig">The sig.</param>
        /// <returns></returns>
        public virtual string SigByteArrayToString(byte[] sig)
        {
            StringBuilder builder = new StringBuilder();
            int length = sig.Length;
            for (int i = 0; i < length; i++)
            {

                if (i == 0)
                    builder.Append("SIG [");

                else
                    builder.Append(Constants.SpaceChar);

                builder.Append(this.Int8ToHex(sig[i]));

            }

            builder.Append("]");
            return builder.ToString();
        }
    }
}
