#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    public partial class ThemeTest : KryptonForm
    {
        public ThemeTest()
        {
            InitializeComponent();
        }

        private void kbtnKMBTest_Click(object sender, EventArgs e)
        {
            KryptonMessageBox.Show(@"Can you read this text?", @"Test", KryptonMessageBoxButtons.YesNo,
                KryptonMessageBoxIcon.Question);
        }

        private void ktrkProgressValues_ValueChanged(object sender, EventArgs e)
        {
            kryptonProgressBarToolStripItem1.Value = ktrkProgressValues.Value;
            toolStripProgressBar1.Value = ktrkProgressValues.Value;
        }

        private void leftToRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = new KryptonThemeBrowserData()
            {
                ShowImportButton = true,
                ShowSilentOption = true,
                StartIndex = GlobalStaticValues.GLOBAL_DEFAULT_THEME_INDEX,
                StartPosition = FormStartPosition.CenterScreen,
                WindowTitle = KryptonManager.Strings.MiscellaneousThemeStrings.ThemeBrowserWindowTitle
            };

            KryptonThemeBrowser.Show(data);
        }

        private void rightToLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = new KryptonThemeBrowserData()
            {
                ShowImportButton = true,
                ShowSilentOption = true,
                StartIndex = GlobalStaticValues.GLOBAL_DEFAULT_THEME_INDEX,
                StartPosition = FormStartPosition.CenterScreen,
                WindowTitle = KryptonManager.Strings.MiscellaneousThemeStrings.ThemeBrowserWindowTitle
            };

            KryptonThemeBrowser.Show(data, Krypton.Toolkit.RightToLeftLayout.RightToLeft);
        }
    }
}
