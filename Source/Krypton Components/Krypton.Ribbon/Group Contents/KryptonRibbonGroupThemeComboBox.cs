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
    /// <summary>
    /// Represents a ribbon group theme combo box.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupThemeComboBox), "ToolboxBitmaps.KryptonRibbonGroupComboBox.bmp")]
    [Designer(typeof(KryptonRibbonGroupThemeComboBoxDesigner))]
    [DesignerCategory(@"code")]
    [DesignTimeVisible(false)]
    [DefaultEvent("SelectedTextChanged")]
    [DefaultProperty(nameof(Text))]
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

        #region Removed Designer

        /// <summary>Gets and sets the text associated with the control.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [AllowNull]
        public new string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        /// <summary>Gets and sets the appearance and functionality of the KryptonComboBox.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ComboBoxStyle DropDownStyle
        {
            get => base.DropDownStyle;
            set => base.DropDownStyle = value;
        }

        /// <summary>Gets or sets the items in the KryptonComboBox.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ComboBox.ObjectCollection Items => base.Items;

        /// <summary>Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => base.AutoCompleteCustomSource;
            set => base.AutoCompleteCustomSource = value;
        }

        /// <summary>Gets or sets the text completion behavior of the combobox.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new AutoCompleteMode AutoCompleteMode
        {
            get => base.AutoCompleteMode;
            set => base.AutoCompleteMode = value;
        }

        /// <summary>Gets or sets the autocomplete source, which can be one of the values from AutoCompleteSource enumeration.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new AutoCompleteSource AutoCompleteSource
        {
            get => base.AutoCompleteSource;
            set => base.AutoCompleteSource = value;
        }

        #endregion
    }
}