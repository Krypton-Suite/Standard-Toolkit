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
        private readonly List<string> _supportedThemesList;
        private int _selectedIndex;
        #endregion

        #region Properties

        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<string> SupportedThemesList => _supportedThemesList;

        /// <summary>
        /// Gets and sets the ThemeSelectedIndex.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Theme Selected Index. (Default = `Office 365 - Blue`)")]
        [DefaultValue(25)]
        public int ThemeSelectedIndex
        {
            get => _selectedIndex;

            set => SelectedIndex = value;
        }

        private void ResetThemeSelectedIndex() => _selectedIndex = 25;

        private bool ShouldSerializeThemeSelectedIndex() => _selectedIndex != 25;


        [EditorBrowsable(EditorBrowsableState.Never)]
        public KryptonManager Manager
        {
            get;

        } = new KryptonManager();

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="KryptonThemeComboBox" /> class.</summary>
        public KryptonThemeComboBox()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;

            _supportedThemesList = ThemeManager.PropagateSupportedThemeList();
            _selectedIndex = 25;
        }
        #endregion

        #region Methods

        /// <summary>Returns the palette mode.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public PaletteMode ReturnPaletteMode() => (PaletteMode)Manager.GlobalPaletteMode;

        #endregion

        #region Protected Overrides

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Items.AddRange(ThemeManager.ReturnThemeArray());
            SelectedIndex = _selectedIndex;
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            ThemeManager.ApplyTheme(Text, Manager);

            ThemeSelectedIndex = SelectedIndex;

            base.OnSelectedIndexChanged(e);
        }

        #endregion

        #region Removed Designer visibility
        /// <summary>
        /// Gets and sets the text associated associated with the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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