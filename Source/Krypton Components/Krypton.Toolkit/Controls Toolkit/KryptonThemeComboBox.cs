#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Allows the user to change themes using a <see cref="KryptonComboBox"/>.
    /// </summary>
    /// <seealso cref="Krypton.Toolkit.KryptonComboBox" />
    public class KryptonThemeComboBox : KryptonComboBox
    {
        #region Instance Fields
        private readonly ICollection<string> _supportedThemesNames;
        private int _selectedIndex;
        #endregion

        #region Properties

        /// <summary>
        /// Helper, to return a new list of names
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<string> SupportedThemesList
        {
            get => _supportedThemesNames.ToList();

            //private set { _supportedThemesNames = value.ToArray(); }
        }

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

        } = new();

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="KryptonThemeComboBox" /> class.</summary>
        public KryptonThemeComboBox()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;

            _supportedThemesNames = ThemeManager.SupportedInternalThemeNames;
            _selectedIndex = 33;
        }
        #endregion

        #region Methods

        /// <summary>Returns the palette mode.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public PaletteMode ReturnPaletteMode() => Manager.GlobalPaletteMode;

        // TODO: Refresh the theme names if the values have been altered
        //public void RefreshSupportedThemeList() => SupportedThemesNames = ThemeManager.SupportedInternalThemeNames;

        #endregion

        #region Protected Overrides

        /// <inheritdoc />
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Items.AddRange(_supportedThemesNames.ToArray());
            SelectedIndex = _selectedIndex;
        }

        /// <inheritdoc />
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            ThemeManager.ApplyTheme(Text, Manager);

            ThemeSelectedIndex = SelectedIndex;

            base.OnSelectedIndexChanged(e);
            if ((ThemeManager.GetThemeManagerMode(Text) == PaletteMode.Custom)
                && (KryptonCustomPalette != null)
               )
            {
                Manager.GlobalPalette = KryptonCustomPalette;
            }
        }

        #endregion

        #region Removed Designer visibility
        /// <summary>
        /// Gets and sets the text associated associated with the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [AllowNull]
        public override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        /// <summary>
        /// Gets and sets the appearance and functionality of the KryptonComboBox.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ComboBoxStyle DropDownStyle
        {
            get => base.DropDownStyle;
            set => base.DropDownStyle = value;
        }

        /// <summary>
        /// Gets or sets the items in the KryptonComboBox.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ComboBox.ObjectCollection Items => base.Items;

        /// <summary>Gets or sets the draw mode of the combobox.</summary>
        /// <value>The draw mode of the combobox.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DrawMode DrawMode
        {
            get => base.DrawMode;
            set => base.DrawMode = value;
        }

        /// <summary>
        /// Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => base.AutoCompleteCustomSource;
            set => base.AutoCompleteCustomSource = value;
        }

        #endregion
    }

}