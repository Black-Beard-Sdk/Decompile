using System;
using System.Reflection;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ModuleScopeTokenResolver
    /// </summary>
    public class ModuleScopeTokenResolver : ITokenResolver
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleScopeTokenResolver"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        public ModuleScopeTokenResolver(MethodBase method)
        {
            this.m_enclosingMethod = method;
            this.m_module = method.Module;
            this.m_methodContext = (method is ConstructorInfo) ? null : method.GetGenericArguments();
            this.m_typeContext = (method.DeclaringType == null) ? null : method.DeclaringType.GetGenericArguments();
        }

        /// <summary>
        /// Ases the field.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public FieldInfo AsField(int token)
        {
            return this.m_module.ResolveField(token, this.m_typeContext, this.m_methodContext);
        }

        /// <summary>
        /// Ases the member.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public MemberInfo AsMember(int token)
        {
            return this.m_module.ResolveMember(token, this.m_typeContext, this.m_methodContext);
        }

        /// <summary>
        /// Ases the method.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public MethodBase AsMethod(int token)
        {
            return this.m_module.ResolveMethod(token, this.m_typeContext, this.m_methodContext);
        }

        /// <summary>
        /// Ases the signature.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public byte[] AsSignature(int token)
        {
            return this.m_module.ResolveSignature(token);
        }

        /// <summary>
        /// Ases the string.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public string AsString(int token)
        {
            return this.m_module.ResolveString(token);
        }

        /// <summary>
        /// Ases the type.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public Type AsType(int token)
        {
            return this.m_module.ResolveType(token, this.m_typeContext, this.m_methodContext);
        }

        private MethodBase m_enclosingMethod;
        private Type[] m_methodContext;
        private Module m_module;
        private Type[] m_typeContext;

    }
}
