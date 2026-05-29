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
/// Storage for input control palette settings.
/// </summary>
public class KryptonPaletteInputControls : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteInputControls class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteInputControls([DisallowNull] PaletteRedirect redirector,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirector != null);

        // Create the input control style specific and common palettes
        InputControlCommon = new KryptonPaletteInputControl(redirector!, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, PaletteContentStyle.InputControlStandalone, needPaint);
        InputControlStandalone = new KryptonPaletteInputControl(redirector!, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, PaletteContentStyle.InputControlStandalone, needPaint);
        InputControlRibbon = new KryptonPaletteInputControl(redirector!, PaletteBackStyle.InputControlRibbon, PaletteBorderStyle.InputControlRibbon, PaletteContentStyle.InputControlRibbon, needPaint);
        InputControlCustom1 = new KryptonPaletteInputControl(redirector!, PaletteBackStyle.InputControlCustom1, PaletteBorderStyle.InputControlCustom1, PaletteContentStyle.InputControlCustom1, needPaint);
        InputControlCustom2 = new KryptonPaletteInputControl(redirector!, PaletteBackStyle.InputControlCustom2, PaletteBorderStyle.InputControlCustom2, PaletteContentStyle.InputControlCustom2, needPaint);
        InputControlCustom3 = new KryptonPaletteInputControl(redirector!, PaletteBackStyle.InputControlCustom3, PaletteBorderStyle.InputControlCustom3, PaletteContentStyle.InputControlCustom3, needPaint);

        // Create redirectors for inheriting from style specific to style common
        var redirectCommon = new PaletteRedirectTriple(redirector!,
            InputControlCommon.StateDisabled, InputControlCommon.StateNormal, InputControlCommon.StateActive, InputControlCommon.StatePressed, InputControlCommon.StateContextNormal, InputControlCommon.StateContextPressed, InputControlCommon.StateContextTracking);

        // Inform the input control style to use the new redirector
        InputControlStandalone.SetRedirector(redirectCommon);
        InputControlRibbon.SetRedirector(redirectCommon);
        InputControlCustom1.SetRedirector(redirectCommon);
        InputControlCustom2.SetRedirector(redirectCommon);
        InputControlCustom3.SetRedirector(redirectCommon);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => InputControlCommon.IsDefault &&
                                      InputControlStandalone.IsDefault &&
                                      InputControlRibbon.IsDefault
                                      && InputControlCustom1.IsDefault
                                      && InputControlCustom2.IsDefault
                                      && InputControlCustom3.IsDefault
    ;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="common">Reference to common settings.</param>
    public void PopulateFromBase(KryptonPaletteCommon common)
    {
        // Populate only the designated styles
        common.StateCommon.BackStyle = PaletteBackStyle.InputControlStandalone;
        common.StateCommon.BorderStyle = PaletteBorderStyle.InputControlStandalone;
        common.StateCommon.ContentStyle = PaletteContentStyle.InputControlStandalone;
        InputControlStandalone.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.InputControlRibbon;
        common.StateCommon.BorderStyle = PaletteBorderStyle.InputControlRibbon;
        common.StateCommon.ContentStyle = PaletteContentStyle.InputControlRibbon;
        InputControlRibbon.PopulateFromBase();
    }
    #endregion

    #region InputControlCommon
    /// <summary>
    /// Gets access to the common input control appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common input control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteInputControl InputControlCommon { get; }

    private bool ShouldSerializeInputControlCommon() => !InputControlCommon.IsDefault;

    #endregion

    #region InputControlStandalone
    /// <summary>
    /// Gets access to the standalone input control appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining standalone input control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteInputControl InputControlStandalone { get; }

    private bool ShouldSerializeInputControlStandalone() => !InputControlStandalone.IsDefault;

    #endregion

    #region InputControlRibbon
    /// <summary>
    /// Gets access to the input control ribbon style appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining input control ribbon style appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteInputControl InputControlRibbon { get; }

    private bool ShouldSerializeInputControlRibbon() => !InputControlRibbon.IsDefault;

    #endregion

    #region InputControlCustom1
    /// <summary>
    /// Gets access to the custom input control appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the custom input control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteInputControl InputControlCustom1 { get; }

    private bool ShouldSerializeInputControlCustom1() => !InputControlCustom1.IsDefault;

    #endregion

    #region InputControlCustom2
    /// <summary>
    /// Gets access to the custom input control appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the custom input control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteInputControl InputControlCustom2 { get; }

    private bool ShouldSerializeInputControlCustom2() => !InputControlCustom2.IsDefault;

    #endregion

    #region InputControlCustom3
    /// <summary>
    /// Gets access to the custom input control appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the custom input control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteInputControl InputControlCustom3 { get; }

    private bool ShouldSerializeInputControlCustom3() => !InputControlCustom3.IsDefault;

    #endregion
}