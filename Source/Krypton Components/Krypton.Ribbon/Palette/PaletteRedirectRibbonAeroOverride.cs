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
/// Override the text colors for button specs that are drawn on aero glass.
/// </summary>
public class PaletteRedirectRibbonAeroOverride : PaletteRedirect
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectRibbonAeroOverride class.
    /// </summary>
    /// <param name="ribbon">Reference to owning Ribbon instance.</param>
    /// <param name="redirect">Source for inheriting values.</param>
    public PaletteRedirectRibbonAeroOverride(KryptonRibbon ribbon,
        PaletteRedirect redirect)
        : base(redirect) =>
        _ribbon = ribbon;

    #endregion

    #region ShortText
    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        // We only want to override buttons specs that are drawing in normal mode
        if ((style == PaletteContentStyle.ButtonButtonSpec) && (state == PaletteState.Normal))
        {
            // If the ribbon is showing in office 2010 style and using glass
            if (RibbonShapeIs2010OrHigher())
            {
                return LightBackground(base.GetContentShortTextColor1(style, state));
            }
        }

        return base.GetContentShortTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        // We only want to override buttons specs that are drawing in normal mode
        if ((style == PaletteContentStyle.ButtonButtonSpec) && (state == PaletteState.Normal))
        {
            // If the ribbon is showing in office 2010 style and using glass
            if (RibbonShapeIs2010OrHigher())
            {
                return LightBackground(base.GetContentShortTextColor2(style, state));
            }
        }

        return base.GetContentShortTextColor2(style, state);
    }
    #endregion

    #region LongText
    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
    {
        // We only want to override buttons specs that are drawing in normal mode
        if ((style == PaletteContentStyle.ButtonButtonSpec) && (state == PaletteState.Normal))
        {
            // If the ribbon is showing in office 2010 style and using glass
            if ( RibbonShapeIs2010OrHigher())
            {
                return LightBackground(base.GetContentLongTextColor1(style, state));
            }
        }

        return base.GetContentLongTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
    {
        // We only want to override buttons specs that are drawing in normal mode
        if ((style == PaletteContentStyle.ButtonButtonSpec) && (state == PaletteState.Normal))
        {
            // If the ribbon is showing in office 2010 style and using glass
            if ( RibbonShapeIs2010OrHigher())
            {
                return LightBackground(base.GetContentLongTextColor2(style, state));
            }
        }

        return base.GetContentLongTextColor2(style, state);
    }
    #endregion

    #region Implementation
    private Color LightBackground(Color retColor) =>
        // With a light background we force the color to be dark in normal state so it stands out
        Color.FromArgb(Math.Min(retColor.R, (byte)60),
            Math.Min(retColor.G, (byte)60),
            Math.Min(retColor.B, (byte)60));

    private bool RibbonShapeIs2010OrHigher() => _ribbon.RibbonShape is PaletteRibbonShape.Office2010 or PaletteRibbonShape.VisualStudio2010 or PaletteRibbonShape.VisualStudio2010 or PaletteRibbonShape.Office2013 or PaletteRibbonShape.Microsoft365 or PaletteRibbonShape.VisualStudio;
    #endregion
}