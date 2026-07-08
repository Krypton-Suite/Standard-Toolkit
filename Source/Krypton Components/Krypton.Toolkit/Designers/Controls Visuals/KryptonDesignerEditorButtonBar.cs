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
/// Creates the standard <see cref="PaletteBackStyle.PanelAlternate"/> footer bar used by designer editors.
/// </summary>
public static class KryptonDesignerEditorButtonBar
{
    private const int DesignHeight = 50;
    private const int DesignPadding = 9;
    private const int DesignButtonTop = 13;
    private const int DesignButtonWidth = 90;
    private const int DesignButtonHeight = 25;
    private const int DesignButtonGap = 6;
    private const int DesignThemeWidth = 220;

    /// <summary>
    /// Creates a bottom-docked button bar with <see cref="PaletteBackStyle.PanelAlternate"/> chrome,
    /// a top-docked <see cref="KryptonBorderEdge"/>, optional local theme selector, and dialog buttons.
    /// </summary>
    /// <param name="owner">Owning editor form used for DPI scaling and theme application.</param>
    /// <param name="okButton">OK / accept button.</param>
    /// <param name="cancelButton">Optional Cancel button.</param>
    /// <param name="extraButtons">Optional additional buttons placed to the left of OK/Cancel.</param>
    /// <returns>Configured button bar panel.</returns>
    public static KryptonPanel Create(
        KryptonForm owner,
        KryptonButton okButton,
        KryptonButton? cancelButton = null,
        params KryptonButton[] extraButtons) =>
        Create(owner, includeThemeSelector: true, okButton, cancelButton, extraButtons);

    /// <summary>
    /// Creates a bottom-docked button bar with <see cref="PaletteBackStyle.PanelAlternate"/> chrome.
    /// </summary>
    /// <param name="owner">Owning editor form used for DPI scaling and theme application.</param>
    /// <param name="includeThemeSelector">
    /// When <c>true</c>, adds a local theme combo that restyles only this editor dialog.
    /// </param>
    /// <param name="okButton">OK / accept button.</param>
    /// <param name="cancelButton">Optional Cancel button.</param>
    /// <param name="extraButtons">Optional additional buttons placed to the left of OK/Cancel.</param>
    /// <returns>Configured button bar panel.</returns>
    public static KryptonPanel Create(
        KryptonForm owner,
        bool includeThemeSelector,
        KryptonButton okButton,
        KryptonButton? cancelButton = null,
        params KryptonButton[] extraButtons)
    {
        var padding = KryptonDesignerEditorDpi.Scale(owner, DesignPadding);
        var buttonTop = KryptonDesignerEditorDpi.Scale(owner, DesignButtonTop);
        var buttonSize = KryptonDesignerEditorDpi.Scale(owner, new Size(DesignButtonWidth, DesignButtonHeight));
        var buttonGap = KryptonDesignerEditorDpi.Scale(owner, DesignButtonGap);
        var panelHeight = KryptonDesignerEditorDpi.Scale(owner, DesignHeight);
        var themeWidth = KryptonDesignerEditorDpi.Scale(owner, DesignThemeWidth);

        var panel = new KryptonPanel
        {
            Dock = DockStyle.Bottom,
            Height = panelHeight,
            PanelBackStyle = PaletteBackStyle.PanelAlternate
        };

        // Top separator matching VisualMultilineStringEditorForm / toast footers.
        var edge = new KryptonBorderEdge
        {
            BorderStyle = PaletteBorderStyle.HeaderPrimary,
            Dock = DockStyle.Top,
            Orientation = Orientation.Horizontal,
            Name = @"kbDesignerEditorButtonBarEdge"
        };

        KryptonComboBox? themeCombo = null;
        if (includeThemeSelector)
        {
            themeCombo = KryptonDesignerEditorTheme.CreateThemeSelector(
                owner,
                owner.PaletteMode,
                owner.LocalCustomPalette);
            themeCombo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            themeCombo.Size = new Size(themeWidth, buttonSize.Height);
            themeCombo.Location = new Point(padding, buttonTop);
            panel.Controls.Add(themeCombo);
        }

        var buttons = new List<KryptonButton>();
        if (extraButtons is { Length: > 0 })
        {
            buttons.AddRange(extraButtons);
        }

        buttons.Add(okButton);
        if (cancelButton is not null)
        {
            buttons.Add(cancelButton);
        }

        // Layout right-to-left: Cancel is rightmost when present, then OK, then extras.
        var right = padding;
        for (var i = buttons.Count - 1; i >= 0; i--)
        {
            var button = buttons[i];
            ConfigureDialogButton(button, buttonSize);
            button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button.Location = new Point(panel.Width - right - buttonSize.Width, buttonTop);
            panel.Controls.Add(button);
            right += buttonSize.Width + buttonGap;
        }

        // Added last so docking places the separator at the top of the panel
        // (same pattern as VisualMultilineStringEditorForm).
        panel.Controls.Add(edge);
        panel.Resize += (_, _) =>
        {
            if (themeCombo is not null)
            {
                themeCombo.Location = new Point(padding, buttonTop);
                themeCombo.Size = new Size(themeWidth, buttonSize.Height);
            }

            LayoutButtons(panel, buttons, padding, buttonTop, buttonSize, buttonGap);
        };

        return panel;
    }

    private static void ConfigureDialogButton(KryptonButton button, Size buttonSize)
    {
        button.AutoSize = false;
        button.Size = buttonSize;
        button.MinimumSize = buttonSize;
    }

    private static void LayoutButtons(
        KryptonPanel panel,
        IReadOnlyList<KryptonButton> buttons,
        int padding,
        int buttonTop,
        Size buttonSize,
        int buttonGap)
    {
        var right = padding;
        for (var i = buttons.Count - 1; i >= 0; i--)
        {
            var button = buttons[i];
            button.Size = buttonSize;
            button.Location = new Point(panel.ClientSize.Width - right - buttonSize.Width, buttonTop);
            right += buttonSize.Width + buttonGap;
        }
    }
}
