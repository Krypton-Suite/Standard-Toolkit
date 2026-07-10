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
/// Shared helpers for designer-editor form chrome.
/// </summary>
internal static class InternalDesignerEditorFormChrome
{
    /// <summary>
    /// Creates a fill-docked <see cref="PaletteBackStyle.PanelClient"/> content host.
    /// </summary>
    /// <param name="content">Root content control.</param>
    /// <returns>Configured content panel.</returns>
    public static KryptonPanel CreateContentHost(Control content)
    {
        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Name = @"kpnlDesignerEditorContent",
            Padding = new Padding(9),
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        content.Dock = DockStyle.Fill;
        panel.Controls.Add(content);
        return panel;
    }

    /// <summary>
    /// Applies footer chrome after designer initialization.
    /// </summary>
    /// <param name="form">Owning editor form.</param>
    /// <param name="contentHost">Content host panel.</param>
    /// <param name="buttonBar">Footer button bar panel.</param>
    public static void Apply(
        KryptonForm form,
        KryptonPanel contentHost,
        InternalDesignerEditorButtonBarPanel buttonBar)
    {
        buttonBar.ApplyDpi(form);
        buttonBar.WireThemeToForm(form);
        form.AcceptButton = buttonBar.OkButton;
        form.CancelButton = buttonBar.CancelButton;
    }
}
