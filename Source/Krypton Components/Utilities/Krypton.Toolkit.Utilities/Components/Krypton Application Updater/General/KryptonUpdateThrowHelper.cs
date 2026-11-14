#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Cold-path helpers for <see cref="KryptonUpdateException"/>. BCL throws still go through <see cref="Krypton.Shared.ThrowHelper"/>.
/// </summary>
[StackTraceHidden]
internal static class KryptonUpdateThrowHelper
{
    /// <summary>Throws <see cref="KryptonUpdateException"/> with a UI-safe message.</summary>
    /// <param name="message">Text safe to show in the UI.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Throw(string message) =>
        throw new KryptonUpdateException(message);

    /// <summary>Throws <see cref="KryptonUpdateException"/> with an inner cause.</summary>
    /// <param name="message">Text safe to show in the UI.</param>
    /// <param name="innerException">The underlying failure.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Throw(string message, Exception innerException) =>
        throw new KryptonUpdateException(message, innerException);

    /// <summary>Throws an HTTP failure. <paramref name="httpStatus"/> lets a range download tell 416 from other errors.</summary>
    /// <param name="message">Text safe to show in the UI.</param>
    /// <param name="httpStatus">Status code returned by the server.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Throw(string message, int httpStatus) =>
        throw new KryptonUpdateException(message, httpStatus);

    /// <summary>Throws <see cref="KryptonUpdateException"/> for expression forms (e.g. switch arms).</summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="message">Text safe to show in the UI.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T Throw<T>(string message) =>
        throw new KryptonUpdateException(message);

    /// <summary>Throws <see cref="KryptonUpdateException"/> with an inner cause for expression forms.</summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="message">Text safe to show in the UI.</param>
    /// <param name="innerException">The underlying failure.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T Throw<T>(string message, Exception innerException) =>
        throw new KryptonUpdateException(message, innerException);

    /// <summary>Throws an HTTP failure for expression forms.</summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="message">Text safe to show in the UI.</param>
    /// <param name="httpStatus">Status code returned by the server.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T Throw<T>(string message, int httpStatus) =>
        throw new KryptonUpdateException(message, httpStatus);
}
