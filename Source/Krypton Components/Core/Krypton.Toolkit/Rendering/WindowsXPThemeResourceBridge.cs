#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Bridges Windows XP theme renderers to the strongly typed bitmaps in <c>Krypton.Resources</c>.
/// </summary>
internal static partial class WindowsXPThemeResourceBridge
{
    internal static Bitmap? LunaButton => WindowsXPLunaImageResources.Button;

    internal static Bitmap? EmbeddedButton => WindowsXPEmbeddedImageResources.Button;

    internal static Bitmap? RoyaleButton => WindowsXPRoyaleImageResources.Button;

    internal static Bitmap? RoyaleNoirButton => WindowsXPRoyaleNoirImageResources.DARKROYALEBUTTONBmp;

    internal static Bitmap? ZuneButton => WindowsXPZuneImageResources.Button;

    /// <summary>
    /// Draws a vertically stacked XP theme sprite state (typically five states).
    /// </summary>
    internal static void DrawSpriteSlice(Graphics graphics, Bitmap? sprite, Rectangle destination, int state, int states)
    {
        if (graphics == null || sprite == null || destination.Width <= 0 || destination.Height <= 0 || states <= 0)
        {
            return;
        }

        var sourceHeight = Math.Max(1, sprite.Height / states);
        var index = WindowsXPRenderHelper.Clamp(state, 0, states - 1);
        var source = new Rectangle(0, index * sourceHeight, sprite.Width, sourceHeight);
        var interpolation = graphics.InterpolationMode;
        var pixelOffset = graphics.PixelOffsetMode;
        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        graphics.PixelOffsetMode = PixelOffsetMode.Half;
        graphics.DrawImage(sprite, destination, source, GraphicsUnit.Pixel);
        graphics.InterpolationMode = interpolation;
        graphics.PixelOffsetMode = pixelOffset;
    }
}
