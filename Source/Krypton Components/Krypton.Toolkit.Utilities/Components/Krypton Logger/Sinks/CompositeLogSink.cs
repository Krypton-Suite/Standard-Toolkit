#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal sealed class CompositeLogSink : IKryptonLogSink
{
    private readonly IKryptonLogSink[] _sinks;

    public CompositeLogSink(IList<IKryptonLogSink> sinks) =>
        _sinks = sinks == null || sinks.Count == 0
            ? Array.Empty<IKryptonLogSink>()
            : sinks.ToArray();

    public bool IsEnabled(KryptonLogLevel level)
    {
        foreach (var sink in _sinks)
        {
            if (sink.IsEnabled(level))
            {
                return true;
            }
        }

        return false;
    }

    public void Emit(KryptonLogEvent logEvent)
    {
        foreach (var sink in _sinks)
        {
            if (sink.IsEnabled(logEvent.Level))
            {
                sink.Emit(logEvent);
            }
        }
    }

    public void Dispose()
    {
        foreach (var sink in _sinks)
        {
            sink.Dispose();
        }
    }
}
