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
/// Storage for label palette settings.
/// </summary>
public class KryptonPaletteLabels : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteLabels class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteLabels([DisallowNull] PaletteRedirect redirector,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirector != null);

        // Create the button style specific and common palettes
        LabelCommon = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelNormalControl, needPaint);
        LabelNormalControl = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelNormalControl, needPaint);
        LabelBoldControl = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelBoldControl, needPaint);
        LabelItalicControl = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelItalicControl, needPaint);
        LabelTitleControl = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelTitleControl, needPaint);
        LabelNormalPanel = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelNormalPanel, needPaint);
        LabelBoldPanel = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelBoldPanel, needPaint);
        LabelItalicPanel = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelItalicPanel, needPaint);
        LabelTitlePanel = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelTitlePanel, needPaint);
        LabelCaptionPanel = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelGroupBoxCaption, needPaint);
        LabelToolTip = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelToolTip, needPaint);
        LabelSuperTip = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelSuperTip, needPaint);
        LabelKeyTip = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelKeyTip, needPaint);
        LabelCustom1 = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelCustom1, needPaint);
        LabelCustom2 = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelCustom2, needPaint);
        LabelCustom3 = new KryptonPaletteLabel(redirector!, PaletteContentStyle.LabelCustom3, needPaint);

        // Create redirectors for inheriting from style specific to style common
        var redirectCommon =
            new PaletteRedirectContent(redirector!, LabelCommon.StateDisabled, LabelCommon.StateNormal);

        // Inform the button style to use the new redirector
        LabelNormalControl.SetRedirector(redirectCommon);
        LabelBoldControl.SetRedirector(redirectCommon);
        LabelItalicControl.SetRedirector(redirectCommon);
        LabelTitleControl.SetRedirector(redirectCommon);
        LabelNormalPanel.SetRedirector(redirectCommon);
        LabelBoldPanel.SetRedirector(redirectCommon);
        LabelItalicPanel.SetRedirector(redirectCommon);
        LabelTitlePanel.SetRedirector(redirectCommon);
        LabelCaptionPanel.SetRedirector(redirectCommon);
        LabelToolTip.SetRedirector(redirectCommon);
        LabelSuperTip.SetRedirector(redirectCommon);
        LabelKeyTip.SetRedirector(redirectCommon);
        LabelCustom1.SetRedirector(redirectCommon);
        LabelCustom2.SetRedirector(redirectCommon);
        LabelCustom3.SetRedirector(redirectCommon);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => LabelCommon.IsDefault &&
                                      LabelNormalControl.IsDefault &&
                                      LabelBoldControl.IsDefault &&
                                      LabelItalicControl.IsDefault &&
                                      LabelTitleControl.IsDefault &&
                                      LabelNormalPanel.IsDefault &&
                                      LabelBoldPanel.IsDefault &&
                                      LabelItalicPanel.IsDefault &&
                                      LabelTitlePanel.IsDefault &&
                                      LabelCaptionPanel.IsDefault &&
                                      LabelToolTip.IsDefault &&
                                      LabelSuperTip.IsDefault &&
                                      LabelKeyTip.IsDefault &&
                                      LabelCustom1.IsDefault &&
                                      LabelCustom2.IsDefault &&
                                      LabelCustom3.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="common">Reference to common settings.</param>
    public void PopulateFromBase(KryptonPaletteCommon common)
    {
        // Populate only the designated styles
        common.StateCommon.ContentStyle = PaletteContentStyle.LabelNormalControl;
        LabelNormalControl.PopulateFromBase();
        common.StateCommon.ContentStyle = PaletteContentStyle.LabelBoldControl;
        LabelNormalControl.PopulateFromBase();
        common.StateCommon.ContentStyle = PaletteContentStyle.LabelItalicControl;
        LabelNormalControl.PopulateFromBase();
        common.StateCommon.ContentStyle = PaletteContentStyle.LabelTitleControl;
        LabelTitleControl.PopulateFromBase();
        common.StateCommon.ContentStyle = PaletteContentStyle.LabelNormalPanel;
        LabelNormalPanel.PopulateFromBase();
        common.StateCommon.ContentStyle = PaletteContentStyle.LabelBoldPanel;
        LabelNormalPanel.PopulateFromBase();
        common.StateCommon.ContentStyle = PaletteContentStyle.LabelItalicPanel;
        LabelNormalPanel.PopulateFromBase();
        common.StateCommon.ContentStyle = PaletteContentStyle.LabelTitlePanel;
        LabelTitlePanel.PopulateFromBase();
        common.StateCommon.ContentStyle = PaletteContentStyle.LabelGroupBoxCaption;
        LabelCaptionPanel.PopulateFromBase();
        common.StateCommon.ContentStyle = PaletteContentStyle.LabelToolTip;
        LabelToolTip.PopulateFromBase();
        common.StateCommon.ContentStyle = PaletteContentStyle.LabelSuperTip;
        LabelSuperTip.PopulateFromBase();
        common.StateCommon.ContentStyle = PaletteContentStyle.LabelKeyTip;
        LabelKeyTip.PopulateFromBase();
    }
    #endregion

    #region LabelCommon
    /// <summary>
    /// Gets access to the common label appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelCommon { get; }

    private bool ShouldSerializeLabelCommon() => !LabelCommon.IsDefault;

    #endregion

    #region LabelNormalControl
    /// <summary>
    /// Gets access to the normal label used for control style backgrounds.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal label appearance for use on control style backgrounds.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelNormalControl { get; }

    private bool ShouldSerializeLabelNormalControl() => !LabelNormalControl.IsDefault;

    #endregion

    #region LabelBoldControl
    /// <summary>
    /// Gets access to the bold label used for control style backgrounds.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining bold label appearance for use on control style backgrounds.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelBoldControl { get; }

    private bool ShouldSerializeLabelBoldControl() => !LabelBoldControl.IsDefault;

    #endregion

    #region LabelItalicControl
    /// <summary>
    /// Gets access to the italic label used for control style backgrounds.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining italic label appearance for use on control style backgrounds.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelItalicControl { get; }

    private bool ShouldSerializeLabelItalicControl() => !LabelItalicControl.IsDefault;

    #endregion

    #region LabelTitleControl
    /// <summary>
    /// Gets access to the title label used for control style backgrounds.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining title label appearance for use on control style backgrounds.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelTitleControl { get; }

    private bool ShouldSerializeLabelTitleControl() => !LabelTitleControl.IsDefault;

    #endregion

    #region LabelNormalPanel
    /// <summary>
    /// Gets access to the normal label used for panel style backgrounds.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal label appearance for use on panel style backgrounds.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelNormalPanel { get; }

    private bool ShouldSerializeLabelNormalPanel() => !LabelNormalPanel.IsDefault;

    #endregion

    #region LabelBoldPanel
    /// <summary>
    /// Gets access to the bold label used for panel style backgrounds.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining bold label appearance for use on panel style backgrounds.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelBoldPanel { get; }

    private bool ShouldSerializeLabelBoldPanel() => !LabelBoldPanel.IsDefault;

    #endregion

    #region LabelItalicPanel
    /// <summary>
    /// Gets access to the italic label used for panel style backgrounds.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining italic label appearance for use on panel style backgrounds.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelItalicPanel { get; }

    private bool ShouldSerializeLabelItalicPanel() => !LabelItalicPanel.IsDefault;

    #endregion

    #region LabelTitlePanel
    /// <summary>
    /// Gets access to the title label used for panel style backgrounds.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining title label appearance for use on panel style backgrounds.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelTitlePanel { get; }

    private bool ShouldSerializeLabelTitlePanel() => !LabelTitlePanel.IsDefault;

    #endregion

    #region LabelCaptionPanel
    /// <summary>
    /// Gets access to the caption label used for group box style backgrounds.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining caption label appearance for use on group box style backgrounds.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelCaptionPanel { get; }

    private bool ShouldSerializeLabelCaptionPanel() => !LabelCaptionPanel.IsDefault;

    #endregion

    #region LabelToolTip
    /// <summary>
    /// Gets access to the tooltip label appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the tooltip label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelToolTip { get; }

    private bool ShouldSerializeLabelToolTip() => !LabelToolTip.IsDefault;

    #endregion

    #region LabelSuperTip
    /// <summary>
    /// Gets access to the super tooltip label appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the super tooltip label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelSuperTip { get; }

    private bool ShouldSerializeLabelSuperTip() => !LabelSuperTip.IsDefault;

    #endregion

    #region LabelKeyTip
    /// <summary>
    /// Gets access to the keytip label appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the keytip label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelKeyTip { get; }

    private bool ShouldSerializeLabelKeyTip() => !LabelKeyTip.IsDefault;

    #endregion

    #region LabelCustom1
    /// <summary>
    /// Gets access to the first custom label appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the first custom label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelCustom1 { get; }

    private bool ShouldSerializeLabelCustom1() => !LabelCustom1.IsDefault;

    #endregion

    #region LabelCustom2
    /// <summary>
    /// Gets access to the second custom label appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the first second label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelCustom2 { get; }

    private bool ShouldSerializeLabelCustom2() => !LabelCustom2.IsDefault;

    #endregion

    #region LabelCustom3
    /// <summary>
    /// Gets access to the third custom label appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the third second label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteLabel LabelCustom3 { get; }

    private bool ShouldSerializeLabelCustom3() => !LabelCustom3.IsDefault;

    #endregion
}