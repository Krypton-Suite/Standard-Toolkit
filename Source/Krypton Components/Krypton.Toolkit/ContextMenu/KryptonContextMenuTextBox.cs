#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide a context menu text box item.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonContextMenuTextBox), "ToolboxBitmaps.KryptonTextBox.bmp")]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty(nameof(Text))]
[DefaultEvent(nameof(TextChanged))]
public class KryptonContextMenuTextBox : KryptonContextMenuItemBase
{
    #region Instance Fields
    private int _minimumWidth;
    private readonly KryptonTextBox _textBox;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the Text property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the Text property changes.")]
    public event EventHandler? TextChanged;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuTextBox class.
    /// </summary>
    public KryptonContextMenuTextBox()
        : this(string.Empty)
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuTextBox class.
    /// </summary>
    /// <param name="initialText">Initial text value.</param>
    public KryptonContextMenuTextBox(string initialText)
    {
        _minimumWidth = 120;

        _textBox = new KryptonTextBox { Text = initialText };
        _textBox.TextChanged += OnTextBoxTextChanged;
    }

    /// <summary>
    /// Returns a description of the instance.
    /// </summary>
    /// <returns>String representation.</returns>
    public override string ToString() => Text;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _textBox.TextChanged -= OnTextBoxTextChanged;
            _textBox.Dispose();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public
    /// <summary>
    /// Returns the number of child menu items.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int ItemChildCount => 0;

    /// <summary>
    /// Returns the indexed child menu item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override KryptonContextMenuItemBase? this[int index] => null;

    /// <summary>
    /// Test for the provided shortcut and perform relevant action if a match is found.
    /// </summary>
    /// <param name="keyData">Key data to check against shortcut definitions.</param>
    /// <returns>True if shortcut was handled, otherwise false.</returns>
    public override bool ProcessShortcut(Keys keyData) => false;

    /// <summary>
    /// Returns a view appropriate for this item based on the object it is inside.
    /// </summary>
    /// <param name="provider">Provider of context menu information.</param>
    /// <param name="parent">Owning object reference.</param>
    /// <param name="columns">Containing columns.</param>
    /// <param name="standardStyle">Draw items with standard or alternate style.</param>
    /// <param name="imageColumn">Draw an image background for the item images.</param>
    /// <returns>ViewBase that is the root of the view hierarchy being added.</returns>
    public override ViewBase GenerateView(IContextMenuProvider provider,
        object parent,
        ViewLayoutStack columns,
        bool standardStyle,
        bool imageColumn)
    {
        SetProvider(provider);
        return new ViewDrawMenuTextBox(provider, this);
    }

    /// <summary>
    /// Gets and sets the text box value.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Text box value.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string Text
    {
        get => _textBox.Text;

        set
        {
            if (_textBox.Text != value)
            {
                _textBox.Text = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the minimum display width of the text box.
    /// </summary>
    [KryptonPersist]
    [Category(@"Layout")]
    [Description(@"Minimum display width of the text box in pixels.")]
    [DefaultValue(120)]
    public int MinimumWidth
    {
        get => _minimumWidth;

        set
        {
            if (_minimumWidth != value)
            {
                _minimumWidth = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(MinimumWidth)));
            }
        }
    }

    /// <summary>
    /// Gets and sets a value indicating whether the text box is enabled.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates whether the text box is enabled.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _textBox.Enabled;

        set
        {
            if (_textBox.Enabled != value)
            {
                _textBox.Enabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Enabled)));
            }
        }
    }

    #endregion

    #region Internal
    /// <summary>
    /// Gets the underlying KryptonTextBox; used by the view to host the control directly.
    /// </summary>
    internal KryptonTextBox TextBox => _textBox;

    #endregion

    #region Private
    private void OnTextBoxTextChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Text)));
        TextChanged?.Invoke(this, EventArgs.Empty);
    }
    #endregion
}
