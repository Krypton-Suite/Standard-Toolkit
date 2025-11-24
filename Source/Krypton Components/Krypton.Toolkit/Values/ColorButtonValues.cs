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
/// Storage for color button content value information.
/// </summary>
public class ColorButtonValues : Storage,
    IContentValues
{
    #region Static Fields

    private readonly string _defaultText = KryptonManager.Strings.ColorStrings.Color;
    private static readonly string _defaultExtraText = GlobalStaticValues.DEFAULT_EMPTY_STRING;
    private static readonly Image? _defaultImage = GenericImageResources.ButtonColorImageSmall;
    #endregion

    #region Instance Fields
    private Image? _image;
    private Image? _sourceImage;
    private Image? _compositeImage;
    private Color _transparent;
    private string? _text;
    private string _extraText;
    private Color _selectedColor;
    private Color _emptyBorderColor;
    private Rectangle _selectedRect;
    private byte _roundedCorners;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the Text property changes.
    /// </summary>
    public event EventHandler? TextChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ColorButtonValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ColorButtonValues(NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Set initial values
        _image = _defaultImage;
        _transparent = GlobalStaticValues.EMPTY_COLOR;
        _text = _defaultText;
        _extraText = _defaultExtraText;
        ImageStates = CreateImageStates();
        ImageStates.NeedPaint = needPaint;
        _emptyBorderColor = Color.Gray;
        _selectedColor = Color.Red;
        _selectedRect = new Rectangle(0, 12, 16, 4);
        _roundedCorners = 0;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ImageStates.IsDefault &&
                                      (Image == _defaultImage) &&
                                      (ImageTransparentColor == GlobalStaticValues.EMPTY_COLOR) &&
                                      (Text == _defaultText) &&
                                      (ExtraText == _defaultExtraText)
                                      && (_roundedCorners == 0)
    ;

    #endregion

    #region Image
    /// <summary>
    /// Gets and sets the button image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button image.")]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Image
    {
        get => _image;

        set
        {
            if (_image != value)
            {
                _image = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeImage() => Image != _defaultImage;

    /// <summary>
    /// Resets the Image property to its default value.
    /// </summary>
    public void ResetImage() => Image = _defaultImage;
    #endregion

    #region ImageTransparentColor
    /// <summary>
    /// Gets and sets the label image transparent color.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Label image transparent color.")]
    [RefreshProperties(RefreshProperties.All)]
    [KryptonDefaultColor]
    public Color ImageTransparentColor
    {
        get => _transparent;

        set
        {
            if (_transparent != value)
            {
                _transparent = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeImageTransparentColor() => ImageTransparentColor != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Resets the ImageTransparentColor property to its default value.
    /// </summary>
    public void ResetImageTransparentColor() => ImageTransparentColor = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the content image transparent color.
    /// </summary>
    /// <param name="state">The state for which the image color is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => ImageTransparentColor;

    #endregion

    #region ImageStates
    /// <summary>
    /// Gets access to the state specific images for the button.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"State specific images for the button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ButtonImageStates ImageStates { get; }

    private bool ShouldSerializeImageStates() => !ImageStates.IsDefault;

    #endregion

    #region Text
    /// <summary>
    /// Gets and sets the button text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button text.")]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [AllowNull]
    public string Text
    {
        get => _text ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;

        set
        {
            if (_text != value)
            {
                _text = value;
                PerformNeedPaint(true);
                TextChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool ShouldSerializeText() => Text != _defaultText;

    /// <summary>
    /// Resets the Text property to its default value.
    /// </summary>
    public void ResetText() => Text = _defaultText;
    #endregion

    #region ExtraText
    /// <summary>
    /// Gets and sets the button extra text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button extra text.")]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DefaultValue(@"")]
    public string ExtraText
    {
        get => _extraText;

        set
        {
            if (_extraText != value)
            {
                _extraText = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeExtraText() => ExtraText != _defaultExtraText;

    /// <summary>
    /// Resets the Description property to its default value.
    /// </summary>
    public void ResetExtraText() => ExtraText = _defaultExtraText;
    #endregion

    #region SelectedColor
    /// <summary>
    /// Gets and sets the selected color for the composite image.
    /// </summary>
    internal Color SelectedColor
    {
        get => _selectedColor;

        set
        {
            _selectedColor = value;
            _compositeImage = null;
        }
    }
    #endregion

    #region EmptyBorderColor
    /// <summary>
    /// Gets and sets the empty border color for the composite image.
    /// </summary>
    internal Color EmptyBorderColor
    {
        get => _emptyBorderColor;

        set
        {
            _emptyBorderColor = value;
            _compositeImage = null;
        }
    }
    #endregion

    #region SelectedRect
    /// <summary>
    /// Gets and sets the selected rectangle for the composite image.
    /// </summary>
    internal Rectangle SelectedRect
    {
        get => _selectedRect;

        set
        {
            _selectedRect = value;
            _compositeImage = null;
        }
    }

    /// <summary>
    /// Gets and sets the selected color drawing rectangle.
    /// </summary>
    [Bindable(true)]
    [Category(@"Appearance")]
    [Description(@"Rounded color drawing rectangle.")]
    public int RoundedCorners
    {
        get => _roundedCorners;

        set
        {
            _roundedCorners = (byte)value;
            _compositeImage = null;
            PerformNeedPaint(true);
        }
    }
    private bool ShouldSerializeRoundedCorners() => _roundedCorners != 0;

    /// <summary>
    /// Resets the Description property to its default value.
    /// </summary>
    public void ResetRoundedCorners() => RoundedCorners = 0;

    #endregion

    #region CreateImageStates
    /// <summary>
    /// Create the storage for the image states.
    /// </summary>
    /// <returns>Storage object.</returns>
    protected virtual ButtonImageStates CreateImageStates() => new ButtonImageStates();

    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public virtual Image? GetImage(PaletteState state)
    {
        // Try and find a state specific image
        Image? image = state switch
        {
            PaletteState.Disabled => ImageStates.ImageDisabled,
            PaletteState.Normal => ImageStates.ImageNormal,
            PaletteState.Pressed => ImageStates.ImagePressed,
            PaletteState.Tracking => ImageStates.ImageTracking,
            _ => null
        };

        // If there is no image then use the generic image
        image ??= Image;

        // Do we need to create another composite image?
        if ((_sourceImage != image) || (_compositeImage == null))
        {
            // Remember image used to create the composite image
            _sourceImage = image;

            if (image == null)
            {
                _compositeImage = null;
            }
            else
            {
                // Create a copy of the source image
                Size selectedRectSize = _selectedRect.Size;
                Size imageSize = image.Size;
                var copyBitmap = new Bitmap(image, Math.Max(selectedRectSize.Width, imageSize.Width),
                    Math.Max(selectedRectSize.Height, imageSize.Height));

                // Paint over the image with a color indicator
                using (Graphics g = Graphics.FromImage(copyBitmap))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    // If the color is not defined, i.e. it is empty then...
                    if (_selectedColor.Equals(GlobalStaticValues.EMPTY_COLOR))
                    {
                        // Indicate the absence of a color by drawing a border around 
                        // the selected color area, thus indicating the area inside the
                        // block is blank/empty.
                        using var borderPen = new Pen(_emptyBorderColor);
                        DrawRoundedRectangle(g, borderPen, _selectedRect, _roundedCorners);
                    }
                    else
                    {
                        // We have a valid selected color so draw a solid block of color
                        using var colorBrush = new SolidBrush(_selectedColor);
                        FillRoundedRectangle(g, colorBrush, _selectedRect, _roundedCorners);
                    }
                }

                // Cache it for future use
                _compositeImage = copyBitmap;
            }
        }

        return _compositeImage;
    }

    private static void DrawRoundedRectangle(Graphics g, Pen pen, Rectangle rect, int radius)
    {
        var roundRect = new RoundedRectangleF(rect.Width - 1, rect.Height - 1, radius, rect.X, rect.Y);
        g.DrawPath(pen, roundRect.Path);
    }

    private static void FillRoundedRectangle(Graphics g, Brush brush, Rectangle rect, int radius)
    {
        var roundRect = new RoundedRectangleF(rect.Width - 1, rect.Height - 1, radius, rect.X, rect.Y);
        g.FillPath(brush, roundRect.Path);
    }

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    public virtual string GetShortText() => Text;

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    public virtual string GetLongText() => ExtraText;

    #endregion
}