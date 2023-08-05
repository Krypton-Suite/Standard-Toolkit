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
        private int _selectedIndex;
        #endregion

        #region Public

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
        public KryptonCustomPaletteBase? KryptonCustomPalette { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public KryptonManager Manager
        {
            get;
        } = new KryptonManager();

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonRibbonGroupThemeComboBox" /> class.</summary>
        public KryptonRibbonGroupThemeComboBox()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
            DisplayMember = "Key";
            ValueMember = "Value";
            foreach (var kvp in PaletteModeStrings.SupportedThemesMap)
            {
                Items.Add(kvp);
            }
            var cnvtr = new PaletteModeConverter();
            Text = cnvtr.ConvertToString(PaletteMode.Microsoft365Blue)!;
            _selectedIndex = SelectedIndex;
            Debug.Assert(_selectedIndex == 33, "Microsoft365Blue needs to be at the 33rd index for backward compatibility");
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

        /// <inheritdoc />
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

    }
}