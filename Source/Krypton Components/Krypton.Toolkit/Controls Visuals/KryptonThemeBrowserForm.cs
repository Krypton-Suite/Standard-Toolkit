#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    internal partial class KryptonThemeBrowserForm : KryptonForm
    {
        #region Instance Fields

        private readonly bool _showImportButton;
        private readonly bool _showSilentOption;
        private readonly FormStartPosition _formStartPosition;
        private readonly int _startIndex;
        private readonly string _windowTitle;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonThemeBrowserForm" /> class.</summary>
        /// <param name="startPosition">The start position.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="windowTitle">The window title.</param>
        /// <param name="showImportButton">The show import button.</param>
        /// <param name="showSilentOption">The show silent option.</param>
        public KryptonThemeBrowserForm(FormStartPosition startPosition = FormStartPosition.CenterScreen, int? startIndex = (int)PaletteMode.Microsoft365Blue, string? windowTitle = null, bool? showImportButton = null, bool? showSilentOption = null)
        {
            InitializeComponent();

            _showImportButton = showImportButton ?? false;

            _showSilentOption = showSilentOption ?? false;

            _formStartPosition = startPosition;

            _startIndex = startIndex ?? (int)PaletteMode.Microsoft365Blue;

            _windowTitle = windowTitle ?? KryptonManager.Strings.KryptonMiscellaneousThemeStrings.ThemeBrowserWindowTitle;

            AdjustUI();
        }

        #endregion

        #region Implementation

        private void AdjustUI()
        {
            Text = _windowTitle;

            kbtnImport.Visible = _showImportButton;

            kchkSilent.Visible = _showSilentOption;

            StartPosition = _formStartPosition;

            //klbThemeList.SelectedIndex = _startIndex;

            klblDescription.Text = KryptonManager.Strings.KryptonMiscellaneousThemeStrings.ThemeBrowserDescription;

            kbtnImport.Text = KryptonManager.Strings.KryptonMiscellaneousThemeStrings.Import;

            kchkSilent.Text = KryptonManager.Strings.KryptonMiscellaneousThemeStrings.Silent;

            kbtnCancel.Text = KryptonManager.Strings.GeneralStrings.Cancel;

            kbtnOK.Text = KryptonManager.Strings.GeneralStrings.OK;
        }

        private void kbtnImport_Click(object sender, EventArgs e) => kcpbCustom.Import(kchkSilent.Checked);

        private void KryptonThemeBrowserForm_Load(object sender, EventArgs e)
        {
            foreach (string? themeName in ThemeManager.SupportedInternalThemeNames)
            {
                if (themeName != null)
                {
                    klbThemeList.Items.Add(themeName);
                }
            }

            klbThemeList.SelectedItem = _startIndex;
        }

        private void kbtnOK_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

        private void kbtnCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

        private void klbThemeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(klbThemeList.GetItemText(klbThemeList.SelectedItem), new());

            SetIndexText($@"{klbThemeList.GetItemText(klbThemeList.SelectedItem)} - Index: {klbThemeList.SelectedIndex}");
        }

        private void SetIndexText(string v) => klblSelectedIndex.Text = v;

        #endregion
    }
}