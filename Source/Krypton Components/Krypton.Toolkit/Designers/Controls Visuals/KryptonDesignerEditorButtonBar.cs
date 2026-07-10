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
internal static class KryptonDesignerEditorButtonBar
{
    private const int DesignHeight = 50;
    private const int DesignPadding = 9;
    private const int DesignButtonTop = 13;
    private const int DesignButtonWidth = 112;
    private const int DesignButtonHeight = 28;
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
    public static InternalDesignerEditorButtonBarPanel Create(
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
    public static InternalDesignerEditorButtonBarPanel Create(
        KryptonForm owner,
        bool includeThemeSelector,
        KryptonButton okButton,
        KryptonButton? cancelButton = null,
        params KryptonButton[] extraButtons)
    {
        var panel = new InternalDesignerEditorButtonBarPanel
        {
            IncludeThemeSelector = includeThemeSelector
        };

        TransferButton(panel.OkButton, okButton);
        if (cancelButton is not null)
        {
            TransferButton(panel.CancelButton, cancelButton);
        }
        else
        {
            panel.CancelButton.Visible = false;
        }

        foreach (var extraButton in extraButtons)
        {
            panel.ExtraButtonHost.Controls.Add(extraButton);
        }

        panel.ApplyDpi(owner);
        panel.WireThemeToForm(owner);
        return panel;
    }

    private static void TransferButton(KryptonButton target, KryptonButton source)
    {
        target.DialogResult = source.DialogResult;
        target.Name = string.IsNullOrEmpty(source.Name) ? target.Name : source.Name;
        target.TabIndex = source.TabIndex;
        target.Values.Text = source.Values.Text;
        target.Visible = source.Visible;
        target.Enabled = source.Enabled;
        target.Click += (_, _) => source.PerformClick();
    }
}
