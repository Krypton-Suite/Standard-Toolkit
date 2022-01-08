#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
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

        public KryptonThemeComboBox()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;

            ThemeSelectedIndex = 22;

            _supportedThemesList = ThemeManager.PropagateSupportedThemeList();
        }
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