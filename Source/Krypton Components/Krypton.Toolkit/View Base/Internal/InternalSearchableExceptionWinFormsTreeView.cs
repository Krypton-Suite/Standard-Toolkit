#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class InternalSearchableExceptionWinFormsTreeView : UserControl
{
    #region Instance Fields

    private bool _showSearchFeatures;

    private Color? _highlightColor;

    private readonly List<TreeNode> _originalNodes = new List<TreeNode>();

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets a value indicating whether search-related features are displayed in the control.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the color used to highlight selected elements in the control.
    /// </summary>
    /// <remarks>If no color is explicitly set, the default highlight color is <see cref="Color.LightYellow"/>. This property is not persisted by designers due to its serialization visibility setting.</remarks>
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color HighlightColor
    {
        get => _highlightColor ?? Color.LightYellow;
        set => _highlightColor = value;
    }

    /// <summary>
    /// Gets the exception currently selected in the exception outline view, if any.
    /// </summary>
    public Exception? SelectedException => etvExceptionOutline.SelectedException;

    /// <summary>
    /// Gets the underlying tree view control used to display the exception outline.
    /// </summary>
    /// <remarks>Use this property to access or customize the tree view that presents exception details. Changes made to the returned control will affect how exceptions are displayed in the outline.</remarks>
    public TreeView Tree => etvExceptionOutline;

    /// <summary>
    /// Gets the text box control used for search input within the component.
    /// </summary>
    /// <remarks>Use this property to access or modify the search box's properties, such as its text, appearance, or event handlers. The returned control can be used to retrieve user-entered search queries or to customize search-related behavior.</remarks>
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

    /// <summary>
    /// Populates the exception outline view with the details of the specified exception and updates the internal node collection to reflect the current state.
    /// </summary>
    /// <remarks>Calling this method replaces the current contents of the outline view and resets the internal node collection. The method should be called whenever a new exception needs to be displayed.</remarks>
    /// <param name="exception">The exception whose details are used to populate the outline view. Cannot be null.</param>
    public void Populate(Exception exception)
    {
        etvExceptionOutline.Populate(exception);
        _originalNodes.Clear();

        foreach (TreeNode node in etvExceptionOutline.Nodes)
        {
            _originalNodes.Add((TreeNode)node.Clone());
        }
    }

    /// <summary>
    /// Filters and updates the exception outline tree view based on the specified search query text.
    /// </summary>
    /// <remarks>If the search query is empty or contains only whitespace, all original nodes are restored and
    /// the results label is reset. When a search is performed, only matching nodes are displayed, and the results label
    /// is updated to reflect the number of matches found. The first matching node is automatically selected if
    /// available.</remarks>
    /// <param name="searchQueryText">The text to search for within the exception outline. Leading and trailing whitespace is ignored, and the search
    /// is case-insensitive. If empty or whitespace, the tree view is reset to its original state.</param>
    private void Search(string searchQueryText)
    {
        // Normalize search text
        string searchText = searchQueryText.Trim().ToLowerInvariant();

        // Begin update
        etvExceptionOutline.BeginUpdate();

        // Clear current nodes
        etvExceptionOutline.Nodes.Clear();

        // Reset match count
        int matchCount = 0;

        // If no search text, restore original nodes
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

        // Show clear button
        bsaClearSearch.Visible = !string.IsNullOrEmpty(searchText);

        // If searching
        foreach (TreeNode original in _originalNodes)
        {
            // Filter and clone nodes
            TreeNode? filtered = FilterAndCloneNode(original, searchText, ref matchCount);

            // Add filtered node if not null
            if (filtered != null)
            {
                // Add to tree
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

        // Reset label color
        kwlblResults.StateCommon.TextColor = SystemColors.ControlText;

        // Auto-select match or fallback
        var firstMatch = etvExceptionOutline.Nodes
            .Cast<TreeNode>()
            .SelectMany(FlattenTree)
            .FirstOrDefault(n => n.BackColor == _highlightColor);

        // Select the first matching node or the first node if no matches
        etvExceptionOutline.SelectedNode = firstMatch ??
                                           (etvExceptionOutline.Nodes.Count > 0 ? etvExceptionOutline.Nodes[0] : null);

        // Adjust label color if no matches
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
                clone.BackColor = (Color)_highlightColor!;
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