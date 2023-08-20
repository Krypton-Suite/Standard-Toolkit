#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    public partial class KryptonThemeBrowserForm : KryptonForm
    {
        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonThemeBrowserForm" /> class.</summary>
        public KryptonThemeBrowserForm(FormStartPosition? startPosition = FormStartPosition.CenterScreen, int? startIndex = 33)
        {
            InitializeComponent();

            StartPosition = startPosition ?? FormStartPosition.CenterScreen;

            klbThemeList.SelectedItem = startIndex ?? ThemeManager.GetThemeIndex();
        }

        #endregion

        #region Implementation

        private void kbtnImport_Click(object sender, EventArgs e) => kcpbCustom.Import();

        private void KryptonThemeBrowserForm_Load(object sender, EventArgs e)
        {
            foreach (string? themeName in ThemeManager.SupportedInternalThemeNames)
            {
                if (themeName != null)
                {
                    klbThemeList.Items.Add(themeName);
                }
            }
        }

        private void kbtnOK_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

        private void kbtnCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

        private void klbThemeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(klbThemeList.GetItemText(klbThemeList.SelectedItem), new());
        }

        #endregion
    }
}