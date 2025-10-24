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
/// Storage for panel palette settings.
/// </summary>
public class KryptonPalettePanels : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPalettePanels class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPalettePanels([DisallowNull] PaletteRedirect redirector,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirector != null);

        // Create the button style specific and common palettes
        PanelCommon = new KryptonPalettePanel(redirector!, PaletteBackStyle.PanelClient, needPaint);
        PanelClient = new KryptonPalettePanel(redirector!, PaletteBackStyle.PanelClient, needPaint);
        PanelAlternate = new KryptonPalettePanel(redirector!, PaletteBackStyle.PanelAlternate, needPaint);
        PanelRibbonInactive = new KryptonPalettePanel(redirector!, PaletteBackStyle.PanelRibbonInactive, needPaint);
        PanelCustom1 = new KryptonPalettePanel(redirector!, PaletteBackStyle.PanelCustom1, needPaint);
        PanelCustom2 = new KryptonPalettePanel(redirector!, PaletteBackStyle.PanelCustom2, needPaint);
        PanelCustom3 = new KryptonPalettePanel(redirector!, PaletteBackStyle.PanelCustom3, needPaint);

        // Create redirectors for inheriting from style specific to style common
        var redirectCommon =
            new PaletteRedirectBack(redirector!, PanelCommon.StateDisabled, PanelCommon.StateNormal);

        // Inform the button style to use the new redirector
        PanelClient.SetRedirector(redirectCommon);
        PanelAlternate.SetRedirector(redirectCommon);
        PanelRibbonInactive.SetRedirector(redirectCommon);
        PanelCustom1.SetRedirector(redirectCommon);
        PanelCustom2.SetRedirector(redirectCommon);
        PanelCustom3.SetRedirector(redirectCommon);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => PanelCommon.IsDefault &&
                                      PanelClient.IsDefault &&
                                      PanelAlternate.IsDefault &&
                                      PanelRibbonInactive.IsDefault 
                                      && PanelCustom1.IsDefault
                                      && PanelCustom2.IsDefault
                                      && PanelCustom3.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="common">Reference to common settings.</param>
    public void PopulateFromBase(KryptonPaletteCommon common)
    {
        // Populate only the designated styles
        common.StateCommon.BackStyle = PaletteBackStyle.PanelClient;
        PanelClient.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.PanelAlternate;
        PanelAlternate.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.PanelRibbonInactive;
        PanelRibbonInactive.PopulateFromBase();
    }
    #endregion

    #region PanelCommon
    /// <summary>
    /// Gets access to the common panel appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPalettePanel PanelCommon { get; }

    private bool ShouldSerializePanelCommon() => !PanelCommon.IsDefault;

    #endregion

    #region PanelClient
    /// <summary>
    /// Gets access to the client panel appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining a client panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPalettePanel PanelClient { get; }

    private bool ShouldSerializePanelClient() => !PanelClient.IsDefault;

    #endregion

    #region PanelAlternate
    /// <summary>
    /// Gets access to the alternate panel appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining alternate panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPalettePanel PanelAlternate { get; }

    private bool ShouldSerializePanelAlternate() => !PanelAlternate.IsDefault;

    #endregion

    #region PanelRibbonInactive
    /// <summary>
    /// Gets access to the ribbon inactive panel appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon inactive panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPalettePanel PanelRibbonInactive { get; }

    private bool ShouldSerializePanelRibbonInactive() => !PanelRibbonInactive.IsDefault;

    #endregion

    #region PanelCustom1
    /// <summary>
    /// Gets access to the first custom panel appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the first custom panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPalettePanel PanelCustom1 { get; }

    private bool ShouldSerializePanelCustom1() => !PanelCustom1.IsDefault;

    #endregion

    #region PanelCustom2
    /// <summary>
    /// Gets access to the first custom panel appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the second custom panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPalettePanel PanelCustom2 { get; }

    private bool ShouldSerializePanelCustom2() => !PanelCustom2.IsDefault;

    #endregion

    #region PanelCustom3
    /// <summary>
    /// Gets access to the first custom panel appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the third custom panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPalettePanel PanelCustom3 { get; }

    private bool ShouldSerializePanelCustom3() => !PanelCustom3.IsDefault;

    #endregion

}