#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Ribbon
{
    public class KryptonRibbonGroupThemeComboBox : KryptonRibbonGroupComboBox
    {
        #region Instance Fields
        private readonly ICollection<string> _supportedThemesNames;
        private int _selectedIndex;
        #endregion

        #region Public

        /// <summary>
        /// Helper, to return a new list of names
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<string> SupportedThemesList => _supportedThemesNames.ToList();

        /// <summary>
        /// Gets and sets the ThemeSelectedIndex.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Theme Selected Index. (Default = `Office 365 - Blue`)")]
        [DefaultValue(33)]
        public int ThemeSelectedIndex
        {
            get => _selectedIndex;

            set => SelectedIndex = value;
        }

        private void ResetThemeSelectedIndex() => _selectedIndex = 33;

        private bool ShouldSerializeThemeSelectedIndex() => _selectedIndex != 33;

        /// <summary>
        /// Gets and sets the ThemeSelectedIndex.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Custom Theme to use when `Custom` is selected")]
        [DefaultValue(null)]
        public KryptonCustomPaletteBase KryptonCustomPalette { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public KryptonManager Manager
        {
            get;

        } = new();

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonRibbonGroupThemeComboBox" /> class.</summary>
        public KryptonRibbonGroupThemeComboBox()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;

            _supportedThemesNames = RibbonThemeManager.SupportedInternalThemeNames;

            _selectedIndex = 33;

            Items.AddRange(_supportedThemesNames.ToArray());

            SelectedIndex = _selectedIndex;
        }

        #endregion

        #region Implementation

        /// <summary>Returns the palette mode.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public PaletteMode ReturnPaletteMode() => Manager.GlobalPaletteMode;

        #endregion

        #region Protected Overrides

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            RibbonThemeManager.ApplyTheme(Text, Manager);

            ThemeSelectedIndex = SelectedIndex;

            base.OnSelectedIndexChanged(e);
            if ((RibbonThemeManager.GetThemeManagerMode(Text) == PaletteMode.Custom) && (KryptonCustomPalette != null))
            {
                Manager.GlobalPalette = KryptonCustomPalette;
            }
        }


        #endregion

        #region Removed Designer visibility



        #endregion
    }
}