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
/// Provides data for the <see cref="KryptonComboBoxUserControl.ValueCommitted"/> event and the
/// <see cref="IKryptonDropDownUserControl.CommitValue"/> event raised by the drop-down content.
/// </summary>
public class KryptonDropDownCommitEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonDropDownCommitEventArgs"/> class.
    /// </summary>
    /// <param name="value">The picked value (any object). Pass <see langword="null"/> to clear the host's value.</param>
    /// <param name="displayText">The text to write into the host's editor. Pass <see langword="null"/> to leave the editor unchanged.</param>
    public KryptonDropDownCommitEventArgs(object? value, string? displayText)
    {
        Value = value;
        DisplayText = displayText;
    }

    /// <summary>
    /// Gets the picked value. May be <see langword="null"/>.
    /// </summary>
    public object? Value { get; }

    /// <summary>
    /// Gets the display text to push into the host's editor. May be <see langword="null"/>.
    /// </summary>
    public string? DisplayText { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the drop-down should remain open after the value
    /// is committed. Default is <see langword="false"/> &#8211; the popup closes immediately.
    /// </summary>
    public bool KeepOpen { get; set; }
}
