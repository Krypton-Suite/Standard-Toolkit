#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Optional overlay (badge) image settings for dialog icons and similar surfaces.
/// When <see cref="Image"/> is null the overlay is ignored. Defaults follow a macOS-style
/// bottom-right badge (unlike control <see cref="OverlayImageValues"/>, which default to top-right).
/// </summary>
public struct KryptonOverlayImage
{
    /// <summary>Default overlay scale factor (50% of the smaller main-image dimension).</summary>
    public const float DefaultScaleFactor = 0.5f;

    /// <summary>Default fixed overlay size when using <see cref="OverlayImageScaleMode.FixedSize"/>.</summary>
    public static readonly Size DefaultFixedSize = new Size(16, 16);

    /// <summary>
    /// Initializes a new instance with macOS-style defaults and the specified overlay image.
    /// </summary>
    /// <param name="image">The overlay image, or null for no overlay.</param>
    public KryptonOverlayImage(Image? image)
        : this(image, OverlayImagePosition.BottomRight, OverlayImageScaleMode.Percentage, DefaultScaleFactor,
            DefaultFixedSize, Color.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance with the specified image and corner position (macOS scale defaults).
    /// </summary>
    /// <param name="image">The overlay image, or null for no overlay.</param>
    /// <param name="position">Corner placement relative to the main image.</param>
    public KryptonOverlayImage(Image? image, OverlayImagePosition position)
        : this(image, position, OverlayImageScaleMode.Percentage, DefaultScaleFactor, DefaultFixedSize, Color.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance with full overlay configuration.
    /// </summary>
    /// <param name="image">The overlay image, or null for no overlay.</param>
    /// <param name="position">Corner placement relative to the main image.</param>
    /// <param name="scaleMode">How the overlay is sized relative to the main image.</param>
    /// <param name="scaleFactor">Scale factor for percentage / proportional modes.</param>
    /// <param name="fixedSize">Size when <paramref name="scaleMode"/> is <see cref="OverlayImageScaleMode.FixedSize"/>.</param>
    /// <param name="imageTransparentColor">Colour treated as transparent when drawing; use <see cref="Color.Empty"/> for none.</param>
    public KryptonOverlayImage(Image? image, OverlayImagePosition position, OverlayImageScaleMode scaleMode,
        float scaleFactor, Size fixedSize, Color imageTransparentColor)
    {
        Image = image;
        Position = position;
        ScaleMode = scaleMode;
        ScaleFactor = scaleFactor;
        FixedSize = fixedSize;
        ImageTransparentColor = imageTransparentColor;
    }

    /// <summary>Gets or sets the overlay image. Null means no overlay.</summary>
    [Editor(typeof(KryptonDesignerImageEditor), typeof(UITypeEditor))]
    public Image? Image { get; set; }

    /// <summary>Gets or sets the corner placement. Default is <see cref="OverlayImagePosition.BottomRight"/>.</summary>
    public OverlayImagePosition Position { get; set; }

    /// <summary>Gets or sets how the overlay is scaled. Default is <see cref="OverlayImageScaleMode.Percentage"/>.</summary>
    public OverlayImageScaleMode ScaleMode { get; set; }

    /// <summary>Gets or sets the scale factor for percentage / proportional modes. Default is 0.5.</summary>
    public float ScaleFactor { get; set; }

    /// <summary>Gets or sets the fixed overlay size. Default is 16×16.</summary>
    public Size FixedSize { get; set; }

    /// <summary>Gets or sets the transparent colour key for the overlay image.</summary>
    public Color ImageTransparentColor { get; set; }

    /// <summary>Gets a value indicating whether no overlay image is configured.</summary>
    public bool IsEmpty => Image == null;

    /// <summary>
    /// Creates overlay settings with macOS-style defaults for the given image and optional position.
    /// </summary>
    /// <param name="image">The overlay image.</param>
    /// <param name="position">Corner placement; defaults to bottom-right.</param>
    /// <returns>Configured overlay settings, or an empty instance when <paramref name="image"/> is null.</returns>
    public static KryptonOverlayImage FromImage(Image? image,
        OverlayImagePosition position = OverlayImagePosition.BottomRight) =>
        image == null ? default : new KryptonOverlayImage(image, position);
}
