#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Stores a text/extraText/Image triple of values as a content values source.
/// </summary>
public class FixedContentValue : IContentValues
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the FixedContentValue class.
    /// </summary>
    public FixedContentValue()
        : this(GlobalStaticValues.DEFAULT_EMPTY_STRING, GlobalStaticValues.DEFAULT_EMPTY_STRING, null, GlobalStaticValues.EMPTY_COLOR)
    {
    }

    /// <summary>
    /// Initialize a new instance of the FixedContentValue class.
    /// </summary>
    /// <param name="shortText">Initial short text value.</param>
    /// <param name="longText">Initial long text value.</param>
    /// <param name="image">Initial image value.</param>
    /// <param name="imageTransparentColor">Initial image transparent color value.</param>
    public FixedContentValue(string? shortText,
        string? longText,
        Image? image,
        Color imageTransparentColor)
    {
        ShortText = shortText;
        LongText = longText;
        Image = image;
        ImageTransparentColor = imageTransparentColor;
    }
    #endregion

    #region ShortText
    /// <summary>
    /// Gets and sets the short text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Main text.")]
    [Localizable(true)]
    [DefaultValue(@"")]
    public string? ShortText { get; set; }

    private bool ShouldSerializeShortText() => !string.IsNullOrEmpty(ShortText);

    #endregion

    #region LongText
    /// <summary>
    /// Gets and sets the long text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Supplementary text.")]
    [Localizable(true)]
    [DefaultValue(@"")]
    public string? LongText { get; set; }

    private bool ShouldSerializeLongText() => !string.IsNullOrEmpty(LongText);

    #endregion

    #region Image
    /// <summary>
    /// Gets and sets the image.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Image associated with item.")]
    [Localizable(true)]
    public Image? Image { get; set; }

    private bool ShouldSerializeImage() => Image != null;

    #endregion

    #region ImageTransparentColor
    /// <summary>
    /// Gets and sets the image transparent color.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Color to treat as transparent in the Image.")]
    [Localizable(true)]
    public Color ImageTransparentColor { get; set; }

    private bool ShouldSerializeImageTransparentColor() => ImageTransparentColor != GlobalStaticValues.EMPTY_COLOR;

    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => ShortText!;

    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state) => Image;

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => ImageTransparentColor;

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => LongText!;

    #endregion
}