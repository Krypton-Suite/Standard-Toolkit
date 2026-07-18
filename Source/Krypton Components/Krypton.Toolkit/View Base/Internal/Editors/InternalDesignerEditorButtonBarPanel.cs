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
    public KryptonComboBox ThemeCombo => kcmbTheme;

    /// <summary>
    /// Gets the host for additional footer buttons (for example Delete).
    /// </summary>
    public FlowLayoutPanel ExtraButtonHost => flpExtraButtons;

    /// <summary>
    /// Gets or sets whether the local theme selector is shown.
    /// </summary>
    [DefaultValue(true)]
    public bool IncludeThemeSelector
    {
        get => kcmbTheme.Visible;
        set => kcmbTheme.Visible = value;
    }

    /// <summary>
    /// Wires the footer theme selector to the owning editor form.
    /// </summary>
    /// <param name="owner">Owning editor form.</param>
    public void WireThemeToForm(KryptonForm owner)
    {
        if (!IncludeThemeSelector)
        {
            return;
        }

        KryptonDesignerEditorTheme.WireThemeSelector(
            kcmbTheme,
            owner,
            owner.PaletteMode,
            owner.LocalCustomPalette);
        LayoutFooterButtons();
    }

    /// <summary>
    /// Applies DPI-aware button sizing for the footer.
    /// </summary>
    /// <param name="owner">Owning editor form.</param>
    public void ApplyDpi(KryptonForm owner)
    {
        var buttonSize = KryptonDesignerEditorDpi.Scale(owner, new Size(112, 28));
        foreach (var button in GetFooterButtons())
        {
            button.AutoSize = false;
            button.Size = buttonSize;
            button.MinimumSize = buttonSize;
        }

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
    #endregion

    #region Implementation
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

    private void LayoutFooterButtons()
    {
        if (ClientSize.Width <= 0)
        {
            return;
        }

        const int padding = 9;
        const int buttonTop = 12;
        const int buttonGap = 6;
        const int themeWidth = 220;

        kcmbTheme.Location = new Point(padding, buttonTop);
        kcmbTheme.Size = new Size(Math.Min(themeWidth, ClientSize.Width / 2), kbtnOk.Height > 0 ? kbtnOk.Height : 28);

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
