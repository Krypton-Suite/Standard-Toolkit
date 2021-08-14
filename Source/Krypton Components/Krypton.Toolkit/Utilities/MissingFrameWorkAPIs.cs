// *****************************************************************************
// BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit)
// by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2021. All rights reserved. 
//  Version 6.0.0  
// *****************************************************************************

namespace Krypton.Toolkit
{
    public static class MissingFrameWorkAPIs
    {
        /// <summary>
        /// Optimised Framework independent replacement for string.IsNullOrWhitespace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
// https://github.com/dotnet/designs/blob/main/accepted/2020/or-greater-defines/or-greater-defines.md
#if NET35 || NET40
#else // NET45_OR_GREATER || etc.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsNullOrWhiteSpace(string value)
        {
            // https://github.com/dotnet/runtime/issues/4207
            // And do not allocate string from trim !

            if (value == null) return true;

            // ReSharper disable once ForCanBeConvertedToForeach
            // ReSharper disable once LoopCanBeConvertedToQuery - use optimisation from array bounds
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                    return false;
            }

            return true;

        }

#if NET35 || NET40
#else // NET45_OR_GREATER || etc.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool HasFlag<T>(T source, T flags) where T : Enum
        {
            // Stolen from .net45 code base
            static ulong ToUInt64(object value)
            {
#pragma warning disable IDE0066 // Convert switch statement to expression
                // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                switch (Convert.GetTypeCode(value))
#pragma warning restore IDE0066 // Convert switch statement to expression
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.Byte:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        return Convert.ToUInt64(value, (IFormatProvider)CultureInfo.InvariantCulture);
                    case TypeCode.SByte:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                        return (ulong)Convert.ToInt64(value, (IFormatProvider)CultureInfo.InvariantCulture);
                    default:
                        throw new InvalidOperationException("InvalidOperation_UnknownEnumType");
                }
            }
            var s1 = ToUInt64(source);
            var f1 = ToUInt64(flags);
            return (s1 & f1) == f1;
        }

        private static class EmptyArray<T>
        {
#pragma warning disable CA1825 // this is the implementation of Array.Empty<T>()
            internal static readonly T[] Value = new T[0];
#pragma warning restore CA1825
        }

#if NET35 || NET40
#else // NET45_OR_GREATER || etc.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static T[] Array_Empty<T>()
        {
            return EmptyArray<T>.Value;
        }

    }
}

