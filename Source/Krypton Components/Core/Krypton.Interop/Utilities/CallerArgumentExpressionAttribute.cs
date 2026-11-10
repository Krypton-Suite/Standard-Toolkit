// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Canonical home: Krypton.Interop (ProjectReference). See Documents/Development/Cross-Project-Source-Linking.md

namespace System.Runtime.CompilerServices;
#if NETFRAMEWORK  // https://github.com/dotnet/designs/blob/main/accepted/2020/net5/net5.md#preprocessor-symbols
    /// <summary>
    /// Indicates that a parameter captures the expression passed for another parameter, including leading and trailing whitespace.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    internal sealed class CallerArgumentExpressionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallerArgumentExpressionAttribute"/> class.
        /// </summary>
        /// <param name="parameterName">The name of the parameter whose expression should be captured.</param>
        public CallerArgumentExpressionAttribute(string parameterName) => ParameterName = parameterName;

        /// <summary>Gets the name of the parameter whose expression should be captured.</summary>
        public string ParameterName { get; }
    }
#endif
