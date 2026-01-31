#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public partial class InternalSearchableExceptionTreeView : UserControl
{
    #region Instance Fields

    private bool _showSearchFeatures;

    private readonly List<KryptonTreeNode> _originalNodes = new List<KryptonTreeNode>();

    #endregion

    #region Public

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowSearchFeatures
    {
        get => _showSearchFeatures;
        set
        {
            _showSearchFeatures = value;

            Invalidate();
        }
    }

    public Exception? SelectedException => kietvException.SelectedException;

    public KryptonTreeView Tree => kietvException;

    public KryptonTextBox SearchBox => ktxtSearchBox;

    #endregion

    #region Events

    public event TreeViewEventHandler? NodeSelected
    {
        add => kietvException.AfterSelect += value;
        remove => kietvException.AfterSelect -= value;
    }

    #endregion

    #region Identity

    public InternalSearchableExceptionTreeView()
    {
        InitializeComponent();

        kwlblSearchResults.Text = KryptonManager.Strings.ExceptionDialogStrings.TypeToSearch;

        bsaClearSearch.ToolTipTitle = KryptonManager.Strings.SearchBoxStrings.ClearSearchBoxToolTip;

        bsaClearSearch.ToolTipBody = KryptonManager.Strings.SearchBoxStrings.ClearSearchBoxToolTipDescription;
    }

    #endregion

    #region Implementation

    public void Populate(Exception exception)
    {
        kietvException.Populate(exception);
        _originalNodes.Clear();

        foreach (KryptonTreeNode node in kietvException.Nodes)
        {
            _originalNodes.Add((KryptonTreeNode)node.Clone());
        }
    }

    private void Search(string searchQueryText)
    {
        string searchText = searchQueryText.Trim().ToLowerInvariant();

        kietvException.BeginUpdate();
        kietvException.Nodes.Clear();

        int matchCount = 0;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            // Reset: restore all original nodes
            foreach (KryptonTreeNode original in _originalNodes)
            {
                kietvException.Nodes.Add((KryptonTreeNode)original.Clone());
            }

            // Reset label and styling
            kwlblSearchResults.Text = KryptonManager.Strings.ExceptionDialogStrings.TypeToSearch;
            kwlblSearchResults.StateCommon.TextColor = Color.Gray;

            kietvException.EndUpdate();

            // Select first node if available
            if (kietvException.Nodes.Count > 0)
            {
                kietvException.SelectedNode = kietvException.Nodes[0];
            }

            return;
        }

        bsaClearSearch.Visible = !string.IsNullOrEmpty(searchText);

        // If searching
        foreach (KryptonTreeNode original in _originalNodes)
        {
            KryptonTreeNode? filtered = FilterAndCloneNode(original, searchText, ref matchCount);
            if (filtered != null)
            {
                kietvException.Nodes.Add(filtered);
            }
        }

        kietvException.EndUpdate();

        // Update label
        if (string.IsNullOrWhiteSpace(searchText))
        {
            kwlblSearchResults.Text = KryptonManager.Strings.ExceptionDialogStrings.TypeToSearch;
        }
        else if (matchCount == 0)
        {
            kwlblSearchResults.Text = KryptonManager.Strings.ExceptionDialogStrings.NoMatchesFound;
        }
        else
        {
            kwlblSearchResults.Text = $@"{matchCount} {KryptonManager.Strings.ExceptionDialogStrings.Result}{(matchCount == 1 ? "" : $"{KryptonManager.Strings.ExceptionDialogStrings.ResultsAppendage}")} {KryptonManager.Strings.ExceptionDialogStrings.ResultsFoundAppendage}";
        }

        kwlblSearchResults.StateCommon.TextColor = SystemColors.ControlText;

        // Auto-select match or fallback
        var firstMatch = kietvException.Nodes
            .Cast<KryptonTreeNode>()
            .SelectMany(FlattenTree)
            .FirstOrDefault(n => n.BackColor == Color.LightYellow);

        kietvException.SelectedNode = firstMatch ??
                                      (kietvException.Nodes.Count > 0 ? kietvException.Nodes[0] : null);

        kwlblSearchResults.StateCommon.TextColor = string.IsNullOrWhiteSpace(searchQueryText) ? Color.Gray : Color.Empty;
    }

    private IEnumerable<KryptonTreeNode> FlattenTree(KryptonTreeNode root)
    {
        yield return root;

        foreach (KryptonTreeNode child in root.Nodes)
        {
            foreach (var descendant in FlattenTree(child))
            {
                yield return descendant;
            }
        }
    }

    private KryptonTreeNode? FilterAndCloneNode(KryptonTreeNode node, string searchQuery, ref int matchCount)
    {
        bool isMatch = node.Text.ToLowerInvariant().Contains(searchQuery);
        KryptonTreeNode clone = (KryptonTreeNode)node.Clone();
        clone.Nodes.Clear();

        foreach (KryptonTreeNode child in node.Nodes)
        {
            KryptonTreeNode? filteredChild = FilterAndCloneNode(child, searchQuery, ref matchCount);
            if (filteredChild != null)
            {
                clone.Nodes.Add(filteredChild);
            }
        }

        if (isMatch || clone.Nodes.Count > 0)
        {
            if (isMatch)
            {
                matchCount++;
                clone.BackColor = Color.LightYellow;
                clone.ForeColor = Color.Black;
                clone.NodeFont = new Font(kietvException.Font, FontStyle.Bold);
            }
            else
            {
                clone.BackColor = Color.White;
                clone.ForeColor = kietvException.ForeColor;
                clone.NodeFont = kietvException.Font;
            }

            return clone;
        }

        return null;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (ShowSearchFeatures)
        {
            SearchBox.Visible = true;
            kwlblSearchResults.Visible = true;
        }
        else
        {
            SearchBox.Visible = false;
            kwlblSearchResults.Visible = false;
        }
    }

    private void ktxtSearchBox_TextChanged(object sender, EventArgs e)
    {
        Search(SearchBox.Text);

        bsaClearSearch.Visible = !string.IsNullOrEmpty(SearchBox.Text);
    }

    private void bsaClearSearch_Click(object sender, EventArgs e) => SearchBox.Clear();

    #endregion
}