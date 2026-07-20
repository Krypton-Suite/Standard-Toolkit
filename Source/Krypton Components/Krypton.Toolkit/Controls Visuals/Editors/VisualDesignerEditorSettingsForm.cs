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
internal sealed class VisualDesignerEditorSettingsForm : KryptonForm
{
    #region Instance Fields
    private readonly KryptonLabel _lblTheme;
    private readonly KryptonComboBox _kcmbTheme;
    private readonly KryptonLabel _lblDescription;
    private readonly KryptonButton _kbtnClear;
    private readonly KryptonButton _kbtnOk;
    private readonly KryptonButton _kbtnCancel;
    private bool _clearPreference;
    #endregion

    #region Identity
    public VisualDesignerEditorSettingsForm()
    {
        SetInheritedControlOverride();

        var strings = KryptonManager.Strings.EditorSettingStrings;
        var general = KryptonManager.Strings.GeneralStrings;

        Text = strings.DesignerEditorSettingsTitle;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        ShowInTaskbar = false;
        ShowIcon = false;
        MaximizeBox = false;
        MinimizeBox = false;
        ControlBox = true;

        var clientSize = KryptonDesignerEditorDpi.Scale(this, new Size(420, 210));
        ClientSize = clientSize;
        MinimumSize = clientSize;
        MaximumSize = new Size(clientSize.Width + 1, clientSize.Height + 1);

        var padding = KryptonDesignerEditorDpi.Scale(this, 12);
        var rowGap = KryptonDesignerEditorDpi.Scale(this, 8);
        var buttonWidth = KryptonDesignerEditorDpi.Scale(this, 100);
        var buttonHeight = KryptonDesignerEditorDpi.Scale(this, 28);
        var contentWidth = clientSize.Width - (padding * 2);

        _lblTheme = new KryptonLabel
        {
            Location = new Point(padding, padding),
            Size = new Size(contentWidth, KryptonDesignerEditorDpi.Scale(this, 20)),
            Values = { Text = strings.DesignerEditorSettingsThemeLabel }
        };

        _kcmbTheme = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(padding, _lblTheme.Bottom + rowGap),
            Size = new Size(contentWidth, buttonHeight)
        };
        _kcmbTheme.Items.AddRange(CommonHelperThemeSelectors.GetThemesArray());

        _lblDescription = new KryptonLabel
        {
            Location = new Point(padding, _kcmbTheme.Bottom + rowGap),
            Size = new Size(contentWidth, KryptonDesignerEditorDpi.Scale(this, 48)),
            Values = { Text = strings.DesignerEditorSettingsDescription }
        };

        _kbtnClear = new KryptonButton
        {
            Location = new Point(padding, clientSize.Height - padding - buttonHeight),
            Size = new Size(KryptonDesignerEditorDpi.Scale(this, 140), buttonHeight),
            Values = { Text = strings.DesignerEditorSettingsClearPreference }
        };
        _kbtnClear.Click += OnClearPreference;

        _kbtnCancel = new KryptonButton
        {
            DialogResult = DialogResult.Cancel,
            Location = new Point(clientSize.Width - padding - buttonWidth, _kbtnClear.Top),
            Size = new Size(buttonWidth, buttonHeight),
            Values = { Text = general.Cancel }
        };

        _kbtnOk = new KryptonButton
        {
            DialogResult = DialogResult.OK,
            Location = new Point(_kbtnCancel.Left - rowGap - buttonWidth, _kbtnClear.Top),
            Size = new Size(buttonWidth, buttonHeight),
            Values = { Text = general.OK }
        };

        Controls.Add(_lblTheme);
        Controls.Add(_kcmbTheme);
        Controls.Add(_lblDescription);
        Controls.Add(_kbtnClear);
        Controls.Add(_kbtnOk);
        Controls.Add(_kbtnCancel);

        AcceptButton = _kbtnOk;
        CancelButton = _kbtnCancel;

        LoadCurrentSelection();
        ApplyPreviewTheme();
        _kcmbTheme.SelectedIndexChanged += (_, _) => ApplyPreviewTheme();
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

        if (_kcmbTheme.SelectedItem is not string themeName || themeName.Length == 0)
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
    private void LoadCurrentSelection()
    {
        var mode = KryptonManager.CurrentGlobalPaletteMode;
        if (KryptonDesignerEditorThemePreferences.TryGetPreferredPaletteMode(out var preferred))
        {
            mode = preferred == PaletteMode.Global
                ? KryptonManager.CurrentGlobalPaletteMode
                : preferred;
        }

        var index = CommonHelperThemeSelectors.GetPaletteIndex(_kcmbTheme.Items, mode);
        if (index >= 0 && index < _kcmbTheme.Items.Count)
        {
            _kcmbTheme.SelectedIndex = index;
        }
        else if (_kcmbTheme.Items.Count > 0)
        {
            _kcmbTheme.SelectedIndex = 0;
        }
    }

    private void ApplyPreviewTheme()
    {
        if (_kcmbTheme.SelectedItem is not string themeName || themeName.Length == 0)
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
    #endregion
}
