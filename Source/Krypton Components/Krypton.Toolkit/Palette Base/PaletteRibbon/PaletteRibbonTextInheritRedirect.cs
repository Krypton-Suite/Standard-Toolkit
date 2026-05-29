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
/// Provide inheritance of palette ribbon text properties from source redirector.
/// </summary>
public class PaletteRibbonTextInheritRedirect : PaletteRibbonTextInherit
{
    #region Instance Fields
    private PaletteRedirect _redirect;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonTextInheritRedirect class.
    /// </summary>
    /// <param name="redirect">Source for inherit requests.</param>
    /// <param name="styleText">Ribbon item text style.</param>
    public PaletteRibbonTextInheritRedirect(PaletteRedirect redirect,
        PaletteRibbonTextStyle styleText)
    {
        // Debug.Assert() causes the null assignment warning.
        // Suppressed by the null forgiving operator
        Debug.Assert(redirect is not null);

        _redirect = redirect!;
        StyleText = styleText;
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect) => _redirect = redirect;
    #endregion

    #region StyleText
    /// <summary>
    /// Gets and sets the ribbon text style to use when inheriting.
    /// </summary>
    public PaletteRibbonTextStyle StyleText { get; set; }

    #endregion

    #region IPaletteRibbonBack
    /// <summary>
    /// Gets the tab color for the item text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTextColor(PaletteState state) => _redirect.GetRibbonTextColor(StyleText, state);

    #endregion
}