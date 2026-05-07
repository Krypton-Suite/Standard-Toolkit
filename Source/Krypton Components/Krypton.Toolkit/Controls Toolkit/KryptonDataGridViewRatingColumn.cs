#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// A column to display a rating.<br/>
/// Ratings can reach from 1 to 253, set RatingMaximum to the desired number of images.<br/>
/// You van assign custom images to the properties Image and ImgeDisabled.<br/>
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
    // The fallback rating image
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
        _ratingMaximum = 0;
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
    /// A maximum 253 images is supported.<br/>
    /// Width of the column to accomodate the rating is at the descretion of the user.<br/>
    /// Set to zero to disable the display of rating images an empty cell is displayed instead.
    /// </summary>
    [Browsable(true)]
    [DefaultValue(10)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("The maximum value the rating can have.")]
    public byte RatingMaximum
    {
        get => _ratingMaximum;
        set
        {
            if (_ratingMaximum != value)
            {
                _ratingMaximum = value;
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

        foreach(KeyValuePair<byte, Image> kvp in _images)
        {
            cloned._images.Add(kvp.Key, kvp.Value);
        }

        foreach (KeyValuePair<byte, Image> kvp in _imagesDisabled)
        {
            cloned._imagesDisabled.Add(kvp.Key, kvp.Value);
        }

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
            // Enabled/disabled image
            // Get the respective image for the enabled state
            Image enabledImage = _image is not null
                ? _image
                : ResourceFiles.Stars.StarImageResources.star_yellow is Image imgEnabled
                    ? imgEnabled
                    : _ratingFallBackImageEnabled;

            // Get the respective image for the disabled state
            Image disabledImage = _imageDisabled is not null
                ? _imageDisabled
                : ResourceFiles.Stars.StarImageResources.star_yellow_disabled is Image imgDisabled
                    ? imgDisabled
                    : _ratingFallBackImageDisabled;

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
        // Rating images are stored by their byte key. Starting at 1 until Byte.MaxValue minus few
        // Zero is not used as a rating of zero will result in a blank cell

        // Rectangle to copy out the slice on each iteration
        Rectangle rectangle = new Rectangle(0, 0, 0, _ratingImageCanvasSize);
        // Get the base image
        baseImage = GenerateBaseImage(baseImage);

        // Create the full size bitmap once
        using Bitmap bitmap = new(_ratingMaximum * _ratingImageCanvasSize, _ratingImageCanvasSize);
        // One time using
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            for (byte i = 1, j = 0; i <= _ratingMaximum; i++, j++)
            {
                // Draw the next baseImage on each iteration
                g.DrawImage(baseImage, j * _ratingImageCanvasSize, 0);

                // Adjust the rectangle width to copy out the slice of bitmap.
                rectangle.Width = i * _ratingImageCanvasSize;

                // Clone just the needed portion into its own Bitmap, and save it to the dictionary.
                images.Add(i, bitmap.Clone(rectangle, bitmap.PixelFormat));
            }
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
        Bitmap result = new Bitmap(_ratingImageSize, _ratingImageSize);

        using Brush brush = new SolidBrush(color);
        using Graphics g = Graphics.FromImage(result);

        g.FillEllipse(brush, _ratingImageRectangle);

        return result;
    }
    /// <summary>
    /// Paints the rating image onto the canvas.
    /// </summary>
    /// <param name="image">A single image that represents one rating point.</param>
    /// <returns>A base rating image used to construct the rating images.</returns>
    private Image GenerateBaseImage(Image image)
    {
        Bitmap canvas = new Bitmap(_ratingImageCanvasSize, _ratingImageCanvasSize);
        using Graphics g = Graphics.FromImage(canvas);

        // Resize the image if needed
        if (image.Width != _ratingImageSize || image.Height != _ratingImageSize)
        {
            image = new Bitmap(image, _ratingImageSize, _ratingImageSize);
        }

        // Paint it on the canvas
        g.DrawImage(image, _ratingImageRectangle);

        return canvas;
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
    /// Gets the rating image that corresponds to the the ratingIndex and if the DataGridView is disabled or not.
    /// </summary>
    /// <param name="ratingIndex">The rating number.</param>
    /// <returns>
    /// The rating image.<br/>
    /// If the ratingIndex is below the lower bound it will return null.<br/>
    /// Is the ratingIndex over the upper bound it will return the largest index available.</returns>
    internal Image? GetImage(byte ratingIndex)
    {
        if (ratingIndex > 0)
        {
            // Is the requested index to large, return the largest available
            if (ratingIndex > _images.Count)
            {
                ratingIndex = (byte)_images.Count;
            }

            // Teturn the image based on the state of the grid
            return DataGridView!.Enabled
                ? _images[ratingIndex]
                : _imagesDisabled[ratingIndex];
        }
        else
        {
            // 1 is the lowest key we have in the dictionaries
            return null;
        }
    }

    /// <summary>
    /// Returns the number of rating images in a single dictionary.
    /// </summary>
    internal byte RatingImageCount => (byte)_images.Count;
    #endregion
}
