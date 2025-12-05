#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Access 'Global' Krypton string settings.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonGlobalToolkitStrings : GlobalId
{
    #region Static Strings

    /// <summary>Gets the color strings.</summary>
    /// <value>The color strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static GlobalColorStrings GlobalColorStrings { get; } = new GlobalColorStrings();

    /// <summary>Gets the spec style strings.</summary>
    /// <value>The spec style strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static ButtonStyleStrings ButtonStyles { get; } = new ButtonStyleStrings();

    /// <summary>Gets the custom toolkit strings.</summary>
    /// <value>The custom toolkit strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static CustomToolkitStrings CustomToolkitStrings { get; } = new CustomToolkitStrings();

    /// <summary>Gets the general ribbon strings.</summary>
    /// <value>The general ribbon strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static GeneralRibbonStrings GeneralRibbonStrings { get; } = new GeneralRibbonStrings();

    /// <summary>Gets the strings.</summary>
    /// <value>The strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static GeneralToolkitStrings GeneralToolkitStrings
    { get; } = new GeneralToolkitStrings();

    /// <summary>Gets the grid view style strings.</summary>
    /// <value>The grid view style strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static DataGridViewStyleStrings DataGridViewStyles { get; } = new DataGridViewStyleStrings();

    /// <summary>Gets the style strings.</summary>
    /// <value>The style strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static GridStyleStrings GridStyles { get; } = new GridStyleStrings();

    /// <summary>Gets the group collapsed target strings.</summary>
    /// <value>The group collapsed target strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static HeaderGroupCollapsedTargetStrings GroupCollapsedTargetStrings { get; } =
        new HeaderGroupCollapsedTargetStrings();

    /// <summary>Gets the header styles.</summary>
    /// <value>The header styles.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static HeaderStyleStrings HeaderStyles { get; } = new HeaderStyleStrings();

    /// <summary>Gets the input control styles.</summary>
    /// <value>The input control styles.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static InputControlStyleStrings InputControlStyles { get; } = new InputControlStyleStrings();

    /// <summary>Gets the tool bar strings.</summary>
    /// <value>The tool bar strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static IntegratedToolBarStrings IntegratedToolBarStrings { get; } = new IntegratedToolBarStrings();

    /// <summary>Gets the link behavior strings.</summary>
    /// <value>The link behavior strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static KryptonLinkBehaviorStrings KryptonLinkBehaviorStrings { get; } = new KryptonLinkBehaviorStrings();

    /// <summary>Gets the krypton label style strings.</summary>
    /// <value>The krypton label style strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static LabelStyleStrings KryptonLabelStyleStrings { get; } = new LabelStyleStrings();

    /// <summary>Gets the back style strings.</summary>
    /// <value>The back style strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static PaletteBackStyleStrings PaletteBackStyleStrings { get; } = new PaletteBackStyleStrings();

    /// <summary>Gets the border style strings.</summary>
    /// <value>The border style strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static PaletteBorderStyleStrings PaletteBorderStyleStrings { get; } = new PaletteBorderStyleStrings();

    /// <summary>Gets the button orientation strings.</summary>
    /// <value>The button orientation strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static PaletteButtonOrientationStrings PaletteButtonOrientationStrings { get; } =
        new PaletteButtonOrientationStrings();

    /// <summary>Gets the button spec styles.</summary>
    /// <value>The button spec styles.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static PaletteButtonSpecStyleStrings PaletteButtonSpecStyleStrings { get; } = new PaletteButtonSpecStyleStrings();

    /// <summary>Gets the button style strings.</summary>
    /// <value>The button style strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static PaletteButtonStyleStrings PaletteButtonStyleStrings { get; } = new PaletteButtonStyleStrings();

    /// <summary>Gets the content style strings.</summary>
    /// <value>The content style strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static PaletteContentStyleStrings ContentStyleStrings { get; } = new PaletteContentStyleStrings();

    /// <summary>Gets the image effect strings.</summary>
    /// <value>The image effect strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static PaletteImageEffectStrings ImageEffectStrings { get; } = new PaletteImageEffectStrings();

    /// <summary>Gets the image style strings.</summary>
    /// <value>The image style strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static PaletteImageStyleStrings ImageStyleStrings { get; } = new PaletteImageStyleStrings();

    /// <summary>Gets the mode strings.</summary>
    /// <value>The mode strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static PaletteModeStrings ModeStrings { get; } = new PaletteModeStrings();

    /// <summary>Gets the text trim strings.</summary>
    /// <value>The text trim strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static PaletteTextTrimStrings TextTrimStrings { get; } = new PaletteTextTrimStrings();

    /// <summary>Gets the placement mode strings.</summary>
    /// <value>The placement mode strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static PlacementModeStrings PlacementModeStrings { get; } = new PlacementModeStrings();

    /// <summary>Gets the separator styles.</summary>
    /// <value>The separator styles.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static SeparatorStyleStrings SeparatorStyles { get; } = new SeparatorStyleStrings();

    /// <summary>Gets the tab border styles.</summary>
    /// <value>The tab border styles.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static TabBorderStyleStrings TabBorderStyles { get; } = new TabBorderStyleStrings();

    /// <summary>Gets the tab styles.</summary>
    /// <value>The tab styles.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static TabStyleStrings TabStyles { get; } = new TabStyleStrings();

    /// <summary>Gets the toast notification icon.</summary>
    /// <value>The toast notification icon.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static ToastNotificationIconStrings ToastNotificationIcon { get; } = new ToastNotificationIconStrings();

    /// <summary>Gets the basic application information strings.</summary>
    /// <value>The basic application information strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static KryptonAboutBoxBasicApplicationInformationStrings KryptonAboutBoxBasicApplicationInformationStrings { get; } = new KryptonAboutBoxBasicApplicationInformationStrings();

    /// <summary>Gets the about box strings.</summary>
    /// <value>The about box strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static KryptonAboutBoxStrings KryptonAboutBoxStrings { get; } = new KryptonAboutBoxStrings();

    /// <summary>Gets the exception dialog strings.</summary>
    /// <value>The exception dialog strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static KryptonExceptionDialogStrings KryptonExceptionDialogStrings { get; } = new KryptonExceptionDialogStrings();

    /// <summary>Gets the miscellaneous theme strings.</summary>
    /// <value>The miscellaneous theme strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static KryptonMiscellaneousThemeStrings KryptonMiscellaneousThemeStrings { get; } =
        new KryptonMiscellaneousThemeStrings();

    /// <summary>Gets the scroll bar strings.</summary>
    /// <value>The scroll bar strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static KryptonScrollBarStrings KryptonScrollBarStrings { get; } = new KryptonScrollBarStrings();

    /// <summary>Gets the toast notification strings.</summary>
    /// <value>The toast notification strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static KryptonToastNotificationStrings KryptonToastNotificationStrings { get; } =
        new KryptonToastNotificationStrings();

    /// <summary>Gets the krypton splash screen strings.</summary>
    /// <value>The krypton splash screen strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static SplashScreenStrings KryptonSplashScreenStrings { get; } = new SplashScreenStrings();

    /// <summary>Gets the krypton miscellaneous strings.</summary>
    /// <value>The krypton miscellaneous strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static KryptonMiscellaneousStrings KryptonMiscellaneousStrings { get; } = new KryptonMiscellaneousStrings();

    /// <summary>Gets the krypton message box strings.</summary>
    /// <value>The krypton message box strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static MessageBoxStrings KryptonMessageBoxStrings { get; } = new MessageBoxStrings();
    
    /// <summary>Gets the krypton search box strings.</summary>
    /// <value>The krypton search box strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static KryptonSearchBoxStrings KryptonSearchBoxStrings { get; } = new KryptonSearchBoxStrings();

    /// <summary>Gets the win32 system menu strings.</summary>
    /// <value>The win32 system menu strings.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static SystemMenuStrings Win32SystemMenuStrings { get; } = new SystemMenuStrings();

    #endregion

    #region Public

    /// <summary>Gets the palette back style strings.</summary>
    /// <value>The palette back style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of palette back style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public PaletteBackStyleStrings BackStyleStrings => PaletteBackStyleStrings;
    private bool ShouldSerializeBackStyleStrings() => !PaletteBackStyleStrings.IsDefault;
    private void ResetBackStyleStrings() => PaletteBackStyleStrings.Reset();

    /// <summary>Gets the button spec style strings.</summary>
    /// <value>The button spec style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of button spec style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public ButtonStyleStrings ButtonStyleStrings => ButtonStyles;
    private bool ShouldSerializeButtonStyleStrings() => !ButtonStyles.IsDefault;
    private void ResetButtonStyleStrings() => ButtonStyles.Reset();

    /// <summary>Gets the palette border style strings.</summary>
    /// <value>The palette border style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of palette border style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public PaletteBorderStyleStrings BorderStyleStrings => PaletteBorderStyleStrings;
    private bool ShouldSerializeBorderStyleStrings() => !PaletteBorderStyleStrings.IsDefault;
    private void ResetBorderStyleStrings() => PaletteBorderStyleStrings.Reset();

    /// <summary>Gets the palette button orientation strings.</summary>
    /// <value>The palette button orientation strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of palette button orientation strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public PaletteButtonOrientationStrings ButtonOrientationStrings => PaletteButtonOrientationStrings;
    private bool ShouldSerializeButtonOrientationStrings() => !PaletteButtonOrientationStrings.IsDefault;
    private void ResetButtonOrientationStrings() => PaletteButtonOrientationStrings.Reset();

    /// <summary>Gets the palette button spec style strings.</summary>
    /// <value>The palette button spec style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of palette button spec style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public PaletteButtonSpecStyleStrings ButtonSpecStyleStrings => PaletteButtonSpecStyleStrings;
    private bool ShouldSerializeButtonSpecStyleStrings() => !PaletteButtonSpecStyleStrings.IsDefault;
    private void ResetButtonSpecStyleStrings() => PaletteButtonSpecStyleStrings.Reset();

    /// <summary>Gets the palette button style strings.</summary>
    /// <value>The palette button style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of palette button style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public PaletteButtonStyleStrings PaletteButtonStyles => PaletteButtonStyleStrings;
    private bool ShouldSerializePaletteButtonStyles() => !PaletteButtonStyleStrings.IsDefault;
    private void ResetPaletteButtonStyles() => PaletteButtonStyleStrings.Reset();

    /// <summary>Gets the global color strings.</summary>
    /// <value>The global color strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of color strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public GlobalColorStrings ColorStrings => GlobalColorStrings;
    private bool ShouldSerializeColorStrings() => !GlobalColorStrings.IsDefault;
    private void ResetColorStrings() => GlobalColorStrings.Reset();

    /// <summary>Gets the custom toolkit strings.</summary>
    /// <value>The custom toolkit strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of custom toolkit strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public CustomToolkitStrings CustomStrings => CustomToolkitStrings;
    private bool ShouldSerializeCustomStrings() => !CustomToolkitStrings.IsDefault;
    private void ResetCustomStrings() => CustomToolkitStrings.ResetValues();

    /// <summary>Gets the general ribbon strings.</summary>
    /// <value>The general ribbon strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of general ribbon strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public GeneralRibbonStrings RibbonStrings => GeneralRibbonStrings;
    private bool ShouldSerializeGeneralRibbonStrings() => !GeneralRibbonStrings.IsDefault;
    private void ResetGeneralRibbonStrings() => GeneralRibbonStrings.Reset();

    /// <summary>Gets the general toolkit strings.</summary>
    /// <value>The general toolkit strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of general toolkit strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public GeneralToolkitStrings GeneralStrings => GeneralToolkitStrings;
    private bool ShouldSerializeGeneralStrings() => !GeneralToolkitStrings.IsDefault;
    private void ResetGeneralStrings() => GeneralToolkitStrings.Reset();

    /// <summary>Gets the integrated toolbar button strings.</summary>
    /// <value>The integrated toolbar button strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of integrated toolbar button strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public IntegratedToolBarStrings ToolBarStrings => IntegratedToolBarStrings;
    private bool ShouldSerializeToolBarStrings() => !IntegratedToolBarStrings.IsDefault;
    private void ResetToolBarStrings() => IntegratedToolBarStrings.Reset();

    /// <summary>Gets the link behavior style strings.</summary>
    /// <value>The link behavior style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of link behavior style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonLinkBehaviorStrings LinkBehaviorStrings => KryptonLinkBehaviorStrings;
    private bool ShouldSerializeLinkBehaviorStrings() => !KryptonLinkBehaviorStrings.IsDefault;
    private void ResetLinkBehaviorStrings() => KryptonLinkBehaviorStrings.Reset();

    /// <summary>Gets the link style strings.</summary>
    /// <value>The link style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of link style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public LabelStyleStrings LabelStyleStrings => KryptonLabelStyleStrings;
    private bool ShouldSerializeLabelStyleStrings() => !KryptonLabelStyleStrings.IsDefault;
    private void ResetLabelStyleStrings() => KryptonLabelStyleStrings.Reset();

    /// <summary>Gets the palette content style strings.</summary>
    /// <value>The palette content style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of palette mode strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public PaletteContentStyleStrings PaletteContentStyleStrings => ContentStyleStrings;
    private bool ShouldSerializePaletteContentStyleStrings() => !ContentStyleStrings.IsDefault;
    private void ResetPaletteContentStyleStrings() => ContentStyleStrings.Reset();

    /// <summary>Gets the image effect strings.</summary>
    /// <value>The image effect strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of image effect strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public PaletteImageEffectStrings PaletteImageEffectStrings => ImageEffectStrings;
    private bool ShouldSerializePaletteImageEffectStrings() => !ImageEffectStrings.IsDefault;
    private void ResetPaletteImageEffectStrings() => ImageEffectStrings.Reset();

    /// <summary>Gets the image style strings.</summary>
    /// <value>The image style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of image style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public PaletteImageStyleStrings PaletteImageStyleStrings => ImageStyleStrings;
    private bool ShouldSerializePaletteImageStyleStrings() => !ImageStyleStrings.IsDefault;
    private void ResetPaletteImageStyleStrings() => ImageStyleStrings.Reset();

    /// <summary>Gets the palette mode strings.</summary>
    /// <value>The palette mode strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of palette mode strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public PaletteModeStrings PaletteModeStrings => ModeStrings;
    private bool ShouldSerializePaletteModeStrings() => !ModeStrings.IsDefault;
    private void ResetPaletteModeStrings() => ModeStrings.Reset();

    /// <summary>Gets the palette text trim strings.</summary>
    /// <value>The palette text trim strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of palette text trim strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public PaletteTextTrimStrings PaletteTextTrimStrings => TextTrimStrings;
    private bool ShouldSerializePaletteTextTrimStrings() => !TextTrimStrings.IsDefault;
    private void ResetPaletteTextTrimStrings() => TextTrimStrings.Reset();

    /// <summary>Gets the placement mode strings.</summary>
    /// <value>The placement mode strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of placement mode strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public PlacementModeStrings PlacementMode => PlacementModeStrings;
    private bool ShouldSerializePlacementModeStrings() => !PlacementModeStrings.IsDefault;
    private void ResetPlacementModeStrings() => PlacementModeStrings.Reset();

    /// <summary>Gets the separator style strings.</summary>
    /// <value>The separator style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of separator style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public SeparatorStyleStrings SeparatorStyleStrings => SeparatorStyles;
    private bool ShouldSerializeSeparatorStyleStrings() => !SeparatorStyles.IsDefault;
    private void ResetSeparatorStyleStrings() => SeparatorStyles.Reset();

    /// <summary>Gets the tab border style strings.</summary>
    /// <value>The tab border style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of tab border style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public TabBorderStyleStrings TabBorderStyleStrings => TabBorderStyles;
    private bool ShouldSerializeTabBorderStyleStrings() => !TabBorderStyles.IsDefault;
    private void ResetTabBorderStyleStrings() => TabBorderStyles.Reset();

    /// <summary>Gets the tab style strings.</summary>
    /// <value>The tab style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of tab style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public TabStyleStrings TabStyleStrings => TabStyles;
    private bool ShouldSerializeTabStyleStrings() => !TabStyles.IsDefault;
    private void ResetTabStyleStrings() => TabStyles.Reset();

    /// <summary>Gets the toast notification icon strings.</summary>
    /// <value>The toast notification icon strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of toast notification icon strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public ToastNotificationIconStrings ToastNotificationIconStrings => ToastNotificationIcon;

    private bool ShouldSerializeToastNotificationIconStrings() => !ToastNotificationIcon.IsDefault;

    /// <summary>Resets the toast notification icon strings.</summary>
    public void ResetToastNotificationIconStrings() => ToastNotificationIcon.Reset();

    /// <summary>Gets the krypton about box basic application information strings.</summary>
    /// <value>The krypton about box basic application information strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of about box basic application information strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonAboutBoxBasicApplicationInformationStrings AboutBoxBasicStrings => KryptonAboutBoxBasicApplicationInformationStrings;
    private bool ShouldSerializeAboutBoxBasicStrings() => !KryptonAboutBoxBasicApplicationInformationStrings.IsDefault;
    private void ResetAboutBoxBasicStrings() => KryptonAboutBoxBasicApplicationInformationStrings.Reset();

    /// <summary>Gets the krypton about box strings.</summary>
    /// <value>The krypton about box strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of about box strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonAboutBoxStrings AboutBoxStrings => KryptonAboutBoxStrings;
    private bool ShouldSerializeAboutBoxStrings() => !KryptonAboutBoxStrings.IsDefault;
    private void ResetAboutBoxStrings() => KryptonAboutBoxStrings.Reset();

    /// <summary>Gets the krypton exception dialog strings.</summary>
    /// <value>The krypton exception dialog strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of exception dialog strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonExceptionDialogStrings ExceptionDialogStrings => KryptonExceptionDialogStrings;
    private bool ShouldSerializeExceptionDialogStrings() => !KryptonExceptionDialogStrings.IsDefault;
    private void ResetExceptionDialogStrings() => KryptonExceptionDialogStrings.Reset();

    /// <summary>Gets the krypton miscellaneous theme strings.</summary>
    /// <value>The krypton miscellaneous theme strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of miscellaneous theme strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonMiscellaneousThemeStrings MiscellaneousThemeStrings => KryptonMiscellaneousThemeStrings;
    private bool ShouldSerializeMiscellaneousThemeStrings() => !KryptonMiscellaneousThemeStrings.IsDefault;
    private void ResetMiscellaneousThemeStrings() => KryptonMiscellaneousThemeStrings.Reset();

    /// <summary>Gets the scrollbar strings.</summary>
    /// <value>The scrollbar strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of scrollbar strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonScrollBarStrings ScrollBarStrings => KryptonScrollBarStrings;
    private bool ShouldSerializeScrollBarStrings() => !KryptonScrollBarStrings.IsDefault;
    private void ResetScrollBarStrings() => KryptonScrollBarStrings.Reset();

    /// <summary>Gets the data grid view strings.</summary>
    /// <value>The data grid view strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of data grid view strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public DataGridViewStyleStrings GridViewStyleStrings => DataGridViewStyles;
    private bool ShouldSerializeGridViewStyleStrings() => !DataGridViewStyles.IsDefault;
    private void ResetGridViewStyleStrings() => DataGridViewStyles.Reset();

    /// <summary>Gets the grid style strings.</summary>
    /// <value>The grid style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of grid style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public GridStyleStrings GridStyleStrings => GridStyles;
    private bool ShouldSerializeGridStyleStrings() => !GridStyles.IsDefault;
    private void ResetGridStyleStrings() => GridStyles.Reset();

    /// <summary>Gets the header group collapsed target strings.</summary>
    /// <value>The header group collapsed target strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of header group collapsed target strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public HeaderGroupCollapsedTargetStrings HeaderGroupCollapsedTargetStrings => GroupCollapsedTargetStrings;
    private bool ShouldSerializeHeaderGroupCollapsedTargetStrings() => !GroupCollapsedTargetStrings.IsDefault;
    private void ResetHeaderGroupCollapsedTargetStrings() => GroupCollapsedTargetStrings.Reset();

    /// <summary>Gets the header style strings.</summary>
    /// <value>The header style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of header style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public HeaderStyleStrings HeaderStyleStrings => HeaderStyles;
    private bool ShouldSerializeHeaderStyleStrings() => !HeaderStyles.IsDefault;
    private void ResetHeaderStyleStrings() => HeaderStyles.Reset();

    /// <summary>Gets the input control style strings.</summary>
    /// <value>The input control style strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of input control style strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public InputControlStyleStrings InputControlStyleStrings => InputControlStyles;
    private bool ShouldSerializeInputControlStyleStrings() => !InputControlStyles.IsDefault;
    private void ResetInputControlStyleStrings() => InputControlStyles.Reset();

    /// <summary>Gets the krypton toast notification strings.</summary>
    /// <value>The krypton toast notification strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of toast notificaion strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonToastNotificationStrings ToastNotificationStrings => KryptonToastNotificationStrings;

    private bool ShouldSerializeToastNotificationStrings() => !KryptonToastNotificationStrings.IsDefault;

    /// <summary>Resets the krypton toast notification strings.</summary>
    public void ResetToastNotificationStrings() => KryptonToastNotificationStrings.Reset();

    /// <summary>Gets the krypton splash screen strings.</summary>
    /// <value>The krypton splash screen strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of splash screen strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public SplashScreenStrings SplashScreenStrings => KryptonSplashScreenStrings;

    private bool ShouldSerializeSplashScreenStringsStrings() => !KryptonSplashScreenStrings.IsDefault;

    /// <summary>Resets the krypton splash screen strings.</summary>
    public void ResetSplashScreenStringsStrings() => KryptonSplashScreenStrings.Reset();

    /// <summary>Gets the miscellaneous strings.</summary>
    /// <value>The miscellaneous strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of miscellaneous strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonMiscellaneousStrings MiscellaneousStrings => KryptonMiscellaneousStrings;

    private bool ShouldSerializeMiscellaneousStrings() => !KryptonMiscellaneousStrings.IsDefault;

    public void ResetMiscellaneousStrings() => KryptonMiscellaneousStrings.Reset();

    /// <summary>Gets the message box strings.</summary>
    /// <value>The message box strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of message box strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public MessageBoxStrings MessageBoxStrings => KryptonMessageBoxStrings;

    private bool ShouldSerializeMessageBoxStringsStrings() => !KryptonMessageBoxStrings.IsDefault;

    /// <summary>Resets the krypton message box strings.</summary>
    public void ResetMessageBoxStrings() => KryptonMessageBoxStrings.Reset();

    /// <summary>Gets the krypton search box strings.</summary>
    /// <value>The krypton search box strings.</value>
    [Category(@"Visuals")]
    [Description(@"Collection of search box strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonSearchBoxStrings SearchBoxStrings => KryptonSearchBoxStrings;

    private bool ShouldSerializeSearchBoxStrings() => !KryptonSearchBoxStrings.IsDefault;

    /// <summary>Resets the krypton search box strings.</summary>
    public void ResetSearchBoxStrings() => KryptonSearchBoxStrings.Reset();

    /// <summary>Gets the win32 system menu strings.</summary>
    [Category(@"Visuals")]
    [Description(@"Collection of win32 system menu strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public SystemMenuStrings SystemMenuStrings => Win32SystemMenuStrings;

    private bool ShouldSerializeSystemMenuStrings() => !Win32SystemMenuStrings.IsDefault;

    /// <summary>Resets the win32 system menu strings.</summary>
    public void ResetSystemMenuStrings() => Win32SystemMenuStrings.ResetValues();

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonGlobalToolkitStrings" /> class.</summary>
    public KryptonGlobalToolkitStrings()
    {
        //throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Implementation

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault => !(ShouldSerializeAboutBoxBasicStrings() || ShouldSerializeAboutBoxStrings() ||
                               ShouldSerializeExceptionDialogStrings() ||
                               ShouldSerializeBackStyleStrings() || ShouldSerializeBorderStyleStrings() ||
                               ShouldSerializeButtonOrientationStrings() ||
                               ShouldSerializeButtonSpecStyleStrings() || ShouldSerializeButtonStyleStrings() ||
                               ShouldSerializeColorStrings() || ShouldSerializeCustomStrings() ||
                               ShouldSerializeGeneralRibbonStrings() || ShouldSerializeGeneralStrings() ||
                               ShouldSerializeGridStyleStrings() || ShouldSerializeGridViewStyleStrings() ||
                               ShouldSerializeHeaderGroupCollapsedTargetStrings() ||
                               ShouldSerializeHeaderStyleStrings() || ShouldSerializeInputControlStyleStrings() ||
                               ShouldSerializeLabelStyleStrings() || ShouldSerializeLinkBehaviorStrings() ||
                               ShouldSerializeMiscellaneousThemeStrings() ||
                               ShouldSerializePaletteButtonStyles() ||
                               ShouldSerializePaletteContentStyleStrings() ||
                               ShouldSerializePaletteImageEffectStrings() ||
                               ShouldSerializePaletteImageStyleStrings() || ShouldSerializePaletteModeStrings() ||
                               ShouldSerializePaletteTextTrimStrings() || ShouldSerializePlacementModeStrings() ||
                               ShouldSerializeScrollBarStrings() || ShouldSerializeSeparatorStyleStrings() ||
                               ShouldSerializeToastNotificationIconStrings() ||
                               ShouldSerializeTabBorderStyleStrings() || ShouldSerializeTabStyleStrings() ||
                               ShouldSerializeToastNotificationStrings() || ShouldSerializeToolBarStrings() ||
                               ShouldSerializeSplashScreenStringsStrings() || ShouldSerializeMiscellaneousStrings() || 
                               ShouldSerializeMessageBoxStringsStrings() || ShouldSerializeSystemMenuStrings());

    /// <summary>Resets this instance.</summary>
    public void Reset()
    {
        ResetAboutBoxBasicStrings();
        ResetAboutBoxStrings();
        ResetExceptionDialogStrings();
        ResetBackStyleStrings();
        ResetBorderStyleStrings();
        ResetButtonOrientationStrings();
        ResetButtonSpecStyleStrings();
        ResetButtonStyleStrings();
        ResetColorStrings();
        ResetCustomStrings();
        ResetGeneralRibbonStrings();
        ResetGeneralStrings();
        ResetGridStyleStrings();
        ResetGridViewStyleStrings();
        ResetHeaderGroupCollapsedTargetStrings();
        ResetHeaderStyleStrings();
        ResetInputControlStyleStrings();
        ResetLabelStyleStrings();
        ResetLinkBehaviorStrings();
        ResetMiscellaneousThemeStrings();
        ResetPaletteButtonStyles();
        ResetPaletteContentStyleStrings();
        ResetPaletteImageEffectStrings();
        ResetPaletteImageStyleStrings();
        ResetPaletteModeStrings();
        ResetPaletteTextTrimStrings();
        ResetPlacementModeStrings();
        ResetScrollBarStrings();
        ResetSeparatorStyleStrings();
        ResetTabBorderStyleStrings();
        ResetTabStyleStrings();
        ResetToastNotificationIconStrings();
        ResetToastNotificationStrings();
        ResetToolBarStrings();
        ResetSplashScreenStringsStrings();
        ResetMiscellaneousStrings();
        ResetMessageBoxStrings();
        ResetSearchBoxStrings();
        ResetSystemMenuStrings();
    }

    #endregion
}