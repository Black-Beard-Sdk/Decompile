using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    internal class DynamicScopeTokenResolver : ITokenResolver
    {

        // Methods
        static DynamicScopeTokenResolver()
        {
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;
            s_indexer = Type.GetType("System.Reflection.Emit.DynamicScope").GetProperty("Item", bindingAttr);
            s_scopeFi = Type.GetType("System.Reflection.Emit.DynamicILGenerator").GetField("m_scope", bindingAttr);
            s_varArgMethodType = Type.GetType("System.Reflection.Emit.VarArgMethod");
            s_varargFi1 = s_varArgMethodType.GetField("m_method", bindingAttr);
            s_varargFi2 = s_varArgMethodType.GetField("m_signature", bindingAttr);
            s_genMethodInfoType = Type.GetType("System.Reflection.Emit.GenericMethodInfo");
            s_genmethFi1 = s_genMethodInfoType.GetField("m_method", bindingAttr);
            s_genmethFi2 = s_genMethodInfoType.GetField("m_context", bindingAttr);
            s_genFieldInfoType = Type.GetType("System.Reflection.Emit.GenericFieldInfo", false);
            if (s_genFieldInfoType != null)
            {
                s_genfieldFi1 = s_genFieldInfoType.GetField("m_field", bindingAttr);
                s_genfieldFi2 = s_genFieldInfoType.GetField("m_context", bindingAttr);
            }
            else
            {
                s_genfieldFi1 = (FieldInfo)(s_genfieldFi2 = null);
            }
        }

        public DynamicScopeTokenResolver(DynamicMethod dm)
        {
            this.m_scope = s_scopeFi.GetValue(dm.GetILGenerator());
        }

        public FieldInfo AsField(int token)
        {
            if (this[token] is RuntimeFieldHandle)
            {
                return FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)this[token]);
            }
            if (this[token].GetType() == s_genFieldInfoType)
            {
                return FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)s_genfieldFi1.GetValue(this[token]), (RuntimeTypeHandle)s_genfieldFi2.GetValue(this[token]));
            }
            Debug.Assert(false, string.Format("unexpected type: {0}", this[token].GetType()));
            return null;
        }

        public MemberInfo AsMember(int token)
        {
            if ((token & 0x2000000) == 0x2000000)
            {
                return this.AsType(token);
            }
            if ((token & 0x6000000) == 0x6000000)
            {
                return this.AsMethod(token);
            }
            if ((token & 0x4000000) == 0x4000000)
            {
                return this.AsField(token);
            }
            Debug.Assert(false, string.Format("unexpected token type: {0:x8}", token));
            return null;
        }

        public MethodBase AsMethod(int token)
        {
            if (this[token] is DynamicMethod)
            {
                return (this[token] as DynamicMethod);
            }
            if (this[token] is RuntimeMethodHandle)
            {
                return MethodBase.GetMethodFromHandle((RuntimeMethodHandle)this[token]);
            }
            if (this[token].GetType() == s_genMethodInfoType)
            {
                return MethodBase.GetMethodFromHandle((RuntimeMethodHandle)s_genmethFi1.GetValue(this[token]), (RuntimeTypeHandle)s_genmethFi2.GetValue(this[token]));
            }
            if (this[token].GetType() == s_varArgMethodType)
            {
                return (MethodInfo)s_varargFi1.GetValue(this[token]);
            }
            Debug.Assert(false, string.Format("unexpected type: {0}", this[token].GetType()));
            return null;
        }

        public byte[] AsSignature(int token)
        {
            return (this[token] as byte[]);
        }

        public string AsString(int token)
        {
            return (this[token] as string);
        }

        public Type AsType(int token)
        {
            return Type.GetTypeFromHandle((RuntimeTypeHandle)this[token]);
        }

        // Properties
        internal object this[int token]
        {
            get
            {
                return s_indexer.GetValue(this.m_scope, new object[] { token });
            }
        }


        // Fields
        private object m_scope = null;
        private static FieldInfo s_genfieldFi1;
        private static FieldInfo s_genfieldFi2;
        private static Type s_genFieldInfoType;
        private static FieldInfo s_genmethFi1;
        private static FieldInfo s_genmethFi2;
        private static Type s_genMethodInfoType;
        private static PropertyInfo s_indexer;
        private static FieldInfo s_scopeFi;
        private static FieldInfo s_varargFi1;
        private static FieldInfo s_varargFi2;
        private static Type s_varArgMethodType;

    }


}
