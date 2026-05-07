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

internal class RibbonGroupNormalDisabledTextToContent : RibbonToContent
{
    #region Instance Fields
    private readonly IPaletteRibbonText _ribbonGroupTextNormal;
    private readonly IPaletteRibbonText _ribbonGroupTextDisabled;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the RibbonGroupNormalDisabledTextToContent class.
    /// </summary>
    /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
    /// <param name="ribbonGroupTextNormal">Source for ribbon group button normal settings.</param>
    /// <param name="ribbonGroupTextDisabled">Source for ribbon group button disabled settings.</param>
    public RibbonGroupNormalDisabledTextToContent([DisallowNull] PaletteRibbonGeneral ribbonGeneral,
        [DisallowNull] IPaletteRibbonText ribbonGroupTextNormal,
        [DisallowNull] IPaletteRibbonText ribbonGroupTextDisabled)
        : base(ribbonGeneral)
    {
        Debug.Assert(ribbonGroupTextNormal is not null);
        Debug.Assert(ribbonGroupTextDisabled is not null);

        if (ribbonGroupTextNormal is null)
        {
            throw new ArgumentNullException(nameof(ribbonGroupTextNormal));
        }

        if (ribbonGroupTextDisabled is null)
        {
            throw new ArgumentNullException(nameof(ribbonGroupTextDisabled));
        }

        _ribbonGroupTextNormal = ribbonGroupTextNormal;
        _ribbonGroupTextDisabled = ribbonGroupTextDisabled;
    }
    #endregion

    #region IPaletteContent
    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteState state) => state == PaletteState.Disabled
        ? _ribbonGroupTextDisabled.GetRibbonTextColor(state)
        : _ribbonGroupTextNormal.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteState state) => state == PaletteState.Disabled
        ? _ribbonGroupTextDisabled.GetRibbonTextColor(state)
        : _ribbonGroupTextNormal.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteState state) => state == PaletteState.Disabled
        ? _ribbonGroupTextDisabled.GetRibbonTextColor(state)
        : _ribbonGroupTextNormal.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteState state) => state == PaletteState.Disabled
        ? _ribbonGroupTextDisabled.GetRibbonTextColor(state)
        : _ribbonGroupTextNormal.GetRibbonTextColor(state);

    #endregion
}