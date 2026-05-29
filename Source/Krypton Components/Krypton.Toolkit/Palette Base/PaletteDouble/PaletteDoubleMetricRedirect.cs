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
/// Redirect storage for a double palette with palette metrics.
/// </summary>
public class PaletteDoubleMetricRedirect : PaletteDoubleRedirect,
    IPaletteMetric
{
    #region Instance Fields
    private PaletteRedirect _redirect;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteDoubleMetricRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backStyle">Style for the background.</param>
    /// <param name="borderStyle">Style for the border.</param>
    public PaletteDoubleMetricRedirect(PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle)
        : this(redirect, backStyle, borderStyle, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteDoubleMetricRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backStyle">Style for the background.</param>
    /// <param name="borderStyle">Style for the border.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteDoubleMetricRedirect(PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        NeedPaintHandler? needPaint)
        : base(redirect,
            backStyle,
            borderStyle,
            needPaint) =>
        // Remember the redirect reference
        _redirect = redirect;

    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public override void SetRedirector(PaletteRedirect redirect)
    {
        base.SetRedirector(redirect);
        _redirect = redirect;
    }
    #endregion

    #region IPaletteMetric

    /// <summary>
    /// Gets an integer metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Integer value.</returns>
    public virtual int GetMetricInt(KryptonForm? owningForm, PaletteState state, PaletteMetricInt metric) =>
        // Pass onto the inheritance
        _redirect.GetMetricInt(owningForm, state, metric);

    /// <summary>
    /// Gets a boolean metric value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>InheritBool value.</returns>
    public virtual InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric) =>
        // Pass onto the inheritance
        _redirect.GetMetricBool(state, metric);

    /// <summary>
    /// Gets a padding metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Padding value.</returns>
    public virtual Padding GetMetricPadding(KryptonForm? owningForm, PaletteState state, PaletteMetricPadding metric) =>
        // Pass onto the inheritance
        _redirect.GetMetricPadding(owningForm, state, metric);

    #endregion
}