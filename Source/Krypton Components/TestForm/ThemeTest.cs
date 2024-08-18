#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

using System.IO;

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

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            // Note: For ExceptionHandler testing

            //try
            //{
            //    throw new ArgumentOutOfRangeException();
            //}
            //catch (Exception ex)
            //{
            //    ExceptionHandler.CaptureException(ex, showStackTrace: true);
            //}
        }

        private void bsaBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Title = "Open a custom theme:",
                Filter = "XML Files|*.xml"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                kryptonTextBox1.Text = Path.GetFullPath(dlg.FileName);
            }
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            kryptonButton2.Enabled = !string.IsNullOrEmpty(kryptonTextBox1.Text);
            kbtnCustomTheme.Enabled = !string.IsNullOrEmpty(kryptonTextBox1.Text);
        }

        private void kbtnCustomTheme_Click(object sender, EventArgs e)
        {
            string themePath = kryptonTextBox1.Text;

            KryptonCustomPaletteBase palette = new KryptonCustomPaletteBase();

            using (Stream stream = new FileStream(themePath, FileMode.Open))
            {
                palette.ImportWithUpgrade(stream);
            }

            //KryptonManager.CurrentGlobalPalette = palette;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            kryptonCustomPaletteBase1.ImportWithUpgrade(kryptonTextBox1.Text);
        }
    }
}
