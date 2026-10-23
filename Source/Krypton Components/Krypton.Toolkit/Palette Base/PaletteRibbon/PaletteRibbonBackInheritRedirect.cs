#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide inheritance of palette ribbon background properties from source redirector.
/// </summary>
public class PaletteRibbonBackInheritRedirect : PaletteRibbonBackInherit
{
    #region Instance Fields
    private PaletteRedirect _redirect;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonBackInheritRedirect class.
    /// </summary>
    /// <param name="redirect">Source for inherit requests.</param>
    /// <param name="styleBack">Ribbon item background style.</param>
    public PaletteRibbonBackInheritRedirect([DisallowNull] PaletteRedirect redirect,
        PaletteRibbonBackStyle styleBack)
    {
        Debug.Assert(redirect != null);

        _redirect = redirect!;
        StyleBack = styleBack;
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect) => _redirect = redirect;

    #endregion

    #region StyleBack
    /// <summary>
    /// Gets and sets the ribbon background style to use when inheriting.
    /// </summary>
    public PaletteRibbonBackStyle StyleBack { get; set; }

    #endregion

    #region IPaletteRibbonBack
    /// <summary>
    /// Gets the method used to draw the background of a ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteRibbonBackStyle value.</returns>
    public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state) => _redirect.GetRibbonBackColorStyle(StyleBack, state);

    /// <summary>
    /// Gets the first background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor1(PaletteState state) => _redirect.GetRibbonBackColor1(StyleBack, state);

    /// <summary>
    /// Gets the second background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor2(PaletteState state) => _redirect.GetRibbonBackColor2(StyleBack, state);

    /// <summary>
    /// Gets the third background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor3(PaletteState state) => _redirect.GetRibbonBackColor3(StyleBack, state);

    /// <summary>
    /// Gets the fourth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor4(PaletteState state) => _redirect.GetRibbonBackColor4(StyleBack, state);

    /// <summary>
    /// Gets the fifth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor5(PaletteState state) => _redirect.GetRibbonBackColor5(StyleBack, state);

    #endregion
}