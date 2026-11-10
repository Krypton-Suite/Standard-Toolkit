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
/// Severity of a <see cref="KryptonLogEvent"/>. Higher values are more severe.
/// </summary>
public enum KryptonLogLevel
{
    /// <summary>Verbose diagnostic detail (disabled in typical production configuration).</summary>
    Trace = 0,

    /// <summary>Debug information useful while developing.</summary>
    Debug = 1,

    /// <summary>Normal operational messages.</summary>
    Information = 2,

    /// <summary>Unexpected but recoverable conditions.</summary>
    Warning = 3,

    /// <summary>Failures that affect a request or operation.</summary>
    Error = 4,

    /// <summary>Failures that may require process shutdown or immediate attention.</summary>
    Fatal = 5
}
