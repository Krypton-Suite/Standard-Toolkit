#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide inheritance of palette ribbon "File App Tab" properties from source redirector.
/// </summary>
public class PaletteRibbonFileAppTabInheritRedirect : PaletteRibbonFileAppTabInherit
{
    #region Instance Fields
    private PaletteRedirect _redirect;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonGeneralInheritRedirect class.
    /// </summary>
    /// <param name="redirect">Source for inherit requests.</param>
    public PaletteRibbonFileAppTabInheritRedirect([DisallowNull] PaletteRedirect redirect)
    {
        Debug.Assert(redirect != null);
        _redirect = redirect!;
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect) => _redirect = redirect;
    #endregion

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => _redirect.GetRibbonFileAppTabBottomColor(state);

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => _redirect.GetRibbonFileAppTabTopColor(state);

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => _redirect.GetRibbonFileAppTabTextColor(state);
}