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
/// Maps toolkit <see cref="IKryptonLogger.Write"/> calls into <see cref="KryptonLog"/>.
/// Lines starting with <c>[WM] </c> use category <c>Krypton.Toolkit.WM</c> at Debug.
/// </summary>
internal sealed class KryptonLogIKryptonLoggerAdapter : IKryptonLogger
{
    private const string WmPrefix = "[WM] ";
    private const string ToolkitCategory = "Krypton.Toolkit";
    private const string WmCategory = "Krypton.Toolkit.WM";

    public void Write(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        if (message.StartsWith(WmPrefix, StringComparison.Ordinal))
        {
            KryptonLog.ForContext(WmCategory).Write(KryptonLogLevel.Debug, message);
            return;
        }

        KryptonLog.ForContext(ToolkitCategory).Write(KryptonLogLevel.Information, message);
    }
}
