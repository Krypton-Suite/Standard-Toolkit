#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Access 'Global' Krypton string settings.</summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KryptonGlobalToolkitStrings : GlobalId
    {
        #region Public

        /// <summary>Gets the global color strings.</summary>
        /// <value>The global color strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of color strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public GlobalColorStrings GlobalColorStrings => ColorStrings;
        private bool ShouldSerializeColorStrings() => !ColorStrings.IsDefault;
        private void ResetColorStrings() => ColorStrings.Reset();

        /// <summary>Gets the button spec style strings.</summary>
        /// <value>The button spec style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of button spec style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public ButtonStyleStrings ButtonStyleStrings => ButtonStyles;
        private bool ShouldSerializeButtonSpecStyleStrings() => !ButtonStyles.IsDefault;
        private void ResetButtonSpecStyleStrings() => ButtonStyles.Reset();

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
        public IntegratedToolBarStrings IntegratedToolBarStrings => ToolBarStrings;
        private bool ShouldSerializeIntegratedToolBarStrings() => !ToolBarStrings.IsDefault;
        private void ResetIntegratedToolBarStrings() => ToolBarStrings.Reset();

        /// <summary>Gets the link behavior style strings.</summary>
        /// <value>The link behavior style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of link behavior style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public KryptonLinkBehaviorStrings KryptonLinkBehaviorStrings => LinkBehaviorStrings;
        private bool ShouldSerializeKryptonLinkBehaviorStrings() => !LinkBehaviorStrings.IsDefault;
        private void ResetKryptonLinkBehaviorStrings() => LinkBehaviorStrings.Reset();

        /// <summary>Gets the link style strings.</summary>
        /// <value>The link style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of link style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public LabelStyleStrings LabelStyleStrings => KryptonLabelStyleStrings;
        private bool ShouldSerializeLabelStyleStrings() => !LabelStyleStrings.IsDefault;
        private void ResetLabelStyleStrings() => LabelStyleStrings.Reset();

        /// <summary>Gets the palette back style strings.</summary>
        /// <value>The palette back style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of palette back style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public PaletteBackStyleStrings PaletteBackStyleStrings => BackStyleStrings;
        private bool ShouldSerializePaletteBackStyleStrings() => !BackStyleStrings.IsDefault;
        private void ResetPaletteBackStyleStrings() => BackStyleStrings.Reset();

        /// <summary>Gets the palette border style strings.</summary>
        /// <value>The palette border style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of palette border style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public PaletteBorderStyleStrings PaletteBorderStyleStrings => BorderStyleStrings;
        private bool ShouldSerializePaletteBorderStyleStrings() => !BorderStyleStrings.IsDefault;
        private void ResetPaletteBorderStyleStrings() => BorderStyleStrings.Reset();

        /// <summary>Gets the palette button orientation strings.</summary>
        /// <value>The palette button orientation strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of palette button orientation strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public PaletteButtonOrientationStrings PaletteButtonOrientationStrings => ButtonOrientationStrings;
        private bool ShouldSerializePaletteButtonOrientationStrings() => !ButtonOrientationStrings.IsDefault;
        private void ResetPaletteButtonOrientationStrings() => ButtonOrientationStrings.Reset();

        /// <summary>Gets the palette button spec style strings.</summary>
        /// <value>The palette button spec style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of palette button spec style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public PaletteButtonSpecStyleStrings PaletteButtonSpecStyleStrings => ButtonSpecStyles;
        private bool ShouldSerializePaletteButtonSpecStyleStrings() => !ButtonSpecStyles.IsDefault;
        private void ResetPaletteButtonSpecStyleStrings() => ButtonSpecStyles.Reset();

        /// <summary>Gets the palette button style strings.</summary>
        /// <value>The palette button style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of palette button style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public PaletteButtonStyleStrings PaletteButtonStyleStrings => PaletteButtonStyles;
        private bool ShouldSerializePaletteButtonStyleStrings() => !PaletteButtonStyles.IsDefault;
        private void ResetPaletteButtonStyleStrings() => PaletteButtonStyles.Reset();

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
        public KryptonAboutBoxBasicApplicationInformationStrings KryptonAboutBoxBasicApplicationInformationStrings => BasicApplicationInformationStrings;
        private bool ShouldSerializeKryptonAboutBoxBasicApplicationInformationStrings() => !BasicApplicationInformationStrings.IsDefault;
        private void ResetKryptonKryptonAboutBoxBasicApplicationInformationStrings() => BasicApplicationInformationStrings.Reset();

        /// <summary>Gets the krypton about box strings.</summary>
        /// <value>The krypton about box strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of about box strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public KryptonAboutBoxStrings KryptonAboutBoxStrings => AboutBoxStrings;
        private bool ShouldSerializeKryptonAboutBoxStrings() => !AboutBoxStrings.IsDefault;
        private void ResetKryptonAboutBoxStrings() => AboutBoxStrings.Reset();

        /// <summary>Gets the krypton miscellaneous theme strings.</summary>
        /// <value>The krypton miscellaneous theme strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of miscellaneous theme strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public KryptonMiscellaneousThemeStrings KryptonMiscellaneousThemeStrings => MiscellaneousThemeStrings;
        private bool ShouldSerializeKryptonMiscellaneousThemeStrings() => !MiscellaneousThemeStrings.IsDefault;
        private void ResetKryptonMiscellaneousThemeStrings() => MiscellaneousThemeStrings.Reset();

        [Category(@"Visuals")]
        [Description(@"Collection of outlook grid strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public KryptonOutlookGridStrings KryptonOutlookGridStrings => OutlookGridStrings;
        private bool ShouldSerializeKryptonOutlookGridStrings() => !OutlookGridStrings.IsDefault;
        private void ResetKryptonOutlookGridStrings() => OutlookGridStrings.Reset();

        /// <summary>Gets the scrollbar strings.</summary>
        /// <value>The scrollbar strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of scrollbar strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public KryptonScrollBarStrings KryptonScrollBarStrings => ScrollBarStrings;
        private bool ShouldSerializeKryptonScrollBarStrings() => !ScrollBarStrings.IsDefault;
        private void ResetKryptonScrollBarStrings() => ScrollBarStrings.Reset();

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
        public KryptonToastNotificationStrings KryptonToastNotificationStrings => ToastNotificationStrings;

        private bool ShouldSerializeKryptonToastNotificationStrings() => !ToastNotificationStrings.IsDefault;

        /// <summary>Resets the krypton toast notification strings.</summary>
        public void ResetKryptonToastNotificationStrings() => ToastNotificationStrings.Reset();

        #endregion

        #region Static Strings

        /// <summary>Gets the color strings.</summary>
        /// <value>The color strings.</value>
        public static GlobalColorStrings ColorStrings { get; } = new GlobalColorStrings();

        /// <summary>Gets the spec style strings.</summary>
        /// <value>The spec style strings.</value>
        public static ButtonStyleStrings ButtonStyles { get; } = new ButtonStyleStrings();

        /// <summary>Gets the custom toolkit strings.</summary>
        /// <value>The custom toolkit strings.</value>
        public static CustomToolkitStrings CustomToolkitStrings { get; } = new CustomToolkitStrings();

        /// <summary>Gets the strings.</summary>
        /// <value>The strings.</value>
        public static GeneralToolkitStrings GeneralToolkitStrings
        { get; } = new GeneralToolkitStrings();

        /// <summary>Gets the grid view style strings.</summary>
        /// <value>The grid view style strings.</value>
        public static DataGridViewStyleStrings DataGridViewStyles { get; } = new DataGridViewStyleStrings();

        /// <summary>Gets the style strings.</summary>
        /// <value>The style strings.</value>
        public static GridStyleStrings GridStyles { get; } = new GridStyleStrings();

        /// <summary>Gets the group collapsed target strings.</summary>
        /// <value>The group collapsed target strings.</value>
        public static HeaderGroupCollapsedTargetStrings GroupCollapsedTargetStrings { get; } =
        new HeaderGroupCollapsedTargetStrings();

        /// <summary>Gets the header styles.</summary>
        /// <value>The header styles.</value>
        public static HeaderStyleStrings HeaderStyles { get; } = new HeaderStyleStrings();

        /// <summary>Gets the input control styles.</summary>
        /// <value>The input control styles.</value>
        public static InputControlStyleStrings InputControlStyles { get; } = new InputControlStyleStrings();

        /// <summary>Gets the tool bar strings.</summary>
        /// <value>The tool bar strings.</value>
        public static IntegratedToolBarStrings ToolBarStrings { get; } = new IntegratedToolBarStrings();

        /// <summary>Gets the link behavior strings.</summary>
        /// <value>The link behavior strings.</value>
        public static KryptonLinkBehaviorStrings LinkBehaviorStrings { get; } = new KryptonLinkBehaviorStrings();

        /// <summary>Gets the krypton label style strings.</summary>
        /// <value>The krypton label style strings.</value>
        public static LabelStyleStrings KryptonLabelStyleStrings { get; } = new LabelStyleStrings();

        /// <summary>Gets the back style strings.</summary>
        /// <value>The back style strings.</value>
        public static PaletteBackStyleStrings BackStyleStrings { get; } = new PaletteBackStyleStrings();

        /// <summary>Gets the border style strings.</summary>
        /// <value>The border style strings.</value>
        public static PaletteBorderStyleStrings BorderStyleStrings { get; } = new PaletteBorderStyleStrings();

        /// <summary>Gets the button orientation strings.</summary>
        /// <value>The button orientation strings.</value>
        public static PaletteButtonOrientationStrings ButtonOrientationStrings { get; } =
        new PaletteButtonOrientationStrings();

        /// <summary>Gets the button spec styles.</summary>
        /// <value>The button spec styles.</value>
        public static PaletteButtonSpecStyleStrings ButtonSpecStyles { get; } = new PaletteButtonSpecStyleStrings();

        /// <summary>Gets the button style strings.</summary>
        /// <value>The button style strings.</value>
        public static PaletteButtonStyleStrings PaletteButtonStyles { get; } = new PaletteButtonStyleStrings();

        /// <summary>Gets the content style strings.</summary>
        /// <value>The content style strings.</value>
        public static PaletteContentStyleStrings ContentStyleStrings { get; } = new PaletteContentStyleStrings();

        /// <summary>Gets the image effect strings.</summary>
        /// <value>The image effect strings.</value>
        public static PaletteImageEffectStrings ImageEffectStrings { get; } = new PaletteImageEffectStrings();

        /// <summary>Gets the image style strings.</summary>
        /// <value>The image style strings.</value>
        public static PaletteImageStyleStrings ImageStyleStrings { get; } = new PaletteImageStyleStrings();

        /// <summary>Gets the mode strings.</summary>
        /// <value>The mode strings.</value>
        public static PaletteModeStrings ModeStrings { get; } = new PaletteModeStrings();

        /// <summary>Gets the text trim strings.</summary>
        /// <value>The text trim strings.</value>
        public static PaletteTextTrimStrings TextTrimStrings { get; } = new PaletteTextTrimStrings();

        /// <summary>Gets the placement mode strings.</summary>
        /// <value>The placement mode strings.</value>
        public static PlacementModeStrings PlacementModeStrings { get; } = new PlacementModeStrings();

        /// <summary>Gets the separator styles.</summary>
        /// <value>The separator styles.</value>
        public static SeparatorStyleStrings SeparatorStyles { get; } = new SeparatorStyleStrings();

        /// <summary>Gets the tab border styles.</summary>
        /// <value>The tab border styles.</value>
        public static TabBorderStyleStrings TabBorderStyles { get; } = new TabBorderStyleStrings();

        /// <summary>Gets the tab styles.</summary>
        /// <value>The tab styles.</value>
        public static TabStyleStrings TabStyles { get; } = new TabStyleStrings();

        /// <summary>Gets the toast notification icon.</summary>
        /// <value>The toast notification icon.</value>
        public static ToastNotificationIconStrings ToastNotificationIcon { get; } = new ToastNotificationIconStrings();

        /// <summary>Gets the basic application information strings.</summary>
        /// <value>The basic application information strings.</value>
        public static KryptonAboutBoxBasicApplicationInformationStrings BasicApplicationInformationStrings { get; } = new KryptonAboutBoxBasicApplicationInformationStrings();

        /// <summary>Gets the about box strings.</summary>
        /// <value>The about box strings.</value>
        public KryptonAboutBoxStrings AboutBoxStrings { get; } = new KryptonAboutBoxStrings();

        /// <summary>Gets the miscellaneous theme strings.</summary>
        /// <value>The miscellaneous theme strings.</value>
        public static KryptonMiscellaneousThemeStrings MiscellaneousThemeStrings { get; } =
        new KryptonMiscellaneousThemeStrings();

        /// <summary>Gets the outlook grid strings.</summary>
        /// <value>The outlook grid strings.</value>
        public static KryptonOutlookGridStrings OutlookGridStrings { get; } = new KryptonOutlookGridStrings();

        /// <summary>Gets the scroll bar strings.</summary>
        /// <value>The scroll bar strings.</value>
        public static KryptonScrollBarStrings ScrollBarStrings { get; } = new KryptonScrollBarStrings();

        /// <summary>Gets the toast notification strings.</summary>
        /// <value>The toast notification strings.</value>
        public KryptonToastNotificationStrings ToastNotificationStrings { get; } = new KryptonToastNotificationStrings();

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonGlobalToolkitStrings" /> class.</summary>
        public KryptonGlobalToolkitStrings()
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region Implementation

        /// <summary>Gets a value indicating whether this instance is default.</summary>
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDefault => !(ShouldSerializeCustomStrings() ||
                                   ShouldSerializeGeneralStrings() ||
                                   ShouldSerializeColorStrings() ||
                                   ShouldSerializePaletteModeStrings() ||
                                   ShouldSerializeButtonSpecStyleStrings() ||
                                   ShouldSerializeGridViewStyleStrings() ||
                                   ShouldSerializeGridStyleStrings() ||
                                   ShouldSerializeHeaderGroupCollapsedTargetStrings() ||
                                   ShouldSerializeHeaderStyleStrings() ||
                                   ShouldSerializeInputControlStyleStrings() ||
                                   ShouldSerializeIntegratedToolBarStrings() ||
                                   ShouldSerializeKryptonLinkBehaviorStrings() ||
                                   ShouldSerializePaletteBackStyleStrings() ||
                                   ShouldSerializePaletteBorderStyleStrings() ||
                                   //ShouldSerializePaletteButtonOrientationStrings() ||
                                   ShouldSerializePaletteButtonSpecStyleStrings() ||
                                   ShouldSerializePaletteButtonStyleStrings() ||
                                   ShouldSerializePaletteContentStyleStrings() ||
                                   ShouldSerializePaletteImageEffectStrings() ||
                                   ShouldSerializePaletteImageStyleStrings() ||
                                   ShouldSerializePaletteTextTrimStrings() ||
                                   ShouldSerializePlacementModeStrings() ||
                                   ShouldSerializeSeparatorStyleStrings() ||
                                   ShouldSerializeTabBorderStyleStrings() ||
                                   ShouldSerializeTabStyleStrings() ||
                                   ShouldSerializeToastNotificationIconStrings() ||
                                   ShouldSerializeKryptonAboutBoxBasicApplicationInformationStrings() ||
                                   ShouldSerializeKryptonAboutBoxStrings() ||
                                   ShouldSerializeKryptonMiscellaneousThemeStrings() ||
                                   ShouldSerializeKryptonOutlookGridStrings() ||
                                   ShouldSerializeKryptonScrollBarStrings() ||
                                   ShouldSerializeKryptonToastNotificationStrings());

        /// <summary>Resets this instance.</summary>
        public void Reset()
        {
            ResetColorStrings();

            ResetButtonSpecStyleStrings();

            ResetCustomStrings();

            ResetGeneralStrings();

            ResetPaletteModeStrings();

            ResetGridViewStyleStrings();

            ResetGridStyleStrings();

            ResetHeaderGroupCollapsedTargetStrings();

            ResetHeaderStyleStrings();

            ResetInputControlStyleStrings();

            ResetIntegratedToolBarStrings();

            ResetKryptonLinkBehaviorStrings();

            ResetPaletteBackStyleStrings();

            ResetPaletteBorderStyleStrings();

            //ResetPaletteButtonOrientationStrings();

            ResetPaletteButtonSpecStyleStrings();

            ResetPaletteButtonStyleStrings();

            ResetPaletteContentStyleStrings();

            ResetPaletteImageEffectStrings();

            ResetPaletteImageStyleStrings();

            ResetPaletteTextTrimStrings();

            ResetPlacementModeStrings();

            ResetSeparatorStyleStrings();

            ResetTabBorderStyleStrings();

            ResetTabStyleStrings();

            ResetToastNotificationIconStrings();

            ResetKryptonKryptonAboutBoxBasicApplicationInformationStrings();

            ResetKryptonAboutBoxStrings();

            ResetKryptonMiscellaneousThemeStrings();

            ResetKryptonOutlookGridStrings();

            ResetKryptonScrollBarStrings();

            ResetKryptonToastNotificationStrings();
        }

        #endregion
    }
}