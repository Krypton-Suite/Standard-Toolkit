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
/// Represents a ribbon group masked text box.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupMaskedTextBox), "ToolboxBitmaps.KryptonRibbonGroupMaskedTextBox.bmp")]
[Designer(typeof(KryptonRibbonGroupMaskedTextBoxDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultEvent(nameof(TextChanged))]
[DefaultProperty(nameof(Mask))]
public class KryptonRibbonGroupMaskedTextBox : KryptonRibbonGroupItem
{
    #region Instance Fields
    private bool _visible;
    private bool _enabled;
    private string? _keyTip;
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
    /// Occurs when the value of the Text property changes.
    /// </summary>
    [Description(@"Occurs when the value of the Text property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? TextChanged;

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
    /// Occurs when the value of the ReadOnly property changes.
    /// </summary>
    [Description(@"Occurs when the value of the ReadOnly property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? ReadOnlyChanged;

    /// <summary>
    /// Occurs when the value of the TextAlign property changes.
    /// </summary>
    [Description(@"Occurs when the value of the TextAlign property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? TextAlignChanged;

    /// <summary>
    /// Occurs when the value of the Mask property changes.
    /// </summary>
    [Description(@"Occurs when the value of the Mask property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? MaskChanged;

    /// <summary>
    /// Occurs when the value of the IsOverwriteMode property changes.
    /// </summary>
    [Description(@"Occurs when the value of the IsOverwriteMode property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? IsOverwriteModeChanged;

    /// <summary>
    /// Occurs when the input character or text does not comply with the mask specification.
    /// </summary>
    [Description(@"Occurs when the input character or text does not comply with the mask specification.")]
    [Category(@"Behavior")]
    public event MaskInputRejectedEventHandler? MaskInputRejected;

    /// <summary>
    /// Occurs when the validating type object has completed parsing the input text.
    /// </summary>
    [Description(@"Occurs when the validating type object has completed parsing the input text.")]
    [Category(@"Focus")]
    public event TypeValidationEventHandler? TypeValidationCompleted;

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
    /// Initialise a new instance of the KryptonRibbonGroupMaskedTextBox class.
    /// </summary>
    public KryptonRibbonGroupMaskedTextBox()
    {
        // Default fields
        _visible = true;
        _enabled = true;
        _itemSizeCurrent = GroupItemSize.Medium;
        ShortcutKeys = Keys.None;
        _keyTip = @"X";

        // Create the actual masked text box control and set initial settings
        MaskedTextBox = new KryptonMaskedTextBox
        {
            InputControlStyle = InputControlStyle.Ribbon,
            AlwaysActive = false,
            MinimumSize = new Size(121, 0),
            MaximumSize = new Size(121, 0),
            TabStop = false
        };

        // Hook into events to expose via our container
        MaskedTextBox.TextAlignChanged += OnMaskedTextBoxTextAlignChanged;
        MaskedTextBox.TextChanged += OnMaskedTextBoxTextChanged;
        MaskedTextBox.HideSelectionChanged += OnMaskedTextBoxHideSelectionChanged;
        MaskedTextBox.ModifiedChanged += OnMaskedTextBoxModifiedChanged;
        MaskedTextBox.ReadOnlyChanged += OnMaskedTextBoxReadOnlyChanged;
        MaskedTextBox.MaskChanged += OnMaskedMaskChanged;
        MaskedTextBox.IsOverwriteModeChanged += OnMaskedIsOverwriteModeChanged;
        MaskedTextBox.MaskInputRejected += OnMaskedMaskInputRejected;
        MaskedTextBox.TypeValidationCompleted += OnMaskedTypeValidationCompleted;
        MaskedTextBox.GotFocus += OnMaskedTextBoxGotFocus;
        MaskedTextBox.LostFocus += OnMaskedTextBoxLostFocus;
        MaskedTextBox.KeyDown += OnMaskedTextBoxKeyDown;
        MaskedTextBox.KeyUp += OnMaskedTextBoxKeyUp;
        MaskedTextBox.KeyPress += OnMaskedTextBoxKeyPress;
        MaskedTextBox.PreviewKeyDown += OnMaskedTextBoxPreviewKeyDown;

        // Ensure we can track mouse events on the masked text box
        MonitorControl(MaskedTextBox);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (MaskedTextBox != null!)
            {
                UnmonitorControl(MaskedTextBox);
                MaskedTextBox.Dispose();
                MaskedTextBox = null!;
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
                // Use the same palette in the masked text box as the ribbon, plus we need
                // to know when the ribbon palette changes so we can reflect that change
                MaskedTextBox.PaletteMode = Ribbon!.PaletteMode;
                MaskedTextBox.LocalCustomPalette = Ribbon!.LocalCustomPalette;
                value.PaletteChanged += OnRibbonPaletteChanged;
            }
        }
    }

    /// <summary>
    /// Gets and sets the shortcut key combination.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Shortcut key combination to set focus to the masked text box.")]
    public Keys ShortcutKeys { get; set; }

    private bool ShouldSerializeShortcutKeys() => ShortcutKeys != Keys.None;

    /// <summary>
    /// Resets the ShortcutKeys property to its default value.
    /// </summary>
    public void ResetShortcutKeys() => ShortcutKeys = Keys.None;

    /// <summary>
    /// Access to the actual embedded KryptonMaskedTextBox instance.
    /// </summary>
    [Description(@"Access to the actual embedded KryptonMaskedTextBox instance.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonMaskedTextBox MaskedTextBox { get; private set; }

    /// <summary>
    /// Gets and sets the key tip for the ribbon group masked text box.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Ribbon group masked text box key tip.")]
    [DefaultValue("X")]
    [AllowNull]
    public string? KeyTip
    {
        get => _keyTip;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                value = @"X";
            }

            _keyTip = value?.ToUpper();
        }
    }

    /// <summary>
    /// Gets and sets the visible state of the masked text box.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the masked text box is visible or hidden.")]
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
    /// Make the ribbon group masked text box visible.
    /// </summary>
    public void Show() => Visible = true;

    /// <summary>
    /// Make the ribbon group masked text box hidden.
    /// </summary>
    public void Hide() => Visible = false;

    /// <summary>
    /// Gets and sets the enabled state of the group masked text box.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the group masked text box is enabled.")]
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
        get => MaskedTextBox.MinimumSize;
        set => MaskedTextBox.MinimumSize = value;
    }

    /// <summary>
    /// Gets or sets the maximum size of the control.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies the maximum size of the control.")]
    [DefaultValue(typeof(Size), "121, 0")]
    public Size MaximumSize
    {
        get => MaskedTextBox.MaximumSize;
        set => MaskedTextBox.MaximumSize = value;
    }

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
    /// Gets and sets the text associated with the control.
    /// </summary>
    [Category(@"Appearance")]
    [Editor(@"System.Windows.Forms.Design.MaskedTextBoxTextEditor", typeof(UITypeEditor))]
    [AllowNull]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string? Text
    {
        get => MaskedTextBox.Text;
        set => MaskedTextBox.Text = value;
    }

    /// <summary>
    /// Gets a value indicating whether the contents have changed since last last.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Modified => MaskedTextBox.Modified;

    /// <summary>
    /// Gets and sets the selected text within the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public string? SelectedText
    {
        get => MaskedTextBox.SelectedText;
        set => MaskedTextBox.SelectedText = value;
    }

    /// <summary>
    /// Gets and sets the selection length for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionLength
    {
        get => MaskedTextBox.SelectionLength;
        set => MaskedTextBox.SelectionLength = value;
    }

    /// <summary>
    /// Gets and sets the starting point of text selected in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionStart
    {
        get => MaskedTextBox.SelectionStart;
        set => MaskedTextBox.SelectionStart = value;
    }

    /// <summary>
    /// Gets the length of text in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TextLength => MaskedTextBox.TextLength;

    /// <summary>
    /// Gets a value that specifies whether new user input overwrites existing input.
    /// </summary>
    [Browsable(false)]
    public bool IsOverwriteMode => MaskedTextBox.IsOverwriteMode;

    /// <summary>
    /// Gets a value indicating whether all required inputs have been entered into the input mask.
    /// </summary>
    [Browsable(false)]
    public bool MaskCompleted => MaskedTextBox.MaskCompleted;

    /// <summary>
    /// Gets a clone of the mask provider associated with this instance of the masked text box control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public MaskedTextProvider? MaskedTextProvider => MaskedTextBox.MaskedTextProvider;

    /// <summary>
    /// Gets a value indicating whether all required and optional inputs have been entered into the input mask.
    /// </summary>
    [Browsable(false)]
    public bool MaskFull => MaskedTextBox.MaskFull;

    /// <summary>
    /// Gets or sets the maximum number of characters that can be entered into the edit control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int MaxLength
    {
        get => MaskedTextBox.MaxLength;
        set => MaskedTextBox.MaxLength = value;
    }

    /// <summary>
    /// Gets or sets the data type used to verify the data input by the user.
    /// </summary>
    [Browsable(false)]
    [DefaultValue(null)]
    public Type? ValidatingType
    {
        get => MaskedTextBox.ValidatingType;
        set => MaskedTextBox.ValidatingType = value;
    }

    /// <summary>
    /// Gets or sets how the text should be aligned for edit controls.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates how the text should be aligned for edit controls.")]
    [DefaultValue(typeof(HorizontalAlignment), @"Left")]
    [Localizable(true)]
    public HorizontalAlignment TextAlign
    {
        get => MaskedTextBox.TextAlign;
        set => MaskedTextBox.TextAlign = value;
    }

    /// <summary>
    /// Indicates the character used as the placeholder.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates the character used as the placeholder.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("_")]
    [Localizable(true)]
    public char PromptChar
    {
        get => MaskedTextBox.PromptChar;
        set => MaskedTextBox.PromptChar = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether PromptChar can be entered as valid data by the user.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the prompt character is valid as input.")]
    [DefaultValue(true)]
    public bool AllowPromptAsInput
    {
        get => MaskedTextBox.AllowPromptAsInput;
        set => MaskedTextBox.AllowPromptAsInput = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the MaskedTextBox control accepts characters outside of the ASCII character set.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether only Ascii characters are valid as input.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(false)]
    public bool AsciiOnly
    {
        get => MaskedTextBox.AsciiOnly;
        set => MaskedTextBox.AsciiOnly = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the masked text box control raises the system beep for each user key stroke that it rejects.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the control will beep when an invalid character is typed.")]
    [DefaultValue(false)]
    public bool BeepOnError
    {
        get => MaskedTextBox.BeepOnError;
        set => MaskedTextBox.BeepOnError = value;
    }

    /// <summary>
    /// Gets or sets the culture information associated with the masked text box.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The culture that determines the value of the locaizable mask language separators and placeholders.")]
    [RefreshProperties(RefreshProperties.All)]
    public CultureInfo Culture
    {
        get => MaskedTextBox.Culture;
        set => MaskedTextBox.Culture = value;
    }

    private bool ShouldSerializeCulture() => !CultureInfo.CurrentCulture.Equals(Culture);

    /// <summary>
    /// Gets or sets a value that determines whether literals and prompt characters are copied to the clipboard.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the text to be copied to the clipboard includes literals and/or prompt characters.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(typeof(MaskFormat), @"IncludeLiterals")]
    public MaskFormat CutCopyMaskFormat
    {
        get => MaskedTextBox.CutCopyMaskFormat;
        set => MaskedTextBox.CutCopyMaskFormat = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the prompt characters in the input mask are hidden when the masked text box loses focus.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether prompt characters are Displayed when the control does not have focus.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(false)]
    public bool HidePromptOnLeave
    {
        get => MaskedTextBox.HidePromptOnLeave;
        set => MaskedTextBox.HidePromptOnLeave = value;
    }

    /// <summary>
    /// Gets or sets the text insertion mode of the masked text box control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates the masked text box input character typing mode.")]
    [DefaultValue(typeof(InsertKeyMode), @"Default")]
    public InsertKeyMode InsertKeyMode
    {
        get => MaskedTextBox.InsertKeyMode;
        set => MaskedTextBox.InsertKeyMode = value;
    }

    /// <summary>
    /// Gets or sets the input mask to use at run time. 
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Sets the string governing the input allowed for the control.")]
    [RefreshProperties(RefreshProperties.All)]
    [MergableProperty(false)]
    [DefaultValue("")]
    [Localizable(true)]
    [AllowNull]
    public string Mask
    {
        get => MaskedTextBox.Mask;
        set => MaskedTextBox.Mask = value;
    }

    /// <summary>
    /// Gets or sets a value indicating that the selection should be hidden when the edit control loses focus.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates that the selection should be hidden when the edit control loses focus.")]
    [DefaultValue(true)]
    public bool HideSelection
    {
        get => MaskedTextBox.HideSelection;
        set => MaskedTextBox.HideSelection = value;
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
        get => MaskedTextBox.ReadOnly;
        set => MaskedTextBox.ReadOnly = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the parsing of user input should stop after the first invalid character is reached.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"If true, the input is rejected whenever a character fails to comply with the mask; otherwise, characters in the text area are processed one by one as individual inputs.")]
    [DefaultValue(false)]
    public bool RejectInputOnFirstFailure
    {
        get => MaskedTextBox.RejectInputOnFirstFailure;
        set => MaskedTextBox.RejectInputOnFirstFailure = value;
    }

    /// <summary>
    /// Gets or sets a value that determines how an input character that matches the prompt character should be handled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies whether to reset and skip the current position if editable, when the input characters has the same value as the prompt.")]
    [DefaultValue(true)]
    public bool ResetOnPrompt
    {
        get => MaskedTextBox.ResetOnPrompt;
        set => MaskedTextBox.ResetOnPrompt = value;
    }

    /// <summary>
    /// Gets or sets a value that determines how a space input character should be handled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies whether to reset and skip the current position if editable, when the input is the space character.")]
    [DefaultValue(true)]
    public bool ResetOnSpace
    {
        get => MaskedTextBox.ResetOnSpace;
        set => MaskedTextBox.ResetOnSpace = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user is allowed to reenter literal values.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies whether to skip the current position if non-editable and the input character has the same value as the literal at that position.")]
    [DefaultValue(true)]
    public bool SkipLiterals
    {
        get => MaskedTextBox.SkipLiterals;
        set => MaskedTextBox.SkipLiterals = value;
    }

    /// <summary>
    /// Gets or sets a value that determines whether literals and prompt characters are included in the formatted string.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the string returned from the Text property includes literal and/or prompt characters.")]
    [DefaultValue(typeof(MaskFormat), "IncludeLiterals")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public MaskFormat TextMaskFormat
    {
        get => MaskedTextBox.TextMaskFormat;
        set => MaskedTextBox.TextMaskFormat = value;
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
        get => MaskedTextBox.PasswordChar;
        set => MaskedTextBox.PasswordChar = value;
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
        get => MaskedTextBox.UseSystemPasswordChar;
        set => MaskedTextBox.UseSystemPasswordChar = value;
    }

    /// <summary>
    /// Gets and sets the associated context menu strip.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The shortcut to display when the user right-clicks the control.")]
    [DefaultValue(null)]
    public ContextMenuStrip? ContextMenuStrip
    {
        get => MaskedTextBox.ContextMenuStrip;
        set => MaskedTextBox.ContextMenuStrip = value;
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu for showing when the masked textbox is right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"KryptonContextMenu to be shown when the masked textbox is right clicked.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => MaskedTextBox.KryptonContextMenu;
        set => MaskedTextBox.KryptonContextMenu = value;
    }

    /// <summary>
    /// Gets access to the Wrapped Controls Tooltips.
    /// </summary>
    public override ToolTipValues ToolTipValues => MaskedTextBox.ToolTipValues;

    /// <summary>
    /// Gets and sets a value indicating if tooltips should be Displayed for button specs.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should tooltips be Displayed for button specs.")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTips
    {
        get => MaskedTextBox.AllowButtonSpecToolTips;
        set => MaskedTextBox.AllowButtonSpecToolTips = value;
    }

    /// <summary>
    /// Gets and sets a value indicating if button spec tooltips should remove the parent tooltip.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should button spec tooltips should remove the parent tooltip")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTipPriority
    {
        get => MaskedTextBox.AllowButtonSpecToolTipPriority;
        set => MaskedTextBox.AllowButtonSpecToolTipPriority = value;
    }

    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonMaskedTextBox.MaskedTextBoxButtonSpecCollection ButtonSpecs => MaskedTextBox.ButtonSpecs;

    /// <summary>
    /// Creates an appropriate view element for this item.
    /// </summary>
    /// <param name="ribbon">Reference to the owning ribbon control.</param>
    /// <param name="needPaint">Delegate for notifying changes in display.</param>
    /// <returns>ViewBase derived instance.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override ViewBase CreateView(KryptonRibbon ribbon,
        NeedPaintHandler needPaint) =>
        new ViewDrawRibbonGroupMaskedTextBox(ribbon, this, needPaint);

    /// <summary>
    /// Gets and sets the associated designer.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public IKryptonDesignObject MaskedTextBoxDesigner { get; set; }

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase? MaskedTextBoxView { get; set; }

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
    /// Raises the ReadOnlyChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnReadOnlyChanged(EventArgs e) => ReadOnlyChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the MaskChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnMaskChanged(EventArgs e) => MaskChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TextAlignChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnTextAlignChanged(EventArgs e) => TextAlignChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the IsOverwriteModeChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnIsOverwriteModeChanged(EventArgs e) => IsOverwriteModeChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the MaskInputRejected event.
    /// </summary>
    /// <param name="e">An MaskInputRejectedEventArgs that contains the event data.</param>
    protected virtual void OnMaskInputRejected(MaskInputRejectedEventArgs e) => MaskInputRejected?.Invoke(this, e);

    /// <summary>
    /// Raises the TypeValidationCompleted event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnTypeValidationCompleted(TypeValidationEventArgs e) => TypeValidationCompleted?.Invoke(this, e);

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
    internal KryptonMaskedTextBox? LastMaskedTextBox { get; set; }

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
                    // Can the masked text box take the focus
                    if (LastMaskedTextBox is { CanFocus: true })
                    {
                        LastMaskedTextBox.MaskedTextBox.Focus();
                    }

                    return true;
                }
            }
        }

        return false;
    }
    #endregion

    #region Implementation
    private void MonitorControl(KryptonMaskedTextBox c)
    {
        c.MouseEnter += OnControlEnter;
        c.MouseLeave += OnControlLeave;
        c.TrackMouseEnter += OnControlEnter;
        c.TrackMouseLeave += OnControlLeave;
    }

    private void UnmonitorControl(KryptonMaskedTextBox c)
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

    private void OnMaskedTextBoxTextChanged(object? sender, EventArgs e) => OnTextChanged(e);

    private void OnMaskedTextBoxTextAlignChanged(object? sender, EventArgs e) => OnTextAlignChanged(e);

    private void OnMaskedMaskChanged(object? sender, EventArgs e) => OnMaskChanged(e);

    private void OnMaskedIsOverwriteModeChanged(object? sender, EventArgs e) => OnIsOverwriteModeChanged(e);

    private void OnMaskedMaskInputRejected(object? sender, MaskInputRejectedEventArgs e) => OnMaskInputRejected(e);

    private void OnMaskedTypeValidationCompleted(object? sender, TypeValidationEventArgs e) => OnTypeValidationCompleted(e);

    private void OnMaskedTextBoxHideSelectionChanged(object? sender, EventArgs e) => OnHideSelectionChanged(e);

    private void OnMaskedTextBoxModifiedChanged(object? sender, EventArgs e) => OnModifiedChanged(e);

    private void OnMaskedTextBoxReadOnlyChanged(object? sender, EventArgs e) => OnReadOnlyChanged(e);

    private void OnMaskedTextBoxGotFocus(object? sender, EventArgs e) => OnGotFocus(e);

    private void OnMaskedTextBoxLostFocus(object? sender, EventArgs e) => OnLostFocus(e);

    private void OnMaskedTextBoxKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnMaskedTextBoxKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnMaskedTextBoxKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnMaskedTextBoxPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnRibbonPaletteChanged(object? sender, EventArgs e)
    {
        MaskedTextBox.PaletteMode = Ribbon!.PaletteMode;
        MaskedTextBox.LocalCustomPalette = Ribbon!.LocalCustomPalette;
    }
    #endregion
}