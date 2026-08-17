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
/// PowerToys-style screen colour picker: freeze the desktop, magnify pixels under the cursor, and click to sample.
/// </summary>
/// <remarks>
/// Inspired by the PowerToys Color Picker overlay (screenshot, zoomed pixel grid, hex/RGB readout).
/// Click samples, Esc or right-click cancels, mouse wheel changes zoom.
/// When an owner window is supplied it is made fully transparent for the snapshot so colours behind the dialog can be picked.
/// </remarks>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonScreenColorPicker
{
    /// <summary>
    /// Creates a 16 by 16 eyedropper image suitable for a screen-picker button.
    /// </summary>
    /// <returns>A disposable image for use with button image values.</returns>
    public static Image CreateDropperGlyphImage() => ScreenColorPickerGlyph.Create();

    /// <summary>
    /// Captures a colour from the screen. Returns <c>false</c> when the user cancels or the screenshot cannot be taken.
    /// </summary>
    /// <param name="color">The sampled colour when the method returns <c>true</c>.</param>
    /// <returns><c>true</c> when a colour was picked.</returns>
    public static bool TryPick(out Color color) => TryPick(null, out color);

    /// <summary>
    /// Captures a colour from the screen. Makes <paramref name="owner"/> fully transparent while taking the snapshot when it is a visible form.
    /// </summary>
    /// <param name="owner">Owner window. May be null.</param>
    /// <param name="color">The sampled colour when the method returns <c>true</c>.</param>
    /// <returns><c>true</c> when a colour was picked.</returns>
    public static bool TryPick(IWin32Window? owner, out Color color)
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

            Bitmap? screenshot = CaptureVirtualScreen();
            if (screenshot is null)
            {
                return false;
            }

            using (var overlay = new VisualScreenColorPickerOverlay(screenshot))
            {
                // Do not parent the overlay to a fully transparent owner; TopMost is enough.
                DialogResult result = overlay.ShowDialog();

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

    private static Bitmap? CaptureVirtualScreen()
    {
        Rectangle bounds = SystemInformation.VirtualScreen;
        if (bounds.Width <= 0 || bounds.Height <= 0)
        {
            return null;
        }

        var bitmap = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
        try
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size);
            }

            return bitmap;
        }
        catch (Exception)
        {
            bitmap.Dispose();
            return null;
        }
    }
}
