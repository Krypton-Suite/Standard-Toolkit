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
/// Storage for element color values.
/// </summary>
public class PaletteElementColorRedirect : PaletteElementColor
{
    #region Instance Fields
    private readonly PaletteElementColorInheritRedirect _redirect;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteElementColorRedirect class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="element">Element value.</param>
    /// <param name="needPaint">Delegate for notifying changes in value.</param>
    public PaletteElementColorRedirect(PaletteRedirect redirect,
        PaletteElement element,
        NeedPaintHandler? needPaint)
        : base(null, needPaint)
    {
        // Setup inheritance to recover values from the redirect instance
        _redirect = new PaletteElementColorInheritRedirect(redirect, element);
        SetInherit(_redirect);
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public virtual void SetRedirector(PaletteRedirect redirect) => _redirect.SetRedirector(redirect);

    #endregion
}