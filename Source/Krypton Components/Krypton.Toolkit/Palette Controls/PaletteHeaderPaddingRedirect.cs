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
/// Redirect storage for headers within a HeaderGroup state.
/// </summary>
public class PaletteHeaderPaddingRedirect : PaletteHeaderButtonRedirect
{
    #region Instance Fields
    private readonly PaletteRedirect _redirect;
    private Padding _headerPadding;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteHeaderPaddingRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backStyle">Initial background style.</param>
    /// <param name="borderStyle">Initial border style.</param>
    /// <param name="contentStyle">Initial content style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteHeaderPaddingRedirect([DisallowNull] PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        PaletteContentStyle contentStyle,
        NeedPaintHandler needPaint)
        : base(redirect, backStyle, borderStyle, contentStyle, needPaint)
    {
        Debug.Assert(redirect != null);

        // Remember the redirect reference
        _redirect = redirect!;

        // Set default value for padding property
        _headerPadding = CommonHelper.InheritPadding;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => base.IsDefault &&
                                      HeaderPadding.Equals(CommonHelper.InheritPadding);

    #endregion

    #region HeaderPadding
    /// <summary>
    /// Gets and sets the padding used to inset the header within the HeaderGroup
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Padding used to inset the header within the HeaderGroup.")]
    [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
    [RefreshProperties(RefreshProperties.All)]
    public Padding HeaderPadding
    {
        get => _headerPadding;

        set
        {
            if (_headerPadding != value)
            {
                _headerPadding = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Reset the HeaderPadding to the default value.
    /// </summary>
    public void ResetHeaderPadding() => HeaderPadding = CommonHelper.InheritPadding;
    #endregion

    #region IPaletteMetric

    /// <summary>
    /// Gets a padding metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Padding value.</returns>
    public override Padding GetMetricPadding(KryptonForm? owningForm, PaletteState state,
        PaletteMetricPadding metric)
    {
        // Is this the metric we provide?
        if (metric is PaletteMetricPadding.HeaderGroupPaddingPrimary or PaletteMetricPadding.HeaderGroupPaddingSecondary or PaletteMetricPadding.HeaderGroupPaddingDockInactive or PaletteMetricPadding.HeaderGroupPaddingDockActive)
        {
            // If the user has defined an actual value to use
            if (!HeaderPadding.Equals(CommonHelper.InheritPadding))
            {
                return HeaderPadding;
            }
        }

        // Let base class perform its own testing
        return base.GetMetricPadding(owningForm, state, metric);
    }
    #endregion
}