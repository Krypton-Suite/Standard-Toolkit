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
/// Redirect back/border ribbon values based on the incoming state of the request.
/// </summary>
public class PaletteRedirectRibbonBack : PaletteRedirect
{
    #region Instance Fields
    private IPaletteRibbonBack? _disabledBack;
    private IPaletteRibbonBack? _normalBack;
    private IPaletteRibbonBack? _pressedBack;
    private IPaletteRibbonBack? _trackingBack;
    private IPaletteRibbonBack? _selectedBack;
    private IPaletteRibbonBack? _focusOverrideBack;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectRibbonBack class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    public PaletteRedirectRibbonBack(PaletteBase target)
        : this(target, null, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectRibbonBack class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabledBack">Redirection for back disabled state requests.</param>
    /// <param name="normalBack">Redirection for back normal state requests.</param>
    public PaletteRedirectRibbonBack(PaletteBase target,
        IPaletteRibbonBack disabledBack,
        IPaletteRibbonBack normalBack)
        : this(target, disabledBack, normalBack, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectRibbonBack class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabledBack">Redirection for back disabled state requests.</param>
    /// <param name="normalBack">Redirection for back normal state requests.</param>
    /// <param name="pressedBack">Redirection for back pressed state requests.</param>
    /// <param name="trackingBack">Redirection for back tracking state requests.</param>
    /// <param name="selectedBack">Redirection for selected states requests.</param>
    /// <param name="focusOverrideBack">Redirection for back focus override state requests.</param>
    public PaletteRedirectRibbonBack(PaletteBase target,
        IPaletteRibbonBack? disabledBack,
        IPaletteRibbonBack? normalBack,
        IPaletteRibbonBack? pressedBack,
        IPaletteRibbonBack? trackingBack,
        IPaletteRibbonBack? selectedBack,
        IPaletteRibbonBack? focusOverrideBack)
        : base(target)
    {
        // Remember state specific inheritance
        _disabledBack = disabledBack;
        _normalBack = normalBack;
        _pressedBack = pressedBack;
        _trackingBack = trackingBack;
        _selectedBack = selectedBack;
        _focusOverrideBack = focusOverrideBack;
    }
    #endregion

    #region SetRedirectStates
    /// <summary>
    /// Set the redirection states.
    /// </summary>
    /// <param name="disabledBack">Redirection for back disabled state requests.</param>
    /// <param name="normalBack">Redirection for back normal state requests.</param>
    public virtual void SetRedirectStates(IPaletteRibbonBack disabledBack,
        IPaletteRibbonBack normalBack)
    {
        _disabledBack = disabledBack;
        _normalBack = normalBack;
        _pressedBack = null;
        _trackingBack = null;
        _selectedBack = null;
        _focusOverrideBack = null;
    }

    /// <summary>
    /// Set the redirection states.
    /// </summary>
    /// <param name="disabledBack">Redirection for back disabled state requests.</param>
    /// <param name="normalBack">Redirection for back normal state requests.</param>
    /// <param name="pressedBack">Redirection for back pressed state requests.</param>
    /// <param name="trackingBack">Redirection for back tracking state requests.</param>
    /// <param name="selectedBack">Redirection for selected states requests.</param>
    /// <param name="focusOverrideBack">Redirection for back focus override state requests.</param>
    public virtual void SetRedirectStates(IPaletteRibbonBack disabledBack,
        IPaletteRibbonBack normalBack,
        IPaletteRibbonBack pressedBack,
        IPaletteRibbonBack trackingBack,
        IPaletteRibbonBack selectedBack,
        IPaletteRibbonBack focusOverrideBack)
    {
        _disabledBack = disabledBack;
        _normalBack = normalBack;
        _pressedBack = pressedBack;
        _trackingBack = trackingBack;
        _selectedBack = selectedBack;
        _focusOverrideBack = focusOverrideBack;
    }
    #endregion

    #region ResetRedirectStates
    /// <summary>
    /// Reset the redirection states to null.
    /// </summary>
    public virtual void ResetRedirectStates()
    {
        _disabledBack = null;
        _normalBack = null;
        _pressedBack = null;
        _trackingBack = null;
        _selectedBack = null;
        _focusOverrideBack = null;
    }
    #endregion

    #region BackColorStyle
    /// <summary>
    /// Gets the background drawing style for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="style">Style of the ribbon back style requested.</param>
    /// <returns>Color value.</returns>
    public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state)
    {
        IPaletteRibbonBack? inherit = GetBackInherit(state);

        return inherit?.GetRibbonBackColorStyle(state) ?? Target!.GetRibbonBackColorStyle(style, state);
    }
    #endregion

    #region BackColor1
    /// <summary>
    /// Gets the first background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="style">Style of the ribbon color requested.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor1(PaletteRibbonBackStyle style, PaletteState state)
    {
        IPaletteRibbonBack? inherit = GetBackInherit(state);

        return inherit?.GetRibbonBackColor1(state) ?? Target!.GetRibbonBackColor1(style, state);
    }
    #endregion

    #region BackColor2
    /// <summary>
    /// Gets the second background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="style">Style of the ribbon color requested.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor2(PaletteRibbonBackStyle style, PaletteState state)
    {
        IPaletteRibbonBack? inherit = GetBackInherit(state);

        return inherit?.GetRibbonBackColor2(state) ?? Target!.GetRibbonBackColor2(style, state);
    }
    #endregion

    #region BackColor3
    /// <summary>
    /// Gets the third background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="style">Style of the ribbon color requested.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor3(PaletteRibbonBackStyle style, PaletteState state)
    {
        IPaletteRibbonBack? inherit = GetBackInherit(state);

        return inherit?.GetRibbonBackColor3(state) ?? Target!.GetRibbonBackColor3(style, state);
    }
    #endregion

    #region BackColor4
    /// <summary>
    /// Gets the fourth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="style">Style of the ribbon color requested.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor4(PaletteRibbonBackStyle style, PaletteState state)
    {
        IPaletteRibbonBack? inherit = GetBackInherit(state);

        return inherit?.GetRibbonBackColor4(state) ?? Target!.GetRibbonBackColor4(style, state);
    }
    #endregion

    #region BackColor5
    /// <summary>
    /// Gets the fifth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="style">Style of the ribbon color requested.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor5(PaletteRibbonBackStyle style, PaletteState state)
    {
        IPaletteRibbonBack? inherit = GetBackInherit(state);

        return inherit?.GetRibbonBackColor5(state) ?? Target!.GetRibbonBackColor5(style, state);
    }
    #endregion

    #region Implementation
    private IPaletteRibbonBack? GetBackInherit(PaletteState state)
    {
        switch (state)
        {
            case PaletteState.Disabled:
                return _disabledBack;
            case PaletteState.Normal:
                return _normalBack;
            case PaletteState.Pressed:
                return _pressedBack;
            case PaletteState.Tracking:
                return _trackingBack;
            case PaletteState.CheckedNormal:
            case PaletteState.CheckedPressed:
            case PaletteState.CheckedTracking:
                return _selectedBack;
            case PaletteState.FocusOverride:
                return _focusOverrideBack;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(state.ToString());
        }
    }
    #endregion
}