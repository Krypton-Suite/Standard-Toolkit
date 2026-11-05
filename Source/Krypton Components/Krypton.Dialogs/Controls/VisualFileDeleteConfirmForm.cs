#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Dialogs;

/// <summary>
/// Asks whether to move the listed items to the Recycle Bin.
/// </summary>
[ToolboxItem(false)]
public static class KryptonDeleteConfirmationDialog
{
    /// <summary>
    /// Shows the Recycle Bin confirmation.
    /// </summary>
    /// <param name="owner">Owner window, or null.</param>
    /// <param name="paths">Items that would be recycled.</param>
    /// <param name="strings">Caption and button text. Null uses the factory strings.</param>
    /// <param name="showBanner">True to show the top icon banner; false puts the message in the body.</param>
    /// <returns><see cref="DialogResult.Yes"/> when the user confirms.</returns>
    public static DialogResult Show(IWin32Window? owner, IReadOnlyList<string> paths, KryptonFileOperationStrings? strings, bool showBanner = true)
    {
        return VisualFileDeleteConfirmForm.Show(owner, paths, strings, permanent: false, showBanner);
    }
}

/// <summary>
/// Asks whether to permanently delete the listed items.
/// </summary>
[ToolboxItem(false)]
public static class KryptonPermanentDeleteDialog
{
    /// <summary>
    /// Shows the permanent-delete confirmation.
    /// </summary>
    /// <param name="owner">Owner window, or null.</param>
    /// <param name="paths">Items that would be deleted.</param>
    /// <param name="strings">Caption and button text. Null uses the factory strings.</param>
    /// <param name="showBanner">True to show the top icon banner; false puts the message in the body.</param>
    /// <returns><see cref="DialogResult.Yes"/> when the user confirms.</returns>
    public static DialogResult Show(IWin32Window? owner, IReadOnlyList<string> paths, KryptonFileOperationStrings? strings, bool showBanner = true)
    {
        return VisualFileDeleteConfirmForm.Show(owner, paths, strings, permanent: true, showBanner);
    }
}

/// <summary>
/// Shared confirmation window for recycle and permanent delete.
/// </summary>
internal sealed partial class VisualFileDeleteConfirmForm : KryptonForm
{
    /// <summary>Shows the confirmation and returns the dialog result.</summary>
    internal static DialogResult Show(IWin32Window? owner, IReadOnlyList<string> paths, KryptonFileOperationStrings? strings, bool permanent, bool showBanner)
    {
        KryptonFileOperationStrings text = strings ?? new KryptonFileOperationStrings();
        using (var form = new VisualFileDeleteConfirmForm(text, paths, permanent, showBanner))
        {
            form.StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;
            return form.ShowDialog(owner);
        }
    }

    private VisualFileDeleteConfirmForm(KryptonFileOperationStrings strings, IReadOnlyList<string> paths, bool permanent, bool showBanner)
    {
        InitializeComponent();
        int count = paths == null ? 0 : paths.Count;
        string message;
        if (permanent)
        {
            Text = strings.PermanentTitle;
            _picBanner.Image = SystemIcons.Warning.ToBitmap();
            message = count == 1
                ? strings.PermanentOne
                : string.Format(CultureInfo.CurrentCulture, strings.PermanentMany, count);
        }
        else
        {
            Text = strings.DeleteTitle;
            _picBanner.Image = SystemIcons.Question.ToBitmap();
            message = count == 1
                ? strings.DeleteOne
                : string.Format(CultureInfo.CurrentCulture, strings.DeleteMany, count);
        }

        _lblBanner.Text = message;
        _lblMessage.Text = message;
        _btnYes.Text = strings.Yes;
        _btnNo.Text = strings.No;
        _txtPaths.Text = paths == null ? string.Empty : string.Join(Environment.NewLine, paths);
        ApplyBanner(showBanner);
    }

    private void ApplyBanner(bool showBanner)
    {
        _pnlBanner.Visible = showBanner;
        _lblMessage.Visible = !showBanner;
        if (!showBanner)
        {
            _txtPaths.Location = new Point(_txtPaths.Left, 68);
            _txtPaths.Size = new Size(_txtPaths.Width, ClientSize.Height - _pnlButtons.Height - 80);
            ClientSize = new Size(ClientSize.Width, 260);
        }
    }
}
