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
/// Redirect general ribbon values based on the incoming state of the request.
/// </summary>
public class PaletteRedirectRibbonGeneral : PaletteRedirect
{
    #region Instance Fields
    private readonly IPaletteRibbonGeneral? _disabled;
    private readonly IPaletteRibbonGeneral? _normal;
    private readonly IPaletteRibbonGeneral? _pressed;
    private readonly IPaletteRibbonGeneral? _tracking;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectRibbonGeneral class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    public PaletteRedirectRibbonGeneral(PaletteBase target)
        : this(target, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectRibbonGeneral class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    /// <param name="pressed">Redirection for pressed state requests.</param>
    /// <param name="tracking">Redirection for tracking state requests.</param>
    public PaletteRedirectRibbonGeneral(PaletteBase target,
        IPaletteRibbonGeneral? disabled,
        IPaletteRibbonGeneral? normal,
        IPaletteRibbonGeneral? pressed,
        IPaletteRibbonGeneral? tracking
    )
        : base(target)
    {
        // Remember state specific inheritance
        _disabled = disabled;
        _normal = normal;
        _pressed = pressed;
        _tracking = tracking;
    }
    #endregion

    #region RibbonGeneral
    /// <summary>
    /// Gets the dark disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDisabledDark(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonDisabledDark(state) ?? Target!.GetRibbonDisabledDark(state);
    }

    /// <summary>
    /// Gets the light disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDisabledLight(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonDisabledLight(state) ?? Target!.GetRibbonDisabledLight(state);
    }

    /// <summary>
    /// Gets the color for the dialog launcher dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupDialogDark(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonGroupDialogDark(state) ?? Target!.GetRibbonGroupDialogDark(state);
    }

    /// <summary>
    /// Gets the color for the dialog launcher light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupDialogLight(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonGroupDialogLight(state) ?? Target!.GetRibbonGroupDialogLight(state);
    }

    /// <summary>
    /// Gets the color for the group separator dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupSeparatorDark(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonGroupSeparatorDark(state) ?? Target!.GetRibbonGroupSeparatorDark(state);
    }

    /// <summary>
    /// Gets the color for the group separator light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupSeparatorLight(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonGroupSeparatorLight(state) ?? Target!.GetRibbonGroupSeparatorLight(state);
    }

    /// <summary>
    /// Gets the color for the minimize bar dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonMinimizeBarDark(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonMinimizeBarDark(state) ?? Target!.GetRibbonMinimizeBarDark(state);
    }

    /// <summary>
    /// Gets the color for the minimize bar light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonMinimizeBarLight(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonMinimizeBarLight(state) ?? Target!.GetRibbonMinimizeBarLight(state);
    }

    /// <summary>
    /// Gets the color for the tab separator.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabSeparatorColor(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonTabSeparatorColor(state) ?? Target!.GetRibbonTabSeparatorColor(state);
    }

    /// <summary>
    /// Gets the color for the tab context separators.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabSeparatorContextColor(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonTabSeparatorContextColor(state) ?? Target!.GetRibbonTabSeparatorContextColor(state);
    }

    /// <summary>
    /// Gets the font for the ribbon text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetRibbonTextFont(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonTextFont(state) ?? Target!.GetRibbonTextFont(state);
    }

    /// <summary>
    /// Gets the rendering hint for the ribbon font.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetRibbonTextHint(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonTextHint(state) ?? Target!.GetRibbonTextHint(state);
    }

    /// <summary>
    /// Gets the color for the extra QAT button dark content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonQATButtonDark(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonQATButtonDark(state) ?? Target!.GetRibbonQATButtonDark(state);
    }

    /// <summary>
    /// Gets the color for the extra QAT button light content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonQATButtonLight(PaletteState state)
    {
        IPaletteRibbonGeneral? inherit = GetInherit(state);

        return inherit?.GetRibbonQATButtonLight(state) ?? Target!.GetRibbonQATButtonLight(state);
    }
    #endregion

    #region Implementation
    private IPaletteRibbonGeneral? GetInherit(PaletteState state)
    {
        switch (state)
        {
            case PaletteState.Disabled:
                return _disabled;
            case PaletteState.Normal:
                return _normal;
            case PaletteState.Pressed:
                return _pressed;
            case PaletteState.Tracking:
                return _tracking;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(state.ToString());
        }
    }
    #endregion
}