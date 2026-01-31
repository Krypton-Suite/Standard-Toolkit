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
/// Map button spec tooltip value to content values.
/// </summary>
public class ButtonSpecToContent : IContentValues
{
    #region Instance Fields
    private readonly ButtonSpec _buttonSpec;
    private readonly PaletteBase _palette;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PageToTooltipMapping class.
    /// </summary>
    /// <param name="palette">Palette for sourcing information.</param>
    /// <param name="buttonSpec">Source button spec instance.</param>
    public ButtonSpecToContent([DisallowNull] PaletteBase palette,
        [DisallowNull] ButtonSpec buttonSpec)
    {
        Debug.Assert(palette != null);
        Debug.Assert(buttonSpec != null);
        _palette = palette!;
        _buttonSpec = buttonSpec!;
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
    public Image? GetImage(PaletteState state) => _buttonSpec.ToolTipImage;

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => _buttonSpec.ToolTipImageTransparentColor;

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => _buttonSpec.GetToolTipTitle(_palette);

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => _buttonSpec.ToolTipBody;

    #endregion
}