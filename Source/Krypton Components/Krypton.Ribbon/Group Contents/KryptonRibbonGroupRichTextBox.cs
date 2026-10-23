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
/// Represents a ribbon group rich text box.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupRichTextBox), "ToolboxBitmaps.KryptonRibbonGroupRichTextBox.bmp")]
[Designer(typeof(KryptonRibbonGroupRichTextBoxDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultEvent(nameof(TextChanged))]
[DefaultProperty(nameof(Text))]
public class KryptonRibbonGroupRichTextBox : KryptonRibbonGroupItem
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
    /// Occurs when the current selection has changed.
    /// </summary>
    [Description(@"Occurs when the current selection has changed.")]
    [Category(@"Behavior")]
    public event EventHandler? SelectionChanged;

    /// <summary>
    /// Occurs when the user takes an action that would change a protected range of text.
    /// </summary>
    [Description(@"Occurs when the user takes an action that would change a protected range of text.")]
    [Category(@"Behavior")]
    public event EventHandler? Protected;

    /// <summary>
    /// Occurs when a hyperlink in the text is clicked.
    /// </summary>
    [Description(@"Occurs when a hyperlink in the text is clicked.")]
    [Category(@"Behavior")]
    public event EventHandler? LinkClicked;

    /// <summary>
    /// Occurs when the horizontal scroll bar is clicked.
    /// </summary>
    [Description(@"Occurs when the horizontal scroll bar is clicked.")]
    [Category(@"Behavior")]
    public event EventHandler? HScroll;

    /// <summary>
    /// Occurs when the vertical scroll bar is clicked.
    /// </summary>
    [Description(@"Occurs when the vertical scroll bar is clicked.")]
    [Category(@"Behavior")]
    public event EventHandler? VScroll;

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
    /// Initialise a new instance of the KryptonRibbonGroupRichTextBox class.
    /// </summary>
    public KryptonRibbonGroupRichTextBox()
    {
        // Default fields
        _visible = true;
        _enabled = true;
        _itemSizeCurrent = GroupItemSize.Medium;
        ShortcutKeys = Keys.None;
        _keyTip = "X";

        // Create the actual text box control and set initial settings
        RichTextBox = new KryptonRichTextBox
        {
            InputControlStyle = InputControlStyle.Ribbon,
            AlwaysActive = false,
            MinimumSize = new Size(121, 0),
            MaximumSize = new Size(121, 0),
            Multiline = false,
            ScrollBars = RichTextBoxScrollBars.None,
            TabStop = false
        };

        // Hook into events to expose via our container
        RichTextBox!.AcceptsTabChanged += OnRichTextBoxAcceptsTabChanged;
        RichTextBox.TextChanged += OnRichTextBoxTextChanged;
        RichTextBox.HideSelectionChanged += OnRichTextBoxHideSelectionChanged;
        RichTextBox.ModifiedChanged += OnRichTextBoxModifiedChanged;
        RichTextBox.MultilineChanged += OnRichTextBoxMultilineChanged;
        RichTextBox.ReadOnlyChanged += OnRichTextBoxReadOnlyChanged;
        RichTextBox.GotFocus += OnRichTextBoxGotFocus;
        RichTextBox.LostFocus += OnRichTextBoxLostFocus;
        RichTextBox.KeyDown += OnRichTextBoxKeyDown;
        RichTextBox.KeyUp += OnRichTextBoxKeyUp;
        RichTextBox.KeyPress += OnRichTextBoxKeyPress;
        RichTextBox.PreviewKeyDown += OnRichTextBoxPreviewKeyDown;
        RichTextBox.LinkClicked += OnRichTextBoxLinkClicked;
        RichTextBox.Protected += OnRichTextBoxProtected;
        RichTextBox.SelectionChanged += OnRichTextBoxSelectionChanged;
        RichTextBox.HScroll += OnRichTextBoxHScroll;
        RichTextBox.VScroll += OnRichTextBoxVScroll;

        // Ensure we can track mouse events on the text box
        MonitorControl(RichTextBox);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (RichTextBox != null)
            {
                UnmonitorControl(RichTextBox);
                RichTextBox!.Dispose();
                RichTextBox = null;
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
                RichTextBox!.PaletteMode = Ribbon!.PaletteMode;
                RichTextBox.LocalCustomPalette = Ribbon!.LocalCustomPalette;
                Ribbon.PaletteChanged += OnRibbonPaletteChanged;
            }
        }
    }

    /// <summary>
    /// Gets and sets the shortcut key combination.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Shortcut key combination to set focus to the rich text box.")]
    public Keys ShortcutKeys { get; set; }

    private bool ShouldSerializeShortcutKeys() => ShortcutKeys != Keys.None;

    /// <summary>
    /// Resets the ShortcutKeys property to its default value.
    /// </summary>
    public void ResetShortcutKeys() => ShortcutKeys = Keys.None;

    /// <summary>
    /// Access to the actual embedded KryptonRichTextBox instance.
    /// </summary>
    [Description(@"Access to the actual embedded KryptonRichTextBox instance.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonRichTextBox? RichTextBox { get; private set; }

    /// <summary>
    /// Gets and sets the key tip for the ribbon group rich text box.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Ribbon group rich text box key tip.")]
    [DefaultValue("X")]
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
    /// Gets and sets the visible state of the rich text box.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the rich text box is visible or hidden.")]
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
    /// Gets and sets the enabled state of the group rich text box.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the group rich text box is enabled.")]
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
        get => RichTextBox!.MinimumSize;
        set => RichTextBox!.MinimumSize = value;
    }

    /// <summary>
    /// Gets or sets the maximum size of the control.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies the maximum size of the control.")]
    [DefaultValue(typeof(Size), "121, 0")]
    public Size MaximumSize
    {
        get => RichTextBox!.MaximumSize;
        set => RichTextBox!.MaximumSize = value;
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
        get => RichTextBox!.Text;
        set => RichTextBox!.Text = value;
    }

    /// <summary>
    /// Gets the length of text in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TextLength => RichTextBox!.TextLength;

    /// <summary>
    /// Gets and sets the associated context menu strip.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The shortcut to display when the user right-clicks the control.")]
    [DefaultValue(null)]
    public ContextMenuStrip? ContextMenuStrip
    {
        get => RichTextBox!.ContextMenuStrip;
        set => RichTextBox!.ContextMenuStrip = value;
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu for showing when the rich text box is right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"KryptonContextMenu to be shown when the rich text box is right clicked.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => RichTextBox!.KryptonContextMenu;
        set => RichTextBox!.KryptonContextMenu = value;
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
        get => RichTextBox!.Lines;
        set => RichTextBox!.Lines = value;
    }

    /// <summary>
    /// Gets or sets, for multiline edit controls, which scroll bars will be shown for this control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates, for multiline edit controls, which scroll bars will be shown for this control.")]
    [DefaultValue(typeof(RichTextBoxScrollBars), "None")]
    [Localizable(true)]
    public RichTextBoxScrollBars ScrollBars
    {
        get => RichTextBox!.ScrollBars;
        set => RichTextBox!.ScrollBars = value;
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
        get => RichTextBox!.WordWrap;
        set => RichTextBox!.WordWrap = value;
    }

    /// <summary>
    /// Defines the right margin dimensions.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Defines the right margin dimensions.")]
    [DefaultValue(0)]
    [Localizable(true)]
    public int RightMargin
    {
        get => RichTextBox!.RightMargin;
        set => RichTextBox!.RightMargin = value;
    }

    /// <summary>
    /// Turns on/off the selection margin.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Turns on/off the selection margin.")]
    [DefaultValue(false)]
    public bool ShowSelectionMargin
    {
        get => RichTextBox!.ShowSelectionMargin;
        set => RichTextBox!.ShowSelectionMargin = value;
    }

    /// <summary>
    /// Defines the current scaling factor of the KryptonRichTextBox display; 1.0 is normal viewing.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Defines the current scaling factor of the KryptonRichTextBox display; 1.0 is normal viewing.")]
    [DefaultValue(1.0f)]
    [Localizable(true)]
    public float ZoomFactor
    {
        get => RichTextBox!.ZoomFactor;
        set => RichTextBox!.ZoomFactor = value;
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
        get => RichTextBox!.Multiline;
        set => RichTextBox!.Multiline = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if tab characters are accepted as input for multiline edit controls.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if tab characters are accepted as input for multiline edit controls.")]
    [DefaultValue(false)]
    public bool AcceptsTab
    {
        get => RichTextBox!.AcceptsTab;
        set => RichTextBox!.AcceptsTab = value;
    }

    /// <summary>
    /// Gets or sets a value indicating that the selection should be hidden when the edit control loses focus.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates that the selection should be hidden when the edit control loses focus.")]
    [DefaultValue(true)]
    public bool HideSelection
    {
        get => RichTextBox!.HideSelection;
        set => RichTextBox!.HideSelection = value;
    }

    /// <summary>
    /// Gets or sets the maximum number of characters that can be entered into the edit control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies the maximum number of characters that can be entered into the edit control.")]
    [DefaultValue(0x7fffffff)]
    [Localizable(true)]
    public int MaxLength
    {
        get => RichTextBox!.MaxLength;
        set => RichTextBox!.MaxLength = value;
    }

    /// <summary>
    /// Turns on/off automatic word selection.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Turns on/off automatic word selection.")]
    [DefaultValue(false)]
    public bool AutoWordSelection
    {
        get => RichTextBox!.AutoWordSelection;
        set => RichTextBox!.AutoWordSelection = value;
    }

    /// <summary>
    /// Defines the indent for bullets in the control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Defines the indent for bullets in the control.")]
    [DefaultValue(0)]
    [Localizable(true)]
    public int BulletIndent
    {
        get => RichTextBox!.BulletIndent;
        set => RichTextBox!.BulletIndent = value;
    }

    /// <summary>
    /// Indicates whether URLs are automatically formatted as links.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether URLs are automatically formatted as links.")]
    [DefaultValue(true)]
    public bool DetectUrls
    {
        get => RichTextBox!.DetectUrls;
        set => RichTextBox!.DetectUrls = value;
    }

    /// <summary>
    /// Enable drag/drop of text, pictures and other data.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Enable drag/drop of text, pictures and other data.")]
    [DefaultValue(false)]
    public bool EnableAutoDragDrop
    {
        get => RichTextBox!.EnableAutoDragDrop;
        set => RichTextBox!.EnableAutoDragDrop = value;
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
        get => RichTextBox!.ReadOnly;
        set => RichTextBox!.ReadOnly = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether shortcuts defined for the control are enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether shortcuts defined for the control are enabled.")]
    [DefaultValue(true)]
    public bool ShortcutsEnabled
    {
        get => RichTextBox!.ShortcutsEnabled;
        set => RichTextBox!.ShortcutsEnabled = value;
    }

    /// <summary>
    /// Gets access to the Wrapped Controls Tooltips.
    /// </summary>
    public override ToolTipValues ToolTipValues => RichTextBox!.ToolTipValues;

    /// <summary>
    /// Gets and sets if the control can redo a previously undo operation.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanRedo => RichTextBox!.CanRedo;

    /// <summary>
    /// Gets a value indicating whether the user can undo the previous operation in a rich text box control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanUndo => RichTextBox!.CanUndo;

    /// <summary>
    /// Gets a value indicating whether the contents have changed since last last.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Modified => RichTextBox!.Modified;

    /// <summary>
    /// Gets and sets the language option.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RichTextBoxLanguageOptions LanguageOption
    {
        get => RichTextBox!.LanguageOption;
        set => RichTextBox!.LanguageOption = value;
    }

    /// <summary>
    /// Gets and sets the name of the action to be redone.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? RedoActionName => RichTextBox!.RedoActionName;

    /// <summary>
    /// Gets and sets the name of the action to be undone.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string UndoActionName => RichTextBox!.UndoActionName!;

    /// <summary>
    /// Gets and sets if keyboard shortcuts are enabled.
    /// </summary>
    [Browsable(false)]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool RichTextShortcutsEnabled
    {
        get => RichTextBox!.RichTextShortcutsEnabled;
        set => RichTextBox!.RichTextShortcutsEnabled = value;
    }

    /// <summary>
    /// Gets and sets the text in rich text format.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RefreshProperties(RefreshProperties.All)]
    [AllowNull]
    public string? Rtf
    {
        get => RichTextBox!.Rtf;
        set => RichTextBox!.Rtf = value;
    }

    /// <summary>
    /// Gets and sets the selection portion of the rich text format.
    /// </summary>
    [Browsable(false)]
    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedRtf
    {
        get => RichTextBox!.SelectedRtf;
        set => RichTextBox!.SelectedRtf = value;
    }

    /// <summary>
    /// Gets and sets the selected text within the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedText
    {
        get => RichTextBox!.SelectedText;
        set => RichTextBox!.SelectedText = value;
    }

    /// <summary>
    /// Gets and sets the alignment of the selection.
    /// </summary>
    [Browsable(false)]
    [DefaultValue(typeof(HorizontalAlignment), "Left")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public HorizontalAlignment SelectionAlignment
    {
        get => RichTextBox!.SelectionAlignment;
        set => RichTextBox!.SelectionAlignment = value;
    }

    /// <summary>
    /// Gets and sets the background color of the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SelectionBackColor
    {
        get => RichTextBox!.SelectionBackColor;
        set => RichTextBox!.SelectionBackColor = value;
    }

    /// <summary>
    /// Gets and sets the bullet indentation of the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SelectionBullet
    {
        get => RichTextBox!.SelectionBullet;
        set => RichTextBox!.SelectionBullet = value;
    }

    /// <summary>
    /// Gets and sets the character offset of the selection.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionCharOffset
    {
        get => RichTextBox!.SelectionCharOffset;
        set => RichTextBox!.SelectionCharOffset = value;
    }

    /// <summary>
    /// Gets and sets the text color of the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SelectionColor
    {
        get => RichTextBox!.SelectionColor;
        set => RichTextBox!.SelectionColor = value;
    }

    /// <summary>
    /// Gets and sets the text font for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font SelectionFont
    {
        get => RichTextBox!.SelectionFont;
        set => RichTextBox!.SelectionFont = value;
    }

    /// <summary>
    /// Gets and sets the hanging indent for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionHangingIndent
    {
        get => RichTextBox!.SelectionHangingIndent;
        set => RichTextBox!.SelectionHangingIndent = value;
    }

    /// <summary>
    /// Gets and sets the indent for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionIndent
    {
        get => RichTextBox!.SelectionIndent;
        set => RichTextBox!.SelectionIndent = value;
    }

    /// <summary>
    /// Gets and sets the selection length for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionLength
    {
        get => RichTextBox!.SelectionLength;
        set => RichTextBox!.SelectionLength = value;
    }

    /// <summary>
    /// Gets and sets the protected setting for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionProtected
    {
        get => RichTextBox!.SelectionLength;
        set => RichTextBox!.SelectionLength = value;
    }

    /// <summary>
    /// Gets and sets the right indent for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionRightIndent
    {
        get => RichTextBox!.SelectionRightIndent;
        set => RichTextBox!.SelectionRightIndent = value;
    }

    /// <summary>
    /// Gets and sets the starting point of text selected in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionStart
    {
        get => RichTextBox!.SelectionStart;
        set => RichTextBox!.SelectionStart = value;
    }

    /// <summary>
    /// Gets and sets the tab settings for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int[] SelectionTabs
    {
        get => RichTextBox!.SelectionTabs;
        set => RichTextBox!.SelectionTabs = value;
    }

    /// <summary>
    /// Gets and sets the type of selection.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RichTextBoxSelectionTypes SelectionType => RichTextBox!.SelectionType;

    /// <summary>
    /// Appends text to the current text of a rich text box.
    /// </summary>
    /// <param name="text">The text to append to the current contents of the text box.</param>
    public void AppendText(string text) => RichTextBox!.AppendText(text);

    /// <summary>
    /// Clears all text from the text box control.
    /// </summary>
    public void Clear() => RichTextBox!.Clear();

    /// <summary>
    /// Clears information about the most recent operation from the undo buffer of the rich text box. 
    /// </summary>
    public void ClearUndo() => RichTextBox!.ClearUndo();

    /// <summary>
    /// Copies the current selection in the text box to the Clipboard.
    /// </summary>
    public void Copy() => RichTextBox!.Copy();

    /// <summary>
    /// Moves the current selection in the text box to the Clipboard.
    /// </summary>
    public void Cut() => RichTextBox!.Cut();

    /// <summary>
    /// Specifies that the value of the SelectionLength property is zero so that no characters are selected in the control.
    /// </summary>
    public void DeselectAll() => RichTextBox!.DeselectAll();

    /// <summary>
    /// Determines whether you can paste information from the Clipboard in the specified data format.
    /// </summary>
    /// <param name="clipFormat">One of the System.Windows.Forms.DataFormats.Format values.</param>
    /// <returns>true if you can paste data from the Clipboard in the specified data format; otherwise, false.</returns>
    public bool CanPaste(DataFormats.Format clipFormat) => RichTextBox!.CanPaste(clipFormat);

    /// <summary>
    /// Searches the text in a RichTextBox control for a string.
    /// </summary>
    /// <param name="str">The text to locate in the control.</param>
    /// <returns>The location within the control where the search text was found or -1 if the search string is not found or an empty search string is specified in the str parameter.</returns>
    public int Find(string str) => RichTextBox!.Find(str);

    /// <summary>
    /// Searches the text of a RichTextBox control for the first instance of a character from a list of characters.
    /// </summary>
    /// <param name="characterSet">The array of characters to search for.</param>
    /// <returns>The location within the control where the search characters were found or -1 if the search characters are not found or an empty search character set is specified in the char parameter.</returns>
    public int Find(char[] characterSet) => RichTextBox!.Find(characterSet);

    /// <summary>
    /// Searches the text of a RichTextBox control, at a specific starting point, for the first instance of a character from a list of characters.
    /// </summary>
    /// <param name="characterSet">The array of characters to search for.</param>
    /// <param name="start">The location within the control's text at which to begin searching.</param>
    /// <returns>The location within the control where the search characters are found.</returns>
    public int Find(char[] characterSet, int start) => RichTextBox!.Find(characterSet, start);

    /// <summary>
    /// Searches the text in a RichTextBox control for a string with specific options applied to the search.
    /// </summary>
    /// <param name="str">The text to locate in the control.</param>
    /// <param name="options">A bitwise combination of the RichTextBoxFinds values.</param>
    /// <returns>The location within the control where the search text was found.</returns>
    public int Find(string str, RichTextBoxFinds options) => RichTextBox!.Find(str, options);

    /// <summary>
    /// Searches a range of text in a RichTextBox control for the first instance of a character from a list of characters.
    /// </summary>
    /// <param name="characterSet">The array of characters to search for.</param>
    /// <param name="start">The location within the control's text at which to begin searching.</param>
    /// <param name="end">The location within the control's text at which to end searching.</param>
    /// <returns>The location within the control where the search characters are found.</returns>
    public int Find(char[] characterSet, int start, int end) => RichTextBox!.Find(characterSet, start, end);

    /// <summary>
    /// Searches the text in a RichTextBox control for a string at a specific location within the control and with specific options applied to the search.
    /// </summary>
    /// <param name="str">The text to locate in the control.</param>
    /// <param name="start">The location within the control's text at which to begin searching.</param>
    /// <param name="options">A bitwise combination of the RichTextBoxFinds values.</param>
    /// <returns>The location within the control where the search text was found.</returns>
    public int Find(string str, int start, RichTextBoxFinds options) => RichTextBox!.Find(str, start, options);

    /// <summary>
    /// Searches the text in a RichTextBox control for a string within a range of text within the control and with specific options applied to the search.
    /// </summary>
    /// <param name="str">The text to locate in the control.</param>
    /// <param name="start">The location within the control's text at which to begin searching.</param>
    /// <param name="end">The location within the control's text at which to end searching. This value must be equal to negative one (-1) or greater than or equal to the start parameter.</param>
    /// <param name="options">A bitwise combination of the RichTextBoxFinds values.</param>
    /// <returns></returns>
    public int Find(string str, int start, int end, RichTextBoxFinds options) => RichTextBox!.Find(str, start, end, options);

    /// <summary>
    /// Retrieves the character that is closest to the specified location within the control.
    /// </summary>
    /// <param name="pt">The location from which to seek the nearest character.</param>
    /// <returns>The character at the specified location.</returns>
    public int GetCharFromPosition(Point pt) => RichTextBox!.GetCharFromPosition(pt);

    /// <summary>
    /// Retrieves the index of the character nearest to the specified location.
    /// </summary>
    /// <param name="pt">The location to search.</param>
    /// <returns>The zero-based character index at the specified location.</returns>
    public int GetCharIndexFromPosition(Point pt) => RichTextBox!.GetCharIndexFromPosition(pt);

    /// <summary>
    /// Retrieves the index of the first character of a given line.
    /// </summary>
    /// <param name="lineNumber">The line for which to get the index of its first character.</param>
    /// <returns>The zero-based character index in the specified line.</returns>
    public int GetFirstCharIndexFromLine(int lineNumber) => RichTextBox!.GetFirstCharIndexFromLine(lineNumber);

    /// <summary>
    /// Retrieves the index of the first character of the current line.
    /// </summary>
    /// <returns>The zero-based character index in the current line.</returns>
    public int GetFirstCharIndexOfCurrentLine() => RichTextBox!.GetFirstCharIndexOfCurrentLine();

    /// <summary>
    /// Retrieves the line number from the specified character position within the text of the RichTextBox control.
    /// </summary>
    /// <param name="index">The character index position to search.</param>
    /// <returns>The zero-based line number in which the character index is located.</returns>
    public int GetLineFromCharIndex(int index) => RichTextBox!.GetLineFromCharIndex(index);

    /// <summary>
    /// Retrieves the location within the control at the specified character index.
    /// </summary>
    /// <param name="index">The index of the character for which to retrieve the location.</param>
    /// <returns>The location of the specified character.</returns>
    public Point GetPositionFromCharIndex(int index) => RichTextBox!.GetPositionFromCharIndex(index);

    /// <summary>
    /// Loads a rich text format (RTF) or standard ASCII text file into the RichTextBox control.
    /// </summary>
    /// <param name="path">The name and location of the file to load into the control.</param>
    public void LoadFile(string path) => RichTextBox!.LoadFile(path);

    /// <summary>
    /// Loads the contents of an existing data stream into the RichTextBox control.
    /// </summary>
    /// <param name="data">A stream of data to load into the RichTextBox control.</param>
    /// <param name="fileType">One of the RichTextBoxStreamType values.</param>
    public void LoadFile(Stream data, RichTextBoxStreamType fileType) => RichTextBox!.LoadFile(data, fileType);

    /// <summary>
    /// Loads a specific type of file into the RichTextBox control.
    /// </summary>
    /// <param name="path">The name and location of the file to load into the control.</param>
    /// <param name="fileType">One of the RichTextBoxStreamType values.</param>
    public void LoadFile(string path, RichTextBoxStreamType fileType) => RichTextBox!.LoadFile(path, fileType);

    /// <summary>
    /// Replaces the current selection in the text box with the contents of the Clipboard.
    /// </summary>
    public void Paste() => RichTextBox!.Paste();

    /// <summary>
    /// Undoes the last edit operation in the text box.
    /// </summary>
    public void Undo() => RichTextBox!.Undo();

    /// <summary>
    /// Pastes the contents of the Clipboard in the specified Clipboard format.
    /// </summary>
    /// <param name="clipFormat">The Clipboard format in which the data should be obtained from the Clipboard.</param>
    public void Paste(DataFormats.Format clipFormat) => RichTextBox!.Paste(clipFormat);

    /// <summary>
    /// Reapplies the last operation that was undone in the control.
    /// </summary>
    public void Redo() => RichTextBox!.Redo();

    /// <summary>
    /// Saves the contents of the RichTextBox to a rich text format (RTF) file.
    /// </summary>
    /// <param name="path">The name and location of the file to save.</param>
    public void SaveFile(string path) => RichTextBox!.SaveFile(path);

    /// <summary>
    /// Saves the contents of a RichTextBox control to an open data stream.
    /// </summary>
    /// <param name="data">The data stream that contains the file to save to.</param>
    /// <param name="fileType">One of the RichTextBoxStreamType values.</param>
    public void SaveFile(Stream data, RichTextBoxStreamType fileType) => RichTextBox!.SaveFile(data, fileType);

    /// <summary>
    /// Saves the contents of the KryptonRichTextBox to a specific type of file.
    /// </summary>
    /// <param name="path">The name and location of the file to save.</param>
    /// <param name="fileType">One of the RichTextBoxStreamType values.</param>
    public void SaveFile(string path, RichTextBoxStreamType fileType) => RichTextBox!.SaveFile(path, fileType);

    /// <summary>
    /// Scrolls the contents of the control to the current caret position.
    /// </summary>
    public void ScrollToCaret() => RichTextBox!.ScrollToCaret();

    /// <summary>
    /// Selects a range of text in the control.
    /// </summary>
    /// <param name="start">The position of the first character in the current text selection within the text box.</param>
    /// <param name="length">The number of characters to select.</param>
    public void Select(int start, int length) => RichTextBox!.Select(start, length);

    /// <summary>
    /// Selects all text in the control.
    /// </summary>
    public void SelectAll() => RichTextBox!.SelectAll();

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
        new ViewDrawRibbonGroupRichTextBox(ribbon, this, needPaint);

    /// <summary>
    /// Gets and sets the associated designer.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public IKryptonDesignObject RichTextBoxDesigner { get; set; }

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase? RichTextBoxView { get; set; }

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
    /// Raises the VScroll event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnVScroll(EventArgs e) => VScroll?.Invoke(this, e);

    /// <summary>
    /// Raises the HScroll event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnHScroll(EventArgs e) => HScroll?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectionChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnSelectionChanged(EventArgs e) => SelectionChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the Protected event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnProtected(EventArgs e) => Protected?.Invoke(this, e);

    /// <summary>
    /// Raises the LinkClicked event.
    /// </summary>
    /// <param name="e">A LinkClickedEventArgs that contains the event data.</param>
    protected virtual void OnLinkClicked(LinkClickedEventArgs e) => LinkClicked?.Invoke(this, e);

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
    internal KryptonRichTextBox? LastRichTextBox { get; set; }

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
                    // Can the rich text box take the focus
                    if (LastRichTextBox is { CanFocus: true })
                    {
                        LastRichTextBox.RichTextBox.Focus();
                    }

                    return true;
                }
            }
        }

        return false;
    }
    #endregion

    #region Implementation
    private void MonitorControl(KryptonRichTextBox c)
    {
        c.MouseEnter += OnControlEnter;
        c.MouseLeave += OnControlLeave;
        c.TrackMouseEnter += OnControlEnter;
        c.TrackMouseLeave += OnControlLeave;
    }

    private void UnmonitorControl(KryptonRichTextBox c)
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

    private void OnRichTextBoxAcceptsTabChanged(object? sender, EventArgs e) => OnAcceptsTabChanged(e);

    private void OnRichTextBoxTextChanged(object? sender, EventArgs e) => OnTextChanged(e);

    private void OnRichTextBoxHideSelectionChanged(object? sender, EventArgs e) => OnHideSelectionChanged(e);

    private void OnRichTextBoxModifiedChanged(object? sender, EventArgs e) => OnModifiedChanged(e);

    private void OnRichTextBoxMultilineChanged(object? sender, EventArgs e) => OnMultilineChanged(e);

    private void OnRichTextBoxReadOnlyChanged(object? sender, EventArgs e) => OnReadOnlyChanged(e);

    private void OnRichTextBoxGotFocus(object? sender, EventArgs e) => OnGotFocus(e);

    private void OnRichTextBoxLostFocus(object? sender, EventArgs e) => OnLostFocus(e);

    private void OnRichTextBoxKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnRichTextBoxKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnRichTextBoxKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnRichTextBoxPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnRichTextBoxVScroll(object? sender, EventArgs e) => OnVScroll(e);

    private void OnRichTextBoxHScroll(object? sender, EventArgs e) => OnHScroll(e);

    private void OnRichTextBoxSelectionChanged(object? sender, EventArgs e) => OnSelectionChanged(e);

    private void OnRichTextBoxProtected(object? sender, EventArgs e) => OnProtected(e);

    private void OnRichTextBoxLinkClicked(object? sender, LinkClickedEventArgs e) => OnLinkClicked(e);

    private void OnRibbonPaletteChanged(object? sender, EventArgs e)
    {
        if (RichTextBox is not null && Ribbon is not null)
        {
            RichTextBox.PaletteMode = Ribbon.PaletteMode;
            RichTextBox.LocalCustomPalette = Ribbon.LocalCustomPalette;
        }
    }

    #endregion
}