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
/// Fluent configuration for <see cref="KryptonLog.Configure(Action{KryptonLogConfiguration})"/>.
/// </summary>
public sealed class KryptonLogConfiguration
{
    internal KryptonLogLevel MinimumLevelValue { get; private set; } = KryptonLogLevel.Information;
    internal List<(string Prefix, KryptonLogLevel Level)> OverrideList { get; } = new();
    internal List<IKryptonLogSink> SyncSinks { get; } = new();
    internal List<IKryptonLogSink> AsyncSinks { get; } = new();
    internal MemoryLogSink? Memory { get; set; }
    internal FileLogSink? File { get; set; }
    internal bool EnrichThreadId { get; set; }
    internal bool EnrichMachineName { get; set; }
    internal bool UseAsync { get; private set; } = true;
    internal int AsyncQueueCapacity { get; private set; } = 4096;
    internal KryptonLogLayout Layout { get; private set; } = KryptonLogLayout.Default;

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonLogConfiguration"/> class.
    /// </summary>
    public KryptonLogConfiguration()
    {
        WriteTo = new KryptonLogWriteToConfiguration(this);
        Enrich = new KryptonLogEnrichConfiguration(this);
    }

    /// <summary>Gets the sink registration API.</summary>
    public KryptonLogWriteToConfiguration WriteTo { get; }

    /// <summary>Gets the enrichment API.</summary>
    public KryptonLogEnrichConfiguration Enrich { get; }

    /// <summary>Sets the global minimum level. Defaults to <see cref="KryptonLogLevel.Information"/>.</summary>
    public KryptonLogConfiguration MinimumLevel(KryptonLogLevel level)
    {
        MinimumLevelValue = level;
        return this;
    }

    /// <summary>
    /// Sets a minimum level for categories whose name starts with <paramref name="categoryPrefix"/>.
    /// Longest matching prefix wins.
    /// </summary>
    public KryptonLogConfiguration Override(string categoryPrefix, KryptonLogLevel level)
    {
        OverrideList.Add((categoryPrefix ?? string.Empty, level));
        return this;
    }

    /// <summary>Sets the layout used by Debug, Trace, File, and EventLog sinks.</summary>
    public KryptonLogConfiguration UseLayout(string pattern)
    {
        Layout = new KryptonLogLayout(pattern);
        return this;
    }

    /// <summary>
    /// Wraps file and Event Log sinks in a bounded background queue. This is the default.
    /// </summary>
    /// <param name="queueCapacity">Maximum queued events. Values below 32 are raised to 32.</param>
    public KryptonLogConfiguration Async(int queueCapacity = 4096)
    {
        UseAsync = true;
        AsyncQueueCapacity = queueCapacity;
        return this;
    }

    /// <summary>Writes file and Event Log sinks on the calling thread.</summary>
    public KryptonLogConfiguration Sync()
    {
        UseAsync = false;
        return this;
    }

    internal KryptonLogPipeline Build()
    {
        var sinks = new List<IKryptonLogSink>();
        sinks.AddRange(SyncSinks);
        if (AsyncSinks.Count > 0)
        {
            IKryptonLogSink asyncRoot = AsyncSinks.Count == 1 ? AsyncSinks[0] : new CompositeLogSink(AsyncSinks);
            if (UseAsync)
            {
                asyncRoot = new AsyncLogSink(asyncRoot, AsyncQueueCapacity);
            }

            sinks.Add(asyncRoot);
        }

        IKryptonLogSink root = sinks.Count == 0
            ? NullLogSink.Instance
            : sinks.Count == 1
                ? sinks[0]
                : new CompositeLogSink(sinks);

        var filter = new KryptonLogFilter(MinimumLevelValue, OverrideList);
        return new KryptonLogPipeline(filter, root, EnrichThreadId, EnrichMachineName, Memory, File);
    }
}

/// <summary>
/// Registers sinks on a <see cref="KryptonLogConfiguration"/>.
/// </summary>
public sealed class KryptonLogWriteToConfiguration
{
    private readonly KryptonLogConfiguration _owner;

    internal KryptonLogWriteToConfiguration(KryptonLogConfiguration owner) => _owner = owner;

    /// <summary>Writes rendered lines to <see cref="Debug"/>.</summary>
    public KryptonLogConfiguration Debug()
    {
        _owner.SyncSinks.Add(new DebugLogSink(_owner.Layout));
        return _owner;
    }

    /// <summary>Writes rendered lines to <see cref="Trace"/>.</summary>
    public KryptonLogConfiguration Trace()
    {
        _owner.SyncSinks.Add(new TraceLogSink(_owner.Layout));
        return _owner;
    }

    /// <summary>
    /// Writes to a rolling file. When <paramref name="path"/> is null, uses
    /// <c>%LOCALAPPDATA%\Krypton-Suite\Toolkit\Krypton.log</c>.
    /// </summary>
    public KryptonLogConfiguration File(string? path = null, long rollOnSizeBytes = 5_000_000, int retainedFileCount = 7, bool rollOnDate = true)
    {
        var sink = new FileLogSink(path ?? KryptonLogPaths.DefaultFilePath, rollOnSizeBytes, retainedFileCount, rollOnDate, _owner.Layout);
        _owner.File = sink;
        _owner.AsyncSinks.Add(sink);
        return _owner;
    }

    /// <summary>Keeps a ring buffer of recent events for the viewer and bug-report excerpts.</summary>
    public KryptonLogConfiguration Memory(int capacity = 2000)
    {
        var sink = new MemoryLogSink(capacity);
        _owner.Memory = sink;
        _owner.SyncSinks.Add(sink);
        return _owner;
    }

    /// <summary>Invokes <paramref name="callback"/> on the calling thread for each event.</summary>
    public KryptonLogConfiguration Callback(Action<KryptonLogEvent> callback)
    {
        _owner.SyncSinks.Add(new CallbackLogSink(callback));
        return _owner;
    }

    /// <summary>
    /// Writes to the Windows Event Log. Source creation requires elevation; failures are ignored.
    /// </summary>
    public KryptonLogConfiguration EventLog(string source = "Krypton", string logName = "Application")
    {
        _owner.AsyncSinks.Add(new EventLogSink(source, logName, _owner.Layout));
        return _owner;
    }

    /// <summary>Adds a custom sink. The sink is written on the calling thread.</summary>
    public KryptonLogConfiguration Sink(IKryptonLogSink sink)
    {
        ThrowHelper.ThrowIfNull(sink);
        _owner.SyncSinks.Add(sink);
        return _owner;
    }
}

/// <summary>
/// Adds optional properties to every <see cref="KryptonLogEvent"/>.
/// </summary>
public sealed class KryptonLogEnrichConfiguration
{
    private readonly KryptonLogConfiguration _owner;

    internal KryptonLogEnrichConfiguration(KryptonLogConfiguration owner) => _owner = owner;

    /// <summary>Records <see cref="Thread.ManagedThreadId"/> on each event.</summary>
    public KryptonLogConfiguration WithThreadId()
    {
        _owner.EnrichThreadId = true;
        return _owner;
    }

    /// <summary>Records <see cref="Environment.MachineName"/> on each event.</summary>
    public KryptonLogConfiguration WithMachineName()
    {
        _owner.EnrichMachineName = true;
        return _owner;
    }
}
