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
/// Storage for header palette settings.
/// </summary>
public class KryptonPaletteHeaders : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteHeaders class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteHeaders([DisallowNull] PaletteRedirect redirector,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirector != null);
        // Create the button style specific and common palettes
        HeaderCommon = new KryptonPaletteHeader(redirector!, PaletteBackStyle.HeaderPrimary, PaletteBorderStyle.HeaderPrimary, PaletteContentStyle.HeaderPrimary, needPaint);
        HeaderPrimary = new KryptonPaletteHeader(redirector!, PaletteBackStyle.HeaderPrimary, PaletteBorderStyle.HeaderPrimary, PaletteContentStyle.HeaderPrimary, needPaint);
        HeaderSecondary = new KryptonPaletteHeader(redirector!, PaletteBackStyle.HeaderSecondary, PaletteBorderStyle.HeaderSecondary, PaletteContentStyle.HeaderSecondary, needPaint);
        HeaderDockInactive = new KryptonPaletteHeader(redirector!, PaletteBackStyle.HeaderDockInactive, PaletteBorderStyle.HeaderDockInactive, PaletteContentStyle.HeaderDockInactive, needPaint);
        HeaderDockActive = new KryptonPaletteHeader(redirector!, PaletteBackStyle.HeaderDockActive, PaletteBorderStyle.HeaderDockActive, PaletteContentStyle.HeaderDockActive, needPaint);
        HeaderCalendar = new KryptonPaletteHeader(redirector!, PaletteBackStyle.HeaderCalendar, PaletteBorderStyle.HeaderCalendar, PaletteContentStyle.HeaderCalendar, needPaint);
        HeaderForm = new KryptonPaletteHeader(redirector!, PaletteBackStyle.HeaderForm, PaletteBorderStyle.HeaderForm, PaletteContentStyle.HeaderForm, needPaint);
        HeaderCustom1 = new KryptonPaletteHeader(redirector!, PaletteBackStyle.HeaderCustom1, PaletteBorderStyle.HeaderCustom1, PaletteContentStyle.HeaderCustom1, needPaint);
        HeaderCustom2 = new KryptonPaletteHeader(redirector!, PaletteBackStyle.HeaderCustom2, PaletteBorderStyle.HeaderCustom2, PaletteContentStyle.HeaderCustom2, needPaint);
        HeaderCustom3 = new KryptonPaletteHeader(redirector!, PaletteBackStyle.HeaderCustom3, PaletteBorderStyle.HeaderCustom3, PaletteContentStyle.HeaderCustom3, needPaint);

        // Create redirectors for inheriting from style specific to style common
        var redirectCommon = new PaletteRedirectTripleMetric(redirector!,
            HeaderCommon.StateDisabled, HeaderCommon.StateDisabled, HeaderCommon.StateNormal,
            HeaderCommon.StateNormal);

        // Inform the button style to use the new redirector
        HeaderPrimary.SetRedirector(redirectCommon);
        HeaderSecondary.SetRedirector(redirectCommon);
        HeaderDockInactive.SetRedirector(redirectCommon);
        HeaderDockActive.SetRedirector(redirectCommon);
        HeaderCalendar.SetRedirector(redirectCommon);
        HeaderForm.SetRedirector(redirectCommon);
        HeaderCustom1.SetRedirector(redirectCommon);
        HeaderCustom2.SetRedirector(redirectCommon);
        HeaderCustom3.SetRedirector(redirectCommon);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => HeaderCommon.IsDefault &&
                                      HeaderPrimary.IsDefault &&
                                      HeaderSecondary.IsDefault &&
                                      HeaderDockInactive.IsDefault &&
                                      HeaderDockActive.IsDefault &&
                                      HeaderCalendar.IsDefault &&
                                      HeaderForm.IsDefault &&
                                      HeaderCustom1.IsDefault 
                                      && HeaderCustom2.IsDefault
                                      && HeaderCustom3.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="common">Reference to common settings.</param>
    public void PopulateFromBase(KryptonPaletteCommon common)
    {
        // Populate only the designated styles
        common.StateCommon.BackStyle = PaletteBackStyle.HeaderPrimary;
        common.StateCommon.BorderStyle = PaletteBorderStyle.HeaderPrimary;
        common.StateCommon.ContentStyle = PaletteContentStyle.HeaderPrimary;
        HeaderPrimary.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.HeaderSecondary;
        common.StateCommon.BorderStyle = PaletteBorderStyle.HeaderSecondary;
        common.StateCommon.ContentStyle = PaletteContentStyle.HeaderSecondary;
        HeaderSecondary.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.HeaderDockInactive;
        common.StateCommon.BorderStyle = PaletteBorderStyle.HeaderDockInactive;
        common.StateCommon.ContentStyle = PaletteContentStyle.HeaderDockInactive;
        HeaderDockInactive.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.HeaderDockActive;
        common.StateCommon.BorderStyle = PaletteBorderStyle.HeaderDockActive;
        common.StateCommon.ContentStyle = PaletteContentStyle.HeaderDockActive;
        HeaderDockActive.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.HeaderCalendar;
        common.StateCommon.BorderStyle = PaletteBorderStyle.HeaderCalendar;
        common.StateCommon.ContentStyle = PaletteContentStyle.HeaderCalendar;
        HeaderCalendar.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.HeaderForm;
        common.StateCommon.BorderStyle = PaletteBorderStyle.HeaderForm;
        common.StateCommon.ContentStyle = PaletteContentStyle.HeaderForm;
        HeaderForm.PopulateFromBase();
    }
    #endregion

    #region HeaderCommon
    /// <summary>
    /// Gets access to the common header appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteHeader HeaderCommon { get; }

    private bool ShouldSerializeHeaderCommon() => !HeaderCommon.IsDefault;

    #endregion

    #region HeaderPrimary
    /// <summary>
    /// Gets access to the primary header appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining primary header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteHeader HeaderPrimary { get; }

    private bool ShouldSerializeHeaderPrimary() => !HeaderPrimary.IsDefault;

    #endregion

    #region HeaderSecondary
    /// <summary>
    /// Gets access to the secondary header appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining secondary header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteHeader HeaderSecondary { get; }

    private bool ShouldSerializeHeaderSecondary() => !HeaderSecondary.IsDefault;

    #endregion

    #region HeaderDockInactive
    /// <summary>
    /// Gets access to the inactive dock header appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining inactive dock header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteHeader HeaderDockInactive { get; }

    private bool ShouldSerializeHeaderDockInactive() => !HeaderDockInactive.IsDefault;

    #endregion

    #region HeaderDockActive
    /// <summary>
    /// Gets access to the active dock header appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active dock header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteHeader HeaderDockActive { get; }

    private bool ShouldSerializeHeaderDockActive() => !HeaderDockActive.IsDefault;

    #endregion

    #region HeaderCalendar
    /// <summary>
    /// Gets access to the calendar header appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining calendar header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteHeader HeaderCalendar { get; }

    private bool ShouldSerializeHeaderCalendar() => !HeaderCalendar.IsDefault;

    #endregion

    #region HeaderForm
    /// <summary>
    /// Gets access to the main form header appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining main form header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteHeader HeaderForm { get; }

    private bool ShouldSerializeHeaderForm() => !HeaderForm.IsDefault;

    #endregion

    #region HeaderCustom1
    /// <summary>
    /// Gets access to the first custom header appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the first custom header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteHeader HeaderCustom1 { get; }

    private bool ShouldSerializeHeaderCustom1() => !HeaderCustom1.IsDefault;

    #endregion

    #region HeaderCustom2
    /// <summary>
    /// Gets access to the second custom header appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the second custom header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteHeader HeaderCustom2 { get; }

    private bool ShouldSerializeHeaderCustom2() => !HeaderCustom2.IsDefault;

    #endregion

    #region HeaderCustom3
    /// <summary>
    /// Gets access to the second custom header appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the third custom header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteHeader HeaderCustom3 { get; }

    private bool ShouldSerializeHeaderCustom3() => !HeaderCustom3.IsDefault;

    #endregion
}