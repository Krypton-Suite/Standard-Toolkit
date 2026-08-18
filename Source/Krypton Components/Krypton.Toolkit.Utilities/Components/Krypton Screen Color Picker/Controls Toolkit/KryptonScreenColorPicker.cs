#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// PowerToys-style screen colour picker: live-refresh the desktop, magnify pixels under the cursor, and click to sample.
/// </summary>
/// <remarks>
/// Inspired by the PowerToys Color Picker overlay (live capture, zoomed pixel grid, hex/RGB readout).
/// Click samples, Esc or right-click cancels, mouse wheel changes zoom, Ctrl+wheel changes magnifier size.
/// When an owner window is supplied it is made fully transparent so colours behind the dialog can be picked.
/// The magnifier flyout defaults to themed Krypton chrome; Classic painted PowerToys chrome is still available.
/// </remarks>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonScreenColorPicker
{
    /// <summary>
    /// Smallest odd number of source pixels shown in the magnifier flyout.
    /// </summary>
    public const int MinimumMagnifierSize = 7;

    /// <summary>
    /// Largest odd number of source pixels shown in the magnifier flyout.
    /// </summary>
    public const int MaximumMagnifierSize = 21;

    /// <summary>
    /// Smallest pixel zoom for the magnifier.
    /// </summary>
    public const int MinimumZoom = 6;

    /// <summary>
    /// Largest pixel zoom for the magnifier.
    /// </summary>
    public const int MaximumZoom = 24;

    private static int _defaultMagnifierSize = 11;
    private static int _defaultZoom = 12;

    /// <summary>
    /// Flyout chrome used when <see cref="TryPick(IWin32Window?, out Color)"/> does not specify a style.
    /// Defaults to <see cref="KryptonScreenColorPickerFlyoutStyle.Krypton"/>.
    /// </summary>
    public static KryptonScreenColorPickerFlyoutStyle DefaultFlyoutStyle { get; set; } =
        KryptonScreenColorPickerFlyoutStyle.Krypton;

    /// <summary>
    /// Odd number of source pixels shown in the magnifier when a pick starts (7–21).
    /// Updated to the last size used when a pick session ends.
    /// </summary>
    public static int DefaultMagnifierSize
    {
        get => _defaultMagnifierSize;
        set => _defaultMagnifierSize = ClampMagnifierSize(value);
    }

    /// <summary>
    /// Pixel zoom used when a pick starts (6–24).
    /// Updated to the last zoom used when a pick session ends.
    /// </summary>
    public static int DefaultZoom
    {
        get => _defaultZoom;
        set => _defaultZoom = ClampZoom(value);
    }

    /// <summary>
    /// Clamps <paramref name="size"/> to an odd value between <see cref="MinimumMagnifierSize"/> and <see cref="MaximumMagnifierSize"/>.
    /// </summary>
    /// <param name="size">Requested source-pixel count.</param>
    /// <returns>An odd size in range.</returns>
    public static int ClampMagnifierSize(int size)
    {
        int clamped = Math.Max(MinimumMagnifierSize, Math.Min(MaximumMagnifierSize, size));
        return (clamped & 1) == 0 ? clamped - 1 : clamped;
    }

    /// <summary>
    /// Clamps <paramref name="zoom"/> to <see cref="MinimumZoom"/>–<see cref="MaximumZoom"/>.
    /// </summary>
    /// <param name="zoom">Requested pixel zoom.</param>
    /// <returns>A zoom in range.</returns>
    public static int ClampZoom(int zoom) =>
        Math.Max(MinimumZoom, Math.Min(MaximumZoom, zoom));

    /// <summary>
    /// Creates a 16 by 16 eyedropper image suitable for a screen-picker button.
    /// </summary>
    /// <returns>A disposable image for use with button image values.</returns>
    public static Image CreateDropperGlyphImage() => ScreenColorPickerGlyph.Create();

    /// <summary>
    /// Display name for a flyout style, suitable for a combo box.
    /// </summary>
    /// <param name="style">Flyout chrome.</param>
    /// <returns>A short label.</returns>
    public static string GetFlyoutStyleDisplayName(KryptonScreenColorPickerFlyoutStyle style) =>
        style == KryptonScreenColorPickerFlyoutStyle.Krypton
            ? @"Krypton"
            : @"Classic (PowerToys)";

    /// <summary>
    /// Captures a colour from the screen. Returns <c>false</c> when the user cancels.
    /// </summary>
    /// <param name="color">The sampled colour when the method returns <c>true</c>.</param>
    /// <returns><c>true</c> when a colour was picked.</returns>
    public static bool TryPick(out Color color) => TryPick(null, DefaultFlyoutStyle, out color);

    /// <summary>
    /// Captures a colour from the screen. Makes <paramref name="owner"/> fully transparent while taking the snapshot when it is a visible form.
    /// </summary>
    /// <param name="owner">Owner window. May be null.</param>
    /// <param name="color">The sampled colour when the method returns <c>true</c>.</param>
    /// <returns><c>true</c> when a colour was picked.</returns>
    public static bool TryPick(IWin32Window? owner, out Color color) =>
        TryPick(owner, DefaultFlyoutStyle, out color);

    /// <summary>
    /// Captures a colour from the screen using the specified flyout chrome.
    /// </summary>
    /// <param name="owner">Owner window. May be null.</param>
    /// <param name="flyoutStyle">Classic painted flyout or themed Krypton flyout.</param>
    /// <param name="color">The sampled colour when the method returns <c>true</c>.</param>
    /// <returns><c>true</c> when a colour was picked.</returns>
    public static bool TryPick(IWin32Window? owner, KryptonScreenColorPickerFlyoutStyle flyoutStyle, out Color color) =>
        TryPick(owner, flyoutStyle, DefaultMagnifierSize, out color);

    /// <summary>
    /// Captures a colour from the screen using the specified flyout chrome and magnifier size.
    /// </summary>
    /// <param name="owner">Owner window. May be null.</param>
    /// <param name="flyoutStyle">Classic painted flyout or themed Krypton flyout.</param>
    /// <param name="magnifierSize">Odd number of source pixels shown in the flyout. Clamped to 7–21.</param>
    /// <param name="color">The sampled colour when the method returns <c>true</c>.</param>
    /// <returns><c>true</c> when a colour was picked.</returns>
    public static bool TryPick(IWin32Window? owner, KryptonScreenColorPickerFlyoutStyle flyoutStyle, int magnifierSize, out Color color)
    {
        color = Color.Empty;
        Form? ownerForm = ResolveOwnerForm(owner);
        double previousOpacity = 1d;
        bool hidden = false;

        try
        {
            if (ownerForm != null && ownerForm.Visible)
            {
                previousOpacity = ownerForm.Opacity;
                ownerForm.Opacity = 0d;
                ownerForm.Update();
                hidden = true;
                Application.DoEvents();
            }

            KryptonCustomPaletteBase? palette = ownerForm is KryptonForm kryptonForm
                ? kryptonForm.LocalCustomPalette
                : null;

            using (var overlay = new VisualScreenColorPickerOverlay(flyoutStyle, palette, magnifierSize, DefaultZoom))
            {
                // Do not parent the overlay to a fully transparent owner; TopMost is enough.
                DialogResult result = overlay.ShowDialog();
                DefaultMagnifierSize = overlay.MagnifierSize;
                DefaultZoom = overlay.Zoom;

                if (result == DialogResult.OK)
                {
                    color = overlay.SelectedColor;
                    return true;
                }
            }
        }
        finally
        {
            if (hidden && ownerForm != null && !ownerForm.IsDisposed)
            {
                ownerForm.Opacity = previousOpacity;
            }
        }

        return false;
    }

    private static Form? ResolveOwnerForm(IWin32Window? owner)
    {
        if (owner is Form form)
        {
            return form;
        }

        return owner is Control control ? control.FindForm() : null;
    }
}
