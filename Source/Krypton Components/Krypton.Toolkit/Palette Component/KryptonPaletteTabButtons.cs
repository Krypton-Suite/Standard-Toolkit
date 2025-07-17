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
/// Storage for tab button palette settings.
/// </summary>
public class KryptonPaletteTabButtons : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteTabButtons class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteTabButtons([DisallowNull] PaletteRedirect redirector,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirector is not null);

        if (redirector is null)
        {
            throw new ArgumentNullException(nameof(redirector));
        }

        // Create the button style specific and common palettes
        TabCommon = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabHighProfile, PaletteBorderStyle.TabHighProfile, PaletteContentStyle.TabHighProfile, needPaint);
        TabHighProfile = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabHighProfile, PaletteBorderStyle.TabHighProfile, PaletteContentStyle.TabHighProfile, needPaint);
        TabStandardProfile = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabStandardProfile, PaletteBorderStyle.TabStandardProfile, PaletteContentStyle.TabStandardProfile, needPaint);
        TabLowProfile = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabLowProfile, PaletteBorderStyle.TabLowProfile, PaletteContentStyle.TabLowProfile, needPaint);
        TabDock = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabDock, PaletteBorderStyle.TabDock, PaletteContentStyle.TabDock, needPaint);
        TabDockAutoHidden = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabDockAutoHidden, PaletteBorderStyle.TabDockAutoHidden, PaletteContentStyle.TabDockAutoHidden, needPaint);
        TabOneNote = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabOneNote, PaletteBorderStyle.TabOneNote, PaletteContentStyle.TabOneNote, needPaint);
        TabCustom1 = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabCustom1, PaletteBorderStyle.TabCustom1, PaletteContentStyle.TabCustom1, needPaint);
        TabCustom2 = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabCustom2, PaletteBorderStyle.TabCustom2, PaletteContentStyle.TabCustom2, needPaint);
        TabCustom3 = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabCustom3, PaletteBorderStyle.TabCustom3, PaletteContentStyle.TabCustom3, needPaint);

        // Create redirectors for inheriting from style specific to style common
        var redirectCommon = new PaletteRedirectTriple(redirector!, TabCommon.StateDisabled,
            TabCommon.StateNormal, TabCommon.StatePressed, TabCommon.StateTracking, TabCommon.StateSelected,
            TabCommon.OverrideFocus);
        // Inform the button style to use the new redirector
        TabHighProfile.SetRedirector(redirectCommon);
        TabStandardProfile.SetRedirector(redirectCommon);
        TabLowProfile.SetRedirector(redirectCommon);
        TabDock.SetRedirector(redirectCommon);
        TabDockAutoHidden.SetRedirector(redirectCommon);
        TabOneNote.SetRedirector(redirectCommon);
        TabCustom1.SetRedirector(redirectCommon);
        TabCustom2.SetRedirector(redirectCommon);
        TabCustom3.SetRedirector(redirectCommon);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => TabCommon.IsDefault &&
                                      TabHighProfile.IsDefault &&
                                      TabStandardProfile.IsDefault &&
                                      TabLowProfile.IsDefault &&
                                      TabDock.IsDefault &&
                                      TabDockAutoHidden.IsDefault &&
                                      TabOneNote.IsDefault &&
                                      TabCustom1.IsDefault &&
                                      TabCustom2.IsDefault &&
                                      TabCustom3.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="common">Reference to common settings.</param>
    public void PopulateFromBase(KryptonPaletteCommon common)
    {
        // Populate only the designated styles
        common.StateCommon.BackStyle = PaletteBackStyle.TabHighProfile;
        common.StateCommon.BorderStyle = PaletteBorderStyle.TabHighProfile;
        common.StateCommon.ContentStyle = PaletteContentStyle.TabHighProfile;
        TabHighProfile.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.TabStandardProfile;
        common.StateCommon.BorderStyle = PaletteBorderStyle.TabStandardProfile;
        common.StateCommon.ContentStyle = PaletteContentStyle.TabStandardProfile;
        TabStandardProfile.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.TabLowProfile;
        common.StateCommon.BorderStyle = PaletteBorderStyle.TabLowProfile;
        common.StateCommon.ContentStyle = PaletteContentStyle.TabLowProfile;
        TabLowProfile.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.TabDock;
        common.StateCommon.BorderStyle = PaletteBorderStyle.TabDock;
        common.StateCommon.ContentStyle = PaletteContentStyle.TabDock;
        TabDock.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.TabDockAutoHidden;
        common.StateCommon.BorderStyle = PaletteBorderStyle.TabDockAutoHidden;
        common.StateCommon.ContentStyle = PaletteContentStyle.TabDockAutoHidden;
        TabDockAutoHidden.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.TabOneNote;
        common.StateCommon.BorderStyle = PaletteBorderStyle.TabOneNote;
        common.StateCommon.ContentStyle = PaletteContentStyle.TabOneNote;
        TabOneNote.PopulateFromBase();
    }
    #endregion

    #region TabCommon
    /// <summary>
    /// Gets access to the common appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTabButton TabCommon { get; }

    private bool ShouldSerializeTabCommon() => !TabCommon.IsDefault;

    #endregion

    #region TabHighProfile
    /// <summary>
    /// Gets access to the High Profile appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining High Profile appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTabButton TabHighProfile { get; }

    private bool ShouldSerializeTabHighProfile() => !TabHighProfile.IsDefault;

    #endregion

    #region TabStandardProfile
    /// <summary>
    /// Gets access to the Standard Profile appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining Standard Profile appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTabButton TabStandardProfile { get; }

    private bool ShouldSerializeTabStandardProfile() => !TabStandardProfile.IsDefault;

    #endregion

    #region TabLowProfile
    /// <summary>
    /// Gets access to the LowProfile appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining LowProfile appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTabButton TabLowProfile { get; }

    private bool ShouldSerializeTabLowProfile() => !TabLowProfile.IsDefault;

    #endregion

    #region TabDock
    /// <summary>
    /// Gets access to the Dock appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining Dock appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTabButton TabDock { get; }

    private bool ShouldSerializeTabDock() => !TabDock.IsDefault;

    #endregion

    #region TabDockAutoHidden
    /// <summary>
    /// Gets access to the Dock AutoHidden appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining Dock AutoHidden appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTabButton TabDockAutoHidden { get; }

    private bool ShouldSerializeTabDockAutoHidden() => !TabDockAutoHidden.IsDefault;

    #endregion

    #region TabOneNote
    /// <summary>
    /// Gets access to the OneNote appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining OneNote appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTabButton TabOneNote { get; }

    private bool ShouldSerializeTabOneNote() => !TabOneNote.IsDefault;

    #endregion

    #region TabCustom1
    /// <summary>
    /// Gets access to the Custom1 appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining Custom1 appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTabButton TabCustom1 { get; }

    private bool ShouldSerializeTabCustom1() => !TabCustom1.IsDefault;

    #endregion

    #region TabCustom2
    /// <summary>
    /// Gets access to the Custom2 appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining Custom2 appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTabButton TabCustom2 { get; }

    private bool ShouldSerializeTabCustom2() => !TabCustom2.IsDefault;

    #endregion

    #region TabCustom3
    /// <summary>
    /// Gets access to the Custom3 appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining Custom3 appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTabButton TabCustom3 { get; }

    private bool ShouldSerializeTabCustom3() => !TabCustom3.IsDefault;

    #endregion
}