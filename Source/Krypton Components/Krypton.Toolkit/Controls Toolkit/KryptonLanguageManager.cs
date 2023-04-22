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
    [Category(@"Code")]
    [Description(@"Access 'Global' Krypton string settings.")]
    [ToolboxBitmap(typeof(KryptonLanguageManager), "ToolboxBitmaps.KryptonLanguageManager.bmp")]
    [ToolboxItem(true)]
    public class KryptonLanguageManager : Component
    {
        #region Instance Fields


        #endregion

        #region Public

        [Category(@"Visuals")]
        [Description(@"Collection of color strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public GlobalColorStrings GlobalColorStrings => ColorStrings;

        private bool ShouldSerializeColorStrings() => !ColorStrings.IsDefault;

        public void ResetColorStrings() => ColorStrings.Reset();

        [Category(@"Visuals")]
        [Description(@"Collection of button spec style strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public ButtonSpecStyleStrings ButtonSpecStyleStrings => SpecStyleStrings;

        private bool ShouldSerializeButtonSpecStyleStrings() => !SpecStyleStrings.IsDefault;

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

        public void ResetGeneralStrings() => Strings.Reset();

        public PaletteModeStrings PaletteModeStrings => ModeStrings;

        private bool ShouldSerializePaletteModeStrings() => !ModeStrings.IsDefault;

        public void ResetPaletteModeStrings() => ModeStrings.Reset();

        public DataGridViewStyleStrings DataGridViewStyleStrings => GridViewStyleStrings;

        private bool ShouldSerializeDataGridViewStyleStrings() => !GridViewStyleStrings.IsDefault;

        public void ResetDataGridViewStyleStrings() => GridViewStyleStrings.Reset();

        public GridStyleStrings GridStyleStrings => StyleStrings;

        private bool ShouldSerializeGridStyleStrings() => !StyleStrings.IsDefault;

        public void ResetGridStyleStrings() => StyleStrings.Reset();

        #endregion

        #region Static Strings

        public static GlobalColorStrings ColorStrings
        { get; } = new();

        public static ButtonSpecStyleStrings SpecStyleStrings { get; } = new();

        /// <summary>Gets the strings.</summary>
        /// <value>The strings.</value>
        public static GeneralStrings Strings { get; } = new();

        public static PaletteModeStrings ModeStrings { get; } = new();

        public static DataGridViewStyleStrings GridViewStyleStrings { get; } = new();

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