#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Resolves the default command-link arrow image for the current operating system.
/// </summary>
/// <remarks>
/// Windows 10 and later expose the arrow as <c>shell32.dll</c> resource
/// <see cref="SharedStaticConstants.COMMAND_LINK_ARROW_ICON_ID"/>. That resource is missing (or mapped
/// to a different image) on Windows 7 / 8 / 8.1, so those versions use an embedded bitmap instead.
/// </remarks>
public static class CommandLinkArrowHelper
{
    private const int DefaultSize = 32;
    private static readonly object Sync = new object();
    private static Image? _cachedDefault;

    /// <summary>
    /// Gets the default command-link arrow image for the current operating system.
    /// </summary>
    /// <param name="size">Target size in pixels. Defaults to 32.</param>
    /// <returns>
    /// The arrow image. The 32×32 result is cached and must not be disposed by the caller.
    /// Other sizes are new instances owned by the caller.
    /// </returns>
    public static Image GetDefaultArrowImage(int size = DefaultSize)
    {
        if (size != DefaultSize)
        {
            return CreateDefaultArrowImage(size);
        }

        lock (Sync)
        {
            if (_cachedDefault == null)
            {
                _cachedDefault = CreateDefaultArrowImage(DefaultSize);
            }

            return _cachedDefault;
        }
    }

    private static Image CreateDefaultArrowImage(int size)
    {
        Image? extracted = TryExtractFromShell32(size);
        if (extracted != null)
        {
            return extracted;
        }

        Image? embedded = GetEmbeddedArrowForCurrentOs();
        if (embedded != null)
        {
            Bitmap? scaled = GraphicsExtensions.ScaleImage(embedded, size, size);
            if (scaled != null)
            {
                return scaled;
            }
        }

        return DrawFallbackArrow(size);
    }

    /// <summary>
    /// Attempts to extract the command-link arrow from <c>shell32.dll</c>.
    /// Returns <see langword="null"/> on Windows 7 / 8 / 8.1, where resource 16805 is not present.
    /// </summary>
    private static Image? TryExtractFromShell32(int size)
    {
        // RtlGetVersion: Windows 7 = 6.1, 8 = 6.2, 8.1 = 6.3, 10/11 = 10.x.
        if (OSUtilities.OsVersionInfo.MajorVersion < 10)
        {
            return null;
        }

        Icon? icon = GraphicsExtensions.ExtractIcon(Libraries.Shell32, SharedStaticConstants.COMMAND_LINK_ARROW_ICON_ID, true);
        if (icon == null)
        {
            return null;
        }

        try
        {
            using (Bitmap raw = icon.ToBitmap())
            {
                return GraphicsExtensions.ScaleImage(raw, size, size);
            }
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
            return null;
        }
        finally
        {
            icon.Dispose();
        }
    }

    private static Image? GetEmbeddedArrowForCurrentOs()
    {
        if (OSUtilities.IsAtLeastWindowsEleven)
        {
            return CommandLinkImageResources.Windows_11_CommandLink_Arrow;
        }

        if (OSUtilities.IsWindowsTen)
        {
            return CommandLinkImageResources.Windows_10_CommandLink_Arrow;
        }

        // Windows 7 / 8 / 8.1 (and any older client) share the Aero-style embedded arrow.
        return CommandLinkImageResources.Windows_7_CommandLink_Arrow;
    }

    /// <summary>
    /// Draws a Windows 7 Aero-style command-link arrow when neither the system DLL nor embedded resources are available.
    /// </summary>
    private static Bitmap DrawFallbackArrow(int size)
    {
        var bitmap = new Bitmap(size, size, PixelFormat.Format32bppArgb);
        using (var graphics = Graphics.FromImage(bitmap))
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.Clear(Color.Transparent);

            float s = size / 32f;
            PointF[] points =
            {
                new PointF(6f * s, 11f * s),
                new PointF(16f * s, 11f * s),
                new PointF(16f * s, 6f * s),
                new PointF(27f * s, 16f * s),
                new PointF(16f * s, 26f * s),
                new PointF(16f * s, 21f * s),
                new PointF(6f * s, 21f * s)
            };

            using (var path = new GraphicsPath())
            {
                path.AddPolygon(points);
                RectangleF bounds = path.GetBounds();
                using (var brush = new LinearGradientBrush(bounds,
                           Color.FromArgb(255, 91, 178, 247),
                           Color.FromArgb(255, 0, 90, 173),
                           90f))
                {
                    graphics.FillPath(brush, path);
                }

                using (var outline = new Pen(Color.FromArgb(220, 0, 70, 140), Math.Max(1f, s)))
                {
                    graphics.DrawPath(outline, path);
                }
            }
        }

        return bitmap;
    }
}
