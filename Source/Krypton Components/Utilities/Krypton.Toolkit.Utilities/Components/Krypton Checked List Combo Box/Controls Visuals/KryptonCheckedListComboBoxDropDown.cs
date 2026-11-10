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
/// Drop-down <see cref="KryptonCheckedListBox"/> for <see cref="KryptonCheckedListComboBox"/>.
/// </summary>
/// <remarks>
/// Hosted directly as <see cref="KryptonComboBoxUserControl.DropContent"/> (not inside a plain
/// <see cref="UserControl"/>) so Krypton layout and painting run correctly inside the popup.
/// </remarks>
internal sealed class KryptonCheckedListComboBoxDropDown : KryptonCheckedListBox, IKryptonDropDownUserControl
{
    #region Instance Fields

    private readonly KryptonCheckedListComboBox _owner;

    #endregion

    #region Events

    public event EventHandler<KryptonDropDownCommitEventArgs>? CommitValue;
    public event EventHandler? RequestClose;

    #endregion

    #region Identity

    public KryptonCheckedListComboBoxDropDown(KryptonCheckedListComboBox owner)
    {
        _owner = owner;
        CheckOnClick = true;
        PaletteMode = owner.PaletteMode;
        ItemCheck += OnListItemCheck;
        KeyDown += OnListKeyDown;
    }

    #endregion

    #region Public

    /// <summary>
    /// Pushes the current checked selection to the host editor.
    /// </summary>
    /// <param name="keepDropDownOpen">When <see langword="true"/> the popup remains visible.</param>
    internal void PublishSelection(bool keepDropDownOpen)
    {
        CommitValue?.Invoke(this, new KryptonDropDownCommitEventArgs(
            _owner.BuildCheckedValue(),
            _owner.FormatCheckedItemsDisplay())
        {
            KeepOpen = keepDropDownOpen
        });
    }

    /// <summary>
    /// Ensures the inner list is sized and painted after the popup is shown.
    /// </summary>
    internal void EnsureDropDownLayout()
    {
        PaletteMode = _owner.PaletteMode;

        if (IsHandleCreated)
        {
            InvokeLayout();
        }
        else
        {
            ForceControlLayout();
        }

        PerformNeedPaint(true);
        ListBox.Invalidate();
    }

    #endregion

    #region IKryptonDropDownUserControl

    public Size GetPreferredDropSize(Size proposedSize) => proposedSize.IsEmpty ? Size.Empty : proposedSize;

    public void OnDropDownOpening(object owner) => EnsureDropDownLayout();

    public void OnDropDownOpened(object owner)
    {
        EnsureDropDownLayout();
        Focus();
    }

    public void OnDropDownClosing(object owner, ref bool cancel)
    {
    }

    public void OnDropDownClosed(object owner)
    {
    }

    #endregion

    #region Implementation

    private void OnListItemCheck(object? sender, ItemCheckEventArgs e) =>
        SchedulePublishAfterItemCheckApplied();

    private void SchedulePublishAfterItemCheckApplied()
    {
        void Publish() => PublishSelection(keepDropDownOpen: true);

        if (_owner.IsHandleCreated)
        {
            _owner.BeginInvoke(new Action(Publish));
            return;
        }

        if (IsHandleCreated)
        {
            BeginInvoke(new Action(Publish));
        }
    }

    private void OnListKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter && _owner.CloseDropDownOnEnter)
        {
            PublishSelection(keepDropDownOpen: false);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    #endregion
}
