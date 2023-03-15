#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    public partial class KryptonThemeSelector : KryptonForm
    {
        #region Instance Fields

        private readonly int _themeSelectedIndex;

        private readonly PaletteMode _paletteMode;

        #endregion

        #region Public

        public KryptonThemeComboBox ThemeComboBox => ktcmbSelectedTheme;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonThemeSelector" /> class.</summary>
        /// <param name="themeSelectedIndex">Index of the theme selected.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="header">The header.</param>
        public KryptonThemeSelector(int? themeSelectedIndex, PaletteMode? mode, string? header)
        {
            InitializeComponent();

            _themeSelectedIndex = themeSelectedIndex ?? 33;

            _paletteMode = mode ?? PaletteMode.Microsoft365Blue;

            klblHeader.Text = header ?? @"Choose a theme:";

            kbtnCancel.Text = KryptonManager.Strings.Cancel;

            kbtnOK.Text = KryptonManager.Strings.OK;

            kbtnReset.Text = KryptonManager.Strings.Reset;
        }

        #endregion

        #region Implementation

        private void ktcmbSelectedTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            kbtnReset.Enabled = true;

            kbtnOK.Enabled = true;
        }

        private void kbtnReset_Click(object sender, EventArgs e)
        {
            ktcmbSelectedTheme.SelectedIndex = 33;

            ktcmbSelectedTheme.ResetToDefault();

            kbtnReset.Enabled = false;
        }

        #endregion
    }
}
