#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonPrintPreviewDialogStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_PRINT_BUTTON_TEXT = @"&Print";
    private const string DEFAULT_ZOOM_IN_BUTTON_TEXT = @"Zoom &In";
    private const string DEFAULT_ZOOM_OUT_BUTTON_TEXT = @"Zoom &Out";
    private const string DEFAULT_ONE_PAGE_BUTTON_TEXT = @"&One Page";
    private const string DEFAULT_TWO_PAGES_BUTTON_TEXT = @"&Two Pages";
    private const string DEFAULT_THREE_PAGES_BUTTON_TEXT = @"T&hree Pages";
    private const string DEFAULT_FOUR_PAGES_BUTTON_TEXT = @"&Four Pages";
    private const string DEFAULT_SIX_PAGES_BUTTON_TEXT = @"&Six Pages";
    private const string DEFAULT_PAGINATION_PART_ONE_TEXT = @"Page";
    private const string DEFAULT_PAGINATION_PART_TWO_TEXT = @"of";
    private const string DEFAULT_PREVIEW_TEXT = @"Preview";

    #endregion

    #region Identity

    public KryptonPrintPreviewDialogStrings()
    {
        Reset();
    }

    #endregion

    #region Public

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The print button text.")]
    [DefaultValue(DEFAULT_PRINT_BUTTON_TEXT)]
    public string PrintButtonText { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The zoom in button text.")]
    [DefaultValue(DEFAULT_ZOOM_IN_BUTTON_TEXT)]
    public string ZoomInButtonText { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The zoom out button text.")]
    [DefaultValue(DEFAULT_ZOOM_OUT_BUTTON_TEXT)]
    public string ZoomOutButtonText { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The one page button text.")]
    [DefaultValue(DEFAULT_ONE_PAGE_BUTTON_TEXT)]
    public string OnePageButtonText { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The two pages button text.")]
    [DefaultValue(DEFAULT_TWO_PAGES_BUTTON_TEXT)]
    public string TwoPagesButtonText { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The three pages button text.")]
    [DefaultValue(DEFAULT_THREE_PAGES_BUTTON_TEXT)]
    public string ThreePagesButtonText { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The four pages button text.")]
    [DefaultValue(DEFAULT_FOUR_PAGES_BUTTON_TEXT)]
    public string FourPagesButtonText { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The six pages button text.")]
    [DefaultValue(DEFAULT_SIX_PAGES_BUTTON_TEXT)]
    public string SixPagesButtonText { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The pagination part one text.")]
    [DefaultValue(DEFAULT_PAGINATION_PART_ONE_TEXT)]
    public string PaginationPartOneText { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The pagination part two text.")]
    [DefaultValue(DEFAULT_PAGINATION_PART_TWO_TEXT)]
    public string PaginationPartTwoText { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The preview text.")]
    [DefaultValue(DEFAULT_PREVIEW_TEXT)]
    public string PreviewText { get; set; }

    #endregion

    #region Protected Overrides

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion

    #region Implementation

    [Browsable(false)]
    public bool IsDefault => PaginationPartOneText.Equals(DEFAULT_PAGINATION_PART_ONE_TEXT) &&
                             PaginationPartTwoText.Equals(DEFAULT_PAGINATION_PART_TWO_TEXT) &&
                             PreviewText.Equals(DEFAULT_PREVIEW_TEXT) &&
                             PrintButtonText.Equals(DEFAULT_PRINT_BUTTON_TEXT) &&
                             ZoomInButtonText.Equals(DEFAULT_ZOOM_IN_BUTTON_TEXT) &&
                             ZoomInButtonText.Equals(DEFAULT_ZOOM_OUT_BUTTON_TEXT) &&
                             OnePageButtonText.Equals(DEFAULT_ONE_PAGE_BUTTON_TEXT) &&
                             TwoPagesButtonText.Equals(DEFAULT_TWO_PAGES_BUTTON_TEXT) &&
                             ThreePagesButtonText.Equals(DEFAULT_THREE_PAGES_BUTTON_TEXT) &&
                             FourPagesButtonText.Equals(DEFAULT_FOUR_PAGES_BUTTON_TEXT) &&
                             SixPagesButtonText.Equals(DEFAULT_SIX_PAGES_BUTTON_TEXT);

    public void Reset()
    {
        PaginationPartOneText = DEFAULT_PAGINATION_PART_ONE_TEXT;
        PaginationPartOneText = DEFAULT_PAGINATION_PART_TWO_TEXT;
        PreviewText = DEFAULT_PREVIEW_TEXT;
        PrintButtonText = DEFAULT_PRINT_BUTTON_TEXT;
        ZoomInButtonText = DEFAULT_ZOOM_IN_BUTTON_TEXT;
        ZoomInButtonText = DEFAULT_ZOOM_OUT_BUTTON_TEXT;
        OnePageButtonText = DEFAULT_ONE_PAGE_BUTTON_TEXT;
        TwoPagesButtonText = DEFAULT_TWO_PAGES_BUTTON_TEXT;
        ThreePagesButtonText = DEFAULT_THREE_PAGES_BUTTON_TEXT;
        FourPagesButtonText = DEFAULT_FOUR_PAGES_BUTTON_TEXT;
        SixPagesButtonText = DEFAULT_SIX_PAGES_BUTTON_TEXT;
    }

    #endregion
}