#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal sealed class DebugLogSink : IKryptonLogSink
{
    private readonly KryptonLogLayout _layout;

    public DebugLogSink(KryptonLogLayout? layout) => _layout = layout ?? KryptonLogLayout.Default;

    public bool IsEnabled(KryptonLogLevel level) => true;

    public void Emit(KryptonLogEvent logEvent)
    {
        var rendered = _layout.Render(logEvent);
        if (rendered.EndsWith(Environment.NewLine, StringComparison.Ordinal))
        {
            rendered = rendered.Substring(0, rendered.Length - Environment.NewLine.Length);
        }

        Debug.WriteLine(KryptonLogProtect.Protect(rendered));
    }

    public void Dispose()
    {
    }
}
