#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides access to the toolkit diagnostic logger.
/// </summary>
public static class KryptonLogger
{
    #region Static Fields
    private static readonly object LoggerSync = new();
    private static IKryptonLogger? _customLogger;
    #endregion

    #region Public
    /// <summary>
    /// Gets the active logger instance.
    /// </summary>
    public static IKryptonLogger Current
    {
        get
        {
            // Snapshot without locking; SetLogger may replace _customLogger on another thread.
            var custom = _customLogger;
            return custom ?? DefaultKryptonLogger.Instance;
        }
    }

    /// <summary>
    /// Replaces the default logger. Pass null to restore the built-in default.
    /// </summary>
    /// <param name="logger">The custom logger implementation.</param>
    public static void SetLogger(IKryptonLogger? logger)
    {
        lock (LoggerSync)
        {
            _customLogger = logger;
        }
    }

    /// <summary>
    /// Writes a diagnostic message through the active logger.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public static void Write(string message) => Current.Write(message);
    #endregion
}

/// <summary>
/// Built-in logger that writes to <see cref="Debug"/> and optionally to a UAC-safe log file.
/// </summary>
internal sealed class DefaultKryptonLogger : IKryptonLogger
{
    #region Static Fields
    internal static readonly DefaultKryptonLogger Instance = new();

    private static readonly object FileSync = new();
    #endregion

    #region Instance Fields
    private readonly StreamWriter? _fileWriter;
    private readonly bool _fileLoggingEnabled;
    #endregion

    #region Identity
    private DefaultKryptonLogger()
    {
        var filePath = ResolveLogFilePath();
        if (string.IsNullOrWhiteSpace(filePath))
        {
            _fileLoggingEnabled = false;
            return;
        }

        try
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            _fileWriter = new StreamWriter(filePath, append: true) { AutoFlush = true };
            _fileLoggingEnabled = true;
        }
        catch
        {
            _fileLoggingEnabled = false;
        }
    }
    #endregion

    #region IKryptonLogger
    /// <inheritdoc />
    public void Write(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        Debug.WriteLine(message);

        if (!_fileLoggingEnabled)
        {
            return;
        }

        try
        {
            var line = $"[{DateTime.Now:HH:mm:ss.fff}] {message}";
            lock (FileSync)
            {
                _fileWriter!.WriteLine(line);
            }
        }
        catch
        {
        }
    }
    #endregion

    #region Implementation
    // Env-var precedence: KRYPTON_LOG_PATH (always) -> KRYPTON_LOG gate -> KRYPTON_LOG_WM -> %LOCALAPPDATA% default.
    // KRYPTON_LOG is checked before WM/default so the common "logging off" path avoids those lookups at type init.
    private static string? ResolveLogFilePath()
    {
        var explicitPath = Environment.GetEnvironmentVariable("KRYPTON_LOG_PATH");
        if (!string.IsNullOrWhiteSpace(explicitPath))
        {
            return explicitPath;
        }

        if (!IsFileLoggingEnabled())
        {
            return null;
        }

        // Legacy theme-swap WM tracing path; only consulted when KRYPTON_LOG is enabled.
        explicitPath = Environment.GetEnvironmentVariable("KRYPTON_LOG_WM");
        if (!string.IsNullOrWhiteSpace(explicitPath))
        {
            return explicitPath;
        }

        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(localAppData, "Krypton-Suite", "Toolkit", "Krypton.log");
    }

    private static bool IsFileLoggingEnabled()
    {
        var value = Environment.GetEnvironmentVariable("KRYPTON_LOG");
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return value.Equals("1", StringComparison.OrdinalIgnoreCase)
            || value.Equals("true", StringComparison.OrdinalIgnoreCase)
            || value.Equals("yes", StringComparison.OrdinalIgnoreCase)
            || value.Equals("on", StringComparison.OrdinalIgnoreCase);
    }
    #endregion
}
