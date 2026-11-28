#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Reference-counted <see cref="ImageAnimator"/> registration for tooltip images.
/// </summary>
/// <remarks>
/// <see cref="PaletteImageDrawing"/> copies the active frame before applying palette effects.
/// <see cref="Graphics.DrawImage(Image, Rectangle, int, int, int, int, GraphicsUnit, ImageAttributes)"/>
/// often keeps painting GIF frame 0, so the copy is made without attributes.
/// Static images are not registered and are drawn unchanged.
/// </remarks>
internal static class TooltipAnimatedImage
{
    #region Instance Fields

    private static readonly object _gate = new object();

    private static readonly Dictionary<Image, Attachment> _attachments = new Dictionary<Image, Attachment>();

    #endregion

    #region Identity

    /// <summary>
    /// Starts animating <paramref name="image"/> for one popup.
    /// </summary>
    /// <param name="image">Multi-frame image. Ignored when <see cref="ImageAnimator.CanAnimate"/> is false.</param>
    /// <param name="onFrameChanged">Callback invoked on a thread-pool thread when the frame changes.</param>
    public static void Attach(Image image, EventHandler onFrameChanged)
    {
        if (image == null || onFrameChanged == null || !ImageAnimator.CanAnimate(image))
        {
            return;
        }

        lock (_gate)
        {
            if (!_attachments.TryGetValue(image, out Attachment? attachment))
            {
                attachment = new Attachment();
                _attachments.Add(image, attachment);
            }

            attachment.Count++;
        }

        ImageAnimator.Animate(image, onFrameChanged);
    }

    /// <summary>
    /// Stops one popup's animation callback. The frame cache is released when the last popup detaches.
    /// </summary>
    /// <param name="image">Image previously passed to <see cref="Attach"/>.</param>
    /// <param name="onFrameChanged">The same delegate instance passed to <see cref="Attach"/>.</param>
    public static void Detach(Image? image, EventHandler onFrameChanged)
    {
        if (image == null || onFrameChanged == null)
        {
            return;
        }

        ImageAnimator.StopAnimate(image, onFrameChanged);

        lock (_gate)
        {
            if (!_attachments.TryGetValue(image, out Attachment? attachment))
            {
                return;
            }

            attachment.Count--;
            if (attachment.Count > 0)
            {
                return;
            }

            _attachments.Remove(image);
            attachment.Frame?.Dispose();
            attachment.Frame = null;
        }
    }

    /// <summary>
    /// Draws <paramref name="image"/>, substituting the active frame when the image is attached.
    /// </summary>
    /// <param name="image">Image about to be drawn.</param>
    /// <param name="draw">Receives either <paramref name="image"/> or the cached active-frame bitmap.</param>
    public static void DrawFrame(Image image, Action<Image> draw)
    {
        if (draw == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(draw));
        }

        lock (_gate)
        {
            if (!_attachments.TryGetValue(image, out Attachment? attachment))
            {
                draw(image);
                return;
            }

            ImageAnimator.UpdateFrames(image);
            int width = Math.Max(1, image.Width);
            int height = Math.Max(1, image.Height);
            if (attachment.Frame == null || attachment.Frame.Width != width || attachment.Frame.Height != height)
            {
                attachment.Frame?.Dispose();
                attachment.Frame = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            }

            using (Graphics frameGraphics = Graphics.FromImage(attachment.Frame))
            {
                frameGraphics.Clear(Color.Transparent);
                frameGraphics.DrawImage(image, new Rectangle(0, 0, width, height));
            }

            draw(attachment.Frame);
        }
    }

    #endregion

    #region Private Types

    private sealed class Attachment
    {
        public int Count;

        public Bitmap? Frame;
    }

    #endregion
}
