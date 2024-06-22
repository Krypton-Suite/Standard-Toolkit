namespace TestForm
{
    public partial class ThemeControlExamples : KryptonForm
    {
        public ThemeControlExamples()
        {
            InitializeComponent();
        }

        private void kbtnThemeBrowser_Click(object sender, EventArgs e)
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
    }
}
