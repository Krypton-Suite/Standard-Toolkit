#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Navigator;

/// <summary>
/// Map tooltip values from a source page.
/// </summary>
internal class PageToToolTipMapping : IContentValues
{
    #region Instance Fields
    private readonly KryptonPage _page;
    private readonly MapKryptonPageImage _mapImage;
    private readonly MapKryptonPageText _mapText;
    private readonly MapKryptonPageText _mapExtraText;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PageToToolTipMapping class.
    /// </summary>
    /// <param name="page">Page to source values from.</param>
    /// <param name="mapImage">How to map the image from the page to the tooltip.</param>
    /// <param name="mapText">How to map the text from the page to the tooltip.</param>
    /// <param name="mapExtraText">How to map the extra text from the page to the tooltip.</param>
    public PageToToolTipMapping([DisallowNull] KryptonPage page,
        MapKryptonPageImage mapImage,
        MapKryptonPageText mapText,
        MapKryptonPageText mapExtraText)
    {
        Debug.Assert(page != null);

        _page = page ?? throw new ArgumentNullException(nameof(page));
        _mapImage = mapImage;
        _mapText = mapText;
        _mapExtraText = mapExtraText;
    }
    #endregion

    #region HasContent
    /// <summary>
    /// Gets a value indicating if the mapping produces any content.
    /// </summary>
    public bool HasContent => (GetImage(PaletteState.Normal) != null) ||
                              !string.IsNullOrEmpty(GetShortText()) ||
                              !string.IsNullOrEmpty(GetLongText());

    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state) => _page.GetImageMapping(_mapImage);

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => _page.ToolTipImageTransparentColor;

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => _page.GetTextMapping(_mapText);

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => _page.GetTextMapping(_mapExtraText);

    /// <summary>
    /// Gets the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay image is needed.</param>
    /// <returns>Overlay image value, or null if no overlay image is set.</returns>
    public Image? GetOverlayImage(PaletteState state) => null;

    /// <summary>
    /// Gets the overlay image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the overlay image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetOverlayImageTransparentColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the position of the overlay image relative to the main image.
    /// </summary>
    /// <param name="state">The state for which the overlay position is needed.</param>
    /// <returns>Overlay image position.</returns>
    public OverlayImagePosition GetOverlayImagePosition(PaletteState state) => OverlayImagePosition.TopRight;

    /// <summary>
    /// Gets the scaling mode for the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay scale mode is needed.</param>
    /// <returns>Overlay image scale mode.</returns>
    public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) => OverlayImageScaleMode.None;

    /// <summary>
    /// Gets the scale factor for the overlay image (used when scale mode is Percentage or ProportionalToMain).
    /// </summary>
    /// <param name="state">The state for which the overlay scale factor is needed.</param>
    /// <returns>Scale factor (0.0 to 2.0).</returns>
    public float GetOverlayImageScaleFactor(PaletteState state) => 0.5f;

    /// <summary>
    /// Gets the fixed size for the overlay image (used when scale mode is FixedSize).
    /// </summary>
    /// <param name="state">The state for which the overlay fixed size is needed.</param>
    /// <returns>Fixed size.</returns>
    public Size GetOverlayImageFixedSize(PaletteState state) => new Size(16, 16);

    #endregion
}