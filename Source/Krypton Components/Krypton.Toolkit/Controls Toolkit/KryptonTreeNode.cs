#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Krypton extension of the TreeNode allowing extra information to be drawn.
/// </summary>
[ToolboxItem(false)]
public class KryptonTreeNode : TreeNode
{
    #region Instance Fields
    private string _longText;
    private Color _longForeColor;
    private Font? _longNodeFont;
    private bool _isCheckBoxVisible;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a property has changed value.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonTreeNode class.
    /// </summary>
    public KryptonTreeNode()
    {
        Init();
    }

    /// <summary>
    /// Initializes a new instance of the System.Windows.Forms.TreeNode class with the specified label text.
    /// </summary>
    /// <param name="text">The label System.Windows.Forms.TreeNode.Text of the new tree node.</param>
    public KryptonTreeNode(string text)
        : base(text)
    {
        Init();
    }

    /// <summary>
    /// Initializes a new instance of the System.Windows.Forms.TreeNode class with the specified label text and child tree nodes.
    /// </summary>
    /// <param name="text">The label System.Windows.Forms.TreeNode.Text of the new tree node.</param>
    /// <param name="children">An array of child System.Windows.Forms.TreeNode objects.</param>
    public KryptonTreeNode(string text, TreeNode[] children)
        : base(text, children)
    {
        Init();
    }

    /// <summary>
    /// Initializes a new instance of the System.Windows.Forms.TreeNode class with the specified label text and images to display when the tree node is in a selected and unselected state.
    /// </summary>
    /// <param name="text">The label System.Windows.Forms.TreeNode.Text of the new tree node.</param>
    /// <param name="imageIndex">The index value of System.Drawing.Image to display when the tree node is unselected.</param>
    /// <param name="selectedImageIndex">The index value of System.Drawing.Image to display when the tree node is selected.</param>
    public KryptonTreeNode(string text, int imageIndex, int selectedImageIndex)
        : base(text, imageIndex, selectedImageIndex)
    {
        Init();
    }

    /// <summary>
    /// Initializes a new instance of the System.Windows.Forms.TreeNode class with the specified label text, child tree nodes, and images to display when the tree node is in a selected and unselected state.
    /// </summary>
    /// <param name="text">The label System.Windows.Forms.TreeNode.Text of the new tree node.</param>
    /// <param name="imageIndex">The index value of System.Drawing.Image to display when the tree node is unselected.</param>
    /// <param name="selectedImageIndex">The index value of System.Drawing.Image to display when the tree node is selected.</param>
    /// <param name="children">An array of child System.Windows.Forms.TreeNode objects.</param>
    public KryptonTreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children)
        : base(text, imageIndex, selectedImageIndex, children)
    {
        Init();
    }

    private void Init()
    {
        _longText = string.Empty;
        _longForeColor = GlobalStaticValues.EMPTY_COLOR;
        _longNodeFont = null;
        _isCheckBoxVisible = true;
    }
    #endregion

    #region LongText
    /// <summary>
    /// Gets and sets the long text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Supplementary text.")]
    [Localizable(true)]
    public string LongText
    {
        get => _longText;

        set
        {
            if (_longText != value)
            {
                _longText = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(LongText)));
            }
        }
    }

    private bool ShouldSerializeLongText() => !string.IsNullOrEmpty(_longText);

    #endregion    

    #region LongForeColor
    /// <summary>
    /// Gets or sets the foreground color of the long text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Foreground color of the long text")]
    public Color LongForeColor
    {
        get => _longForeColor;

        set
        {
            if (_longForeColor != value)
            {
                _longForeColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(LongForeColor)));
            }
        }
    }

    private bool ShouldSerializeLongForeColor() => _longForeColor != GlobalStaticValues.EMPTY_COLOR;

    #endregion    

    #region LongNodeFont
    /// <summary>
    /// Gets or sets the font of the long text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Font of the long text")]
    public Font? LongNodeFont
    {
        get => _longNodeFont;

        set
        {
            if (_longNodeFont != value)
            {
                _longNodeFont = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(LongNodeFont)));
            }
        }
    }

    private bool ShouldSerializeLongNodeFont() => _longNodeFont != null;

    #endregion

    #region LongText
    /// <summary>
    /// Gets and sets the long text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Is the CheckBox Visible on this node when the TreeView has Checkboxes")]
    [DefaultValue(true)]
    public bool IsCheckBoxVisible
    {
        get => _isCheckBoxVisible;

        set
        {
            if (_isCheckBoxVisible != value)
            {
                _isCheckBoxVisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsCheckBoxVisible)));
                Rectangle callOnce = Bounds;
                if (callOnce != Rectangle.Empty)
                {
                    // Have to do this as RowBounds is not accessible ! and the check box is on the left, normally !
                    Rectangle nodeWidth = Rectangle.FromLTRB(0, callOnce.Top, callOnce.Right + callOnce.Left,
                        callOnce.Bottom);
                    TreeView!.Invalidate(nodeWidth);
                    TreeView.Update();
                }
            }
        }
    }

    private bool ShouldSerializeIsCheckBoxVisible() => !_isCheckBoxVisible;

    #endregion    

    #region Protected
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="e">A PropertyChangedEventArgs containing the event data.</param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

    #endregion 
}