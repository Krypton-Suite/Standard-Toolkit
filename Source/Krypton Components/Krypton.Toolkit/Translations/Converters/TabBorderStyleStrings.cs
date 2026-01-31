#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="TabBorderStyleConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TabBorderStyleStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_TAB_BORDER_STYLE_ONE_NOTE = @"OneNote";
    private const string DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_SMALL = @"Square Equal Small";
    private const string DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_MEDIUM = @"Square Equal Medium";
    private const string DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_LARGE = @"Square Equal Large";
    private const string DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_SMALL = @"Square Outsize Small";
    private const string DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_MEDIUM = @"Square Outsize Medium";
    private const string DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_LARGE = @"Square Outsize Large";
    private const string DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_SMALL = @"Rounded Equal Small";
    private const string DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_MEDIUM = @"Rounded Equal Medium";
    private const string DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_LARGE = @"Rounded Equal Large";
    private const string DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_SMALL = @"Rounded Outsize Small";
    private const string DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_MEDIUM = @"Rounded Outsize Medium";
    private const string DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_LARGE = @"Rounded Outsize Large";
    private const string DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_NEAR = @"Slant Equal Near";
    private const string DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_FAR = @"Slant Equal Far";
    private const string DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_BOTH = @"Slant Equal Both";
    private const string DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_NEAR = @"Slant Outsize Near";
    private const string DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_FAR = @"Slant Outsize Far";
    private const string DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_BOTH = @"Slant Outsize Both";
    private const string DEFAULT_TAB_BORDER_STYLE_SMOOTH_EQUAL = @"Smooth Equal";
    private const string DEFAULT_TAB_BORDER_STYLE_SMOOTH_OUTSIZE = @"Smooth Outsize";
    private const string DEFAULT_TAB_BORDER_STYLE_DOCK_EQUAL = @"Dock Equal";
    private const string DEFAULT_TAB_BORDER_STYLE_DOCK_OUTSIZE = @"Dock Outsize";

    #endregion

    #region Identity

    public TabBorderStyleStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => OneNote.Equals(DEFAULT_TAB_BORDER_STYLE_ONE_NOTE) &&
                             SquareEqualSmall.Equals(DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_SMALL) &&
                             SquareEqualMedium.Equals(DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_MEDIUM) &&
                             SquareEqualLarge.Equals(DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_LARGE) &&
                             SquareOutsizeSmall.Equals(DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_SMALL) &&
                             SquareOutsizeMedium.Equals(DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_MEDIUM) &&
                             SquareOutsizeLarge.Equals(DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_LARGE) &&
                             RoundedEqualSmall.Equals(DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_SMALL) &&
                             RoundedEqualMedium.Equals(DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_MEDIUM) &&
                             RoundedEqualLarge.Equals(DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_LARGE) &&
                             RoundedOutsizeSmall.Equals(DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_SMALL) &&
                             RoundedOutsizeMedium.Equals(DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_MEDIUM) &&
                             RoundedOutsizeLarge.Equals(DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_LARGE) &&
                             SlantEqualNear.Equals(DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_NEAR) &&
                             SlantEqualFar.Equals(DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_FAR) &&
                             SlantEqualBoth.Equals(DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_BOTH) &&
                             SlantOutsizeNear.Equals(DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_NEAR) &&
                             SlantOutsizeFar.Equals(DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_FAR) &&
                             SlantOutsizeBoth.Equals(DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_BOTH) &&
                             SmoothEqual.Equals(DEFAULT_TAB_BORDER_STYLE_SMOOTH_EQUAL) &&
                             SmoothOutsize.Equals(DEFAULT_TAB_BORDER_STYLE_SMOOTH_OUTSIZE) &&
                             DockEqual.Equals(DEFAULT_TAB_BORDER_STYLE_DOCK_EQUAL) &&
                             DockOutsize.Equals(DEFAULT_TAB_BORDER_STYLE_DOCK_OUTSIZE);

    public void Reset()
    {
        OneNote = DEFAULT_TAB_BORDER_STYLE_ONE_NOTE;

        SquareEqualSmall = DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_SMALL;

        SquareEqualMedium = DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_MEDIUM;

        SquareEqualLarge = DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_LARGE;

        SquareOutsizeSmall = DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_SMALL;

        SquareOutsizeMedium = DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_MEDIUM;

        SquareOutsizeLarge = DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_LARGE;

        RoundedEqualSmall = DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_SMALL;

        RoundedEqualMedium = DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_MEDIUM;

        RoundedEqualLarge = DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_LARGE;

        RoundedOutsizeSmall = DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_SMALL;

        RoundedOutsizeMedium = DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_MEDIUM;

        RoundedOutsizeLarge = DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_LARGE;

        SlantEqualNear = DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_NEAR;

        SlantEqualFar = DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_FAR;

        SlantEqualBoth = DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_BOTH;

        SlantOutsizeNear = DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_NEAR;

        SlantOutsizeFar = DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_FAR;

        SlantOutsizeBoth = DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_BOTH;

        SmoothEqual = DEFAULT_TAB_BORDER_STYLE_SMOOTH_EQUAL;

        SmoothOutsize = DEFAULT_TAB_BORDER_STYLE_SMOOTH_OUTSIZE;

        DockEqual = DEFAULT_TAB_BORDER_STYLE_DOCK_EQUAL;

        DockOutsize = DEFAULT_TAB_BORDER_STYLE_DOCK_OUTSIZE;
    }

    /// <summary>Gets or sets the OneNote tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The OneNote tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_ONE_NOTE)]
    [RefreshProperties(RefreshProperties.All)]
    public string OneNote { get; set; }

    /// <summary>Gets or sets the square equal small tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The square equal small tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_SMALL)]
    [RefreshProperties(RefreshProperties.All)]
    public string SquareEqualSmall { get; set; }

    /// <summary>Gets or sets the square equal medium tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The square equal medium tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_MEDIUM)]
    [RefreshProperties(RefreshProperties.All)]
    public string SquareEqualMedium { get; set; }

    /// <summary>Gets or sets the square equal large tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The square equal large tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_LARGE)]
    [RefreshProperties(RefreshProperties.All)]
    public string SquareEqualLarge { get; set; }

    /// <summary>Gets or sets the square outsize small tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The square outsize small tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_SMALL)]
    [RefreshProperties(RefreshProperties.All)]
    public string SquareOutsizeSmall { get; set; }

    /// <summary>Gets or sets the square outsize medium tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The square outsize medium tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_MEDIUM)]
    [RefreshProperties(RefreshProperties.All)]
    public string SquareOutsizeMedium { get; set; }

    /// <summary>Gets or sets the square outsize large tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The square outsize large tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_LARGE)]
    [RefreshProperties(RefreshProperties.All)]
    public string SquareOutsizeLarge { get; set; }

    /// <summary>Gets or sets the rounded equal small tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The rounded equal small tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_SMALL)]
    [RefreshProperties(RefreshProperties.All)]
    public string RoundedEqualSmall { get; set; }

    /// <summary>Gets or sets the rounded equal medium tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The rounded equal medium tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_MEDIUM)]
    [RefreshProperties(RefreshProperties.All)]
    public string RoundedEqualMedium { get; set; }

    /// <summary>Gets or sets the rounded equal large tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The rounded equal large tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_LARGE)]
    [RefreshProperties(RefreshProperties.All)]
    public string RoundedEqualLarge { get; set; }

    /// <summary>Gets or sets the rounded outsize small tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The rounded outsize small tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_SMALL)]
    [RefreshProperties(RefreshProperties.All)]
    public string RoundedOutsizeSmall { get; set; }

    /// <summary>Gets or sets the rounded outsize medium tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The rounded outsize medium tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_MEDIUM)]
    [RefreshProperties(RefreshProperties.All)]
    public string RoundedOutsizeMedium { get; set; }

    /// <summary>Gets or sets the rounded outsize large tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The rounded outsize large tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_LARGE)]
    [RefreshProperties(RefreshProperties.All)]
    public string RoundedOutsizeLarge { get; set; }

    /// <summary>Gets or sets the slant equal near tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The slant equal near tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_NEAR)]
    [RefreshProperties(RefreshProperties.All)]
    public string SlantEqualNear { get; set; }

    /// <summary>Gets or sets the slant equal far tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The slant equal far tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_FAR)]
    [RefreshProperties(RefreshProperties.All)]
    public string SlantEqualFar { get; set; }

    /// <summary>Gets or sets the slant equal both tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The slant equal both tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_BOTH)]
    [RefreshProperties(RefreshProperties.All)]
    public string SlantEqualBoth { get; set; }

    /// <summary>Gets or sets the slant outsize near tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The slant outsize near tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_NEAR)]
    [RefreshProperties(RefreshProperties.All)]
    public string SlantOutsizeNear { get; set; }

    /// <summary>Gets or sets the slant outsize far tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The slant outsize far tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_FAR)]
    [RefreshProperties(RefreshProperties.All)]
    public string SlantOutsizeFar { get; set; }

    /// <summary>Gets or sets the slant outsize both tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The slant outsize both tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_BOTH)]
    [RefreshProperties(RefreshProperties.All)]
    public string SlantOutsizeBoth { get; set; }

    /// <summary>Gets or sets the smooth equal tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The smooth equal tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SMOOTH_EQUAL)]
    [RefreshProperties(RefreshProperties.All)]
    public string SmoothEqual { get; set; }

    /// <summary>Gets or sets the smooth outsize tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The smooth outsize tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_SMOOTH_OUTSIZE)]
    [RefreshProperties(RefreshProperties.All)]
    public string SmoothOutsize { get; set; }

    /// <summary>Gets or sets the dock equal tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The dock equal tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_DOCK_EQUAL)]
    [RefreshProperties(RefreshProperties.All)]
    public string DockEqual { get; set; }

    /// <summary>Gets or sets the dock outsize tab border style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The dock outsize tab border style.")]
    [DefaultValue(DEFAULT_TAB_BORDER_STYLE_DOCK_OUTSIZE)]
    [RefreshProperties(RefreshProperties.All)]
    public string DockOutsize { get; set; }

    #endregion
}