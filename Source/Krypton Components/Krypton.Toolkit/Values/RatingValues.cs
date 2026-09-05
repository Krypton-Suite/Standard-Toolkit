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
/// Storage for <see cref="KryptonRating"/> glyph size, spacing, shape, and images. Colours live on the control StateCommon / StateNormal / StateTracking / StateDisabled properties.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class RatingValues : Storage
{
    #region Static Fields

    internal const int DefaultItemSize = 20;
    internal const int DefaultItemSpacing = 4;

    #endregion

    #region Instance Fields

    private int _itemSize;
    private int _itemSpacing;
    private KryptonRatingGlyph _glyph;
    private Image? _imageFilled;
    private Image? _imageEmpty;
    private Image? _imageHalf;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="RatingValues"/> class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public RatingValues(NeedPaintHandler? needPaint)
    {
        NeedPaint = needPaint;
        Reset();
    }

    /// <inheritdoc />
    public override bool IsDefault => !ShouldSerializeItemSize()
                                      && !ShouldSerializeItemSpacing()
                                      && !ShouldSerializeGlyph()
                                      && !ShouldSerializeImageFilled()
                                      && !ShouldSerializeImageEmpty()
                                      && !ShouldSerializeImageHalf();

    /// <summary>
    /// Restore appearance to factory defaults.
    /// </summary>
    public void Reset()
    {
        ResetItemSize();
        ResetItemSpacing();
        ResetGlyph();
        ResetImageFilled();
        ResetImageEmpty();
        ResetImageHalf();
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the pixel size of each glyph.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Pixel size of each rating glyph.")]
    [DefaultValue(DefaultItemSize)]
    public int ItemSize
    {
        get => _itemSize;
        set
        {
            value = Math.Max(8, Math.Min(128, value));
            if (_itemSize != value)
            {
                _itemSize = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeItemSize() => _itemSize != DefaultItemSize;

    /// <summary>
    /// Resets the ItemSize property to its default value.
    /// </summary>
    public void ResetItemSize() => ItemSize = DefaultItemSize;

    /// <summary>
    /// Gets and sets the gap in pixels between glyphs.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Gap in pixels between rating glyphs.")]
    [DefaultValue(DefaultItemSpacing)]
    public int ItemSpacing
    {
        get => _itemSpacing;
        set
        {
            value = Math.Max(0, Math.Min(64, value));
            if (_itemSpacing != value)
            {
                _itemSpacing = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeItemSpacing() => _itemSpacing != DefaultItemSpacing;

    /// <summary>
    /// Resets the ItemSpacing property to its default value.
    /// </summary>
    public void ResetItemSpacing() => ItemSpacing = DefaultItemSpacing;

    /// <summary>
    /// Gets and sets the glyph drawn for each rating item.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Glyph drawn for each rating item.")]
    [DefaultValue(KryptonRatingGlyph.Star)]
    public KryptonRatingGlyph Glyph
    {
        get => _glyph;
        set
        {
            if (_glyph != value)
            {
                _glyph = value;
                PerformNeedPaint(false);
            }
        }
    }

    private bool ShouldSerializeGlyph() => _glyph != KryptonRatingGlyph.Star;

    /// <summary>
    /// Resets the Glyph property to its default value.
    /// </summary>
    public void ResetGlyph() => Glyph = KryptonRatingGlyph.Star;

    /// <summary>
    /// Gets and sets the filled image used when <see cref="Glyph"/> is <see cref="KryptonRatingGlyph.Image"/>.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Filled image when Glyph is Image. Null uses the stock yellow star.")]
    [DefaultValue(null)]
    [Editor(typeof(KryptonDesignerImageEditor), typeof(UITypeEditor))]
    public Image? ImageFilled
    {
        get => _imageFilled;
        set
        {
            if (!ReferenceEquals(_imageFilled, value))
            {
                _imageFilled = value;
                PerformNeedPaint(false);
            }
        }
    }

    private bool ShouldSerializeImageFilled() => _imageFilled != null;

    /// <summary>
    /// Resets the ImageFilled property to its default value.
    /// </summary>
    public void ResetImageFilled() => ImageFilled = null;

    /// <summary>
    /// Gets and sets the empty image used when <see cref="Glyph"/> is <see cref="KryptonRatingGlyph.Image"/>.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Empty image when Glyph is Image. Null fades the filled image.")]
    [DefaultValue(null)]
    [Editor(typeof(KryptonDesignerImageEditor), typeof(UITypeEditor))]
    public Image? ImageEmpty
    {
        get => _imageEmpty;
        set
        {
            if (!ReferenceEquals(_imageEmpty, value))
            {
                _imageEmpty = value;
                PerformNeedPaint(false);
            }
        }
    }

    private bool ShouldSerializeImageEmpty() => _imageEmpty != null;

    /// <summary>
    /// Resets the ImageEmpty property to its default value.
    /// </summary>
    public void ResetImageEmpty() => ImageEmpty = null;

    /// <summary>
    /// Gets and sets the half-filled image used when <see cref="Glyph"/> is <see cref="KryptonRatingGlyph.Image"/>.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Half-filled image when Glyph is Image. Null clips the filled image.")]
    [DefaultValue(null)]
    [Editor(typeof(KryptonDesignerImageEditor), typeof(UITypeEditor))]
    public Image? ImageHalf
    {
        get => _imageHalf;
        set
        {
            if (!ReferenceEquals(_imageHalf, value))
            {
                _imageHalf = value;
                PerformNeedPaint(false);
            }
        }
    }

    private bool ShouldSerializeImageHalf() => _imageHalf != null;

    /// <summary>
    /// Resets the ImageHalf property to its default value.
    /// </summary>
    public void ResetImageHalf() => ImageHalf = null;

    #endregion
}
