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
/// Combines color button functionality with the styling features of the Krypton Toolkit.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonColorButton), "ToolboxBitmaps.KryptonColorButton.bmp")]
[DefaultEvent(nameof(SelectedColorChanged))]
[DefaultProperty(nameof(SelectedColor))]
[Designer(typeof(KryptonColorButtonDesigner))]
[DesignerCategory(@"code")]
[Description(@"Raises an event when the user clicks it.")]
public class KryptonColorButton : VisualSimpleBase, IButtonControl, IContentValues
{
    #region Instance Fields
    private readonly ViewDrawButton _drawButton;
    private ButtonStyle _style;
    private readonly ButtonController _buttonController;
    private readonly PaletteTripleOverride _overrideFocus;
    private readonly PaletteTripleOverride _overrideNormal;
    private readonly PaletteTripleOverride _overrideTracking;
    private readonly PaletteTripleOverride _overridePressed;
    private KryptonCommand? _command;
    private Rectangle _selectedRect;
    private Color _selectedColor;
    private Color _emptyBorderColor;
    private readonly List<Color> _recentColors;
    private Image? _wasImage;
    private bool _wasEnabled;
    private bool _isDefault;
    private bool _useMnemonic;
    private bool _allowFullOpen;
    private bool _clickOverriden;
    private KryptonColorButtonCustomColorPreviewShape _customColorPreviewShape;

    // Context menu items
    private readonly KryptonContextMenu? _kryptonContextMenu;
    private readonly KryptonContextMenuSeparator _separatorTheme;
    private readonly KryptonContextMenuSeparator _separatorStandard;
    private readonly KryptonContextMenuSeparator _separatorRecent;
    private readonly KryptonContextMenuHeading _headingTheme;
    private readonly KryptonContextMenuHeading _headingStandard;
    private readonly KryptonContextMenuHeading _headingRecent;
    private readonly KryptonContextMenuColorColumns _colorsTheme;
    private readonly KryptonContextMenuColorColumns _colorsStandard;
    private readonly KryptonContextMenuColorColumns _colorsRecent;
    private readonly KryptonContextMenuSeparator _separatorNoColor;
    private readonly KryptonContextMenuItems _itemsNoColor;
    private readonly KryptonContextMenuItem _itemNoColor;
    private readonly KryptonContextMenuSeparator _separatorMoreColors;
    private readonly KryptonContextMenuItems _itemsMoreColors;
    private readonly KryptonContextMenuItem _itemMoreColors;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the drop-down portion of the color button is pressed.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the drop-down portion of the color button is pressed.")]
    public event EventHandler<ContextPositionMenuArgs>? DropDown;

    /// <summary>
    /// Occurs when the value of the KryptonCommand property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the KryptonCommand property changes.")]
    public event EventHandler? KryptonCommandChanged;

    /// <summary>
    /// Occurs when the SelectedColor property changes value.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the SelectedColor property changes value.")]
    public event EventHandler<ColorEventArgs>? SelectedColorChanged;

    /// <summary>
    /// Occurs when the user is tracking over a color.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when user is tracking over a color.")]
    public event EventHandler<ColorEventArgs>? TrackingColor;

    /// <summary>
    /// Occurs when the user selects the more colors option.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when user selects the more colors option.")]
    public event CancelEventHandler? MoreColors;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonColorButton class.
    /// </summary>
    public KryptonColorButton()
    {
        // We generate click events manually, suppress default
        // production of them by the base Control class
        SetStyle(ControlStyles.StandardClick |
                 ControlStyles.StandardDoubleClick, false);

        // Set default color button properties
        _style = ButtonStyle.Standalone;
        VisibleThemes = true;
        VisibleStandard = true;
        VisibleRecent = true;
        VisibleNoColor = true;
        VisibleMoreColors = true;
        AutoRecentColors = true;
        SchemeThemes = ColorScheme.OfficeThemes;
        SchemeStandard = ColorScheme.OfficeStandard;
        _selectedRect = new Rectangle(0, 12, 16, 4);
        _selectedColor = Color.Red;
        _emptyBorderColor = Color.DarkGray;
        DialogResult = DialogResult.None;
        _useMnemonic = true;
        MaxRecentColors = 10;
        _recentColors = [];
        _allowFullOpen = true;

        // Create the context menu items
        _kryptonContextMenu = new KryptonContextMenu();
        _separatorTheme = new KryptonContextMenuSeparator();
        _headingTheme = new KryptonContextMenuHeading(KryptonManager.Strings.ColorStrings.ThemeColors); //@"Theme Colors");
        _colorsTheme = new KryptonContextMenuColorColumns(ColorScheme.OfficeThemes);
        _separatorStandard = new KryptonContextMenuSeparator();
        _headingStandard = new KryptonContextMenuHeading(KryptonManager.Strings.ColorStrings.StandardColors); //@"Standard Colors");
        _colorsStandard = new KryptonContextMenuColorColumns(ColorScheme.OfficeStandard);
        _separatorRecent = new KryptonContextMenuSeparator();
        _headingRecent = new KryptonContextMenuHeading(KryptonManager.Strings.ColorStrings.RecentColors); //@"Recent Colors");
        _colorsRecent = new KryptonContextMenuColorColumns(ColorScheme.None);
        _separatorNoColor = new KryptonContextMenuSeparator();
        _itemNoColor = new KryptonContextMenuItem(/*@"&No Color"*/ KryptonManager.Strings.ColorStrings.NoColor, GenericImageResources.ButtonNoColor, OnClickNoColor);
        _itemsNoColor = new KryptonContextMenuItems();
        _itemsNoColor.Items.Add(_itemNoColor);
        _separatorMoreColors = new KryptonContextMenuSeparator();
        _itemMoreColors = new KryptonContextMenuItem(/*@"&More Colors..."*/ KryptonManager.Strings.ColorStrings.MoreColors, OnClickMoreColors);
        _itemsMoreColors = new KryptonContextMenuItems();
        _itemsMoreColors.Items.Add(_itemMoreColors);
        _kryptonContextMenu.Items.AddRange([
            _separatorTheme, _headingTheme, _colorsTheme,
            _separatorStandard, _headingStandard, _colorsStandard,
            _separatorRecent, _headingRecent, _colorsRecent,
            _separatorNoColor, _itemsNoColor,
            _separatorMoreColors, _itemsMoreColors
        ]);

        // Create content storage
        Values = CreateButtonValues(NeedPaintDelegate);
        Values.TextChanged += OnButtonTextChanged;

        // Create the palette storage
        Strings = new PaletteColorButtonStrings();
        StateCommon = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);
        StateDisabled = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StateTracking = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StatePressed = new PaletteTriple(StateCommon, NeedPaintDelegate);
        OverrideDefault = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);
        OverrideFocus = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);

        // Create the override handling classes
        _overrideFocus = new PaletteTripleOverride(OverrideFocus, StateNormal, PaletteState.FocusOverride);
        _overrideNormal = new PaletteTripleOverride(OverrideDefault, _overrideFocus, PaletteState.NormalDefaultOverride);
        _overrideTracking = new PaletteTripleOverride(OverrideFocus, StateTracking, PaletteState.FocusOverride);
        _overridePressed = new PaletteTripleOverride(OverrideFocus, StatePressed, PaletteState.FocusOverride);

        // Create the view color button instance
        _drawButton = new ViewDrawButton(StateDisabled,
            _overrideNormal,
            _overrideTracking,
            _overridePressed,
            new PaletteMetricRedirect(Redirector),
            this,
            VisualOrientation.Top,
            UseMnemonic)
        {

            // Set default color button state
            DropDown = true,
            Splitter = true,
            TestForFocusCues = true
        };

        // Create a color button controller to handle button style behaviour
        _buttonController = new ButtonController(_drawButton, NeedPaintDelegate)
        {
            BecomesFixed = true
        };

        // Assign the controller to the view element to treat as a button
        _drawButton.MouseController = _buttonController;
        _drawButton.KeyController = _buttonController;
        _drawButton.SourceController = _buttonController;

        // Need to know when user clicks the button view or mouse selects it
        _buttonController.Click += OnButtonClick;
        _buttonController.MouseSelect += OnButtonSelect;

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawButton);

        CustomColorPreviewShape = KryptonColorButtonCustomColorPreviewShape.None;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the automatic resize of the control to fit contents.
    /// </summary>
    [Browsable(true)]
    [Localizable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RefreshProperties(RefreshProperties.All)]
    public override bool AutoSize
    {
        get => base.AutoSize;
        set => base.AutoSize = value;
    }

    /// <summary>
    /// Gets and sets the internal padding space.
    /// </summary>
    [Browsable(false)]
    [Localizable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Padding Padding
    {
        get => base.Padding;
        set => base.Padding = value;
    }

    /// <summary>
    /// Allows the developer to add their action
    /// </summary>
    [Category(@"Action")]
    [Description(@"Override to allow your click event")]
    public new event EventHandler? Click
    {
        add
        {
            _clickOverriden = true;
            _drawButton.DropDown = false;
            Splitter = false;
            base.Click += value;
        }
        remove
        {
            _clickOverriden = false;
            base.Click -= value;
        }
    }

    /// <summary>
    /// Gets or sets the text associated with this control. 
    /// </summary>
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [AllowNull]
    public override string Text
    {
        get => Values.Text;

        set => Values.Text = value;
    }

    private bool ShouldSerializeText() =>
        // Never serialize, let the color button values serialize instead
        false;

    /// <summary>
    /// Resets the Text property to its default value.
    /// </summary>
    public override void ResetText() =>
        // Map onto the color button property from the values
        Values.ResetText();

    /// <summary>
    /// Gets or sets the ContextMenuStrip associated with this control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ContextMenuStrip? ContextMenuStrip
    {
        get => null;
        set { }
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu to show when right clicked.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override KryptonContextMenu? KryptonContextMenu
    {
        get => null;
        set { }
    }

    /// <summary>
    /// Gets and sets the maximum number of recent colors to store and display.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determine the maximum number of recent colors to store and display.")]
    [DefaultValue(10)]
    public int MaxRecentColors { get; set; }

    /// <summary>
    /// Gets and sets the visible state of the themes color set.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determine the visible state of the themes color set.")]
    [DefaultValue(true)]
    public bool VisibleThemes { get; set; }

    /// <summary>
    /// Gets and sets the visible state of the standard color set.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determine the visible state of the standard color set.")]
    [DefaultValue(true)]
    public bool VisibleStandard { get; set; }

    /// <summary>
    /// Gets and sets the visible state of the recent color set.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determine the visible state of the recent color set.")]
    [DefaultValue(true)]
    public bool VisibleRecent { get; set; }

    /// <summary>
    /// Gets and sets the visible state of the no color menu item.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determine if the 'No Color' menu item is used.")]
    [DefaultValue(true)]
    public bool VisibleNoColor { get; set; }

    /// <summary>
    /// Gets and sets the visible state of the more colors menu item.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determine if the 'More Colors...' menu item is used.")]
    [DefaultValue(true)]
    public bool VisibleMoreColors { get; set; }

    /// <summary>
    /// Gets and sets if the recent colors should be automatically updated.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Should recent colors be automatically updated.")]
    [DefaultValue(true)]
    public bool AutoRecentColors { get; set; }

    /// <summary>
    /// Gets and sets the color scheme for the themes color set.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Color scheme to use for the themes color set.")]
    [DefaultValue(ColorScheme.OfficeThemes)]
    public ColorScheme SchemeThemes { get; set; }

    /// <summary>
    /// Gets and sets the color scheme for the standard color set.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Color scheme to use for the standard color set.")]
    [DefaultValue(ColorScheme.OfficeStandard)]
    public ColorScheme SchemeStandard { get; set; }

    /// <summary>
    /// Gets and sets the selected color.
    /// </summary>
    [Bindable(true)]
    [Category(@"Appearance")]
    [Description(@"Selected color.")]
    [DefaultValue(typeof(Color), "Red")]
    public Color SelectedColor
    {
        get => _selectedColor;

        set
        {
            if (value != _selectedColor)
            {
                _selectedColor = value;
                Values.SelectedColor = value;
                UpdateRecentColors(_selectedColor);
                OnSelectedColorChanged(_selectedColor);
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the selected color block when selected color is empty.
    /// </summary>
    [Bindable(true)]
    [Category(@"Appearance")]
    [Description(@"Border color of selected block when selected color is empty.")]
    [DefaultValue(typeof(Color), "DarkGray")]
    public Color EmptyBorderColor
    {
        get => _emptyBorderColor;

        set
        {
            if (value != _emptyBorderColor)
            {
                _emptyBorderColor = value;
                Values.EmptyBorderColor = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the selected color drawing rectangle.
    /// </summary>
    [Bindable(true)]
    [Category(@"Appearance")]
    [Description(@"Selected color drawing rectangle.")]
    [DefaultValue(typeof(Rectangle), "0,12,16,4")]
    public Rectangle SelectedRect
    {
        get => _selectedRect;

        set
        {
            _selectedRect = value;
            Values.SelectedRect = value;
            PerformNeedPaint(true);
        }
    }


    /// <summary>
    /// Gets and sets the visual orientation of the control.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Visual orientation of the control.")]
    [DefaultValue(VisualOrientation.Top)]
    public virtual VisualOrientation ButtonOrientation
    {
        get => _drawButton.Orientation;

        set
        {
            if (_drawButton.Orientation != value)
            {
                _drawButton.Orientation = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the position of the drop arrow within the color button.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Position of the drop arrow within the color button.")]
    [DefaultValue(VisualOrientation.Right)]
    public virtual VisualOrientation DropDownPosition
    {
        get => _drawButton.DropDownPosition;

        set
        {
            if (_drawButton.DropDownPosition != value)
            {
                _drawButton.DropDownPosition = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the orientation of the drop arrow within the color button.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Orientation of the drop arrow within the color button.")]
    [DefaultValue(VisualOrientation.Bottom)]
    public virtual VisualOrientation DropDownOrientation
    {
        get
        {
            return _drawButton.DropDownOrientation switch
            {
                VisualOrientation.Top => VisualOrientation.Bottom,
                VisualOrientation.Bottom => VisualOrientation.Top,
                VisualOrientation.Left => VisualOrientation.Right,
                VisualOrientation.Right => VisualOrientation.Left,
                _ => VisualOrientation.Bottom
            };
        }

        set
        {
            VisualOrientation converted = value switch
            {
                VisualOrientation.Bottom => VisualOrientation.Top,
                VisualOrientation.Top => VisualOrientation.Bottom,
                VisualOrientation.Right => VisualOrientation.Left,
                VisualOrientation.Left => VisualOrientation.Right,
                _ => VisualOrientation.Top
            };

            if (_drawButton.DropDownOrientation != converted)
            {
                _drawButton.DropDownOrientation = converted;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets if the color button works as a splitter or as a drop-down.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Determine if color button acts as a splitter or just a drop-down.")]
    [DefaultValue(true)]
    public virtual bool Splitter
    {
        get => _drawButton.Splitter;

        set
        {
            if (_drawButton.Splitter != value)
            {
                _drawButton.Splitter = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the color button style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color button style.")]
    public ButtonStyle ButtonStyle
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                SetStyles(_style);
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeButtonStyle() => ButtonStyle != ButtonStyle.Standalone;

    private void ResetButtonStyle() => ButtonStyle = ButtonStyle.Standalone;

    /// <summary>
    /// Gets access to the color button content.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color button values")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ColorButtonValues Values { get; }

    private bool ShouldSerializeValues() => !Values.IsDefault;

    /// <summary>
    /// Gets access to the context menu display strings.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Context menu display strings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteColorButtonStrings Strings { get; }

    /// <summary>
    /// Gets access to the common color button appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common color button appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled color button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled color button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal color button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal color button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the hot tracking color button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking color button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed color button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed color button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    /// <summary>
    /// Gets access to the normal color button appearance when default.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal color button appearance when default.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect OverrideDefault { get; }

    private bool ShouldSerializeOverrideDefault() => !OverrideDefault.IsDefault;

    /// <summary>
    /// Gets access to the color button appearance when it has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining color button appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    /// <summary>
    /// Gets or sets the value returned to the parent form when the color button is clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The dialog-box result produced in a modal form by clicking the color button.")]
    [DefaultValue(DialogResult.None)]
    public DialogResult DialogResult { get; set; }

    /// <summary>
    /// Gets and sets the associated KryptonCommand.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Command associated with the color button.")]
    [DefaultValue(null)]
    public virtual KryptonCommand? KryptonCommand
    {
        get => _command;

        set
        {
            if (_command != value)
            {
                if (_command != null)
                {
                    _command.PropertyChanged -= OnCommandPropertyChanged;
                }
                else
                {
                    _wasEnabled = Enabled;
                    _wasImage = Values.Image;
                }

                _command = value;
                OnKryptonCommandChanged(EventArgs.Empty);

                if (_command != null)
                {
                    _command.PropertyChanged += OnCommandPropertyChanged;
                }
                else
                {
                    Enabled = _wasEnabled;
                    Values.Image = _wasImage;
                }
            }
        }
    }

    /// <summary>
    /// Notifies a control that it is the default color button so that its appearance and behavior is adjusted accordingly. 
    /// </summary>
    /// <param name="value">true if the control should behave as a default color button; otherwise false.</param>
    public void NotifyDefault(bool value)
    {
        if (!ViewDrawButton.IsFixed && (_isDefault != value))
        {
            // Remember new default status
            _isDefault = value;

            // Decide if the default overrides should be applied
            _overrideNormal.Apply = value;

            // Change in deault state requires a layout and repaint
            PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Generates a Click event for the control.
    /// </summary>
    public void PerformClick()
    {
        if (CanSelect)
        {
            OnClick(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Generates a DropDown event for the control.
    /// </summary>
    public void PerformDropDown()
    {
        if (CanSelect)
        {
            ShowDropDown();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether an ampersand is included in the text of the control. 
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"When true the first character after an ampersand will be used as a mnemonic.")]
    [DefaultValue(true)]
    public bool UseMnemonic
    {
        get => _useMnemonic;

        set
        {
            if (_useMnemonic != value)
            {
                _useMnemonic = value;
                _drawButton.UseMnemonic = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state)
    {
        if (state == PaletteState.NormalDefaultOverride)
        {
            // Setup the overrides correctly to match state
            _overrideFocus.Apply = true;
            _overrideNormal.Apply = true;

            // Must pass a proper drawing state to the view
            state = PaletteState.Normal;
        }

        // Request fixed state from the view
        _drawButton.FixedState = state;
    }

    /// <summary>
    /// Determines the IME status of the object when selected.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ImeMode ImeMode
    {
        get => base.ImeMode;
        set => base.ImeMode = value;
    }

    /// <summary>Full color dialog.</summary>
    [DefaultValue(true), Description(@"Full color dialog.")]
    public bool AllowFullOpen
    {
        get => _allowFullOpen;

        set => _allowFullOpen = value;
    }

    /// <summary>
    /// Allows the configuration of a custom colour preview shape.
    /// </summary>
    [DefaultValue(KryptonColorButtonCustomColorPreviewShape.None)]
    [Description(@"Allows the configuration of a custom colour preview shape.")]
    public KryptonColorButtonCustomColorPreviewShape CustomColorPreviewShape
    {
        get => _customColorPreviewShape;

        set
        {
            if (_customColorPreviewShape != value)
            {
                _customColorPreviewShape = value;

                SetCustomColorPreviewShape(value);

                PerformNeedPaint(true);
            }
        }
    }
    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => KryptonCommand?.Text ?? Values.GetShortText();

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => KryptonCommand?.ExtraText ?? Values.GetLongText();

    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state) => Values.GetImage(state);

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) =>
        KryptonCommand?.ImageTransparentColor ?? Values.GetImageTransparentColor(state);

    #endregion

    #region Protected Overrides
    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(90, 25);

    /// <summary>
    /// Gets the default Input Method Editor (IME) mode supported by this control.
    /// </summary>
    protected override ImeMode DefaultImeMode => ImeMode.Disable;

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Must inform the color button view which itself tells the embedded elements
        _drawButton.Enabled = Enabled;

        // Change in enabled state requires a layout and repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
        if (!ViewDrawButton.IsFixed)
        {
            // Apply the focus overrides
            _overrideFocus.Apply = true;
            _overrideTracking.Apply = true;
            _overridePressed.Apply = true;

            // Change in focus requires a repaint
            PerformNeedPaint(false);
        }

        // Let base class fire standard event
        base.OnGotFocus(e);
    }

    /// <summary>
    /// Raises the LostFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnLostFocus(EventArgs e)
    {
        if (!ViewDrawButton.IsFixed)
        {
            // Apply the focus overrides
            _overrideFocus.Apply = false;
            _overrideTracking.Apply = false;
            _overridePressed.Apply = false;

            // Change in focus requires a repaint
            PerformNeedPaint(false);
        }

        // Let base class fire standard event
        base.OnLostFocus(e);
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnClick(EventArgs e)
    {
        // Find the form this color button is on
        Form? owner = FindForm();

        // If we find a valid owner
        if (owner != null)
        {
            // Update owner with our dialog result setting
            owner.DialogResult = DialogResult;
        }

        // Let base class fire standard event
        base.OnClick(e);

        // If we have an attached command then execute it
        KryptonCommand?.PerformExecute();
    }

    /// <summary>
    /// Processes a mnemonic character.
    /// </summary>
    /// <param name="charCode">The mnemonic character entered.</param>
    /// <returns>true if the mnemonic was processed; otherwise, false.</returns>
    protected override bool ProcessMnemonic(char charCode)
    {
        // Are we allowed to process mnemonics?
        if (UseMnemonic && CanProcessMnemonic())
        {
            // Does the color button primary text contain the mnemonic?
            if (IsMnemonic(charCode, Values.Text))
            {
                if (!Splitter)
                {
                    PerformDropDown();
                }
                else
                {
                    PerformClick();
                }

                return true;
            }
        }

        // No match found, let base class do standard processing
        return base.ProcessMnemonic(charCode);
    }

    /// <summary>
    /// Called when a context menu has just been closed.
    /// </summary>
    protected override void ContextMenuClosed() => _buttonController.RemoveFixed();

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        // Prevent base class from showing a context menu when right clicking it
        if (m.Msg != PI.WM_.CONTEXTMENU)
        {
            base.WndProc(ref m);
        }
    }

    // <summary>Raises the Paint event.</summary>
    // <param name="e">A PaintEventArgs that contains the event data.</param>
    //protected override void OnPaint(PaintEventArgs e)
    //{
    //    if (_useCustomPreviewShape)
    //    {
    //        Values.Image = GenericImageResources.Transparent_16_x_16;

    //        Values.RoundedCorners = 8;

    //        Values.SelectedRect = new Rectangle(0, 0, 16, 16);
    //    }
    //    else
    //    {
    //        Values.Image = GenericImageResources.ButtonColorImageSmall;

    //        Values.RoundedCorners = 0;

    //        Values.SelectedRect = new Rectangle(0, 12, 16, 4);
    //    }

    //    base.OnPaint(e);
    //}

    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the DropDown event.
    /// </summary>
    /// <param name="e">An ContextPositionMenuArgs containing the event data.</param>
    protected virtual void OnDropDown(ContextPositionMenuArgs e) => DropDown?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectedColorChanged event.
    /// </summary>
    /// <param name="selectedColor">New selected color.</param>
    protected virtual void OnSelectedColorChanged(Color selectedColor) => SelectedColorChanged?.Invoke(this, new ColorEventArgs(selectedColor));

    /// <summary>
    /// Raises the TrackingColor event.
    /// </summary>
    /// <param name="e">An ColorEventArgs that contains the event data.</param>
    protected virtual void OnTrackingColor(ColorEventArgs e) => TrackingColor?.Invoke(this, e);

    /// <summary>
    /// Raises the MoreColors event.
    /// </summary>
    /// <param name="e">An CancelEventArgs that contains the event data.</param>
    protected virtual void OnMoreColors(CancelEventArgs e) => MoreColors?.Invoke(this, e);

    /// <summary>
    /// Raises the KryptonCommandChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnKryptonCommandChanged(EventArgs e)
    {
        KryptonCommandChanged?.Invoke(this, e);

        // Use the values from the new command
        if (KryptonCommand != null)
        {
            Enabled = KryptonCommand.Enabled;
            Values.Image = KryptonCommand.ImageSmall;
        }

        // Redraw to update the text/extratext/image properties
        PerformNeedPaint(true);
    }

    /// <summary>
    /// Handles a change in the property of an attached command.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
    protected virtual void OnCommandPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Enabled):
                Enabled = KryptonCommand!.Enabled;
                break;
            case @"ImageSmall":
                Values.Image = KryptonCommand!.ImageSmall;
                PerformNeedPaint(true);
                break;
            case nameof(Text):
            case @"ExtraText":
            case @"ImageTransparentColor":
                PerformNeedPaint(true);
                break;
        }
    }

    /// <summary>
    /// Update the state objects to reflect the new color button style.
    /// </summary>
    /// <param name="buttonStyle">New color button style.</param>
    protected virtual void SetStyles(ButtonStyle buttonStyle)
    {
        StateCommon.SetStyles(buttonStyle);
        OverrideDefault.SetStyles(buttonStyle);
        OverrideFocus.SetStyles(buttonStyle);
    }

    /// <summary>
    /// Creates a values storage object appropriate for control.
    /// </summary>
    /// <returns>Set of color button values.</returns>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    protected virtual ColorButtonValues CreateButtonValues(NeedPaintHandler needPaint) =>
        new ColorButtonValues(needPaint);

    /// <summary>
    /// Gets access to the view element for the color button.
    /// </summary>
    protected virtual ViewDrawButton ViewDrawButton => _drawButton;
    #endregion

    #region Implementation
    private void OnButtonTextChanged(object? sender, EventArgs e) => OnTextChanged(EventArgs.Empty);

    private void OnButtonClick(object? sender, MouseEventArgs e)
    {
        var showingContextMenu = false;

        // Do we need to show a drop-down menu?
        if (!_clickOverriden
            && (!Splitter || (Splitter && _drawButton.SplitRectangle.Contains(e.Location)))
           )
        {
            showingContextMenu = ShowDropDown();
        }
        else
        {
            // Raise the standard click event
            OnClick(EventArgs.Empty);

            // Raise event to indicate it was a mouse activated click
            OnMouseClick(e);
        }

        // If not showing a context menu then perform cleanup straight away
        if (!showingContextMenu)
        {
            ContextMenuClosed();
        }
    }

    private bool ShowDropDown()
    {
        var showingContextMenu = false;

        // Update the context menu state
        UpdateContextMenu();

        // Update the krypton menu with this controls palette state
        if (_kryptonContextMenu != null)
        {
            if (PaletteMode != PaletteMode.Custom)
            {
                _kryptonContextMenu.PaletteMode = PaletteMode;
            }
            else
            {
                _kryptonContextMenu.LocalCustomPalette = LocalCustomPalette;
            }
        }

        // Package up the context menu and positioning values we will use later
        var cpma = new ContextPositionMenuArgs(null,
            _kryptonContextMenu, GetPositionH(), GetPositionV());
        // Let use examine and later values
        OnDropDown(cpma);

        // If we still want to show a context menu
        if (cpma is { Cancel: false, KryptonContextMenu: not null })
        {
            // Convert the client rect to screen coords
            Rectangle screenRect = RectangleToScreen(ClientRectangle);
            if (CommonHelper.ValidKryptonContextMenu(cpma.KryptonContextMenu))
            {
                // Modify the screen rect so that we have a pixel gap between color button and menu
                switch (cpma.PositionV)
                {
                    case KryptonContextMenuPositionV.Above:
                        screenRect.Y -= 1;
                        break;
                    case KryptonContextMenuPositionV.Below:
                        screenRect.Height += 1;
                        break;
                }

                switch (cpma.PositionH)
                {
                    case KryptonContextMenuPositionH.Before:
                        screenRect.X -= 1;
                        break;
                    case KryptonContextMenuPositionH.After:
                        screenRect.Width += 1;
                        break;
                }

                // We are showing a drop-down
                showingContextMenu = true;

                // Decide which separators are needed
                DecideOnVisible(_separatorTheme, _colorsTheme);
                DecideOnVisible(_separatorStandard, _colorsStandard);
                DecideOnVisible(_separatorRecent, _colorsRecent);
                DecideOnVisible(_separatorNoColor, _itemsNoColor);
                DecideOnVisible(_separatorMoreColors, _itemsMoreColors);

                // Monitor relevant events inside the context menu
                HookContextMenuEvents(_kryptonContextMenu!.Items, true);

                // Show relative to the screen rectangle
                cpma.KryptonContextMenu.Closed += OnKryptonContextMenuClosed;
                cpma.KryptonContextMenu.Show(this, screenRect, cpma.PositionH, cpma.PositionV);
            }
        }

        return showingContextMenu;
    }

    private KryptonContextMenuPositionH GetPositionH() => DropDownOrientation switch
    {
        //VisualOrientation.Bottom => KryptonContextMenuPositionH.Left,
        //VisualOrientation.Top => KryptonContextMenuPositionH.Left,
        VisualOrientation.Left => KryptonContextMenuPositionH.Before,
        VisualOrientation.Right => KryptonContextMenuPositionH.After,
        _ => KryptonContextMenuPositionH.Left
    };

    private KryptonContextMenuPositionV GetPositionV() => DropDownOrientation switch
    {
        //VisualOrientation.Bottom => KryptonContextMenuPositionV.Below,
        VisualOrientation.Top => KryptonContextMenuPositionV.Above,
        VisualOrientation.Left => KryptonContextMenuPositionV.Top,
        VisualOrientation.Right => KryptonContextMenuPositionV.Top,
        _ => KryptonContextMenuPositionV.Below
    };

    private void OnContextMenuClosed(object sender, EventArgs e) => ContextMenuClosed();

    private void OnKryptonContextMenuClosed(object? sender, EventArgs e)
    {
        var kcm = sender as KryptonContextMenu ?? throw new ArgumentNullException(nameof(sender));
        kcm.Closed -= OnKryptonContextMenuClosed;
        ContextMenuClosed();

        // Unhook from item events
        HookContextMenuEvents(_kryptonContextMenu!.Items, false);
    }

    private void OnButtonSelect(object? sender, MouseEventArgs e)
    {
        // Take the focus if allowed
        if (CanFocus)
        {
            Focus();
        }
    }

    private void HookContextMenuEvents(KryptonContextMenuCollection collection, bool hook)
    {
        // Search for items of interest
        foreach (KryptonContextMenuItemBase item in collection)
        {
            // Hook into color events
            if (item is KryptonContextMenuColorColumns columns)
            {
                columns.SelectedColor = _selectedColor;

                if (hook)
                {
                    columns.TrackingColor += OnColumnsTrackingColor;
                    columns.SelectedColorChanged += OnColumnsSelectedColorChanged;
                }
                else
                {
                    columns.TrackingColor -= OnColumnsTrackingColor;
                    columns.SelectedColorChanged -= OnColumnsSelectedColorChanged;
                }
            }
        }
    }

    /// <summary>
    /// Add or update the recent colours to display.
    /// Notes:
    ///   - If number to display > MaxRecentColors(10) then the earliest ones will be removed
    ///   - Colours will appear in reverse order to those passed in. 
    /// </summary>
    /// <param name="colors"></param>
    public void AddUpdateRecentColors(IList<Color> colors)
    {
        foreach (Color color in colors
                     .Where(static color => (color != null) && !color.Equals(GlobalStaticValues.EMPTY_COLOR))
                     .Where(color => !Enumerable.Contains(_recentColors, color)))
        {
            // Add to start of the list
            _recentColors.Insert(0, color);
        }

        // Enforce the maximum number of recent colors
        if (_recentColors.Count > MaxRecentColors)
        {
            _recentColors.RemoveRange(MaxRecentColors, _recentColors.Count - MaxRecentColors);
        }
    }

    private void UpdateRecentColors(Color color)
    {
        // Do we need to update the recent colors collection?
        if (AutoRecentColors)
        {
            // We do not add to recent colors if it is inside another color columns 
            foreach (KryptonContextMenuItemBase item in _kryptonContextMenu!.Items)
            {
                // Only interested in the non-recent colors, color columns
                if ((item != _colorsRecent) && (item is KryptonContextMenuColorColumns colors))
                {
                    // Cast to correct type

                    // We do not change the theme or standard entries if they are not to be used
                    if (((item == _colorsTheme) && !VisibleThemes) ||
                        ((item == _colorsStandard) && !VisibleStandard))
                    {
                        continue;
                    }

                    // If matching color found, do not add to recent colors
                    if (colors.ContainsColor(color))
                    {
                        return;
                    }
                }
            }

            // If this color valid and so possible to become a recent color
            if ((color != null) && !color.Equals(GlobalStaticValues.EMPTY_COLOR))
            {
                var found = false;
                foreach (Color recentColor in _recentColors)
                {
                    if (recentColor.Equals(color))
                    {
                        found = true;
                        break;
                    }
                }

                // If the color is not already part of the recent colors
                if (!found)
                {
                    // Add to start of the list
                    _recentColors.Insert(0, color);

                    // Enforce the maximum number of recent colors
                    if (_recentColors.Count > MaxRecentColors)
                    {
                        _recentColors.RemoveRange(MaxRecentColors, _recentColors.Count - MaxRecentColors);
                    }
                }
            }
        }
    }

    private void UpdateContextMenu()
    {
        // Update visible state based of properties
        _separatorTheme.Visible = _headingTheme.Visible = _colorsTheme.Visible = VisibleThemes;
        _separatorStandard.Visible = _headingStandard.Visible = _colorsStandard.Visible = VisibleStandard;
        _separatorRecent.Visible = _headingRecent.Visible = _colorsRecent.Visible = VisibleRecent && (_recentColors.Count > 0);
        _itemsNoColor.Visible = VisibleNoColor;
        _itemsMoreColors.Visible = VisibleMoreColors;

        // Define the display strings
        _headingTheme.Text = Strings.ThemeColors;
        _headingStandard.Text = Strings.StandardColors;
        _headingRecent.Text = Strings.RecentColors;
        _itemNoColor.Text = Strings.NoColor;
        _itemMoreColors.Text = Strings.MoreColors;

        // Define the colors used in the first two color schemes
        _colorsTheme.ColorScheme = SchemeThemes;
        _colorsStandard.ColorScheme = SchemeStandard;

        // Define the recent colors
        if (_recentColors.Count == 0)
        {
            _colorsRecent.SetCustomColors(null);
        }
        else
        {
            // Create an array of color arrays
            var colors = new Color[_recentColors.Count][];

            // Each column is just a single color
            for (var i = 0; i < _recentColors.Count; i++)
            {
                colors[i] = [_recentColors[i]];
            }

            _colorsRecent.SetCustomColors(colors);
        }

        // Should the no color entry be checked?
        _itemNoColor.Checked = _selectedColor.Equals(GlobalStaticValues.EMPTY_COLOR);
    }

    private void DecideOnVisible(KryptonContextMenuItemBase visible, KryptonContextMenuItemBase target)
    {
        var previous = false;

        // Only search if the target itself is visible
        if (target.Visible)
        {
            // Check all items before the target
            foreach (KryptonContextMenuItemBase item in _kryptonContextMenu!.Items)
            {
                // Finish when we reach the target
                if (item == target)
                {
                    break;
                }

                // We do not consider existing separators
                if (!(item is KryptonContextMenuSeparator or KryptonContextMenuHeading))
                {
                    // If the previous item is visible, then make the parameter visible
                    if (item.Visible)
                    {
                        previous = true;
                        break;
                    }
                }
            }
        }

        visible.Visible = previous;
    }

    private void OnColumnsTrackingColor(object? sender, ColorEventArgs e) => OnTrackingColor(new ColorEventArgs(e.Color));

    private void OnColumnsSelectedColorChanged(object? sender, ColorEventArgs e) => SelectedColor = e.Color;

    private void OnClickNoColor(object? sender, EventArgs e) => SelectedColor = GlobalStaticValues.EMPTY_COLOR;

    private void OnClickMoreColors(object? sender, EventArgs e)
    {
        // Give user a chance to cancel showing the Krypton more colors dialog
        var cea = new CancelEventArgs();
        OnMoreColors(cea);

        // If not instructed to cancel then...
        if (!cea.Cancel)
        {
            // Use a Krypton color dialog for the selection of custom colors
            var cd = new KryptonColorDialog
            {
                Color = SelectedColor,
                FullOpen = _allowFullOpen
            };

            // Only if user selected a value do we want to use it
            if (cd.ShowDialog() == DialogResult.OK)
            {
                SelectedColor = cd.Color;
            }
        }
    }

    private void SetCustomColorPreviewShape(KryptonColorButtonCustomColorPreviewShape customShape)
    {
        switch (customShape)
        {
            case KryptonColorButtonCustomColorPreviewShape.None:
                Values.Image = GenericImageResources.ButtonColorImageSmall;

                Values.RoundedCorners = 0;

                Values.SelectedRect = new Rectangle(0, 12, 16, 4);
                break;
            case KryptonColorButtonCustomColorPreviewShape.Circle:
                Values.Image = GenericImageResources.Transparent_16_x_16;

                Values.RoundedCorners = 8;

                Values.SelectedRect = new Rectangle(0, 0, 16, 16);
                break;
            case KryptonColorButtonCustomColorPreviewShape.RoundedSquare:
                Values.Image = GenericImageResources.Transparent_16_x_16;

                Values.RoundedCorners = 2;

                Values.SelectedRect = new Rectangle(0, 0, 16, 16);
                break;
            case KryptonColorButtonCustomColorPreviewShape.Square:
                Values.Image = GenericImageResources.Transparent_16_x_16;

                Values.RoundedCorners = 0;

                Values.SelectedRect = new Rectangle(0, 0, 16, 16);
                break;

        }
    }
    #endregion
}