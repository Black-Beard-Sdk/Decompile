using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Bb.Sdk.Decompiler.IlParser
{

    public sealed partial class ILInstructionReader
    {


        private ILInstruction Next()
        {

            int position = this._position;
            OpCode operation = System.Reflection.Emit.OpCodes.Nop;
            byte index = this.ReadByte();

            if (index != 0xfe)
                operation = _oneByteOpCodes[index];

            else
            {
                index = this.ReadByte();
                operation = _twoByteOpCodes[index];
            }

            return DecodeInstruction(position, operation);

        }

        private ILInstruction DecodeInstruction(int position, OpCode operation)
        {

            switch (operation)
            {

                default:
                    switch (operation.OperandType)
                    {
                        case OperandType.InlineBrTarget:
                            return new InlineBrTargetInstruction(position, operation, this.ReadInt32());

                        case OperandType.InlineField:
                            return new InlineFieldInstruction(this._resolver, position, operation, this.ReadInt32());

                        case OperandType.InlineI:
                            return new InlineIInstruction(position, operation, this.ReadInt32());

                        case OperandType.InlineI8:
                            return new InlineI8Instruction(position, operation, this.ReadInt64());

                        case OperandType.InlineMethod:
                            return new InlineMethodInstruction(position, operation, this.ReadInt32(), this._resolver);

                        case OperandType.InlineNone:
                            return new InlineNoneInstruction(position, operation);

                        case OperandType.InlineR:
                            return new InlineRInstruction(position, operation, this.ReadDouble());

                        case OperandType.InlineSig:
                            return new InlineSigInstruction(position, operation, this.ReadInt32(), this._resolver);

                        case OperandType.InlineString:
                            return new InlineStringInstruction(position, operation, this.ReadInt32(), this._resolver);

                        case OperandType.InlineSwitch:
                            {
                                int num13 = this.ReadInt32();
                                int[] deltas = new int[num13];
                                for (int i = 0; i < num13; i++)
                                    deltas[i] = this.ReadInt32();
                                return new InlineSwitchInstruction(position, operation, deltas);
                            }
                        case OperandType.InlineTok:
                            return new InlineTokInstruction(position, operation, this.ReadInt32(), this._resolver);

                        case OperandType.InlineType:
                            return new InlineTypeInstruction(position, operation, this.ReadInt32(), this._resolver);

                        case OperandType.InlineVar:
                            return new InlineVarInstruction(position, operation, this.ReadUInt16());

                        case OperandType.ShortInlineBrTarget:
                            return new ShortInlineBrTargetInstruction(position, operation, this.ReadSByte());

                        case OperandType.ShortInlineI:
                            return new ShortInlineIInstruction(position, operation, this.ReadByte());

                        case OperandType.ShortInlineR:
                            return new ShortInlineRInstruction(position, operation, this.ReadSingle());

                        case OperandType.ShortInlineVar:
                            return new ShortInlineVarInstruction(position, operation, this.ReadByte());

                    }
                    break;

                case System.Reflection.Emit.OpCode c66 when c66 == System.Reflection.Emit.OpCodes.Stloc_0:
                case System.Reflection.Emit.OpCode c67 when c67 == System.Reflection.Emit.OpCodes.Stloc_1:
                case System.Reflection.Emit.OpCode c68 when c68 == System.Reflection.Emit.OpCodes.Stloc_2:
                case System.Reflection.Emit.OpCode c69 when c69 == System.Reflection.Emit.OpCodes.Stloc_3:
                    return new InlineStLocInstruction(position, operation);

                case System.Reflection.Emit.OpCode c71 when c71 == System.Reflection.Emit.OpCodes.Stloc_S:
                    return new InlineStLocSInstruction(position, operation, this.ReadByte());

                case System.Reflection.Emit.OpCode c13 when c13 == System.Reflection.Emit.OpCodes.Ldloc_3:
                case System.Reflection.Emit.OpCode c27 when c27 == System.Reflection.Emit.OpCodes.Ldloc_2:
                case System.Reflection.Emit.OpCode c28 when c28 == System.Reflection.Emit.OpCodes.Ldloc_1:
                case System.Reflection.Emit.OpCode c29 when c29 == System.Reflection.Emit.OpCodes.Ldloc_0:
                    return new InlineLdLocInstruction(position, operation);

                case System.Reflection.Emit.OpCode c02 when c02 == System.Reflection.Emit.OpCodes.Ldloc_S:
                    return new InlineLdLocSInstruction(position, operation, this.ReadByte());

                case System.Reflection.Emit.OpCode c184 when c184 == System.Reflection.Emit.OpCodes.Ldc_I4_0:
                case System.Reflection.Emit.OpCode c186 when c186 == System.Reflection.Emit.OpCodes.Ldc_I4_1:
                case System.Reflection.Emit.OpCode c198 when c198 == System.Reflection.Emit.OpCodes.Ldc_I4_2:
                case System.Reflection.Emit.OpCode c187 when c187 == System.Reflection.Emit.OpCodes.Ldc_I4_3:
                case System.Reflection.Emit.OpCode c188 when c188 == System.Reflection.Emit.OpCodes.Ldc_I4_4:
                case System.Reflection.Emit.OpCode c189 when c189 == System.Reflection.Emit.OpCodes.Ldc_I4_5:
                case System.Reflection.Emit.OpCode c190 when c190 == System.Reflection.Emit.OpCodes.Ldc_I4_6:
                case System.Reflection.Emit.OpCode c191 when c191 == System.Reflection.Emit.OpCodes.Ldc_I4_7:
                case System.Reflection.Emit.OpCode c192 when c192 == System.Reflection.Emit.OpCodes.Ldc_I4_8:
                case System.Reflection.Emit.OpCode c193 when c193 == System.Reflection.Emit.OpCodes.Ldc_I4_M1:
                    return new InlineLdI4Instruction(position, operation);

                case System.Reflection.Emit.OpCode c01 when c01 == System.Reflection.Emit.OpCodes.Add:
                case System.Reflection.Emit.OpCode c73 when c73 == System.Reflection.Emit.OpCodes.Sub:
                case System.Reflection.Emit.OpCode c223 when c223 == System.Reflection.Emit.OpCodes.Div:
                case System.Reflection.Emit.OpCode c16 when c16 == System.Reflection.Emit.OpCodes.Mul:
                case System.Reflection.Emit.OpCode c94 when c94 == System.Reflection.Emit.OpCodes.Rem:
                case System.Reflection.Emit.OpCode c224 when c224 == System.Reflection.Emit.OpCodes.Div_Un:
                case System.Reflection.Emit.OpCode c95 when c95 == System.Reflection.Emit.OpCodes.Rem_Un:
                case System.Reflection.Emit.OpCode c146 when c146 == System.Reflection.Emit.OpCodes.Add_Ovf_Un:
                case System.Reflection.Emit.OpCode c18 when c18 == System.Reflection.Emit.OpCodes.Mul_Ovf_Un:
                case System.Reflection.Emit.OpCode c75 when c75 == System.Reflection.Emit.OpCodes.Sub_Ovf_Un:
                case System.Reflection.Emit.OpCode c145 when c145 == System.Reflection.Emit.OpCodes.Add_Ovf:
                case System.Reflection.Emit.OpCode c17 when c17 == System.Reflection.Emit.OpCodes.Mul_Ovf:
                case System.Reflection.Emit.OpCode c74 when c74 == System.Reflection.Emit.OpCodes.Sub_Ovf:
                    return new InlineOperatorInstruction(position, operation);

                case System.Reflection.Emit.OpCode c124 when c124 == System.Reflection.Emit.OpCodes.Ceq:
                case System.Reflection.Emit.OpCode c125 when c125 == System.Reflection.Emit.OpCodes.Cgt:
                case System.Reflection.Emit.OpCode c129 when c129 == System.Reflection.Emit.OpCodes.Clt:
                case System.Reflection.Emit.OpCode c147 when c147 == System.Reflection.Emit.OpCodes.And:
                case System.Reflection.Emit.OpCode c114 when c114 == System.Reflection.Emit.OpCodes.Xor:
                case System.Reflection.Emit.OpCode c24 when c24 == System.Reflection.Emit.OpCodes.Or:
                case System.Reflection.Emit.OpCode c126 when c126 == System.Reflection.Emit.OpCodes.Cgt_Un:
                case System.Reflection.Emit.OpCode c141 when c141 == System.Reflection.Emit.OpCodes.Clt_Un:
                    return new InlineConditionInstruction(position, operation);

                case System.Reflection.Emit.OpCode c133 when c133 == System.Reflection.Emit.OpCodes.Conv_I2:
                case System.Reflection.Emit.OpCode c227 when c227 == System.Reflection.Emit.OpCodes.Conv_Ovf_I4_Un:
                case System.Reflection.Emit.OpCode c134 when c134 == System.Reflection.Emit.OpCodes.Conv_I4:
                case System.Reflection.Emit.OpCode c135 when c135 == System.Reflection.Emit.OpCodes.Conv_I8:
                case System.Reflection.Emit.OpCode c220 when c220 == System.Reflection.Emit.OpCodes.Conv_U8:
                case System.Reflection.Emit.OpCode c132 when c132 == System.Reflection.Emit.OpCodes.Conv_I1:
                case System.Reflection.Emit.OpCode c214 when c214 == System.Reflection.Emit.OpCodes.Conv_R4:
                case System.Reflection.Emit.OpCode c215 when c215 == System.Reflection.Emit.OpCodes.Conv_R8:
                case System.Reflection.Emit.OpCode c219 when c219 == System.Reflection.Emit.OpCodes.Conv_U4:
                case System.Reflection.Emit.OpCode c131 when c131 == System.Reflection.Emit.OpCodes.Conv_I:
                case System.Reflection.Emit.OpCode c213 when c213 == System.Reflection.Emit.OpCodes.Conv_R_Un:
                case System.Reflection.Emit.OpCode c216 when c216 == System.Reflection.Emit.OpCodes.Conv_U:
                case System.Reflection.Emit.OpCode c217 when c217 == System.Reflection.Emit.OpCodes.Conv_U1:
                case System.Reflection.Emit.OpCode c136 when c136 == System.Reflection.Emit.OpCodes.Conv_Ovf_I:
                case System.Reflection.Emit.OpCode c137 when c137 == System.Reflection.Emit.OpCodes.Conv_Ovf_I_Un:
                case System.Reflection.Emit.OpCode c138 when c138 == System.Reflection.Emit.OpCodes.Conv_Ovf_I1:
                case System.Reflection.Emit.OpCode c139 when c139 == System.Reflection.Emit.OpCodes.Conv_Ovf_I1_Un:
                case System.Reflection.Emit.OpCode c140 when c140 == System.Reflection.Emit.OpCodes.Conv_Ovf_I2:
                case System.Reflection.Emit.OpCode c170 when c170 == System.Reflection.Emit.OpCodes.Conv_Ovf_I2_Un:
                case System.Reflection.Emit.OpCode c172 when c172 == System.Reflection.Emit.OpCodes.Conv_Ovf_I4:
                case System.Reflection.Emit.OpCode c173 when c173 == System.Reflection.Emit.OpCodes.Conv_Ovf_I8:
                case System.Reflection.Emit.OpCode c202 when c202 == System.Reflection.Emit.OpCodes.Conv_Ovf_I8_Un:
                case System.Reflection.Emit.OpCode c203 when c203 == System.Reflection.Emit.OpCodes.Conv_Ovf_U:
                case System.Reflection.Emit.OpCode c204 when c204 == System.Reflection.Emit.OpCodes.Conv_Ovf_U_Un:
                case System.Reflection.Emit.OpCode c205 when c205 == System.Reflection.Emit.OpCodes.Conv_Ovf_U1:
                case System.Reflection.Emit.OpCode c206 when c206 == System.Reflection.Emit.OpCodes.Conv_Ovf_U1_Un:
                case System.Reflection.Emit.OpCode c207 when c207 == System.Reflection.Emit.OpCodes.Conv_Ovf_U2:
                case System.Reflection.Emit.OpCode c208 when c208 == System.Reflection.Emit.OpCodes.Conv_Ovf_U2_Un:
                case System.Reflection.Emit.OpCode c209 when c209 == System.Reflection.Emit.OpCodes.Conv_Ovf_U4:
                case System.Reflection.Emit.OpCode c210 when c210 == System.Reflection.Emit.OpCodes.Conv_Ovf_U4_Un:
                case System.Reflection.Emit.OpCode c211 when c211 == System.Reflection.Emit.OpCodes.Conv_Ovf_U8:
                case System.Reflection.Emit.OpCode c212 when c212 == System.Reflection.Emit.OpCodes.Conv_Ovf_U8_Un:
                case System.Reflection.Emit.OpCode c42 when c42 == System.Reflection.Emit.OpCodes.Ldind_I:
                case System.Reflection.Emit.OpCode c43 when c43 == System.Reflection.Emit.OpCodes.Ldind_I1:
                case System.Reflection.Emit.OpCode c44 when c44 == System.Reflection.Emit.OpCodes.Ldind_I2:
                case System.Reflection.Emit.OpCode c45 when c45 == System.Reflection.Emit.OpCodes.Ldind_I4:
                case System.Reflection.Emit.OpCode c46 when c46 == System.Reflection.Emit.OpCodes.Ldind_I8:
                case System.Reflection.Emit.OpCode c47 when c47 == System.Reflection.Emit.OpCodes.Ldind_R4:
                case System.Reflection.Emit.OpCode c48 when c48 == System.Reflection.Emit.OpCodes.Ldind_R8:
                case System.Reflection.Emit.OpCode c49 when c49 == System.Reflection.Emit.OpCodes.Ldind_Ref:
                case System.Reflection.Emit.OpCode c50 when c50 == System.Reflection.Emit.OpCodes.Ldind_U1:
                case System.Reflection.Emit.OpCode c51 when c51 == System.Reflection.Emit.OpCodes.Ldind_U2:
                case System.Reflection.Emit.OpCode c52 when c52 == System.Reflection.Emit.OpCodes.Ldind_U4:
                case System.Reflection.Emit.OpCode c59 when c59 == System.Reflection.Emit.OpCodes.Stind_I2:
                case System.Reflection.Emit.OpCode c60 when c60 == System.Reflection.Emit.OpCodes.Stind_I4:
                case System.Reflection.Emit.OpCode c61 when c61 == System.Reflection.Emit.OpCodes.Stind_I8:
                case System.Reflection.Emit.OpCode c62 when c62 == System.Reflection.Emit.OpCodes.Stind_R4:
                case System.Reflection.Emit.OpCode c63 when c63 == System.Reflection.Emit.OpCodes.Stind_R8:
                case System.Reflection.Emit.OpCode c64 when c64 == System.Reflection.Emit.OpCodes.Stind_Ref:
                case System.Reflection.Emit.OpCode c70 when c70 == System.Reflection.Emit.OpCodes.Stind_I1:
                    return new InlineConvertionInstruction(position, operation);

                case System.Reflection.Emit.OpCode c176 when c176 == System.Reflection.Emit.OpCodes.Ldarg_0:
                case System.Reflection.Emit.OpCode c177 when c177 == System.Reflection.Emit.OpCodes.Ldarg_1:
                case System.Reflection.Emit.OpCode c178 when c178 == System.Reflection.Emit.OpCodes.Ldarg_2:
                case System.Reflection.Emit.OpCode c179 when c179 == System.Reflection.Emit.OpCodes.Ldarg_3:
                    return new InlineLdArgInstruction(position, operation);

                case System.Reflection.Emit.OpCode c115 when c115 == System.Reflection.Emit.OpCodes.Ldelem_I1:
                case System.Reflection.Emit.OpCode c56 when c56 == System.Reflection.Emit.OpCodes.Ldelem_I2:
                case System.Reflection.Emit.OpCode c30 when c30 == System.Reflection.Emit.OpCodes.Ldelem_I4:
                case System.Reflection.Emit.OpCode c31 when c31 == System.Reflection.Emit.OpCodes.Ldelem_I8:
                case System.Reflection.Emit.OpCode c32 when c32 == System.Reflection.Emit.OpCodes.Ldelem_R4:
                case System.Reflection.Emit.OpCode c33 when c33 == System.Reflection.Emit.OpCodes.Ldelem_R8:
                case System.Reflection.Emit.OpCode c34 when c34 == System.Reflection.Emit.OpCodes.Ldelem_Ref:
                case System.Reflection.Emit.OpCode c35 when c35 == System.Reflection.Emit.OpCodes.Ldelem_U1:
                case System.Reflection.Emit.OpCode c36 when c36 == System.Reflection.Emit.OpCodes.Ldelem_U2:
                case System.Reflection.Emit.OpCode c37 when c37 == System.Reflection.Emit.OpCodes.Ldelem_U4:
                    return new InlineLdElemInstruction(position, operation);

                case System.Reflection.Emit.OpCode c86 when c86 == System.Reflection.Emit.OpCodes.Stelem_Ref:
                case System.Reflection.Emit.OpCode c105 when c105 == System.Reflection.Emit.OpCodes.Stelem:
                case System.Reflection.Emit.OpCode c107 when c107 == System.Reflection.Emit.OpCodes.Stelem_I1:
                case System.Reflection.Emit.OpCode c108 when c108 == System.Reflection.Emit.OpCodes.Stelem_I2:
                case System.Reflection.Emit.OpCode c109 when c109 == System.Reflection.Emit.OpCodes.Stelem_I4:
                case System.Reflection.Emit.OpCode c110 when c110 == System.Reflection.Emit.OpCodes.Stelem_I8:
                case System.Reflection.Emit.OpCode c111 when c111 == System.Reflection.Emit.OpCodes.Stelem_R4:
                case System.Reflection.Emit.OpCode c112 when c112 == System.Reflection.Emit.OpCodes.Stelem_R8:
                    return new InlineStElemInstruction(position, operation);

                    //case System.Reflection.Emit.OpCode c180 when c180 == System.Reflection.Emit.OpCodes.Ldarg_S:
                    //case System.Reflection.Emit.OpCode c181 when c181 == System.Reflection.Emit.OpCodes.Ldarga:
                    //case System.Reflection.Emit.OpCode c175 when c175 == System.Reflection.Emit.OpCodes.Ldarg:
                    //case System.Reflection.Emit.OpCode c194 when c194 == System.Reflection.Emit.OpCodes.Ldc_I4_S:
                    //case System.Reflection.Emit.OpCode c182 when c182 == System.Reflection.Emit.OpCodes.Ldarga_S:
                    //case System.Reflection.Emit.OpCode c03 when c03 == System.Reflection.Emit.OpCodes.Ldloca:
                    //case System.Reflection.Emit.OpCode c04 when c04 == System.Reflection.Emit.OpCodes.Ldloca_S:
                    //case System.Reflection.Emit.OpCode c06 when c06 == System.Reflection.Emit.OpCodes.Ldobj:
                    //case System.Reflection.Emit.OpCode c07 when c07 == System.Reflection.Emit.OpCodes.Ldsfld:
                    //case System.Reflection.Emit.OpCode c08 when c08 == System.Reflection.Emit.OpCodes.Ldsflda:                    
                    //case System.Reflection.Emit.OpCode c10 when c10 == System.Reflection.Emit.OpCodes.Ldtoken:
                    //case System.Reflection.Emit.OpCode c11 when c11 == System.Reflection.Emit.OpCodes.Ldvirtftn:                    
                    //case System.Reflection.Emit.OpCode c38 when c38 == System.Reflection.Emit.OpCodes.Ldelema:
                    //case System.Reflection.Emit.OpCode c39 when c39 == System.Reflection.Emit.OpCodes.Ldfld:
                    //case System.Reflection.Emit.OpCode c40 when c40 == System.Reflection.Emit.OpCodes.Ldflda:
                    //case System.Reflection.Emit.OpCode c41 when c41 == System.Reflection.Emit.OpCodes.Ldftn:                    
                    //case System.Reflection.Emit.OpCode c53 when c53 == System.Reflection.Emit.OpCodes.Ldlen:
                    //case System.Reflection.Emit.OpCode c54 when c54 == System.Reflection.Emit.OpCodes.Ldloc:

                    //case System.Reflection.Emit.OpCode c80 when c80 == System.Reflection.Emit.OpCodes.Unbox:
                    //case System.Reflection.Emit.OpCode c81 when c81 == System.Reflection.Emit.OpCodes.Unbox_Any:
                    //case System.Reflection.Emit.OpCode c169 when c169 == System.Reflection.Emit.OpCodes.Box:

                    //case System.Reflection.Emit.OpCode c106 when c106 == System.Reflection.Emit.OpCodes.Stelem_I:

                    //case System.Reflection.Emit.OpCode c65 when c65 == System.Reflection.Emit.OpCodes.Stloc:


                    //case System.Reflection.Emit.OpCode c12 when c12 == System.Reflection.Emit.OpCodes.Leave:
                    //case System.Reflection.Emit.OpCode c14 when c14 == System.Reflection.Emit.OpCodes.Leave_S:

                    //case System.Reflection.Emit.OpCode c15 when c15 == System.Reflection.Emit.OpCodes.Mkrefany:


                    //case System.Reflection.Emit.OpCode c26 when c26 == System.Reflection.Emit.OpCodes.Localloc:
                    //case System.Reflection.Emit.OpCode c55 when c55 == System.Reflection.Emit.OpCodes.Prefix1:                    
                    //case System.Reflection.Emit.OpCode c57 when c57 == System.Reflection.Emit.OpCodes.Prefix2:
                    //case System.Reflection.Emit.OpCode c58 when c58 == System.Reflection.Emit.OpCodes.Prefix4:
                    //case System.Reflection.Emit.OpCode c72 when c72 == System.Reflection.Emit.OpCodes.Stsfld:                    
                    //case System.Reflection.Emit.OpCode c77 when c77 == System.Reflection.Emit.OpCodes.Tailcall:
                    //case System.Reflection.Emit.OpCode c79 when c79 == System.Reflection.Emit.OpCodes.Unaligned:                    
                    //case System.Reflection.Emit.OpCode c83 when c83 == System.Reflection.Emit.OpCodes.Stobj:
                    //case System.Reflection.Emit.OpCode c84 when c84 == System.Reflection.Emit.OpCodes.Stind_I:
                    //case System.Reflection.Emit.OpCode c85 when c85 == System.Reflection.Emit.OpCodes.Stfld:
                    //case System.Reflection.Emit.OpCode c87 when c87 == System.Reflection.Emit.OpCodes.Prefix5:
                    //case System.Reflection.Emit.OpCode c88 when c88 == System.Reflection.Emit.OpCodes.Prefix6:
                    //case System.Reflection.Emit.OpCode c89 when c89 == System.Reflection.Emit.OpCodes.Prefix7:
                    //case System.Reflection.Emit.OpCode c90 when c90 == System.Reflection.Emit.OpCodes.Prefixref:

                    //case System.Reflection.Emit.OpCode c92 when c92 == System.Reflection.Emit.OpCodes.Refanytype:
                    //case System.Reflection.Emit.OpCode c93 when c93 == System.Reflection.Emit.OpCodes.Refanyval:                   
                    //case System.Reflection.Emit.OpCode c98 when c98 == System.Reflection.Emit.OpCodes.Shl:
                    //case System.Reflection.Emit.OpCode c99 when c99 == System.Reflection.Emit.OpCodes.Shr:
                    //case System.Reflection.Emit.OpCode c101 when c101 == System.Reflection.Emit.OpCodes.Shr_Un:
                    //case System.Reflection.Emit.OpCode c102 when c102 == System.Reflection.Emit.OpCodes.Sizeof:
                    //case System.Reflection.Emit.OpCode c103 when c103 == System.Reflection.Emit.OpCodes.Starg:
                    //case System.Reflection.Emit.OpCode c104 when c104 == System.Reflection.Emit.OpCodes.Starg_S:

                    //case System.Reflection.Emit.OpCode c113 when c113 == System.Reflection.Emit.OpCodes.Prefix3:                    
                    //case System.Reflection.Emit.OpCode c116 when c116 == System.Reflection.Emit.OpCodes.Ldelem:

                    //case System.Reflection.Emit.OpCode c20 when c20 == System.Reflection.Emit.OpCodes.Newarr:

                    //case System.Reflection.Emit.OpCode c21 when c21 == System.Reflection.Emit.OpCodes.Newobj:
                    //case System.Reflection.Emit.OpCode c120 when c120 == System.Reflection.Emit.OpCodes.Call:
                    //case System.Reflection.Emit.OpCode c121 when c121 == System.Reflection.Emit.OpCodes.Calli:
                    //case System.Reflection.Emit.OpCode c122 when c122 == System.Reflection.Emit.OpCodes.Callvirt:

                    //case System.Reflection.Emit.OpCode c123 when c123 == System.Reflection.Emit.OpCodes.Castclass:
                    //case System.Reflection.Emit.OpCode c127 when c127 == System.Reflection.Emit.OpCodes.Ckfinite:

                    //case System.Reflection.Emit.OpCode c130 when c130 == System.Reflection.Emit.OpCodes.Constrained:                    
                    //case System.Reflection.Emit.OpCode c148 when c148 == System.Reflection.Emit.OpCodes.Arglist:

                    //case System.Reflection.Emit.OpCode c128 when c128 == System.Reflection.Emit.OpCodes.Brfalse:
                    //case System.Reflection.Emit.OpCode c117 when c117 == System.Reflection.Emit.OpCodes.Brfalse_S:
                    //case System.Reflection.Emit.OpCode c118 when c118 == System.Reflection.Emit.OpCodes.Brtrue:
                    //case System.Reflection.Emit.OpCode c119 when c119 == System.Reflection.Emit.OpCodes.Brtrue_S:
                    //case System.Reflection.Emit.OpCode c143 when c143 == System.Reflection.Emit.OpCodes.Br_S:
                    //case System.Reflection.Emit.OpCode c144 when c144 == System.Reflection.Emit.OpCodes.Br:

                    //case System.Reflection.Emit.OpCode c149 when c149 == System.Reflection.Emit.OpCodes.Beq:
                    //case System.Reflection.Emit.OpCode c150 when c150 == System.Reflection.Emit.OpCodes.Beq_S:

                    //case System.Reflection.Emit.OpCode c151 when c151 == System.Reflection.Emit.OpCodes.Bge:
                    //case System.Reflection.Emit.OpCode c152 when c152 == System.Reflection.Emit.OpCodes.Bge_S:
                    //case System.Reflection.Emit.OpCode c153 when c153 == System.Reflection.Emit.OpCodes.Bge_Un:
                    //case System.Reflection.Emit.OpCode c154 when c154 == System.Reflection.Emit.OpCodes.Bge_Un_S:

                    //case System.Reflection.Emit.OpCode c155 when c155 == System.Reflection.Emit.OpCodes.Bgt:
                    //case System.Reflection.Emit.OpCode c156 when c156 == System.Reflection.Emit.OpCodes.Bgt_S:
                    //case System.Reflection.Emit.OpCode c157 when c157 == System.Reflection.Emit.OpCodes.Bgt_Un:
                    //case System.Reflection.Emit.OpCode c158 when c158 == System.Reflection.Emit.OpCodes.Bgt_Un_S:

                    //case System.Reflection.Emit.OpCode c159 when c159 == System.Reflection.Emit.OpCodes.Ble:
                    //case System.Reflection.Emit.OpCode c160 when c160 == System.Reflection.Emit.OpCodes.Ble_S:
                    //case System.Reflection.Emit.OpCode c161 when c161 == System.Reflection.Emit.OpCodes.Ble_Un:
                    //case System.Reflection.Emit.OpCode c162 when c162 == System.Reflection.Emit.OpCodes.Ble_Un_S:

                    //case System.Reflection.Emit.OpCode c163 when c163 == System.Reflection.Emit.OpCodes.Blt:
                    //case System.Reflection.Emit.OpCode c164 when c164 == System.Reflection.Emit.OpCodes.Blt_S:
                    //case System.Reflection.Emit.OpCode c165 when c165 == System.Reflection.Emit.OpCodes.Blt_Un:
                    //case System.Reflection.Emit.OpCode c166 when c166 == System.Reflection.Emit.OpCodes.Blt_Un_S:
                    //case System.Reflection.Emit.OpCode c167 when c167 == System.Reflection.Emit.OpCodes.Bne_Un:

                    //case System.Reflection.Emit.OpCode c168 when c168 == System.Reflection.Emit.OpCodes.Bne_Un_S:

                    //case System.Reflection.Emit.OpCode c171 when c171 == System.Reflection.Emit.OpCodes.Ldelem_I:                    

                    //case System.Reflection.Emit.OpCode c174 when c174 == System.Reflection.Emit.OpCodes.Jmp:                    
                    //case System.Reflection.Emit.OpCode c185 when c185 == System.Reflection.Emit.OpCodes.Isinst:

                    //case System.Reflection.Emit.OpCode c183 when c183 == System.Reflection.Emit.OpCodes.Ldc_I4:
                    //case System.Reflection.Emit.OpCode c195 when c195 == System.Reflection.Emit.OpCodes.Ldc_I8:
                    //case System.Reflection.Emit.OpCode c196 when c196 == System.Reflection.Emit.OpCodes.Ldc_R4:
                    //case System.Reflection.Emit.OpCode c197 when c197 == System.Reflection.Emit.OpCodes.Ldc_R8:
                    

                    //case System.Reflection.Emit.OpCode c199 when c199 == System.Reflection.Emit.OpCodes.Initobj:
                    //case System.Reflection.Emit.OpCode c200 when c200 == System.Reflection.Emit.OpCodes.Initblk:

                    //case System.Reflection.Emit.OpCode c221 when c221 == System.Reflection.Emit.OpCodes.Cpblk:
                    //case System.Reflection.Emit.OpCode c222 when c222 == System.Reflection.Emit.OpCodes.Cpobj:                                        




            }

            throw new BadImageFormatException("unexpected OperandType " + operation.OperandType);

        }


        private ILInstruction DecodeInstruction(ILInstruction currentInstruction)
        {

            switch (currentInstruction.OpCode)
            {

                case System.Reflection.Emit.OpCode c01 when c01 == System.Reflection.Emit.OpCodes.Add:
                case System.Reflection.Emit.OpCode c02 when c02 == System.Reflection.Emit.OpCodes.Ldloc_S:
                case System.Reflection.Emit.OpCode c03 when c03 == System.Reflection.Emit.OpCodes.Ldloca:
                case System.Reflection.Emit.OpCode c04 when c04 == System.Reflection.Emit.OpCodes.Ldloca_S:
                case System.Reflection.Emit.OpCode c05 when c05 == System.Reflection.Emit.OpCodes.Ldnull:
                case System.Reflection.Emit.OpCode c06 when c06 == System.Reflection.Emit.OpCodes.Ldobj:
                case System.Reflection.Emit.OpCode c07 when c07 == System.Reflection.Emit.OpCodes.Ldsfld:
                case System.Reflection.Emit.OpCode c08 when c08 == System.Reflection.Emit.OpCodes.Ldsflda:
                case System.Reflection.Emit.OpCode c09 when c09 == System.Reflection.Emit.OpCodes.Ldstr:
                case System.Reflection.Emit.OpCode c10 when c10 == System.Reflection.Emit.OpCodes.Ldtoken:
                case System.Reflection.Emit.OpCode c11 when c11 == System.Reflection.Emit.OpCodes.Ldvirtftn:
                case System.Reflection.Emit.OpCode c13 when c13 == System.Reflection.Emit.OpCodes.Ldloc_3:
                case System.Reflection.Emit.OpCode c27 when c27 == System.Reflection.Emit.OpCodes.Ldloc_2:
                case System.Reflection.Emit.OpCode c28 when c28 == System.Reflection.Emit.OpCodes.Ldloc_1:
                case System.Reflection.Emit.OpCode c29 when c29 == System.Reflection.Emit.OpCodes.Ldloc_0:
                case System.Reflection.Emit.OpCode c30 when c30 == System.Reflection.Emit.OpCodes.Ldelem_I4:
                case System.Reflection.Emit.OpCode c31 when c31 == System.Reflection.Emit.OpCodes.Ldelem_I8:
                case System.Reflection.Emit.OpCode c32 when c32 == System.Reflection.Emit.OpCodes.Ldelem_R4:
                case System.Reflection.Emit.OpCode c33 when c33 == System.Reflection.Emit.OpCodes.Ldelem_R8:
                case System.Reflection.Emit.OpCode c34 when c34 == System.Reflection.Emit.OpCodes.Ldelem_Ref:
                case System.Reflection.Emit.OpCode c35 when c35 == System.Reflection.Emit.OpCodes.Ldelem_U1:
                case System.Reflection.Emit.OpCode c36 when c36 == System.Reflection.Emit.OpCodes.Ldelem_U2:
                case System.Reflection.Emit.OpCode c37 when c37 == System.Reflection.Emit.OpCodes.Ldelem_U4:
                case System.Reflection.Emit.OpCode c38 when c38 == System.Reflection.Emit.OpCodes.Ldelema:
                case System.Reflection.Emit.OpCode c39 when c39 == System.Reflection.Emit.OpCodes.Ldfld:
                case System.Reflection.Emit.OpCode c40 when c40 == System.Reflection.Emit.OpCodes.Ldflda:
                case System.Reflection.Emit.OpCode c41 when c41 == System.Reflection.Emit.OpCodes.Ldftn:
                case System.Reflection.Emit.OpCode c42 when c42 == System.Reflection.Emit.OpCodes.Ldind_I:
                case System.Reflection.Emit.OpCode c43 when c43 == System.Reflection.Emit.OpCodes.Ldind_I1:
                case System.Reflection.Emit.OpCode c44 when c44 == System.Reflection.Emit.OpCodes.Ldind_I2:
                case System.Reflection.Emit.OpCode c45 when c45 == System.Reflection.Emit.OpCodes.Ldind_I4:
                case System.Reflection.Emit.OpCode c46 when c46 == System.Reflection.Emit.OpCodes.Ldind_I8:
                case System.Reflection.Emit.OpCode c47 when c47 == System.Reflection.Emit.OpCodes.Ldind_R4:
                case System.Reflection.Emit.OpCode c48 when c48 == System.Reflection.Emit.OpCodes.Ldind_R8:
                case System.Reflection.Emit.OpCode c49 when c49 == System.Reflection.Emit.OpCodes.Ldind_Ref:
                case System.Reflection.Emit.OpCode c50 when c50 == System.Reflection.Emit.OpCodes.Ldind_U1:
                case System.Reflection.Emit.OpCode c51 when c51 == System.Reflection.Emit.OpCodes.Ldind_U2:
                case System.Reflection.Emit.OpCode c52 when c52 == System.Reflection.Emit.OpCodes.Ldind_U4:
                case System.Reflection.Emit.OpCode c53 when c53 == System.Reflection.Emit.OpCodes.Ldlen:
                case System.Reflection.Emit.OpCode c54 when c54 == System.Reflection.Emit.OpCodes.Ldloc:

                case System.Reflection.Emit.OpCode c12 when c12 == System.Reflection.Emit.OpCodes.Leave:
                case System.Reflection.Emit.OpCode c14 when c14 == System.Reflection.Emit.OpCodes.Leave_S:
                case System.Reflection.Emit.OpCode c15 when c15 == System.Reflection.Emit.OpCodes.Mkrefany:
                case System.Reflection.Emit.OpCode c16 when c16 == System.Reflection.Emit.OpCodes.Mul:
                case System.Reflection.Emit.OpCode c17 when c17 == System.Reflection.Emit.OpCodes.Mul_Ovf:
                case System.Reflection.Emit.OpCode c18 when c18 == System.Reflection.Emit.OpCodes.Mul_Ovf_Un:
                case System.Reflection.Emit.OpCode c19 when c19 == System.Reflection.Emit.OpCodes.Neg:
                case System.Reflection.Emit.OpCode c20 when c20 == System.Reflection.Emit.OpCodes.Newarr:
                case System.Reflection.Emit.OpCode c21 when c21 == System.Reflection.Emit.OpCodes.Newobj:
                case System.Reflection.Emit.OpCode c22 when c22 == System.Reflection.Emit.OpCodes.Nop:
                case System.Reflection.Emit.OpCode c23 when c23 == System.Reflection.Emit.OpCodes.Not:
                case System.Reflection.Emit.OpCode c24 when c24 == System.Reflection.Emit.OpCodes.Or:
                case System.Reflection.Emit.OpCode c25 when c25 == System.Reflection.Emit.OpCodes.Pop:
                case System.Reflection.Emit.OpCode c26 when c26 == System.Reflection.Emit.OpCodes.Localloc:
                case System.Reflection.Emit.OpCode c55 when c55 == System.Reflection.Emit.OpCodes.Prefix1:
                case System.Reflection.Emit.OpCode c56 when c56 == System.Reflection.Emit.OpCodes.Ldelem_I2:
                case System.Reflection.Emit.OpCode c57 when c57 == System.Reflection.Emit.OpCodes.Prefix2:
                case System.Reflection.Emit.OpCode c58 when c58 == System.Reflection.Emit.OpCodes.Prefix4:
                case System.Reflection.Emit.OpCode c59 when c59 == System.Reflection.Emit.OpCodes.Stind_I2:
                case System.Reflection.Emit.OpCode c60 when c60 == System.Reflection.Emit.OpCodes.Stind_I4:
                case System.Reflection.Emit.OpCode c61 when c61 == System.Reflection.Emit.OpCodes.Stind_I8:
                case System.Reflection.Emit.OpCode c62 when c62 == System.Reflection.Emit.OpCodes.Stind_R4:
                case System.Reflection.Emit.OpCode c63 when c63 == System.Reflection.Emit.OpCodes.Stind_R8:
                case System.Reflection.Emit.OpCode c64 when c64 == System.Reflection.Emit.OpCodes.Stind_Ref:
                case System.Reflection.Emit.OpCode c65 when c65 == System.Reflection.Emit.OpCodes.Stloc:
                case System.Reflection.Emit.OpCode c66 when c66 == System.Reflection.Emit.OpCodes.Stloc_0:
                case System.Reflection.Emit.OpCode c67 when c67 == System.Reflection.Emit.OpCodes.Stloc_1:
                case System.Reflection.Emit.OpCode c68 when c68 == System.Reflection.Emit.OpCodes.Stloc_2:
                case System.Reflection.Emit.OpCode c69 when c69 == System.Reflection.Emit.OpCodes.Stloc_3:
                case System.Reflection.Emit.OpCode c70 when c70 == System.Reflection.Emit.OpCodes.Stind_I1:
                case System.Reflection.Emit.OpCode c71 when c71 == System.Reflection.Emit.OpCodes.Stloc_S:
                case System.Reflection.Emit.OpCode c72 when c72 == System.Reflection.Emit.OpCodes.Stsfld:
                case System.Reflection.Emit.OpCode c73 when c73 == System.Reflection.Emit.OpCodes.Sub:
                case System.Reflection.Emit.OpCode c74 when c74 == System.Reflection.Emit.OpCodes.Sub_Ovf:
                case System.Reflection.Emit.OpCode c75 when c75 == System.Reflection.Emit.OpCodes.Sub_Ovf_Un:
                case System.Reflection.Emit.OpCode c76 when c76 == System.Reflection.Emit.OpCodes.Switch:
                case System.Reflection.Emit.OpCode c77 when c77 == System.Reflection.Emit.OpCodes.Tailcall:
                case System.Reflection.Emit.OpCode c78 when c78 == System.Reflection.Emit.OpCodes.Throw:
                case System.Reflection.Emit.OpCode c79 when c79 == System.Reflection.Emit.OpCodes.Unaligned:
                case System.Reflection.Emit.OpCode c80 when c80 == System.Reflection.Emit.OpCodes.Unbox:
                case System.Reflection.Emit.OpCode c81 when c81 == System.Reflection.Emit.OpCodes.Unbox_Any:
                case System.Reflection.Emit.OpCode c82 when c82 == System.Reflection.Emit.OpCodes.Volatile:
                case System.Reflection.Emit.OpCode c83 when c83 == System.Reflection.Emit.OpCodes.Stobj:
                case System.Reflection.Emit.OpCode c84 when c84 == System.Reflection.Emit.OpCodes.Stind_I:
                case System.Reflection.Emit.OpCode c85 when c85 == System.Reflection.Emit.OpCodes.Stfld:
                case System.Reflection.Emit.OpCode c86 when c86 == System.Reflection.Emit.OpCodes.Stelem_Ref:
                case System.Reflection.Emit.OpCode c87 when c87 == System.Reflection.Emit.OpCodes.Prefix5:
                case System.Reflection.Emit.OpCode c88 when c88 == System.Reflection.Emit.OpCodes.Prefix6:
                case System.Reflection.Emit.OpCode c89 when c89 == System.Reflection.Emit.OpCodes.Prefix7:
                case System.Reflection.Emit.OpCode c90 when c90 == System.Reflection.Emit.OpCodes.Prefixref:
                case System.Reflection.Emit.OpCode c91 when c91 == System.Reflection.Emit.OpCodes.Readonly:
                case System.Reflection.Emit.OpCode c92 when c92 == System.Reflection.Emit.OpCodes.Refanytype:
                case System.Reflection.Emit.OpCode c93 when c93 == System.Reflection.Emit.OpCodes.Refanyval:
                case System.Reflection.Emit.OpCode c94 when c94 == System.Reflection.Emit.OpCodes.Rem:
                case System.Reflection.Emit.OpCode c95 when c95 == System.Reflection.Emit.OpCodes.Rem_Un:
                case System.Reflection.Emit.OpCode c96 when c96 == System.Reflection.Emit.OpCodes.Ret:
                case System.Reflection.Emit.OpCode c97 when c97 == System.Reflection.Emit.OpCodes.Rethrow:
                case System.Reflection.Emit.OpCode c98 when c98 == System.Reflection.Emit.OpCodes.Shl:
                case System.Reflection.Emit.OpCode c99 when c99 == System.Reflection.Emit.OpCodes.Shr:
                case System.Reflection.Emit.OpCode c101 when c101 == System.Reflection.Emit.OpCodes.Shr_Un:
                case System.Reflection.Emit.OpCode c102 when c102 == System.Reflection.Emit.OpCodes.Sizeof:
                case System.Reflection.Emit.OpCode c103 when c103 == System.Reflection.Emit.OpCodes.Starg:
                case System.Reflection.Emit.OpCode c104 when c104 == System.Reflection.Emit.OpCodes.Starg_S:
                case System.Reflection.Emit.OpCode c105 when c105 == System.Reflection.Emit.OpCodes.Stelem:
                case System.Reflection.Emit.OpCode c106 when c106 == System.Reflection.Emit.OpCodes.Stelem_I:
                case System.Reflection.Emit.OpCode c107 when c107 == System.Reflection.Emit.OpCodes.Stelem_I1:
                case System.Reflection.Emit.OpCode c108 when c108 == System.Reflection.Emit.OpCodes.Stelem_I2:
                case System.Reflection.Emit.OpCode c109 when c109 == System.Reflection.Emit.OpCodes.Stelem_I4:
                case System.Reflection.Emit.OpCode c110 when c110 == System.Reflection.Emit.OpCodes.Stelem_I8:
                case System.Reflection.Emit.OpCode c111 when c111 == System.Reflection.Emit.OpCodes.Stelem_R4:
                case System.Reflection.Emit.OpCode c112 when c112 == System.Reflection.Emit.OpCodes.Stelem_R8:
                case System.Reflection.Emit.OpCode c113 when c113 == System.Reflection.Emit.OpCodes.Prefix3:
                case System.Reflection.Emit.OpCode c114 when c114 == System.Reflection.Emit.OpCodes.Xor:
                case System.Reflection.Emit.OpCode c115 when c115 == System.Reflection.Emit.OpCodes.Ldelem_I1:
                case System.Reflection.Emit.OpCode c116 when c116 == System.Reflection.Emit.OpCodes.Ldelem:
                case System.Reflection.Emit.OpCode c117 when c117 == System.Reflection.Emit.OpCodes.Brfalse_S:
                case System.Reflection.Emit.OpCode c118 when c118 == System.Reflection.Emit.OpCodes.Brtrue:
                case System.Reflection.Emit.OpCode c119 when c119 == System.Reflection.Emit.OpCodes.Brtrue_S:
                case System.Reflection.Emit.OpCode c120 when c120 == System.Reflection.Emit.OpCodes.Call:
                case System.Reflection.Emit.OpCode c121 when c121 == System.Reflection.Emit.OpCodes.Calli:
                case System.Reflection.Emit.OpCode c122 when c122 == System.Reflection.Emit.OpCodes.Callvirt:
                case System.Reflection.Emit.OpCode c123 when c123 == System.Reflection.Emit.OpCodes.Castclass:
                case System.Reflection.Emit.OpCode c124 when c124 == System.Reflection.Emit.OpCodes.Ceq:
                case System.Reflection.Emit.OpCode c125 when c125 == System.Reflection.Emit.OpCodes.Cgt:
                case System.Reflection.Emit.OpCode c126 when c126 == System.Reflection.Emit.OpCodes.Cgt_Un:
                case System.Reflection.Emit.OpCode c127 when c127 == System.Reflection.Emit.OpCodes.Ckfinite:
                case System.Reflection.Emit.OpCode c128 when c128 == System.Reflection.Emit.OpCodes.Brfalse:
                case System.Reflection.Emit.OpCode c129 when c129 == System.Reflection.Emit.OpCodes.Clt:
                case System.Reflection.Emit.OpCode c130 when c130 == System.Reflection.Emit.OpCodes.Constrained:
                case System.Reflection.Emit.OpCode c131 when c131 == System.Reflection.Emit.OpCodes.Conv_I:
                case System.Reflection.Emit.OpCode c132 when c132 == System.Reflection.Emit.OpCodes.Conv_I1:
                case System.Reflection.Emit.OpCode c133 when c133 == System.Reflection.Emit.OpCodes.Conv_I2:
                case System.Reflection.Emit.OpCode c134 when c134 == System.Reflection.Emit.OpCodes.Conv_I4:
                case System.Reflection.Emit.OpCode c135 when c135 == System.Reflection.Emit.OpCodes.Conv_I8:
                case System.Reflection.Emit.OpCode c136 when c136 == System.Reflection.Emit.OpCodes.Conv_Ovf_I:
                case System.Reflection.Emit.OpCode c137 when c137 == System.Reflection.Emit.OpCodes.Conv_Ovf_I_Un:
                case System.Reflection.Emit.OpCode c138 when c138 == System.Reflection.Emit.OpCodes.Conv_Ovf_I1:
                case System.Reflection.Emit.OpCode c139 when c139 == System.Reflection.Emit.OpCodes.Conv_Ovf_I1_Un:
                case System.Reflection.Emit.OpCode c140 when c140 == System.Reflection.Emit.OpCodes.Conv_Ovf_I2:
                case System.Reflection.Emit.OpCode c141 when c141 == System.Reflection.Emit.OpCodes.Clt_Un:
                case System.Reflection.Emit.OpCode c142 when c142 == System.Reflection.Emit.OpCodes.Break:
                case System.Reflection.Emit.OpCode c143 when c143 == System.Reflection.Emit.OpCodes.Br_S:
                case System.Reflection.Emit.OpCode c144 when c144 == System.Reflection.Emit.OpCodes.Br:
                case System.Reflection.Emit.OpCode c145 when c145 == System.Reflection.Emit.OpCodes.Add_Ovf:
                case System.Reflection.Emit.OpCode c146 when c146 == System.Reflection.Emit.OpCodes.Add_Ovf_Un:
                case System.Reflection.Emit.OpCode c147 when c147 == System.Reflection.Emit.OpCodes.And:
                case System.Reflection.Emit.OpCode c148 when c148 == System.Reflection.Emit.OpCodes.Arglist:
                case System.Reflection.Emit.OpCode c149 when c149 == System.Reflection.Emit.OpCodes.Beq:
                case System.Reflection.Emit.OpCode c150 when c150 == System.Reflection.Emit.OpCodes.Beq_S:
                case System.Reflection.Emit.OpCode c151 when c151 == System.Reflection.Emit.OpCodes.Bge:
                case System.Reflection.Emit.OpCode c152 when c152 == System.Reflection.Emit.OpCodes.Bge_S:
                case System.Reflection.Emit.OpCode c153 when c153 == System.Reflection.Emit.OpCodes.Bge_Un:
                case System.Reflection.Emit.OpCode c154 when c154 == System.Reflection.Emit.OpCodes.Bge_Un_S:
                case System.Reflection.Emit.OpCode c155 when c155 == System.Reflection.Emit.OpCodes.Bgt:
                case System.Reflection.Emit.OpCode c156 when c156 == System.Reflection.Emit.OpCodes.Bgt_S:
                case System.Reflection.Emit.OpCode c157 when c157 == System.Reflection.Emit.OpCodes.Bgt_Un:
                case System.Reflection.Emit.OpCode c158 when c158 == System.Reflection.Emit.OpCodes.Bgt_Un_S:
                case System.Reflection.Emit.OpCode c159 when c159 == System.Reflection.Emit.OpCodes.Ble:
                case System.Reflection.Emit.OpCode c160 when c160 == System.Reflection.Emit.OpCodes.Ble_S:
                case System.Reflection.Emit.OpCode c161 when c161 == System.Reflection.Emit.OpCodes.Ble_Un:
                case System.Reflection.Emit.OpCode c162 when c162 == System.Reflection.Emit.OpCodes.Ble_Un_S:
                case System.Reflection.Emit.OpCode c163 when c163 == System.Reflection.Emit.OpCodes.Blt:
                case System.Reflection.Emit.OpCode c164 when c164 == System.Reflection.Emit.OpCodes.Blt_S:
                case System.Reflection.Emit.OpCode c165 when c165 == System.Reflection.Emit.OpCodes.Blt_Un:
                case System.Reflection.Emit.OpCode c166 when c166 == System.Reflection.Emit.OpCodes.Blt_Un_S:
                case System.Reflection.Emit.OpCode c167 when c167 == System.Reflection.Emit.OpCodes.Bne_Un:
                case System.Reflection.Emit.OpCode c168 when c168 == System.Reflection.Emit.OpCodes.Bne_Un_S:
                case System.Reflection.Emit.OpCode c169 when c169 == System.Reflection.Emit.OpCodes.Box:
                case System.Reflection.Emit.OpCode c170 when c170 == System.Reflection.Emit.OpCodes.Conv_Ovf_I2_Un:
                case System.Reflection.Emit.OpCode c171 when c171 == System.Reflection.Emit.OpCodes.Ldelem_I:
                case System.Reflection.Emit.OpCode c172 when c172 == System.Reflection.Emit.OpCodes.Conv_Ovf_I4:
                case System.Reflection.Emit.OpCode c173 when c173 == System.Reflection.Emit.OpCodes.Conv_Ovf_I8:
                case System.Reflection.Emit.OpCode c174 when c174 == System.Reflection.Emit.OpCodes.Jmp:
                case System.Reflection.Emit.OpCode c175 when c175 == System.Reflection.Emit.OpCodes.Ldarg:
                case System.Reflection.Emit.OpCode c176 when c176 == System.Reflection.Emit.OpCodes.Ldarg_0:
                case System.Reflection.Emit.OpCode c177 when c177 == System.Reflection.Emit.OpCodes.Ldarg_1:
                case System.Reflection.Emit.OpCode c178 when c178 == System.Reflection.Emit.OpCodes.Ldarg_2:
                case System.Reflection.Emit.OpCode c179 when c179 == System.Reflection.Emit.OpCodes.Ldarg_3:
                case System.Reflection.Emit.OpCode c180 when c180 == System.Reflection.Emit.OpCodes.Ldarg_S:
                case System.Reflection.Emit.OpCode c181 when c181 == System.Reflection.Emit.OpCodes.Ldarga:
                case System.Reflection.Emit.OpCode c182 when c182 == System.Reflection.Emit.OpCodes.Ldarga_S:
                case System.Reflection.Emit.OpCode c183 when c183 == System.Reflection.Emit.OpCodes.Ldc_I4:
                case System.Reflection.Emit.OpCode c184 when c184 == System.Reflection.Emit.OpCodes.Ldc_I4_0:
                case System.Reflection.Emit.OpCode c185 when c185 == System.Reflection.Emit.OpCodes.Isinst:
                case System.Reflection.Emit.OpCode c186 when c186 == System.Reflection.Emit.OpCodes.Ldc_I4_1:
                case System.Reflection.Emit.OpCode c187 when c187 == System.Reflection.Emit.OpCodes.Ldc_I4_3:
                case System.Reflection.Emit.OpCode c188 when c188 == System.Reflection.Emit.OpCodes.Ldc_I4_4:
                case System.Reflection.Emit.OpCode c189 when c189 == System.Reflection.Emit.OpCodes.Ldc_I4_5:
                case System.Reflection.Emit.OpCode c190 when c190 == System.Reflection.Emit.OpCodes.Ldc_I4_6:
                case System.Reflection.Emit.OpCode c191 when c191 == System.Reflection.Emit.OpCodes.Ldc_I4_7:
                case System.Reflection.Emit.OpCode c192 when c192 == System.Reflection.Emit.OpCodes.Ldc_I4_8:
                case System.Reflection.Emit.OpCode c193 when c193 == System.Reflection.Emit.OpCodes.Ldc_I4_M1:
                case System.Reflection.Emit.OpCode c194 when c194 == System.Reflection.Emit.OpCodes.Ldc_I4_S:
                case System.Reflection.Emit.OpCode c195 when c195 == System.Reflection.Emit.OpCodes.Ldc_I8:
                case System.Reflection.Emit.OpCode c196 when c196 == System.Reflection.Emit.OpCodes.Ldc_R4:
                case System.Reflection.Emit.OpCode c197 when c197 == System.Reflection.Emit.OpCodes.Ldc_R8:
                case System.Reflection.Emit.OpCode c198 when c198 == System.Reflection.Emit.OpCodes.Ldc_I4_2:
                case System.Reflection.Emit.OpCode c199 when c199 == System.Reflection.Emit.OpCodes.Initobj:
                case System.Reflection.Emit.OpCode c200 when c200 == System.Reflection.Emit.OpCodes.Initblk:
                case System.Reflection.Emit.OpCode c201 when c201 == System.Reflection.Emit.OpCodes.Endfinally:
                case System.Reflection.Emit.OpCode c202 when c202 == System.Reflection.Emit.OpCodes.Conv_Ovf_I8_Un:
                case System.Reflection.Emit.OpCode c203 when c203 == System.Reflection.Emit.OpCodes.Conv_Ovf_U:
                case System.Reflection.Emit.OpCode c204 when c204 == System.Reflection.Emit.OpCodes.Conv_Ovf_U_Un:
                case System.Reflection.Emit.OpCode c205 when c205 == System.Reflection.Emit.OpCodes.Conv_Ovf_U1:
                case System.Reflection.Emit.OpCode c206 when c206 == System.Reflection.Emit.OpCodes.Conv_Ovf_U1_Un:
                case System.Reflection.Emit.OpCode c207 when c207 == System.Reflection.Emit.OpCodes.Conv_Ovf_U2:
                case System.Reflection.Emit.OpCode c208 when c208 == System.Reflection.Emit.OpCodes.Conv_Ovf_U2_Un:
                case System.Reflection.Emit.OpCode c209 when c209 == System.Reflection.Emit.OpCodes.Conv_Ovf_U4:
                case System.Reflection.Emit.OpCode c210 when c210 == System.Reflection.Emit.OpCodes.Conv_Ovf_U4_Un:
                case System.Reflection.Emit.OpCode c211 when c211 == System.Reflection.Emit.OpCodes.Conv_Ovf_U8:
                case System.Reflection.Emit.OpCode c212 when c212 == System.Reflection.Emit.OpCodes.Conv_Ovf_U8_Un:
                case System.Reflection.Emit.OpCode c213 when c213 == System.Reflection.Emit.OpCodes.Conv_R_Un:
                case System.Reflection.Emit.OpCode c214 when c214 == System.Reflection.Emit.OpCodes.Conv_R4:
                case System.Reflection.Emit.OpCode c215 when c215 == System.Reflection.Emit.OpCodes.Conv_R8:
                case System.Reflection.Emit.OpCode c216 when c216 == System.Reflection.Emit.OpCodes.Conv_U:
                case System.Reflection.Emit.OpCode c217 when c217 == System.Reflection.Emit.OpCodes.Conv_U1:
                case System.Reflection.Emit.OpCode c218 when c218 == System.Reflection.Emit.OpCodes.Conv_U2:
                case System.Reflection.Emit.OpCode c219 when c219 == System.Reflection.Emit.OpCodes.Conv_U4:
                case System.Reflection.Emit.OpCode c220 when c220 == System.Reflection.Emit.OpCodes.Conv_U8:
                case System.Reflection.Emit.OpCode c221 when c221 == System.Reflection.Emit.OpCodes.Cpblk:
                case System.Reflection.Emit.OpCode c222 when c222 == System.Reflection.Emit.OpCodes.Cpobj:
                case System.Reflection.Emit.OpCode c223 when c223 == System.Reflection.Emit.OpCodes.Div:
                case System.Reflection.Emit.OpCode c224 when c224 == System.Reflection.Emit.OpCodes.Div_Un:
                case System.Reflection.Emit.OpCode c225 when c225 == System.Reflection.Emit.OpCodes.Dup:
                case System.Reflection.Emit.OpCode c226 when c226 == System.Reflection.Emit.OpCodes.Endfilter:
                case System.Reflection.Emit.OpCode c227 when c227 == System.Reflection.Emit.OpCodes.Conv_Ovf_I4_Un:

                default:
                    break;

            }

            return currentInstruction;


        }




        private int ReadInt32()
        {
            int position = this._position;
            this._position += 4;
            return BitConverter.ToInt32(this._byteArray, position);
        }

        private long ReadInt64()
        {
            int position = this._position;
            this._position += 8;
            return BitConverter.ToInt64(this._byteArray, position);
        }

        private sbyte ReadSByte()
        {
            return (sbyte)this.ReadByte();
        }

        private float ReadSingle()
        {
            int position = this._position;
            this._position += 4;
            return BitConverter.ToSingle(this._byteArray, position);
        }

        private ushort ReadUInt16()
        {
            int position = this._position;
            this._position += 2;
            return BitConverter.ToUInt16(this._byteArray, position);
        }

        private uint ReadUInt32()
        {
            int position = this._position;
            this._position += 4;
            return BitConverter.ToUInt32(this._byteArray, position);
        }

        private ulong ReadUInt64()
        {
            int position = this._position;
            this._position += 8;
            return BitConverter.ToUInt64(this._byteArray, position);
        }

        private byte ReadByte()
        {
            return this._byteArray[this._position++];
        }

        private double ReadDouble()
        {
            int position = this._position;
            this._position += 8;
            return BitConverter.ToDouble(this._byteArray, position);
        }


    }

}
