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
    //[Designer(typeof(KryptonLanguageManagerDesigner))]
    [ToolboxBitmap(typeof(KryptonLanguageManager), "ToolboxBitmaps.KryptonLanguageManager.bmp")]
    [ToolboxItem(true)]
    public class KryptonLanguageManager : Component
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

        /// <summary>Resets the color strings.</summary>
        public void ResetColorStrings() => ColorStrings.Reset();

        /// <summary>Gets the custom toolkit strings.</summary>
        /// <value>The custom toolkit strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of custom toolkit strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public CustomToolkitStrings CustomStrings => CustomToolkitStrings;

        private bool ShouldSerializeCustomStrings() => !CustomToolkitStrings.IsDefault;

        /// <summary>Resets the custom strings.</summary>
        public void ResetCustomStrings() => CustomToolkitStrings.ResetValues();

        /// <summary>Gets the general toolkit strings.</summary>
        /// <value>The general toolkit strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of general toolkit strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public GeneralToolkitStrings GeneralStrings => GeneralToolkitStrings;

        private bool ShouldSerializeGeneralStrings() => !GeneralToolkitStrings.IsDefault;

        /// <summary>Resets the general strings.</summary>
        public void ResetGeneralStrings() => GeneralToolkitStrings.Reset();

        /// <summary>Gets the scrollbar strings.</summary>
        /// <value>The scrollbar strings.</value>
        [Category(@"Visuals")]
        [Description(@"Collection of scrollbar strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public KryptonScrollBarStrings KryptonScrollBarStrings => ScrollBarStrings;

        private bool ShouldSerializeKryptonScrollBarStrings() => !ScrollBarStrings.IsDefault;

        /// <summary>Resets the krypton scroll bar strings.</summary>
        public void ResetKryptonScrollBarStrings() => ScrollBarStrings.Reset();

        #endregion

        #region Static Strings

        /// <summary>Gets the color strings.</summary>
        /// <value>The color strings.</value>
        public static GlobalColorStrings ColorStrings { get; } = new GlobalColorStrings();

        public static CustomToolkitStrings CustomToolkitStrings { get; } = new CustomToolkitStrings();

        /// <summary>Gets the strings.</summary>
        /// <value>The strings.</value>
        public static GeneralToolkitStrings GeneralToolkitStrings
        { get; } = new GeneralToolkitStrings();

        /// <summary>Gets the scroll bar strings.</summary>
        /// <value>The scroll bar strings.</value>
        public static KryptonScrollBarStrings ScrollBarStrings { get; } = new KryptonScrollBarStrings();

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
        public bool IsDefault => !(ShouldSerializeCustomStrings() ||
                                   ShouldSerializeGeneralStrings() ||
                                   ShouldSerializeColorStrings() ||
                                   ShouldSerializeKryptonScrollBarStrings());

        /// <summary>Resets this instance.</summary>
        public void Reset()
        {
            ResetColorStrings();

            ResetCustomStrings();

            ResetGeneralStrings();

            ResetKryptonScrollBarStrings();
        }

        #endregion
    }
}