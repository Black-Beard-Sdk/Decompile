using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ILReaderFactory
    /// </summary>
    public class ILReaderFactory
    {
        
        static ILReaderFactory()
        {
            s_dynamicMethodType = typeof(System.Reflection.Emit.DynamicMethod);  // Type.GetType("System.Reflection.Emit.DynamicMethod");
            s_rtDynamicMethodType = Type.GetType("System.Reflection.Emit.DynamicMethod+RTDynamicMethod");
            s_runtimeConstructorInfoType = Type.GetType("System.Reflection.RuntimeConstructorInfo");
            s_runtimeMethodInfoType = Type.GetType("System.Reflection.RuntimeMethodInfo");
            s_fiOwner = s_rtDynamicMethodType.GetField("m_owner", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        // Methods
        /// <summary>
        /// Creates the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException"></exception>
        public static ILInstructionReader Create(object obj, int offset)
        {

            Type type = obj.GetType();
            if ((type == s_dynamicMethodType) || (type == s_rtDynamicMethodType))
            {
                DynamicMethod method;
                if (type == s_rtDynamicMethodType)
                    method = (DynamicMethod)s_fiOwner.GetValue(obj);
                else
                    method = obj as DynamicMethod;
                return new ILInstructionReader(new DynamicMethodILProvider(method), new DynamicScopeTokenResolver(method));
            }

            if ((type != s_runtimeMethodInfoType) && (type != s_runtimeConstructorInfoType))
                throw new NotSupportedException(string.Format("Reading IL from type {0} is currently not supported", type));

            var ilReader = new ILInstructionReader(obj as MethodBase);

            ilReader.ReadIl();

            return ilReader;

        }


        // Fields
        private static Type s_dynamicMethodType;
        private static FieldInfo s_fiOwner;
        private static Type s_rtDynamicMethodType;
        private static Type s_runtimeConstructorInfoType;
        private static Type s_runtimeMethodInfoType;


    }



}
