#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class VisualThemeBrowserFormRtlAware : KryptonForm
{
    #region Instance Fields

    private readonly KryptonThemeBrowserData _themeBrowserData;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="VisualThemeBrowserFormRtlAware" /> class.</summary>
    /// <param name="themeBrowserData">The data to create the <see cref="VisualThemeBrowserFormRtlAware"/> UI.</param>
    public VisualThemeBrowserFormRtlAware(KryptonThemeBrowserData themeBrowserData)
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        InitializeComponent();

        _themeBrowserData = themeBrowserData;

        AdjustUI();
    }

    #endregion

    #region Implementation

    private void AdjustUI()
    {
        Text = _themeBrowserData.WindowTitle ?? KryptonManager.Strings.MiscellaneousThemeStrings.ThemeBrowserWindowTitle;

        kbtnImport.Visible = _themeBrowserData.ShowImportButton ?? false;

        kchkSilent.Visible = _themeBrowserData.ShowSilentOption ?? false;

        StartPosition = _themeBrowserData.StartPosition ?? FormStartPosition.CenterScreen;

        if (klbThemeList.Items.Count > 0)
        {
            klbThemeList.SelectedIndex = _themeBrowserData.StartIndex ?? GlobalStaticValues.GLOBAL_DEFAULT_THEME_INDEX;
        }

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

        klbThemeList.SelectedItem = _themeBrowserData.StartIndex;
    }

    private void klbThemeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ThemeManager.ApplyTheme(klbThemeList.GetItemText(klbThemeList.SelectedItem)!, new KryptonManager());

        SetIndexText($@"{klbThemeList.GetItemText(klbThemeList.SelectedItem)} - Index: {klbThemeList.SelectedIndex}");
    }

    private void SetIndexText(string value) => klblHeader.Text = value;

    private void kbtnCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

    private void kbtnOk_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

    #endregion
}