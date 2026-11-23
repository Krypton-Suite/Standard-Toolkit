#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal sealed class NullLogSink : IKryptonLogSink
{
    internal static readonly NullLogSink Instance = new();

    private NullLogSink()
    {
    }

    public bool IsEnabled(KryptonLogLevel level) => false;

    public void Emit(KryptonLogEvent logEvent)
    {
    }

    public void Dispose()
    {
    }
}
