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
/// Storage for check button palette settings.
/// </summary>
public class KryptonPaletteCheckButtons : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteButtons class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteCheckButtons([DisallowNull] PaletteRedirect redirector,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirector != null);

        // Create the button style specific and common palettes
        ButtonCommon = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, needPaint);
        ButtonStandalone = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, needPaint);
        ButtonAlternate = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonAlternate, PaletteBorderStyle.ButtonAlternate, PaletteContentStyle.ButtonAlternate, needPaint);
        ButtonLowProfile = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonLowProfile, PaletteBorderStyle.ButtonLowProfile, PaletteContentStyle.ButtonLowProfile, needPaint);
        ButtonButtonSpec = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonButtonSpec, PaletteBorderStyle.ButtonButtonSpec, PaletteContentStyle.ButtonButtonSpec, needPaint);
        ButtonBreadCrumb = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonBreadCrumb, PaletteBorderStyle.ButtonBreadCrumb, PaletteContentStyle.ButtonBreadCrumb, needPaint);
        ButtonCalendarDay = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
        ButtonCluster = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonCluster, PaletteBorderStyle.ButtonCluster, PaletteContentStyle.ButtonCluster, needPaint);
        ButtonGallery = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonGallery, PaletteBorderStyle.ButtonGallery, PaletteContentStyle.ButtonGallery, needPaint);
        ButtonNavigatorStack = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonNavigatorStack, PaletteBorderStyle.ButtonNavigatorStack, PaletteContentStyle.ButtonNavigatorStack, needPaint);
        ButtonNavigatorOverflow = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonNavigatorOverflow, PaletteBorderStyle.ButtonNavigatorOverflow, PaletteContentStyle.ButtonNavigatorOverflow, needPaint);
        ButtonNavigatorMini = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonNavigatorMini, PaletteBorderStyle.ButtonNavigatorMini, PaletteContentStyle.ButtonNavigatorMini, needPaint);
        ButtonInputControl = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonInputControl, PaletteBorderStyle.ButtonInputControl, PaletteContentStyle.ButtonInputControl, needPaint);
        ButtonListItem = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonListItem, PaletteBorderStyle.ButtonListItem, PaletteContentStyle.ButtonListItem, needPaint);
        ButtonForm = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonForm, PaletteBorderStyle.ButtonForm, PaletteContentStyle.ButtonForm, needPaint);
        ButtonFormClose = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonFormClose, PaletteBorderStyle.ButtonFormClose, PaletteContentStyle.ButtonFormClose, needPaint);
        ButtonCommand = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonCommand, PaletteBorderStyle.ButtonCommand, PaletteContentStyle.ButtonCommand, needPaint);
        ButtonCustom1 = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonCustom1, PaletteBorderStyle.ButtonCustom1, PaletteContentStyle.ButtonCustom1, needPaint);
        ButtonCustom2 = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonCustom2, PaletteBorderStyle.ButtonCustom2, PaletteContentStyle.ButtonCustom2, needPaint);
        ButtonCustom3 = new KryptonPaletteCheckButton(redirector!, PaletteBackStyle.ButtonCustom3, PaletteBorderStyle.ButtonCustom3, PaletteContentStyle.ButtonCustom3, needPaint);

        // Create redirectors for inheriting from style specific to style common
        var redirectCommon = new PaletteRedirectTriple(redirector!, ButtonCommon.StateDisabled,
            ButtonCommon.StateNormal, ButtonCommon.StatePressed, ButtonCommon.StateTracking,
            ButtonCommon.StateCheckedNormal, ButtonCommon.StateCheckedPressed, ButtonCommon.StateCheckedTracking,
            ButtonCommon.OverrideFocus, ButtonCommon.OverrideDefault);
        // Inform the button style to use the new redirector
        ButtonStandalone.SetRedirector(redirectCommon);
        ButtonAlternate.SetRedirector(redirectCommon);
        ButtonLowProfile.SetRedirector(redirectCommon);
        ButtonButtonSpec.SetRedirector(redirectCommon);
        ButtonBreadCrumb.SetRedirector(redirectCommon);
        ButtonCalendarDay.SetRedirector(redirectCommon);
        ButtonCluster.SetRedirector(redirectCommon);
        ButtonGallery.SetRedirector(redirectCommon);
        ButtonNavigatorStack.SetRedirector(redirectCommon);
        ButtonNavigatorOverflow.SetRedirector(redirectCommon);
        ButtonNavigatorMini.SetRedirector(redirectCommon);
        ButtonInputControl.SetRedirector(redirectCommon);
        ButtonListItem.SetRedirector(redirectCommon);
        ButtonForm.SetRedirector(redirectCommon);
        ButtonFormClose.SetRedirector(redirectCommon);
        ButtonCommand.SetRedirector(redirectCommon);
        ButtonCustom1.SetRedirector(redirectCommon);
        ButtonCustom2.SetRedirector(redirectCommon);
        ButtonCustom3.SetRedirector(redirectCommon);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ButtonCommon.IsDefault &&
                                      ButtonStandalone.IsDefault &&
                                      ButtonAlternate.IsDefault &&
                                      ButtonLowProfile.IsDefault &&
                                      ButtonButtonSpec.IsDefault &&
                                      ButtonBreadCrumb.IsDefault &&
                                      ButtonCalendarDay.IsDefault &&
                                      ButtonCluster.IsDefault &&
                                      ButtonGallery.IsDefault &&
                                      ButtonNavigatorStack.IsDefault &&
                                      ButtonNavigatorOverflow.IsDefault &&
                                      ButtonNavigatorMini.IsDefault &&
                                      ButtonInputControl.IsDefault &&
                                      ButtonListItem.IsDefault &&
                                      ButtonForm.IsDefault &&
                                      ButtonFormClose.IsDefault &&
                                      ButtonCommand.IsDefault &&
                                      ButtonCustom1.IsDefault &&
                                      ButtonCustom2.IsDefault &&
                                      ButtonCustom3.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="common">Reference to common settings.</param>
    public void PopulateFromBase(KryptonPaletteCommon common)
    {
        // Populate only the designated styles
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonStandalone;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonStandalone;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonStandalone;
        ButtonStandalone.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonAlternate;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonAlternate;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonAlternate;
        ButtonAlternate.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonLowProfile;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonLowProfile;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonLowProfile;
        ButtonLowProfile.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonButtonSpec;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonButtonSpec;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonButtonSpec;
        ButtonButtonSpec.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonBreadCrumb;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonBreadCrumb;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonBreadCrumb;
        ButtonBreadCrumb.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonCalendarDay;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonCalendarDay;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonCalendarDay;
        ButtonCalendarDay.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonNavigatorStack;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonNavigatorStack;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonNavigatorStack;
        ButtonNavigatorStack.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonNavigatorOverflow;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonNavigatorOverflow;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonNavigatorOverflow;
        ButtonNavigatorOverflow.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonNavigatorMini;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonNavigatorMini;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonNavigatorMini;
        ButtonNavigatorMini.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonInputControl;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonInputControl;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonInputControl;
        ButtonInputControl.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonListItem;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonListItem;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonListItem;
        ButtonListItem.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonForm;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonForm;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonForm;
        ButtonForm.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonFormClose;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonFormClose;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonFormClose;
        ButtonFormClose.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonCommand;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonCommand;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonCommand;
        ButtonCommand.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonCluster;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonCluster;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonCluster;
        ButtonCluster.PopulateFromBase();
        common.StateCommon.BackStyle = PaletteBackStyle.ButtonGallery;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonGallery;
        common.StateCommon.ContentStyle = PaletteContentStyle.ButtonGallery;
        ButtonGallery.PopulateFromBase();
    }
    #endregion

    #region ButtonCommon
    /// <summary>
    /// Gets access to the common inherited button appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common inherited button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonCommon { get; }

    private bool ShouldSerializeButtonCommon() => !ButtonCommon.IsDefault;

    #endregion

    #region ButtonStandalone
    /// <summary>
    /// Gets access to the Standalone appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining Standalone appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonStandalone { get; }

    private bool ShouldSerializeButtonStandalone() => !ButtonStandalone.IsDefault;

    #endregion

    #region ButtonAlternate
    /// <summary>
    /// Gets access to the Alternate appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining Alternate appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonAlternate { get; }

    private bool ShouldSerializeButtonAlternate() => !ButtonAlternate.IsDefault;

    #endregion

    #region ButtonLowProfile
    /// <summary>
    /// Gets access to the LowProfile appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining LowProfile appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonLowProfile { get; }

    private bool ShouldSerializeButtonLowProfile() => !ButtonLowProfile.IsDefault;

    #endregion

    #region ButtonButtonSpec
    /// <summary>
    /// Gets access to the ButtonSpec appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ButtonSpec appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonButtonSpec { get; }

    private bool ShouldSerializeButtonButtonSpec() => !ButtonButtonSpec.IsDefault;

    #endregion

    #region ButtonBreadCrumb
    /// <summary>
    /// Gets access to the BreadCrumb appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining BreadCrumb appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonBreadCrumb { get; }

    private bool ShouldSerializeButtonBreadCrumb() => !ButtonBreadCrumb.IsDefault;

    #endregion

    #region ButtonCalendarDay
    /// <summary>
    /// Gets access to the CalendarDay appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining CalendarDay appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonCalendarDay { get; }

    private bool ShouldSerializeButtonCalendarDay() => !ButtonCalendarDay.IsDefault;

    #endregion

    #region ButtonCluster
    /// <summary>
    /// Gets access to the ButtonCluster appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ButtonCluster appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonCluster { get; }

    private bool ShouldSerializeButtonCluster() => !ButtonCluster.IsDefault;

    #endregion

    #region ButtonGallery
    /// <summary>
    /// Gets access to the ButtonGallery appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ButtonGallery appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonGallery { get; }

    private bool ShouldSerializeButtonGallery() => !ButtonGallery.IsDefault;

    #endregion

    #region ButtonNavigatorStack
    /// <summary>
    /// Gets access to the ButtonNavigatorStack appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ButtonNavigatorStack appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonNavigatorStack { get; }

    private bool ShouldSerializeButtonNavigatorStack() => !ButtonNavigatorStack.IsDefault;

    #endregion

    #region ButtonNavigatorOverflow
    /// <summary>
    /// Gets access to the ButtonNavigatorOverflow appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ButtonNavigatorOverflow appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonNavigatorOverflow { get; }

    private bool ShouldSerializeButtonNavigatorOverflow() => !ButtonNavigatorOverflow.IsDefault;

    #endregion

    #region ButtonNavigatorMini
    /// <summary>
    /// Gets access to the ButtonNavigatorMini appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ButtonNavigatorMini appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonNavigatorMini { get; }

    private bool ShouldSerializeButtonNavigatorMini() => !ButtonNavigatorMini.IsDefault;

    #endregion

    #region ButtonInputControl
    /// <summary>
    /// Gets access to the ButtonInputControl appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ButtonInputControl appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonInputControl { get; }

    private bool ShouldSerializeButtonInputControl() => !ButtonInputControl.IsDefault;

    #endregion

    #region ButtonListItem
    /// <summary>
    /// Gets access to the ButtonListItem appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ButtonListItem appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonListItem { get; }

    private bool ShouldSerializeButtonListItem() => !ButtonListItem.IsDefault;

    #endregion

    #region ButtonForm
    /// <summary>
    /// Gets access to the ButtonForm appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ButtonForm appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonForm { get; }

    private bool ShouldSerializeButtonForm() => !ButtonForm.IsDefault;

    #endregion

    #region ButtonFormClose
    /// <summary>
    /// Gets access to the ButtonFormClose appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ButtonFormClose appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonFormClose { get; }

    private bool ShouldSerializeButtonFormClose() => !ButtonFormClose.IsDefault;

    #endregion

    #region ButtonCommand
    /// <summary>
    /// Gets access to the ButtonCommand appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ButtonCommand appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonCommand { get; }

    private bool ShouldSerializeButtonCommand() => !ButtonCommand.IsDefault;

    #endregion

    #region ButtonCustom1
    /// <summary>
    /// Gets access to the Custom1 appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining Custom1 appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonCustom1 { get; }

    private bool ShouldSerializeButtonCustom1() => !ButtonCustom1.IsDefault;

    #endregion

    #region ButtonCustom2
    /// <summary>
    /// Gets access to the Custom2 appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining Custom2 appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonCustom2 { get; }

    private bool ShouldSerializeButtonCustom2() => !ButtonCustom2.IsDefault;

    #endregion

    #region ButtonCustom3
    /// <summary>
    /// Gets access to the Custom3 appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining Custom3 appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteCheckButton ButtonCustom3 { get; }

    private bool ShouldSerializeButtonCustom3() => !ButtonCustom3.IsDefault;

    #endregion
}