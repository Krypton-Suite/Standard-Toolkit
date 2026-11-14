#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>Optional text strip shown at the top of the update prompt and notes windows.</summary>
public sealed class KryptonUpdateBanner
{
    /// <summary>Initializes a banner.</summary>
    /// <param name="text">Caption. May be empty when only an image is shown.</param>
    /// <param name="backColor">First panel colour. Empty keeps the theme default.</param>
    /// <param name="foreColor">Caption colour. Empty keeps the theme default.</param>
    /// <param name="imageUrl">HTTPS address or local image path. Null when there is no image.</param>
    /// <param name="backColor2">Second panel colour for a linear gradient. Empty uses a solid <paramref name="backColor"/>.</param>
    public KryptonUpdateBanner(string? text, Color backColor, Color foreColor, string? imageUrl, Color backColor2 = default)
    {
        Text = text ?? string.Empty;
        BackColor = backColor;
        BackColor2 = backColor2;
        ForeColor = foreColor;
        if (imageUrl != null && !string.IsNullOrWhiteSpace(imageUrl))
        {
            ImageUrl = imageUrl.Trim();
        }
    }

    /// <summary>Gets the caption. Empty when only an image is shown.</summary>
    public string Text { get; }

    /// <summary>Gets the first panel colour. Empty keeps the theme default.</summary>
    public Color BackColor { get; }

    /// <summary>Gets the second panel colour for a linear gradient. Empty draws a solid <see cref="BackColor"/>.</summary>
    public Color BackColor2 { get; }

    /// <summary>Gets the caption colour. Empty keeps the theme default.</summary>
    public Color ForeColor { get; }

    /// <summary>Gets the HTTPS address or local image path, or null.</summary>
    public string? ImageUrl { get; }

    /// <summary>Gets whether the banner has text or an image to show.</summary>
    public bool HasContent =>
        (Text != null && !string.IsNullOrWhiteSpace(Text)) ||
        (ImageUrl != null && !string.IsNullOrWhiteSpace(ImageUrl));
}
