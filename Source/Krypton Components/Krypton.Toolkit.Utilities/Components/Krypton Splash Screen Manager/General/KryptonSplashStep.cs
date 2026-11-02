#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// One startup step executed by <see cref="KryptonSplashScreenManager.Run(KryptonSplashScreenManagerData, IList{KryptonSplashStep})"/>.
/// </summary>
/// <remarks>
/// <see cref="Action"/> runs on the caller thread. The splash window stays on its own STA thread
/// so status and progress keep updating while the action performs blocking work.
/// </remarks>
public class KryptonSplashStep
{
    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonSplashStep"/> class.</summary>
    public KryptonSplashStep()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="KryptonSplashStep"/> class.</summary>
    /// <param name="status">Status text shown on the splash before <paramref name="action"/> runs.</param>
    /// <param name="action">Startup work to run on the caller thread. May be null.</param>
    public KryptonSplashStep(string? status, Action? action)
    {
        Status = status;
        Action = action;
    }

    #endregion

    #region Public

    /// <summary>Gets or sets the status text shown on the splash before this step runs.</summary>
    public string? Status { get; set; }

    /// <summary>Gets or sets the startup work to run on the caller thread.</summary>
    public Action? Action { get; set; }

    #endregion
}
