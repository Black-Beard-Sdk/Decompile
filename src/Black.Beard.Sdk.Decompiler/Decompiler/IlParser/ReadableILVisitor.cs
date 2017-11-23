using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;


namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ReadableILStringVisitor
    /// </summary>
    public class ReadableILVisitor : AbstractILInstructionVisitor
    {


        static ReadableILVisitor()
        {

            ReadableILVisitor._type_GetTypeFromHandle = new Func<RuntimeTypeHandle, Type>(Type.GetTypeFromHandle).Method;
            ReadableILVisitor._type_CreateDecimal = typeof(System.Decimal).GetConstructor(new Type[] { typeof(UInt32) });
            ReadableILVisitor._type_CreateDecimal2 = typeof(System.Decimal).GetConstructor(new Type[] { typeof(Int32), typeof(Int32), typeof(Int32), typeof(bool), typeof(Byte) });


            // System.Decimal..ctor(Int32, Int32, Int32, Boolean, Byte)
            // typeof(System.Decimal).GetConstructor(new Type[] {typeof(UInt32) });


        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadableILStringVisitor"/> class.
        /// </summary>
        /// <param name="collector">The collector.</param>
        public ReadableILVisitor()
        {
            this._stack = new Stack<ILInstruction>();
            this._stack2 = new List<Marker>();
        }

        public CodeMemberMethod GetMethod()
        {


            var isPublic = (this.Method.Attributes & MethodAttributes.Public) == MethodAttributes.Public;
            var isStatic = (this.Method.Attributes & MethodAttributes.Static) == MethodAttributes.Static;
            MemberAttributes attributes = isPublic ? MemberAttributes.Public : MemberAttributes.Private;

            if (isPublic)
                attributes = attributes | MemberAttributes.Static;

            var m = new CodeMemberMethod(0)
            {
                Name = base.Method.Name,
                Attributes = attributes,
            };


            if (this.Method.IsGenericMethod)
            {
                Stop();
            }

            #region build code statment

            List<CodeStatement> _items = new List<CodeStatement>();
            while (this._stack.Count > 0)
            {

                var i = Pop();
                var e = i.Expression;

                if (e != null)
                {
                    if (e is CodeStatement st)
                    {
                        if (e is CodeMethodReturnStatement r && r.Expression == null)
                        {
                            if (_items.Count > 0)
                                _items.Insert(0, st);
                        }
                        else
                            _items.Insert(0, st);
                    }

                    else if (e is CodeExpression ex)
                        _items.Insert(0, new CodeExpressionStatement(i, ex));

                    else
                    {
                        Stop();
                    }

                }
                else
                    if (i.OpCode != System.Reflection.Emit.OpCodes.Nop)
                    Stop();
            }
            m.Statements.AddRange(_items.ToArray());

            #endregion build code statment


            foreach (var p in base.Method.GetParameters())
            {
                var _p = new CodeParameterDeclarationExpression(null, new CodeTypeReference(p.ParameterType), p.Name);
                m.Parameters.Add(_p);
            }

            return m;

        }

        /// <summary>
        /// Visits the inline br target instruction.
        /// </summary>
        /// <param name="inlineBrTargetInstruction">The inline br target instruction.</param>
        public override void VisitInlineBrTargetInstruction(InlineBrTargetInstruction inline)
        {

            TestMarker(inline);

            ILInstruction il;

            switch (inline.OpCode)
            {
                //case OpCode c when c == System.Reflection.Emit.OpCodes.Call:

                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        /// <summary>
        /// Visits the inline field instruction.
        /// </summary>
        /// <param name="inlineFieldInstruction">The inline field instruction.</param>
        public override void VisitInlineFieldInstruction(InlineFieldInstruction inline)
        {
            TestMarker(inline);
            ILInstruction il;

            switch (inline.OpCode)
            {
                //case OpCode c when c == System.Reflection.Emit.OpCodes.Call:

                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        /// <summary>
        /// Visits the inline i8 instruction.
        /// </summary>
        /// <param name="inlineI8Instruction">The inline i8 instruction.</param>
        public override void VisitInlineI8Instruction(InlineI8Instruction inline)
        {
            TestMarker(inline);
            ILInstruction il;

            switch (inline.OpCode)
            {
                //case OpCode c when c == System.Reflection.Emit.OpCodes.Call:

                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        /// <summary>
        /// Visits the inline i instruction.
        /// </summary>
        /// <param name="inlineIInstruction">The inline i instruction.</param>
        public override void VisitInlineIInstruction(InlineIInstruction inline)
        {
            TestMarker(inline);
            inline.Expression = new CodePrimitiveExpression(inline, inline.Int32);
            Stack(inline);
        }


        /// <summary>
        /// Visits the inline method instruction.
        /// </summary>
        /// <param name="inlineMethodInstruction">The inline method instruction.</param>
        public override void VisitInlineMethodInstruction(InlineMethodInstruction inline)
        {
            TestMarker(inline);

            ILInstruction il;
            CodeExpression il2;
            var isStatic = (inline.Method.Attributes & MethodAttributes.Static) == MethodAttributes.Static;
            var isCtor = inline.Method is ConstructorInfo;
            CodeExpression[] arguments = getArguments(inline);

            switch (inline.OpCode)
            {

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldftn:
                    il = Pop();
                    il2 = il.Expression as CodeExpression;
                    Debug.Assert(il.Expression == null || il2 != null);
                    inline.Expression = new CodeMethodReferenceExpression(inline, il2, inline.Method.Name);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Callvirt:
                    il = Pop();
                    il2 = il.Expression as CodeExpression;
                    Debug.Assert(il.Expression == null || il2 != null);
                    inline.Expression = new CodeMethodInvokeExpression(inline, new CodeMethodReferenceExpression(inline, il2, inline.Method.Name), arguments);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Call:

                    if (!Resolve(inline, arguments))
                    {
                        if (isStatic)
                        {
                            il2 = new CodeTypeReferenceExpression(inline, new CodeTypeReference(inline.Method.DeclaringType));
                            inline.Expression = new CodeMethodInvokeExpression(inline, new CodeMethodReferenceExpression(inline, il2, inline.Method.Name), arguments);
                        }
                        else
                        {

                            il = Pop();
                            il2 = il.Expression as CodeExpression;
                            Debug.Assert(il.Expression == null || il2 != null);

                            if (isCtor)
                            {
                                inline.Expression = new CodeObjectCreateExpression(inline, new CodeTypeReference(inline.Method.DeclaringType), arguments);
                            }
                            else
                            {
                                inline.Expression = new CodeMethodInvokeExpression(inline, new CodeMethodReferenceExpression(inline, il2, inline.Method.Name), arguments);
                            }

                            if (il.OpCode == System.Reflection.Emit.OpCodes.Ldloca_S)
                                inline.Expression = new CodeAssignStatement(inline, il.Expression as CodeExpression, inline.Expression as CodeExpression);

                        }


                    }
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Newobj:
                    inline.Expression = new CodeObjectCreateExpression(inline, new CodeTypeReference(inline.Method.DeclaringType), arguments);
                    break;

                default:
                    Stop();
                    break;
            }


            Stack(inline);
            //Stop();

        }

        private bool Resolve(InlineMethodInstruction inline, CodeExpression[] arguments)
        {

            ILInstruction il;

            if (inline.Method == ReadableILVisitor._type_CreateDecimal)
            {
                var value = ((CodePrimitiveExpression)arguments[0]).Value;
                var d = new Decimal((int)value);
                inline.Expression = new CodePrimitiveExpression(inline, d);
            }

            else if (inline.Method == ReadableILVisitor._type_CreateDecimal2)
            {
                int lo = (Int32)((CodePrimitiveExpression)arguments[0]).Value;
                int mid = (Int32)((CodePrimitiveExpression)arguments[1]).Value;
                int hi = (Int32)((CodePrimitiveExpression)arguments[2]).Value;
                bool isNegative = ((Int32)((CodePrimitiveExpression)arguments[3]).Value) == 0 ? false : true;
                byte scale = (byte)(Int32)((CodePrimitiveExpression)arguments[4]).Value;
                var d = new Decimal(lo, mid, hi, isNegative, scale);
                inline.Expression = new CodePrimitiveExpression(inline, d);
            }

            else if (inline.Method == ReadableILVisitor._type_GetTypeFromHandle)
                inline.Expression = new CodeTypeOfExpression(inline, (arguments[0] as CodeTypeReferenceExpression).Type);

            if (inline.Expression != null && Peek().OpCode == System.Reflection.Emit.OpCodes.Ldloca_S)
            {
                il = Pop();
                inline.Expression = new CodeAssignStatement(inline, il.Expression as CodeExpression, inline.Expression as CodeExpression);
            }

            return inline.Expression != null;

        }

        public override void VisitInlineStLocInstruction(InlineStLocInstruction inline)
        {

            TestMarker(inline);
            ILInstruction il;
            CodeExpression i;

            switch (inline.OpCode)
            {

                case OpCode c when c == System.Reflection.Emit.OpCodes.Stloc_0:
                    il = Pop();
                    i = il.Expression as CodeExpression;
                    if (i != null)
                        inline.Expression = new CodeAssignStatement(inline, new CodeVariableReferenceExpression(inline, "var0"), i);
                    else
                    {
                        if (il.Expression is CodeStatement s)
                        {
                            if (s is CodeAssignStatement a)
                                inline.Expression = new CodeAssignStatement(inline, new CodeVariableReferenceExpression(inline, "var0"), a.Left);

                            else
                                Stop();
                        }
                        else
                            Stop();
                    }
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Stloc_1:
                    il = Pop();
                    i = il.Expression as CodeExpression;
                    Debug.Assert(i != null);
                    inline.Expression = new CodeAssignStatement(inline, new CodeVariableReferenceExpression(inline, "var1"), i);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Stloc_2:
                    il = Pop();
                    i = il.Expression as CodeExpression;
                    Debug.Assert(i != null);
                    inline.Expression = new CodeAssignStatement(inline, new CodeVariableReferenceExpression(inline, "var2"), i);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Stloc_3:
                    il = Pop();
                    i = il.Expression as CodeExpression;
                    Debug.Assert(i != null);
                    inline.Expression = new CodeAssignStatement(inline, new CodeVariableReferenceExpression(inline, "var3"), i);
                    break;

                default:
                    Stop();
                    break;

            }

            Stack(inline);

        }

        public override void VisitInlineLdElemInstruction(InlineLdElemInstruction inline)
        {

            TestMarker(inline);
            ILInstruction il;
            CodeExpression i;
            switch (inline.OpCode)
            {
                default:
                    Stop();
                    break;
            }
            Stack(inline);

        }

        public override void VisitInlineStElemInstruction(InlineStElemInstruction inline)
        {

            TestMarker(inline);
            ILInstruction il;
            CodeExpression i;
            switch (inline.OpCode)
            {
                default:
                    Stop();
                    break;
            }
            Stack(inline);

        }

        /// <summary>
        /// Inlines the ld loc instruction.
        /// </summary>
        /// <param name="inline">The inline.</param>
        public override void VisitInlineLdLocInstruction(InlineLdLocInstruction inline)
        {

            TestMarker(inline);

            switch (inline.OpCode)
            {
                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldloc_0:
                    inline.Expression = new CodeVariableReferenceExpression(inline, "var0");
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldloc_1:
                    inline.Expression = new CodeVariableReferenceExpression(inline, "var1");
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldloc_2:
                    inline.Expression = new CodeVariableReferenceExpression(inline, "var2");
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldloc_3:
                    inline.Expression = new CodeVariableReferenceExpression(inline, "var3");
                    break;

                default:
                    Stop();
                    break;

            }

            Stack(inline);

        }

        /// <summary>
        /// Visits the inline ld i4 s instruction.
        /// </summary>
        /// <param name="inline">The inline.</param>
        public override void VisitInlineLdI4Instruction(InlineLdI4Instruction inline)
        {

            TestMarker(inline);

            switch (inline.OpCode)
            {


                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldc_I4_0:
                    inline.Expression = new CodePrimitiveExpression(inline, 0);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldc_I4_1:
                    inline.Expression = new CodePrimitiveExpression(inline, 1);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldc_I4_2:
                    inline.Expression = new CodePrimitiveExpression(inline, 2);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldc_I4_3:
                    inline.Expression = new CodePrimitiveExpression(inline, 3);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldc_I4_4:
                    inline.Expression = new CodePrimitiveExpression(inline, 4);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldc_I4_5:
                    inline.Expression = new CodePrimitiveExpression(inline, 5);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldc_I4_6:
                    inline.Expression = new CodePrimitiveExpression(inline, 6);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldc_I4_7:
                    inline.Expression = new CodePrimitiveExpression(inline, 7);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldc_I4_8:
                    inline.Expression = new CodePrimitiveExpression(inline, 8);
                    break;

                default:
                    Stop();
                    break;

            }

            Stack(inline);

        }

        /// <summary>
        /// Visits the inline operator loc instruction.
        /// </summary>
        /// <param name="inline">The inline operator loc instruction.</param>
        public override void VisitInlineOperatorInstruction(InlineOperatorInstruction inline)
        {

            TestMarker(inline);

            switch (inline.OpCode)
            {
                case OpCode c when c == System.Reflection.Emit.OpCodes.Add:
                    inline.Expression = new CodeBinaryOperatorExpression(inline
                        , Pop().Expression as CodeExpression
                        , CodeBinaryOperatorType.Add
                        , Pop().Expression as CodeExpression);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Sub:
                    inline.Expression = new CodeBinaryOperatorExpression(inline
                        , Pop().Expression as CodeExpression
                        , CodeBinaryOperatorType.Subtract
                        , Pop().Expression as CodeExpression);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Div:
                    inline.Expression = new CodeBinaryOperatorExpression(inline
                        , Pop().Expression as CodeExpression
                        , CodeBinaryOperatorType.Divide
                        , Pop().Expression as CodeExpression);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Mul:
                    inline.Expression = new CodeBinaryOperatorExpression(inline
                        , Pop().Expression as CodeExpression
                        , CodeBinaryOperatorType.Multiply
                        , Pop().Expression as CodeExpression);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Rem:
                    inline.Expression = new CodeBinaryOperatorExpression(inline
                        , Pop().Expression as CodeExpression
                        , CodeBinaryOperatorType.Modulus
                        , Pop().Expression as CodeExpression);
                    break;

                default:
                    Stop();
                    break;
            }

            Stack(inline);

        }

        /// <summary>
        /// Visits the inline none instruction.
        /// </summary>
        /// <param name="inlineNoneInstruction">The inline none instruction.</param>
        public override void VisitInlineNoneInstruction(InlineNoneInstruction inline)
        {
            TestMarker(inline);

            ILInstruction il;

            switch (inline.OpCode)
            {

                case OpCode c when c == System.Reflection.Emit.OpCodes.Nop:
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ret:

                    if ((this.Method as MethodInfo).ReturnType == typeof(void))
                        inline.Expression = new CodeMethodReturnStatement(inline);

                    else
                    {
                        il = Pop();
                        Debug.Assert(il.Expression != null);
                        inline.Expression = new CodeMethodReturnStatement(inline, il.Expression as CodeExpression);
                    }
                    break;

                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        public override void VisitInlineLdArgInstruction(InlineLdArgInstruction inline)
        {

            TestMarker(inline);

            ILInstruction il;
            ILInstruction il2;
            CodeExpression i;

            switch (inline.OpCode)
            {
                default:
                    Stop();
                    break;
            }
            Stack(inline);


        }

        public override void VisitInlineConditionInstruction(InlineConditionInstruction inline)
        {


            TestMarker(inline);

            ILInstruction il;
            ILInstruction il2;
            CodeExpression i;

            switch (inline.OpCode)
            {

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ceq:
                    il = Pop();
                    il2 = Pop();
                    if (il2.Expression is CodeBinaryOperatorExpression c2 && il.Expression != null && il.Expression is CodePrimitiveExpression c3 && c3.Value != null && c3.Value.Equals(0))
                    {
                        switch (c2.Operator)
                        {
                            //case CodeBinaryOperatorType.Add:
                            //    break;
                            //case CodeBinaryOperatorType.Subtract:
                            //    break;
                            //case CodeBinaryOperatorType.Multiply:
                            //    break;
                            //case CodeBinaryOperatorType.Divide:
                            //    break;
                            //case CodeBinaryOperatorType.Modulus:
                            //    break;
                            //case CodeBinaryOperatorType.Assign:
                            //    break;
                            //case CodeBinaryOperatorType.IdentityInequality:
                            //    break;
                            //case CodeBinaryOperatorType.IdentityEquality:
                            //    break;
                            //case CodeBinaryOperatorType.ValueEquality:
                            //    break;
                            //case CodeBinaryOperatorType.BitwiseOr:
                            //    break;
                            //case CodeBinaryOperatorType.BitwiseAnd:
                            //    break;
                            //case CodeBinaryOperatorType.BooleanOr:
                            //    break;
                            //case CodeBinaryOperatorType.BooleanAnd:
                            //    break;
                            case CodeBinaryOperatorType.LessThan:
                                c2.Operator = CodeBinaryOperatorType.GreaterThanOrEqual;
                                break;
                            case CodeBinaryOperatorType.GreaterThan:
                                c2.Operator = CodeBinaryOperatorType.LessThanOrEqual;
                                break;

                            //case CodeBinaryOperatorType.LessThanOrEqual:
                            //case CodeBinaryOperatorType.GreaterThanOrEqual:
                            //    break;
                            default:
                                Stop();
                                break;
                        }
                        inline.Expression = il2.Expression;
                    }
                    else
                    {
                        inline.Expression = new CodeBinaryOperatorExpression
                            (inline, il2.Expression as CodeExpression
                            , CodeBinaryOperatorType.ValueEquality
                            , il.Expression as CodeExpression
                        );
                    }
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Cgt:
                    il = Pop();
                    il2 = Pop();
                    inline.Expression = new CodeBinaryOperatorExpression
                        (inline, il2.Expression as CodeExpression
                        , CodeBinaryOperatorType.GreaterThan
                        , il.Expression as CodeExpression
                    );
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Clt:
                    il = Pop();
                    il2 = Pop();
                    inline.Expression = new CodeBinaryOperatorExpression
                        (inline, il2.Expression as CodeExpression
                        , CodeBinaryOperatorType.LessThan
                        , il.Expression as CodeExpression
                    );
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.And:
                    il = Pop();
                    il2 = Pop();
                    inline.Expression = new CodeBinaryOperatorExpression
                        (inline, il2.Expression as CodeExpression
                        , CodeBinaryOperatorType.BitwiseAnd
                        , il.Expression as CodeExpression
                    );
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Or:
                    il = Pop();
                    il2 = Pop();
                    inline.Expression = new CodeBinaryOperatorExpression
                        (inline, il2.Expression as CodeExpression
                        , CodeBinaryOperatorType.BitwiseOr
                        , il.Expression as CodeExpression
                    );
                    break;

                default:
                    Stop();
                    break;

            }

            Stack(inline);

        }

        public override void VisitInlineConvertionInstruction(InlineConvertionInstruction inline)
        {

            TestMarker(inline);

            ILInstruction il;
            CodeExpression i;

            switch (inline.OpCode)
            {

                case OpCode c when c == System.Reflection.Emit.OpCodes.Conv_I2:
                    il = Pop();
                    Debug.Assert(il.Expression != null);
                    i = il.Expression as CodeExpression;
                    inline.Expression = new CodeCastExpression(inline, new CodeTypeReference(typeof(Int16)), i);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Conv_I4:
                    il = Pop();
                    Debug.Assert(il.Expression != null);
                    i = il.Expression as CodeExpression;
                    inline.Expression = new CodeCastExpression(inline, new CodeTypeReference(typeof(Int32)), i);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Conv_U8:
                case OpCode c1 when c1 == System.Reflection.Emit.OpCodes.Conv_I8:
                    il = Pop();
                    Debug.Assert(il.Expression != null);
                    i = il.Expression as CodeExpression;
                    inline.Expression = new CodeCastExpression(inline, new CodeTypeReference(typeof(Int64)), i);
                    break;

            }

            Stack(inline);

        }
        /// <summary>
        /// Visits the inline r instruction.
        /// </summary>
        /// <param name="inlineRInstruction">The inline r instruction.</param>
        public override void VisitInlineRInstruction(InlineRInstruction inline)
        {

            TestMarker(inline);

            switch (inline.OpCode)
            {
                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldc_R8:
                    inline.Expression = new CodePrimitiveExpression(inline, inline.Double);
                    break;

                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        /// <summary>
        /// Visits the inline sig instruction.
        /// </summary>
        /// <param name="inlineSigInstruction">The inline sig instruction.</param>
        public override void VisitInlineSigInstruction(InlineSigInstruction inline)
        {
            TestMarker(inline);
            ILInstruction il;

            switch (inline.OpCode)
            {
                //case OpCode c when c == System.Reflection.Emit.OpCodes.Call:

                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        /// <summary>
        /// Visits the inline string instruction.
        /// </summary>
        /// <param name="inlineStringInstruction">The inline string instruction.</param>
        public override void VisitInlineStringInstruction(InlineStringInstruction inline)
        {
            TestMarker(inline);
            inline.Expression = new CodePrimitiveExpression(inline, inline.String);
            Stack(inline);
        }

        /// <summary>
        /// Visits the inline switch instruction.
        /// </summary>
        /// <param name="inlineSwitchInstruction">The inline switch instruction.</param>
        public override void VisitInlineSwitchInstruction(InlineSwitchInstruction inline)
        {
            TestMarker(inline);
            ILInstruction il;

            switch (inline.OpCode)
            {
                //case OpCode c when c == System.Reflection.Emit.OpCodes.Call:

                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        /// <summary>
        /// Visits the inline tok instruction.
        /// </summary>
        /// <param name="inlineTokInstruction">The inline tok instruction.</param>
        public override void VisitInlineTokInstruction(InlineTokInstruction inline)
        {
            TestMarker(inline);

            if (inline.Member is Type t)
                inline.Expression = new CodeTypeReferenceExpression(inline, new CodeTypeReference(t));

            else
                Stop();

            Stack(inline);
        }

        /// <summary>
        /// Visits the inline type instruction.
        /// </summary>
        /// <param name="inlineTypeInstruction">The inline type instruction.</param>
        public override void VisitInlineTypeInstruction(InlineTypeInstruction inline)
        {
            TestMarker(inline);
            ILInstruction il;

            switch (inline.OpCode)
            {
                case OpCode c when c == System.Reflection.Emit.OpCodes.Box:
                    il = Pop();
                    var i = il.Expression as CodeExpression;
                    Debug.Assert(i != null);
                    inline.Expression = new CodeCastExpression(inline, new CodeTypeReference(inline.Type), i);
                    break;

                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        /// <summary>
        /// Visits the inline variable instruction.
        /// </summary>
        /// <param name="inlineVarInstruction">The inline variable instruction.</param>
        public override void VisitInlineVarInstruction(InlineVarInstruction inline)
        {
            TestMarker(inline);
            ILInstruction il;

            switch (inline.OpCode)
            {
                //case OpCode c when c == System.Reflection.Emit.OpCodes.Call:

                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        /// <summary>
        /// Visits the short inline br target instruction.
        /// </summary>
        /// <param name="shortInlineBrTargetInstruction">The short inline br target instruction.</param>
        public override void VisitShortInlineBrTargetInstruction(ShortInlineBrTargetInstruction inline)
        {
            TestMarker(inline);
            ILInstruction il;
            Marker m;
            CodeConditionStatement condition;

            switch (inline.OpCode)
            {
                case OpCode c when c == System.Reflection.Emit.OpCodes.Brtrue_S:
                    condition = new CodeConditionStatement(inline, Pop().Expression as CodeExpression);
                    inline.Expression = condition;
                    m = AddMarker(inline, inline.TargetOffset, blockType.IfTrue);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Brfalse_S:
                    condition = new CodeConditionStatement(inline, Pop().Expression as CodeExpression);
                    inline.Expression = condition;
                    m = AddMarker(inline, inline.TargetOffset, blockType.IfTrue);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Br_S:
                    Marker _m;
                    if (LastInstructionBeforeFalseBlock(inline, out _m))
                        m = AddMarker(_m.Inline, inline.TargetOffset, blockType.IfFalse);
                    else
                        m = AddMarker(inline, inline.TargetOffset, blockType.Undefined);
                    inline.Expression = new CodeGotoStatement(inline, m);
                    break;

                case OpCode c when c == System.Reflection.Emit.OpCodes.Bne_Un_S:

                    break;

                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        /// <summary>
        /// Visits the short inline i instruction.
        /// </summary>
        /// <param name="shortInlineIInstruction">The short inline i instruction.</param>
        public override void VisitShortInlineIInstruction(ShortInlineIInstruction inline)
        {
            TestMarker(inline);

            switch (inline.OpCode)
            {
                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldc_I4_S:
                    inline.Expression = new CodePrimitiveExpression(inline, inline.Byte);
                    break;

                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        /// <summary>
        /// Visits the short inline r instruction.
        /// </summary>
        /// <param name="inlineRInstruction">The short inline r instruction.</param>
        public override void VisitShortInlineRInstruction(ShortInlineRInstruction inline)
        {

            TestMarker(inline);
            ILInstruction il;

            switch (inline.OpCode)
            {
                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldc_R4:
                    inline.Expression = new CodePrimitiveExpression(inline, inline.Single);
                    break;
                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        /// <summary>
        /// Visits the short inline variable instruction.
        /// </summary>
        /// <param name="inline">The short inline variable instruction.</param>
        public override void VisitShortInlineVarInstruction(ShortInlineVarInstruction inline)
        {
            TestMarker(inline);
            ILInstruction il;

            switch (inline.OpCode)
            {
                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldloca_S:
                    inline.Expression = new CodeVariableReferenceExpression(inline, $"var{inline.Ordinal}");
                    break;

                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        public override void VisitInlineLdLocSInstruction(InlineLdLocSInstruction inline)
        {
            TestMarker(inline);

            switch (inline.OpCode)
            {

                case OpCode c when c == System.Reflection.Emit.OpCodes.Ldloc_S:
                    inline.Expression = new CodeVariableReferenceExpression(inline, $"var{inline.Ordinal}");
                    break;

                default:
                    Stop();
                    break;
            }

            Stack(inline);
        }

        public override void VisitInlineStLocSInstruction(InlineStLocSInstruction inline)
        {
            TestMarker(inline);
            ILInstruction il;

            switch (inline.OpCode)
            {

                case OpCode c when c == System.Reflection.Emit.OpCodes.Stloc_S:
                    var right = Pop().Expression as CodeExpression;
                    inline.Expression = new CodeVariableReferenceExpression(inline, $"var{inline.Ordinal}");
                    inline.Expression = new CodeAssignStatement(inline, inline.Expression as CodeExpression, right);
                    break;

                default:
                    Stop();
                    break;

            }

            Stack(inline);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private CodeExpression[] getArguments(InlineMethodInstruction inline)
        {
            ILInstruction il;

            ParameterInfo[] parameters = inline.Method.GetParameters();
            int length = parameters.Length;
            CodeExpression[] arguments = new CodeExpression[length];
            parameters = parameters.Reverse().ToArray();
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    il = Pop();
                    ParameterInfo parameter = parameters[i];

                    Debug.Assert(il.OpCode == System.Reflection.Emit.OpCodes.Nop || il.Expression != null);
                    arguments[i] = il.Expression as CodeExpression;

                    if (parameter.IsOut)
                        arguments[i] = new CodeDirectionExpression(inline, FieldDirection.Out, arguments[i]);

                    else if (parameter.ParameterType.IsByRef)
                        arguments[i] = new CodeDirectionExpression(inline, FieldDirection.Ref, arguments[i]);

                    Debug.Assert((il.OpCode == System.Reflection.Emit.OpCodes.Nop || il.Expression != null) || arguments[i] != null);
                }
                arguments = arguments.Reverse().Where(c => c != null).ToArray();
                // Le nombre de parametre et le nombre d'arguments doivent correspondre, sauf dans le cas de pointeur. (regle probablement fausse)
                Debug.Assert(arguments.Length == (parameters.Where(c => c.ParameterType != typeof(IntPtr) && c.ParameterType != typeof(UIntPtr)).Count()));

            }

            return arguments;

        }

        [System.Diagnostics.DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Stack(ILInstruction instruction)
        {
            this._stack.Push(instruction);
        }

        [System.Diagnostics.DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ILInstruction Pop()
        {
            return this._stack.Pop();
        }

        [System.Diagnostics.DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ILInstruction Peek()
        {
            return this._stack.Peek();
        }

        [System.Diagnostics.DebuggerHidden]
        private static void Stop()
        {
            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Break();
        }

        private readonly Stack<ILInstruction> _stack;
        private static readonly MethodInfo _type_GetTypeFromHandle;
        private static readonly ConstructorInfo _type_CreateDecimal;
        private static readonly ConstructorInfo _type_CreateDecimal2;
        private List<Marker> _stack2;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TestMarker(ILInstruction inline)
        {
            ILInstruction il;
            CodeConditionStatement condition;
            List<ILInstruction> instructions;

            if (this._stack2.Count > 0)
            {
                var marker = this._stack2[0];
                if (marker.TargetOffset == inline.Offset)
                {

                    this._stack2.RemoveAt(0);

                    switch (marker.Kind)
                    {

                        case blockType.IfTrue:
                            instructions = GetInstructions(marker);
                            condition = Peek().Expression as CodeConditionStatement;
                            il = instructions.LastOrDefault();
                            if (il != null && il.OpCode == System.Reflection.Emit.OpCodes.Br_S)
                                instructions.Remove(il);
                            condition.TrueStatements.Add(instructions.Select(c => c.Expression));
                            break;

                        case blockType.IfFalse:
                            instructions = GetInstructions(marker);
                            condition = Peek().Expression as CodeConditionStatement;
                            condition.FalseStatements.Add(instructions.Select(c => c.Expression));

                            break;

                        case blockType.Undefined:
                        default:
                            break;
                    }


                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private List<ILInstruction> GetInstructions(Marker marker)
        {

            List<ILInstruction> instructions = new List<ILInstruction>();

            while (Peek() != marker.Inline)
                instructions.Add(Pop());

            instructions.Reverse();

            return instructions;

        }

        private bool LastInstructionBeforeFalseBlock(ShortInlineBrTargetInstruction inline, out Marker marker)
        {
            marker = null;
            if (this._stack2.Count > 0)
            {
                var _marker = this._stack2[0];
                if (_marker.TargetOffset - inline.GetSize() == inline.Offset)
                    marker = _marker;
            }
            return marker != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Marker AddMarker(ILInstruction inline, int targetOffset, blockType kind)
        {
            var m = new Marker(inline, targetOffset, kind);
            this._stack2.Add(m);
            this._stack2 = this._stack2.OrderBy(c => c.TargetOffset).ToList();
            return m;
        }

    }
}


/*
 
                //case InlineBrTargetInstruction inline:
                //case InlineFieldInstruction inline:
                //case InlineI8Instruction inline:
                //case InlineIInstruction inline:
                //case InlineMethodInstruction inline:
                //case InlineNoneInstruction inline:
                //case InlineRInstruction inline:
                //case InlineSigInstruction inline:
                //case InlineStringInstruction inline:
                //case InlineSwitchInstruction inline:
                //case InlineTokInstruction inline:
                //case InlineTypeInstruction inline:
                //case InlineVarInstruction inline:
                //case ShortInlineBrTargetInstruction inline:
                //case ShortInlineIInstruction inline:
                //case ShortInlineRInstruction inline:
                //case ShortInlineVarInstruction inline:

 */
