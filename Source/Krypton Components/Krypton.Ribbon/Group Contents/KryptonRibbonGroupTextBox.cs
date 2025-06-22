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
/// Represents a ribbon group text box.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupTextBox), "ToolboxBitmaps.KryptonRibbonGroupTextBox.bmp")]
[Designer(typeof(KryptonRibbonGroupTextBoxDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultEvent(nameof(TextChanged))]
[DefaultProperty(nameof(Text))]
public class KryptonRibbonGroupTextBox : KryptonRibbonGroupItem
{
    #region Instance Fields
    private bool _visible;
    private bool _enabled;
    private string _keyTip;
    private GroupItemSize _itemSizeCurrent;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the Text property changes.
    /// </summary>
    [Description(@"Occurs when the value of the Text property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? TextChanged;

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
    /// Occurs when the value of the AcceptsTab property changes.
    /// </summary>
    [Description(@"Occurs when the value of the AcceptsTab property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? AcceptsTabChanged;

    /// <summary>
    /// Occurs when the value of the HideSelection property changes.
    /// </summary>
    [Description(@"Occurs when the value of the HideSelection property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? HideSelectionChanged;

    /// <summary>
    /// Occurs when the value of the TextAlign property changes.
    /// </summary>
    [Description(@"Occurs when the value of the TextAlign property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? TextAlignChanged;

    /// <summary>
    /// Occurs when the value of the Modified property changes.
    /// </summary>
    [Description(@"Occurs when the value of the Modified property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? ModifiedChanged;

    /// <summary>
    /// Occurs when the value of the Multiline property changes.
    /// </summary>
    [Description(@"Occurs when the value of the Multiline property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? MultilineChanged;

    /// <summary>
    /// Occurs when the value of the ReadOnly property changes.
    /// </summary>
    [Description(@"Occurs when the value of the ReadOnly property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? ReadOnlyChanged;

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
    /// Initialise a new instance of the KryptonRibbonGroupTextBox class.
    /// </summary>
    public KryptonRibbonGroupTextBox()
    {
        // Default fields
        _visible = true;
        _enabled = true;
        _itemSizeCurrent = GroupItemSize.Medium;
        ShortcutKeys = Keys.None;
        _keyTip = "X";

        // Create the actual text box control and set initial settings
        TextBox = new KryptonTextBox
        {
            InputControlStyle = InputControlStyle.Ribbon,
            AlwaysActive = false,
            MinimumSize = new Size(121, 0),
            MaximumSize = new Size(121, 0),
            TabStop = false
        };

        // Hook into events to expose via this container
        TextBox!.AcceptsTabChanged += OnTextBoxAcceptsTabChanged;
        TextBox.TextAlignChanged += OnTextBoxTextAlignChanged;
        TextBox.TextChanged += OnTextBoxTextChanged;
        TextBox.HideSelectionChanged += OnTextBoxHideSelectionChanged;
        TextBox.ModifiedChanged += OnTextBoxModifiedChanged;
        TextBox.MultilineChanged += OnTextBoxMultilineChanged;
        TextBox.ReadOnlyChanged += OnTextBoxReadOnlyChanged;
        TextBox.GotFocus += OnTextBoxGotFocus;
        TextBox.LostFocus += OnTextBoxLostFocus;
        TextBox.KeyDown += OnTextBoxKeyDown;
        TextBox.KeyUp += OnTextBoxKeyUp;
        TextBox.KeyPress += OnTextBoxKeyPress;
        TextBox.PreviewKeyDown += OnTextBoxPreviewKeyDown;

        // Ensure we can track mouse events on the text box
        MonitorControl(TextBox);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (TextBox != null)
            {
                UnmonitorControl(TextBox);
                TextBox!.Dispose();
                TextBox = null;
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

            if (Ribbon != null)
            {
                // Use the same palette in the text box as the ribbon, plus we need
                // to know when the ribbon palette changes so we can reflect that change
                TextBox!.PaletteMode = Ribbon!.PaletteMode;
                TextBox.LocalCustomPalette = Ribbon!.LocalCustomPalette;
                Ribbon.PaletteChanged += OnRibbonPaletteChanged;
            }
        }
    }

    /// <summary>
    /// Gets and sets the shortcut key combination.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Shortcut key combination to set focus to the text box.")]
    public Keys ShortcutKeys { get; set; }

    private bool ShouldSerializeShortcutKeys() => ShortcutKeys != Keys.None;

    /// <summary>
    /// Resets the ShortcutKeys property to its default value.
    /// </summary>
    public void ResetShortcutKeys() => ShortcutKeys = Keys.None;

    /// <summary>
    /// Access to the actual embedded KryptonTextBox instance.
    /// </summary>
    [Description(@"Access to the actual embedded KryptonTextBox instance.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonTextBox? TextBox { get; private set; }

    /// <summary>
    /// Gets and sets the key tip for the ribbon group text box.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Ribbon group text box key tip.")]
    [DefaultValue("X")]
    public string KeyTip
    {
        get => _keyTip;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                value = "X";
            }

            _keyTip = value.ToUpper();
        }
    }

    /// <summary>
    /// Gets and sets the visible state of the text box.
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
    /// Make the ribbon group textbox visible.
    /// </summary>
    public void Show() => Visible = true;

    /// <summary>
    /// Make the ribbon group textbox hidden.
    /// </summary>
    public void Hide() => Visible = false;

    /// <summary>
    /// Gets and sets the enabled state of the group text box.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the group text box is enabled.")]
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
        get => TextBox!.MinimumSize;
        set => TextBox!.MinimumSize = value;
    }

    /// <summary>
    /// Gets or sets the maximum size of the control.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies the maximum size of the control.")]
    [DefaultValue(typeof(Size), "121, 0")]
    public Size MaximumSize
    {
        get => TextBox!.MaximumSize;
        set => TextBox!.MaximumSize = value;
    }

    /// <summary>
    /// Gets and sets the text associated with the control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text associated with the control.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Text
    {
        get => TextBox!.Text;
        set => TextBox!.Text = value;
    }

    /// <summary>
    /// Gets or sets the lines of text in a multiline edit, as an array of String values.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The lines of text in a multiline edit, as an array of String values.")]
    [Editor(@"System.Windows.Forms.Design.StringArrayEditor", typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [MergableProperty(false)]
    [Localizable(true)]
    public string[] Lines
    {
        get => TextBox!.Lines;
        set => TextBox!.Lines = value;
    }

    /// <summary>
    /// Gets or sets, for multiline edit controls, which scroll bars will be shown for this control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates, for multiline edit controls, which scroll bars will be shown for this control.")]
    [DefaultValue(typeof(ScrollBars), "None")]
    [Localizable(true)]
    public ScrollBars ScrollBars
    {
        get => TextBox!.ScrollBars;
        set => TextBox!.ScrollBars = value;
    }

    /// <summary>
    /// Gets or sets how the text should be aligned for edit controls.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates how the text should be aligned for edit controls.")]
    [DefaultValue(typeof(HorizontalAlignment), "Left")]
    [Localizable(true)]
    public HorizontalAlignment TextAlign
    {
        get => TextBox!.TextAlign;
        set => TextBox!.TextAlign = value;
    }

    /// <summary>
    /// Gets and sets the associated context menu strip.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The shortcut to display when the user right-clicks the control.")]
    [DefaultValue(null)]
    public ContextMenuStrip? ContextMenuStrip
    {
        get => TextBox!.ContextMenuStrip;
        set => TextBox!.ContextMenuStrip = value;
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu for showing when the text box is right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"KryptonContextMenu to be shown when the text box is right clicked.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => TextBox!.KryptonContextMenu;
        set => TextBox!.KryptonContextMenu = value;
    }

    /// <summary>
    /// Indicates if lines are automatically word-wrapped for multiline edit controls.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if lines are automatically word-wrapped for multiline edit controls.")]
    [DefaultValue(true)]
    [Localizable(true)]
    public bool WordWrap
    {
        get => TextBox!.WordWrap;
        set => TextBox!.WordWrap = value;
    }

    /// <summary>
    /// Gets and sets whether the text in the control can span more than one line.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Control whether the text in the control can span more than one line.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(false)]
    [Localizable(true)]
    public bool Multiline
    {
        get => TextBox!.Multiline;
        set => TextBox!.Multiline = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if return characters are accepted as input for multiline edit controls.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if return characters are accepted as input for multiline edit controls.")]
    [DefaultValue(false)]
    public bool AcceptsReturn
    {
        get => TextBox!.AcceptsReturn;
        set => TextBox!.AcceptsReturn = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if tab characters are accepted as input for multiline edit controls.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if tab characters are accepted as input for multiline edit controls.")]
    [DefaultValue(false)]
    public bool AcceptsTab
    {
        get => TextBox!.AcceptsTab;
        set => TextBox!.AcceptsTab = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if all the characters should be left alone or converted to uppercase or lowercase.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if all the characters should be left alone or converted to uppercase or lowercase.")]
    [DefaultValue(typeof(CharacterCasing), "Normal")]
    public CharacterCasing CharacterCasing
    {
        get => TextBox!.CharacterCasing;
        set => TextBox!.CharacterCasing = value;
    }

    /// <summary>
    /// Gets or sets a value indicating that the selection should be hidden when the edit control loses focus.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates that the selection should be hidden when the edit control loses focus.")]
    [DefaultValue(true)]
    public bool HideSelection
    {
        get => TextBox!.HideSelection;
        set => TextBox!.HideSelection = value;
    }

    /// <summary>
    /// Gets or sets the maximum number of characters that can be entered into the edit control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies the maximum number of characters that can be entered into the edit control.")]
    [DefaultValue(32767)]
    [Localizable(true)]
    public int MaxLength
    {
        get => TextBox!.MaxLength;
        set => TextBox!.MaxLength = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the text in the edit control can be changed or not.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Controls whether the text in the edit control can be changed or not.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(false)]
    public bool ReadOnly
    {
        get => TextBox!.ReadOnly;
        set => TextBox!.ReadOnly = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether shortcuts defined for the control are enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether shortcuts defined for the control are enabled.")]
    [DefaultValue(true)]
    public bool ShortcutsEnabled
    {
        get => TextBox!.ShortcutsEnabled;
        set => TextBox!.ShortcutsEnabled = value;
    }

    /// <summary>
    /// Gets or sets a the character to display for password input for single-line edit controls.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates the character to display for password input for single-line edit controls.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue('\0')]
    [Localizable(true)]
    public char PasswordChar
    {
        get => TextBox!.PasswordChar;
        set => TextBox!.PasswordChar = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if the text in the edit control should appear as the default password character.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if the text in the edit control should appear as the default password character.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(false)]
    public bool UseSystemPasswordChar
    {
        get => TextBox!.UseSystemPasswordChar;
        set => TextBox!.UseSystemPasswordChar = value;
    }

    /// <summary>
    /// Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.
    /// </summary>
    [Description(@"The StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
    [Editor(@"System.Windows.Forms.Design.ListControlStringCollectionEditor", typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Localizable(true)]
    [Browsable(true)]
    public AutoCompleteStringCollection AutoCompleteCustomSource
    {
        get => TextBox!.AutoCompleteCustomSource;
        set => TextBox!.AutoCompleteCustomSource = value;
    }

    /// <summary>
    /// Gets or sets the text completion behavior of the TextBox!.
    /// </summary>
    [Description(@"Indicates the text completion behavior of the TextBox.")]
    [DefaultValue(typeof(AutoCompleteMode), "None")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public AutoCompleteMode AutoCompleteMode
    {
        get => TextBox!.AutoCompleteMode;
        set => TextBox!.AutoCompleteMode = value;
    }

    /// <summary>
    /// Gets or sets the autocomplete source, which can be one of the values from AutoCompleteSource enumeration.
    /// </summary>
    [Description(@"The autocomplete source, which can be one of the values from AutoCompleteSource enumeration.")]
    [DefaultValue(typeof(AutoCompleteSource), "None")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public AutoCompleteSource AutoCompleteSource
    {
        get => TextBox!.AutoCompleteSource;
        set => TextBox!.AutoCompleteSource = value;
    }

    /// <summary>
    /// Gets access to the Wrapped Controls Tooltips.
    /// </summary>
    public override ToolTipValues ToolTipValues => TextBox!.ToolTipValues;

    /// <summary>
    /// Gets and sets a value indicating if tooltips should be Displayed for button specs.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should tooltips be Displayed for button specs.")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTips
    {
        get => TextBox!.AllowButtonSpecToolTips;
        set => TextBox!.AllowButtonSpecToolTips = value;
    }

    /// <summary>
    /// Gets and sets a value indicating if button spec tooltips should remove the parent tooltip.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should button spec tooltips should remove the parent tooltip")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTipPriority
    {
        get => TextBox!.AllowButtonSpecToolTipPriority;
        set => TextBox!.AllowButtonSpecToolTipPriority = value;
    }

    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonTextBox.TextBoxButtonSpecCollection ButtonSpecs => TextBox!.ButtonSpecs;

    /// <summary>
    /// Gets a value indicating whether the user can undo the previous operation in a rich text box control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanUndo => TextBox!.CanUndo;

    /// <summary>
    /// Gets a value indicating whether the contents have changed since last last.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Modified => TextBox!.Modified;

    /// <summary>
    /// Gets and sets the selected text within the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedText
    {
        get => TextBox!.SelectedText;
        set => TextBox!.SelectedText = value;
    }

    /// <summary>
    /// Gets and sets the selection length for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionLength
    {
        get => TextBox!.SelectionLength;
        set => TextBox!.SelectionLength = value;
    }

    /// <summary>
    /// Gets and sets the starting point of text selected in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionStart
    {
        get => TextBox!.SelectionStart;
        set => TextBox!.SelectionStart = value;
    }

    /// <summary>
    /// Gets the length of text in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TextLength => TextBox!.TextLength;

    /// <summary>
    /// Appends text to the current text of a rich text box.
    /// </summary>
    /// <param name="text">The text to append to the current contents of the text box.</param>
    public void AppendText(string text) => TextBox!.AppendText(text);

    /// <summary>
    /// Clears all text from the text box control.
    /// </summary>
    public void Clear() => TextBox!.Clear();

    /// <summary>
    /// Clears information about the most recent operation from the undo buffer of the rich text box. 
    /// </summary>
    public void ClearUndo() => TextBox!.ClearUndo();

    /// <summary>
    /// Copies the current selection in the text box to the Clipboard.
    /// </summary>
    public void Copy() => TextBox!.Copy();

    /// <summary>
    /// Moves the current selection in the text box to the Clipboard.
    /// </summary>
    public void Cut() => TextBox!.Cut();

    /// <summary>
    /// Replaces the current selection in the text box with the contents of the Clipboard.
    /// </summary>
    public void Paste() => TextBox!.Paste();

    /// <summary>
    /// Scrolls the contents of the control to the current caret position.
    /// </summary>
    public void ScrollToCaret() => TextBox!.ScrollToCaret();

    /// <summary>
    /// Selects a range of text in the control.
    /// </summary>
    /// <param name="start">The position of the first character in the current text selection within the text box.</param>
    /// <param name="length">The number of characters to select.</param>
    public void Select(int start, int length) => TextBox!.Select(start, length);

    /// <summary>
    /// Selects all text in the control.
    /// </summary>
    public void SelectAll() => TextBox!.SelectAll();

    /// <summary>
    /// Undoes the last edit operation in the text box.
    /// </summary>
    public void Undo() => TextBox!.Undo();

    /// <summary>
    /// Specifies that the value of the SelectionLength property is zero so that no characters are selected in the control.
    /// </summary>
    public void DeselectAll() => TextBox!.DeselectAll();

    /// <summary>
    /// Retrieves the character that is closest to the specified location within the control.
    /// </summary>
    /// <param name="pt">The location from which to seek the nearest character.</param>
    /// <returns>The character at the specified location.</returns>
    public int GetCharFromPosition(Point pt) => TextBox!.GetCharFromPosition(pt);

    /// <summary>
    /// Retrieves the index of the character nearest to the specified location.
    /// </summary>
    /// <param name="pt">The location to search.</param>
    /// <returns>The zero-based character index at the specified location.</returns>
    public int GetCharIndexFromPosition(Point pt) => TextBox!.GetCharIndexFromPosition(pt);

    /// <summary>
    /// Retrieves the index of the first character of a given line.
    /// </summary>
    /// <param name="lineNumber">The line for which to get the index of its first character.</param>
    /// <returns>The zero-based character index in the specified line.</returns>
    public int GetFirstCharIndexFromLine(int lineNumber) => TextBox!.GetFirstCharIndexFromLine(lineNumber);

    /// <summary>
    /// Retrieves the index of the first character of the current line.
    /// </summary>
    /// <returns>The zero-based character index in the current line.</returns>
    public int GetFirstCharIndexOfCurrentLine() => TextBox!.GetFirstCharIndexOfCurrentLine();

    /// <summary>
    /// Retrieves the line number from the specified character position within the text of the RichTextBox control.
    /// </summary>
    /// <param name="index">The character index position to search.</param>
    /// <returns>The zero-based line number in which the character index is located.</returns>
    public int GetLineFromCharIndex(int index) => TextBox!.GetLineFromCharIndex(index);

    /// <summary>
    /// Retrieves the location within the control at the specified character index.
    /// </summary>
    /// <param name="index">The index of the character for which to retrieve the location.</param>
    /// <returns>The location of the specified character.</returns>
    public Point GetPositionFromCharIndex(int index) => TextBox!.GetPositionFromCharIndex(index);

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
        new ViewDrawRibbonGroupTextBox(ribbon, this, needPaint);

    /// <summary>
    /// Gets and sets the associated designer.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public IKryptonDesignObject TextBoxDesigner { get; set; }

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase? TextBoxView { get; set; }

    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the TextChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnTextChanged(EventArgs e) => TextChanged?.Invoke(this, e);

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
    /// Raises the AcceptsTabChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnAcceptsTabChanged(EventArgs e) => AcceptsTabChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TextAlignChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnTextAlignChanged(EventArgs e) => TextAlignChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the HideSelectionChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnHideSelectionChanged(EventArgs e) => HideSelectionChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ModifiedChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnModifiedChanged(EventArgs e) => ModifiedChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the MultilineChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnMultilineChanged(EventArgs e) => MultilineChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ReadOnlyChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnReadOnlyChanged(EventArgs e) => ReadOnlyChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    #region Internal
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Control LastParentControl { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal KryptonTextBox? LastTextBox { get; set; }

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
                    // Can the text box take the focus
                    if (LastTextBox is { CanFocus: true })
                    {
                        LastTextBox.TextBox.Focus();
                    }

                    return true;
                }
            }
        }

        return false;
    }
    #endregion

    #region Implementation
    private void MonitorControl(KryptonTextBox c)
    {
        c.MouseEnter += OnControlEnter;
        c.MouseLeave += OnControlLeave;
        c.TrackMouseEnter += OnControlEnter;
        c.TrackMouseLeave += OnControlLeave;
    }

    private void UnmonitorControl(KryptonTextBox c)
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

    private void OnTextBoxAcceptsTabChanged(object? sender, EventArgs e) => OnAcceptsTabChanged(e);

    private void OnTextBoxTextChanged(object? sender, EventArgs e) => OnTextChanged(e);

    private void OnTextBoxTextAlignChanged(object? sender, EventArgs e) => OnTextAlignChanged(e);

    private void OnTextBoxHideSelectionChanged(object? sender, EventArgs e) => OnHideSelectionChanged(e);

    private void OnTextBoxModifiedChanged(object? sender, EventArgs e) => OnModifiedChanged(e);

    private void OnTextBoxMultilineChanged(object? sender, EventArgs e) => OnMultilineChanged(e);

    private void OnTextBoxReadOnlyChanged(object? sender, EventArgs e) => OnReadOnlyChanged(e);

    private void OnTextBoxGotFocus(object? sender, EventArgs e) => OnGotFocus(e);

    private void OnTextBoxLostFocus(object? sender, EventArgs e) => OnLostFocus(e);

    private void OnTextBoxKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnTextBoxKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnTextBoxKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnTextBoxPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnRibbonPaletteChanged(object? sender, EventArgs e)
    {
        TextBox!.PaletteMode = Ribbon!.PaletteMode;
        TextBox.LocalCustomPalette = Ribbon.LocalCustomPalette;
    }

    #endregion
}