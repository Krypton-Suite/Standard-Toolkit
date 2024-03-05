#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    internal partial class VisualThemeBrowserFormRtlAware : KryptonForm
    {
        #region Instance Fields

        private readonly bool _showImportButton;
        private readonly bool _showSilentOption;
        private readonly FormStartPosition _formStartPosition;
        private readonly int _startIndex;
        private readonly string _windowTitle;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="VisualThemeBrowserFormRtlAware" /> class.</summary>
        /// <param name="startPosition">The start position.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="windowTitle">The window title.</param>
        /// <param name="showImportButton">The show import button.</param>
        /// <param name="showSilentOption">The show silent option.</param>
        public VisualThemeBrowserFormRtlAware(FormStartPosition startPosition = FormStartPosition.CenterScreen, int? startIndex = (int)PaletteMode.Microsoft365Blue, string? windowTitle = null, bool? showImportButton = null, bool? showSilentOption = null)
        {
            InitializeComponent();

            _showImportButton = showImportButton ?? false;

            _showSilentOption = showSilentOption ?? false;

            _formStartPosition = startPosition;

            _startIndex = startIndex ?? GlobalStaticValues.GLOBAL_DEFAULT_THEME_INDEX;

            _windowTitle = windowTitle ?? KryptonManager.Strings.MiscellaneousThemeStrings.ThemeBrowserWindowTitle;

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

            klbThemeList.SelectedIndex = _startIndex;

            klblHeader.Text = KryptonManager.Strings.MiscellaneousThemeStrings.ThemeBrowserDescription;

            kbtnImport.Text = KryptonManager.Strings.MiscellaneousThemeStrings.Import;

            kchkSilent.Text = KryptonManager.Strings.MiscellaneousThemeStrings.Silent;

            kbtnCancel.Text = KryptonManager.Strings.GeneralStrings.Cancel;

            kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;
        }

        private void kbtnImport_Click(object sender, EventArgs e) => kcpbCustom.Import(kchkSilent.Checked);

        private void VisualThemeBrowserFormRtlAware_Load(object sender, EventArgs e)
        {
            foreach (var themeName in ThemeManager.SupportedInternalThemeNames)
            {
                if (themeName != null)
                {
                    klbThemeList.Items.Add(themeName);
                }
            }

            klbThemeList.SelectedItem = _startIndex;
        }

        private void klbThemeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(klbThemeList.GetItemText(klbThemeList.SelectedItem)!, new());

            SetIndexText($@"{klbThemeList.GetItemText(klbThemeList.SelectedItem)} - Index: {klbThemeList.SelectedIndex}");
        }

        private void SetIndexText(string value) => klblHeader.Text = value;

        private void kbtnCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

        private void kbtnOk_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

        #endregion
    }
}