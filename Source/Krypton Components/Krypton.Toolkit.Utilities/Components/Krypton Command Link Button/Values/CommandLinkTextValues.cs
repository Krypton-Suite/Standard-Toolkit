#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

// ReSharper disable PossibleUnintendedReferenceComparison
namespace Krypton.Toolkit.Utilities;

public class CommandLinkTextValues : CaptionValues
{
    #region Static Fields
    private const string DEFAULT_HEADING = @"Krypton Command Link Button";
    private const string DEFAULT_DESCRIPTION = @"Krypton Command Link Button ""Note Text""";
    private static readonly Image DEFAULT_IMAGE = CommandLinkArrowHelper.GetDefaultArrowImage();
    #endregion

    #region Instance Fields
    private Font? _descriptionFont;
    private Font? _headingFont;

    private PaletteRelativeAlign _descriptionTextHAlignment;
    private PaletteRelativeAlign _descriptionTextVAlignment;
    private PaletteRelativeAlign _headingTextHAlignment;
    private PaletteRelativeAlign _headingTextVAlignment;
    private Image? _elevationOverlay;
    private bool _elevationOverlayPlaced;
    #endregion

    #region Identity
    /// <summary>Initializes a new instance of the <see cref="CommandLinkTextValues" /> class.</summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="getDpiFactor"></param>
    public CommandLinkTextValues(NeedPaintHandler needPaint, GetDpiFactor getDpiFactor)
        : base(needPaint, getDpiFactor)
    {
        _descriptionFont = null;
        _headingFont = null;

        _headingTextHAlignment = PaletteRelativeAlign.Near;
        _headingTextVAlignment = PaletteRelativeAlign.Center;

        _descriptionTextHAlignment = PaletteRelativeAlign.Near;
        _descriptionTextVAlignment = PaletteRelativeAlign.Far;

        UseDefaultImage = true;
    }
    #endregion

    #region Public
    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializeDescriptionFont()
        && !ShouldSerializeDescription()
        && !ShouldSerializeDescriptionTextHAlignment()
        && !ShouldSerializeDescriptionTextVAlignment()
        && !ShouldSerializeHeadingFont()
        && !ShouldSerializeHeadingTextHAlignment()
        && !ShouldSerializeHeadingTextVAlignment()
        && !ShouldSerializeUseDefaultImage() 
        && Image is not null;

    /// <summary>
    /// Use the default image or not.
    /// </summary>
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Use the default image or not.")]
    public bool UseDefaultImage
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;

                Image = value
                    ? DEFAULT_IMAGE
                    : null;
            }
        }
    }
    public bool ShouldSerializeUseDefaultImage() => !UseDefaultImage;
    public void ResetUseDefaultImage() => UseDefaultImage = true;

    /// <summary>
    /// Notifies the owning button when the command-link image changes so the elevation shield can move.
    /// </summary>
    internal Action? ElevationImageChanged { get; set; }

    /// <inheritdoc />
    protected override void OnImageChanged() => ElevationImageChanged?.Invoke();

    /// <summary>
    /// Applies the default command-link arrow, mirrored when <paramref name="rightToLeft"/> is true.
    /// </summary>
    /// <param name="rightToLeft">True when both RTL layout flags are set on the owning button.</param>
    public void SyncDefaultArrow(bool rightToLeft)
    {
        if (UseDefaultImage)
        {
            Image = CommandLinkArrowHelper.GetDefaultArrowImage(rightToLeft: rightToLeft);
        }
    }
    #endregion

    #region Protected

    /// <inheritdoc />
    protected override Image? GetImageDefault() => DEFAULT_IMAGE;

    /// <inheritdoc />
    protected override string GetDescriptionDefault() => DEFAULT_DESCRIPTION;

    /// <inheritdoc />
    protected override string GetHeadingDefault() => DEFAULT_HEADING;

    #endregion

    #region Public

    /// <inheritdoc />
    [Category(@"CommandLink")]
    [Description(@"The description text for the command link button.")]
    [DefaultValue(DEFAULT_DESCRIPTION)]
    public override string Description
    { 
        get => base.Description; 
        set => base.Description = value; 
    }
    public bool ShouldSerializeDescription() => base.Description != DEFAULT_DESCRIPTION;
    public new void ResetDescription() => base.Description = DEFAULT_DESCRIPTION;

    /// <summary>Resets the text.</summary>
    public void ResetText()
    {
        Heading         = DEFAULT_HEADING;
        Description     = DEFAULT_DESCRIPTION;
        DescriptionFont = _descriptionFont;
        HeadingFont     = _headingFont;
    }

    /// <summary>Gets or sets the description font.</summary>
    /// <value>The description font.</value>
    [Category(@"CommandLink")]
    [Description(@"The description text font for the command link button.")]
    [DefaultValue(null)]
    public Font? DescriptionFont
    {
        get => _descriptionFont;

        set
        {
            if (_descriptionFont != value)
            {
                _descriptionFont = value;
                PerformNeedPaint(true);
            }
        }
    }
    public bool ShouldSerializeDescriptionFont() => DescriptionFont is not null;
    public void ResetDescriptionFont() => DescriptionFont = null;

    /// <summary>Gets or sets the heading font.</summary>
    /// <value>The heading font.</value>
    [Category(@"CommandLink")]
    [Description(@"The heading text font for the command link button.")]
    [DefaultValue(null)]
    public Font? HeadingFont
    {
        get => _headingFont;

        set
        {
            if (_headingFont != value)
            {
                _headingFont = value;
                PerformNeedPaint(true);
            }
        }
    }
    public bool ShouldSerializeHeadingFont() => HeadingFont is not null;
    public void ResetHeadingFont() => HeadingFont = null;

    /// <summary>Gets or sets the description text horizontal alignment.</summary>
    /// <value>The description text horizontal alignment.</value>
    [Category(@"CommandLink")]
    [Description(@"The description text horizontal alignment for the command link button.")]
    [DefaultValue(PaletteRelativeAlign.Near)]
    public PaletteRelativeAlign DescriptionTextHAlignment
    {
        get => _descriptionTextHAlignment;

        set
        {
            if (_descriptionTextHAlignment != value)
            {
                _descriptionTextHAlignment = value;
                PerformNeedPaint(true);
            }
        }
    }
    public bool ShouldSerializeDescriptionTextHAlignment() => DescriptionTextHAlignment != PaletteRelativeAlign.Near;
    public void ResetDescriptionTextHAlignment() => DescriptionTextHAlignment = PaletteRelativeAlign.Near;

    /// <summary>Gets or sets the description text vertical alignment.</summary>
    /// <value>The description text vertical alignment.</value>
    [Category(@"CommandLink")]
    [Description(@"The description text verticl alignment for the command link button.")]
    [DefaultValue(PaletteRelativeAlign.Far)]
    public PaletteRelativeAlign DescriptionTextVAlignment
    {
        get => _descriptionTextVAlignment;

        set
        {
            if (_descriptionTextVAlignment != value)
            {
                _descriptionTextVAlignment = value;
                PerformNeedPaint(true);
            }
        }
    }
    public bool ShouldSerializeDescriptionTextVAlignment() => DescriptionTextVAlignment != PaletteRelativeAlign.Far;
    public void ResetDescriptionTextVAlignment() => DescriptionTextVAlignment = PaletteRelativeAlign.Far;

    /// <summary>Gets or sets the heading text horizontal alignment.</summary>
    /// <value>The heading text horizontal alignment.</value>
    [Category(@"CommandLink")]
    [Description(@"The heading text horizontal alignment for the command link button.")]
    [DefaultValue(PaletteRelativeAlign.Near)]
    public PaletteRelativeAlign HeadingTextHAlignment
    {
        get => _headingTextHAlignment;

        set
        {
            if (_headingTextHAlignment != value)
            {
                _headingTextHAlignment = value;
                PerformNeedPaint(true);
            }
        }
    }
    public bool ShouldSerializeHeadingTextHAlignment() => HeadingTextHAlignment != PaletteRelativeAlign.Near;
    public void ResetHeadingTextHAlignment() => HeadingTextHAlignment = PaletteRelativeAlign.Near;

    /// <summary>Gets or sets the heading text vertical alignment.</summary>
    /// <value>The heading text vertical alignment.</value>
    [Category(@"CommandLink")]
    [Description(@"The heading text vertical alignment for the command link button.")]
    [DefaultValue(PaletteRelativeAlign.Center)]
    public PaletteRelativeAlign HeadingTextVAlignment
    {
        get => _headingTextVAlignment;

        set
        {
            if (_headingTextVAlignment != value)
            {
                _headingTextVAlignment = value;
                PerformNeedPaint(true);
            }
        }
    }
    public bool ShouldSerializeHeadingTextVAlignment() => HeadingTextVAlignment != PaletteRelativeAlign.Center;
    public void ResetHeadingTextVAlignment() => HeadingTextVAlignment = PaletteRelativeAlign.Center;

    /// <inheritdoc />
    public override Image? GetOverlayImage(PaletteState state) =>
        _elevationOverlayPlaced ? _elevationOverlay : base.GetOverlayImage(state);

    /// <inheritdoc />
    public override OverlayImagePosition GetOverlayImagePosition(PaletteState state) =>
        _elevationOverlayPlaced ? OverlayImagePosition.BottomRight : base.GetOverlayImagePosition(state);

    /// <inheritdoc />
    public override OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) =>
        _elevationOverlayPlaced ? OverlayImageScaleMode.Percentage : base.GetOverlayImageScaleMode(state);

    /// <summary>
    /// Draws a half-size UAC shield on the bottom-right of the command-link image.
    /// </summary>
    /// <remarks>
    /// The existing image stays. With no image the overlay does not paint.
    /// </remarks>
    internal void ShowElevationShieldOverlay()
    {
        _elevationOverlay?.Dispose();
        // The renderer draws this at half the 32px arrow, so supply that size directly.
        _elevationOverlay = CommandLinkImageValues.CreateShieldBitmap(16);
        _elevationOverlayPlaced = _elevationOverlay != null;
        PerformNeedPaint(true);
    }

    /// <summary>
    /// Removes the elevation shield overlay when this type placed it.
    /// </summary>
    internal void ClearElevationShieldOverlay()
    {
        if (!_elevationOverlayPlaced)
        {
            return;
        }

        _elevationOverlay?.Dispose();
        _elevationOverlay = null;
        _elevationOverlayPlaced = false;
        PerformNeedPaint(true);
    }
    #endregion
}
