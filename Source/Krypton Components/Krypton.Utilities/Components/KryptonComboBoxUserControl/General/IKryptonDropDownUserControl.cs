#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Optional contract a developer-supplied <see cref="UserControl"/> can implement so that the
/// hosting <see cref="KryptonComboBoxUserControl"/> can drive sizing, life-cycle events and value
/// commit/cancel signalling. Implementing the interface is not required &#8211; a control hosted by
/// <see cref="KryptonComboBoxUserControl"/> that does not implement it will simply be displayed at
/// the size requested via <c>DropDownWidth</c> / <c>DropDownHeight</c>.
/// </summary>
public interface IKryptonDropDownUserControl
{
    /// <summary>
    /// Gives the drop-down content a chance to declare its preferred initial size before the
    /// popup is shown. Return <see cref="Size.Empty"/> to fall back to the host's
    /// <c>DropDownWidth</c> / <c>DropDownHeight</c> values.
    /// </summary>
    /// <param name="proposedSize">Size proposed by the host (typically the host's preferred size).</param>
    /// <returns>The preferred size for the popup, or <see cref="Size.Empty"/> to use defaults.</returns>
    Size GetPreferredDropSize(Size proposedSize);

    /// <summary>
    /// Called immediately before the drop-down is shown. Allows the content to refresh data,
    /// preselect items, etc.
    /// </summary>
    /// <param name="owner">The hosting <see cref="KryptonComboBoxUserControl"/>.</param>
    void OnDropDownOpening(object owner);

    /// <summary>
    /// Called once the drop-down has been shown.
    /// </summary>
    /// <param name="owner">The hosting <see cref="KryptonComboBoxUserControl"/>.</param>
    void OnDropDownOpened(object owner);

    /// <summary>
    /// Called when the drop-down is about to close. Implementations can suppress closing by
    /// setting <paramref name="cancel"/> to <see langword="true"/>.
    /// </summary>
    /// <param name="owner">The hosting <see cref="KryptonComboBoxUserControl"/>.</param>
    /// <param name="cancel">Set to <see langword="true"/> to keep the drop-down open.</param>
    void OnDropDownClosing(object owner, ref bool cancel);

    /// <summary>
    /// Called once the drop-down has been hidden.
    /// </summary>
    /// <param name="owner">The hosting <see cref="KryptonComboBoxUserControl"/>.</param>
    void OnDropDownClosed(object owner);

    /// <summary>
    /// Raised by the drop-down content when the user picks a value. The host listens to this
    /// event to update its <c>Text</c> / <c>SelectedValue</c> and (optionally) close the popup.
    /// </summary>
    event EventHandler<KryptonDropDownCommitEventArgs> CommitValue;

    /// <summary>
    /// Raised by the drop-down content when it wants the popup to close without committing
    /// (for example when the user presses Escape inside the user control).
    /// </summary>
    event EventHandler RequestClose;
}
