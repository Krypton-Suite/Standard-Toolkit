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

        /// <summary>Gets the data grid view style strings.</summary>
        /// <value>The data grid view style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of datagrid view style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public DataGridViewStyleStrings DataGridViewStyleStrings => GridViewStyleStrings;

        private bool ShouldSerializeDataGridViewStyleStrings() => !GridViewStyleStrings.IsDefault;

        /// <summary>Resets the data grid view style strings.</summary>
        public void ResetDataGridViewStyleStrings() => GridViewStyleStrings.Reset();

        /// <summary>Gets the grid style strings.</summary>
        /// <value>The grid style strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of grid view style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public GridStyleStrings GridStyleStrings => StyleStrings;

        private bool ShouldSerializeGridStyleStrings() => !StyleStrings.IsDefault;

        /// <summary>Resets the grid style strings.</summary>
        public void ResetGridStyleStrings() => StyleStrings.Reset();

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

        /// <summary>Gets the mode strings.</summary>
        /// <value>The mode strings.</value>
        public static PaletteModeStrings ModeStrings { get; } = new();

        /// <summary>Gets the grid view style strings.</summary>
        /// <value>The grid view style strings.</value>
        public static DataGridViewStyleStrings GridViewStyleStrings { get; } = new();

        /// <summary>Gets the style strings.</summary>
        /// <value>The style strings.</value>
        public static GridStyleStrings StyleStrings { get; } = new();

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
                                       ShouldSerializeGridStyleStrings());

        /// <summary>Resets this instance.</summary>
        public void Reset()
        {
            ResetColorStrings();

            ResetButtonSpecStyleStrings();

            ResetGeneralStrings();

            ResetPaletteModeStrings();

            ResetDataGridViewStyleStrings();

            ResetGridStyleStrings();
        }

        #endregion
    }
}