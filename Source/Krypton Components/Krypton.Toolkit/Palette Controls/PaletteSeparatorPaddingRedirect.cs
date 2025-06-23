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
/// Implement storage for palette border,background and separator padding.
/// </summary>
public class PaletteSeparatorPaddingRedirect : PaletteDoubleMetricRedirect
                                            
{
    #region Instance Fields
    private readonly PaletteRedirect? _redirect;
    private Padding _separatorPadding;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteSeparatorPaddingRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backStyle">Initial background style.</param>
    /// <param name="borderStyle">Initial border style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteSeparatorPaddingRedirect([DisallowNull] PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        NeedPaintHandler needPaint)
        : base(redirect, backStyle, borderStyle, needPaint)
    {
        Debug.Assert(redirect != null);

        // Remember the redirect reference
        _redirect = redirect;
            
        // Set default value for padding property
        _separatorPadding = CommonHelper.InheritPadding;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => base.IsDefault &&
                                      Padding.Equals(CommonHelper.InheritPadding);

    #endregion

    #region Padding
    /// <summary>
    /// Gets the padding used to position the separator.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Padding used to position the separator.")]
    [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
    [RefreshProperties(RefreshProperties.All)]
    public Padding Padding
    {
        get => _separatorPadding;

        set
        {
            if (_separatorPadding != value)
            {
                _separatorPadding = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Reset the Padding to the default value.
    /// </summary>
    public void ResetPadding() => Padding = CommonHelper.InheritPadding;
    #endregion

    #region IPaletteMetric

    /// <summary>
    /// Gets an integer metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Integer value.</returns>
    public override int GetMetricInt(KryptonForm? owningForm, PaletteState state, PaletteMetricInt metric) =>
        // Pass onto the inheritance
        _redirect!.GetMetricInt(owningForm, state, metric);

    /// <summary>
    /// Gets a boolean metric value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric) =>
        // Pass onto the inheritance
        _redirect!.GetMetricBool(state, metric);

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
        if (metric is PaletteMetricPadding.SeparatorPaddingLowProfile 
            or PaletteMetricPadding.SeparatorPaddingHighProfile 
            or PaletteMetricPadding.SeparatorPaddingHighInternalProfile
            or PaletteMetricPadding.SeparatorPaddingCustom1
            or PaletteMetricPadding.SeparatorPaddingCustom2
            or PaletteMetricPadding.SeparatorPaddingCustom3
           )
        {
            // If the user has defined an actual value to use
            return !Padding.Equals(CommonHelper.InheritPadding)
                ? Padding
                : _redirect!.GetMetricPadding(owningForm, state, metric);
        }

        // Pass onto the inheritance
        return _redirect!.GetMetricPadding(owningForm, state, metric);
    }
    #endregion
}