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
#if NET35 || NET40
#else // NET45_OR_GREATER || CORE
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
    }
}
