#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Map application button tooltip values to content values.
/// </summary>
internal class AppButtonToolTipToContent : IContentValues
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the AppButtonToolTipToContent class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon instance.</param>
    public AppButtonToolTipToContent([DisallowNull] KryptonRibbon ribbon)
    {
        Debug.Assert(ribbon != null);
        _ribbon = ribbon!;
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
    public Image? GetImage(PaletteState state) => _ribbon.RibbonFileAppButton.AppButtonToolTipImage;

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => _ribbon.RibbonFileAppButton.AppButtonToolTipImageTransparentColor;

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => _ribbon.RibbonFileAppButton.AppButtonToolTipTitle;

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => _ribbon.RibbonFileAppButton.AppButtonToolTipBody;

    #endregion
}