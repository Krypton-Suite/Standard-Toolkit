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
    private readonly KryptonPaletteCollectionEditorStrings _strings;

    internal VisualKryptonPaletteCollectionEditorForm()
        : this(null, null)
    {
    }

    internal VisualKryptonPaletteCollectionEditorForm(string? collectionPath)
        : this(collectionPath, null)
    {
    }

    internal VisualKryptonPaletteCollectionEditorForm(string? collectionPath, KryptonPaletteCollectionEditorStrings? strings)
    {
        _strings = strings ?? new KryptonPaletteCollectionEditorStrings();
        InitializeComponent();
        kbtnBrowse.Click += (_, _) => BrowseCollection();
        kbtnSaveName.Click += (_, _) => SaveCollectionName();
        kbtnAdd.Click += (_, _) => AddThemes();
        kbtnRemove.Click += (_, _) => RemoveSelectedTheme();
        kbtnClose.Click += (_, _) => Close();
        CancelButton = kbtnClose;
        SetupThemeListView();
        ApplyStrings();
        PopulateViews();

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
            Title = _strings.OpenCollectionTitle,
            Filter = _strings.ThemeContainerDialogFilter,
            DefaultExt = KryptonPaletteFile.BinaryExtension,
            CheckFileExists = false,
            FileName = string.IsNullOrWhiteSpace(ktxtCollectionPath.Text)
                ? _strings.CollectionFileName
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
            SetStatus(_strings.AddThemeBeforeSavingName);
            return;
        }

        try
        {
            KryptonPaletteFile.SetCollectionName(path, ktxtCollectionName.Text);
            SetStatus(string.Format(_strings.CollectionNameSavedFormat, KryptonPaletteFile.GetCollectionName(path)));
        }
        catch (Exception ex)
        {
            ShowError(ex, _strings.SaveCollectionNameTitle);
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
            Title = _strings.AddFilesTitle,
            Filter = _strings.AddFilesFilter,
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
            ? _strings.NoThemesAdded
            : string.Format(_strings.AddedFilesFormat, added));
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
                ex.Message + Environment.NewLine + Environment.NewLine + _strings.ReplaceExistingTheme,
                _strings.DuplicateThemeTitle,
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
                ShowError(retryEx, _strings.AddToCollectionTitle);
                return false;
            }
        }
        catch (Exception ex)
        {
            ShowError(ex, _strings.AddToCollectionTitle);
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

        var themeName = SelectedThemeName();
        if (string.IsNullOrWhiteSpace(themeName))
        {
            SetStatus(_strings.SelectThemeToRemove);
            return;
        }

        try
        {
            KryptonPaletteFile.RemoveFromCollection(path, themeName!);
            RefreshThemes();
            SetStatus(string.Format(_strings.RemovedThemeFormat, themeName));
        }
        catch (Exception ex)
        {
            ShowError(ex, _strings.RemoveFromCollectionTitle);
        }
    }

    private void RefreshThemes()
    {
        klvThemes.Items.Clear();
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
                ? _strings.StatusChooseThenAdd
                : _strings.CollectionMissingAddToCreate);
            return;
        }

        try
        {
            ktxtCollectionName.Text = KryptonPaletteFile.GetCollectionName(path);
            var names = KryptonPaletteFile.GetThemeNames(path);
            for (var i = 0; i < names.Length; i++)
            {
                klvThemes.Items.Add(CreateThemeItem(names[i]));
            }

            if (klvThemes.Items.Count > 0)
            {
                klvThemes.Items[0].Selected = true;
                klvThemes.Items[0].Focused = true;
            }

            kbtnRemove.Enabled = klvThemes.Items.Count > 1;
            kbtnSaveName.Enabled = KryptonPaletteFile.IsCollection(path);
            var collectionLabel = KryptonPaletteFile.IsCollection(path)
                ? _strings.CollectionKindCollection
                : _strings.CollectionKindSingleTheme;
            SetStatus(string.Format(_strings.ThemeCountFormat, names.Length, collectionLabel));
        }
        catch (Exception ex)
        {
            kbtnRemove.Enabled = false;
            kbtnSaveName.Enabled = false;
            ShowError(ex, _strings.OpenCollectionErrorTitle);
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
            SetStatus(_strings.ChooseCollectionFirst);
            return null;
        }

        using var dialog = new SaveFileDialog
        {
            Title = _strings.CreateCollectionTitle,
            Filter = _strings.ThemeContainerDialogFilter,
            DefaultExt = KryptonPaletteFile.BinaryExtension,
            FileName = _strings.CollectionFileName,
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

    private string? SelectedThemeName()
    {
        if (klvThemes.SelectedItems.Count == 0)
        {
            return null;
        }

        var selected = klvThemes.SelectedItems[0];
        return selected.Tag as string ?? selected.Text;
    }

    private static ListViewItem CreateThemeItem(string themeName)
    {
        var normalised = KryptonPaletteFile.NormalizeCollectionThemeName(themeName);
        var slash = normalised.LastIndexOf(KryptonPaletteFile.CollectionPathSeparator);
        string theme;
        string folder;
        if (slash <= 0)
        {
            theme = string.IsNullOrEmpty(normalised) ? themeName : normalised;
            folder = string.Empty;
        }
        else
        {
            folder = KryptonPaletteFile.ToDisplayPath(normalised.Substring(0, slash));
            theme = normalised.Substring(slash + 1);
        }

        var item = new ListViewItem(theme)
        {
            Tag = themeName,
            ToolTipText = string.IsNullOrEmpty(normalised)
                ? themeName
                : KryptonPaletteFile.ToDisplayPath(normalised)
        };
        item.SubItems.Add(folder);
        return item;
    }

    private void SetStatus(string text) => klblStatus.Text = text;

    private void SetupThemeListView()
    {
        klvThemes.View = View.Details;
        klvThemes.FullRowSelect = true;
        klvThemes.MultiSelect = false;
        klvThemes.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        klvThemes.ShowItemToolTips = true;
        if (klvThemes.Columns.Count == 0)
        {
            klvThemes.Columns.Add(string.Empty, 240);
            klvThemes.Columns.Add(string.Empty, -2);
        }
    }

    private void ApplyColumnHeadings()
    {
        if (klvThemes.Columns.Count > 0)
        {
            klvThemes.Columns[0].Text = _strings.ColumnTheme;
        }

        if (klvThemes.Columns.Count > 1)
        {
            klvThemes.Columns[1].Text = _strings.ColumnFolder;
        }
    }

    private void ApplyStrings()
    {
        var s = _strings;
        Text = s.WindowTitle;
        kwlblInfo.Text = s.Info;
        klblCollectionPath.Values.Text = s.CollectionFileLabel;
        kbtnBrowse.Values.Text = s.Browse;
        klblCollectionName.Values.Text = s.CollectionNameLabel;
        kbtnSaveName.Values.Text = s.SaveName;
        klblThemes.Values.Text = s.ThemesLabel;
        kbtnAdd.Values.Text = s.Add;
        kbtnRemove.Values.Text = s.Remove;
        kbtnClose.Values.Text = KryptonManager.Strings.GeneralStrings.Close;
        klblViewBy.Values.Text = s.ViewByLabel;
        klblStatus.Values.Text = s.StatusChooseThenAdd;
        ApplyColumnHeadings();
    }

    private void PopulateViews()
    {
        var selected = klvThemes.View;
        kcmbViewBy.Items.Clear();
        kcmbViewBy.Items.Add(new ViewDisplayItem(View.LargeIcon, _strings.ViewLargeIcon));
        kcmbViewBy.Items.Add(new ViewDisplayItem(View.Details, _strings.ViewDetails));
        kcmbViewBy.Items.Add(new ViewDisplayItem(View.SmallIcon, _strings.ViewSmallIcon));
        kcmbViewBy.Items.Add(new ViewDisplayItem(View.List, _strings.ViewList));
        kcmbViewBy.Items.Add(new ViewDisplayItem(View.Tile, _strings.ViewTile));
        var index = 0;
        for (var i = 0; i < kcmbViewBy.Items.Count; i++)
        {
            if (kcmbViewBy.Items[i] is ViewDisplayItem item && item.View == selected)
            {
                index = i;
                break;
            }
        }

        kcmbViewBy.SelectedIndex = index;
    }

    private void ShowError(Exception ex, string title)
    {
        var message = ex.GetBaseException().Message;
        SetStatus(message);
        KryptonMessageBox.Show(this, message, title, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
    }

    private void kcmbViewBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (kcmbViewBy.SelectedItem is ViewDisplayItem item)
        {
            klvThemes.View = item.View;
        }
    }

    private sealed class ViewDisplayItem
    {
        internal ViewDisplayItem(View view, string text)
        {
            View = view;
            Text = text;
        }

        internal View View { get; }

        private string Text { get; }

        public override string ToString() => Text;
    }
}
