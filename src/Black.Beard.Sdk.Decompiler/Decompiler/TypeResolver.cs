using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Sdk.Decompiler.Exceptions
{
    
    static class TypeResolver
    {


        public static MethodDefinition Resolve(this MethodBase self)
        {

            var m = (self as MethodInfo);

            if (m == null)
                return null;

            var type = m.DeclaringType.Resolve();

            if (type == null)
                return null;

            var method = type.Methods.FirstOrDefault(c => c.Name == m.Name && MatchParameter(c.Parameters, m.GetParameters) && MatchGenericParameters(c.GenericParameters, m.GetGenericArguments()));

            return method;

        }

        private static bool MatchParameter(Collections.Generic.Collection<ParameterDefinition> collection, Func<ParameterInfo[]> func)
        {
            return true;
        }

        private static bool MatchGenericParameters(Collections.Generic.Collection<GenericParameter> collection, Type[] type)
        {
            return true;
        }

        public static MethodDefinition Create(this MethodBase self)
        {

            var m = (self as MethodInfo);

            if (m == null)
                return null;

            var type = m.ReturnType;

            var attributes = (Bb.Sdk.Introspections.MethodAttributes)(int)self.Attributes;

            var method = new MethodDefinition(self.Name, attributes, m.ReturnType.Resolve())
            {
                DeclaringType = m.DeclaringType.Resolve(),
                // GenericParameters = new Collections.Generic.Collection<GenericParameter>(),
                // Parameters = new Collections.Generic.Collection<ParameterDefinition>(),
            }
                ;

            return method;

        }

        public static TypeDefinition Resolve(this Type self)
        {

            if (self == null)
                return null;

            var assembly = self.Assembly.Resolve();
            var types = assembly.MainModule.GetTypes();
            
            TypeDefinition returnType = types.FirstOrDefault(c => c.Namespace == self.Namespace && c.Name == self.Name);

            return returnType;

        }

        public static AssemblyDefinition Resolve(this Assembly self)
        {

            var assembly = self;
            var filename = assembly.Location;
            var _assembly = Bb.Sdk.Introspections.AssemblyDefinition.ReadAssembly(filename);

            return _assembly;

        }

    }

}
