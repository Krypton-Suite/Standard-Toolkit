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

// ReSharper disable MemberCanBeInternal
// ReSharper disable MemberCanBeProtected.Global

namespace Krypton.Toolkit;

/// <summary>
/// Specification for a button.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[ToolboxBitmap(typeof(ButtonSpec), "ToolboxBitmaps.KryptonButtonSpec.bmp")]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(Style))]
public abstract class ButtonSpec : Component,
    IButtonSpecValues,
    ICloneable
{
    #region Instance Fields
    private Image? _image;
    private Image? _toolTipImage;
    private Color _colorMap;
    private Color _imageTransparentColor;
    private Color _toolTipImageTransparentColor;
    private string _text;
    private string _extraText;
    private string _toolTipTitle;
    private string _toolTipBody;
    private bool _allowInheritImage;
    private bool _allowInheritText;
    private bool _allowInheritExtraText;
    private bool _allowInheritToolTipTitle;
    private ViewBase? _buttonSpecView;
    private KryptonCommand? _command;
    private PaletteButtonStyle _style;
    private PaletteButtonOrientation _orientation;
    private PaletteRelativeEdgeAlign _edge;
    private readonly CheckButtonImageStates _imageStates;

    #endregion

    #region Events
    /// <summary>
    /// Occurs whenever a button specification property has changed.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the component is clicked.")]
    public event EventHandler? Click;

    /// <summary>
    /// Occurs whenever a button specification property has changed.
    /// </summary>
    [Category(@"ButtonSpec")]
    [Description(@"Occurs when a button specification property has changed.")]
    public event PropertyChangedEventHandler? ButtonSpecPropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpec class.
    /// </summary>
    protected ButtonSpec()
    {
        _image = null;
        _toolTipImage = null;
        _colorMap = GlobalStaticValues.EMPTY_COLOR;
        _imageTransparentColor = GlobalStaticValues.EMPTY_COLOR;
        _toolTipImageTransparentColor = GlobalStaticValues.EMPTY_COLOR;
        _text = string.Empty;
        _extraText = string.Empty;
        UniqueName = CommonHelper.UniqueString;
        _toolTipTitle = string.Empty;
        _toolTipBody = string.Empty;
        _allowInheritImage = true;
        _allowInheritText = true;
        _allowInheritExtraText = true;
        _allowInheritToolTipTitle = true;
        ToolTipStyle = LabelStyle.ToolTip;
        _style = PaletteButtonStyle.Inherit;
        _orientation = PaletteButtonOrientation.Inherit;
        ProtectedType = PaletteButtonSpecStyle.Generic;
        _edge = PaletteRelativeEdgeAlign.Inherit;
        _imageStates = new CheckButtonImageStates
        {
            NeedPaint = OnImageStateChanged!
        };
        ContextMenuStrip = null;
        KryptonContextMenu = null;
        _buttonSpecView = null;
    }

    /// <summary>
    /// Returns a string that represents the current defaulted state.
    /// </summary>
    /// <returns>A string that represents the current defaulted state.</returns>
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    /// <summary>
    /// Make a clone of this object.
    /// </summary>
    /// <returns>New instance.</returns>
    public virtual object Clone()
    {
        // ReSharper disable RedundantBaseQualifier
        var clone = (ButtonSpec)Activator.CreateInstance(base.GetType())!;
        // ReSharper restore RedundantBaseQualifier
        clone.Image = Image;
        clone.ImageTransparentColor = ImageTransparentColor;
        clone.Text = Text;
        clone.ExtraText = ExtraText;
        clone.ToolTipImage = ToolTipImage;
        clone.ToolTipImageTransparentColor = ToolTipImageTransparentColor;
        clone.ToolTipTitle = ToolTipTitle;
        clone.ToolTipBody = ToolTipBody;
        clone.ToolTipStyle = ToolTipStyle;
        clone.UniqueName = UniqueName;
        clone.AllowInheritImage = AllowInheritImage;
        clone.AllowInheritText = AllowInheritText;
        clone.AllowInheritExtraText = AllowInheritExtraText;
        clone.AllowInheritToolTipTitle = AllowInheritToolTipTitle;
        clone.ColorMap = ColorMap;
        clone.Style = Style;
        clone.Orientation = Orientation;
        clone.Edge = Edge;
        clone.ContextMenuStrip = ContextMenuStrip;
        clone.KryptonContextMenu = KryptonContextMenu;
        clone.KryptonCommand = KryptonCommand;
        clone.Owner = Owner;
        clone.Tag = Tag;
        return clone;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool IsDefault => !ShouldSerializeImageStates() &&
                                     !ShouldSerializeImage() &&
                                     !ShouldSerializeColorMap() &&
                                     !ShouldSerializeImageTransparentColor() &&
                                     !ShouldSerializeText() &&
                                     !ShouldSerializeExtraText() &&
                                     !ShouldSerializeToolTipImage() &&
                                     !ShouldSerializeToolTipImageTransparentColor() &&
                                     !ShouldSerializeToolTipTitle() &&
                                     !ShouldSerializeToolTipBody() &&
                                     !ShouldSerializeToolTipStyle() &&
                                     !ShouldSerializeToolTipShadow() &&
                                     !ShouldSerializeStyle() &&
                                     !ShouldSerializeOrientation() &&
                                     !ShouldSerializeEdge() &&
                                     (ContextMenuStrip == null) &&
                                     AllowInheritImage &&
                                     AllowInheritText &&
                                     AllowInheritExtraText &&
                                     AllowInheritToolTipTitle;

    #endregion

    #region Image
    /// <summary>
    /// Gets and sets the button image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Button image.")]
    public Image? Image
    {
        get => _image;

        set
        {
            if (_image != value)
            {
                _image = value;
                OnButtonSpecPropertyChanged(nameof(Image));
            }
        }
    }
    private bool ShouldSerializeImage() => Image != null;
    private void ResetImage() => Image = null;

    #endregion

    #region ImageTransparentColor
    /// <summary>
    /// Gets and sets the button image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Button image transparent color.")]
    [KryptonDefaultColor]
    public Color ImageTransparentColor
    {
        get => _imageTransparentColor;

        set
        {
            if (_imageTransparentColor != value)
            {
                _imageTransparentColor = value;
                OnButtonSpecPropertyChanged(nameof(ImageTransparentColor));
            }
        }
    }
    private bool ShouldSerializeImageTransparentColor() => ImageTransparentColor != GlobalStaticValues.EMPTY_COLOR;
    private void ResetImageTransparentColor() => ImageTransparentColor = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ImageStates
    /// <summary>
    /// Gets access to the state specific images for the button.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"State specific images for the button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ButtonImageStates ImageStates => _imageStates;
    private bool ShouldSerializeImageStates() => !_imageStates.IsDefault;
    #endregion

    #region Text
    /// <summary>
    /// Gets and sets the button text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Button text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    public string Text
    {
        get => _text;

        set
        {
            if (_text != value)
            {
                _text = value;
                OnButtonSpecPropertyChanged(nameof(Text));
            }
        }
    }
    private bool ShouldSerializeText() => Text != string.Empty;
    private void ResetText() => Text = string.Empty;
    #endregion

    #region ExtraText
    /// <summary>
    /// Gets and sets the button extra text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Button extra text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    public string ExtraText
    {
        get => _extraText;

        set
        {
            if (_extraText != value)
            {
                _extraText = value;
                OnButtonSpecPropertyChanged(nameof(ExtraText));
            }
        }
    }
    private bool ShouldSerializeExtraText() => ExtraText != string.Empty;
    private void ResetExtraText() => ExtraText = string.Empty;
    #endregion

    #region ToolTipImage
    /// <summary>
    /// Gets and sets the button tooltip image.
    /// </summary>
    [Localizable(true)]
    [Category(@"ToolTip")]
    [Description(@"Button tooltip image.")]
    [DefaultValue(null)]
    public Image? ToolTipImage
    {
        get => _toolTipImage;

        set
        {
            if (_toolTipImage != value)
            {
                _toolTipImage = value;
                OnButtonSpecPropertyChanged(nameof(ToolTipImage));
            }
        }
    }
    private bool ShouldSerializeToolTipImage() => ToolTipImage != null;
    private void ResetToolTipImage() => ToolTipImage = null;
    #endregion

    #region ToolTipImageTransparentColor
    /// <summary>
    /// Gets and sets the tooltip image transparent color.
    /// </summary>
    [Localizable(true)]
    [Category(@"ToolTip")]
    [Description(@"Button image transparent color.")]
    [KryptonDefaultColor]
    public Color ToolTipImageTransparentColor
    {
        get => _toolTipImageTransparentColor;

        set
        {
            if (_toolTipImageTransparentColor != value)
            {
                _toolTipImageTransparentColor = value;
                OnButtonSpecPropertyChanged(nameof(ToolTipImageTransparentColor));
            }
        }
    }
    private bool ShouldSerializeToolTipImageTransparentColor() => ToolTipImageTransparentColor != GlobalStaticValues.EMPTY_COLOR;
    private void ResetToolTipImageTransparentColor() => ToolTipImageTransparentColor = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ToolTipTitle
    /// <summary>
    /// Gets and sets the button title tooltip text.
    /// </summary>
    [Localizable(true)]
    [Category(@"ToolTip")]
    [Description(@"Button tooltip title text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DefaultValue(@"")]
    public string ToolTipTitle
    {
        get => _toolTipTitle;

        set
        {
            if (_toolTipTitle != value)
            {
                _toolTipTitle = value;
                OnButtonSpecPropertyChanged(nameof(ToolTipTitle));
            }
        }
    }
    private bool ShouldSerializeToolTipTitle() => ToolTipTitle != string.Empty;
    private void ResetToolTipTitle() => ToolTipTitle = string.Empty;
    #endregion

    #region ToolTipBody
    /// <summary>
    /// Gets and sets the button body tooltip text.
    /// </summary>
    [Localizable(true)]
    [Category(@"ToolTip")]
    [Description(@"Button tooltip body text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DefaultValue(@"")]
    public string ToolTipBody
    {
        get => _toolTipBody;

        set
        {
            if (_toolTipBody != value)
            {
                _toolTipBody = value;
                OnButtonSpecPropertyChanged(nameof(ToolTipBody));
            }
        }
    }
    private bool ShouldSerializeToolTipBody() => ToolTipBody != string.Empty;
    private void ResetToolTipBody() => ToolTipBody = string.Empty;
    #endregion

    #region ToolTipStyle
    /// <summary>
    /// Gets and sets the tooltip label style.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Button tooltip label style.")]
    [DefaultValue(LabelStyle.ToolTip)]
    public LabelStyle ToolTipStyle { get; set; }
    private bool ShouldSerializeToolTipStyle() => ToolTipStyle != LabelStyle.ToolTip;
    private void ResetToolTipStyle() => ToolTipStyle = LabelStyle.ToolTip;
    #endregion

    #region ToolTipShadow
    /// <summary>
    /// Gets and sets the tooltip label style.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Button tooltip Shadow.")]
    [DefaultValue(true)]
    public bool ToolTipShadow { get; set; } = true;
    private bool ShouldSerializeToolTipShadow() => !ToolTipShadow;
    private void ResetToolTipShadow() => ToolTipShadow = true;
    #endregion

    #region UniqueName
    /// <summary>
    /// Gets and sets the unique name of the ButtonSpec.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The unique name of the ButtonSpec.")]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible)]
    public string? UniqueName { get; set; }
    private void ResetUniqueName() => UniqueName = CommonHelper.UniqueString;
    #endregion

    #region AllowInheritImage
    /// <summary>
    /// Gets and sets if the button image be inherited if defined as null.
    /// </summary>
    [Localizable(true)]
    [Category(@"Inherit")]
    [Description(@"Should button image be inherited if defined as null.")]
    [DefaultValue(true)]
    public bool AllowInheritImage
    {
        get => _allowInheritImage;

        set
        {
            if (_allowInheritImage != value)
            {
                _allowInheritImage = value;
                OnButtonSpecPropertyChanged(nameof(Image));
            }
        }
    }
    private void ResetAllowInheritImage() => AllowInheritImage = true;
    #endregion

    #region AllowInheritText
    /// <summary>
    /// Gets and sets if the button text be inherited if defined as empty.
    /// </summary>
    [Localizable(true)]
    [Category(@"Inherit")]
    [Description(@"Should button text be inherited if defined as empty.")]
    [DefaultValue(true)]
    public bool AllowInheritText
    {
        get => _allowInheritText;

        set
        {
            if (_allowInheritText != value)
            {
                _allowInheritText = value;
                OnButtonSpecPropertyChanged(nameof(Text));
            }
        }
    }
    private void ResetAllowInheritText() => AllowInheritText = true;
    #endregion

    #region AllowInheritExtraText
    /// <summary>
    /// Gets and sets if the button extra text be inherited if defined as empty.
    /// </summary>
    [Localizable(true)]
    [Category(@"Inherit")]
    [Description(@"Should button extra text be inherited if defined as empty.")]
    [DefaultValue(true)]
    public bool AllowInheritExtraText
    {
        get => _allowInheritExtraText;

        set
        {
            if (_allowInheritExtraText != value)
            {
                _allowInheritExtraText = value;
                OnButtonSpecPropertyChanged(nameof(ExtraText));
            }
        }
    }
    private void ResetAllowInheritExtraText() => AllowInheritExtraText = true;
    #endregion

    #region AllowInheritToolTipTitle
    /// <summary>
    /// Gets and sets if the button tooltip title be inherited if defined as empty.
    /// </summary>
    [Localizable(true)]
    [Category(@"Inherit")]
    [Description(@"Should button tooltip title text be inherited if defined as empty.")]
    [DefaultValue(true)]
    public bool AllowInheritToolTipTitle
    {
        get => _allowInheritToolTipTitle;

        set
        {
            if (_allowInheritToolTipTitle != value)
            {
                _allowInheritToolTipTitle = value;
                OnButtonSpecPropertyChanged(nameof(ToolTipTitle));
            }
        }
    }
    private void ResetAllowInheritToolTipTitle() => AllowInheritToolTipTitle = true;
    #endregion

    #region AllowComponent
    /// <summary>
    /// Gets a value indicating if the component is allowed to be selected at design time.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool AllowComponent => true;
    #endregion

    #region ColorMap
    /// <summary>
    /// Gets and sets image color to remap to container foreground.
    /// </summary>
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Image color to remap to container foreground.")]
    [KryptonDefaultColor]
    public Color ColorMap
    {
        get => _colorMap;

        set
        {
            if (_colorMap != value)
            {
                _colorMap = value;
                OnButtonSpecPropertyChanged(nameof(ColorMap));
            }
        }
    }
    private bool ShouldSerializeColorMap() => ColorMap != GlobalStaticValues.EMPTY_COLOR;
    private void ResetColorMap() => ColorMap = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region Style
    /// <summary>
    /// Gets and sets the button style.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Button style.")]
    [DefaultValue(PaletteButtonStyle.Inherit)]
    public PaletteButtonStyle Style
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                OnButtonSpecPropertyChanged(nameof(Style));
            }
        }
    }
    private bool ShouldSerializeStyle() => Style != PaletteButtonStyle.Inherit;
    private void ResetStyle() => Style = PaletteButtonStyle.Inherit;
    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the button orientation.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Defines the button orientation.")]
    [RefreshProperties(RefreshProperties.All)]
    public PaletteButtonOrientation Orientation
    {
        get => _orientation;

        set
        {
            if (_orientation != value)
            {
                _orientation = value;
                OnButtonSpecPropertyChanged(nameof(Orientation));
            }
        }
    }
    private bool ShouldSerializeOrientation() => Orientation != PaletteButtonOrientation.Inherit;
    private void ResetOrientation() => Orientation = PaletteButtonOrientation.Inherit;
    #endregion

    #region Edge
    /// <summary>
    /// Gets and sets the header edge to display the button against.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"The header edge to display the button against.")]
    [RefreshProperties(RefreshProperties.All)]
    public PaletteRelativeEdgeAlign Edge
    {
        get => _edge;

        set
        {
            if (_edge != value)
            {
                _edge = value;
                OnButtonSpecPropertyChanged(nameof(Edge));
            }
        }
    }
    private bool ShouldSerializeEdge() => Edge != PaletteRelativeEdgeAlign.Inherit;
    private void ResetEdge() => Edge = PaletteRelativeEdgeAlign.Inherit;
    #endregion

    #region ContextMenuStrip
    /// <summary>
    /// Gets and sets the context menu strip for the button.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"ContextMenuStrip to show when the button is pressed.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(null)]
    public ContextMenuStrip? ContextMenuStrip { get; set; }
    #endregion

    #region KryptonContextMenu
    /// <summary>
    /// Gets and sets the krypton context menu for the button.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"KryptonContextMenu to show when the button is pressed.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu { get; set; }
    #endregion

    #region KryptonCommand
    /// <summary>
    /// Gets and sets the associated KryptonCommand.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Command associated with the button.")]
    [RefreshProperties(RefreshProperties.All)]
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

                _command = value;

                OnButtonSpecPropertyChanged(nameof(KryptonCommand));

                if (_command != null)
                {
                    _command.PropertyChanged += OnCommandPropertyChanged;

                    _command.AssignedButtonSpec = this;
                }
            }
        }
    }
    #endregion

    #region Owner
    /// <summary>
    /// Gets and sets user-defined data associated with the object.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? Owner { get; set; }
    #endregion

    #region Tag
    /// <summary>
    /// Gets and sets user-defined data associated with the object.
    /// </summary>
    [Category(@"Data")]
    [Description(@"User-defined data associated with the object.")]
    [TypeConverter(typeof(StringConverter))]
    [DefaultValue(null)]
    public object? Tag { get; set; }
    #endregion

    #region CopyFrom
    /// <summary>
    /// Value copy from the provided source to self.
    /// </summary>
    /// <param name="source">Source instance.</param>
    public virtual void CopyFrom(ButtonSpec source)
    {
        // Copy class specific values
        Image = source.Image;
        ImageTransparentColor = source.ImageTransparentColor;
        ImageStates.CopyFrom(source.ImageStates);
        Text = source.Text;
        ExtraText = source.ExtraText;
        AllowInheritImage = source.AllowInheritImage;
        AllowInheritText = source.AllowInheritText;
        AllowInheritExtraText = source.AllowInheritExtraText;
        ColorMap = source.ColorMap;
        Style = source.Style;
        Orientation = source.Orientation;
        Edge = source.Edge;
        ProtectedType = source.ProtectedType;
    }
    #endregion

    #region PerformClick
    /// <summary>
    /// Generates a Click event for the control.
    /// </summary>
    public void PerformClick() => PerformClick(EventArgs.Empty);

    /// <summary>
    /// Generates a Click event for the control.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    public void PerformClick(EventArgs e) => OnClick(e);
    #endregion

    #region IButtonSpecValues
    /// <summary>
    /// Gets the button image.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <param name="state">State for which an image is needed.</param>
    /// <returns>Button image.</returns>
    public virtual Image? GetImage(PaletteBase? palette, PaletteState state)
    {
        Image? image = null;

        // Prefer to get image from the command first
        if (KryptonCommand != null)
        {
            return KryptonCommand.ImageSmall;
        }

        // Try and recover a state specific image
        image = state switch
        {
            PaletteState.Disabled => ImageStates.ImageDisabled,
            PaletteState.Normal => ImageStates.ImageNormal,
            PaletteState.Pressed => ImageStates.ImagePressed,
            PaletteState.Tracking => ImageStates.ImageTracking,
            PaletteState.CheckedNormal => ImageStates.ImageCheckedNormal,
            PaletteState.CheckedPressed => ImageStates.ImageCheckedPressed,
            PaletteState.CheckedTracking => ImageStates.ImageCheckedTracking,
            _ => image
        } ?? Image; // Default to the image if no state specific image is found

        return (image != null) || !AllowInheritImage
            ? image
            : palette?.GetButtonSpecImage(ProtectedType, state);
    }

    /// <summary>
    /// Gets the image transparent color.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetImageTransparentColor(PaletteBase? palette)
    {
        if (KryptonCommand != null)
        {
            return KryptonCommand.ImageTransparentColor;
        }

        return ImageTransparentColor != GlobalStaticValues.EMPTY_COLOR
            ? ImageTransparentColor
            : palette?.GetButtonSpecImageTransparentColor(ProtectedType) ?? GlobalStaticValues.EMPTY_COLOR;
    }

    /// <summary>
    /// Gets the button short text.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Short text string.</returns>
    public virtual string GetShortText(PaletteBase? palette)
    {
        if (KryptonCommand != null)
        {
            return KryptonCommand.Text;
        }

        return (Text.Length > 0)
               || !AllowInheritText
            ? Text
            : palette?.GetButtonSpecShortText(ProtectedType) ?? string.Empty;
    }

    /// <summary>
    /// Gets the button long text.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Long text string.</returns>
    public virtual string GetLongText(PaletteBase? palette)
    {
        if (KryptonCommand != null)
        {
            return KryptonCommand.ExtraText;
        }
        return (ExtraText.Length > 0)
               || !AllowInheritExtraText
            ? ExtraText
            : palette?.GetButtonSpecLongText(ProtectedType) ?? string.Empty;
    }

    /// <summary>
    /// Gets the button tooltip title text.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Tooltip title string.</returns>
    public virtual string GetToolTipTitle(PaletteBase palette) => !string.IsNullOrEmpty(ToolTipTitle)
                                                                  || !AllowInheritToolTipTitle
        ? ToolTipTitle
        : palette.GetButtonSpecToolTipTitle(ProtectedType) ?? string.Empty;

    /// <summary>
    /// Gets the color to remap from the image to the container foreground.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetColorMap(PaletteBase? palette) => ColorMap != GlobalStaticValues.EMPTY_COLOR
        ? ColorMap
        : palette!.GetButtonSpecColorMap(ProtectedType);

    /// <summary>
    /// Gets the button style.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button style.</returns>
    public virtual ButtonStyle GetStyle(PaletteBase? palette) => ConvertToButtonStyle(Style != PaletteButtonStyle.Inherit
        ? Style
        : palette?.GetButtonSpecStyle(ProtectedType));

    /// <summary>
    /// Gets the button orientation.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button orientation.</returns>
    public virtual ButtonOrientation GetOrientation(PaletteBase? palette) => ConvertToButtonOrientation(
        Orientation != PaletteButtonOrientation.Inherit
            ? Orientation
            : palette?.GetButtonSpecOrientation(ProtectedType) ?? PaletteButtonOrientation.Auto);

    /// <summary>
    /// Gets the edge for the button.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button edge.</returns>
    public virtual RelativeEdgeAlign GetEdge(PaletteBase? palette) =>
        ConvertToRelativeEdgeAlign(Edge != PaletteRelativeEdgeAlign.Inherit
            ? Edge
            : palette?.GetButtonSpecEdge(ProtectedType) ?? PaletteRelativeEdgeAlign.Near);

    /// <summary>
    /// Gets the button location.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button location.</returns>
    public virtual HeaderLocation GetLocation(PaletteBase? palette) => HeaderLocation.PrimaryHeader;

    /// <summary>
    /// Gets the button enabled state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button enabled state.</returns>
    public abstract ButtonEnabled GetEnabled(PaletteBase palette);

    /// <summary>
    /// Sets the current view associated with the button spec.
    /// </summary>
    /// <param name="view">View element reference.</param>
    public virtual void SetView(ViewBase view) => _buttonSpecView = view;

    /// <summary>
    /// Get the current view associated with the button spec.
    /// </summary>
    /// <returns>View element reference.</returns>
    public virtual ViewBase GetView() => _buttonSpecView!;

    /// <summary>
    /// Gets a value indicating if the associated view is enabled.
    /// </summary>
    /// <returns>True if enabled; otherwise false.</returns>
    public bool GetViewEnabled() => _buttonSpecView != null && _buttonSpecView.State != PaletteState.Disabled;

    /// <summary>
    /// Gets the button visible value.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button visibility.</returns>
    public abstract bool GetVisible(PaletteBase palette);

    /// <summary>
    /// Gets the button checked state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button checked state.</returns>
    public abstract ButtonCheckState GetChecked(PaletteBase? palette);
    #endregion

    #region Protected
    /// <summary>
    /// Generates the Click event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected void GenerateClick(EventArgs e)
    {
        Click?.Invoke(this, e);

        // If we have an attached command then execute it
        KryptonCommand?.PerformExecute();
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnClick(EventArgs e)
    {
        // Only if associated view is enabled do we perform the click
        if (GetViewEnabled())
        {
            Click?.Invoke(this, e);

            // If we have an attached command then execute it
            KryptonCommand?.PerformExecute();
        }
    }

    /// <summary>
    /// Raises the ButtonSpecPropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of the appearance property that has changed.</param>
    protected virtual void OnButtonSpecPropertyChanged(string propertyName) => ButtonSpecPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Handles a change in the property of an attached command.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
    protected virtual void OnCommandPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Text):
            case nameof(ExtraText):
            case @"ImageSmall":
            case nameof(ImageTransparentColor):
                OnButtonSpecPropertyChanged(e.PropertyName);
                break;
        }
    }

    /// <summary>
    /// Gets and sets the actual type of the button.
    /// </summary>
    protected PaletteButtonSpecStyle ProtectedType { get; set; }

    /// <summary>
    /// Convert from palette specific edge alignment to resolved edge alignment.
    /// </summary>
    /// <param name="paletteRelativeEdgeAlign">Palette specific edge alignment.</param>
    /// <returns>Resolved edge alignment.</returns>
    protected RelativeEdgeAlign ConvertToRelativeEdgeAlign(PaletteRelativeEdgeAlign paletteRelativeEdgeAlign)
    {
        switch (paletteRelativeEdgeAlign)
        {
            case PaletteRelativeEdgeAlign.Near:
                return RelativeEdgeAlign.Near;

            case PaletteRelativeEdgeAlign.Far:
                return RelativeEdgeAlign.Far;

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(paletteRelativeEdgeAlign.ToString());
                return RelativeEdgeAlign.Far;
        }
    }

    /// <summary>
    /// Convert from palette specific button orientation to resolved button orientation.
    /// </summary>
    /// <param name="paletteButtonOrientation">Palette specific button orientation.</param>
    /// <returns>Resolved button orientation.</returns>
    protected ButtonOrientation ConvertToButtonOrientation(PaletteButtonOrientation paletteButtonOrientation)
    {
        switch (paletteButtonOrientation)
        {
            case PaletteButtonOrientation.Auto:
                return ButtonOrientation.Auto;

            case PaletteButtonOrientation.FixedBottom:
                return ButtonOrientation.FixedBottom;

            case PaletteButtonOrientation.FixedLeft:
                return ButtonOrientation.FixedLeft;

            case PaletteButtonOrientation.FixedRight:
                return ButtonOrientation.FixedRight;

            case PaletteButtonOrientation.FixedTop:
                return ButtonOrientation.FixedTop;

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(paletteButtonOrientation.ToString());
                return ButtonOrientation.Auto;
        }
    }

    /// <summary>
    /// Convert from palette specific button style to resolved button style.
    /// </summary>
    /// <param name="paletteButtonStyle">Palette specific button style.</param>
    /// <returns>Resolve button style.</returns>
    protected ButtonStyle ConvertToButtonStyle(PaletteButtonStyle? paletteButtonStyle)
    {
        switch (paletteButtonStyle)
        {
            case PaletteButtonStyle.Standalone:
                return ButtonStyle.Standalone;
            case PaletteButtonStyle.Alternate:
                return ButtonStyle.Alternate;
            case PaletteButtonStyle.LowProfile:
                return ButtonStyle.LowProfile;
            case PaletteButtonStyle.ButtonSpec:
                return ButtonStyle.ButtonSpec;
            case PaletteButtonStyle.BreadCrumb:
                return ButtonStyle.BreadCrumb;
            case PaletteButtonStyle.Cluster:
                return ButtonStyle.Cluster;
            case PaletteButtonStyle.NavigatorStack:
                return ButtonStyle.NavigatorStack;
            case PaletteButtonStyle.NavigatorOverflow:
                return ButtonStyle.NavigatorOverflow;
            case PaletteButtonStyle.NavigatorMini:
                return ButtonStyle.NavigatorMini;
            case PaletteButtonStyle.InputControl:
                return ButtonStyle.InputControl;
            case PaletteButtonStyle.ListItem:
                return ButtonStyle.ListItem;
            case PaletteButtonStyle.Form:
                return ButtonStyle.Form;
            case PaletteButtonStyle.FormClose:
                return ButtonStyle.FormClose;
            case PaletteButtonStyle.Command:
                return ButtonStyle.Command;
            case PaletteButtonStyle.Custom1:
                return ButtonStyle.Custom1;
            case PaletteButtonStyle.Custom2:
                return ButtonStyle.Custom2;
            case PaletteButtonStyle.Custom3:
                return ButtonStyle.Custom3;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(paletteButtonStyle.ToString());
                return ButtonStyle.Standalone;
        }
    }
    #endregion

    #region Implementation
    private void OnImageStateChanged(object sender, NeedLayoutEventArgs e) => OnButtonSpecPropertyChanged(nameof(Image));

    #endregion
}