namespace Bb.Sdk.Decompiler.IlParser
{
    using System;
    using System.Reflection;

    /// <summary>
    /// ITokenResolver
    /// </summary>
    public interface ITokenResolver
    {
        /// <summary>
        /// Ases the field.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        FieldInfo AsField(int token);

        /// <summary>
        /// Ases the member.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        MemberInfo AsMember(int token);

        /// <summary>
        /// Ases the method.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        MethodBase AsMethod(int token);

        /// <summary>
        /// Ases the signature.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        byte[] AsSign(int token);

        /// <summary>
        /// Ases the string.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        string AsString(int token);

        /// <summary>
        /// Ases the type.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Type AsType(int token);

    }
}
