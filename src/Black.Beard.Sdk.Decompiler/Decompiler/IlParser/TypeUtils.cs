using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Sdk.Decompiler.IlParser
{

    public static class TypeUtils
    {
        public const int NativeIntSize = 6; // between 4 (Int32) and 8 (Int64)

        ///// <summary>
        ///// Gets the size (in bytes) of the input type.
        ///// Returns <c>NativeIntSize</c> for pointer-sized types.
        ///// Returns 0 for structs and other types of unknown size.
        ///// </summary>
        //public static int GetSize(this IType type)
        //{
        //    switch (type.Kind)
        //    {
        //        case TypeKind.Pointer:
        //        case TypeKind.ByReference:
        //        case TypeKind.Class:
        //            return NativeIntSize;
        //        case TypeKind.Enum:
        //            type = type.GetEnumUnderlyingType();
        //            break;
        //    }

        //    var typeDef = type.GetDefinition();
        //    if (typeDef == null)
        //        return 0;
        //    switch (typeDef.KnownTypeCode)
        //    {
        //        case KnownTypeCode.Boolean:
        //        case KnownTypeCode.SByte:
        //        case KnownTypeCode.Byte:
        //            return 1;
        //        case KnownTypeCode.Char:
        //        case KnownTypeCode.Int16:
        //        case KnownTypeCode.UInt16:
        //            return 2;
        //        case KnownTypeCode.Int32:
        //        case KnownTypeCode.UInt32:
        //        case KnownTypeCode.Single:
        //            return 4;
        //        case KnownTypeCode.IntPtr:
        //        case KnownTypeCode.UIntPtr:
        //            return NativeIntSize;
        //        case KnownTypeCode.Int64:
        //        case KnownTypeCode.UInt64:
        //        case KnownTypeCode.Double:
        //            return 8;
        //    }
        //    return 0;
        //}

        ///// <summary>
        ///// Gets the size of the input stack type.
        ///// </summary>
        ///// <returns>
        ///// * 4 for <c>I4</c>,
        ///// * 8 for <c>I8</c>,
        ///// * <c>NativeIntSize</c> for <c>I</c> and <c>Ref</c>,
        ///// * 0 otherwise (O, F, Void, Unknown).
        ///// </returns>
        //public static int GetSize(this StackType type)
        //{
        //    switch (type)
        //    {
        //        case StackType.I4:
        //            return 4;
        //        case StackType.I8:
        //            return 8;
        //        case StackType.I:
        //        case StackType.Ref:
        //            return NativeIntSize;
        //        default:
        //            return 0;
        //    }
        //}

        //public static IType GetLargerType(IType type1, IType type2)
        //{
        //    return GetSize(type1) >= GetSize(type2) ? type1 : type2;
        //}

        ///// <summary>
        ///// Gets whether the type is a small integer type.
        ///// Small integer types are:
        ///// * bool, sbyte, byte, char, short, ushort
        ///// * any enums that have a small integer type as underlying type
        ///// </summary>
        //public static bool IsSmallIntegerType(this IType type)
        //{
        //    int size = GetSize(type);
        //    return size > 0 && size < 4;
        //}

        ///// <summary>
        ///// Gets whether the type is a C# small integer type: byte, sbyte, short or ushort.
        ///// 
        ///// Unlike the ILAst, C# does not consider bool or enums to be small integers.
        ///// </summary>
        //public static bool IsCSharpSmallIntegerType(this IType type)
        //{
        //    switch (type.GetDefinition()?.KnownTypeCode)
        //    {
        //        case KnownTypeCode.Byte:
        //        case KnownTypeCode.SByte:
        //        case KnownTypeCode.Int16:
        //        case KnownTypeCode.UInt16:
        //            return true;
        //        default:
        //            return false;
        //    }
        //}

        ///// <summary>
        ///// Gets whether the type is a C# primitive integer type: byte, sbyte, short, ushort, int, uint, long and ulong.
        ///// 
        ///// Unlike the ILAst, C# does not consider bool, enums, pointers or IntPtr to be integers.
        ///// </summary>
        //public static bool IsCSharpPrimitiveIntegerType(this IType type)
        //{
        //    switch (type.GetDefinition()?.KnownTypeCode)
        //    {
        //        case KnownTypeCode.Byte:
        //        case KnownTypeCode.SByte:
        //        case KnownTypeCode.Int16:
        //        case KnownTypeCode.UInt16:
        //        case KnownTypeCode.Int32:
        //        case KnownTypeCode.UInt32:
        //        case KnownTypeCode.Int64:
        //        case KnownTypeCode.UInt64:
        //            return true;
        //        default:
        //            return false;
        //    }
        //}

        ///// <summary>
        ///// Gets whether the type is an IL integer type.
        ///// Returns true for I4, I, or I8.
        ///// </summary>
        //public static bool IsIntegerType(this StackType type)
        //{
        //    switch (type)
        //    {
        //        case StackType.I4:
        //        case StackType.I:
        //        case StackType.I8:
        //            return true;
        //        default:
        //            return false;
        //    }
        //}

        ///// <summary>
        ///// Gets whether reading/writing an element of accessType from the pointer
        ///// is equivalent to reading/writing an element of the pointer's element type.
        ///// </summary>
        ///// <remarks>
        ///// The access semantics may sligthly differ on read accesses of small integer types,
        ///// due to zero extension vs. sign extension when the signs differ.
        ///// </remarks>
        //public static bool IsCompatibleTypeForMemoryAccess(IType pointerType, IType accessType)
        //{
        //    IType memoryType;
        //    if (pointerType is PointerType || pointerType is ByReferenceType)
        //        memoryType = ((TypeWithElementType)pointerType).ElementType;
        //    else
        //        return false;
        //    if (memoryType.Equals(accessType))
        //        return true;
        //    // If the types are not equal, the access still might produce equal results in some cases:
        //    // 1) Both types are reference types
        //    if (memoryType.IsReferenceType == true && accessType.IsReferenceType == true)
        //        return true;
        //    // 2) Both types are integer types of equal size
        //    StackType memoryStackType = memoryType.GetStackType();
        //    StackType accessStackType = accessType.GetStackType();
        //    return memoryStackType == accessStackType && memoryStackType.IsIntegerType() && GetSize(memoryType) == GetSize(accessType);
        //}

        /// <summary>
        /// Gets the stack type corresponding to this type.
        /// </summary>
        public static StackType GetStackType(this Type type)
        {

            if (type.IsByRef)
                return StackType.Unknown;
            if (type.IsPointer)
                return StackType.I;
            if (type == typeof(bool))
                return StackType.I4;
            if (type == typeof(Boolean))
                return StackType.I4;
            if (type == typeof(Char))
                return StackType.I4;
            if (type == typeof(SByte))
                return StackType.I4;
            if (type == typeof(Byte))
                return StackType.I4;
            if (type == typeof(Int16))
                return StackType.I4;
            if (type == typeof(UInt16))
                return StackType.I4;
            if (type == typeof(Int32))
                return StackType.I4;
            if (type == typeof(UInt32))
                return StackType.I4;
            if (type == typeof(Int64))
                return StackType.I8;
            if (type == typeof(Int64))
                return StackType.I8;
            if (type == typeof(Single))
                return StackType.F;
            if (type == typeof(double))
                return StackType.F;
            if (type == typeof(void))
                return StackType.Void;
            if (type == typeof(IntPtr))
                return StackType.I;
            if (type == typeof(UIntPtr))
                return StackType.I;

            return StackType.O;

        }
    }

    ///// <summary>
    ///// If type is an enumeration type, returns the underlying type.
    ///// Otherwise, returns type unmodified.
    ///// </summary>
    //public static IType GetEnumUnderlyingType(this IType type)
    //{
    //    return (type.Kind == TypeKind.Enum) ? type.GetDefinition().EnumUnderlyingType : type;
    //}

    ///// <summary>
    ///// Gets the sign of the input type.
    ///// </summary>
    ///// <remarks>
    ///// Integer types (including IntPtr/UIntPtr) return the sign as expected.
    ///// Floating point types and <c>decimal</c> are considered to be signed.
    ///// <c>char</c>, <c>bool</c> and pointer types (e.g. <c>void*</c>) are unsigned.
    ///// Enums have a sign based on their underlying type.
    ///// All other types return <c>Sign.None</c>.
    ///// </remarks>
    //public static Sign GetSign(this IType type)
    //{
    //    if (type.Kind == TypeKind.Pointer)
    //        return Sign.Unsigned;
    //    var typeDef = type.GetEnumUnderlyingType().GetDefinition();
    //    if (typeDef == null)
    //        return Sign.None;
    //    switch (typeDef.KnownTypeCode)
    //    {
    //        case KnownTypeCode.SByte:
    //        case KnownTypeCode.Int16:
    //        case KnownTypeCode.Int32:
    //        case KnownTypeCode.Int64:
    //        case KnownTypeCode.IntPtr:
    //        case KnownTypeCode.Single:
    //        case KnownTypeCode.Double:
    //        case KnownTypeCode.Decimal:
    //            return Sign.Signed;
    //        case KnownTypeCode.UIntPtr:
    //        case KnownTypeCode.Char:
    //        case KnownTypeCode.Boolean:
    //        case KnownTypeCode.Byte:
    //        case KnownTypeCode.UInt16:
    //        case KnownTypeCode.UInt32:
    //        case KnownTypeCode.UInt64:
    //            return Sign.Unsigned;
    //        default:
    //            return Sign.None;
    //    }
    //}

    ///// <summary>
    ///// Maps the KnownTypeCode values to the corresponding PrimitiveTypes.
    ///// </summary>
    //public static PrimitiveType ToPrimitiveType(this KnownTypeCode knownTypeCode)
    //{
    //    switch (knownTypeCode)
    //    {
    //        case KnownTypeCode.SByte:
    //            return PrimitiveType.I1;
    //        case KnownTypeCode.Int16:
    //            return PrimitiveType.I2;
    //        case KnownTypeCode.Int32:
    //            return PrimitiveType.I4;
    //        case KnownTypeCode.Int64:
    //            return PrimitiveType.I8;
    //        case KnownTypeCode.Single:
    //            return PrimitiveType.R4;
    //        case KnownTypeCode.Double:
    //            return PrimitiveType.R8;
    //        case KnownTypeCode.Byte:
    //            return PrimitiveType.U1;
    //        case KnownTypeCode.UInt16:
    //        case KnownTypeCode.Char:
    //            return PrimitiveType.U2;
    //        case KnownTypeCode.UInt32:
    //            return PrimitiveType.U4;
    //        case KnownTypeCode.UInt64:
    //            return PrimitiveType.U8;
    //        case KnownTypeCode.IntPtr:
    //            return PrimitiveType.I;
    //        case KnownTypeCode.UIntPtr:
    //            return PrimitiveType.U;
    //        default:
    //            return PrimitiveType.None;
    //    }
    //}

    ///// <summary>
    ///// Maps the KnownTypeCode values to the corresponding PrimitiveTypes.
    ///// </summary>
    //public static PrimitiveType ToPrimitiveType(this IType type)
    //{
    //    var def = type.GetEnumUnderlyingType().GetDefinition();
    //    return def != null ? def.KnownTypeCode.ToPrimitiveType() : PrimitiveType.None;
    //}

    ///// <summary>
    ///// Maps the PrimitiveType values to the corresponding KnownTypeCodes.
    ///// </summary>
    //public static KnownTypeCode ToKnownTypeCode(this PrimitiveType primitiveType)
    //{
    //    switch (primitiveType)
    //    {
    //        case PrimitiveType.I1:
    //            return KnownTypeCode.SByte;
    //        case PrimitiveType.I2:
    //            return KnownTypeCode.Int16;
    //        case PrimitiveType.I4:
    //            return KnownTypeCode.Int32;
    //        case PrimitiveType.I8:
    //            return KnownTypeCode.Int64;
    //        case PrimitiveType.R4:
    //            return KnownTypeCode.Single;
    //        case PrimitiveType.R8:
    //            return KnownTypeCode.Double;
    //        case PrimitiveType.U1:
    //            return KnownTypeCode.Byte;
    //        case PrimitiveType.U2:
    //            return KnownTypeCode.UInt16;
    //        case PrimitiveType.U4:
    //            return KnownTypeCode.UInt32;
    //        case PrimitiveType.U8:
    //            return KnownTypeCode.UInt64;
    //        case PrimitiveType.I:
    //            return KnownTypeCode.IntPtr;
    //        case PrimitiveType.U:
    //            return KnownTypeCode.UIntPtr;
    //        default:
    //            return KnownTypeCode.None;
    //    }
    //}

    //public static KnownTypeCode ToKnownTypeCode(this StackType stackType, Sign sign = Sign.None)
    //{
    //    switch (stackType)
    //    {
    //        case StackType.I4:
    //            return sign == Sign.Unsigned ? KnownTypeCode.UInt32 : KnownTypeCode.Int32;
    //        case StackType.I8:
    //            return sign == Sign.Unsigned ? KnownTypeCode.UInt64 : KnownTypeCode.Int64;
    //        case StackType.I:
    //            return sign == Sign.Unsigned ? KnownTypeCode.UIntPtr : KnownTypeCode.IntPtr;
    //        case StackType.F:
    //            return KnownTypeCode.Double;
    //        case StackType.O:
    //            return KnownTypeCode.Object;
    //        case StackType.Void:
    //            return KnownTypeCode.Void;
    //        default:
    //            return KnownTypeCode.None;
    //    }
    //}


    public enum Sign : byte
    {
        None,
        Signed,
        Unsigned
    }

}
