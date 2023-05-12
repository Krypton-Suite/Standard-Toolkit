#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Access 'Global' Krypton string settings.</summary>
    [Category(@"Code")]
    [Description(@"Access 'Global' Krypton string settings.")]
    [ToolboxBitmap(typeof(KryptonLanguageManager), "ToolboxBitmaps.KryptonLanguageManager.bmp")]
    [ToolboxItem(true)]
    public class KryptonLanguageManager : Component
    {
        #region Instance Fields


        #endregion

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

        /// <summary>Resets the color strings.</summary>
        public void ResetColorStrings() => ColorStrings.Reset();

        /// <summary>Gets the button spec style strings.</summary>
        /// <value>The button spec style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of button spec style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public ButtonSpecStyleStrings ButtonSpecStyleStrings => SpecStyleStrings;

        private bool ShouldSerializeButtonSpecStyleStrings() => !SpecStyleStrings.IsDefault;

        /// <summary>Resets the button spec style strings.</summary>
        public void ResetButtonSpecStyleStrings() => SpecStyleStrings.Reset();

        /// <summary>Gets the general strings.</summary>
        /// <value>The general strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of general strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public GeneralStrings GeneralStrings => Strings;

        private bool ShouldSerializeGeneralStrings() => !Strings.IsDefault;

        /// <summary>Resets the general strings.</summary>
        public void ResetGeneralStrings() => Strings.Reset();

        /// <summary>Gets the data grid view style strings.</summary>
        /// <value>The data grid view style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of datagrid view style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public DataGridViewStyleStrings DataGridViewStyleStrings => DataGridViewStyles;

        private bool ShouldSerializeDataGridViewStyleStrings() => !DataGridViewStyles.IsDefault;

        /// <summary>Resets the data grid view style strings.</summary>
        public void ResetDataGridViewStyleStrings() => DataGridViewStyles.Reset();

        /// <summary>Gets the grid style strings.</summary>
        /// <value>The grid style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of grid style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public GridStyleStrings GridStyleStrings => GridStyles;

        private bool ShouldSerializeGridStyleStrings() => !GridStyles.IsDefault;

        /// <summary>Resets the grid style strings.</summary>
        public void ResetGridStyleStrings() => GridStyles.Reset();

        /// <summary>Gets the header group collapsed target strings.</summary>
        /// <value>The header group collapsed target strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of header group collapsed target strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public HeaderGroupCollapsedTargetStrings HeaderGroupCollapsedTargetStrings => GroupCollapsedTargetStrings;

        private bool ShouldSerializeHeaderGroupCollapsedTargetStrings() => !GroupCollapsedTargetStrings.IsDefault;

        /// <summary>Resets the header group collapsed target strings.</summary>
        public void ResetHeaderGroupCollapsedTargetStrings() => GroupCollapsedTargetStrings.Reset();

        /// <summary>Gets the header style strings.</summary>
        /// <value>The header style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of header style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public HeaderStyleStrings HeaderStyleStrings => HeaderStyles;

        private bool ShouldSerializeHeaderStyleStrings() => !HeaderStyles.IsDefault;

        /// <summary>Resets the header style strings.</summary>
        public void ResetHeaderStyleStrings() => HeaderStyles.Reset();

        /// <summary>Gets the input control style strings.</summary>
        /// <value>The input control style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of input control style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public InputControlStyleStrings InputControlStyleStrings => InputControlStyles;

        private bool ShouldSerializeInputControlStyleStrings() => !InputControlStyles.IsDefault;

        /// <summary>Resets the input control style strings.</summary>
        public void ResetInputControlStyleStrings() => InputControlStyles.Reset();

        /// <summary>Gets the integrated toolbar button strings.</summary>
        /// <value>The integrated toolbar button strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of integrated toolbar button strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public IntegratedToolBarStrings IntegratedToolBarStrings => ToolBarStrings;

        private bool ShouldSerializeIntegratedToolBarStrings() => !ToolBarStrings.IsDefault;

        /// <summary>Resets the integrated tool bar strings.</summary>
        public void ResetIntegratedToolBarStrings() => ToolBarStrings.Reset();

        /// <summary>Gets the link behavior style strings.</summary>
        /// <value>The link behavior style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of link behavior style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public KryptonLinkBehaviorStrings KryptonLinkBehaviorStrings => LinkBehaviorStrings;

        private bool ShouldSerializeKryptonLinkBehaviorStrings() => !LinkBehaviorStrings.IsDefault;

        /// <summary>Resets the krypton link behavior strings.</summary>
        public void ResetKryptonLinkBehaviorStrings() => LinkBehaviorStrings.Reset();

        /// <summary>Gets the link style strings.</summary>
        /// <value>The link style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of link style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public LabelStyleStrings LabelStyleStrings => KryptonLabelStyleStrings;

        private bool ShouldSerializeLabelStyleStrings() => !LabelStyleStrings.IsDefault;

        /// <summary>Resets the label style strings.</summary>
        public void ResetLabelStyleStrings() => LabelStyleStrings.Reset();

        /// <summary>Gets the palette back style strings.</summary>
        /// <value>The palette back style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of palette back style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public PaletteBackStyleStrings PaletteBackStyleStrings => BackStyleStrings;

        private bool ShouldSerializePaletteBackStyleStrings() => !BackStyleStrings.IsDefault;

        /// <summary>Resets the palette back style strings.</summary>
        public void ResetPaletteBackStyleStrings() => BackStyleStrings.Reset();

        /// <summary>Gets the palette border style strings.</summary>
        /// <value>The palette border style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of palette border style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public PaletteBorderStyleStrings PaletteBorderStyleStrings => BorderStyleStrings;

        private bool ShouldSerializePaletteBorderStyleStrings() => !BorderStyleStrings.IsDefault;

        /// <summary>Resets the palette border style strings.</summary>
        public void ResetPaletteBorderStyleStrings() => BorderStyleStrings.Reset();

        /// <summary>Gets the palette button orientation strings.</summary>
        /// <value>The palette button orientation strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of palette button orientation strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public PaletteButtonOrientationStrings PaletteButtonOrientationStrings => ButtonOrientationStrings;

        private bool ShouldSerializePaletteButtonOrientationStrings() => !ButtonOrientationStrings.IsDefault;

        /// <summary>Resets the palette button orientation strings.</summary>
        public void ResetPaletteButtonOrientationStrings() => ButtonOrientationStrings.Reset();

        /// <summary>Gets the palette button spec style strings.</summary>
        /// <value>The palette button spec style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of palette button spec style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public PaletteButtonSpecStyleStrings PaletteButtonSpecStyleStrings => ButtonSpecStyles;

        private bool ShouldSerializePaletteButtonSpecStyleStrings() => !ButtonSpecStyles.IsDefault;

        /// <summary>Resets the palette button spec style strings.</summary>
        public void ResetPaletteButtonSpecStyleStrings() => ButtonSpecStyles.Reset();

        /// <summary>Gets the palette button style strings.</summary>
        /// <value>The palette button style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of palette button style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public PaletteButtonStyleStrings PaletteButtonStyleStrings => ButtonStyleStrings;

        private bool ShouldSerializePaletteButtonStyleStrings() => !ButtonStyleStrings.IsDefault;

        private void ResetPaletteButtonStyleStrings() => ButtonStyleStrings.Reset();

        /// <summary>Gets the palette content style strings.</summary>
        /// <value>The palette content style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of palette mode strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public PaletteContentStyleStrings PaletteContentStyleStrings => ContentStyleStrings;

        private bool ShouldSerializePaletteContentStyleStrings() => !ContentStyleStrings.IsDefault;

        public void ResetPaletteContentStyleStrings() => ContentStyleStrings.Reset();

        /// <summary>Gets the palette mode strings.</summary>
        /// <value>The palette mode strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of palette mode strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public PaletteModeStrings PaletteModeStrings => ModeStrings;

        private bool ShouldSerializePaletteModeStrings() => !ModeStrings.IsDefault;

        /// <summary>Resets the palette mode strings.</summary>
        public void ResetPaletteModeStrings() => ModeStrings.Reset();

        #endregion

        #region Static Strings

        /// <summary>Gets the color strings.</summary>
        /// <value>The color strings.</value>
        public static GlobalColorStrings ColorStrings
        { get; } = new();

        /// <summary>Gets the spec style strings.</summary>
        /// <value>The spec style strings.</value>
        public static ButtonSpecStyleStrings SpecStyleStrings { get; } = new();

        /// <summary>Gets the strings.</summary>
        /// <value>The strings.</value>
        public static GeneralStrings Strings { get; } = new();

        /// <summary>Gets the grid view style strings.</summary>
        /// <value>The grid view style strings.</value>
        public static DataGridViewStyleStrings DataGridViewStyles { get; } = new();

        /// <summary>Gets the style strings.</summary>
        /// <value>The style strings.</value>
        public static GridStyleStrings GridStyles { get; } = new();

        /// <summary>Gets the group collapsed target strings.</summary>
        /// <value>The group collapsed target strings.</value>
        public static HeaderGroupCollapsedTargetStrings GroupCollapsedTargetStrings { get; } = new();

        /// <summary>Gets the header styles.</summary>
        /// <value>The header styles.</value>
        public static HeaderStyleStrings HeaderStyles { get; } = new();

        /// <summary>Gets the input control styles.</summary>
        /// <value>The input control styles.</value>
        public static InputControlStyleStrings InputControlStyles { get; } = new();

        /// <summary>Gets the tool bar strings.</summary>
        /// <value>The tool bar strings.</value>
        public static IntegratedToolBarStrings ToolBarStrings { get; } = new();

        /// <summary>Gets the link behavior strings.</summary>
        /// <value>The link behavior strings.</value>
        public static KryptonLinkBehaviorStrings LinkBehaviorStrings { get; } = new();

        /// <summary>Gets the krypton label style strings.</summary>
        /// <value>The krypton label style strings.</value>
        public static LabelStyleStrings KryptonLabelStyleStrings { get; } = new();

        /// <summary>Gets the back style strings.</summary>
        /// <value>The back style strings.</value>
        public static PaletteBackStyleStrings BackStyleStrings { get; } = new();

        /// <summary>Gets the border style strings.</summary>
        /// <value>The border style strings.</value>
        public static PaletteBorderStyleStrings BorderStyleStrings { get; } = new();

        /// <summary>Gets the button orientation strings.</summary>
        /// <value>The button orientation strings.</value>
        public static PaletteButtonOrientationStrings ButtonOrientationStrings { get; } = new();

        /// <summary>Gets the button spec styles.</summary>
        /// <value>The button spec styles.</value>
        public static PaletteButtonSpecStyleStrings ButtonSpecStyles { get; } = new();

        /// <summary>Gets the button style strings.</summary>
        /// <value>The button style strings.</value>
        public static PaletteButtonStyleStrings ButtonStyleStrings { get; } = new();

        /// <summary>Gets the content style strings.</summary>
        /// <value>The content style strings.</value>
        public static PaletteContentStyleStrings ContentStyleStrings { get; } = new();

        /// <summary>Gets the mode strings.</summary>
        /// <value>The mode strings.</value>
        public static PaletteModeStrings ModeStrings { get; } = new();

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonLanguageManager" /> class.</summary>
        public KryptonLanguageManager()
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region Implementation

        /// <summary>Gets a value indicating whether this instance is default.</summary>
        /// <value>
        ///   <c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDefault => !(ShouldSerializeGeneralStrings() ||
                                   ShouldSerializeColorStrings() ||
                                   ShouldSerializePaletteModeStrings() ||
                                   ShouldSerializeButtonSpecStyleStrings() ||
                                   ShouldSerializeDataGridViewStyleStrings() ||
                                   ShouldSerializeGridStyleStrings() ||
                                   ShouldSerializeHeaderGroupCollapsedTargetStrings() ||
                                   ShouldSerializeHeaderStyleStrings() ||
                                   ShouldSerializeInputControlStyleStrings() ||
                                   ShouldSerializeIntegratedToolBarStrings() ||
                                   ShouldSerializeKryptonLinkBehaviorStrings() ||
                                   ShouldSerializePaletteBackStyleStrings() ||
                                   ShouldSerializePaletteBorderStyleStrings() ||
                                   ShouldSerializePaletteButtonOrientationStrings() ||
                                   ShouldSerializePaletteButtonSpecStyleStrings() ||
                                   ShouldSerializePaletteButtonStyleStrings() ||
                                   ShouldSerializePaletteContentStyleStrings());

        /// <summary>Resets this instance.</summary>
        public void Reset()
        {
            ResetColorStrings();

            ResetButtonSpecStyleStrings();

            ResetGeneralStrings();

            ResetPaletteModeStrings();

            ResetDataGridViewStyleStrings();

            ResetGridStyleStrings();

            ResetHeaderGroupCollapsedTargetStrings();

            ResetHeaderStyleStrings();

            ResetInputControlStyleStrings();

            ResetIntegratedToolBarStrings();

            ResetKryptonLinkBehaviorStrings();

            ResetPaletteBackStyleStrings();

            ResetPaletteBorderStyleStrings();

            ResetPaletteButtonOrientationStrings();

            ResetPaletteButtonSpecStyleStrings();

            ResetPaletteButtonStyleStrings();

            ResetPaletteContentStyleStrings();
        }

        #endregion

        #region Protected



        #endregion
    }
}