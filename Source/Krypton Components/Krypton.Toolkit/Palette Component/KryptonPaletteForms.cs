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
/// Storage for form palette settings.
/// </summary>
public class KryptonPaletteForms : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteForms class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteForms([DisallowNull] PaletteRedirect redirector,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirector != null);

        // Create the form style specific and common palettes
        FormCommon = new KryptonPaletteForm(redirector!, PaletteBackStyle.FormMain, PaletteBorderStyle.FormMain, needPaint);
        FormMain = new KryptonPaletteForm(redirector!, PaletteBackStyle.FormMain, PaletteBorderStyle.FormMain, needPaint);
        FormCustom1 = new KryptonPaletteForm(redirector!, PaletteBackStyle.FormCustom1, PaletteBorderStyle.FormCustom1, needPaint);
        FormCustom2 = new KryptonPaletteForm(redirector!, PaletteBackStyle.FormCustom2, PaletteBorderStyle.FormCustom2, needPaint);
        FormCustom3 = new KryptonPaletteForm(redirector!, PaletteBackStyle.FormCustom3, PaletteBorderStyle.FormCustom3, needPaint);

        // Create redirectors for inheriting from style specific to style common
        var redirectCommon =
            new PaletteRedirectDouble(redirector!, FormCommon.StateInactive, FormCommon.StateActive);

        // Inform the form style to use the new redirector
        FormMain.SetRedirector(redirectCommon);
        FormCustom1.SetRedirector(redirectCommon);
        FormCustom2.SetRedirector(redirectCommon);
        FormCustom3.SetRedirector(redirectCommon);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => FormCommon.IsDefault &&
                                      FormMain.IsDefault &&
                                      FormCustom1.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="common">Reference to common settings.</param>
    public void PopulateFromBase(KryptonPaletteCommon common)
    {
        // Populate only the designated styles
        common.StateCommon.BackStyle = PaletteBackStyle.FormMain;
        common.StateCommon.BorderStyle = PaletteBorderStyle.FormMain;
        FormMain.PopulateFromBase();
    }
    #endregion

    #region FormCommon
    /// <summary>
    /// Gets access to the common form appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common form appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteForm FormCommon { get; }

    private bool ShouldSerializeFormCommon() => !FormCommon.IsDefault;

    #endregion

    #region FormMain
    /// <summary>
    /// Gets access to the main form appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining main form appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteForm FormMain { get; }

    private bool ShouldSerializeFormMain() => !FormMain.IsDefault;

    #endregion

    #region FormCustom1
    /// <summary>
    /// Gets access to the first custom form appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the first custom form appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteForm FormCustom1 { get; }

    private bool ShouldSerializeFormCustom1() => !FormCustom1.IsDefault;

    #endregion

    #region FormCustom2
    /// <summary>
    /// Gets access to the first custom form appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the first custom form appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteForm FormCustom2 { get; }

    private bool ShouldSerializeFormCustom2() => !FormCustom2.IsDefault;

    #endregion

    #region FormCustom3
    /// <summary>
    /// Gets access to the first custom form appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the thrid custom form appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteForm FormCustom3 { get; }

    private bool ShouldSerializeFormCustom3() => !FormCustom3.IsDefault;

    #endregion
}