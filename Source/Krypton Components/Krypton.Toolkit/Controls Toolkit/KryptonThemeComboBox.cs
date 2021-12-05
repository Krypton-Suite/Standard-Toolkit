﻿#region BSD License
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
        private List<string> _supportedThemesList;

        private int _selectedIndex;

        private KryptonManager _manager;

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

        #endregion

        #region Constructor

        public KryptonThemeComboBox()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;

            ThemeSelectedIndex = 22;

            ThemeManager.PropagateSupportedThemeList(_supportedThemesList);

            try
            {
               
            }
            catch (Exception e)
            {
                ExceptionHandler.CaptureException(e);
                throw;
            }

            

            //_paletteMode = _manager.GlobalPaletteMode;
        }
        #endregion

        #region Protected Overrides

        protected override void OnCreateControl()
        {
            if (!DesignMode)
            {
                Items.AddRange(ThemeManager.ReturnThemeArray());
            }

            if (_manager == null)
            {
                _manager = new KryptonManager();
            }

            Text = _manager.GlobalPaletteMode.ToString();


            _paletteModeManager = _manager.GlobalPaletteMode;

            base.OnCreateControl();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            ThemeManager.ApplyTheme(Text, _manager);

            ThemeSelectedIndex = SelectedIndex;

            base.OnSelectedIndexChanged(e);
        }

        #endregion
    }
}