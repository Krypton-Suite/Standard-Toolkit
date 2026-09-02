#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Dialog that adds palette files to a <c>.kpal</c> pack and removes named themes.
/// </summary>
internal partial class VisualKryptonPalettePackEditorForm : KryptonForm
{
    internal VisualKryptonPalettePackEditorForm()
    {
        InitializeComponent();
        kbtnBrowse.Click += (_, _) => BrowsePack();
        kbtnSaveName.Click += (_, _) => SavePackName();
        kbtnAdd.Click += (_, _) => AddThemes();
        kbtnRemove.Click += (_, _) => RemoveSelectedTheme();
        kbtnClose.Click += (_, _) => Close();
        CancelButton = kbtnClose;
    }

    internal VisualKryptonPalettePackEditorForm(string? packPath)
        : this()
    {
        if (!string.IsNullOrWhiteSpace(packPath))
        {
            ktxtPackPath.Text = packPath;
            RefreshThemes();
        }
    }

    /// <summary>
    /// Pack path shown in the dialog after close (browse or add may change it).
    /// </summary>
    internal string PackPath => ktxtPackPath.Text.Trim();

    private void BrowsePack()
    {
        using var dialog = new OpenFileDialog
        {
            Title = @"Open or create palette pack",
            Filter = @"Binary palette files (*.kpal)|*.kpal|All files (*.*)|*.*",
            DefaultExt = KryptonPaletteFile.BinaryExtension,
            CheckFileExists = false,
            FileName = string.IsNullOrWhiteSpace(ktxtPackPath.Text)
                ? @"themes.kpal"
                : Path.GetFileName(ktxtPackPath.Text),
            InitialDirectory = InitialDirectory(ktxtPackPath.Text)
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        ktxtPackPath.Text = dialog.FileName;
        RefreshThemes();
    }

    private void SavePackName()
    {
        var path = RequirePackPath(createIfMissing: false);
        if (path == null)
        {
            return;
        }

        if (!File.Exists(path))
        {
            SetStatus(@"Add a theme before saving the pack name.");
            return;
        }

        try
        {
            KryptonPaletteFile.SetPackName(path, ktxtPackName.Text);
            SetStatus($@"Pack name saved as '{KryptonPaletteFile.GetPackName(path)}'.");
        }
        catch (Exception ex)
        {
            ShowError(ex, @"Save pack name");
        }
    }

    private void AddThemes()
    {
        var path = RequirePackPath(createIfMissing: true);
        if (path == null)
        {
            return;
        }

        using var dialog = new OpenFileDialog
        {
            Title = @"Add palette files to pack",
            Filter = KryptonPaletteFile.DialogFilter,
            DefaultExt = KryptonPaletteFile.Extension,
            Multiselect = true,
            CheckFileExists = true,
            InitialDirectory = InitialDirectory(path)
        };

        if (dialog.ShowDialog(this) != DialogResult.OK || dialog.FileNames.Length == 0)
        {
            return;
        }

        var added = 0;
        for (var i = 0; i < dialog.FileNames.Length; i++)
        {
            var source = dialog.FileNames[i];
            if (TryAddSource(path, source))
            {
                added++;
            }
        }

        RefreshThemes();
        PersistPackNameIfNeeded(path);
        SetStatus(added == 0
            ? @"No themes were added."
            : $@"Added {added} file(s) to the pack.");
    }

    private bool TryAddSource(string packPath, string sourcePath)
    {
        try
        {
            KryptonPaletteFile.AddToPack(packPath, sourcePath, themeName: null, replaceExisting: false);
            return true;
        }
        catch (ArgumentException ex) when (IsDuplicateName(ex))
        {
            var replace = KryptonMessageBox.Show(this,
                ex.Message + Environment.NewLine + Environment.NewLine + @"Replace the existing theme?",
                @"Duplicate theme name",
                KryptonMessageBoxButtons.YesNo,
                KryptonMessageBoxIcon.Question);
            if (replace != DialogResult.Yes)
            {
                return false;
            }

            try
            {
                KryptonPaletteFile.AddToPack(packPath, sourcePath, themeName: null, replaceExisting: true);
                return true;
            }
            catch (Exception retryEx)
            {
                ShowError(retryEx, @"Add to pack");
                return false;
            }
        }
        catch (Exception ex)
        {
            ShowError(ex, @"Add to pack");
            return false;
        }
    }

    private void RemoveSelectedTheme()
    {
        var path = RequirePackPath(createIfMissing: false);
        if (path == null)
        {
            return;
        }

        var themeName = klstThemes.GetItemText(klstThemes.SelectedItem);
        if (string.IsNullOrWhiteSpace(themeName))
        {
            SetStatus(@"Select a theme to remove.");
            return;
        }

        try
        {
            KryptonPaletteFile.RemoveFromPack(path, themeName!);
            RefreshThemes();
            SetStatus($@"Removed '{themeName}'.");
        }
        catch (Exception ex)
        {
            ShowError(ex, @"Remove from pack");
        }
    }

    private void RefreshThemes()
    {
        klstThemes.Items.Clear();
        var path = PackPath;
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
        {
            if (string.IsNullOrWhiteSpace(ktxtPackName.Text) && !string.IsNullOrWhiteSpace(path))
            {
                ktxtPackName.Text = Path.GetFileNameWithoutExtension(path);
            }

            kbtnRemove.Enabled = false;
            kbtnSaveName.Enabled = File.Exists(path);
            SetStatus(string.IsNullOrWhiteSpace(path)
                ? @"Choose a .kpal pack, then add .kpalx files."
                : @"Pack file does not exist yet. Add a .kpalx file to create it.");
            return;
        }

        try
        {
            ktxtPackName.Text = KryptonPaletteFile.GetPackName(path);
            var names = KryptonPaletteFile.GetThemeNames(path);
            for (var i = 0; i < names.Length; i++)
            {
                klstThemes.Items.Add(names[i]);
            }

            if (klstThemes.Items.Count > 0)
            {
                klstThemes.SelectedIndex = 0;
            }

            kbtnRemove.Enabled = klstThemes.Items.Count > 1;
            kbtnSaveName.Enabled = KryptonPaletteFile.IsPack(path);
            var packLabel = KryptonPaletteFile.IsPack(path) ? @"pack" : @"single-theme .kpal (will become a pack on add)";
            SetStatus($@"{names.Length} theme(s) in {packLabel}.");
        }
        catch (Exception ex)
        {
            kbtnRemove.Enabled = false;
            kbtnSaveName.Enabled = false;
            ShowError(ex, @"Open pack");
        }
    }

    private string? RequirePackPath(bool createIfMissing)
    {
        var path = PackPath;
        if (!string.IsNullOrWhiteSpace(path))
        {
            return path;
        }

        if (!createIfMissing)
        {
            SetStatus(@"Choose a .kpal pack first.");
            return null;
        }

        using var dialog = new SaveFileDialog
        {
            Title = @"Create palette pack",
            Filter = @"Binary palette files (*.kpal)|*.kpal|All files (*.*)|*.*",
            DefaultExt = KryptonPaletteFile.BinaryExtension,
            FileName = @"themes.kpal",
            OverwritePrompt = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return null;
        }

        ktxtPackPath.Text = dialog.FileName;
        if (string.IsNullOrWhiteSpace(ktxtPackName.Text))
        {
            ktxtPackName.Text = Path.GetFileNameWithoutExtension(dialog.FileName);
        }

        return dialog.FileName;
    }

    private void PersistPackNameIfNeeded(string packPath)
    {
        if (!File.Exists(packPath) || !KryptonPaletteFile.IsPack(packPath))
        {
            return;
        }

        var desired = ktxtPackName.Text.Trim();
        var current = KryptonPaletteFile.GetPackName(packPath);
        if (string.Equals(desired, current, StringComparison.Ordinal))
        {
            return;
        }

        try
        {
            KryptonPaletteFile.SetPackName(packPath, desired);
            ktxtPackName.Text = KryptonPaletteFile.GetPackName(packPath);
        }
        catch (Exception)
        {
            // Name save is best-effort after add; the themes were already written.
        }
    }

    private static bool IsDuplicateName(Exception ex)
    {
        var message = ex.GetBaseException().Message;
        return message.IndexOf(@"Duplicate palette name", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private static string InitialDirectory(string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return string.Empty;
        }

        var directory = Path.GetDirectoryName(path);
        return string.IsNullOrEmpty(directory) ? string.Empty : directory;
    }

    private void SetStatus(string text) => klblStatus.Text = text;

    private void ShowError(Exception ex, string title)
    {
        var message = ex.GetBaseException().Message;
        SetStatus(message);
        KryptonMessageBox.Show(this, message, title, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
    }
}
