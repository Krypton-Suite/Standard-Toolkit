#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

// ReSharper disable PossibleUnintendedReferenceComparison
namespace Krypton.Toolkit;

public class CommandLinkTextValues : CaptionValues
{
    #region Static Fields

    private const string DEFAULT_HEADING = @"Krypton Command Link Button";

    private const string DEFAULT_DESCRIPTION = @"Krypton Command Link Button ""Note Text""";

    #endregion

    #region Instance Fields

    private Font? _descriptionFont;

    private Font? _headingFont;

    private PaletteRelativeAlign? _descriptionTextHAlignment;

    private PaletteRelativeAlign? _descriptionTextVAlignment;

    private PaletteRelativeAlign? _headingTextHAlignment;

    private PaletteRelativeAlign? _headingTextVAlignment;

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

        _descriptionTextHAlignment = PaletteRelativeAlign.Near;

        _descriptionTextVAlignment = PaletteRelativeAlign.Far;

        _headingTextHAlignment = PaletteRelativeAlign.Near;

        _headingTextVAlignment = PaletteRelativeAlign.Center;
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override string GetDescriptionDefault() => DEFAULT_DESCRIPTION;

    /// <inheritdoc />
    protected override string GetHeadingDefault() => DEFAULT_HEADING;

    #endregion

    #region Implementation

    /// <inheritdoc />
    [Category(@"CommandLink")]
    [Description(@"The description text for the command link button.")]
    [DefaultValue(DEFAULT_DESCRIPTION)]
    public override string Description { get => base.Description; set => base.Description = value; }

    /// <summary>Resets the text.</summary>
    public void ResetText()
    {
        Heading = DEFAULT_HEADING;

        Description = DEFAULT_DESCRIPTION;

        DescriptionFont = _descriptionFont;

        HeadingFont = _headingFont;
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

    /// <summary>Gets or sets the description text horizontal alignment.</summary>
    /// <value>The description text horizontal alignment.</value>
    [Category(@"CommandLink")]
    [Description(@"The description text horizontal alignment for the command link button.")]
    [DefaultValue(null)]
    public PaletteRelativeAlign? DescriptionTextHAlignment
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

    /// <summary>Gets or sets the description text vertical alignment.</summary>
    /// <value>The description text vertical alignment.</value>
    [Category(@"CommandLink")]
    [Description(@"The description text verticl alignment for the command link button.")]
    [DefaultValue(null)]
    public PaletteRelativeAlign? DescriptionTextVAlignment
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

    /// <summary>Gets or sets the heading text horizontal alignment.</summary>
    /// <value>The heading text horizontal alignment.</value>
    [Category(@"CommandLink")]
    [Description(@"The heading text horizontal alignment for the command link button.")]
    [DefaultValue(null)]
    public PaletteRelativeAlign? HeadingTextHAlignment
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

    /// <summary>Gets or sets the heading text vertical alignment.</summary>
    /// <value>The heading text vertical alignment.</value>
    [Category(@"CommandLink")]
    [Description(@"The heading text vertical alignment for the command link button.")]
    [DefaultValue(null)]
    public PaletteRelativeAlign? HeadingTextVAlignment
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

    #endregion
}