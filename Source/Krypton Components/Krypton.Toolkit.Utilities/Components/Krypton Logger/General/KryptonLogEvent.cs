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
/// An immutable log event emitted by <see cref="KryptonLog"/> after filtering and rendering.
/// </summary>
public sealed class KryptonLogEvent
{
    private static readonly KryptonLogProperty[] EmptyProperties = Array.Empty<KryptonLogProperty>();

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonLogEvent"/> class.
    /// </summary>
    public KryptonLogEvent(
        DateTime timestamp,
        KryptonLogLevel level,
        string category,
        string message,
        string? messageTemplate,
        IReadOnlyList<KryptonLogProperty>? properties,
        Exception? exception,
        int threadId,
        string? machineName)
    {
        Timestamp = timestamp;
        Level = level;
        Category = category ?? string.Empty;
        Message = message ?? string.Empty;
        MessageTemplate = messageTemplate;
        Properties = properties ?? EmptyProperties;
        Exception = exception;
        ThreadId = threadId;
        MachineName = machineName;
    }

    /// <summary>Gets the local timestamp when the event was created.</summary>
    public DateTime Timestamp { get; }

    /// <summary>Gets the severity.</summary>
    public KryptonLogLevel Level { get; }

    /// <summary>Gets the logger category (source context).</summary>
    public string Category { get; }

    /// <summary>Gets the rendered message.</summary>
    public string Message { get; }

    /// <summary>Gets the original message template when structured arguments were supplied.</summary>
    public string? MessageTemplate { get; }

    /// <summary>Gets captured template properties.</summary>
    public IReadOnlyList<KryptonLogProperty> Properties { get; }

    /// <summary>Gets the associated exception, if any.</summary>
    public Exception? Exception { get; }

    /// <summary>Gets the managed thread id of the caller, or 0 when thread enrichment is off.</summary>
    public int ThreadId { get; }

    /// <summary>Gets the machine name when machine enrichment is on; otherwise null.</summary>
    public string? MachineName { get; }
}
