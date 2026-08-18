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
/// Active pipeline installed by <see cref="KryptonLog"/>.
/// </summary>
internal sealed class KryptonLogPipeline : IDisposable
{
    private static readonly object?[] EmptyArgs = Array.Empty<object?>();

    private readonly KryptonLogFilter _filter;
    private readonly IKryptonLogSink _sink;
    private readonly bool _enrichThreadId;
    private readonly bool _enrichMachineName;
    private readonly string? _machineName;

    public KryptonLogPipeline(
        KryptonLogFilter filter,
        IKryptonLogSink sink,
        bool enrichThreadId,
        bool enrichMachineName,
        MemoryLogSink? memory,
        FileLogSink? file)
    {
        _filter = filter;
        _sink = sink;
        _enrichThreadId = enrichThreadId;
        _enrichMachineName = enrichMachineName;
        _machineName = enrichMachineName ? Environment.MachineName : null;
        Memory = memory;
        FilePath = file?.Path;
    }

    public MemoryLogSink? Memory { get; }

    public string? FilePath { get; }

    public bool IsEnabled(string category, KryptonLogLevel level) =>
        _filter.IsEnabled(category, level);

    public void Write(string category, KryptonLogLevel level, Exception? exception, string? message)
    {
        if (!_filter.IsEnabled(category, level))
        {
            return;
        }

        Emit(category, level, exception, message ?? string.Empty, null, Array.Empty<KryptonLogProperty>());
    }

    public void Write(string category, KryptonLogLevel level, Exception? exception, string? template, object?[]? args)
    {
        if (!_filter.IsEnabled(category, level))
        {
            return;
        }

        var rendered = KryptonLogMessageTemplate.Render(template, args ?? EmptyArgs, out var properties);
        Emit(category, level, exception, rendered, template, properties);
    }

    public void Dispose() => _sink.Dispose();

    private void Emit(
        string category,
        KryptonLogLevel level,
        Exception? exception,
        string message,
        string? template,
        IReadOnlyList<KryptonLogProperty> properties)
    {
        var logEvent = new KryptonLogEvent(
            DateTime.Now,
            level,
            category ?? string.Empty,
            message,
            template,
            properties,
            exception,
            _enrichThreadId ? Thread.CurrentThread.ManagedThreadId : 0,
            _machineName);

        _sink.Emit(logEvent);
    }
}
