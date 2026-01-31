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
/// Storage for control palette settings.
/// </summary>
public class KryptonPaletteControls : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteControls class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteControls([DisallowNull] PaletteRedirect redirector,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirector != null);

        // Create the button style specific and common palettes
        ControlCommon = new KryptonPaletteControl(redirector!, PaletteBackStyle.ControlClient, PaletteBorderStyle.ControlClient, needPaint);
        ControlClient = new KryptonPaletteControl(redirector!, PaletteBackStyle.ControlClient, PaletteBorderStyle.ControlClient, needPaint);
        ControlAlternate = new KryptonPaletteControl(redirector!, PaletteBackStyle.ControlAlternate, PaletteBorderStyle.ControlAlternate, needPaint);
        ControlGroupBox = new KryptonPaletteControl(redirector!, PaletteBackStyle.ControlGroupBox, PaletteBorderStyle.ControlGroupBox, needPaint);
        ControlToolTip = new KryptonPaletteControl(redirector!, PaletteBackStyle.ControlToolTip, PaletteBorderStyle.ControlToolTip, needPaint);
        ControlRibbon = new KryptonPaletteControl(redirector!, PaletteBackStyle.ControlRibbon, PaletteBorderStyle.ControlRibbon, needPaint);
        ControlRibbonAppMenu = new KryptonPaletteControl(redirector!, PaletteBackStyle.ControlRibbonAppMenu, PaletteBorderStyle.ControlRibbonAppMenu, needPaint);
        ControlCustom1 = new KryptonPaletteControl(redirector!, PaletteBackStyle.ControlCustom1, PaletteBorderStyle.ControlCustom1, needPaint);
        ControlCustom2 = new KryptonPaletteControl(redirector!, PaletteBackStyle.ControlCustom2, PaletteBorderStyle.ControlCustom2, needPaint);
        ControlCustom3 = new KryptonPaletteControl(redirector!, PaletteBackStyle.ControlCustom3, PaletteBorderStyle.ControlCustom3, needPaint);

        // Create redirectors for inheriting from style specific to style common
        var redirectCommon =
            new PaletteRedirectDouble(redirector!, ControlCommon.StateDisabled, ControlCommon.StateNormal);

        // Inform the button style to use the new redirector
        ControlClient.SetRedirector(redirectCommon);
        ControlAlternate.SetRedirector(redirectCommon);
        ControlGroupBox.SetRedirector(redirectCommon);
        ControlToolTip.SetRedirector(redirectCommon);
        ControlRibbon.SetRedirector(redirectCommon);
        ControlRibbonAppMenu.SetRedirector(redirectCommon);
        ControlCustom1.SetRedirector(redirectCommon);
        ControlCustom2.SetRedirector(redirectCommon);
        ControlCustom3.SetRedirector(redirectCommon);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ControlCommon.IsDefault &&
                                      ControlClient.IsDefault &&
                                      ControlAlternate.IsDefault &&
                                      ControlGroupBox.IsDefault &&
                                      ControlToolTip.IsDefault &&
                                      ControlRibbon.IsDefault &&
                                      ControlRibbonAppMenu.IsDefault &&
                                      ControlCustom1.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="common">Reference to common settings.</param>
    public void PopulateFromBase(KryptonPaletteCommon common)
    {
        // Populate only the designated styles
        common.StateCommon.BackStyle = PaletteBackStyle.ControlClient;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ControlClient;
        ControlClient.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ControlAlternate;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ControlAlternate;
        ControlAlternate.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ControlGroupBox;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ControlGroupBox;
        ControlGroupBox.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ControlToolTip;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ControlToolTip;
        ControlToolTip.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ControlRibbon;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ControlRibbon;
        ControlRibbon.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ControlRibbonAppMenu;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ControlRibbonAppMenu;
        ControlRibbonAppMenu.PopulateFromBase();
    }
    #endregion

    #region ControlCommon
    /// <summary>
    /// Gets access to the common control appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteControl ControlCommon { get; }

    private bool ShouldSerializeControlCommon() => !ControlCommon.IsDefault;

    #endregion

    #region ControlClient
    /// <summary>
    /// Gets access to the client control appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining client control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteControl ControlClient { get; }

    private bool ShouldSerializeControlClient() => !ControlClient.IsDefault;

    #endregion

    #region ControlAlternate
    /// <summary>
    /// Gets access to the alternate control appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining alternate control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteControl ControlAlternate { get; }

    private bool ShouldSerializeControlAlternate() => !ControlAlternate.IsDefault;

    #endregion

    #region ControlGroupBox
    /// <summary>
    /// Gets access to the group box control appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining group box control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteControl ControlGroupBox { get; }

    private bool ShouldSerializeControlGroupBox() => !ControlGroupBox.IsDefault;

    #endregion

    #region ControlToolTip
    /// <summary>
    /// Gets access to the tooltip control appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tooltip control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteControl ControlToolTip { get; }

    private bool ShouldSerializeControlToolTip() => !ControlToolTip.IsDefault;

    #endregion

    #region ControlRibbon
    /// <summary>
    /// Gets access to the control ribbon style appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining control ribbon style appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteControl ControlRibbon { get; }

    private bool ShouldSerializeControlRibbon() => !ControlRibbon.IsDefault;

    #endregion

    #region ControlRibbonAppMenu
    /// <summary>
    /// Gets access to the control ribbon application menu style appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining control ribbon application menu style appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteControl ControlRibbonAppMenu { get; }

    private bool ShouldSerializeControlRibbonAppMenu() => !ControlRibbonAppMenu.IsDefault;

    #endregion

    #region ControlCustom1
    /// <summary>
    /// Gets access to the first custom control appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the first custom control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteControl ControlCustom1 { get; }

    private bool ShouldSerializeControlCustom1() => !ControlCustom1.IsDefault;

    #endregion

    #region ControlCustom2
    /// <summary>
    /// Gets access to the first custom control appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the first custom control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteControl ControlCustom2 { get; }

    private bool ShouldSerializeControlCustom2() => !ControlCustom2.IsDefault;

    #endregion

    #region ControlCustom3
    /// <summary>
    /// Gets access to the first custom control appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the third custom control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteControl ControlCustom3 { get; }

    private bool ShouldSerializeControlCustom3() => !ControlCustom3.IsDefault;

    #endregion
}