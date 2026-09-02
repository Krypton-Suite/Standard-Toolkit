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
/// Dialog that adds palette files to a <c>.ktheme</c> collection and removes named themes.
/// </summary>
internal partial class VisualKryptonPaletteCollectionEditorForm : KryptonForm
{
    internal VisualKryptonPaletteCollectionEditorForm()
    {
        InitializeComponent();
        kbtnBrowse.Click += (_, _) => BrowseCollection();
        kbtnSaveName.Click += (_, _) => SaveCollectionName();
        kbtnAdd.Click += (_, _) => AddThemes();
        kbtnRemove.Click += (_, _) => RemoveSelectedTheme();
        kbtnClose.Click += (_, _) => Close();
        CancelButton = kbtnClose;
    }

    internal VisualKryptonPaletteCollectionEditorForm(string? collectionPath)
        : this()
    {
        if (!string.IsNullOrWhiteSpace(collectionPath))
        {
            ktxtCollectionPath.Text = collectionPath;
            RefreshThemes();
        }
    }

    /// <summary>
    /// Pack path shown in the dialog after close (browse or add may change it).
    /// </summary>
    internal string CollectionPath => ktxtCollectionPath.Text.Trim();

    private void BrowseCollection()
    {
        using var dialog = new OpenFileDialog
        {
            Title = @"Open or create palette collection",
            Filter = @"Krypton theme containers (*.ktheme)|*.ktheme|All files (*.*)|*.*",
            DefaultExt = KryptonPaletteFile.BinaryExtension,
            CheckFileExists = false,
            FileName = string.IsNullOrWhiteSpace(ktxtCollectionPath.Text)
                ? @"themes.ktheme"
                : Path.GetFileName(ktxtCollectionPath.Text),
            InitialDirectory = InitialDirectory(ktxtCollectionPath.Text)
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        ktxtCollectionPath.Text = dialog.FileName;
        RefreshThemes();
    }

    private void SaveCollectionName()
    {
        var path = RequireCollectionPath(createIfMissing: false);
        if (path == null)
        {
            return;
        }

        if (!File.Exists(path))
        {
            SetStatus(@"Add a theme before saving the collection name.");
            return;
        }

        try
        {
            KryptonPaletteFile.SetCollectionName(path, ktxtCollectionName.Text);
            SetStatus($@"Collection name saved as '{KryptonPaletteFile.GetCollectionName(path)}'.");
        }
        catch (Exception ex)
        {
            ShowError(ex, @"Save collection name");
        }
    }

    private void AddThemes()
    {
        var path = RequireCollectionPath(createIfMissing: true);
        if (path == null)
        {
            return;
        }

        using var dialog = new OpenFileDialog
        {
            Title = @"Add palette files to collection",
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
        PersistCollectionNameIfNeeded(path);
        SetStatus(added == 0
            ? @"No themes were added."
            : $@"Added {added} file(s) to the collection.");
    }

    private bool TryAddSource(string collectionPath, string sourcePath)
    {
        try
        {
            KryptonPaletteFile.AddToCollection(collectionPath, sourcePath, themeName: null, replaceExisting: false);
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
                KryptonPaletteFile.AddToCollection(collectionPath, sourcePath, themeName: null, replaceExisting: true);
                return true;
            }
            catch (Exception retryEx)
            {
                ShowError(retryEx, @"Add to collection");
                return false;
            }
        }
        catch (Exception ex)
        {
            ShowError(ex, @"Add to collection");
            return false;
        }
    }

    private void RemoveSelectedTheme()
    {
        var path = RequireCollectionPath(createIfMissing: false);
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
            KryptonPaletteFile.RemoveFromCollection(path, themeName!);
            RefreshThemes();
            SetStatus($@"Removed '{themeName}'.");
        }
        catch (Exception ex)
        {
            ShowError(ex, @"Remove from collection");
        }
    }

    private void RefreshThemes()
    {
        klstThemes.Items.Clear();
        var path = CollectionPath;
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
        {
            if (string.IsNullOrWhiteSpace(ktxtCollectionName.Text) && !string.IsNullOrWhiteSpace(path))
            {
                ktxtCollectionName.Text = Path.GetFileNameWithoutExtension(path);
            }

            kbtnRemove.Enabled = false;
            kbtnSaveName.Enabled = File.Exists(path);
            SetStatus(string.IsNullOrWhiteSpace(path)
                ? @"Choose a .ktheme collection, then add .kthemex files."
                : @"Collection file does not exist yet. Add a .kthemex file to create it.");
            return;
        }

        try
        {
            ktxtCollectionName.Text = KryptonPaletteFile.GetCollectionName(path);
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
            kbtnSaveName.Enabled = KryptonPaletteFile.IsCollection(path);
            var collectionLabel = KryptonPaletteFile.IsCollection(path) ? @"collection" : @"single-theme .ktheme (will become a collection on add)";
            SetStatus($@"{names.Length} theme(s) in {collectionLabel}.");
        }
        catch (Exception ex)
        {
            kbtnRemove.Enabled = false;
            kbtnSaveName.Enabled = false;
            ShowError(ex, @"Open collection");
        }
    }

    private string? RequireCollectionPath(bool createIfMissing)
    {
        var path = CollectionPath;
        if (!string.IsNullOrWhiteSpace(path))
        {
            return path;
        }

        if (!createIfMissing)
        {
            SetStatus(@"Choose a .ktheme collection first.");
            return null;
        }

        using var dialog = new SaveFileDialog
        {
            Title = @"Create palette collection",
            Filter = @"Krypton theme containers (*.ktheme)|*.ktheme|All files (*.*)|*.*",
            DefaultExt = KryptonPaletteFile.BinaryExtension,
            FileName = @"themes.ktheme",
            OverwritePrompt = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return null;
        }

        ktxtCollectionPath.Text = dialog.FileName;
        if (string.IsNullOrWhiteSpace(ktxtCollectionName.Text))
        {
            ktxtCollectionName.Text = Path.GetFileNameWithoutExtension(dialog.FileName);
        }

        return dialog.FileName;
    }

    private void PersistCollectionNameIfNeeded(string collectionPath)
    {
        if (!File.Exists(collectionPath) || !KryptonPaletteFile.IsCollection(collectionPath))
        {
            return;
        }

        var desired = ktxtCollectionName.Text.Trim();
        var current = KryptonPaletteFile.GetCollectionName(collectionPath);
        if (string.Equals(desired, current, StringComparison.Ordinal))
        {
            return;
        }

        try
        {
            KryptonPaletteFile.SetCollectionName(collectionPath, desired);
            ktxtCollectionName.Text = KryptonPaletteFile.GetCollectionName(collectionPath);
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
