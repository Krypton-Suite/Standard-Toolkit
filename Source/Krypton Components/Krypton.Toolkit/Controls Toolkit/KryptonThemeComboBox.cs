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

        private PaletteModeManager _paletteModeManager;
        #endregion

        #region Properties

        public List<string> SupportedThemesList => _supportedThemesList;

        [DefaultValue(22)]
        public int ThemeSelectedIndex
        {
            get => _selectedIndex;

            set => _selectedIndex = value;
        }

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

            ThemeSelectedIndex = 22;

            _supportedThemesList = ThemeManager.PropagateSupportedThemeList();
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
            if (!DesignMode)
            {
                Items.AddRange(ThemeManager.ReturnThemeArray());
            }

            Text = Manager.GlobalPaletteMode.ToString();


            _paletteModeManager = Manager.GlobalPaletteMode;

            base.OnCreateControl();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            ThemeManager.ApplyTheme(Text, Manager);

            ThemeSelectedIndex = SelectedIndex;

            base.OnSelectedIndexChanged(e);
        }

        #endregion
    }
}