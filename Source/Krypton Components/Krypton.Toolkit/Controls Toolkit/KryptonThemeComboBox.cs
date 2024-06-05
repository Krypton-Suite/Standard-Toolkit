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
    internal interface IKryptonThemeSelectorBase
    {
        PaletteMode DefaultPalette { get; set; }
        
        void ResetDefaultPalette();
        bool ShouldSerializeDefaultPalette();

        KryptonCustomPaletteBase? KryptonCustomPalette { get; set; }

        void ResetKryptonCustomPalette();
        bool ShouldSerializeKryptonCustomPalette();

        bool ReportSelectedThemeIndex { get; set; }
    }

    /// <summary>Allows the user to change themes using a <see cref="KryptonComboBox"/>.</summary>
    /// <seealso cref="KryptonComboBox" />
    public class KryptonThemeComboBox : KryptonComboBox, IKryptonThemeSelectorBase
    {
        #region Instance Fields

        /// <summary> When we change the palette, Krypton Manager will notify us that there was a change. Since we are changing it that notification can be skipped.</summary>
        private bool _isLocalUpdate = false;
        /// <summary> Suppress code execution in the SelectedIndexChanged event handler. when a theme change via the KManager has been performed.</summary>
        private bool _isExternalUpdate = false;
        /// <summary> Backing var for the DefaultPalette property.</summary>
        private PaletteMode _defaultPalette;
        /// <summary> Local Krypton Manager instance.</summary>
        private KryptonManager _manager;
        /// <summary> User defined palette.</summary>
        private KryptonCustomPaletteBase? _kryptonCustomPalette = null;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonThemeComboBox" /> class.</summary>
        public KryptonThemeComboBox()
        {
            _manager = new KryptonManager();
            DropDownStyle = ComboBoxStyle.DropDownList;

            Items.Clear();
            Items.AddRange(CommonHelperThemeSelectors.GetThemesArray());

            // If the DefaultPaletteMode is Global and KManager.GlobalPaletteMode is not Custom or Global, set the combo's text:
            if (DefaultPalette == PaletteMode.Global
                && _manager.GlobalPaletteMode != PaletteMode.Custom 
                && _manager.GlobalPaletteMode != PaletteMode.Global)
            {
                // this triggers below OnSelectedIndexChanged
                SelectedIndex = CommonHelperThemeSelectors.GetPaletteIndex(Items, _manager.GlobalPaletteMode);
            }

            // Process external theme changes
            KryptonManager.GlobalPaletteChanged += KryptonManagerGlobalPaletteChanged;
        }
        #endregion

        #region Public

        // TODO: Deprecated should be removed
        /// <summary>
        /// ReportSelectedThemeIndex is deprecated and will be removed.
        /// </summary>
        public bool ReportSelectedThemeIndex { get; set; }

        /// <summary>Gets or sets the default palette mode.</summary>
        /// <value>The default palette mode.</value>
        [Category(@"Visuals")]
        [Description(@"The custom assigned palette mode.")]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public KryptonCustomPaletteBase? KryptonCustomPalette 
        {
            get => _kryptonCustomPalette;
            set => _kryptonCustomPalette = value;
        }

        public void ResetKryptonCustomPalette() => _kryptonCustomPalette = null;
        public bool ShouldSerializeKryptonCustomPalette() => _kryptonCustomPalette is not null;

        /// <summary>Gets or sets the default palette mode.</summary>
        /// <value>The default palette mode.</value>
        [Category(@"Visuals")]
        [Description(@"The default palette mode.")]
        [DefaultValue(PaletteMode.Global)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public PaletteMode DefaultPalette {
            get => _defaultPalette;

            set
            {
                // Value needs to be different
                if (_defaultPalette != value)
                {
                    _defaultPalette = value;

                    // Any PaletteMode can be set as a theme, EXCEPT Global.
                    if (value != PaletteMode.Global)
                    {
                        // settings the index triggers OnSelectedIndexChanged()
                        SelectedIndex = CommonHelperThemeSelectors.GetPaletteIndex(Items, _defaultPalette);
                    }
                }
            }
        }

        public void ResetDefaultPalette() => DefaultPalette = PaletteMode.Global;
        public bool ShouldSerializeDefaultPalette() => _defaultPalette != PaletteMode.Global;

        #endregion

        #region Implementation

        /// <summary>
        /// This method will run the KryptonManager.GlobalPaletteChanged event is fired,<br/>
        /// and will synchronize the SelectedIndex with the newly assigned Global Palette.
        /// </summary>
        /// <param name="sender">Object that intiated the call.</param>
        /// <param name="e">Eventargs object data (not used).</param>
        private void KryptonManagerGlobalPaletteChanged(object sender, EventArgs e)
        {
            SelectedIndex = CommonHelperThemeSelectors.KryptonManagerGlobalPaletteChanged(_isLocalUpdate, ref _isExternalUpdate, SelectedIndex, Items);
        }

        #endregion

        #region Protected Overrides

        /// <inheritdoc />
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if ( !CommonHelperThemeSelectors.OnSelectedIndexChanged(ref _isLocalUpdate, _isExternalUpdate, Text, _manager, _kryptonCustomPalette))
            {
                //theme change went wrong, make the active theme the selected theme in the list.
                SelectedIndex = CommonHelperThemeSelectors.GetPaletteIndex(Items, _manager.GlobalPaletteMode);
            }

            base.OnSelectedIndexChanged(e);
        }

        #endregion

        #region Removed Designer Visibility

        /// <summary>
        /// Gets and sets the text associated with the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [AllowNull]
        public override string Text
        {
            //nullable operator removed
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

        /// <summary>Gets and sets the selected index.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int SelectedIndex 
        {
            get => base.SelectedIndex;
            set => base.SelectedIndex = value;
        }

        #endregion
    }
}