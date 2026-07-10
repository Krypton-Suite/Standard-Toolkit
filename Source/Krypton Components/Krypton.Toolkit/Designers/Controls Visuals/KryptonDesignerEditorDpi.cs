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
/// Applies consistent DPI scaling to Krypton designer editor dialogs.
/// </summary>
/// <remarks>
/// Use these helpers when building a custom <see cref="VisualDesignerCollectionForm"/> so
/// sizes match other toolkit designer dialogs across DPI settings.
/// </remarks>
public static class KryptonDesignerEditorDpi
{
    private const float DesignDpi = 96f;

    /// <summary>
    /// Configures a designer editor form to scale from a 96 DPI design baseline.
    /// </summary>
    /// <param name="form">Editor form.</param>
    public static void Configure(Form form)
    {
        form.AutoScaleDimensions = new SizeF(DesignDpi, DesignDpi);
        form.AutoScaleMode = AutoScaleMode.Dpi;
    }

    /// <summary>
    /// Refreshes layout after the editor is shown on a possibly high-DPI display.
    /// </summary>
    /// <param name="form">Editor form.</param>
    public static void ApplyOnShown(Form form)
    {
        KryptonManager.InvalidateDpiCache();

        RefreshPropertyGrids(form.Controls);
        form.PerformLayout();
    }

    /// <summary>
    /// Gets the DPI scale factor for the editor form's window.
    /// </summary>
    /// <param name="form">Editor form.</param>
    /// <returns>DPI scale factor relative to 96 DPI.</returns>
    public static float GetDpiFactor(Form form) =>
        form.IsHandleCreated ? KryptonManager.GetDpiFactor(form.Handle) : KryptonManager.GetDpiFactor();

    /// <summary>
    /// Scales a design-time pixel measurement for the editor form's current DPI.
    /// </summary>
    /// <param name="form">Editor form.</param>
    /// <param name="designPixels">Value authored at 96 DPI.</param>
    /// <returns>Scaled pixel value.</returns>
    public static int Scale(Form form, int designPixels) =>
        (int)Math.Round(designPixels * GetDpiFactor(form));

    /// <summary>
    /// Scales a design-time <see cref="Size"/> for the editor form's current DPI.
    /// </summary>
    /// <param name="form">Editor form.</param>
    /// <param name="designSize">Size authored at 96 DPI.</param>
    /// <returns>Scaled size.</returns>
    public static Size Scale(Form form, Size designSize)
    {
        var factor = GetDpiFactor(form);
        return new Size(
            (int)Math.Round(designSize.Width * factor),
            (int)Math.Round(designSize.Height * factor));
    }

    private static void RefreshPropertyGrids(Control.ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            if (control is KryptonPropertyGrid propertyGrid)
            {
                propertyGrid.PerformLayout();
                propertyGrid.Invalidate(true);
            }

            if (control.HasChildren)
            {
                RefreshPropertyGrids(control.Controls);
            }
        }
    }
}
