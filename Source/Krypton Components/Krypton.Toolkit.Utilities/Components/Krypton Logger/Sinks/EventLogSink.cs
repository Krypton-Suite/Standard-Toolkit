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
/// Writes to the Windows Event Log. Source creation requires elevation; failures are ignored.
/// </summary>
internal sealed class EventLogSink : IKryptonLogSink
{
    private readonly string _source;
    private readonly string _logName;
    private readonly KryptonLogLayout _layout;
    private readonly bool _available;

    public EventLogSink(string source, string logName, KryptonLogLayout? layout)
    {
        _source = string.IsNullOrWhiteSpace(source) ? "Krypton" : source;
        _logName = string.IsNullOrWhiteSpace(logName) ? "Application" : logName;
        _layout = layout ?? KryptonLogLayout.Default;
        _available = TryEnsureSource();
    }

    public bool IsEnabled(KryptonLogLevel level) => _available && level >= KryptonLogLevel.Information;

    public void Emit(KryptonLogEvent logEvent)
    {
        if (!_available)
        {
            return;
        }

        try
        {
            var type = logEvent.Level switch
            {
                KryptonLogLevel.Warning => EventLogEntryType.Warning,
                KryptonLogLevel.Error => EventLogEntryType.Error,
                KryptonLogLevel.Fatal => EventLogEntryType.Error,
                _ => EventLogEntryType.Information
            };

            EventLog.WriteEntry(_source, KryptonLogProtect.Protect(_layout.Render(logEvent)), type);
        }
        catch
        {
        }
    }

    public void Dispose()
    {
    }

    private bool TryEnsureSource()
    {
        try
        {
            if (!EventLog.SourceExists(_source))
            {
                EventLog.CreateEventSource(_source, _logName);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}
