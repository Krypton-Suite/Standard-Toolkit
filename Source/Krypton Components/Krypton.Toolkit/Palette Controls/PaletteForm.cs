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
/// Implement storage for PaletteForm states.
/// </summary>
public class PaletteForm : PaletteDouble,
    IPaletteMetric
{
    #region Instance Fields
    private IPaletteMetric _inherit;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteForm class.
    /// </summary>
    /// <param name="inheritForm">Source for inheriting palette defaulted values.</param>
    /// <param name="inheritHeader">Source for inheriting header defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteForm([DisallowNull] PaletteFormRedirect inheritForm,
        [DisallowNull] PaletteTripleMetricRedirect inheritHeader,
        NeedPaintHandler needPaint)
        : base(inheritForm, needPaint)
    {
        Debug.Assert(inheritForm != null);
        Debug.Assert(inheritHeader != null);

        // Remember the inheritance
        _inherit = inheritForm!;

        // Create the palette storage
        Header = new PaletteTripleMetric(inheritHeader!, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => base.IsDefault && Header.IsDefault;

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    /// <param name="inheritHeader">Source for inheriting.</param>
    public void SetInherit(PaletteForm inheritHeader)
    {
        base.SetInherit(inheritHeader);
        _inherit = inheritHeader;
        Header.SetInherit(inheritHeader.Header);
    }
    #endregion

    #region Header
    /// <summary>
    /// Gets access to the header appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleMetric Header { get; }

    private bool ShouldSerializeHeader() => !Header.IsDefault;

    #endregion

    #region IPaletteMetric

    /// <summary>
    /// Gets an integer metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Integer value.</returns>
    public int GetMetricInt(KryptonForm? owningForm, PaletteState state, PaletteMetricInt metric) =>
        // Pass onto the inheritance
        _inherit.GetMetricInt(owningForm, state, metric);

    /// <summary>
    /// Gets a boolean metric value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric) =>
        // Pass onto the inheritance
        _inherit.GetMetricBool(state, metric);

    /// <summary>
    /// Gets a padding metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Padding value.</returns>
    public Padding GetMetricPadding(KryptonForm? owningForm, PaletteState state, PaletteMetricPadding metric) =>
        // Always pass onto the inheritance
        _inherit.GetMetricPadding(owningForm, state, metric);

    #endregion
}