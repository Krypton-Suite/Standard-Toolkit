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
/// Creates the standard <see cref="PaletteBackStyle.PanelClient"/> content host used by designer editors.
/// </summary>
public static class KryptonDesignerEditorContentPanel
{
    private const int DesignPadding = 9;

    /// <summary>
    /// Creates a fill-docked content panel with <see cref="PaletteBackStyle.PanelClient"/>.
    /// </summary>
    /// <param name="owner">Owning editor form used for DPI scaling.</param>
    /// <param name="content">Root content control hosted inside the panel.</param>
    /// <returns>Configured content panel.</returns>
    public static KryptonPanel Create(Form owner, Control content)
    {
        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Name = @"kpnlDesignerEditorContent",
            Padding = new Padding(KryptonDesignerEditorDpi.Scale(owner, DesignPadding)),
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        content.Dock = DockStyle.Fill;
        panel.Controls.Add(content);
        return panel;
    }
}
