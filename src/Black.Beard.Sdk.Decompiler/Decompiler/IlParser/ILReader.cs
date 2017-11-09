using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// ILReader
    /// </summary>
    public sealed class ILReader : IEnumerable<ILInstruction>, IEnumerable
    {
        
        static ILReader()
        {
            foreach (FieldInfo info in typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                OpCode code = (OpCode) info.GetValue(null);
                ushort index = (ushort) code.Value;
                if (index < 0x100)
                    s_OneByteOpCodes[index] = code;
                else if ((index & 0xff00) == 0xfe00)
                    s_TwoByteOpCodes[index & 0xff] = code;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ILReader"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <exception cref="System.ArgumentNullException">method</exception>
        /// <exception cref="System.ArgumentException">method must be RuntimeMethodInfo or RuntimeConstructorInfo for this constructor.</exception>
        public ILReader(MethodBase method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            Type type = method.GetType();
            if ((type != s_runtimeMethodInfoType) && (type != s_runtimeConstructorInfoType))
                throw new ArgumentException("method must be RuntimeMethodInfo or RuntimeConstructorInfo for this constructor.");

            this.m_ilProvider = new MethodBaseILProvider(method);
            this.m_resolver = new ModuleScopeTokenResolver(method);
            this.m_byteArray = this.m_ilProvider.GetByteArray();
            this.m_position = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ILReader"/> class.
        /// </summary>
        /// <param name="ilProvider">The il provider.</param>
        /// <param name="tokenResolver">The token resolver.</param>
        /// <exception cref="System.ArgumentNullException">ilProvider</exception>
        public ILReader(IILProvider ilProvider, ITokenResolver tokenResolver)
        {
            this.m_resolver = tokenResolver;
            this.m_ilProvider = ilProvider ?? throw new ArgumentNullException("ilProvider");
            this.m_byteArray = this.m_ilProvider.GetByteArray();
            this.m_position = 0;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <exception cref="System.ArgumentNullException">argument 'visitor' can not be null</exception>
        public void Accept(ILInstructionVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("argument 'visitor' can not be null");
            foreach (ILInstruction instruction in this)
                instruction.Accept(visitor);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<ILInstruction> GetEnumerator()
        {
            while (this.m_position < this.m_byteArray.Length)
                yield return this.Next();
            this.m_position = 0;
        }

        private ILInstruction Next()
        {
            int position = this.m_position;
            OpCode nop = OpCodes.Nop;
            byte index = this.ReadByte();
            if (index != 0xfe)
                nop = s_OneByteOpCodes[index];
            else
            {
                index = this.ReadByte();
                nop = s_TwoByteOpCodes[index];
            }
            switch (nop.OperandType)
            {
                case OperandType.InlineBrTarget:
                    return new InlineBrTargetInstruction(position, nop, this.ReadInt32());

                case OperandType.InlineField:
                    return new InlineFieldInstruction(this.m_resolver, position, nop, this.ReadInt32());

                case OperandType.InlineI:
                    return new InlineIInstruction(position, nop, this.ReadInt32());

                case OperandType.InlineI8:
                    return new InlineI8Instruction(position, nop, this.ReadInt64());

                case OperandType.InlineMethod:
                    return new InlineMethodInstruction(position, nop, this.ReadInt32(), this.m_resolver);

                case OperandType.InlineNone:
                    return new InlineNoneInstruction(position, nop);

                case OperandType.InlineR:
                    return new InlineRInstruction(position, nop, this.ReadDouble());

                case OperandType.InlineSig:
                    return new InlineSigInstruction(position, nop, this.ReadInt32(), this.m_resolver);

                case OperandType.InlineString:
                    return new InlineStringInstruction(position, nop, this.ReadInt32(), this.m_resolver);

                case OperandType.InlineSwitch:
                {
                    int num13 = this.ReadInt32();
                    int[] deltas = new int[num13];
                    for (int i = 0; i < num13; i++)
                        deltas[i] = this.ReadInt32();
                    return new InlineSwitchInstruction(position, nop, deltas);
                }
                case OperandType.InlineTok:
                    return new InlineTokInstruction(position, nop, this.ReadInt32(), this.m_resolver);

                case OperandType.InlineType:
                    return new InlineTypeInstruction(position, nop, this.ReadInt32(), this.m_resolver);

                case OperandType.InlineVar:
                    return new InlineVarInstruction(position, nop, this.ReadUInt16());

                case OperandType.ShortInlineBrTarget:
                    return new ShortInlineBrTargetInstruction(position, nop, this.ReadSByte());

                case OperandType.ShortInlineI:
                    return new ShortInlineIInstruction(position, nop, this.ReadByte());

                case OperandType.ShortInlineR:
                    return new ShortInlineRInstruction(position, nop, this.ReadSingle());

                case OperandType.ShortInlineVar:
                    return new ShortInlineVarInstruction(position, nop, this.ReadByte());
            }
            throw new BadImageFormatException("unexpected OperandType " + nop.OperandType);
        }

        private byte ReadByte()
        {
            return this.m_byteArray[this.m_position++];
        }

        private double ReadDouble()
        {
            int position = this.m_position;
            this.m_position += 8;
            return BitConverter.ToDouble(this.m_byteArray, position);
        }

        private int ReadInt32()
        {
            int position = this.m_position;
            this.m_position += 4;
            return BitConverter.ToInt32(this.m_byteArray, position);
        }

        private long ReadInt64()
        {
            int position = this.m_position;
            this.m_position += 8;
            return BitConverter.ToInt64(this.m_byteArray, position);
        }

        private sbyte ReadSByte()
        {
            return (sbyte) this.ReadByte();
        }

        private float ReadSingle()
        {
            int position = this.m_position;
            this.m_position += 4;
            return BitConverter.ToSingle(this.m_byteArray, position);
        }

        private ushort ReadUInt16()
        {
            int position = this.m_position;
            this.m_position += 2;
            return BitConverter.ToUInt16(this.m_byteArray, position);
        }

        private uint ReadUInt32()
        {
            int position = this.m_position;
            this.m_position += 4;
            return BitConverter.ToUInt32(this.m_byteArray, position);
        }

        private ulong ReadUInt64()
        {
            int position = this.m_position;
            this.m_position += 8;
            return BitConverter.ToUInt64(this.m_byteArray, position);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        private byte[] m_byteArray;
        private IILProvider m_ilProvider;
        private int m_position;
        private ITokenResolver m_resolver;
        private static OpCode[] s_OneByteOpCodes = new OpCode[0x100];
        private static Type s_runtimeConstructorInfoType = Type.GetType("System.Reflection.RuntimeConstructorInfo");
        private static Type s_runtimeMethodInfoType = Type.GetType("System.Reflection.RuntimeMethodInfo");
        private static OpCode[] s_TwoByteOpCodes = new OpCode[0x100];

    }
}
