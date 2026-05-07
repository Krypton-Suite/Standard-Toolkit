#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Implement storage for a ribbon palette. 
/// </summary>
public class PaletteRibbonRedirect : PaletteMetricRedirect
{
    #region Instance Fields
    // Storage
    private readonly PaletteRibbonBack _ribbonAppButton;
    private readonly PaletteRibbonBack _ribbonAppMenuInner;
    private readonly PaletteRibbonBack _ribbonAppMenuOuter;
    private readonly PaletteRibbonBack _ribbonAppMenuDocs;
    private readonly PaletteRibbonText _ribbonAppMenuDocsTitle;
    private readonly PaletteRibbonText _ribbonAppMenuDocsEntry;
    private readonly PaletteRibbonGeneral _ribbonGeneral;
    private readonly PaletteRibbonBack _ribbonGroupBackArea;
    private readonly PaletteRibbonText _ribbonGroupButtonText;
    private readonly PaletteRibbonText _ribbonGroupCheckBoxText;
    private readonly PaletteRibbonBack _ribbonGroupCollapsedBack;
    private readonly PaletteRibbonBack _ribbonGroupCollapsedBorder;
    private readonly PaletteRibbonBack _ribbonGroupCollapsedFrameBack;
    private readonly PaletteRibbonBack _ribbonGroupCollapsedFrameBorder;
    private readonly PaletteRibbonText _ribbonGroupCollapsedText;
    private readonly PaletteRibbonBack _ribbonGroupNormalBorder;
    private readonly PaletteRibbonDouble _ribbonGroupNormalTitle;
    private readonly PaletteRibbonImages _ribbonImages;
    private readonly PaletteRibbonText _ribbonGroupRadioButtonText;
    private readonly PaletteRibbonText _ribbonGroupLabelText;
    private readonly PaletteRibbonDouble _ribbonTab;
    private readonly PaletteRibbonBack _ribbonQATFullbar;
    private readonly PaletteRibbonBack _ribbonQATMinibarActive;
    private readonly PaletteRibbonBack _ribbonQATMinibarInactive;
    private readonly PaletteRibbonBack _ribbonQATOverflow;

    // Redirection
    private readonly PaletteRibbonBackInheritRedirect _ribbonAppButtonInherit;
    private readonly PaletteRibbonFileAppTabInheritRedirect _ribbonFileAppTabInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonAppMenuOuterInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonAppMenuInnerInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonAppMenuDocsInherit;
    private readonly PaletteRibbonTextInheritRedirect _ribbonAppMenuDocsTitleInherit;
    private readonly PaletteRibbonTextInheritRedirect _ribbonAppMenuDocsEntryInherit;
    private readonly PaletteRibbonGeneralInheritRedirect _ribbonGeneralInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonGroupBackAreaInherit;
    private readonly PaletteRibbonTextInheritRedirect _ribbonGroupCheckBoxTextInherit;
    private readonly PaletteRibbonTextInheritRedirect _ribbonGroupButtonTextInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonGroupCollapsedBackInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonGroupCollapsedBorderInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonGroupCollapsedFrameBackInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonGroupCollapsedFrameBorderInherit;
    private readonly PaletteRibbonTextInheritRedirect _ribbonGroupCollapsedTextInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonGroupNormalBorderInherit;
    private readonly PaletteRibbonDoubleInheritRedirect _ribbonGroupNormalTitleInherit;
    private readonly PaletteRibbonTextInheritRedirect _ribbonGroupRadioButtonTextInherit;
    private readonly PaletteRibbonTextInheritRedirect _ribbonGroupLabelTextInherit;
    private readonly PaletteRibbonDoubleInheritRedirect _ribbonTabInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonQATFullbarInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonQATMinibarInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonQATOverflowInherit;

    // Style Redirection

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteDoubleRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="panelBackStyle">Initial background style.</param>
    /// <param name="needPaint">Paint delegate.</param>
    public PaletteRibbonRedirect([DisallowNull] PaletteRedirect redirect,
        PaletteBackStyle panelBackStyle,
        NeedPaintHandler needPaint)
        : base(redirect)
    {
        Debug.Assert(redirect != null);
        var _redirect = redirect!;
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create the style redirection instances
        RibbonGroupButton = new PaletteTripleRedirect(_redirect, PaletteBackStyle.ButtonButtonSpec, PaletteBorderStyle.ButtonButtonSpec, PaletteContentStyle.ButtonButtonSpec, needPaint);
        RibbonGroupClusterButton = new PaletteTripleRedirect(_redirect, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, needPaint);
        RibbonGroupCollapsedButton = new PaletteTripleRedirect(_redirect, PaletteBackStyle.ButtonButtonSpec, PaletteBorderStyle.ButtonButtonSpec, PaletteContentStyle.ButtonButtonSpec, needPaint);
        RibbonGroupDialogButton = new PaletteTripleRedirect(_redirect, PaletteBackStyle.ButtonButtonSpec, PaletteBorderStyle.ButtonButtonSpec, PaletteContentStyle.ButtonButtonSpec, needPaint);
        RibbonKeyTip = new PaletteTripleRedirect(_redirect, PaletteBackStyle.ControlToolTip, PaletteBorderStyle.ControlToolTip, PaletteContentStyle.LabelKeyTip, needPaint);
        RibbonQATButton = new PaletteTripleRedirect(_redirect, PaletteBackStyle.ButtonButtonSpec, PaletteBorderStyle.ButtonButtonSpec, PaletteContentStyle.ButtonButtonSpec, needPaint);
        RibbonScroller = new PaletteTripleRedirect(_redirect, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, needPaint);

        // Create the redirection instances
        _ribbonAppButtonInherit = new PaletteRibbonBackInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonAppButton);
        _ribbonFileAppTabInherit = new PaletteRibbonFileAppTabInheritRedirect(_redirect);
        _ribbonAppMenuInnerInherit = new PaletteRibbonBackInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonAppMenuInner);
        _ribbonAppMenuOuterInherit = new PaletteRibbonBackInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonAppMenuOuter);
        _ribbonAppMenuDocsInherit = new PaletteRibbonBackInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonAppMenuDocs);
        _ribbonAppMenuDocsTitleInherit = new PaletteRibbonTextInheritRedirect(_redirect, PaletteRibbonTextStyle.RibbonAppMenuDocsTitle);
        _ribbonAppMenuDocsEntryInherit = new PaletteRibbonTextInheritRedirect(_redirect, PaletteRibbonTextStyle.RibbonAppMenuDocsEntry);
        _ribbonGeneralInherit = new PaletteRibbonGeneralInheritRedirect(_redirect);
        _ribbonGroupBackAreaInherit = new PaletteRibbonBackInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonGroupArea);
        _ribbonGroupButtonTextInherit = new PaletteRibbonTextInheritRedirect(_redirect, PaletteRibbonTextStyle.RibbonGroupButtonText);
        _ribbonGroupCheckBoxTextInherit = new PaletteRibbonTextInheritRedirect(_redirect, PaletteRibbonTextStyle.RibbonGroupCheckBoxText);
        _ribbonGroupCollapsedBackInherit = new PaletteRibbonBackInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonGroupCollapsedBack);
        _ribbonGroupCollapsedBorderInherit = new PaletteRibbonBackInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonGroupCollapsedBorder);
        _ribbonGroupCollapsedFrameBackInherit = new PaletteRibbonBackInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack);
        _ribbonGroupCollapsedFrameBorderInherit = new PaletteRibbonBackInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder);
        _ribbonGroupCollapsedTextInherit = new PaletteRibbonTextInheritRedirect(_redirect, PaletteRibbonTextStyle.RibbonGroupCollapsedText);
        _ribbonGroupNormalBorderInherit = new PaletteRibbonBackInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonGroupNormalBorder);
        _ribbonGroupNormalTitleInherit = new PaletteRibbonDoubleInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonGroupNormalTitle, PaletteRibbonTextStyle.RibbonGroupNormalTitle);
        _ribbonGroupRadioButtonTextInherit = new PaletteRibbonTextInheritRedirect(_redirect, PaletteRibbonTextStyle.RibbonGroupRadioButtonText);
        _ribbonGroupLabelTextInherit = new PaletteRibbonTextInheritRedirect(_redirect, PaletteRibbonTextStyle.RibbonGroupLabelText);
        _ribbonTabInherit = new PaletteRibbonDoubleInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonTab, PaletteRibbonTextStyle.RibbonTab);
        _ribbonQATFullbarInherit = new PaletteRibbonBackInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonQATFullbar);
        _ribbonQATMinibarInherit = new PaletteRibbonBackInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonQATMinibar);
        _ribbonQATOverflowInherit = new PaletteRibbonBackInheritRedirect(_redirect, PaletteRibbonBackStyle.RibbonQATOverflow);        

        // Create storage that maps onto the inherit instances
        _ribbonAppButton = new PaletteRibbonBack(_ribbonAppButtonInherit, needPaint);
        RibbonFileAppTab = new PaletteRibbonFileAppTab(_ribbonFileAppTabInherit, needPaint);
        _ribbonAppMenuInner = new PaletteRibbonBack(_ribbonAppMenuInnerInherit, needPaint);
        _ribbonAppMenuOuter = new PaletteRibbonBack(_ribbonAppMenuOuterInherit, needPaint);
        _ribbonAppMenuDocs = new PaletteRibbonBack(_ribbonAppMenuDocsInherit, needPaint);
        _ribbonAppMenuDocsTitle = new PaletteRibbonText(_ribbonAppMenuDocsTitleInherit, needPaint);
        _ribbonAppMenuDocsEntry = new PaletteRibbonText(_ribbonAppMenuDocsEntryInherit, needPaint);
        _ribbonGeneral = new PaletteRibbonGeneral(_ribbonGeneralInherit, needPaint);
        _ribbonGroupBackArea = new PaletteRibbonBack(_ribbonGroupBackAreaInherit, needPaint);
        _ribbonGroupButtonText = new PaletteRibbonText(_ribbonGroupButtonTextInherit, needPaint);
        _ribbonGroupCheckBoxText = new PaletteRibbonText(_ribbonGroupCheckBoxTextInherit, needPaint);
        _ribbonGroupCollapsedBack = new PaletteRibbonBack(_ribbonGroupCollapsedBackInherit, needPaint);
        _ribbonGroupCollapsedBorder = new PaletteRibbonBack(_ribbonGroupCollapsedBorderInherit, needPaint);
        _ribbonGroupCollapsedFrameBack = new PaletteRibbonBack(_ribbonGroupCollapsedFrameBackInherit, needPaint);
        _ribbonGroupCollapsedFrameBorder = new PaletteRibbonBack(_ribbonGroupCollapsedFrameBorderInherit, needPaint);
        _ribbonGroupCollapsedText = new PaletteRibbonText(_ribbonGroupCollapsedTextInherit, needPaint);
        _ribbonGroupNormalBorder = new PaletteRibbonBack(_ribbonGroupNormalBorderInherit, needPaint);
        _ribbonGroupNormalTitle = new PaletteRibbonDouble(_ribbonGroupNormalTitleInherit, _ribbonGroupNormalTitleInherit, needPaint);
        _ribbonGroupRadioButtonText = new PaletteRibbonText(_ribbonGroupRadioButtonTextInherit, needPaint);
        _ribbonGroupLabelText = new PaletteRibbonText(_ribbonGroupLabelTextInherit, needPaint);
        _ribbonTab = new PaletteRibbonDouble(_ribbonTabInherit, _ribbonTabInherit, needPaint);
        _ribbonQATFullbar = new PaletteRibbonBack(_ribbonQATFullbarInherit, needPaint);
        _ribbonQATMinibarActive = new PaletteRibbonBack(_ribbonQATMinibarInherit, needPaint);
        _ribbonQATMinibarInactive = new PaletteRibbonBack(_ribbonQATMinibarInherit, needPaint);
        _ribbonQATOverflow = new PaletteRibbonBack(_ribbonQATOverflowInherit, needPaint);
        _ribbonImages = new PaletteRibbonImages(_redirect, NeedPaintDelegate);
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public override void SetRedirector(PaletteRedirect redirect)
    {
        base.SetRedirector(redirect);
        RibbonGroupButton.SetRedirector(redirect);
        RibbonGroupClusterButton.SetRedirector(redirect);
        RibbonGroupCollapsedButton.SetRedirector(redirect);
        RibbonGroupDialogButton.SetRedirector(redirect);
        RibbonKeyTip.SetRedirector(redirect);
        RibbonQATButton.SetRedirector(redirect);
        RibbonScroller.SetRedirector(redirect);
        _ribbonAppButtonInherit.SetRedirector(redirect);
        _ribbonFileAppTabInherit.SetRedirector(redirect);
        _ribbonAppMenuInnerInherit.SetRedirector(redirect);
        _ribbonAppMenuOuterInherit.SetRedirector(redirect);
        _ribbonAppMenuDocsInherit.SetRedirector(redirect);
        _ribbonAppMenuDocsTitleInherit.SetRedirector(redirect);
        _ribbonAppMenuDocsEntryInherit.SetRedirector(redirect);
        _ribbonGeneralInherit.SetRedirector(redirect);
        _ribbonGroupBackAreaInherit.SetRedirector(redirect);
        _ribbonGroupCheckBoxTextInherit.SetRedirector(redirect);
        _ribbonGroupNormalBorderInherit.SetRedirector(redirect);
        _ribbonGroupNormalTitleInherit.SetRedirector(redirect);
        _ribbonGroupButtonTextInherit.SetRedirector(redirect);
        _ribbonGroupCollapsedBackInherit.SetRedirector(redirect);
        _ribbonGroupCollapsedBorderInherit.SetRedirector(redirect);
        _ribbonGroupCollapsedFrameBackInherit.SetRedirector(redirect);
        _ribbonGroupCollapsedFrameBorderInherit.SetRedirector(redirect);
        _ribbonGroupCollapsedTextInherit.SetRedirector(redirect);
        _ribbonGroupRadioButtonTextInherit.SetRedirector(redirect);
        _ribbonGroupLabelTextInherit.SetRedirector(redirect);
        _ribbonTabInherit.SetRedirector(redirect);
        _ribbonQATFullbarInherit.SetRedirector(redirect);
        _ribbonQATMinibarInherit.SetRedirector(redirect);
        _ribbonQATOverflowInherit.SetRedirector(redirect);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => RibbonAppButton.IsDefault
                                      && RibbonFileAppTab.IsDefault
                                      && RibbonAppMenuOuter.IsDefault
                                      && RibbonAppMenuInner.IsDefault
                                      && RibbonAppMenuDocs.IsDefault
                                      && RibbonAppMenuDocsTitle.IsDefault
                                      && RibbonAppMenuDocsEntry.IsDefault
                                      && RibbonGeneral.IsDefault
                                      && RibbonGroupBackArea.IsDefault
                                      && RibbonGroupCheckBoxText.IsDefault
                                      && RibbonGroupNormalBorder.IsDefault
                                      && RibbonGroupNormalTitle.IsDefault
                                      && RibbonGroupButtonText.IsDefault
                                      && RibbonGroupCollapsedBorder.IsDefault
                                      && RibbonGroupCollapsedBack.IsDefault
                                      && RibbonGroupCollapsedFrameBorder.IsDefault
                                      && RibbonGroupCollapsedFrameBack.IsDefault
                                      && RibbonGroupCollapsedText.IsDefault
                                      && RibbonGroupRadioButtonText.IsDefault
                                      && RibbonGroupLabelText.IsDefault
                                      && RibbonImages.IsDefault
                                      && RibbonTab.IsDefault
                                      && RibbonQATFullbar.IsDefault
                                      && RibbonQATMinibarActive.IsDefault
                                      && RibbonQATMinibarInactive.IsDefault
                                      && RibbonQATOverflow.IsDefault;

    #endregion

    #region RibbonAppButton
    /// <summary>
    /// Gets access to the application button palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining application button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonAppButton => _ribbonAppButton;
    private bool ShouldSerializeRibbonAppButton() => !_ribbonAppButton.IsDefault;
    #endregion

    #region RibbonFileAppTab
    /// <summary>
    /// Gets access to the application button palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining application button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonFileAppTab RibbonFileAppTab { get; }
    private bool ShouldSerializeRibbonFileAppTab() => !RibbonFileAppTab.IsDefault;
    #endregion

    #region RibbonAppMenuOuter
    /// <summary>
    /// Gets access to the application button menu outer palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining application button menu outer appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonAppMenuOuter => _ribbonAppMenuOuter;

    private bool ShouldSerializeRibbonAppMenuOuter() => !_ribbonAppMenuOuter.IsDefault;

    #endregion

    #region RibbonAppMenuInner
    /// <summary>
    /// Gets access to the application button menu inner palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining application button menu inner appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonAppMenuInner => _ribbonAppMenuInner;

    private bool ShouldSerializeRibbonAppMenuInner() => !_ribbonAppMenuInner.IsDefault;

    #endregion

    #region RibbonAppMenuDocs
    /// <summary>
    /// Gets access to the application button menu recent docs palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining application button menu recent docs appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonAppMenuDocs => _ribbonAppMenuDocs;

    private bool ShouldSerializeRibbonAppMenuDocs() => !_ribbonAppMenuDocs.IsDefault;

    #endregion

    #region RibbonAppMenuDocsTitle
    /// <summary>
    /// Gets access to the application button menu recent documents title.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining application button menu recent documents title.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonAppMenuDocsTitle => _ribbonAppMenuDocsTitle;

    private bool ShouldSerializeRibbonAppMenuDocsTitle() => !_ribbonAppMenuDocsTitle.IsDefault;

    #endregion

    #region RibbonAppMenuDocsEntry
    /// <summary>
    /// Gets access to the application button menu recent documents entry.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining application button menu recent documents entry.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonAppMenuDocsEntry => _ribbonAppMenuDocsEntry;

    private bool ShouldSerializeRibbonAppMenuDocsEntry() => !_ribbonAppMenuDocsEntry.IsDefault;

    #endregion

    #region RibbonGeneral
    /// <summary>
    /// Gets access to the ribbon general palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining general ribbon appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonGeneral RibbonGeneral => _ribbonGeneral;

    private bool ShouldSerializeRibbonGeneral() => !_ribbonGeneral.IsDefault;

    #endregion

    #region RibbonGroupArea
    /// <summary>
    /// Gets access to the ribbon group area palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group area appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonGroupBackArea => _ribbonGroupBackArea;

    private bool ShouldSerializeRibbonGroupBackArea() => !_ribbonGroupBackArea.IsDefault;

    #endregion

    #region RibbonGroupCheckBoxText
    /// <summary>
    /// Gets access to the ribbon group check box label palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group check box label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonGroupCheckBoxText => _ribbonGroupCheckBoxText;
    private bool ShouldSerializeRibbonGroupCheckBoxText() => !_ribbonGroupCheckBoxText.IsDefault;
    #endregion

    #region RibbonGroupButtonText
    /// <summary>
    /// Gets access to the ribbon group button text palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group button text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonGroupButtonText => _ribbonGroupButtonText;

    private bool ShouldSerializeRibbonGroupButtonText() => !_ribbonGroupButtonText.IsDefault;

    #endregion

    #region RibbonGroupCollapsedBorder
    /// <summary>
    /// Gets access to the ribbon group collapsed border palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group collapsed border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonGroupCollapsedBorder => _ribbonGroupCollapsedBorder;

    private bool ShouldSerializeRibbonGroupCollapsedBorder() => !_ribbonGroupCollapsedBorder.IsDefault;

    #endregion

    #region RibbonGroupCollapsedBack
    /// <summary>
    /// Gets access to the ribbon group collapsed background palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group collapsed background appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonGroupCollapsedBack => _ribbonGroupCollapsedBack;

    private bool ShouldSerializeRibbonGroupCollapsedBack() => !_ribbonGroupCollapsedBack.IsDefault;

    #endregion

    #region RibbonGroupCollapsedFrameBorder
    /// <summary>
    /// Gets access to the ribbon group collapsed frame border palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group collapsed frame border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonGroupCollapsedFrameBorder => _ribbonGroupCollapsedFrameBorder;

    private bool ShouldSerializeRibbonGroupCollapsedFrameBorder() => !_ribbonGroupCollapsedFrameBorder.IsDefault;

    #endregion

    #region RibbonGroupCollapsedFrameBack
    /// <summary>
    /// Gets access to the ribbon group collapsed frame background palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group collapsed frame background appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonGroupCollapsedFrameBack => _ribbonGroupCollapsedFrameBack;

    private bool ShouldSerializeRibbonGroupCollapsedFrameBack() => !_ribbonGroupCollapsedFrameBack.IsDefault;

    #endregion

    #region RibbonGroupCollapsedText
    /// <summary>
    /// Gets access to the ribbon group collapsed text palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group collapsed text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonGroupCollapsedText => _ribbonGroupCollapsedText;

    private bool ShouldSerializeRibbonGroupCollapsedText() => !_ribbonGroupCollapsedText.IsDefault;

    #endregion

    #region RibbonGroupNormalBorder
    /// <summary>
    /// Gets access to the ribbon group normal border palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group normal border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonGroupNormalBorder => _ribbonGroupNormalBorder;

    private bool ShouldSerializeRibbonGroupNormalBorder() => !_ribbonGroupNormalBorder.IsDefault;

    #endregion

    #region RibbonGroupNormalTitle
    /// <summary>
    /// Gets access to the ribbon group normal title palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group normal title appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonDouble RibbonGroupNormalTitle => _ribbonGroupNormalTitle;

    private bool ShouldSerializeRibbonGroupNormalTitle() => !_ribbonGroupNormalTitle.IsDefault;

    #endregion

    #region RibbonGroupRadioButtonText
    /// <summary>
    /// Gets access to the ribbon group radio button label palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group radio button label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonGroupRadioButtonText => _ribbonGroupRadioButtonText;

    private bool ShouldSerializeRibbonGroupRadioButtonText() => !_ribbonGroupRadioButtonText.IsDefault;

    #endregion

    #region RibbonGroupLabelText
    /// <summary>
    /// Gets access to the ribbon group label text palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group label text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonGroupLabelText => _ribbonGroupLabelText;

    private bool ShouldSerializeRibbonGroupLabelText() => !_ribbonGroupLabelText.IsDefault;

    #endregion

    #region RibbonImages
    /// <summary>
    /// Gets access to the ribbon images overrides.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon images.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonImages RibbonImages => _ribbonImages;

    private bool ShouldSerializeRibbonImages() => !_ribbonImages.IsDefault;

    #endregion

    #region RibbonTab
    /// <summary>
    /// Gets access to the ribbon tab palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonDouble RibbonTab => _ribbonTab;

    private bool ShouldSerializeRibbonTab() => !_ribbonTab.IsDefault;

    #endregion

    #region RibbonQATFullbar
    /// <summary>
    /// Gets access to the ribbon quick access toolbar in full mode palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon quick access toolbar in full mode.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonQATFullbar => _ribbonQATFullbar;

    private bool ShouldSerializeRibbonQATFullbar() => !_ribbonQATFullbar.IsDefault;

    #endregion

    #region RibbonQATMinibarActive
    /// <summary>
    /// Gets access to the ribbon quick access toolbar in mini mode palette details when form active.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon quick access toolbar in mini mode when form active.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonQATMinibarActive => _ribbonQATMinibarActive;

    private bool ShouldSerializeRibbonQATMinibarActive() => !_ribbonQATMinibarActive.IsDefault;

    #endregion

    #region RibbonQATMinibarInactive
    /// <summary>
    /// Gets access to the ribbon quick access toolbar in mini mode palette details when form inactive.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon quick access toolbar in mini mode when form inactive.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonQATMinibarInactive => _ribbonQATMinibarInactive;

    private bool ShouldSerializeRibbonQATMinibarInactive() => !_ribbonQATMinibarInactive.IsDefault;

    #endregion

    #region RibbonQATOverflow
    /// <summary>
    /// Gets access to the ribbon quick access toolbar overflow palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon quick access toolbar overflow.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonQATOverflow => _ribbonQATOverflow;

    private bool ShouldSerializeRibbonQATOverflow() => !_ribbonQATOverflow.IsDefault;

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

    #region Internal
    internal PaletteTripleRedirect RibbonGroupButton { get; }

    internal PaletteTripleRedirect RibbonGroupClusterButton { get; }

    internal PaletteTripleRedirect RibbonGroupCollapsedButton { get; }

    internal PaletteTripleRedirect RibbonGroupDialogButton { get; }

    internal PaletteTripleRedirect RibbonKeyTip { get; }

    internal PaletteTripleRedirect RibbonQATButton { get; }

    internal PaletteTripleRedirect RibbonScroller { get; }

    #endregion
}