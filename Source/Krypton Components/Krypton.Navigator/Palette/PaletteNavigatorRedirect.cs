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

namespace Krypton.Navigator;

/// <summary>
/// Implement redirected storage for common navigator appearance.
/// </summary>
public class PaletteNavigatorRedirect : PaletteDoubleMetricRedirect
{
    #region Instance Fields

    private readonly PaletteBorderInheritRedirect _paletteBorderEdgeInheritRedirect;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteNavigatorNormabled class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator.</param>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteNavigatorRedirect(KryptonNavigator navigator,
        PaletteRedirect redirect,
        NeedPaintHandler needPaint)
        : this(navigator, redirect, redirect, redirect,
            redirect, redirect, redirect,
            redirect, redirect, redirect,
            redirect, redirect, redirect,
            redirect, redirect, redirect,
            redirect, needPaint)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteNavigatorRedirect class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator.</param>
    /// <param name="redirectNavigator">inheritance redirection for navigator level.</param>
    /// <param name="redirectNavigatorPage">inheritance redirection for page level.</param>
    /// <param name="redirectNavigatorHeaderGroup">inheritance redirection for header groups level.</param>
    /// <param name="redirectNavigatorHeaderPrimary">inheritance redirection for primary header.</param>
    /// <param name="redirectNavigatorHeaderSecondary">inheritance redirection for secondary header.</param>
    /// <param name="redirectNavigatorHeaderBar">inheritance redirection for bar header.</param>
    /// <param name="redirectNavigatorHeaderOverflow">inheritance redirection for bar header.</param>
    /// <param name="redirectNavigatorCheckButton">inheritance redirection for check button.</param>
    /// <param name="redirectNavigatorOverflowButton">inheritance redirection for overflow button.</param>
    /// <param name="redirectNavigatorMiniButton">inheritance redirection for check button.</param>
    /// <param name="redirectNavigatorBar">inheritance redirection for bar.</param>
    /// <param name="redirectNavigatorBorderEdge">inheritance redirection for border edge.</param>
    /// <param name="redirectNavigatorSeparator">inheritance redirection for separator.</param>
    /// <param name="redirectNavigatorTab">inheritance redirection for tab.</param>
    /// <param name="redirectNavigatorRibbonTab">inheritance redirection for ribbon tab.</param>
    /// <param name="redirectNavigatorRibbonGeneral">inheritance redirection for ribbon general.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteNavigatorRedirect(KryptonNavigator? navigator,
        PaletteRedirect redirectNavigator,
        PaletteRedirect redirectNavigatorPage,
        PaletteRedirect redirectNavigatorHeaderGroup,
        PaletteRedirect redirectNavigatorHeaderPrimary,
        PaletteRedirect redirectNavigatorHeaderSecondary,
        PaletteRedirect redirectNavigatorHeaderBar,
        PaletteRedirect redirectNavigatorHeaderOverflow,
        PaletteRedirect redirectNavigatorCheckButton,
        PaletteRedirect redirectNavigatorOverflowButton,
        PaletteRedirect redirectNavigatorMiniButton,
        PaletteRedirect redirectNavigatorBar,
        PaletteRedirect redirectNavigatorBorderEdge,
        PaletteRedirect redirectNavigatorSeparator,
        PaletteRedirect redirectNavigatorTab,
        PaletteRedirect redirectNavigatorRibbonTab,
        PaletteRedirect redirectNavigatorRibbonGeneral,
        NeedPaintHandler needPaint)
        : base(redirectNavigator, PaletteBackStyle.PanelClient,
            PaletteBorderStyle.ControlClient, needPaint)
    {
        // Create the palette storage
        PalettePage = new PalettePageRedirect(redirectNavigatorPage, needPaint);
        HeaderGroup = new PaletteNavigatorHeaderGroupRedirect(redirectNavigatorHeaderGroup, redirectNavigatorHeaderPrimary, redirectNavigatorHeaderSecondary, redirectNavigatorHeaderBar, redirectNavigatorHeaderOverflow, needPaint);
        CheckButton = new PaletteTripleRedirect(redirectNavigatorCheckButton, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, needPaint);
        OverflowButton = new PaletteTripleRedirect(redirectNavigatorOverflowButton, PaletteBackStyle.ButtonNavigatorOverflow, PaletteBorderStyle.ButtonNavigatorOverflow, PaletteContentStyle.ButtonNavigatorOverflow, needPaint);
        MiniButton = new PaletteTripleRedirect(redirectNavigatorMiniButton, PaletteBackStyle.ButtonNavigatorMini, PaletteBorderStyle.ButtonNavigatorMini, PaletteContentStyle.ButtonNavigatorMini, needPaint);
        Bar = new PaletteBarRedirect(redirectNavigatorBar, needPaint);
        _paletteBorderEdgeInheritRedirect = new PaletteBorderInheritRedirect(redirectNavigatorBorderEdge, PaletteBorderStyle.ControlClient);
        BorderEdge = new PaletteBorderEdgeRedirect(_paletteBorderEdgeInheritRedirect, needPaint);
        Separator = new PaletteSeparatorPaddingRedirect(redirectNavigatorSeparator, PaletteBackStyle.SeparatorHighInternalProfile, PaletteBorderStyle.SeparatorHighInternalProfile, needPaint);
        Tab = new PaletteTabTripleRedirect(redirectNavigatorTab, PaletteBackStyle.TabHighProfile, PaletteBorderStyle.TabHighProfile, PaletteContentStyle.TabHighProfile, needPaint);
        RibbonTab = new PaletteRibbonTabContentRedirect(redirectNavigatorRibbonTab, needPaint);
        RibbonGeneral = new PaletteRibbonGeneralNavRedirect(redirectNavigatorRibbonGeneral, needPaint);
        Metrics = new PaletteMetrics(navigator, needPaint);
    }
    #endregion

    #region RedirectBorderEdge
    /// <summary>
    /// Update the redirector for the border edge.
    /// </summary>
    public PaletteRedirect RedirectBorderEdge
    {
        set => _paletteBorderEdgeInheritRedirect.SetRedirector(value);
    }
    #endregion

    #region RedirectRibbonGeneral
    /// <summary>
    /// Update the redirector for the ribbon general.
    /// </summary>
    public PaletteRedirect RedirectRibbonGeneral
    {
        set => RibbonGeneral.SetRedirector(value);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (base.IsDefault &&
                                       PalettePage.IsDefault &&
                                       HeaderGroup.IsDefault &&
                                       CheckButton.IsDefault &&
                                       OverflowButton.IsDefault &&
                                       MiniButton.IsDefault &&
                                       Bar.IsDefault &&
                                       BorderEdge.IsDefault &&
                                       Separator!.IsDefault &&
                                       Tab.IsDefault &&
                                       RibbonTab.IsDefault &&
                                       RibbonGeneral.IsDefault &&
                                       Metrics.IsDefault);

    #endregion

    #region Bar
    /// <summary>
    /// Gets access to the bar appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining bar appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBarRedirect Bar { get; }

    private bool ShouldSerializeBar() => !Bar.IsDefault;

    #endregion

    #region Back
    /// <summary>
    /// Gets access to the background palette details.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override PaletteBack Back => base.Back;


    /// <summary>
    /// Gets the background palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override IPaletteBack PaletteBack => base.PaletteBack;

    #endregion

    #region Border
    /// <summary>
    /// Gets access to the border palette details.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override PaletteBorder Border => base.Border;

    /// <summary>
    /// Gets the border palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override IPaletteBorder? PaletteBorder => base.PaletteBorder;

    #endregion

    #region Panel
    /// <summary>
    /// Gets access to the panel palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack Panel => base.Back;

    private bool ShouldSerializePanel() => !base.Back.IsDefault;

    #endregion

    #region CheckButton
    /// <summary>
    /// Gets access to the check button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining check button appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect CheckButton { get; }

    private bool ShouldSerializeCheckButton() => !CheckButton.IsDefault;

    #endregion

    #region OverflowButton
    /// <summary>
    /// Gets access to the outlook overflow button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining outlook overflow button appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect OverflowButton { get; }

    private bool ShouldSerializeOverflowButton() => !OverflowButton.IsDefault;

    #endregion

    #region MiniButton
    /// <summary>
    /// Gets access to the outlook mini button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining outlook mini button appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect MiniButton { get; }

    private bool ShouldSerializeMiniButton() => !MiniButton.IsDefault;

    #endregion

    #region HeaderGroup
    /// <summary>
    /// Gets access to the header group appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining header group appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteNavigatorHeaderGroupRedirect HeaderGroup { get; }

    private bool ShouldSerializeHeaderGroup() => !HeaderGroup.IsDefault;

    #endregion

    #region Page
    /// <summary>
    /// Gets access to the page appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining page appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack Page => PalettePage.Back;

    private bool ShouldSerializePage() => !PalettePage.Back.IsDefault;

    #endregion

    #region BorderEdge
    /// <summary>
    /// Gets access to the border edge appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining border edge appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBorderEdgeRedirect BorderEdge { get; }

    private bool ShouldSerializeBorderEdge() => !BorderEdge.IsDefault;

    #endregion

    #region Metrics
    /// <summary>
    /// Gets access to the metrics entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining metric entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMetrics Metrics { get; }

    private bool ShouldSerializeMetrics() => !Metrics.IsDefault;

    #endregion

    #region Separator
    /// <summary>
    /// Get access to the overrides for defining separator appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining separator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteSeparatorPaddingRedirect? Separator { get; }

    private bool ShouldSerializeSeparator() => !Separator!.IsDefault;

    #endregion

    #region Tab
    /// <summary>
    /// Gets access to the tab appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tab appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTabTripleRedirect Tab { get; }

    private bool ShouldSerializeTab() => !Tab.IsDefault;

    #endregion

    #region RibbonTab
    /// <summary>
    /// Gets access to the ribbon tab appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon tab appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonTabContentRedirect RibbonTab { get; }

    private bool ShouldSerializeRibbonTab() => !RibbonTab.IsDefault;

    #endregion

    #region RibbonGeneral
    /// <summary>
    /// Gets access to the ribbon general appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon general appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonGeneralNavRedirect RibbonGeneral { get; }

    private bool ShouldSerializeRibbonGeneral() => !RibbonGeneral.IsDefault;

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
        if (metric == PaletteMetricInt.PageButtonInset 
            && Metrics.PageButtonSpecInset != -1)
        {
            return Metrics.PageButtonSpecInset;
        }

        // Pass onto the inheritance
        return base.GetMetricInt(owningForm, state, metric);
    }

    /// <summary>
    /// Gets a padding metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Padding value.</returns>
    public override Padding GetMetricPadding(KryptonForm? owningForm, PaletteState state, PaletteMetricPadding metric)
    {
        if (metric == PaletteMetricPadding.PageButtonPadding 
            && !Metrics.PageButtonSpecPadding.Equals(CommonHelper.InheritPadding))
        {
            return Metrics.PageButtonSpecPadding;
        }

        // Pass onto the inheritance
        return base.GetMetricPadding(owningForm, state, metric);
    }
    #endregion

    #region Internal
    internal PalettePageRedirect PalettePage { get; }

    internal PaletteBorderStyle BorderEdgeStyle
    {
        set => _paletteBorderEdgeInheritRedirect.Style = value;
    }
    #endregion    
}