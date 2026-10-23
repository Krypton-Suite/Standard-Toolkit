#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Storage for knob scale colours that default to palette control label text.
/// </summary>
public class PaletteKnobTickColorRedirect : PaletteElementColor
{
    #region Instance Fields
    private readonly PaletteKnobTickColorInheritRedirect _inherit;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteKnobTickColorRedirect"/> class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteKnobTickColorRedirect(PaletteRedirect redirect, NeedPaintHandler? needPaint)
        : base(null, needPaint)
    {
        _inherit = new PaletteKnobTickColorInheritRedirect(redirect);
        SetInherit(_inherit);
    }
    #endregion

    #region Public
    /// <summary>
    /// Update the redirector with a new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public virtual void SetRedirector(PaletteRedirect redirect) => _inherit.SetRedirector(redirect);
    #endregion
}
