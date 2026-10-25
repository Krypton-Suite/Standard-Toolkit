#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Settings associated with ribbon control.
/// </summary>
public class KryptonPaletteRibbon : Storage
{
    #region Instance Fields
    private readonly PaletteRibbonBackInheritRedirect _ribbonAppMenuOuterInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonAppMenuInnerInherit;
    private readonly PaletteRibbonBackInheritRedirect _ribbonAppMenuDocsInherit;
    private readonly PaletteRibbonTextInheritRedirect _ribbonAppMenuDocsTitleInherit;
    private readonly PaletteRibbonTextInheritRedirect _ribbonAppMenuDocsEntryInherit;
    private readonly PaletteRibbonGeneralInheritRedirect _ribbonGeneralRedirect;
    private readonly PaletteRibbonFileAppTabInheritRedirect _ribbonFileAppTabRedirect;
    private readonly PaletteRibbonBackInheritRedirect _ribbonQATFullRedirect;
    private readonly PaletteRibbonBackInheritRedirect _ribbonQATOverRedirect;
    private readonly PaletteRibbonBackInheritRedirect _ribbonGalleryBackRedirect;
    private readonly PaletteRibbonBackInheritRedirect _ribbonGalleryBorderRedirect;

    private readonly PaletteRibbonBack _ribbonAppMenuInner;
    private readonly PaletteRibbonBack _ribbonAppMenuOuter;
    private readonly PaletteRibbonBack _ribbonAppMenuDocs;
    private readonly PaletteRibbonText _ribbonAppMenuDocsTitle;
    private readonly PaletteRibbonText _ribbonAppMenuDocsEntry;
    private readonly PaletteRibbonBack _ribbonGalleryBack;
    private readonly PaletteRibbonBack _ribbonGalleryBorder;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteRibbon class.
    /// </summary>
    /// <param name="redirect">Redirector to inherit values from.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteRibbon([DisallowNull] PaletteRedirect redirect,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirect != null);

        // Store incoming reference
        PaletteRedirect redirect1 = redirect!;

        // Create redirectors
        _ribbonGeneralRedirect = new PaletteRibbonGeneralInheritRedirect(redirect1);
        _ribbonFileAppTabRedirect = new PaletteRibbonFileAppTabInheritRedirect(redirect1);
        _ribbonAppMenuInnerInherit = new PaletteRibbonBackInheritRedirect(redirect1, PaletteRibbonBackStyle.RibbonAppMenuInner);
        _ribbonAppMenuOuterInherit = new PaletteRibbonBackInheritRedirect(redirect1, PaletteRibbonBackStyle.RibbonAppMenuOuter);
        _ribbonAppMenuDocsInherit = new PaletteRibbonBackInheritRedirect(redirect1, PaletteRibbonBackStyle.RibbonAppMenuDocs);
        _ribbonAppMenuDocsTitleInherit = new PaletteRibbonTextInheritRedirect(redirect1, PaletteRibbonTextStyle.RibbonAppMenuDocsTitle);
        _ribbonAppMenuDocsEntryInherit = new PaletteRibbonTextInheritRedirect(redirect1, PaletteRibbonTextStyle.RibbonAppMenuDocsEntry);
        _ribbonQATFullRedirect = new PaletteRibbonBackInheritRedirect(redirect1, PaletteRibbonBackStyle.RibbonQATFullbar);
        _ribbonQATOverRedirect = new PaletteRibbonBackInheritRedirect(redirect1, PaletteRibbonBackStyle.RibbonQATOverflow);
        _ribbonGalleryBackRedirect = new PaletteRibbonBackInheritRedirect(redirect1, PaletteRibbonBackStyle.RibbonGalleryBack);
        _ribbonGalleryBorderRedirect = new PaletteRibbonBackInheritRedirect(redirect1, PaletteRibbonBackStyle.RibbonGalleryBorder);

        // Create palettes
        RibbonGeneral = new PaletteRibbonGeneral(_ribbonGeneralRedirect, needPaint);
        RibbonFileAppTab = new PaletteRibbonFileAppTab(_ribbonFileAppTabRedirect, needPaint);
        RibbonAppButton = new KryptonPaletteRibbonAppButton(redirect1, needPaint);
        _ribbonAppMenuInner = new PaletteRibbonBack(_ribbonAppMenuInnerInherit, needPaint);
        _ribbonAppMenuOuter = new PaletteRibbonBack(_ribbonAppMenuOuterInherit, needPaint);
        _ribbonAppMenuDocs = new PaletteRibbonBack(_ribbonAppMenuDocsInherit, needPaint);
        _ribbonAppMenuDocsTitle = new PaletteRibbonText(_ribbonAppMenuDocsTitleInherit, needPaint);
        _ribbonAppMenuDocsEntry = new PaletteRibbonText(_ribbonAppMenuDocsEntryInherit, needPaint);
        RibbonGroupArea = new KryptonPaletteRibbonGroupArea(redirect1, needPaint);
        RibbonGroupButtonText = new KryptonPaletteRibbonGroupButtonText(redirect1, needPaint);
        RibbonGroupCheckBoxText = new KryptonPaletteRibbonGroupCheckBoxText(redirect1, needPaint);
        RibbonGroupNormalBorder = new KryptonPaletteRibbonGroupNormalBorder(redirect1, needPaint);
        RibbonGroupNormalTitle = new KryptonPaletteRibbonGroupNormalTitle(redirect1, needPaint);
        RibbonGroupCollapsedBorder = new KryptonPaletteRibbonGroupCollapsedBorder(redirect1, needPaint);
        RibbonGroupCollapsedBack = new KryptonPaletteRibbonGroupCollapsedBack(redirect1, needPaint);
        RibbonGroupCollapsedFrameBorder = new KryptonPaletteRibbonGroupCollapsedFrameBorder(redirect1, needPaint);
        RibbonGroupCollapsedFrameBack = new KryptonPaletteRibbonGroupCollapsedFrameBack(redirect1, needPaint);
        RibbonGroupCollapsedText = new KryptonPaletteRibbonGroupCollapsedText(redirect1, needPaint);
        RibbonGroupRadioButtonText = new KryptonPaletteRibbonGroupRadioButtonText(redirect1, needPaint);
        RibbonGroupLabelText = new KryptonPaletteRibbonGroupLabelText(redirect1, needPaint);
        RibbonQATFullbar = new PaletteRibbonBack(_ribbonQATFullRedirect, needPaint);
        RibbonQATMinibar = new KryptonPaletteRibbonQATMinibar(redirect1, needPaint);
        RibbonQATOverflow = new PaletteRibbonBack(_ribbonQATOverRedirect, needPaint);
        RibbonTab = new KryptonPaletteRibbonTab(redirect1, needPaint);
        _ribbonGalleryBack = new PaletteRibbonBack(_ribbonGalleryBackRedirect, needPaint);
        _ribbonGalleryBorder = new PaletteRibbonBack(_ribbonGalleryBorderRedirect, needPaint);
    }

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => RibbonAppButton.IsDefault &&
                                      RibbonAppMenuOuter.IsDefault &&
                                      RibbonAppMenuInner.IsDefault &&
                                      RibbonAppMenuDocs.IsDefault &&
                                      RibbonAppMenuDocsTitle.IsDefault &&
                                      RibbonAppMenuDocsEntry.IsDefault &&
                                      RibbonGeneral.IsDefault &&
                                      RibbonGroupArea.IsDefault &&
                                      RibbonGroupButtonText.IsDefault &&
                                      RibbonGroupCheckBoxText.IsDefault &&
                                      RibbonGroupNormalBorder.IsDefault &&
                                      RibbonGroupNormalTitle.IsDefault &&
                                      RibbonGroupCollapsedBorder.IsDefault &&
                                      RibbonGroupCollapsedBack.IsDefault &&
                                      RibbonGroupCollapsedFrameBorder.IsDefault &&
                                      RibbonGroupCollapsedFrameBack.IsDefault &&
                                      RibbonGroupCollapsedText.IsDefault &&
                                      RibbonGroupLabelText.IsDefault &&
                                      RibbonGroupRadioButtonText.IsDefault &&
                                      RibbonQATFullbar.IsDefault &&
                                      RibbonQATMinibar.IsDefault &&
                                      RibbonTab.IsDefault &&
                                      RibbonGalleryBack.IsDefault &&
                                      RibbonGalleryBorder.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        RibbonAppButton.PopulateFromBase();
        RibbonFileAppTab.PopulateFromBase();
        RibbonAppMenuOuter.PopulateFromBase(PaletteState.Normal);
        RibbonAppMenuInner.PopulateFromBase(PaletteState.Normal);
        RibbonAppMenuDocs.PopulateFromBase(PaletteState.Normal);
        RibbonAppMenuDocsTitle.PopulateFromBase(PaletteState.Normal);
        RibbonAppMenuDocsEntry.PopulateFromBase(PaletteState.Normal);
        RibbonGeneral.PopulateFromBase();
        RibbonGroupArea.PopulateFromBase();
        RibbonGroupButtonText.PopulateFromBase();
        RibbonGroupCheckBoxText.PopulateFromBase();
        RibbonGroupNormalBorder.PopulateFromBase();
        RibbonGroupNormalTitle.PopulateFromBase();
        RibbonGroupCollapsedBack.PopulateFromBase();
        RibbonGroupCollapsedBorder.PopulateFromBase();
        RibbonGroupCollapsedFrameBorder.PopulateFromBase();
        RibbonGroupCollapsedFrameBack.PopulateFromBase();
        RibbonGroupCollapsedText.PopulateFromBase();
        RibbonGroupRadioButtonText.PopulateFromBase();
        RibbonGroupLabelText.PopulateFromBase();
        RibbonQATFullbar.PopulateFromBase(PaletteState.Normal);
        RibbonQATMinibar.PopulateFromBase();
        RibbonQATOverflow.PopulateFromBase(PaletteState.Normal);
        RibbonTab.PopulateFromBase();
        RibbonGalleryBack.PopulateFromBase(PaletteState.Normal);
        RibbonGalleryBorder.PopulateFromBase(PaletteState.Normal);
    }
    #endregion

    #region RibbonAppButton
    /// <summary>
    /// Get access to the application button tab settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon application button specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonAppButton RibbonAppButton { get; }

    private bool ShouldSerializeRibbonAppButton() => !RibbonAppButton.IsDefault;

    #endregion

    #region RibbonAppMenuOuter
    /// <summary>
    /// Gets access to the application button menu outer palette details.
    /// </summary>
    [KryptonPersist]
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
    [KryptonPersist]
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
    [KryptonPersist]
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
    [KryptonPersist]
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
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining application button menu recent documents entry.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonAppMenuDocsEntry => _ribbonAppMenuDocsEntry;

    private bool ShouldSerializeRibbonAppMenuDocsEntry() => !_ribbonAppMenuDocsEntry.IsDefault;

    #endregion

    #region RibbonGeneral
    /// <summary>
    /// Get access to the general ribbon settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon general settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonGeneral RibbonGeneral { get; }

    private bool ShouldSerializeRibbonGeneral() => !RibbonGeneral.IsDefault;
    #endregion

    #region RibbonFileAppTab
    /// <summary>
    /// Get access to the "File App Tab" ribbon settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon File App Tab settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonFileAppTab RibbonFileAppTab { get; }

    private bool ShouldSerializeRibbonFileAppTab() => !RibbonFileAppTab.IsDefault;
    #endregion
        
    #region RibbonGroupArea
    /// <summary>
    /// Get access to the ribbon group area settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon group area specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonGroupArea RibbonGroupArea { get; }

    private bool ShouldSerializeRibbonGroupArea() => !RibbonGroupArea.IsDefault;

    #endregion

    #region RibbonGroupButtonText
    /// <summary>
    /// Get access to the ribbon group button text settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon group button text specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonGroupButtonText RibbonGroupButtonText { get; }

    private bool ShouldSerializeRibbonGroupButtonText() => !RibbonGroupButtonText.IsDefault;

    #endregion

    #region RibbonGroupCheckBoxText
    /// <summary>
    /// Get access to the ribbon group check box text settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon group check box text specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonGroupCheckBoxText RibbonGroupCheckBoxText { get; }

    private bool ShouldSerializeRibbonGroupCheckBoxText() => !RibbonGroupCheckBoxText.IsDefault;

    #endregion

    #region RibbonGroupNormalBorder
    /// <summary>
    /// Get access to the ribbon group normal border settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon group normal border specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonGroupNormalBorder RibbonGroupNormalBorder { get; }

    private bool ShouldSerializeRibbonGroupNormalBorder() => !RibbonGroupNormalBorder.IsDefault;

    #endregion

    #region RibbonGroupNormalTitle
    /// <summary>
    /// Get access to the ribbon group normal title settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon group normal title specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonGroupNormalTitle RibbonGroupNormalTitle { get; }

    private bool ShouldSerializeRibbonGroupNormalTitle() => !RibbonGroupNormalTitle.IsDefault;

    #endregion

    #region RibbonGroupCollapsedBorder
    /// <summary>
    /// Get access to the ribbon group collapsed border settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon group collapsed border specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonGroupCollapsedBorder RibbonGroupCollapsedBorder { get; }

    private bool ShouldSerializeRibbonGroupCollapsedBorder() => !RibbonGroupCollapsedBorder.IsDefault;

    #endregion

    #region RibbonGroupCollapsedBack
    /// <summary>
    /// Get access to the ribbon group collapsed background settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon group collapsed background specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonGroupCollapsedBack RibbonGroupCollapsedBack { get; }

    private bool ShouldSerializeRibbonGroupCollapsedBack() => !RibbonGroupCollapsedBack.IsDefault;

    #endregion

    #region RibbonGroupCollapsedFrameBorder
    /// <summary>
    /// Get access to the ribbon group collapsed frame border settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon group collapsed frame border specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonGroupCollapsedFrameBorder RibbonGroupCollapsedFrameBorder { get; }

    private bool ShouldSerializeRibbonGroupCollapsedFrameBorder() => !RibbonGroupCollapsedFrameBorder.IsDefault;

    #endregion

    #region RibbonGroupCollapsedFrameBack
    /// <summary>
    /// Get access to the ribbon group collapsed frame background settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon group collapsed frame background specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonGroupCollapsedFrameBack RibbonGroupCollapsedFrameBack { get; }

    private bool ShouldSerializeRibbonGroupCollapsedFrameBack() => !RibbonGroupCollapsedFrameBack.IsDefault;

    #endregion

    #region RibbonGroupCollapsedText
    /// <summary>
    /// Get access to the ribbon group collapsed text settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon group collapsed text specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonGroupCollapsedText RibbonGroupCollapsedText { get; }

    private bool ShouldSerializeRibbonGroupCollapsedText() => !RibbonGroupCollapsedText.IsDefault;

    #endregion

    #region RibbonGroupLabelText
    /// <summary>
    /// Get access to the ribbon group label text settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon group label text specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonGroupLabelText RibbonGroupLabelText { get; }

    private bool ShouldSerializeRibbonGroupLabelText() => !RibbonGroupLabelText.IsDefault;

    #endregion

    #region RibbonGroupRadioButtonText
    /// <summary>
    /// Get access to the ribbon radio button box text settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon group radio button text specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonGroupRadioButtonText RibbonGroupRadioButtonText { get; }

    private bool ShouldSerializeRibbonGroupRadioButtonText() => !RibbonGroupRadioButtonText.IsDefault;

    #endregion

    #region RibbonQATFullbar
    /// <summary>
    /// Get access to the quick access toolbar full settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon quick access toolbar full settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonBack RibbonQATFullbar { get; }

    private bool ShouldSerializeRibbonQATFullbar() => !RibbonQATFullbar.IsDefault;

    #endregion

    #region RibbonQATMinibar
    /// <summary>
    /// Get access to the quick access toolbar mini settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon quick access toolbar mini settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonQATMinibar RibbonQATMinibar { get; }

    private bool ShouldSerializeRibbonQATMinibar() => !RibbonQATMinibar.IsDefault;

    #endregion

    #region RibbonQATOverflow
    /// <summary>
    /// Get access to the quick access toolbar overflow settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon quick access toolbar overflow settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonBack RibbonQATOverflow { get; }

    private bool ShouldSerializeRibbonQATOverflow() => !RibbonQATOverflow.IsDefault;

    #endregion

    #region RibbonTab
    /// <summary>
    /// Get access to the ribbon tab settings.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Ribbon tab specific settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteRibbonTab RibbonTab { get; }

    private bool ShouldSerializeRibbonTab() => !RibbonTab.IsDefault;

    #endregion

    #region RibbonGalleryBack
    /// <summary>
    /// Gets access to the ribbon gallery background palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon gallery background appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonGalleryBack => _ribbonGalleryBack;

    private bool ShouldSerializeRibbonGalleryBack() => !_ribbonGalleryBack.IsDefault;

    #endregion

    #region RibbonGalleryBorder
    /// <summary>
    /// Gets access to the ribbon gallery border palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon gallery border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonBack RibbonGalleryBorder => _ribbonGalleryBorder;

    private bool ShouldSerializeRibbonGalleryBorder() => !_ribbonGalleryBorder.IsDefault;

    #endregion
}