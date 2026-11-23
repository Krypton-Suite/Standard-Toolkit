#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class InternalSearchableExceptionWinFormsTreeView : UserControl
{
    #region Instance Fields

    private bool _showSearchFeatures;

    private readonly List<TreeNode> _originalNodes = new List<TreeNode>();

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

    public Exception? SelectedException => etvExceptionOutline.SelectedException;

    public TreeView Tree => etvExceptionOutline;

    public KryptonTextBox SearchBox => ktxtSearchBox;

    #endregion

    #region Events

    public event TreeViewEventHandler? NodeSelected
    {
        add => etvExceptionOutline.AfterSelect += value;
        remove => etvExceptionOutline.AfterSelect -= value;
    }

    #endregion

    #region Identity

    public InternalSearchableExceptionWinFormsTreeView()
    {
        InitializeComponent();

        kwlblResults.Text = KryptonManager.Strings.ExceptionDialogStrings.TypeToSearch;
    }

    #endregion

    #region Implementation

    public void Populate(Exception exception)
    {
        etvExceptionOutline.Populate(exception);
        _originalNodes.Clear();

        foreach (TreeNode node in etvExceptionOutline.Nodes)
        {
            _originalNodes.Add((TreeNode)node.Clone());
        }
    }

    private void Search(string searchQueryText)
    {
        string searchText = searchQueryText.Trim().ToLowerInvariant();

        etvExceptionOutline.BeginUpdate();
        etvExceptionOutline.Nodes.Clear();

        int matchCount = 0;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            // Reset: restore all original nodes
            foreach (TreeNode original in _originalNodes)
            {
                etvExceptionOutline.Nodes.Add((TreeNode)original.Clone());
            }

            // Reset label and styling
            kwlblResults.Text = KryptonManager.Strings.ExceptionDialogStrings.TypeToSearch;
            kwlblResults.StateCommon.TextColor = Color.Gray;

            etvExceptionOutline.EndUpdate();

            // Select first node if available
            if (etvExceptionOutline.Nodes.Count > 0)
            {
                etvExceptionOutline.SelectedNode = etvExceptionOutline.Nodes[0];
            }

            return;
        }

        bsaClearSearch.Visible = !string.IsNullOrEmpty(searchText);

        // If searching
        foreach (TreeNode original in _originalNodes)
        {
            TreeNode? filtered = FilterAndCloneNode(original, searchText, ref matchCount);
            if (filtered != null)
            {
                etvExceptionOutline.Nodes.Add(filtered);
            }
        }

        etvExceptionOutline.EndUpdate();

        // Update label
        if (string.IsNullOrWhiteSpace(searchText))
        {
            kwlblResults.Text = KryptonManager.Strings.ExceptionDialogStrings.TypeToSearch;
        }
        else if (matchCount == 0)
        {
            kwlblResults.Text = KryptonManager.Strings.ExceptionDialogStrings.NoMatchesFound;
        }
        else
        {
            kwlblResults.Text = $@"{matchCount} {KryptonManager.Strings.ExceptionDialogStrings.Result}{(matchCount == 1 ? "" : $"{KryptonManager.Strings.ExceptionDialogStrings.ResultsAppendage}")} {KryptonManager.Strings.ExceptionDialogStrings.ResultsFoundAppendage}";
        }

        kwlblResults.StateCommon.TextColor = SystemColors.ControlText;

        // Auto-select match or fallback
        var firstMatch = etvExceptionOutline.Nodes
            .Cast<TreeNode>()
            .SelectMany(FlattenTree)
            .FirstOrDefault(n => n.BackColor == Color.LightYellow);

        etvExceptionOutline.SelectedNode = firstMatch ??
                                           (etvExceptionOutline.Nodes.Count > 0 ? etvExceptionOutline.Nodes[0] : null);

        kwlblResults.StateCommon.TextColor = string.IsNullOrWhiteSpace(searchQueryText) ? Color.Gray : Color.Empty;
    }

    private IEnumerable<TreeNode> FlattenTree(TreeNode root)
    {
        yield return root;

        foreach (TreeNode child in root.Nodes)
        {
            foreach (var descendant in FlattenTree(child))
            {
                yield return descendant;
            }
        }
    }

    private TreeNode? FilterAndCloneNode(TreeNode node, string searchQuery, ref int matchCount)
    {
        bool isMatch = node.Text.ToLowerInvariant().Contains(searchQuery);
        TreeNode clone = (TreeNode)node.Clone();
        clone.Nodes.Clear();

        foreach (TreeNode child in node.Nodes)
        {
            TreeNode? filteredChild = FilterAndCloneNode(child, searchQuery, ref matchCount);
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
                clone.NodeFont = new Font(etvExceptionOutline.Font, FontStyle.Bold);
            }
            else
            {
                clone.BackColor = Color.White;
                clone.ForeColor = etvExceptionOutline.ForeColor;
                clone.NodeFont = etvExceptionOutline.Font;
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
            kwlblResults.Visible = true;
        }
        else
        {
            SearchBox.Visible = false;
            kwlblResults.Visible = false;
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