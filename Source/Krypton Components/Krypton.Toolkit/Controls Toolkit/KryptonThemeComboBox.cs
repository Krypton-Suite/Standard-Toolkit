#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Allows the user to change themes using a <see cref="KryptonComboBox"/>.</summary>
    /// <seealso cref="KryptonComboBox" />
    public class KryptonThemeComboBox : KryptonComboBox
    {
        #region Instance Fields

        private bool _isUpdating = false;
        private bool _handleCreated = false;
        private bool _pendingPaletteUpdate = false;
        private PaletteMode _pendingPaletteMode;

        private bool _reportSelectedThemeIndex;

        private int _selectedIndex;

        private readonly int? _defaultPaletteIndex = GlobalStaticValues.GLOBAL_DEFAULT_THEME_INDEX;

        private PaletteMode _defaultPalette;

        private KryptonManager? _manager;

        #endregion

        #region Public

        /// <summary>Gets or sets a value indicating whether [report selected theme index].</summary>
        /// <value><c>true</c> if [report selected theme index]; otherwise, <c>false</c>.</value>
        public bool ReportSelectedThemeIndex
        {
            get => _reportSelectedThemeIndex;

            set => _reportSelectedThemeIndex = value;
        }

        /// <summary>Gets or sets the default palette mode.</summary>
        /// <value>The default palette mode.</value>
        [Category(@"Visuals")]
        [Description(@"The default palette mode.")]
        [DefaultValue(PaletteMode.Microsoft365Blue)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public PaletteMode DefaultPalette
        {
            get => _defaultPalette;

            set
            {
                if (_defaultPalette == value)
                {
                    return;
                }

                if (_handleCreated)
                {
                    // Safe to directly access UI thread now
                    _defaultPalette = value;
                    UpdateDefaultPaletteIndex(value);
                }
                else
                {
                    // Defer until the handle is created
                    _pendingPaletteUpdate = true;
                    _pendingPaletteMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the ThemeSelectedIndex.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Theme Selected Index. (Default = `Office 365 - Blue`)")]
        [DefaultValue((int)PaletteMode.Microsoft365Blue)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private int ThemeSelectedIndex
        {
            get => _selectedIndex = _defaultPaletteIndex ?? 30;
            set => _selectedIndex = SelectedIndex = value;
        }

        private void ResetThemeSelectedIndex() => _selectedIndex = _defaultPaletteIndex ?? 30;

        private bool ShouldSerializeThemeSelectedIndex() => _selectedIndex != _defaultPaletteIndex;

        /// <summary>
        /// Gets and sets a KryptonCustomPalette.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Custom palette (if set)")]
        [DefaultValue(null)]
        public KryptonCustomPaletteBase? KryptonCustomPalette { get; set; }

        [Category(@"Data")]
        [Description(@"")]
        [DefaultValue(null)]
        public KryptonManager? Manager
        {
            get => _manager;
            set => _manager = value;
        }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonThemeComboBox" /> class.</summary>
        public KryptonThemeComboBox()
        {
            #if DEBUG
            _reportSelectedThemeIndex = true;
            #else
            _reportSelectedThemeIndex = false;
            #endif
            _manager = new KryptonManager();

            var defaultThemeName = ThemeManager.ReturnPaletteModeAsString(PaletteMode.Microsoft365Blue);
            _defaultPaletteIndex = 30; // was 24 which is wrong!
            DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (var kvp in PaletteModeStrings.SupportedThemesMap)
            {
                Items.Add(kvp.Key);
                if (kvp.Key == defaultThemeName)
                {
                    _defaultPaletteIndex = Items.Count - 1;
                }
            }
            base.Text = defaultThemeName;
            _selectedIndex = SelectedIndex = (int)_defaultPaletteIndex;

            _defaultPalette = PaletteMode.Microsoft365Blue;

            Debug.Assert(_selectedIndex == _defaultPaletteIndex, $@"Microsoft365Blue needs to be at the index position of {_defaultPaletteIndex} for backward compatibility");

            HandleCreated += KryptonThemeComboBox_HandleCreated;
        }
        #endregion

        #region Implementation

        private void KryptonThemeComboBox_HandleCreated(object sender, EventArgs e)
        {
            _handleCreated = true;
            if (!_pendingPaletteUpdate)
            {
                return;
            }

            _pendingPaletteUpdate = false;
            DefaultPalette = _pendingPaletteMode;
        }

        private void UpdateDefaultPaletteIndex(PaletteMode mode)
        {
            if (_isUpdating)
            {
                return;
            }

            _isUpdating = true;

            var selectedText = ThemeManager.ReturnPaletteModeAsString(mode);
            var newIdx = Items.IndexOf(selectedText);
            if (newIdx >= 0 && newIdx < PaletteModeStrings.SupportedThemesMap.Count)
            {
                ThemeSelectedIndex = newIdx;
            }

            _isUpdating = false;
        }

        /// <summary>Returns the palette mode.</summary>
        /// <returns>PaletteMode of the Manager</returns>
        public PaletteMode ReturnPaletteMode() => Manager!.GlobalPaletteMode;

        // TODO: Refresh the theme names if the values have been altered

        #endregion

        #region Protected Overrides

        /// <inheritdoc />
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            // At this point, a potential new Manager is assigned (by Designer file)
            // If its GlobalPaletteMode is not Custom or Global, set the combo's text:
            var mgrMode = ReturnPaletteMode();
            if (mgrMode != PaletteMode.Custom && mgrMode != PaletteMode.Global)
            {
                var selectedText = ThemeManager.ReturnPaletteModeAsString(mgrMode);
                // this triggers below OnSelectedIndexChanged
                base.Text = selectedText;
                return;
            }
            SelectedIndex = _selectedIndex;
        }

        /// <inheritdoc />
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            ThemeManager.ApplyTheme(Text!, Manager!);

            ThemeSelectedIndex = SelectedIndex;

            base.OnSelectedIndexChanged(e);
            if ((ThemeManager.GetThemeManagerMode(Text!) == PaletteMode.Custom)
                && (KryptonCustomPalette != null)
               )
            {
                Manager!.GlobalCustomPalette = KryptonCustomPalette;
            }

            if (_reportSelectedThemeIndex)
            {
                //KryptonMessageBox.Show($@"The index for '{SelectedItem}' is {SelectedIndex}",
                //  @"Theme Index", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.SystemInformation);
                Debug.WriteLine($@"The index for '{SelectedItem}' is {SelectedIndex}");
            }
        }

        #endregion

        #region Removed Designer Visibility
        /// <summary>
        /// Gets and sets the text associated with the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [AllowNull]
        public override string? Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        /// <summary>Gets or sets the format specifier characters that indicate how a value is to be Displayed.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string FormatString
        {
            get => base.FormatString;

            set => base.FormatString = value;
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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int SelectedIndex { get => base.SelectedIndex; set => base.SelectedIndex = value; }

        #endregion
    }

}