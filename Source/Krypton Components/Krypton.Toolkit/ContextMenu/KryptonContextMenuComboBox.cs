#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide a context menu combo box item.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonContextMenuComboBox), "ToolboxBitmaps.KryptonComboBox.bmp")]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty(nameof(Text))]
[DefaultEvent(nameof(SelectedIndexChanged))]
public class KryptonContextMenuComboBox : KryptonContextMenuItemBase
{
    #region Instance Fields
    private int _minimumWidth;
    private readonly KryptonComboBox _comboBox;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the SelectedIndex property changes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the SelectedIndex property changes.")]
    public event EventHandler? SelectedIndexChanged;

    /// <summary>
    /// Occurs when the value of the Text property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the Text property changes.")]
    public event EventHandler? TextChanged;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuComboBox class.
    /// </summary>
    public KryptonContextMenuComboBox()
        : this(string.Empty)
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuComboBox class.
    /// </summary>
    /// <param name="initialText">Initial text value.</param>
    public KryptonContextMenuComboBox(string initialText)
    {
        _minimumWidth = 120;

        _comboBox = new KryptonComboBox { Text = initialText };
        _comboBox.SelectedIndexChanged += OnComboBoxSelectedIndexChanged;
        _comboBox.TextChanged += OnComboBoxTextChanged;
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
            _comboBox.SelectedIndexChanged -= OnComboBoxSelectedIndexChanged;
            _comboBox.TextChanged -= OnComboBoxTextChanged;
            _comboBox.Dispose();
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
        return new ViewDrawMenuComboBox(provider, this);
    }

    /// <summary>
    /// Gets and sets the text displayed in the combo box edit area.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Text displayed in the combo box edit area.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string Text
    {
        get => _comboBox.Text;

        set
        {
            if (_comboBox.Text != value)
            {
                _comboBox.Text = value;
            }
        }
    }

    /// <summary>
    /// Gets the collection of items in the combo box.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The items in the combo box.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    [MergableProperty(false)]
    public ComboBox.ObjectCollection Items => _comboBox.Items;

    /// <summary>
    /// Gets and sets the index specifying the currently selected item.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex
    {
        get => _comboBox.SelectedIndex;

        set => _comboBox.SelectedIndex = value;
    }

    /// <summary>
    /// Gets the currently selected item.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedItem => _comboBox.SelectedItem;

    /// <summary>
    /// Gets and sets the minimum display width of the combo box.
    /// </summary>
    [KryptonPersist]
    [Category(@"Layout")]
    [Description(@"Minimum display width of the combo box in pixels.")]
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
    /// Gets and sets a value indicating whether the combo box is enabled.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates whether the combo box is enabled.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _comboBox.Enabled;

        set
        {
            if (_comboBox.Enabled != value)
            {
                _comboBox.Enabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Enabled)));
            }
        }
    }

    #endregion

    #region Internal
    /// <summary>
    /// Gets the underlying KryptonComboBox; used by the view to host the control directly.
    /// </summary>
    internal KryptonComboBox ComboBox => _comboBox;

    #endregion

    #region Private
    private void OnComboBoxSelectedIndexChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedIndex)));
        SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnComboBoxTextChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Text)));
        TextChanged?.Invoke(this, EventArgs.Empty);
    }
    #endregion
}

