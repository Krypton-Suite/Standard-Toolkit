#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Interop;

/// <summary>
/// Centralized throw helpers that keep exception construction on a cold path.
/// </summary>
/// <remarks>
/// Methods marked with <see cref="DoesNotReturnAttribute"/> only throw, which keeps calling methods
/// smaller and more likely to be inlined. Prefer <see cref="ThrowArgumentNullException(string?)"/> /
/// <see cref="ThrowIfNull"/> for new validation. Use <see cref="ThrowNullReferenceException(string?)"/>
/// only when preserving an existing <see cref="NullReferenceException"/> contract
/// (for example with <see cref="SharedStaticFunctions"/> messages).
/// Visible to sibling assemblies via <c>InternalsVisibleTo</c>; not part of the public API surface.
/// </remarks>
internal static class ThrowHelper
{
    #region Null checks
    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> when <paramref name="argument"/> is <see langword="null"/>.
    /// </summary>
    /// <param name="argument">The argument to validate.</param>
    /// <param name="paramName">The parameter name; captured from the call site when omitted.</param>
    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
        {
            ThrowArgumentNullException(paramName);
        }
    }

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> for the specified parameter name.
    /// </summary>
    /// <param name="paramName">The name of the parameter that was <see langword="null"/>.</param>
    [DoesNotReturn]
    public static void ThrowArgumentNullException(string? paramName) =>
        throw new ArgumentNullException(paramName);

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> and satisfies expression forms such as
    /// <c>value ?? ThrowHelper.ThrowArgumentNullException&lt;T&gt;(nameof(value))</c>.
    /// </summary>
    /// <typeparam name="T">The expected non-null type of the expression.</typeparam>
    /// <param name="paramName">The name of the parameter that was <see langword="null"/>.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    public static T ThrowArgumentNullException<T>(string? paramName) =>
        throw new ArgumentNullException(paramName);
    #endregion

    #region Argument out of range
    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> for the specified parameter name.
    /// </summary>
    /// <param name="paramName">The name of the parameter that was out of range.</param>
    [DoesNotReturn]
    public static void ThrowArgumentOutOfRangeException(string? paramName) =>
        throw new ArgumentOutOfRangeException(paramName);

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> with an actual value and message.
    /// </summary>
    /// <param name="paramName">The name of the parameter that was out of range.</param>
    /// <param name="actualValue">The value of the argument that caused the exception.</param>
    /// <param name="message">The error message.</param>
    [DoesNotReturn]
    public static void ThrowArgumentOutOfRangeException(string? paramName, object? actualValue, string? message) =>
        throw new ArgumentOutOfRangeException(paramName, actualValue, message);
    #endregion

    #region Invalid operation
    /// <summary>
    /// Throws <see cref="InvalidOperationException"/> with the specified message.
    /// </summary>
    /// <param name="message">The error message.</param>
    [DoesNotReturn]
    public static void ThrowInvalidOperationException(string? message) =>
        throw new InvalidOperationException(message);
    #endregion

    #region Null reference (legacy contracts)
    /// <summary>
    /// Throws <see cref="NullReferenceException"/> with the specified message.
    /// </summary>
    /// <param name="message">The error message (often from <see cref="SharedStaticFunctions"/>).</param>
    /// <remarks>
    /// Prefer <see cref="ThrowArgumentNullException(string?)"/> for new parameter validation.
    /// Use this only to preserve existing NRE behaviour at legacy call sites.
    /// </remarks>
    [DoesNotReturn]
    public static void ThrowNullReferenceException(string? message) =>
        throw new NullReferenceException(message);

    /// <summary>
    /// Throws <see cref="NullReferenceException"/> for use in expressions that require a typed result.
    /// </summary>
    /// <typeparam name="T">The expected non-null type of the expression.</typeparam>
    /// <param name="message">The error message (often from <see cref="SharedStaticFunctions"/>).</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    public static T ThrowNullReferenceException<T>(string? message) =>
        throw new NullReferenceException(message);
    #endregion
}
