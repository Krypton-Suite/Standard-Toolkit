#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// View for drawing an individual key tip.
/// </summary>
internal class ViewDrawRibbonKeyTip : ViewDrawDocker,
    IContentValues
{
    #region Instance Fields

    private readonly ViewDrawContent _drawContent;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonKeyTip class.
    /// </summary>
    /// <param name="keyTipInfo">Key tip information to display.</param>
    /// <param name="paletteBack">Background palette for appearance.</param>
    /// <param name="paletteBorder">Border palette for appearance.</param>
    /// <param name="paletteContent">Content palette for appearance.</param>
    public ViewDrawRibbonKeyTip(KeyTipInfo keyTipInfo,
        IPaletteBack paletteBack,
        IPaletteBorder paletteBorder,
        IPaletteContent? paletteContent)
        : base(paletteBack, paletteBorder)
    {
        KeyTipInfo = keyTipInfo;

        // Create view for the key tip text
        _drawContent = new ViewDrawContent(paletteContent, this, VisualOrientation.Top);

        // Add content as filler for ourself
        Add(_drawContent, ViewDockStyle.Fill);
    }
    #endregion

    #region KeyTipInfo
    /// <summary>
    /// Gets the associated key tip info.
    /// </summary>
    public KeyTipInfo KeyTipInfo { get; }

    #endregion

    #region IContent
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state) => null;

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => KeyTipInfo.KeyString;

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => string.Empty;

    #endregion
}