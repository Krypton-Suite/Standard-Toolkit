#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Represents a ribbon group combo box.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupComboBox), "ToolboxBitmaps.KryptonRibbonGroupComboBox.bmp")]
[Designer(typeof(KryptonRibbonGroupComboBoxDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultEvent("SelectedTextChanged")]
[DefaultProperty(nameof(Text))]
public class KryptonRibbonGroupComboBox : KryptonRibbonGroupItem
{
    #region Instance Fields
    private bool _visible;
    private bool _enabled;
    private string _keyTip;
    private GroupItemSize _itemSizeCurrent;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the control receives focus.
    /// </summary>
    [Browsable(false)]
    public event EventHandler? GotFocus;

    /// <summary>
    /// Occurs when the control loses focus.
    /// </summary>
    [Browsable(false)]
    public event EventHandler? LostFocus;

    /// <summary>
    /// Occurs when a key is pressed while the control has focus. 
    /// </summary>
    [Description(@"Occurs when a key is pressed while the control has focus.")]
    [Category(@"Key")]
    public event KeyPressEventHandler? KeyPress;

    /// <summary>
    /// Occurs when a key is released while the control has focus. 
    /// </summary>
    [Description(@"Occurs when a key is released while the control has focus.")]
    [Category(@"Key")]
    public event KeyEventHandler? KeyUp;

    /// <summary>
    /// Occurs when a key is pressed while the control has focus.
    /// </summary>
    [Description(@"Occurs when a key is pressed while the control has focus.")]
    [Category(@"Key")]
    public event KeyEventHandler? KeyDown;

    /// <summary>
    /// Occurs before the KeyDown event when a key is pressed while focus is on this control.
    /// </summary>
    [Description(@"Occurs before the KeyDown event when a key is pressed while focus is on this control.")]
    [Category(@"Key")]
    public event PreviewKeyDownEventHandler? PreviewKeyDown;

    /// <summary>
    /// Occurs when the drop-down portion of the KryptonComboBox is shown.
    /// </summary>
    [Description(@"Occurs when the drop-down portion of the KryptonComboBox is shown.")]
    [Category(@"Behavior")]
    public event EventHandler? DropDown;

    /// <summary>
    /// Indicates that the drop-down portion of the KryptonComboBox has closed.
    /// </summary>
    [Description(@"Indicates that the drop-down portion of the KryptonComboBox has closed.")]
    [Category(@"Behavior")]
    public event EventHandler? DropDownClosed;

    /// <summary>
    /// Occurs when the value of the DropDownStyle property changed.
    /// </summary>
    [Description(@"Occurs when the value of the DropDownStyle property changed.")]
    [Category(@"Behavior")]
    public event EventHandler? DropDownStyleChanged;

    /// <summary>
    /// Occurs when the value of the SelectedIndex property changes.
    /// </summary>
    [Description(@"Occurs when the value of the SelectedIndex property changes.")]
    [Category(@"Behavior")]
    public event EventHandler? SelectedIndexChanged;

    /// <summary>
    /// Occurs when an item is chosen from the drop-down list and the drop-down list is closed.
    /// </summary>
    [Description(@"Occurs when an item is chosen from the drop-down list and the drop-down list is closed.")]
    [Category(@"Behavior")]
    public event EventHandler? SelectionChangeCommitted;

    /// <summary>
    /// Occurs when the value of the DataSource property changed.
    /// </summary>
    [Description(@"Occurs when the value of the DataSource property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? DataSourceChanged;

    /// <summary>
    /// Occurs when the value of the DisplayMember property changed.
    /// </summary>
    [Description(@"Occurs when the value of the DisplayMember property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? DisplayMemberChanged;

    /// <summary>
    /// Occurs when the list format has changed.
    /// </summary>
    [Description(@"Occurs when the list format has changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? Format;

    /// <summary>
    /// Occurs when the value of the FormatInfo property changed.
    /// </summary>
    [Description(@"Occurs when the value of the FormatInfo property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? FormatInfoChanged;

    /// <summary>
    /// Occurs when the value of the FormatString property changed.
    /// </summary>
    [Description(@"Occurs when the value of the FormatString property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? FormatStringChanged;

    /// <summary>
    /// Occurs when the value of the FormattingEnabled property changed.
    /// </summary>
    [Description(@"Occurs when the value of the FormattingEnabled property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? FormattingEnabledChanged;

    /// <summary>
    /// Occurs when the value of the SelectedValue property changed.
    /// </summary>
    [Description(@"Occurs when the value of the SelectedValue property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? SelectedValueChanged;

    /// <summary>
    /// Occurs when the value of the ValueMember property changed.
    /// </summary>
    [Description(@"Occurs when the value of the ValueMember property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? ValueMemberChanged;

    /// <summary>
    /// Occurs when the KryptonComboBox text has changed.
    /// </summary>
    [Description(@"Occurs when the KryptonComboBox text has changed.")]
    [Category(@"Behavior")]
    public event EventHandler? TextUpdate;

    /// <summary>
    /// Occurs after the value of a property has changed.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs after the value of a property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when the design time context menu is requested.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event MouseEventHandler? DesignTimeContextMenu;

    internal event EventHandler? MouseEnterControl;
    internal event EventHandler? MouseLeaveControl;
    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonRibbonGroupComboBox class.
    /// </summary>
    public KryptonRibbonGroupComboBox()
    {
        // Default fields
        _visible = true;
        _enabled = true;
        _itemSizeCurrent = GroupItemSize.Medium;
        ShortcutKeys = Keys.None;
        _keyTip = "X";

        // Create the actual combo box control and set initial settings
        ComboBox = new KryptonComboBox
        {
            InputControlStyle = InputControlStyle.Ribbon,
            AlwaysActive = false,
            MinimumSize = new Size(121, 0),
            MaximumSize = new Size(121, 0),
            TabStop = false
        };

        // Hook into the events that are then exposed via ourself
        ComboBox.DropDown += OnComboBoxDropDown;
        ComboBox.DropDownClosed += OnComboBoxDropDownClosed;
        ComboBox.DropDownStyleChanged += OnComboBoxDropDownStyleChanged;
        ComboBox.SelectedIndexChanged += OnComboBoxSelectedIndexChanged;
        ComboBox.SelectionChangeCommitted += OnComboBoxSelectionChangeCommitted;
        ComboBox.TextUpdate += OnComboBoxTextUpdate;
        ComboBox.GotFocus += OnComboBoxGotFocus;
        ComboBox.LostFocus += OnComboBoxLostFocus;
        ComboBox.KeyDown += OnComboBoxKeyDown;
        ComboBox.KeyUp += OnComboBoxKeyUp;
        ComboBox.KeyPress += OnComboBoxKeyPress;
        ComboBox.PreviewKeyDown += OnComboBoxPreviewKeyDown;
        ComboBox.DataSourceChanged += OnComboBoxDataSourceChanged;
        ComboBox.DisplayMemberChanged += OnComboBoxDisplayMemberChanged;
        ComboBox.Format += OnComboBoxFormat;
        ComboBox.FormatInfoChanged += OnComboBoxFormatInfoChanged;
        ComboBox.FormatStringChanged += OnComboBoxFormatStringChanged;
        ComboBox.FormattingEnabledChanged += OnComboBoxFormattingEnabledChanged;
        ComboBox.SelectedValueChanged += OnComboBoxSelectedValueChanged;
        ComboBox.ValueMemberChanged += OnComboBoxValueMemberChanged;

        // Ensure we can track mouse events on the text box
        MonitorControl(ComboBox);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (ComboBox != null!)
            {
                UnmonitorControl(ComboBox);
                ComboBox.Dispose();
                ComboBox = null!;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the owning ribbon control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override KryptonRibbon? Ribbon
    {
        set
        {
            base.Ribbon = value;

            if (value != null)
            {
                // Use the same palette in the combo box as the ribbon, plus we need
                // to know when the ribbon palette changes so we can reflect that change
                ComboBox.PaletteMode = Ribbon!.PaletteMode;
                ComboBox.LocalCustomPalette = Ribbon!.LocalCustomPalette;
                Ribbon!.PaletteChanged += OnRibbonPaletteChanged;
            }
        }
    }
    /// <summary>
    /// Gets and sets the shortcut key combination.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Shortcut key combination to set focus to the combo box.")]
    public Keys ShortcutKeys { get; set; }

    private bool ShouldSerializeShortcutKeys() => ShortcutKeys != Keys.None;

    /// <summary>
    /// Resets the ShortcutKeys property to its default value.
    /// </summary>
    public void ResetShortcutKeys() => ShortcutKeys = Keys.None;

    /// <summary>
    /// Access to the actual embedded KryptonComboBox instance.
    /// </summary>
    [Description(@"Access to the actual embedded KryptonComboBox instance.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonComboBox ComboBox { get; private set; }

    /// <summary>
    /// Gets and sets the key tip for the ribbon group text box.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Ribbon group text box key tip.")]
    [DefaultValue(@"X")]
    public string KeyTip
    {
        get => _keyTip;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                value = @"X";
            }

            _keyTip = value.ToUpper();
        }
    }

    /// <summary>
    /// Gets and sets the visible state of the rich text.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the text box is visible or hidden.")]
    [DefaultValue(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override bool Visible
    {
        get => _visible;

        set
        {
            if (value != _visible)
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }
    }

    /// <summary>
    /// Make the ribbon group visible.
    /// </summary>
    public void Show() => Visible = true;

    /// <summary>
    /// Make the ribbon group hidden.
    /// </summary>
    public void Hide() => Visible = false;

    /// <summary>
    /// Gets and sets the enabled state of the group combo box.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the group combo box is enabled.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _enabled;

        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                OnPropertyChanged(nameof(Enabled));
            }
        }
    }

    /// <summary>
    /// Gets or sets the minimum size of the control.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies the minimum size of the control.")]
    [DefaultValue(typeof(Size), "121, 0")]
    public Size MinimumSize
    {
        get => ComboBox.MinimumSize;
        set => ComboBox.MinimumSize = value;
    }

    /// <summary>
    /// Gets or sets the maximum size of the control.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies the maximum size of the control.")]
    [DefaultValue(typeof(Size), "121, 0")]
    public Size MaximumSize
    {
        get => ComboBox.MaximumSize;
        set => ComboBox.MaximumSize = value;
    }

    /// <summary>
    /// Gets and sets the text associated with the control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text associated with the control.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public virtual string Text
    {
        get => ComboBox.Text;
        set => ComboBox.Text = value;
    }

    /// <summary>
    /// Gets and sets the associated context menu strip.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The shortcut to display when the user right-clicks the control.")]
    [DefaultValue(null)]
    public ContextMenuStrip? ContextMenuStrip
    {
        get => ComboBox.ContextMenuStrip;
        set => ComboBox.ContextMenuStrip = value;
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu for showing when the combobox is right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"KryptonContextMenu to be shown when the combobox is right clicked.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => ComboBox.KryptonContextMenu;
        set => ComboBox.KryptonContextMenu = value;
    }

    /// <summary>
    /// Gets and sets the value member.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the property to use as the actual value of the items in the control.")]
    [Editor(@"System.Windows.Forms.Design.DataMemberFieldEditor", typeof(UITypeEditor))]
    [DefaultValue("")]
    public string ValueMember
    {
        get => ComboBox.ValueMember;
        set => ComboBox.ValueMember = value;
    }

    /// <summary>
    /// Gets and sets the list that this control will use to gets its items.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the list that this control will use to gets its items.")]
    [AttributeProvider(typeof(IListSource))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(null)]
    public object? DataSource
    {
        get => ComboBox.DataSource;
        set => ComboBox.DataSource = value;
    }

    /// <summary>
    /// Gets and sets the property to display for the items in this control.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the property to display for the items in this control.")]
    [TypeConverter(@"System.Windows.Forms.Design.DataMemberFieldConverter")]
    [Editor(@"System.Windows.Forms.Design.DataMemberFieldEditor", typeof(UITypeEditor))]
    [DefaultValue("")]
    public string DisplayMember
    {
        get => ComboBox.DisplayMember;
        set => ComboBox.DisplayMember = value;
    }

    /// <summary>
    /// Gets and sets the appearance and functionality of the KryptonComboBox.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Controls the appearance and functionality of the KryptonComboBox.")]
    [DefaultValue(typeof(ComboBoxStyle), nameof(DropDown))]
    [RefreshProperties(RefreshProperties.Repaint)]
    public virtual ComboBoxStyle DropDownStyle
    {
        get => ComboBox.DropDownStyle;
        set => ComboBox.DropDownStyle = value;
    }

    /// <summary>
    /// Gets and sets the height, in pixels, of the drop-down box in a KryptonComboBox.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The height, in pixels, of the drop-down box in a KryptonComboBox.")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(200)]
    [Browsable(true)]
    public int DropDownHeight
    {
        get => ComboBox.DropDownHeight;
        set => ComboBox.DropDownHeight = value;
    }

    /// <summary>
    /// Gets and sets the width, in pixels, of the drop-down box in a KryptonComboBox.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The width, in pixels, of the drop-down box in a KryptonComboBox.")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(143)]
    [Browsable(true)]
    public int DropDownWidth
    {
        get => ComboBox.DropDownWidth;
        set => ComboBox.DropDownWidth = value;
    }

    /// <summary>
    /// Gets and sets the height, in pixels, of items in an owner-draw KryptomComboBox.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The height, in pixels, of items in an owner-draw KryptomComboBox.")]
    [Localizable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int ItemHeight
    {
        get => ComboBox.ItemHeight;
        set => ComboBox.ItemHeight = value;
    }

    /// <summary>
    /// Gets and sets the maximum number of entries to display in the drop-down list.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The maximum number of entries to display in the drop-down list.")]
    [Localizable(true)]
    [DefaultValue(8)]
    public int MaxDropDownItems
    {
        get => ComboBox.MaxDropDownItems;
        set => ComboBox.MaxDropDownItems = value;
    }

    /// <summary>
    /// Gets or sets the maximum number of characters that can be entered into the edit control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies the maximum number of characters that can be entered into the edit control.")]
    [DefaultValue(0)]
    [Localizable(true)]
    public int MaxLength
    {
        get => ComboBox.MaxLength;
        set => ComboBox.MaxLength = value;
    }

    /// <summary>
    /// Gets or sets whether the items in the list portion of the KryptonComboBox are sorted.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies whether the items in the list portion of the KryptonComboBox are sorted.")]
    [DefaultValue(false)]
    public bool Sorted
    {
        get => ComboBox.Sorted;
        set => ComboBox.Sorted = value;
    }

    /// <summary>
    /// Gets access to the Wrapped Controls Tooltips.
    /// </summary>
    public override ToolTipValues ToolTipValues => ComboBox.ToolTipValues;

    /// <summary>
    /// Gets or sets the items in the KryptonComboBox.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The items in the KryptonComboBox.")]
    [Editor(@"System.Windows.Forms.Design.ListControlStringCollectionEditor", typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [MergableProperty(false)]
    [Localizable(true)]
    public virtual ComboBox.ObjectCollection Items => ComboBox.Items;

    /// <summary>
    /// Gets and sets a value indicating if tooltips should be Displayed for button specs.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should tooltips be Displayed for button specs.")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTips
    {
        get => ComboBox.AllowButtonSpecToolTips;
        set => ComboBox.AllowButtonSpecToolTips = value;
    }

    /// <summary>
    /// Gets and sets a value indicating if button spec tooltips should remove the parent tooltip.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should button spec tooltips should remove the parent tooltip")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTipPriority
    {
        get => ComboBox.AllowButtonSpecToolTipPriority;
        set => ComboBox.AllowButtonSpecToolTipPriority = value;
    }

    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonComboBox.ComboBoxButtonSpecCollection ButtonSpecs => ComboBox.ButtonSpecs;

    /// <summary>
    /// Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.
    /// </summary>
    [Description(@"The StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
    [Editor(@"System.Windows.Forms.Design.ListControlStringCollectionEditor", typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Localizable(true)]
    [Browsable(true)]
    public virtual AutoCompleteStringCollection AutoCompleteCustomSource
    {
        get => ComboBox.AutoCompleteCustomSource;
        set => ComboBox.AutoCompleteCustomSource = value;
    }

    /// <summary>
    /// Gets or sets the text completion behavior of the combobox.
    /// </summary>
    [Description(@"Indicates the text completion behavior of the combobox.")]
    [DefaultValue(typeof(AutoCompleteMode), "None")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public virtual AutoCompleteMode AutoCompleteMode
    {
        get => ComboBox.AutoCompleteMode;
        set => ComboBox.AutoCompleteMode = value;
    }

    /// <summary>
    /// Gets or sets the autocomplete source, which can be one of the values from AutoCompleteSource enumeration.
    /// </summary>
    [Description(@"The autocomplete source, which can be one of the values from AutoCompleteSource enumeration.")]
    [DefaultValue(typeof(AutoCompleteSource), "None")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public virtual AutoCompleteSource AutoCompleteSource
    {
        get => ComboBox.AutoCompleteSource;
        set => ComboBox.AutoCompleteSource = value;
    }

    /// <summary>
    /// Gets or sets the format specifier characters that indicate how a value is to be Displayed.
    /// </summary>
    [Description(@"The format specifier characters that indicate how a value is to be Displayed.")]
    [Editor(@"System.Windows.Forms.Design.FormatStringEditor", typeof(UITypeEditor))]
    [MergableProperty(false)]
    [DefaultValue("")]
    public virtual string FormatString
    {
        get => ComboBox.FormatString;
        set => ComboBox.FormatString = value;
    }

    /// <summary>
    /// Gets or sets if this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be Displayed.
    /// </summary>
    [Description(@"If this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be Displayed.")]
    [DefaultValue(true)]
    public bool FormattingEnabled
    {
        get => ComboBox.FormattingEnabled;
        set => ComboBox.FormattingEnabled = value;
    }

    /// <summary>
    /// Gets and sets the formatting provider.
    /// </summary>
    [DefaultValue(null)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public IFormatProvider? FormatInfo
    {
        get => ComboBox.FormatInfo;
        set => ComboBox.FormatInfo = value;
    }

    /// <summary>
    /// Gets and sets the number of characters selected in the editable portion of the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionLength
    {
        get => ComboBox.SelectionLength;
        set => ComboBox.SelectionLength = value;
    }

    /// <summary>
    /// Gets and sets the starting index of selected text in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionStart
    {
        get => ComboBox.SelectionStart;
        set => ComboBox.SelectionStart = value;
    }

    /// <summary>
    /// Gets and sets the selected item.
    /// </summary>
    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedItem
    {
        get => ComboBox.SelectedItem;
        set => ComboBox.SelectedItem = value;
    }

    /// <summary>
    /// Gets and sets the text that is selected in the editable portion of the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SelectedText
    {
        get => ComboBox.SelectedText;
        set => ComboBox.SelectedText = value;
    }

    /// <summary>
    /// Gets and sets the selected index.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex
    {
        get => ComboBox.SelectedIndex;
        set => ComboBox.SelectedIndex = value;
    }

    /// <summary>
    /// Gets and sets the selected value.
    /// </summary>
    [Bindable(true)]
    [Browsable(false)]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedValue
    {
        get => ComboBox.SelectedValue;
        set => ComboBox.SelectedValue = value;
    }

    /// <summary>
    /// Gets and sets a value indicating whether the control is displaying its drop-down portion.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool DroppedDown
    {
        get => ComboBox.DroppedDown;
        set => ComboBox.DroppedDown = value;
    }

    /// <summary>
    /// Finds the first item in the combo box that starts with the specified string.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
    public int FindString(string str) => ComboBox.FindString(str);

    /// <summary>
    /// Finds the first item after the given index which starts with the given string. The search is not case sensitive.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
    public int FindString(string str, int startIndex) => ComboBox.FindString(str, startIndex);

    /// <summary>
    /// Finds the first item in the combo box that matches the specified string.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
    public int FindStringExact(string str) => ComboBox.FindStringExact(str);

    /// <summary>
    /// Finds the first item after the specified index that matches the specified string.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
    public int FindStringExact(string str, int startIndex) => ComboBox.FindStringExact(str, startIndex);

    /// <summary>
    /// Returns the height of an item in the ComboBox.
    /// </summary>
    /// <param name="index">The index of the item to return the height of.</param>
    /// <returns>The height, in pixels, of the item at the specified index.</returns>
    public int GetItemHeight(int index) => ComboBox.GetItemHeight(index);

    /// <summary>
    /// Returns the text representation of the specified item.
    /// </summary>
    /// <param name="item">The object from which to get the contents to display.</param>
    /// <returns>If the DisplayMember property is not specified, the value returned by GetItemText is the value of the item's ToString method. Otherwise, the method returns the string value of the member specified in the DisplayMember property for the object specified in the item parameter.</returns>
    public string? GetItemText(object item) => ComboBox.GetItemText(item);

    /// <summary>
    /// Selects a range of text in the control.
    /// </summary>
    /// <param name="start">The position of the first character in the current text selection within the text box.</param>
    /// <param name="length">The number of characters to select.</param>
    public void Select(int start, int length) => ComboBox.Select(start, length);

    /// <summary>
    /// Selects all text in the control.
    /// </summary>
    public void SelectAll() => ComboBox.SelectAll();

    /// <summary>
    /// Gets and sets the maximum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeMaximum
    {
        get => GroupItemSize.Large;
        set { }
    }

    /// <summary>
    /// Gets and sets the minimum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeMinimum
    {
        get => GroupItemSize.Small;
        set { }
    }

    /// <summary>
    /// Gets and sets the current item size.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeCurrent
    {
        get => _itemSizeCurrent;

        set
        {
            if (_itemSizeCurrent != value)
            {
                _itemSizeCurrent = value;
                OnPropertyChanged(nameof(ItemSizeCurrent));
            }
        }
    }

    /// <summary>
    /// Creates an appropriate view element for this item.
    /// </summary>
    /// <param name="ribbon">Reference to the owning ribbon control.</param>
    /// <param name="needPaint">Delegate for notifying changes in display.</param>
    /// <returns>ViewBase derived instance.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override ViewBase CreateView(KryptonRibbon ribbon,
        NeedPaintHandler needPaint) =>
        new ViewDrawRibbonGroupComboBox(ribbon, this, needPaint);

    /// <summary>
    /// Gets and sets the associated designer.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public IKryptonDesignObject ComboBoxDesigner { get; set; }

    private bool ShouldSerializeComboBoxDesigner() => false;

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase? ComboBoxView { get; set; }

    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the TextUpdate event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnTextUpdate(EventArgs e) => TextUpdate?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectionChangeCommitted event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelectionChangeCommitted(EventArgs e) => SelectionChangeCommitted?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectedIndexChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelectedIndexChanged(EventArgs e) => SelectedIndexChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the DropDownStyleChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnDropDownStyleChanged(EventArgs e) => DropDownStyleChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the DataSourceChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnDataSourceChanged(EventArgs e) => DataSourceChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the DisplayMemberChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnDisplayMemberChanged(EventArgs e) => DisplayMemberChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the Format event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFormat(EventArgs e) => Format?.Invoke(this, e);

    /// <summary>
    /// Raises the FormatInfoChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFormatInfoChanged(EventArgs e) => FormatInfoChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the FormatStringChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFormatStringChanged(EventArgs e) => FormatStringChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the FormattingEnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFormattingEnabledChanged(EventArgs e) => FormattingEnabledChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectedValueChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelectedValueChanged(EventArgs e) => SelectedValueChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ValueMemberChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnValueMemberChanged(EventArgs e) => ValueMemberChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the DropDownClosed event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnDropDownClosed(EventArgs e) => DropDownClosed?.Invoke(this, e);

    /// <summary>
    /// Raises the DropDown event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnDropDown(EventArgs e) => DropDown?.Invoke(this, e);


    /// <summary>
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnGotFocus(EventArgs e) => GotFocus?.Invoke(this, e);

    /// <summary>
    /// Raises the LostFocus event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnLostFocus(EventArgs e) => LostFocus?.Invoke(this, e);

    /// <summary>
    /// Raises the KeyDown event.
    /// </summary>
    /// <param name="e">An KeyEventArgs containing the event data.</param>
    protected virtual void OnKeyDown(KeyEventArgs e) => KeyDown?.Invoke(this, e);

    /// <summary>
    /// Raises the KeyUp event.
    /// </summary>
    /// <param name="e">An KeyEventArgs containing the event data.</param>
    protected virtual void OnKeyUp(KeyEventArgs e) => KeyUp?.Invoke(this, e);

    /// <summary>
    /// Raises the KeyPress event.
    /// </summary>
    /// <param name="e">An KeyPressEventArgs containing the event data.</param>
    protected virtual void OnKeyPress(KeyPressEventArgs e) => KeyPress?.Invoke(this, e);

    /// <summary>
    /// Raises the PreviewKeyDown event.
    /// </summary>
    /// <param name="e">An PreviewKeyDownEventArgs containing the event data.</param>
    protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e) => PreviewKeyDown?.Invoke(this, e);

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    #region Internal
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Control? LastParentControl { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal KryptonComboBox? LastComboBox { get; set; }

    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    internal NeedPaintHandler? ViewPaintDelegate { get; set; }

    internal void OnDesignTimeContextMenu(MouseEventArgs e) => DesignTimeContextMenu?.Invoke(this, e);

    internal override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        // Only interested in key processing if this control definition 
        // is enabled and itself and all parents are also visible
        if (Enabled && ChainVisible)
        {
            // Do we have a shortcut definition for ourself?
            if (ShortcutKeys != Keys.None)
            {
                // Does it match the incoming key combination?
                if (ShortcutKeys == keyData)
                {
                    // Can the combo box take the focus
                    if (LastComboBox is { CanFocus: true })
                    {
                        LastComboBox.ComboBox.Focus();
                    }

                    return true;
                }
            }
        }

        return false;
    }
    #endregion

    #region Implementation
    private void MonitorControl(KryptonComboBox c)
    {
        c.MouseEnter += OnControlEnter;
        c.MouseLeave += OnControlLeave;
        c.TrackMouseEnter += OnControlEnter;
        c.TrackMouseLeave += OnControlLeave;
    }

    private void UnmonitorControl(KryptonComboBox c)
    {
        c.MouseEnter -= OnControlEnter;
        c.MouseLeave -= OnControlLeave;
        c.TrackMouseEnter -= OnControlEnter;
        c.TrackMouseLeave -= OnControlLeave;
    }

    private void OnControlEnter(object? sender, EventArgs e) => MouseEnterControl?.Invoke(this, e);

    private void OnControlLeave(object? sender, EventArgs e) => MouseLeaveControl?.Invoke(this, e);

    private void OnPaletteNeedPaint(object sender, NeedLayoutEventArgs e) =>
        // Pass request onto the view provided paint delegate
        ViewPaintDelegate?.Invoke(this, e);

    private void OnComboBoxGotFocus(object? sender, EventArgs e) => OnGotFocus(e);

    private void OnComboBoxLostFocus(object? sender, EventArgs e) => OnLostFocus(e);

    private void OnComboBoxTextUpdate(object? sender, EventArgs e) => OnTextUpdate(e);

    private void OnComboBoxSelectionChangeCommitted(object? sender, EventArgs e) => OnSelectionChangeCommitted(e);

    private protected void OnComboBoxSelectedIndexChanged(object? sender, EventArgs e) => OnSelectedIndexChanged(e);

    private void OnComboBoxDropDownStyleChanged(object? sender, EventArgs e) => OnDropDownStyleChanged(e);

    private void OnComboBoxDataSourceChanged(object? sender, EventArgs e) => OnDataSourceChanged(e);

    private void OnComboBoxDisplayMemberChanged(object? sender, EventArgs e) => OnDisplayMemberChanged(e);

    private void OnComboBoxDropDownClosed(object? sender, EventArgs e) => OnDropDownClosed(e);

    private void OnComboBoxDropDown(object? sender, EventArgs e) => OnDropDown(e);

    private void OnComboBoxKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnComboBoxKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnComboBoxKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnComboBoxPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnComboBoxFormat(object? sender, ListControlConvertEventArgs e) => OnFormat(e);

    private void OnComboBoxFormatInfoChanged(object? sender, EventArgs e) => OnFormatInfoChanged(e);

    private void OnComboBoxFormatStringChanged(object? sender, EventArgs e) => OnFormatStringChanged(e);

    private void OnComboBoxFormattingEnabledChanged(object? sender, EventArgs e) => OnFormattingEnabledChanged(e);

    private void OnComboBoxSelectedValueChanged(object? sender, EventArgs e) => OnSelectedValueChanged(e);

    private void OnComboBoxValueMemberChanged(object? sender, EventArgs e) => OnValueMemberChanged(e);

    private void OnRibbonPaletteChanged(object? sender, EventArgs e)
    {
        ComboBox.PaletteMode = Ribbon!.PaletteMode;
        ComboBox.LocalCustomPalette = Ribbon!.LocalCustomPalette;
    }

    #endregion
}