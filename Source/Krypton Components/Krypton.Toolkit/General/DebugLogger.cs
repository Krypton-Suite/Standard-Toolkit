#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal static class DebugLogger
{
#if DEBUG
    private static readonly object Sync = new object();
    private static string? _logFilePath;

    private static string EnsurePath()
    {
        if (!string.IsNullOrWhiteSpace(_logFilePath))
        {
            return _logFilePath!;
        }

        var env = Environment.GetEnvironmentVariable("KRYPTON_LOG_WM");
        if (!string.IsNullOrWhiteSpace(env))
        {
            _logFilePath = env;
            return _logFilePath!;
        }

        var baseDir = AppContext.BaseDirectory;
        var logsDir = Path.Combine(baseDir, "logs");
        try
        {
            Directory.CreateDirectory(logsDir);
        }
        catch
        {
        }
        _logFilePath = Path.Combine(logsDir, "KryptonWM.log");
        return _logFilePath!;
    }

    public static void WriteLine(string message)
    {
        try
        {
            var path = EnsurePath();
            var line = $"[{DateTime.Now:HH:mm:ss.fff}] {message}";
            lock (Sync)
            {
                File.AppendAllText(path, line + Environment.NewLine);
            }
        }
        catch
        {
        }
    }
#else
    public static void WriteLine(string message)
    {
    }
#endif
}
