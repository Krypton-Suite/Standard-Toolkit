#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal sealed class TraceLogSink : IKryptonLogSink
{
    private readonly KryptonLogLayout _layout;

    public TraceLogSink(KryptonLogLayout? layout) => _layout = layout ?? KryptonLogLayout.Default;

    public bool IsEnabled(KryptonLogLevel level) => true;

    public void Emit(KryptonLogEvent logEvent)
    {
        var text = _layout.Render(logEvent);
        if (text.EndsWith(Environment.NewLine, StringComparison.Ordinal))
        {
            text = text.Substring(0, text.Length - Environment.NewLine.Length);
        }

        Trace.WriteLine(text);
    }

    public void Dispose()
    {
    }
}
