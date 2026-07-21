#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Shared settings dialog for designer-editor theme preferences.
/// </summary>
internal sealed partial class VisualDesignerEditorSettingsForm : KryptonForm
{
    #region Instance Fields
    private bool _clearPreference;
    #endregion

    #region Identity
    public VisualDesignerEditorSettingsForm()
    {
        SetInheritedControlOverride();
        InitializeComponent();
        ApplyLocalizedText();
        ApplyDpiLayout();

        kcmbTheme.Items.AddRange(CommonHelperThemeSelectors.GetThemesArray());
        LoadCurrentSelection();
        ApplyPreviewTheme();
        kcmbTheme.SelectedIndexChanged += (_, _) => ApplyPreviewTheme();
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);

        if (DialogResult != DialogResult.OK)
        {
            return;
        }

        if (_clearPreference)
        {
            KryptonDesignerEditorThemePreferences.ClearPreferredPaletteMode();
            return;
        }

        if (kcmbTheme.SelectedItem is not string themeName || themeName.Length == 0)
        {
            return;
        }

        var mode = ThemeManager.GetThemeManagerMode(themeName);
        if (mode != PaletteMode.Custom)
        {
            KryptonDesignerEditorThemePreferences.SavePreferredPaletteMode(mode);
        }
    }
    #endregion

    #region Implementation
    private void ApplyLocalizedText()
    {
        var strings = KryptonManager.Strings.EditorSettingStrings;
        var general = KryptonManager.Strings.GeneralStrings;

        Text = strings.DesignerEditorSettingsTitle;
        lblTheme.Values.Text = strings.DesignerEditorSettingsThemeLabel;
        lblDescription.Text = strings.DesignerEditorSettingsDescription;
        kbtnClear.Values.Text = strings.DesignerEditorSettingsClearPreference;
        kbtnOpenFolder.Values.Text = strings.DesignerEditorSettingsOpenFolder;
        kbtnOk.Values.Text = general.OK;
        kbtnCancel.Values.Text = general.Cancel;
    }

    private void ApplyDpiLayout()
    {
        var clientSize = KryptonDesignerEditorDpi.Scale(this, new Size(584, 256));
        ClientSize = clientSize;
        MinimumSize = clientSize;
        MaximumSize = new Size(clientSize.Width + 1, clientSize.Height + 1);

        var padding = KryptonDesignerEditorDpi.Scale(this, 16);
        var rowGap = KryptonDesignerEditorDpi.Scale(this, 12);
        var buttonGap = KryptonDesignerEditorDpi.Scale(this, 8);
        var buttonWidth = KryptonDesignerEditorDpi.Scale(this, 104);
        var buttonHeight = KryptonDesignerEditorDpi.Scale(this, 28);
        var clearWidth = KryptonDesignerEditorDpi.Scale(this, 140);
        var openWidth = KryptonDesignerEditorDpi.Scale(this, 130);
        var contentWidth = clientSize.Width - (padding * 2);

        lblTheme.Location = new Point(padding, padding);
        lblTheme.Size = new Size(contentWidth, KryptonDesignerEditorDpi.Scale(this, 20));

        kcmbTheme.Location = new Point(padding, lblTheme.Bottom + rowGap);
        kcmbTheme.Size = new Size(contentWidth, buttonHeight);

        lblDescription.Location = new Point(padding, kcmbTheme.Bottom + rowGap);
        lblDescription.Size = new Size(contentWidth, KryptonDesignerEditorDpi.Scale(this, 56));

        kbtnClear.Location = new Point(padding, clientSize.Height - padding - buttonHeight);
        kbtnClear.Size = new Size(clearWidth, buttonHeight);

        kbtnOpenFolder.Location = new Point(kbtnClear.Right + buttonGap, kbtnClear.Top);
        kbtnOpenFolder.Size = new Size(openWidth, buttonHeight);

        kbtnCancel.Location = new Point(clientSize.Width - padding - buttonWidth, kbtnClear.Top);
        kbtnCancel.Size = new Size(buttonWidth, buttonHeight);

        kbtnOk.Location = new Point(kbtnCancel.Left - buttonGap - buttonWidth, kbtnClear.Top);
        kbtnOk.Size = new Size(buttonWidth, buttonHeight);
    }

    private void LoadCurrentSelection()
    {
        var mode = KryptonManager.CurrentGlobalPaletteMode;
        if (KryptonDesignerEditorThemePreferences.TryGetPreferredPaletteMode(out var preferred))
        {
            mode = preferred == PaletteMode.Global
                ? KryptonManager.CurrentGlobalPaletteMode
                : preferred;
        }

        var index = CommonHelperThemeSelectors.GetPaletteIndex(kcmbTheme.Items, mode);
        if (index >= 0 && index < kcmbTheme.Items.Count)
        {
            kcmbTheme.SelectedIndex = index;
        }
        else if (kcmbTheme.Items.Count > 0)
        {
            kcmbTheme.SelectedIndex = 0;
        }
    }

    private void ApplyPreviewTheme()
    {
        if (kcmbTheme.SelectedItem is not string themeName || themeName.Length == 0)
        {
            return;
        }

        var mode = ThemeManager.GetThemeManagerMode(themeName);
        if (mode == PaletteMode.Custom)
        {
            return;
        }

        if (mode == PaletteMode.Global)
        {
            KryptonDesignerEditorTheme.ApplyToForm(this, KryptonManager.CurrentGlobalPaletteMode,
                KryptonManager.CurrentGlobalPalette as KryptonCustomPaletteBase);
            return;
        }

        KryptonDesignerEditorTheme.ApplyToForm(this, mode, null);
    }

    private void OnClearPreference(object? sender, EventArgs e)
    {
        _clearPreference = true;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void OnOpenFolder(object? sender, EventArgs e) =>
        KryptonDesignerEditorThemePreferences.OpenSettingsInExplorer();
    #endregion
}
