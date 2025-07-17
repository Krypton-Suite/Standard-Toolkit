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
/// Provide inheritance of palette element colors from source redirector.
/// </summary>
public class PaletteElementColorInheritRedirect : PaletteElementColorInherit
{
    #region Instance Fields
    private PaletteRedirect _redirect;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteElementColorInheritRedirect class.
    /// </summary>
    /// <param name="redirect">Source for inherit requests.</param>
    /// <param name="element">Element value..</param>
    public PaletteElementColorInheritRedirect(PaletteRedirect redirect,
        PaletteElement element)
    {
        // Debug.Assert() causes the null assignment warning.
        // Suppressed by the null forgiving operator
        Debug.Assert(redirect is not null);

        _redirect = redirect!;
        Element = element;
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
    /// Gets and sets the element to use when inheriting.
    /// </summary>
    public PaletteElement Element { get; set; }

    #endregion

    #region IPaletteElementColor
    /// <summary>
    /// Gets the first color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor1(PaletteState state) => _redirect.GetElementColor1(Element, state);

    /// <summary>
    /// Gets the second color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor2(PaletteState state) => _redirect.GetElementColor2(Element, state);

    /// <summary>
    /// Gets the third color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor3(PaletteState state) => _redirect.GetElementColor3(Element, state);

    /// <summary>
    /// Gets the fourth color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor4(PaletteState state) => _redirect.GetElementColor4(Element, state);

    /// <summary>
    /// Gets the fifth color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor5(PaletteState state) => _redirect.GetElementColor5(Element, state);

    #endregion
}