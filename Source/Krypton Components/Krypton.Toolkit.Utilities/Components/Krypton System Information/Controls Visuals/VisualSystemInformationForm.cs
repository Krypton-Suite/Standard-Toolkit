#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Krypton-themed System Information window modelled on Windows msinfo32.
/// </summary>
internal partial class VisualSystemInformationForm : KryptonForm
{
    private readonly KryptonSystemInformationData _data;
    private readonly Dictionary<string, SystemInformationTable> _cache = new Dictionary<string, SystemInformationTable>(StringComparer.Ordinal);
    private readonly List<int> _visibleRowIndexes = new List<int>();
    private readonly List<TreeNode> _treeMatches = new List<TreeNode>();
    private CancellationTokenSource? _collectCts;
    private int _collectGeneration;
    private int _treeMatchIndex;
    private int _printRow;
    private SystemInformationTable? _currentTable;
    private TreeNode? _initialNode;

    public VisualSystemInformationForm(KryptonSystemInformationData data)
    {
        InitializeComponent();
        SetInheritedControlOverride();
        _data = data;
        KeyPreview = true;
        ApplyStrings();
        ApplyRtl();
        kbtnWindowsMsinfo.Visible = data.ShowWindowsSystemInformation ?? true;
        kchkAllModules.Checked = data.EnumerateAllProcessModules ?? false;
        _initialNode = SystemInformationCatalog.Populate(ktvCategories, data.InitialCategoryId);
        RefreshSearchSuggestions();
        if (_initialNode != null)
        {
            ktvCategories.SelectedNode = _initialNode;
        }
    }

    /// <summary>
    /// TreeView does not raise <see cref="TreeView.AfterSelect"/> when the initial node is assigned before the handle exists.
    /// Load that category once the form is shown.
    /// </summary>
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        EnsureInitialCategoryLoaded();
    }

    private void EnsureInitialCategoryLoaded()
    {
        if (_currentTable != null || _collectCts != null)
        {
            return;
        }

        var node = ktvCategories.SelectedNode ?? _initialNode;
        if (node == null)
        {
            return;
        }

        if (!ReferenceEquals(ktvCategories.SelectedNode, node))
        {
            ktvCategories.SelectedNode = node;
        }

        kchkAllModules.Visible = string.Equals(node.Tag as string, SystemInformationCategoryId.SoftwareLoadedModules, StringComparison.Ordinal);

        // AfterSelect may already have started a collect once the handle exists.
        if (_currentTable != null || _collectCts != null)
        {
            return;
        }

        LoadCategory(node, false);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        CancelPendingCollect();
        base.OnFormClosed(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F3)
        {
            FindNextTreeMatch();
            e.Handled = true;
        }

        base.OnKeyDown(e);
    }

    internal void CancelPendingCollect()
    {
        try
        {
            _collectCts?.Cancel();
        }
        catch (ObjectDisposedException)
        {
            // Already disposed.
        }

        _collectCts?.Dispose();
        _collectCts = null;
    }

    private void ApplyStrings()
    {
        var strings = KryptonSystemInformation.Strings;
        Text = strings.WindowTitle;
        ksbFind.SearchBoxValues.PlaceholderText = strings.Find;
        kbtnFindNext.Values.Text = strings.FindNext;
        kbtnCopy.Values.Text = strings.Copy;
        kbtnSave.Values.Text = strings.Save;
        kbtnPrint.Values.Text = strings.Print;
        kbtnRefresh.Values.Text = strings.Refresh;
        kchkAllModules.Values.Text = strings.AllProcessModules;
        kbtnWindowsMsinfo.Values.Text = strings.WindowsSystemInformation;
        kbtnClose.Values.Text = strings.Close;
        tslStatus.Text = strings.Ready;
    }

    private void ApplyRtl()
    {
        if (_data.UseRtlLayout != KryptonUseRTLLayout.Yes)
        {
            return;
        }

        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
    }

    private void ktvCategories_AfterSelect(object sender, TreeViewEventArgs e)
    {
        kchkAllModules.Visible = string.Equals(e.Node?.Tag as string, SystemInformationCategoryId.SoftwareLoadedModules, StringComparison.Ordinal);
        LoadCategory(e.Node, false);
    }

    private void kbtnRefresh_Click(object sender, EventArgs e)
    {
        SystemInformationWmi.InvalidateCache();
        LoadCategory(ktvCategories.SelectedNode, true);
    }

    private void kbtnClose_Click(object sender, EventArgs e) => Close();

    private void kbtnWindowsMsinfo_Click(object sender, EventArgs e) => GlobalToolkitUtilities.LaunchProcess(@"MSInfo32.exe");

    private void ksbFind_TextChanged(object sender, EventArgs e)
    {
        ApplyFindFilter();
        RebuildTreeMatches();
    }

    private void ksbFind_Search(object sender, SearchEventArgs e) => FindNextTreeMatch();

    private void ksbFind_SearchCleared(object sender, EventArgs e)
    {
        ApplyFindFilter();
        RebuildTreeMatches();
    }

    private void kbtnFindNext_Click(object sender, EventArgs e) => FindNextTreeMatch();

    private void kchkAllModules_CheckedChanged(object sender, EventArgs e)
    {
        if (kchkAllModules.Visible)
        {
            LoadCategory(ktvCategories.SelectedNode, true);
        }
    }

    private void kdgvDetails_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
    {
        if (_currentTable == null || e.RowIndex < 0 || e.RowIndex >= _visibleRowIndexes.Count)
        {
            return;
        }

        var sourceIndex = _visibleRowIndexes[e.RowIndex];
        if (sourceIndex < 0 || sourceIndex >= _currentTable.Rows.Count)
        {
            return;
        }

        var row = _currentTable.Rows[sourceIndex];
        e.Value = e.ColumnIndex >= 0 && e.ColumnIndex < row.Length ? row[e.ColumnIndex] : string.Empty;
    }

    private void kbtnCopy_Click(object sender, EventArgs e)
    {
        if (_currentTable == null)
        {
            return;
        }

        var builder = new StringBuilder();
        builder.AppendLine(string.Join("\t", _currentTable.Columns));
        foreach (var index in _visibleRowIndexes)
        {
            builder.AppendLine(string.Join("\t", _currentTable.Rows[index]));
        }

        Clipboard.SetText(builder.ToString());
    }

    private async void kbtnSave_Click(object sender, EventArgs e)
    {
        var strings = KryptonSystemInformation.Strings;
        using var dialog = new SaveFileDialog
        {
            Filter = strings.SaveFilter,
            FileName = "SystemInformation.txt",
            OverwritePrompt = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        var saveAll = KryptonMessageBox.Show(this, strings.SaveAll, strings.WindowTitle,
            KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Question) == DialogResult.Yes;

        var path = dialog.FileName;
        tslStatus.Text = strings.Saving;
        kbtnSave.Enabled = false;
        try
        {
            var allModules = kchkAllModules.Checked;
            var selectedTitle = ktvCategories.SelectedNode?.Text;
            var current = _currentTable;
            var snapshot = new Dictionary<string, SystemInformationTable>(_cache, StringComparer.Ordinal);
            var text = await Task.Run(() => saveAll
                ? BuildFullExport(snapshot, allModules, CancellationToken.None)
                : FormatTable(selectedTitle, current)).ConfigureAwait(true);
            File.WriteAllText(path, text, new UTF8Encoding(true));
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show(this, ex.Message, strings.WindowTitle, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
        finally
        {
            kbtnSave.Enabled = true;
            UpdateReadyStatus();
        }
    }

    private void kbtnPrint_Click(object sender, EventArgs e)
    {
        if (_currentTable == null)
        {
            return;
        }

        _printRow = 0;
        using var document = new PrintDocument();
        document.DocumentName = Text;
        document.PrintPage += DocumentOnPrintPage;
        using var preview = new PrintPreviewDialog
        {
            Document = document,
            Width = 800,
            Height = 600
        };
        preview.ShowDialog(this);
    }

    private void DocumentOnPrintPage(object sender, PrintPageEventArgs e)
    {
        if (_currentTable == null || e.Graphics == null)
        {
            e.HasMorePages = false;
            return;
        }

        var font = Font;
        float y = e.MarginBounds.Top;
        var lineHeight = font.GetHeight(e.Graphics) + 2f;
        if (_printRow == 0)
        {
            e.Graphics.DrawString(ktvCategories.SelectedNode?.Text ?? Text, font, Brushes.Black, e.MarginBounds.Left, y);
            y += lineHeight;
            e.Graphics.DrawString(string.Join(" | ", _currentTable.Columns), font, Brushes.Black, e.MarginBounds.Left, y);
            y += lineHeight;
        }

        while (_printRow < _visibleRowIndexes.Count)
        {
            if (y + lineHeight > e.MarginBounds.Bottom)
            {
                e.HasMorePages = true;
                return;
            }

            var row = _currentTable.Rows[_visibleRowIndexes[_printRow]];
            e.Graphics.DrawString(string.Join(" | ", row), font, Brushes.Black, e.MarginBounds.Left, y);
            y += lineHeight;
            _printRow++;
        }

        e.HasMorePages = false;
        _printRow = 0;
    }

    private void LoadCategory(TreeNode? node, bool forceRefresh)
    {
        if (node?.Tag is not string categoryId)
        {
            return;
        }

        if (!forceRefresh && _cache.TryGetValue(categoryId, out var cached))
        {
            BindTable(cached);
            return;
        }

        if (forceRefresh)
        {
            _cache.Remove(categoryId);
        }

        CancelPendingCollect();
        _collectCts = new CancellationTokenSource();
        var token = _collectCts.Token;
        var generation = Interlocked.Increment(ref _collectGeneration);
        tslStatus.Text = KryptonSystemInformation.Strings.Collecting;
        kdgvDetails.RowCount = 0;
        _visibleRowIndexes.Clear();

        var allModules = kchkAllModules.Checked;
        _ = CollectAsync(categoryId, generation, token, allModules);
    }

    private async Task CollectAsync(string categoryId, int generation, CancellationToken token, bool allProcessModules)
    {
        SystemInformationTable? table = null;
        try
        {
            table = await Task.Run(() => SystemInformationCollector.Collect(categoryId, token, allProcessModules), token).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            return;
        }
        catch (Exception ex)
        {
            table = SystemInformationTable.ItemValue();
            table.AddRow(ex.Message, string.Empty);
        }

        if (table == null)
        {
            return;
        }

        var captured = table;
        void Apply()
        {
            if (IsDisposed || generation != _collectGeneration)
            {
                return;
            }

            _cache[categoryId] = captured;
            BindTable(captured);
        }

        if (IsDisposed)
        {
            return;
        }

        if (InvokeRequired)
        {
            try
            {
                BeginInvoke(new Action(Apply));
            }
            catch (ObjectDisposedException)
            {
                // Form closed while collecting.
            }
        }
        else
        {
            Apply();
        }
    }

    private void BindTable(SystemInformationTable table)
    {
        _currentTable = table;
        kdgvDetails.Columns.Clear();
        foreach (var column in table.Columns)
        {
            kdgvDetails.Columns.Add(column, column);
        }

        ApplyFindFilter();
        UpdateReadyStatus();
    }

    private void ApplyFindFilter()
    {
        _visibleRowIndexes.Clear();
        var filter = ksbFind.Text;
        var hasFilter = !string.IsNullOrWhiteSpace(filter);
        if (_currentTable != null)
        {
            for (var i = 0; i < _currentTable.Rows.Count; i++)
            {
                if (!hasFilter || RowMatches(_currentTable.Rows[i], filter))
                {
                    _visibleRowIndexes.Add(i);
                }
            }
        }

        kdgvDetails.RowCount = 0;
        kdgvDetails.RowCount = _visibleRowIndexes.Count;
        kdgvDetails.Invalidate();
    }

    private static bool RowMatches(string[] row, string filter)
    {
        foreach (var cell in row)
        {
            if (!string.IsNullOrEmpty(cell) && cell.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                return true;
            }
        }

        return false;
    }

    private void RefreshSearchSuggestions()
    {
        var names = new List<string>();
        CollectNodeTexts(ktvCategories.Nodes, names);
        ksbFind.SearchBoxValues.EnableSuggestions = names.Count > 0;
        ksbFind.SetSearchSuggestions(names);
    }

    private static void CollectNodeTexts(TreeNodeCollection nodes, List<string> names)
    {
        foreach (TreeNode node in nodes)
        {
            names.Add(node.Text);
            CollectNodeTexts(node.Nodes, names);
        }
    }

    private void RebuildTreeMatches()
    {
        _treeMatches.Clear();
        _treeMatchIndex = -1;
        var filter = ksbFind.Text;
        if (string.IsNullOrWhiteSpace(filter))
        {
            return;
        }

        CollectTreeMatches(ktvCategories.Nodes, filter);
    }

    private void CollectTreeMatches(TreeNodeCollection nodes, string filter)
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Text.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                _treeMatches.Add(node);
            }

            CollectTreeMatches(node.Nodes, filter);
        }
    }

    private void FindNextTreeMatch()
    {
        if (_treeMatches.Count == 0)
        {
            RebuildTreeMatches();
        }

        if (_treeMatches.Count == 0)
        {
            return;
        }

        _treeMatchIndex = (_treeMatchIndex + 1) % _treeMatches.Count;
        ktvCategories.SelectedNode = _treeMatches[_treeMatchIndex];
        _treeMatches[_treeMatchIndex].EnsureVisible();
    }

    private void UpdateReadyStatus()
    {
        var strings = KryptonSystemInformation.Strings;
        tslStatus.Text = string.Format(CultureInfo.CurrentCulture, "{0} — {1}", strings.Ready,
            string.Format(CultureInfo.CurrentCulture, strings.ItemsFormat, _visibleRowIndexes.Count));
    }

    private static string BuildFullExport(Dictionary<string, SystemInformationTable> snapshot, bool allProcessModules, CancellationToken cancellationToken)
    {
        var builder = new StringBuilder();
        foreach (var id in SystemInformationCollector.LeafCategoryIds)
        {
            if (!snapshot.TryGetValue(id, out SystemInformationTable? table) || table is null)
            {
                try
                {
                    table = SystemInformationCollector.Collect(id, cancellationToken, allProcessModules);
                }
                catch (Exception ex)
                {
                    table = SystemInformationTable.ItemValue();
                    table.AddRow(ex.Message, string.Empty);
                }
            }

            builder.Append(FormatTable(id, table));
            builder.AppendLine();
        }

        return builder.ToString();
    }

    private static string FormatTable(string? title, SystemInformationTable? table)
    {
        var builder = new StringBuilder();
        builder.AppendLine(title ?? string.Empty);
        builder.AppendLine(new string('=', Math.Max(8, title?.Length ?? 8)));
        if (table == null)
        {
            return builder.ToString();
        }

        builder.AppendLine(string.Join("\t", table.Columns));
        foreach (var row in table.Rows)
        {
            builder.AppendLine(string.Join("\t", row));
        }

        return builder.ToString();
    }
}
