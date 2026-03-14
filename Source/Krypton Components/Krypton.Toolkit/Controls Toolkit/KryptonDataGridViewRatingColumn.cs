#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege & Ahmed Abdelhameed et al. 2025 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// A column to display a rating.<br/>
/// Ratings can reach from 1 to 254, set RatingMaximum to the desired number of images.<br/>
/// You can assign custom images to the properties Image and ImageDisabled.<br/>
/// If you do not supply your own images, default stock images will be used.
/// </summary>
public class KryptonDataGridViewRatingColumn : KryptonDataGridViewIconColumn
{
    #region Private/internal static
    internal static readonly Type _defaultValueType = typeof(byte);
    // This is the size of the square image canvas where the rating image is painted on.
    internal const int _ratingImageCanvasSize = 14;
    // This is the size of the rating image that is painted in the canvas above.
    internal const int _ratingImageSize = 12;
    // The rectangle to position the rating image on the canvas
    internal static readonly Rectangle _ratingImageRectangle = new(1, 1, 12, 12);
    // default rating
    private const byte _defaultRatingMaximum = 10;
    // The fallback rating images
    internal static readonly Image _ratingFallBackImageEnabled = GenerateFallBackImage(Color.Green);
    internal static readonly Image _ratingFallBackImageDisabled = GenerateFallBackImage(Color.Gray);
    #endregion

    #region Private fields
    // User configurable: the maximum rating possible to display
    private byte _ratingMaximum;
    // User configurable: custom enabled state rating image
    private Image? _image;
    // User configurable: custom disabled state rating image
    private Image? _imageDisabled;
    // Enabled state dictionary for rating images
    private Dictionary<byte, Image> _images;
    // Disabled state dictionary for rating images
    private Dictionary<byte, Image> _imagesDisabled;
    // If the object has been disposed
    private bool _disposed;
    #endregion

    #region Identity

    /// <summary>
    /// Default constructor.
    /// </summary>
    public KryptonDataGridViewRatingColumn()
        : base(new KryptonDataGridViewRatingCell())
    {
        _ratingMaximum = _defaultRatingMaximum;
        _disposed = false;
        _image = null;
        _imageDisabled = null;
        _images = [];
        _imagesDisabled = [];

        ValueType = _defaultValueType;
        ReadOnly = false;
    }
    #endregion

    #region Public
    /// <summary>
    /// The maximum value the rating can have.<br/>
    /// A maximum 254 images is supported.<br/>
    /// Width of the column to accommodate the rating is at the descretion of the user.<br/>
    /// Set to zero to disable the display of rating images an empty cell is displayed instead.
    /// </summary>
    [Browsable(true)]
    [DefaultValue(_defaultRatingMaximum)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("The maximum value the rating can have. (254 maximum)")]
    public byte RatingMaximum
    {
        get => _ratingMaximum;
        set
        {
            if (_ratingMaximum != value)
            {
                _ratingMaximum = value >= byte.MaxValue 
                    ? (byte)(byte.MaxValue - 1)
                    : value;
                OnGenerateRatingImages();
            }
        }
    }

    /// <summary>
    /// The image to be used that indicates the rating when the DataGridView is enabled.<br/>
    /// The images is replicated the number of times equal to the cell value.<br/>
    /// If Image is set to null a stock images will be used.
    /// </summary>
    [Browsable(true)]
    [DefaultValue(null)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("The image to be used that indicates the rating when the DataGridView is enabled.")]
    public Image? Image 
    {
        get => _image;
        set
        {
            if (_image != value)
            {
                _image = value;
                OnGenerateRatingImages();
            }
        }
    }

    /// <summary>
    /// The image to be used that indicates the rating when the DataGridView is disabled.<br/>
    /// The images is replicated the number of times equal to the cell value.<br/>
    /// If Image is set to null a stock images will be used.
    /// </summary>
    [Browsable(true)]
    [DefaultValue(null)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("The image to be used that indicates the rating when the DataGridView is disabled.")]
    public Image? ImageDisabled 
    {
        get => _imageDisabled;
        set
        {
            if (_imageDisabled != value)
            {
                _imageDisabled = value;
                OnGenerateRatingImages();
            }
        }
    }
    #endregion

    #region Public override
    /// <inheritdoc/>
    public override object Clone()
    {
        var cloned = base.Clone() as KryptonDataGridViewRatingColumn ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("cloned"));

        CloneImageDictionary(_images, cloned._images);
        CloneImageDictionary(_imagesDisabled, cloned._imagesDisabled);

        return cloned;
    }
    #endregion

    #region Protected override
    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _images.Clear();
            _imagesDisabled.Clear();
            
            _ratingMaximum = 0;

            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Implementation
    /// <summary>
    /// Generates the rating images to be used for each possible value.
    /// </summary>
    private void OnGenerateRatingImages()
    {
        // Always clear when we get here
        _images.Clear();
        _imagesDisabled.Clear();

        if (_ratingMaximum > 0)
        {
            // Get the respective image for the enabled state
            Image enabledImage = _image
                ?? ResourceFiles.Stars.StarImageResources.star_yellow as Image
                ?? _ratingFallBackImageEnabled;

            // Get the respective image for the disabled state
            Image disabledImage = _imageDisabled
                ?? ResourceFiles.Stars.StarImageResources.star_yellow_disabled as Image
                ?? _ratingFallBackImageDisabled;

            // Generate the images and store them in the dictionary
            GenerateRatingImages(enabledImage, _images);
            GenerateRatingImages(disabledImage, _imagesDisabled);
        }

        // Refresh the column
        OnInvalidateColumn();
    }

    /// <summary>
    /// Generates the separate rating images and stores them in the corresponding dictionary.<br/>
    /// This has to be run twice, once for the enabled images and once for the disabled.
    /// </summary>
    /// <param name="baseImage">Single base rating image.</param>
    /// <param name="images">Dictionary where the generated rating images are stored.</param>
    private void GenerateRatingImages(Image baseImage, Dictionary<byte, Image> images)
    {
        // Generate the images and then store in the dictionary.
        // Each image in the dictionary is based on the size of the rating. 
        // In that way the image can be aligned in the cell
        // Rating images are stored by their byte key. Starting at 1 until Byte.MaxValue - 1
        // Zero is not used as a rating of zero will result in a blank cell

        // Rectangle to copy out the slice on each iteration
        Rectangle rectangle = new(0, 0, 0, _ratingImageCanvasSize);
        // Get the base image
        baseImage = GenerateBaseImage(baseImage);

        // Create the full size bitmap once
        using Bitmap canvas = new(_ratingMaximum * _ratingImageCanvasSize, _ratingImageCanvasSize);
        using Graphics graphics = Graphics.FromImage(canvas);
            
        for (byte i = 1, j = 0; i <= _ratingMaximum; i++, j++)
        {
            // Draw the next baseImage on each iteration
            graphics.DrawImage(baseImage, j * _ratingImageCanvasSize, 0);

            // Adjust the rectangle width to copy out the bitmap slice.
            rectangle.Width = i * _ratingImageCanvasSize;
            // Clone just the needed portion into its own Bitmap.
            images.Add(i, canvas.Clone(rectangle, canvas.PixelFormat));
        }
    }

    /// <summary>
    /// Generates the static fallback image.<br/>
    /// Fallback images are static and generic for all columns of this type.
    /// </summary>
    /// <param name="color">The color of the image.</param>
    /// <returns>The rating image that will be painted onto the single canvas.</returns>
    private static Image GenerateFallBackImage(Color color)
    {
        using Bitmap   result   = new(_ratingImageSize, _ratingImageSize);
        using Brush    brush    = new SolidBrush(color);
        using Graphics graphics = Graphics.FromImage(result);

        graphics.FillEllipse(brush, _ratingImageRectangle);

        return (Image)result.Clone();
    }

    /// <summary>
    /// Paints the rating image onto the canvas.
    /// </summary>
    /// <param name="image">A single image that represents one rating point.</param>
    /// <returns>A base rating image used to construct the rating images.</returns>
    private Image GenerateBaseImage(Image image)
    {
        using Bitmap result = new(_ratingImageCanvasSize, _ratingImageCanvasSize);
        using Graphics graphics = Graphics.FromImage(result);

        // Resize the image if needed
        if (image.Width != _ratingImageSize || image.Height != _ratingImageSize)
        {
            image = new Bitmap(image, _ratingImageSize, _ratingImageSize);
        }

        graphics.DrawImage(image, _ratingImageRectangle);

        return (Image)result.Clone();
    }

    /// <summary>
    /// Perform a column refresh.
    /// </summary>
    private void OnInvalidateColumn()
    {
        DataGridView?.InvalidateColumn(this.Index);
    }
    #endregion

    #region Internal
    /// <summary>
    /// Gets the rating image that corresponds to the ratingIndex and if the DataGridView is disabled or not.
    /// </summary>
    /// <param name="ratingIndex">The rating number.</param>
    /// <returns>
    /// The rating image.<br/>
    /// If the ratingIndex is below the lower bound it will return null.<br/>
    /// Is the ratingIndex over the upper bound it will return the largest index available.</returns>
    internal Image? GetRatingImage(byte ratingIndex)
    {
        if (ratingIndex > 0)
        {
            // This works on a dictionary and the keys start 1 (not zero)
            // So count does not need decremented by 1
            if (ratingIndex > _images.Count)
            {
                ratingIndex = (byte)_images.Count;
            }

            return DataGridView!.Enabled
                ? _images[ratingIndex]
                : _imagesDisabled[ratingIndex];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Returns the number of rating images in a single dictionary.
    /// </summary>
    internal byte RatingImageCount => (byte)_images.Count;
    #endregion

    #region Private
    private void CloneImageDictionary(Dictionary<byte, Image> source, Dictionary<byte, Image> target)
    {
        foreach (KeyValuePair<byte, Image> sourceItem in source)
        {
            target.Add(sourceItem.Key, sourceItem.Value);
        }
    }
    #endregion
}
