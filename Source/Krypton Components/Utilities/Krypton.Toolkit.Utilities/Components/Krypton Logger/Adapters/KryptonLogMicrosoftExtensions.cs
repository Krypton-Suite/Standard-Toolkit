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
/// Delegate bridges for Microsoft.Extensions.Logging without referencing that package.
/// </summary>
public static class KryptonLogMicrosoftExtensions
{
    /// <summary>
    /// Creates a sink that forwards each event to <paramref name="write"/>. Typical usage:
    /// <c>write: (level, message, ex) => melLogger.Log(Map(level), ex, message)</c>.
    /// </summary>
    /// <param name="write">Receives level, rendered message, and exception. Cannot be null.</param>
    /// <param name="isEnabled">Optional filter. When null, all levels are forwarded.</param>
    public static IKryptonLogSink CreateSink(
        Action<KryptonLogLevel, string, Exception?> write,
        Func<KryptonLogLevel, bool>? isEnabled = null)
    {
        ThrowHelper.ThrowIfNull(write);
        return new DelegateLogSink(write, isEnabled);
    }

    /// <summary>
    /// Creates an <see cref="IKryptonLogger"/> that forwards <see cref="IKryptonLogger.Write"/> to
    /// <paramref name="write"/>. Typical usage: <c>write: message => melLogger.LogInformation(message)</c>.
    /// </summary>
    /// <param name="write">Receives the diagnostic message. Cannot be null.</param>
    public static IKryptonLogger CreateLogger(Action<string> write)
    {
        ThrowHelper.ThrowIfNull(write);
        return new DelegateKryptonLogger(write);
    }

    private sealed class DelegateLogSink : IKryptonLogSink
    {
        private readonly Action<KryptonLogLevel, string, Exception?> _write;
        private readonly Func<KryptonLogLevel, bool>? _isEnabled;

        public DelegateLogSink(Action<KryptonLogLevel, string, Exception?> write, Func<KryptonLogLevel, bool>? isEnabled)
        {
            _write = write;
            _isEnabled = isEnabled;
        }

        public bool IsEnabled(KryptonLogLevel level) => _isEnabled?.Invoke(level) ?? true;

        public void Emit(KryptonLogEvent logEvent) =>
            _write(logEvent.Level, KryptonLogProtect.Protect(logEvent.Message), logEvent.Exception);

        public void Dispose()
        {
        }
    }

    private sealed class DelegateKryptonLogger : IKryptonLogger
    {
        private readonly Action<string> _write;

        public DelegateKryptonLogger(Action<string> write) => _write = write;

        public void Write(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                _write(message);
            }
        }
    }
}
