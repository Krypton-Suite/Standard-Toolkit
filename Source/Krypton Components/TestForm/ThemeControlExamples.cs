#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

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