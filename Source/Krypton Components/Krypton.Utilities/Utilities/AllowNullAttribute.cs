// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

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

#endif