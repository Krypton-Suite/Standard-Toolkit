// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Canonical home: Krypton.Interop (ProjectReference). See Documents/Development/Cross-Project-Source-Linking.md

namespace System.Diagnostics.CodeAnalysis;
#if NETFRAMEWORK  // https://github.com/dotnet/designs/blob/main/accepted/2020/net5/net5.md#preprocessor-symbols
    /// <summary>Specifies that <see langword="null" /> is allowed as an input even if the corresponding type disallows it.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
    internal sealed class AllowNullAttribute : Attribute
    {
    }

    /// <summary>Specifies that <see langword="null" /> is disallowed as an input even if the corresponding type allows it.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
    internal sealed class DisallowNullAttribute : Attribute
    {
    }

    /// <summary>Specifies that an output may be <see langword="null" /> even if the corresponding type disallows it.</summary>
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue,
        Inherited = false)]
    public sealed class MaybeNullAttribute : Attribute
    {
    }

    /// <summary>Specifies that an output is not <see langword="null" /> even if the corresponding type allows it. Specifies that an input argument was not <see langword="null" /> when the call returns.</summary>
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue,
        Inherited = false)]
    public sealed class NotNullAttribute : Attribute
    {
    }

    /// <summary>
    /// Specifies that the output will not be null when the named parameter is <see langword="true"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public sealed class NotNullWhenAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="NotNullWhenAttribute"/> class.</summary>
        /// <param name="returnValue">The return value condition that guarantees a non-null output.</param>
        public NotNullWhenAttribute(bool returnValue) => ReturnValue = returnValue;

        /// <summary>Gets the return value condition.</summary>
        public bool ReturnValue { get; }
    }

    /// <summary>Applied to a method that will never return under any circumstance.</summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class DoesNotReturnAttribute : Attribute
    {
    }

    /// <summary>Specifies that the method will not return if the associated Boolean parameter has the specified value.</summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public sealed class DoesNotReturnIfAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="DoesNotReturnIfAttribute"/> class.</summary>
        /// <param name="parameterValue">The condition parameter value that triggers an early exit / no return.</param>
        public DoesNotReturnIfAttribute(bool parameterValue) => ParameterValue = parameterValue;

        /// <summary>Gets the condition parameter value that causes the method not to return.</summary>
        public bool ParameterValue { get; }
    }

#endif