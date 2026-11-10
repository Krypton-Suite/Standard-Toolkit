#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Provides data for the <see cref="KryptonComboBoxUserControl.DropDownOpening"/> event,
/// allowing subscribers to suppress the drop-down before it appears.
/// </summary>
public class KryptonDropDownOpeningEventArgs : CancelEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonDropDownOpeningEventArgs"/> class.
    /// </summary>
    /// <param name="dropContent">The user control that is about to be shown.</param>
    public KryptonDropDownOpeningEventArgs(Control? dropContent)
    {
        DropContent = dropContent;
    }

    /// <summary>
    /// Gets the user control that is about to be shown. May be <see langword="null"/> when no
    /// drop-down content has been assigned.
    /// </summary>
    public Control? DropContent { get; }
}
