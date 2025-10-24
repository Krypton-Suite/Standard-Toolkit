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
/// Overrides for defining typed button specifications.
/// </summary>
public class KryptonPaletteButtonSpecTyped : KryptonPaletteButtonSpecBase
{
    #region Instance Fields
    private Image? _image;
    private string _text;
    private string _extraText;
    private string _toolTipTitle;
    private Color _colorMap;
    private bool _allowInheritImage;
    private bool _allowInheritText;
    private bool _allowInheritExtraText;
    private bool _allowInheritToolTipTitle;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteButtonSpecCommon class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    internal KryptonPaletteButtonSpecTyped([DisallowNull] PaletteRedirect redirector)
        : base(redirector)
    {
        _image = null;
        _text = string.Empty;
        _extraText = string.Empty;
        _toolTipTitle = string.Empty;
        _colorMap = GlobalStaticValues.EMPTY_COLOR;
        _allowInheritImage = true;
        _allowInheritText = true;
        _allowInheritExtraText = true;
        _allowInheritToolTipTitle = true;
        ImageStates = new CheckButtonImageStates
        {
            NeedPaint = OnImageStateChanged!
        };
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => base.IsDefault &&
                                      ImageStates.IsDefault &&
                                      (Image == null) &&
                                      (Text == string.Empty) &&
                                      (ExtraText == string.Empty) &&
                                      (ToolTipTitle == string.Empty) &&
                                      (ColorMap == GlobalStaticValues.EMPTY_COLOR) &&
                                      AllowInheritImage &&
                                      AllowInheritText &&
                                      AllowInheritExtraText &&
                                      AllowInheritToolTipTitle;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="style">The style of the button spec instance.</param>
    public override void PopulateFromBase(PaletteButtonSpecStyle style)
    {
        base.PopulateFromBase(style);
        Image = Redirector.GetButtonSpecImage(style, PaletteState.Normal);
        ImageStates.ImageDisabled = Redirector.GetButtonSpecImage(style, PaletteState.Disabled);
        ImageStates.ImageNormal = Redirector.GetButtonSpecImage(style, PaletteState.Normal);
        ImageStates.ImageTracking = Redirector.GetButtonSpecImage(style, PaletteState.Tracking);
        ImageStates.ImagePressed = Redirector.GetButtonSpecImage(style, PaletteState.Pressed);
        ImageStates.ImageCheckedNormal = Redirector.GetButtonSpecImage(style, PaletteState.CheckedNormal);
        ImageStates.ImageCheckedTracking = Redirector.GetButtonSpecImage(style, PaletteState.CheckedTracking);
        ImageStates.ImageCheckedPressed = Redirector.GetButtonSpecImage(style, PaletteState.CheckedPressed);
        Text = Redirector.GetButtonSpecShortText(style);
        ExtraText = Redirector.GetButtonSpecLongText(style);
        ColorMap = Redirector.GetButtonSpecColorMap(style);
    }
    #endregion

    #region Image
    /// <summary>
    /// Gets and sets the button image.
    /// </summary>
    [KryptonPersist(false)]
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button image.")]
    [DefaultValue(null)]
    public Image? Image
    {
        get => _image;

        set
        {
            if (_image != value)
            {
                _image = value;
                OnButtonSpecChanged(this, EventArgs.Empty);
            }
        }
    }

    private bool ShouldSerializeImage() => Image != null;

    /// <summary>
    /// Resets the Image property to its default value.
    /// </summary>
    public void ResetImage() => Image = null;

    #endregion

    #region ImageStates
    /// <summary>
    /// Gets access to the state specific images for the button.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"State specific images for the button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CheckButtonImageStates ImageStates { get; }

    private bool ShouldSerializeImageStates() => !ImageStates.IsDefault;

    #endregion

    #region Text
    /// <summary>
    /// Gets and sets the button text.
    /// </summary>
    [KryptonPersist(false)]
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DefaultValue("")]
    public string Text
    {
        get => _text;

        set
        {
            if (_text != value)
            {
                _text = value;
                OnButtonSpecChanged(this, EventArgs.Empty);
            }
        }
    }

    private bool ShouldSerializeText() => Text != string.Empty;

    /// <summary>
    /// Resets the Text property to its default value.
    /// </summary>
    public void ResetText() => Text = string.Empty;

    #endregion

    #region ExtraText
    /// <summary>
    /// Gets and sets the button extra text.
    /// </summary>
    [KryptonPersist(false)]
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button extra text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DefaultValue("")]
    public string ExtraText
    {
        get => _extraText;

        set
        {
            if (_extraText != value)
            {
                _extraText = value;
                OnButtonSpecChanged(this, EventArgs.Empty);
            }
        }
    }

    private bool ShouldSerializeExtraText() => ExtraText != string.Empty;

    /// <summary>
    /// Resets the ExtraText property to its default value.
    /// </summary>
    public void ResetExtraText() => ExtraText = string.Empty;

    #endregion

    #region ToolTipTitle
    /// <summary>
    /// Gets and sets the button tooltip title text.
    /// </summary>
    [KryptonPersist(false)]
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button tooltip title text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DefaultValue("")]
    public string ToolTipTitle
    {
        get => _toolTipTitle;

        set
        {
            if (_toolTipTitle != value)
            {
                _toolTipTitle = value;
                OnButtonSpecChanged(this, EventArgs.Empty);
            }
        }
    }

    private bool ShouldSerializeToolTipTitle() => ToolTipTitle != string.Empty;

    /// <summary>
    /// Resets the ToolTipTitle property to its default value.
    /// </summary>
    public void ResetToolTipTitle() => ToolTipTitle = string.Empty;

    #endregion

    #region ColorMap
    /// <summary>
    /// Gets and sets image color to remap to container foreground.
    /// </summary>
    [KryptonPersist(false)]
    [Localizable(true)]
    [Category(@"Visuals")]
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
                OnButtonSpecChanged(this, EventArgs.Empty);
            }
        }
    }

    private bool ShouldSerializeColorMap() => ColorMap != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Resets the ColorMap property to its default value.
    /// </summary>
    public void ResetColorMap() => ColorMap = GlobalStaticValues.EMPTY_COLOR;

    #endregion

    #region AllowInheritImage
    /// <summary>
    /// Gets and sets if the button image be inherited if defined as null.
    /// </summary>
    [KryptonPersist(false)]
    [Localizable(true)]
    [Category(@"Visuals")]
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
                OnButtonSpecChanged(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Resets the AllowInheritImage property to its default value.
    /// </summary>
    public void ResetAllowInheritImage() => AllowInheritImage = true;

    #endregion

    #region AllowInheritText
    /// <summary>
    /// Gets and sets if the button text be inherited if defined as empty.
    /// </summary>
    [KryptonPersist(false)]
    [Localizable(true)]
    [Category(@"Visuals")]
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
                OnButtonSpecChanged(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Resets the AllowInheritText property to its default value.
    /// </summary>
    public void ResetAllowInheritText() => AllowInheritText = true;

    #endregion

    #region AllowInheritExtraText
    /// <summary>
    /// Gets and sets if the button extra text be inherited if defined as empty.
    /// </summary>
    [KryptonPersist(false)]
    [Localizable(true)]
    [Category(@"Visuals")]
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
                OnButtonSpecChanged(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Resets the AllowInheritExtraText property to its default value.
    /// </summary>
    public void ResetAllowInheritExtraText() => AllowInheritExtraText = true;

    #endregion

    #region AllowInheritToolTipTitle
    /// <summary>
    /// Gets and sets if the button tooltip title text be inherited if defined as empty.
    /// </summary>
    [KryptonPersist(false)]
    [Localizable(true)]
    [Category(@"Visuals")]
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
                OnButtonSpecChanged(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Resets the AllowInheritToolTipTitle property to its default value.
    /// </summary>
    public void ResetAllowInheritToolTipTitle() => AllowInheritToolTipTitle = true;

    #endregion

    #region IPaletteButtonSpec
    /// <summary>
    /// Gets the image to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <param name="state">State for which image is required.</param>
    /// <returns>Image value.</returns>
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style,
        PaletteState state)
    {
        // Try and recover a state specific image
        Image? image = state switch
        {
            PaletteState.Disabled => ImageStates.ImageDisabled,
            PaletteState.Normal => ImageStates.ImageNormal,
            PaletteState.Pressed => ImageStates.ImagePressed,
            PaletteState.Tracking => ImageStates.ImageTracking,
            PaletteState.CheckedNormal => ImageStates.ImageCheckedNormal,
            PaletteState.CheckedPressed => ImageStates.ImageCheckedPressed,
            PaletteState.CheckedTracking => ImageStates.ImageCheckedTracking,
            _ => null
        };

        // Default to the image if no state specific image is found
        image ??= Image;

        return (image != null) || !AllowInheritImage ? image : base.GetButtonSpecImage(style, state);
    }

    /// <summary>
    /// Gets the short text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public override string GetButtonSpecShortText(PaletteButtonSpecStyle style) =>
        (Text.Length > 0) || !AllowInheritText ? Text : base.GetButtonSpecShortText(style);

    /// <summary>
    /// Gets the long text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public override string GetButtonSpecLongText(PaletteButtonSpecStyle style) =>
        (ExtraText.Length > 0) || !AllowInheritExtraText ? ExtraText : base.GetButtonSpecLongText(style);

    /// <summary>
    /// Gets the tooltip title text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public override string GetButtonSpecToolTipTitle(PaletteButtonSpecStyle style) => (ToolTipTitle.Length > 0) || !AllowInheritToolTipTitle ? ToolTipTitle : base.GetButtonSpecToolTipTitle(style);

    /// <summary>
    /// Gets the color to remap from the image to the container foreground.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Color value.</returns>
    public override Color GetButtonSpecColorMap(PaletteButtonSpecStyle style) =>
        ColorMap != GlobalStaticValues.EMPTY_COLOR ? ColorMap : base.GetButtonSpecColorMap(style);

    #endregion

    #region Implementation
    private void OnImageStateChanged(object sender, NeedLayoutEventArgs e) => OnButtonSpecChanged(sender, EventArgs.Empty);
    #endregion
}