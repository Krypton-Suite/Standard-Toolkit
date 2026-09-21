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
/// Standard designer-editor footer with separator, optional theme selector, and dialog buttons.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
internal partial class InternalDesignerEditorButtonBarPanel : KryptonPanel
{
    #region Instance Fields
    private KryptonForm? _ownerForm;
    private bool _themeSettingsWired;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="InternalDesignerEditorButtonBarPanel"/> class.
    /// </summary>
    public InternalDesignerEditorButtonBarPanel()
    {
        InitializeComponent();
        PanelBackStyle = PaletteBackStyle.PanelAlternate;
        Dock = DockStyle.Bottom;
        Height = 52;
        kbtnThemeSettings.Values.Text = KryptonManager.Strings.EditorSettingStrings.DesignerEditorThemeButtonText;
        kbtnThemeSettings.Values.Image = GenericImageResources.Settings_16_x_16;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the OK button hosted in the footer.
    /// </summary>
    public KryptonButton OkButton => kbtnOk;

    /// <summary>
    /// Gets the Cancel button hosted in the footer.
    /// </summary>
    public KryptonButton CancelButton => kbtnCancel;

    /// <summary>
    /// Gets the local theme selector combo box.
    /// </summary>
    public KryptonThemeComboBox ThemeCombo => kcmbTheme;

    /// <summary>
    /// Gets the compact theme settings button used when the combo is hidden.
    /// </summary>
    public KryptonButton ThemeSettingsButton => kbtnThemeSettings;

    /// <summary>
    /// Gets the host for additional footer buttons (for example Delete).
    /// </summary>
    public FlowLayoutPanel ExtraButtonHost => flpExtraButtons;

    /// <summary>
    /// Gets or sets whether the local theme selector is shown.
    /// </summary>
    /// <remarks>
    /// When <c>false</c> (Visual Studio 2022), a compact <c>Theme...</c> button is shown instead
    /// so users can still open the shared designer-editor settings dialog.
    /// </remarks>
    [DefaultValue(true)]
    public bool IncludeThemeSelector
    {
        get => kcmbTheme.Visible;
        set
        {
            kcmbTheme.Visible = value;
            kbtnThemeSettings.Visible = !value;
        }
    }

    /// <summary>
    /// Wires the footer theme selector to the owning editor form.
    /// </summary>
    /// <param name="owner">Owning editor form.</param>
    public void WireThemeToForm(KryptonForm owner)
    {
        _ownerForm = owner;

        if (IncludeThemeSelector)
        {
            KryptonDesignerEditorTheme.WireThemeSelector(
                kcmbTheme,
                owner,
                owner.PaletteMode,
                owner.LocalCustomPalette);
        }
        else
        {
            WireThemeSettingsButton(owner);
        }

        LayoutFooterButtons();
    }

    /// <summary>
    /// Applies DPI-aware button sizing for the footer.
    /// </summary>
    /// <param name="owner">Owning editor form.</param>
    public void ApplyDpi(KryptonForm owner)
    {
        _ownerForm = owner;
        var buttonSize = KryptonDesignerEditorDpi.Scale(owner, new Size(112, 28));
        foreach (var button in GetFooterButtons())
        {
            button.AutoSize = false;
            button.Size = buttonSize;
            button.MinimumSize = buttonSize;
        }

        var settingsSize = KryptonDesignerEditorDpi.Scale(owner, new Size(90, 28));
        kbtnThemeSettings.AutoSize = false;
        kbtnThemeSettings.Size = settingsSize;
        kbtnThemeSettings.MinimumSize = settingsSize;

        Height = KryptonDesignerEditorDpi.Scale(owner, 52);
        LayoutFooterButtons();
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        LayoutFooterButtons();
    }

    /// <inheritdoc />
    protected override void OnParentChanged(EventArgs e)
    {
        base.OnParentChanged(e);
        LayoutFooterButtons();
    }
    #endregion

    #region Implementation
    private int ScaleDesign(int designPixels) =>
        _ownerForm is not null ? KryptonDesignerEditorDpi.Scale(_ownerForm, designPixels) : designPixels;

    private IEnumerable<KryptonButton> GetFooterButtons()
    {
        yield return kbtnOk;
        yield return kbtnCancel;
        foreach (Control control in flpExtraButtons.Controls)
        {
            if (control is KryptonButton button)
            {
                yield return button;
            }
        }
    }

    private void WireThemeSettingsButton(KryptonForm owner)
    {
        if (_themeSettingsWired)
        {
            return;
        }

        kbtnThemeSettings.Click += (_, _) =>
            KryptonDesignerEditorTheme.ShowSettingsDialog(owner, owner);
        _themeSettingsWired = true;
    }

    private void LayoutFooterButtons()
    {
        if (ClientSize.Width <= 0)
        {
            return;
        }

        var padding = ScaleDesign(9);
        var buttonTop = ScaleDesign(12);
        var buttonGap = ScaleDesign(6);
        var themeWidth = ScaleDesign(220);
        var buttonHeight = kbtnOk.Height > 0 ? kbtnOk.Height : ScaleDesign(28);

        if (IncludeThemeSelector)
        {
            kcmbTheme.Location = new Point(padding, buttonTop);
            kcmbTheme.Size = new Size(Math.Min(themeWidth, Math.Max(buttonHeight, ClientSize.Width / 2)), buttonHeight);
        }
        else if (kbtnThemeSettings.Visible)
        {
            kbtnThemeSettings.Location = new Point(padding, buttonTop);
        }

        var right = padding;
        var buttons = new List<KryptonButton> { kbtnCancel, kbtnOk };
        foreach (Control control in flpExtraButtons.Controls)
        {
            if (control is KryptonButton extra)
            {
                buttons.Insert(0, extra);
            }
        }

        foreach (var button in buttons)
        {
            button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button.Location = new Point(ClientSize.Width - right - button.Width, buttonTop);
            right += button.Width + buttonGap;
        }
    }
    #endregion
}
