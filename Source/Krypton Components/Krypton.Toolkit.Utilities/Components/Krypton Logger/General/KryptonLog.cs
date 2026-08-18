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
/// Process-wide native logging facade. Opt-in: until <see cref="Configure(Action{KryptonLogConfiguration})"/>
/// is called, <see cref="ForContext"/> returns a no-op logger and toolkit diagnostics keep using
/// <see cref="KryptonLogger"/>.
/// </summary>
public static class KryptonLog
{
    private static readonly object Sync = new();
    private static readonly ConcurrentDictionary<string, KryptonContextualLogger> Loggers = new(StringComparer.Ordinal);
    private static KryptonLogPipeline? _pipeline;
    private static KryptonLogIKryptonLoggerAdapter? _toolkitAdapter;

    /// <summary>Gets a value indicating whether a pipeline is installed.</summary>
    public static bool IsConfigured => Volatile.Read(ref _pipeline) != null;

    /// <summary>Gets the memory ring buffer when one was registered; otherwise null.</summary>
    public static MemoryLogSink? Memory => CurrentPipeline?.Memory;

    /// <summary>Gets the active rolling file path when a file sink was registered; otherwise null.</summary>
    public static string? ActiveFilePath => CurrentPipeline?.FilePath;

    /// <summary>Gets a logger for the default category.</summary>
    public static IKryptonContextualLogger Logger => ForContext("Default");

    internal static KryptonLogPipeline? CurrentPipeline => Volatile.Read(ref _pipeline);

    /// <summary>
    /// Returns a named logger. Category overrides in configuration use prefix matching against this name.
    /// </summary>
    /// <param name="category">Source context, for example <c>MyApp.Startup</c>.</param>
    public static IKryptonContextualLogger ForContext(string category)
    {
        category ??= string.Empty;
        if (CurrentPipeline == null)
        {
            return KryptonContextualLogger.Disabled;
        }

        return Loggers.GetOrAdd(category, static name => new KryptonContextualLogger(name));
    }

    /// <summary>
    /// Replaces the active pipeline. The previous pipeline is flushed and disposed.
    /// </summary>
    /// <param name="configure">Fluent configuration callback. Cannot be null.</param>
    public static void Configure(Action<KryptonLogConfiguration> configure)
    {
        ThrowHelper.ThrowIfNull(configure);
        var configuration = new KryptonLogConfiguration();
        configure(configuration);
        Configure(configuration);
    }

    /// <summary>
    /// Replaces the active pipeline from a pre-built configuration.
    /// </summary>
    /// <param name="configuration">The configuration. Cannot be null.</param>
    public static void Configure(KryptonLogConfiguration configuration)
    {
        ThrowHelper.ThrowIfNull(configuration);
        var pipeline = configuration.Build();
        lock (Sync)
        {
            var previous = _pipeline;
            _pipeline = pipeline;
            previous?.Dispose();
        }
    }

    /// <summary>
    /// Loads configuration from an XML file. See the developer guide for the schema.
    /// </summary>
    /// <param name="path">Path to the XML file. Cannot be null or empty.</param>
    public static void ConfigureFromXml(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(path));
        }

        using var stream = File.OpenRead(path);
        ConfigureFromXml(stream);
    }

    /// <summary>
    /// Loads configuration from an XML stream.
    /// </summary>
    /// <param name="stream">The XML stream. Cannot be null.</param>
    public static void ConfigureFromXml(Stream stream)
    {
        ThrowHelper.ThrowIfNull(stream);
        Configure(KryptonLogXmlConfiguration.Load(stream));
    }

    /// <summary>
    /// Writes the current configuration shape to XML. This saves the last-applied builder when
    /// <paramref name="configuration"/> is supplied; otherwise a minimal default document is written.
    /// </summary>
    public static void SaveToXml(string path, KryptonLogConfiguration configuration)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(path));
        }

        ThrowHelper.ThrowIfNull(configuration);
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using var stream = File.Create(path);
        KryptonLogXmlConfiguration.Save(stream, configuration);
    }

#if NET8_0_OR_GREATER
    /// <summary>
    /// Loads configuration from a JSON file. Available on .NET 8 and later.
    /// </summary>
    /// <param name="path">Path to the JSON file. Cannot be null or empty.</param>
    public static void ConfigureFromJson(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(path));
        }

        using var stream = File.OpenRead(path);
        ConfigureFromJson(stream);
    }

    /// <summary>
    /// Loads configuration from a JSON stream. Available on .NET 8 and later.
    /// </summary>
    /// <param name="stream">The JSON stream. Cannot be null.</param>
    public static void ConfigureFromJson(Stream stream)
    {
        ThrowHelper.ThrowIfNull(stream);
        Configure(KryptonLogJsonConfiguration.Load(stream));
    }
#endif

    /// <summary>
    /// Configures from environment variables. <c>KRYPTON_LOG_CONFIG</c> points at an XML (or JSON on
    /// .NET 8+) file. Otherwise, when <c>KRYPTON_LOG</c> is truthy, Debug plus a rolling file at
    /// <c>KRYPTON_LOG_PATH</c> (or <c>KRYPTON_LOG_WM</c>, or the LocalAppData default) is used.
    /// </summary>
    public static void ConfigureFromEnvironment()
    {
        var configPath = Environment.GetEnvironmentVariable("KRYPTON_LOG_CONFIG");
        if (!string.IsNullOrWhiteSpace(configPath) && File.Exists(configPath))
        {
#if NET8_0_OR_GREATER
            if (configPath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                ConfigureFromJson(configPath);
                return;
            }
#endif
            ConfigureFromXml(configPath);
            return;
        }

        if (!KryptonLogPaths.IsTruthy(Environment.GetEnvironmentVariable("KRYPTON_LOG")))
        {
            return;
        }

        var filePath = Environment.GetEnvironmentVariable("KRYPTON_LOG_PATH");
        if (string.IsNullOrWhiteSpace(filePath))
        {
            filePath = Environment.GetEnvironmentVariable("KRYPTON_LOG_WM");
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            filePath = KryptonLogPaths.DefaultFilePath;
        }

        Configure(cfg => cfg
            .MinimumLevel(KryptonLogLevel.Debug)
            .WriteTo.Debug()
            .WriteTo.File(filePath)
            .WriteTo.Memory());
    }

    /// <summary>
    /// Flushes and disposes the active pipeline. Safe to call when not configured.
    /// </summary>
    public static void CloseAndFlush()
    {
        lock (Sync)
        {
            var previous = _pipeline;
            _pipeline = null;
            previous?.Dispose();
        }
    }

    /// <summary>
    /// Installs this pipeline as <see cref="KryptonLogger.Current"/> so toolkit diagnostics
    /// (<see cref="CommonHelper.LogOutput"/>, theme-swap WM tracing) flow through Utilities sinks.
    /// Messages starting with <c>[WM] </c> use category <c>Krypton.Toolkit.WM</c> at Debug.
    /// </summary>
    public static void InstallAsToolkitLogger()
    {
        lock (Sync)
        {
            _toolkitAdapter ??= new KryptonLogIKryptonLoggerAdapter();
            KryptonLogger.SetLogger(_toolkitAdapter);
        }
    }

    /// <summary>
    /// Restores the built-in toolkit logger.
    /// </summary>
    public static void UninstallToolkitLogger()
    {
        lock (Sync)
        {
            if (_toolkitAdapter == null)
            {
                return;
            }

            KryptonLogger.SetLogger(null);
            _toolkitAdapter = null;
        }
    }

    /// <summary>
    /// Returns an <see cref="IKryptonLogger"/> that writes Information events to the default category.
    /// Use this for splash <c>Logger</c> when <see cref="KryptonSplashScreenManagerData.UseKryptonLog"/> is not set.
    /// </summary>
    public static IKryptonLogger AsKryptonLogger() => ForContext("Default");
}
