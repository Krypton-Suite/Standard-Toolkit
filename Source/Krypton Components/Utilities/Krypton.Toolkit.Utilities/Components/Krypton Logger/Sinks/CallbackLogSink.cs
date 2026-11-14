#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal sealed class CallbackLogSink : IKryptonLogSink
{
    private readonly Action<KryptonLogEvent> _callback;

    public CallbackLogSink(Action<KryptonLogEvent> callback) =>
        _callback = callback ?? throw new ArgumentNullException(nameof(callback));

    public bool IsEnabled(KryptonLogLevel level) => true;

    public void Emit(KryptonLogEvent logEvent) => _callback(logEvent);

    public void Dispose()
    {
    }
}
