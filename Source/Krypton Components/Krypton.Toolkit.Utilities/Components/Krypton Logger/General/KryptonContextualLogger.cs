#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal sealed class KryptonContextualLogger : IKryptonContextualLogger
{
    internal static readonly KryptonContextualLogger Disabled = new(string.Empty);

    private readonly string _category;

    public KryptonContextualLogger(string category) =>
        _category = category ?? string.Empty;

    public string Category => _category;

    public bool IsEnabled(KryptonLogLevel level)
    {
        var pipeline = KryptonLog.CurrentPipeline;
        return pipeline != null && pipeline.IsEnabled(_category, level);
    }

    public void Write(string message) =>
        Write(KryptonLogLevel.Information, null, message);

    public void Write(KryptonLogLevel level, string message) =>
        Write(level, null, message);

    public void Write(KryptonLogLevel level, Exception? exception, string message)
    {
        var pipeline = KryptonLog.CurrentPipeline;
        pipeline?.Write(_category, level, exception, message);
    }

    public void Write(KryptonLogLevel level, string template, params object?[] args) =>
        Write(level, null, template, args);

    public void Write(KryptonLogLevel level, Exception? exception, string template, params object?[] args)
    {
        var pipeline = KryptonLog.CurrentPipeline;
        pipeline?.Write(_category, level, exception, template, args);
    }

    public void Trace(string message) => Write(KryptonLogLevel.Trace, message);

    public void Trace(string template, params object?[] args) => Write(KryptonLogLevel.Trace, template, args);

    public void Debug(string message) => Write(KryptonLogLevel.Debug, message);

    public void Debug(string template, params object?[] args) => Write(KryptonLogLevel.Debug, template, args);

    public void Information(string message) => Write(KryptonLogLevel.Information, message);

    public void Information(string template, params object?[] args) => Write(KryptonLogLevel.Information, template, args);

    public void Warning(string message) => Write(KryptonLogLevel.Warning, message);

    public void Warning(string template, params object?[] args) => Write(KryptonLogLevel.Warning, template, args);

    public void Error(string message) => Write(KryptonLogLevel.Error, message);

    public void Error(string template, params object?[] args) => Write(KryptonLogLevel.Error, template, args);

    public void Error(Exception? exception, string message) => Write(KryptonLogLevel.Error, exception, message);

    public void Error(Exception? exception, string template, params object?[] args) =>
        Write(KryptonLogLevel.Error, exception, template, args);

    public void Fatal(string message) => Write(KryptonLogLevel.Fatal, message);

    public void Fatal(string template, params object?[] args) => Write(KryptonLogLevel.Fatal, template, args);

    public void Fatal(Exception? exception, string message) => Write(KryptonLogLevel.Fatal, exception, message);

    public void Fatal(Exception? exception, string template, params object?[] args) =>
        Write(KryptonLogLevel.Fatal, exception, template, args);
}
