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
/// Optional contract a drop-down content control can implement to participate in the host's
/// "filter-as-you-type" auto-complete pipeline.
/// </summary>
/// <remarks>
/// When <see cref="KryptonComboBoxUserControl.AutoOpenOnType"/> is <see langword="true"/> and the
/// user types in the editor, the host will automatically open the drop-down (without stealing
/// focus from the editor) and forward the current text to <see cref="ApplyFilter"/>. While the
/// popup is open, Up/Down arrow keys are forwarded to <see cref="NavigateSelection"/> and
/// Enter is forwarded to <see cref="CommitSelection"/>, mimicking the auto-complete UX of a
/// native ComboBox.
/// </remarks>
public interface IKryptonDropDownFilterable
{
    /// <summary>
    /// Update the drop-down content based on the current editor text.
    /// </summary>
    /// <param name="text">The current text in the editor.</param>
    /// <returns>
    /// <see langword="true"/> when at least one item matches the filter, <see langword="false"/>
    /// when no items match. The host may close the popup when this returns <see langword="false"/>.
    /// </returns>
    bool ApplyFilter(string text);

    /// <summary>
    /// Move the selection within the drop-down content.
    /// </summary>
    /// <param name="direction">+1 for "next" (Down arrow), -1 for "previous" (Up arrow).</param>
    void NavigateSelection(int direction);

    /// <summary>
    /// Commit the currently selected item in the drop-down. The implementation is expected to
    /// raise <see cref="IKryptonDropDownUserControl.CommitValue"/> when there is a valid
    /// selection to commit.
    /// </summary>
    /// <returns><see langword="true"/> if a value was committed; otherwise <see langword="false"/>.</returns>
    bool CommitSelection();
}
