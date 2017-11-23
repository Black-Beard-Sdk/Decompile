using Bb.Sdk.Decompiler.IlParser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bb.Sdk.Decompiler.Methods
{

    public sealed class MethodBody
    {
        
        readonly internal MethodDefinition method;

        //internal ParameterDefinition this_parameter;
        internal int max_stack_size;
        internal int code_size;
        internal bool init_locals;
        //internal MetadataToken local_var_token;

        internal Collection<ILInstruction> instructions;
        //internal Collection<ExceptionHandler> exceptions;
        //internal Collection<VariableDefinition> variables;

        public MethodDefinition Method { get { return method; } }

        public int MaxStackSize
        {
            get { return max_stack_size; }
            set { max_stack_size = value; }
        }

        public int CodeSize { get { return code_size; } }

        public bool InitLocals
        {
            get { return init_locals; }
            set { init_locals = value; }
        }

        //public MetadataToken LocalVarToken
        //{
        //    get { return local_var_token; }
        //    set { local_var_token = value; }
        //}

        //public Collection<ILInstruction> Instructions
        //{
        //    get { return instructions ?? (instructions = new InstructionCollection(method)); }
        //}

        //public bool HasExceptionHandlers
        //{
        //    get { return !exceptions.IsNullOrEmpty(); }
        //}

        //public Collection<ExceptionHandler> ExceptionHandlers
        //{
        //    get { return exceptions ?? (exceptions = new Collection<ExceptionHandler>()); }
        //}

        //public bool HasVariables
        //{
        //    get { return !variables.IsNullOrEmpty(); }
        //}

        //public Collection<VariableDefinition> Variables
        //{
        //    get { return variables ?? (variables = new VariableDefinitionCollection()); }
        //}

        //public ParameterDefinition ThisParameter
        //{
        //    get
        //    {
        //        if (method == null || method.DeclaringType == null)
        //            throw new NotSupportedException();

        //        if (!method.HasThis)
        //            return null;

        //        if (this_parameter == null)
        //            Interlocked.CompareExchange(ref this_parameter, CreateThisParameter(method), null);

        //        return this_parameter;
        //    }
        //}

        //static ParameterDefinition CreateThisParameter(MethodDefinition method)
        //{
        //    var parameter_type = method.DeclaringType as TypeReference;

        //    if (parameter_type.HasGenericParameters)
        //    {
        //        var instance = new GenericInstanceType(parameter_type);
        //        for (int i = 0; i < parameter_type.GenericParameters.Count; i++)
        //            instance.GenericArguments.Add(parameter_type.GenericParameters[i]);

        //        parameter_type = instance;

        //    }

        //    if (parameter_type.IsValueType || parameter_type.IsPrimitive)
        //        parameter_type = new ByReferenceType(parameter_type);

        //    return new ParameterDefinition(parameter_type, method);
        //}

        public MethodBody(MethodDefinition method)
        {
            this.method = method;
        }

        public ILInstructionReader GetILProcessor()
        {
            return ILReaderFactory.Create(method, 0);
        }

    }


}
