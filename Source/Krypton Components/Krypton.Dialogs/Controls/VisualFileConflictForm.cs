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
/// Asks whether to replace, skip, or keep both when a destination name already exists.
/// </summary>
[ToolboxItem(false)]
public static class KryptonFileConflictDialog
{
    /// <summary>
    /// Shows the conflict dialog.
    /// </summary>
    /// <param name="owner">Owner window, or null.</param>
    /// <param name="sourcePath">Source that would be written.</param>
    /// <param name="destinationPath">Destination that already exists.</param>
    /// <param name="strings">Button and caption text. Null uses the factory strings.</param>
    /// <param name="applyToRemaining">True when the same choice should be reused for later collisions.</param>
    /// <param name="showBanner">True to show the top icon banner; false puts the intro in the body.</param>
    /// <returns>The user's choice. Closing the dialog returns <see cref="KryptonFileConflictChoice.Cancel"/>.</returns>
    public static KryptonFileConflictChoice Show(
        IWin32Window? owner,
        string sourcePath,
        string destinationPath,
        KryptonFileOperationStrings? strings,
        out bool applyToRemaining,
        bool showBanner = true)
    {
        KryptonFileOperationStrings text = strings ?? new KryptonFileOperationStrings();
        using (var form = new VisualFileConflictForm(text, sourcePath, destinationPath, showBanner))
        {
            form.StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;
            form.ShowDialog(owner);
            applyToRemaining = form.ApplyToRemaining;
            return form.Choice;
        }
    }
}

/// <summary>
/// Modal conflict prompt used by <see cref="KryptonFileConflictDialog"/>.
/// </summary>
internal sealed partial class VisualFileConflictForm : KryptonForm
{
    /// <summary>Creates the prompt.</summary>
    internal VisualFileConflictForm(KryptonFileOperationStrings strings, string sourcePath, string destinationPath, bool showBanner)
    {
        InitializeComponent();
        Text = strings.ConflictTitle;
        _picBanner.Image = SystemIcons.Warning.ToBitmap();
        _lblBanner.Text = strings.ConflictIntro;
        _lblIntro.Text = strings.ConflictIntro;
        _lblSource.Text = strings.FromLabel + " " + sourcePath;
        _lblDestination.Text = strings.ToLabel + " " + destinationPath;
        _chkApply.Text = strings.ApplyToRemaining;
        _btnReplace.Text = strings.Replace;
        _btnSkip.Text = strings.Skip;
        _btnKeepBoth.Text = strings.KeepBoth;
        _btnCancel.Text = strings.Cancel;
        ApplyBanner(showBanner);
        Choice = KryptonFileConflictChoice.Cancel;
        _btnReplace.Click += (sender, args) => Choose(KryptonFileConflictChoice.Replace);
        _btnSkip.Click += (sender, args) => Choose(KryptonFileConflictChoice.Skip);
        _btnKeepBoth.Click += (sender, args) => Choose(KryptonFileConflictChoice.KeepBoth);
        _btnCancel.Click += (sender, args) => Choose(KryptonFileConflictChoice.Cancel);
    }

    /// <summary>Choice selected by the user.</summary>
    internal KryptonFileConflictChoice Choice { get; private set; }

    /// <summary>True when the checkbox was checked.</summary>
    internal bool ApplyToRemaining => _chkApply.Checked;

    private void ApplyBanner(bool showBanner)
    {
        _pnlBanner.Visible = showBanner;
        _lblIntro.Visible = !showBanner;
        if (!showBanner)
        {
            _lblSource.Location = new Point(_lblSource.Left, 56);
            _lblDestination.Location = new Point(_lblDestination.Left, 96);
            _chkApply.Location = new Point(_chkApply.Left, 140);
            ClientSize = new Size(ClientSize.Width, 224);
        }
    }

    private void Choose(KryptonFileConflictChoice choice)
    {
        Choice = choice;
        DialogResult = choice == KryptonFileConflictChoice.Cancel ? DialogResult.Cancel : DialogResult.OK;
    }
}
