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
/// Implement a triple palette that exposes palette metrics.
/// </summary>
public class PaletteTripleMetric : PaletteTriple, 
    IPaletteMetric
{
    #region Instance Fields
    private PaletteTripleMetricRedirect _inherit;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteTripleMetric class.
    /// </summary>
    /// <param name="inherit">Source for palette defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteTripleMetric([DisallowNull] PaletteTripleMetricRedirect inherit,
        NeedPaintHandler needPaint)
        : base(inherit, needPaint)
    {
        Debug.Assert(inherit is not null);
            
        // Remember inheritance for metric values
        _inherit = inherit ?? throw new ArgumentNullException(nameof(inherit));
    }
    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public void SetInherit(PaletteTripleMetricRedirect inherit)
    {
        base.SetInherit(inherit);
        _inherit = inherit;
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
        // Always pass onto the inheritance
        _inherit.GetMetricInt(owningForm, state, metric);

    /// <summary>
    /// Gets a boolean metric value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>InheritBool value.</returns>
    public virtual InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric) =>
        // Always pass onto the inheritance
        _inherit.GetMetricBool(state, metric);

    /// <summary>
    /// Gets a padding metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Padding value.</returns>
    public virtual Padding GetMetricPadding(KryptonForm? owningForm, PaletteState state,
        PaletteMetricPadding metric) =>
        // Always pass onto the inheritance
        _inherit.GetMetricPadding(owningForm, state, metric);

    #endregion
}