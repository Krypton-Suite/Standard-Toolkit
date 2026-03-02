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

    private string _text;
    private int _minimumWidth;
    private bool _enabled;
    private int _selectedIndex;
    private readonly ObjectCollection _items;

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
        _text = initialText;
        _minimumWidth = 120;
        _enabled = true;
        _selectedIndex = -1;
        _items = new ObjectCollection(this);
    }

    /// <summary>
    /// Returns a description of the instance.
    /// </summary>
    /// <returns>String representation.</returns>
    public override string ToString() => Text;

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
        get => _text;

        set
        {
            if (_text != value)
            {
                _text = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Text)));
                TextChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets the collection of items in the combo box.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The items in the combo box.")]
    [Editor(@"System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    [MergableProperty(false)]
    public ObjectCollection Items => _items;

    /// <summary>
    /// Gets and sets the index specifying the currently selected item.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex
    {
        get => _selectedIndex;

        set
        {
            if (_selectedIndex != value)
            {
                _selectedIndex = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedIndex)));
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets the currently selected item.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedItem => (_selectedIndex >= 0 && _selectedIndex < _items.Count) ? _items[_selectedIndex] : null;

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
        get => _enabled;

        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Enabled)));
            }
        }
    }

    #endregion

    #region Internal
    
    internal void OnSelectedIndexChangedInternal() => SelectedIndexChanged?.Invoke(this, EventArgs.Empty);

    #endregion

    /// <summary>
    /// Simple string collection for combo box items.
    /// </summary>
    public class ObjectCollection : List<object>
    {
        private readonly KryptonContextMenuComboBox _owner;

        internal ObjectCollection(KryptonContextMenuComboBox owner)
        {
            _owner = owner;
        }

        /// <summary>
        /// Adds an item and notifies the owner of the change.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public new void Add(object item)
        {
            base.Add(item);
            _owner.OnPropertyChanged(new PropertyChangedEventArgs(nameof(Items)));
        }

        /// <summary>
        /// Removes an item and notifies the owner of the change.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns>True if the item was found and removed.</returns>
        public new bool Remove(object item)
        {
            var result = base.Remove(item);
            if (result)
            {
                _owner.OnPropertyChanged(new PropertyChangedEventArgs(nameof(Items)));
            }
            return result;
        }

        /// <summary>
        /// Clears all items and notifies the owner of the change.
        /// </summary>
        public new void Clear()
        {
            base.Clear();
            _owner.OnPropertyChanged(new PropertyChangedEventArgs(nameof(Items)));
        }
    }
}
