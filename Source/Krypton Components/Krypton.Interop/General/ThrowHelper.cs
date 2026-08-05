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
/// smaller and more likely to be inlined. Throw helpers also use <see cref="MethodImplOptions.NoInlining"/>
/// and <see cref="StackTraceHiddenAttribute"/> so the cold path stays out of the hot method and stack traces.
/// Prefer <see cref="ThrowArgumentNullException(string?)"/> /
/// <see cref="ThrowIfNull"/> for new validation. Use <see cref="ThrowNullReferenceException(string?)"/>
/// only when preserving an existing <see cref="NullReferenceException"/> contract
/// (for example with <see cref="SharedStaticFunctions"/> messages).
/// Generic <c>T</c> overloads exist for expression contexts (<c>??</c>, switch expression arms).
/// Visible to sibling assemblies via <c>InternalsVisibleTo</c>; not part of the public API surface.
/// </remarks>
[StackTraceHidden]
internal static class ThrowHelper
{
    #region Null checks
    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> when <paramref name="argument"/> is <see langword="null"/>.
    /// </summary>
    /// <param name="argument">The argument to validate.</param>
    /// <param name="paramName">The parameter name; captured from the call site when omitted.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
        {
            ThrowArgumentNull(paramName);
        }
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowArgumentNull(string? paramName) =>
        throw new ArgumentNullException(paramName);

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> with no parameter name.
    /// </summary>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowArgumentNullException() =>
        throw new ArgumentNullException();

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> for the specified parameter name.
    /// </summary>
    /// <param name="paramName">The name of the parameter that was <see langword="null"/>.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowArgumentNullException(string? paramName) =>
        ThrowArgumentNull(paramName);

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> with a parameter name and message.
    /// </summary>
    /// <param name="paramName">The name of the parameter that was <see langword="null"/>.</param>
    /// <param name="message">The error message.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowArgumentNullException(string? paramName, string? message) =>
        throw new ArgumentNullException(paramName, message);

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> with no parameter name for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowArgumentNullException<T>() =>
        throw new ArgumentNullException();

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> and satisfies expression forms such as
    /// <c>value ?? ThrowHelper.ThrowArgumentNullException&lt;T&gt;(nameof(value))</c>.
    /// </summary>
    /// <typeparam name="T">The expected non-null type of the expression.</typeparam>
    /// <param name="paramName">The name of the parameter that was <see langword="null"/>.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowArgumentNullException<T>(string? paramName) =>
        throw new ArgumentNullException(paramName);

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> with a message for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected non-null type of the expression.</typeparam>
    /// <param name="paramName">The name of the parameter that was <see langword="null"/>.</param>
    /// <param name="message">The error message.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowArgumentNullException<T>(string? paramName, string? message) =>
        throw new ArgumentNullException(paramName, message);
    #endregion

    #region Argument
    /// <summary>
    /// Throws <see cref="ArgumentException"/> with the specified message and parameter name.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="paramName">The name of the parameter that caused the exception.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowArgumentException(string? message, string? paramName) =>
        throw new ArgumentException(message, paramName);

    /// <summary>
    /// Throws <see cref="ArgumentException"/> with the specified message.
    /// </summary>
    /// <param name="message">The error message.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowArgumentException(string? message) =>
        throw new ArgumentException(message);

    /// <summary>
    /// Throws <see cref="ArgumentException"/> for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="message">The error message.</param>
    /// <param name="paramName">The name of the parameter that caused the exception.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowArgumentException<T>(string? message, string? paramName) =>
        throw new ArgumentException(message, paramName);

    /// <summary>
    /// Throws <see cref="ArgumentException"/> for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="message">The error message.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowArgumentException<T>(string? message) =>
        throw new ArgumentException(message);
    #endregion

    #region Argument out of range
    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> with no parameter name.
    /// </summary>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowArgumentOutOfRangeException() =>
        throw new ArgumentOutOfRangeException();

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> for the specified parameter name.
    /// </summary>
    /// <param name="paramName">The name of the parameter that was out of range.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowArgumentOutOfRangeException(string? paramName) =>
        throw new ArgumentOutOfRangeException(paramName);

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> with a parameter name and message.
    /// </summary>
    /// <param name="paramName">The name of the parameter that was out of range.</param>
    /// <param name="message">The error message.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowArgumentOutOfRangeException(string? paramName, string? message) =>
        throw new ArgumentOutOfRangeException(paramName, message);

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> with an actual value and message.
    /// </summary>
    /// <param name="paramName">The name of the parameter that was out of range.</param>
    /// <param name="actualValue">The value of the argument that caused the exception.</param>
    /// <param name="message">The error message.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowArgumentOutOfRangeException(string? paramName, object? actualValue, string? message) =>
        throw new ArgumentOutOfRangeException(paramName, actualValue, message);

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> for expression forms with no arguments.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowArgumentOutOfRangeException<T>() =>
        throw new ArgumentOutOfRangeException();

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> for expression forms (e.g. switch arms).
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="paramName">The name of the parameter that was out of range.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowArgumentOutOfRangeException<T>(string? paramName) =>
        throw new ArgumentOutOfRangeException(paramName);

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> for expression forms with a message.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="paramName">The name of the parameter that was out of range.</param>
    /// <param name="message">The error message.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowArgumentOutOfRangeException<T>(string? paramName, string? message) =>
        throw new ArgumentOutOfRangeException(paramName, message);

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> for expression forms with an actual value and message.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="paramName">The name of the parameter that was out of range.</param>
    /// <param name="actualValue">The value of the argument that caused the exception.</param>
    /// <param name="message">The error message.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowArgumentOutOfRangeException<T>(string? paramName, object? actualValue, string? message) =>
        throw new ArgumentOutOfRangeException(paramName, actualValue, message);
    #endregion

    #region Invalid operation
    /// <summary>
    /// Throws <see cref="InvalidOperationException"/> with the specified message.
    /// </summary>
    /// <param name="message">The error message.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException(string? message) =>
        throw new InvalidOperationException(message);

    /// <summary>
    /// Throws <see cref="InvalidOperationException"/> for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="message">The error message.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowInvalidOperationException<T>(string? message) =>
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
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowNullReferenceException(string? message) =>
        throw new NullReferenceException(message);

    /// <summary>
    /// Throws <see cref="NullReferenceException"/> for use in expressions that require a typed result.
    /// </summary>
    /// <typeparam name="T">The expected non-null type of the expression.</typeparam>
    /// <param name="message">The error message (often from <see cref="SharedStaticFunctions"/>).</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowNullReferenceException<T>(string? message) =>
        throw new NullReferenceException(message);
    #endregion

    #region Not supported
    /// <summary>
    /// Throws <see cref="NotSupportedException"/> with no message.
    /// </summary>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowNotSupportedException() =>
        throw new NotSupportedException();

    /// <summary>
    /// Throws <see cref="NotSupportedException"/> with the specified message.
    /// </summary>
    /// <param name="message">The error message.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowNotSupportedException(string? message) =>
        throw new NotSupportedException(message);

    /// <summary>
    /// Throws <see cref="NotSupportedException"/> for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowNotSupportedException<T>() =>
        throw new NotSupportedException();

    /// <summary>
    /// Throws <see cref="NotSupportedException"/> for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="message">The error message.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowNotSupportedException<T>(string? message) =>
        throw new NotSupportedException(message);
    #endregion

    #region Not implemented
    /// <summary>
    /// Throws <see cref="NotImplementedException"/> with no message.
    /// </summary>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowNotImplementedException() =>
        throw new NotImplementedException();

    /// <summary>
    /// Throws <see cref="NotImplementedException"/> with the specified message.
    /// </summary>
    /// <param name="message">The error message.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowNotImplementedException(string? message) =>
        throw new NotImplementedException(message);

    /// <summary>
    /// Throws <see cref="NotImplementedException"/> for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowNotImplementedException<T>() =>
        throw new NotImplementedException();

    /// <summary>
    /// Throws <see cref="NotImplementedException"/> for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="message">The error message.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowNotImplementedException<T>(string? message) =>
        throw new NotImplementedException(message);
    #endregion

    #region Object disposed
    /// <summary>
    /// Throws <see cref="ObjectDisposedException"/> for the specified object name.
    /// </summary>
    /// <param name="objectName">The name of the disposed object.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowObjectDisposedException(string? objectName) =>
        throw new ObjectDisposedException(objectName);

    /// <summary>
    /// Throws <see cref="ObjectDisposedException"/> for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="objectName">The name of the disposed object.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowObjectDisposedException<T>(string? objectName) =>
        throw new ObjectDisposedException(objectName);
    #endregion

    #region Invalid cast
    /// <summary>
    /// Throws <see cref="InvalidCastException"/> with no message.
    /// </summary>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidCastException() =>
        throw new InvalidCastException();

    /// <summary>
    /// Throws <see cref="InvalidCastException"/> with the specified message.
    /// </summary>
    /// <param name="message">The error message.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidCastException(string? message) =>
        throw new InvalidCastException(message);

    /// <summary>
    /// Throws <see cref="InvalidCastException"/> for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowInvalidCastException<T>() =>
        throw new InvalidCastException();

    /// <summary>
    /// Throws <see cref="InvalidCastException"/> for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="message">The error message.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowInvalidCastException<T>(string? message) =>
        throw new InvalidCastException(message);
    #endregion

    #region Win32
    /// <summary>
    /// Throws <see cref="Win32Exception"/> using the last Win32 error.
    /// </summary>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowWin32Exception() =>
        throw new Win32Exception();

    /// <summary>
    /// Throws <see cref="Win32Exception"/> for the specified error code.
    /// </summary>
    /// <param name="error">The Win32 error code.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowWin32Exception(int error) =>
        throw new Win32Exception(error);

    /// <summary>
    /// Throws <see cref="Win32Exception"/> with a message (uses the last Win32 error).
    /// </summary>
    /// <param name="message">The error message.</param>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowWin32Exception(string? message) =>
        throw new Win32Exception(message);

    /// <summary>
    /// Throws <see cref="Win32Exception"/> for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowWin32Exception<T>() =>
        throw new Win32Exception();

    /// <summary>
    /// Throws <see cref="Win32Exception"/> for expression forms.
    /// </summary>
    /// <typeparam name="T">The expected type of the expression.</typeparam>
    /// <param name="error">The Win32 error code.</param>
    /// <returns>Never returns; the return type exists only for use in expressions.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ThrowWin32Exception<T>(int error) =>
        throw new Win32Exception(error);
    #endregion
}
