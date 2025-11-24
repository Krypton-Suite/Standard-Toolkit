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
/// Implement storage for palette border, background and content for input control common state.
/// </summary>
public class PaletteInputControlTripleRedirect : Storage,
    IPaletteTriple,
    IPaletteMetric
{
    #region Instance Fields

    private readonly PaletteBackInheritRedirect _backInherit;
    private readonly PaletteBorderInheritRedirect _borderInherit;
    private readonly PaletteContentInheritRedirect _contentInherit;
    private readonly PaletteMetricRedirect _metricRedirect;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteInputControlTripleRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backStyle">Initial background style.</param>
    /// <param name="borderStyle">Initial border style.</param>
    /// <param name="contentStyle">Initial content style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteInputControlTripleRedirect([DisallowNull] PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        PaletteContentStyle contentStyle,
        NeedPaintHandler? needPaint)
    {
        Debug.Assert(redirect != null);

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Store the inherit instances
        _backInherit = new PaletteBackInheritRedirect(redirect!, backStyle);
        _borderInherit = new PaletteBorderInheritRedirect(redirect!, borderStyle);
        _contentInherit = new PaletteContentInheritRedirect(redirect!, contentStyle);
        _metricRedirect = new PaletteMetricRedirect(redirect!);

        // Create storage that maps onto the inherit instances
        Back = new PaletteInputControlBackStates(_backInherit, needPaint);
        Border = new PaletteBorder(_borderInherit, needPaint);
        Content = new PaletteInputControlContentStates(_contentInherit, needPaint!);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Back.IsDefault &&
                                      Border.IsDefault &&
                                      Content.IsDefault;

    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public virtual void SetRedirector(PaletteRedirect redirect)
    {
        _backInherit.SetRedirector(redirect);
        _borderInherit.SetRedirector(redirect);
        _contentInherit.SetRedirector(redirect);
        _metricRedirect.SetRedirector(redirect);
    }
    #endregion

    #region SetStyles
    /// <summary>
    /// Update each individual style based on the input control style.
    /// </summary>
    /// <param name="style">New input control style.</param>
    public void SetStyles(InputControlStyle style)
    {
        switch (style)
        {
            case InputControlStyle.Standalone:
                SetStyles(PaletteBackStyle.InputControlStandalone,
                    PaletteBorderStyle.InputControlStandalone,
                    PaletteContentStyle.InputControlStandalone);
                break;
            case InputControlStyle.Ribbon:
                SetStyles(PaletteBackStyle.InputControlRibbon,
                    PaletteBorderStyle.InputControlRibbon,
                    PaletteContentStyle.InputControlRibbon);
                break;
            case InputControlStyle.Custom1:
                SetStyles(PaletteBackStyle.InputControlCustom1,
                    PaletteBorderStyle.InputControlCustom1,
                    PaletteContentStyle.InputControlCustom1);
                break;
            case InputControlStyle.Custom2:
                SetStyles(PaletteBackStyle.InputControlCustom2,
                    PaletteBorderStyle.InputControlCustom2,
                    PaletteContentStyle.InputControlCustom2);
                break;
            case InputControlStyle.Custom3:
                SetStyles(PaletteBackStyle.InputControlCustom3,
                    PaletteBorderStyle.InputControlCustom3,
                    PaletteContentStyle.InputControlCustom3);
                break;
            case InputControlStyle.PanelClient:
                SetStyles(PaletteBackStyle.PanelClient,
                    PaletteBorderStyle.InputControlStandalone,
                    PaletteContentStyle.LabelNormalControl);
                break;
            case InputControlStyle.PanelAlternate:
                SetStyles(PaletteBackStyle.PanelAlternate,
                    PaletteBorderStyle.InputControlStandalone,
                    PaletteContentStyle.LabelNormalControl);
                break;
        }
    }

    /// <summary>
    /// Update each individual style.
    /// </summary>
    /// <param name="backStyle">New background style.</param>
    /// <param name="borderStyle">New border style.</param>
    /// <param name="contentStyle">New content style.</param>
    public void SetStyles(PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        PaletteContentStyle contentStyle)
    {
        BackStyle = backStyle;
        BorderStyle = borderStyle;
        ContentStyle = contentStyle;
    }
    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public void PopulateFromBase(PaletteState state)
    {
        Back.PopulateFromBase(state);
        Border.PopulateFromBase(state);
        Content.PopulateFromBase(state);
    }
    #endregion

    #region Back
    /// <summary>
    /// Gets access to the background palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining background appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlBackStates Back { get; }

    private bool ShouldSerializeBack() => !Back.IsDefault;

    /// <summary>
    /// Gets the background palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IPaletteBack PaletteBack => Back;

    /// <summary>
    /// Gets and sets the back palette style.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteBackStyle BackStyle
    {
        get => _backInherit.Style;
        set => _backInherit.Style = value;
    }
    #endregion

    #region Border
    /// <summary>
    /// Gets access to the border palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBorder Border { get; }

    private bool ShouldSerializeBorder() => !Border.IsDefault;

    /// <summary>
    /// Gets the border palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IPaletteBorder PaletteBorder => Border;

    /// <summary>
    /// Gets and sets the border palette style.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteBorderStyle BorderStyle
    {
        get => _borderInherit.Style;
        set => _borderInherit.Style = value;
    }
    #endregion

    #region Content
    /// <summary>
    /// Gets access to the content palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining content appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlContentStates Content { get; }

    private bool ShouldSerializeContent() => !Content.IsDefault;

    /// <summary>
    /// Gets the content palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IPaletteContent PaletteContent => Content;

    /// <summary>
    /// Gets and sets the content palette style.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteContentStyle ContentStyle
    {
        get => _contentInherit.Style;
        set => _contentInherit.Style = value;
    }
    #endregion

    #region Metric

    /// <summary>
    /// Gets an integer metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Integer value.</returns>
    public int GetMetricInt(KryptonForm? owningForm, PaletteState state, PaletteMetricInt metric) => _metricRedirect.GetMetricInt(owningForm, state, metric);

    /// <summary>
    /// Gets a boolean metric value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric) => _metricRedirect.GetMetricBool(state, metric);

    /// <summary>
    /// Gets a padding metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Padding value.</returns>
    public Padding GetMetricPadding(KryptonForm? owningForm, PaletteState state, PaletteMetricPadding metric) => _metricRedirect.GetMetricPadding(owningForm, state, metric);

    #endregion

    #region Protected
    /// <summary>
    /// Handle a change event from palette source.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="needLayout">True if a layout is also needed.</param>
    protected void OnNeedPaint(object? sender, bool needLayout) =>
        // Pass request from child to our own handler
        PerformNeedPaint(needLayout);

    #endregion
}