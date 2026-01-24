#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Event data for taskbar thumbnail toolbar button clicks.
/// </summary>
public class ThumbnailButtonClickEventArgs : EventArgs
{
    #region Identity

    /// <summary>
    /// Initialize a new instance of the ThumbnailButtonClickEventArgs class.
    /// </summary>
    /// <param name="buttonId">The application-defined ID of the button that was clicked.</param>
    [CLSCompliant(false)]
    public ThumbnailButtonClickEventArgs(uint buttonId) => ButtonId = buttonId;

    #endregion

    #region ButtonId

    /// <summary>
    /// Gets the ID of the thumbnail toolbar button that was clicked.
    /// </summary>
    [CLSCompliant(false)]
    public uint ButtonId { get; }

    #endregion
}
