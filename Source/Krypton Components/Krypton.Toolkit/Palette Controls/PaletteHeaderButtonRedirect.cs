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
/// Redirect storage for button metrics.
/// </summary>
public class PaletteHeaderButtonRedirect : PaletteTripleMetricRedirect
{
    #region Instance Fields
    private readonly PaletteRedirect _redirect;
    private Padding _buttonPadding;
    private int _buttonEdgeInset;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteHeaderButtonRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backStyle">Initial background style.</param>
    /// <param name="borderStyle">Initial border style.</param>
    /// <param name="contentStyle">Initial content style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteHeaderButtonRedirect([DisallowNull] PaletteRedirect redirect,
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
        _buttonPadding = CommonHelper.InheritPadding;
        _buttonEdgeInset = -1;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => base.IsDefault &&
                                      ButtonPadding.Equals(CommonHelper.InheritPadding) &&
                                      (ButtonEdgeInset == -1);

    #endregion

    #region ButtonEdgeInset
    /// <summary>
    /// Gets the sets how far to inset buttons from the header edge.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"How far to inset buttons from the header edge.")]
    [DefaultValue(-1)]
    [RefreshProperties(RefreshProperties.All)]
    public int ButtonEdgeInset
    {
        get => _buttonEdgeInset;

        set
        {
            if (_buttonEdgeInset != value)
            {
                _buttonEdgeInset = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Reset the ButtonEdgeInset to the default value.
    /// </summary>
    public void ResetButtonEdgeInset() => ButtonEdgeInset = -1;
    #endregion

    #region ButtonPadding
    /// <summary>
    /// Gets and sets the padding used around each button on the header.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Padding used around each button on the header.")]
    [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
    [RefreshProperties(RefreshProperties.All)]
    public Padding ButtonPadding
    {
        get => _buttonPadding;

        set
        {
            if (_buttonPadding != value)
            {
                _buttonPadding = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Reset the ButtonPadding to the default value.
    /// </summary>
    public void ResetButtonPadding() => ButtonPadding = CommonHelper.InheritPadding;
    #endregion

    #region IPaletteMetric

    /// <summary>
    /// Gets an integer metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Integer value.</returns>
    public override int GetMetricInt(KryptonForm? owningForm, PaletteState state, PaletteMetricInt metric)
    {
        return metric switch
        {
            // Is this the metric we provide?
            // If the user has defined an actual value to use
            PaletteMetricInt.HeaderButtonEdgeInsetPrimary or PaletteMetricInt.HeaderButtonEdgeInsetSecondary
                or PaletteMetricInt.HeaderButtonEdgeInsetDockInactive
                or PaletteMetricInt.HeaderButtonEdgeInsetDockActive or PaletteMetricInt.HeaderButtonEdgeInsetForm
                or PaletteMetricInt.HeaderButtonEdgeInsetInputControl
                or PaletteMetricInt.HeaderButtonEdgeInsetCustom1 or PaletteMetricInt.HeaderButtonEdgeInsetCustom2
                or PaletteMetricInt.HeaderButtonEdgeInsetCustom3 when ButtonEdgeInset != -1 => ButtonEdgeInset,
            _ => _redirect.GetMetricInt(owningForm, state, metric)
        };

        // Pass onto the inheritance
    }

    /// <summary>
    /// Gets a boolean metric value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric) =>
        // Always pass onto the inheritance
        _redirect.GetMetricBool(state, metric);

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
        if (metric is PaletteMetricPadding.HeaderButtonPaddingPrimary
            or PaletteMetricPadding.HeaderButtonPaddingSecondary
            or PaletteMetricPadding.HeaderButtonPaddingDockInactive
            or PaletteMetricPadding.HeaderButtonPaddingDockActive 
            //or PaletteMetricPadding.HeaderButtonPaddingForm
            or PaletteMetricPadding.HeaderButtonPaddingInputControl
            or PaletteMetricPadding.HeaderButtonPaddingCustom1
            or PaletteMetricPadding.HeaderButtonPaddingCustom2
            or PaletteMetricPadding.HeaderButtonPaddingCustom3
           )
        {
            // If the user has defined an actual value to use
            if (!ButtonPadding.Equals(CommonHelper.InheritPadding))
            {
                return ButtonPadding;
            }
        }

        // Pass onto the inheritance
        return _redirect.GetMetricPadding(owningForm, state, metric);
    }
    #endregion

}