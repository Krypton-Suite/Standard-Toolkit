#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Named logger used by application code. Also implements <see cref="IKryptonLogger"/> so it can
/// be passed to splash screens and <see cref="KryptonLogger.SetLogger"/>.
/// </summary>
public interface IKryptonContextualLogger : IKryptonLogger
{
    /// <summary>Gets the category (source context) for this logger.</summary>
    string Category { get; }

    /// <summary>
    /// Returns whether events at <paramref name="level"/> will be written for this category.
    /// Call this before expensive message construction.
    /// </summary>
    /// <param name="level">The candidate severity.</param>
    bool IsEnabled(KryptonLogLevel level);

    /// <summary>Writes a pre-rendered message at <paramref name="level"/>.</summary>
    void Write(KryptonLogLevel level, string message);

    /// <summary>Writes a pre-rendered message and optional exception at <paramref name="level"/>.</summary>
    void Write(KryptonLogLevel level, Exception? exception, string message);

    /// <summary>Writes a message template with positional or named holes at <paramref name="level"/>.</summary>
    void Write(KryptonLogLevel level, string template, params object?[] args);

    /// <summary>Writes a message template with an exception at <paramref name="level"/>.</summary>
    void Write(KryptonLogLevel level, Exception? exception, string template, params object?[] args);

    /// <summary>Writes a <see cref="KryptonLogLevel.Trace"/> message.</summary>
    void Trace(string message);

    /// <summary>Writes a <see cref="KryptonLogLevel.Trace"/> template.</summary>
    void Trace(string template, params object?[] args);

    /// <summary>Writes a <see cref="KryptonLogLevel.Debug"/> message.</summary>
    void Debug(string message);

    /// <summary>Writes a <see cref="KryptonLogLevel.Debug"/> template.</summary>
    void Debug(string template, params object?[] args);

    /// <summary>Writes a <see cref="KryptonLogLevel.Information"/> message.</summary>
    void Information(string message);

    /// <summary>Writes a <see cref="KryptonLogLevel.Information"/> template.</summary>
    void Information(string template, params object?[] args);

    /// <summary>Writes a <see cref="KryptonLogLevel.Warning"/> message.</summary>
    void Warning(string message);

    /// <summary>Writes a <see cref="KryptonLogLevel.Warning"/> template.</summary>
    void Warning(string template, params object?[] args);

    /// <summary>Writes a <see cref="KryptonLogLevel.Error"/> message.</summary>
    void Error(string message);

    /// <summary>Writes a <see cref="KryptonLogLevel.Error"/> template.</summary>
    void Error(string template, params object?[] args);

    /// <summary>Writes a <see cref="KryptonLogLevel.Error"/> message with an exception.</summary>
    void Error(Exception? exception, string message);

    /// <summary>Writes a <see cref="KryptonLogLevel.Error"/> template with an exception.</summary>
    void Error(Exception? exception, string template, params object?[] args);

    /// <summary>Writes a <see cref="KryptonLogLevel.Fatal"/> message.</summary>
    void Fatal(string message);

    /// <summary>Writes a <see cref="KryptonLogLevel.Fatal"/> template.</summary>
    void Fatal(string template, params object?[] args);

    /// <summary>Writes a <see cref="KryptonLogLevel.Fatal"/> message with an exception.</summary>
    void Fatal(Exception? exception, string message);

    /// <summary>Writes a <see cref="KryptonLogLevel.Fatal"/> template with an exception.</summary>
    void Fatal(Exception? exception, string template, params object?[] args);
}
