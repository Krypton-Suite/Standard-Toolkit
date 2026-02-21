#region BSD License
/*
 *  
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for overlay image value information.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class OverlayImageValues : Storage
{
    #region Instance Fields

    private Image? _image;
    private Color _transparent;
    private OverlayImagePosition _position;
    private OverlayImageScaleMode _scaleMode;
    private float _scaleFactor;
    private Size _fixedSize;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the OverlayImageValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public OverlayImageValues(NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        Reset();
    }

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (Image == null) &&
                                      (ImageTransparentColor == GlobalStaticValues.EMPTY_COLOR) &&
                                      (Position == OverlayImagePosition.TopRight) &&
                                      (ScaleMode == OverlayImageScaleMode.None) &&
                                      (ScaleFactor == 0.5f) &&
                                      (FixedSize == new Size(16, 16));


    #endregion

    #region Image

    /// <summary>
    /// Gets and sets the overlay image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Overlay image to display on top of the main image.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(null)]
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

    private bool ShouldSerializeImage() => Image != null;

    /// <summary>
    /// Resets the Image property to its default value.
    /// </summary>
    public void ResetImage() => Image = null;
    
    #endregion

    #region ImageTransparentColor
    
    /// <summary>
    /// Gets and sets the overlay image transparent color.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Overlay image transparent color.")]
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
    
    #endregion

    #region Position

    /// <summary>
    /// Gets and sets the position of the overlay image relative to the main image.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Position of the overlay image relative to the main image.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(OverlayImagePosition.TopRight)]
    public OverlayImagePosition Position
    {
        get => _position;

        set
        {
            if (_position != value)
            {
                _position = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializePosition() => Position != OverlayImagePosition.TopRight;

    /// <summary>
    /// Resets the Position property to its default value.
    /// </summary>
    public void ResetPosition() => Position = OverlayImagePosition.TopRight;
    
    #endregion

    #region ScaleMode

    /// <summary>
    /// Gets and sets the scaling mode for the overlay image.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"How the overlay image should be scaled relative to the main image.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(OverlayImageScaleMode.None)]
    public OverlayImageScaleMode ScaleMode
    {
        get => _scaleMode;

        set
        {
            if (_scaleMode != value)
            {
                _scaleMode = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeScaleMode() => ScaleMode != OverlayImageScaleMode.None;

    /// <summary>
    /// Resets the ScaleMode property to its default value.
    /// </summary>
    public void ResetScaleMode() => ScaleMode = OverlayImageScaleMode.None;
    
    #endregion

    #region ScaleFactor

    /// <summary>
    /// Gets and sets the scale factor when ScaleMode is Percentage or ProportionalToMain.
    /// Value represents a multiplier (e.g., 0.5 = 50% of main image size, 1.0 = 100%).
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Scale factor for percentage-based scaling (0.0 to 1.0, where 0.5 = 50% of main image size).")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(0.5f)]
    public float ScaleFactor
    {
        get => _scaleFactor;

        set
        {
            // Clamp between 0.1 and 2.0 for reasonable scaling
            float clampedValue = Math.Max(0.1f, Math.Min(2.0f, value));
            if (Math.Abs(_scaleFactor - clampedValue) > float.Epsilon)
            {
                _scaleFactor = clampedValue;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeScaleFactor() => Math.Abs(ScaleFactor - 0.5f) > float.Epsilon;

    /// <summary>
    /// Resets the ScaleFactor property to its default value.
    /// </summary>
    public void ResetScaleFactor() => ScaleFactor = 0.5f;
    
    #endregion

    #region FixedSize

    /// <summary>
    /// Gets and sets the fixed size when ScaleMode is FixedSize.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Fixed size for the overlay image when ScaleMode is FixedSize.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(typeof(Size), "16, 16")]
    public Size FixedSize
    {
        get => _fixedSize;

        set
        {
            if (_fixedSize != value)
            {
                // Ensure minimum size of 1x1
                _fixedSize = new Size(Math.Max(1, value.Width), Math.Max(1, value.Height));
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeFixedSize() => FixedSize != new Size(16, 16);

    /// <summary>
    /// Resets the FixedSize property to its default value.
    /// </summary>
    public void ResetFixedSize() => FixedSize = new Size(16, 16);
    
    #endregion

    #region Reset
    
    public void Reset()
    {
        // Set initial values
        _image = null;
        _transparent = GlobalStaticValues.EMPTY_COLOR;
        _position = OverlayImagePosition.TopRight;
        _scaleMode = OverlayImageScaleMode.None;
        _scaleFactor = 0.5f; // Default to 50% of main image
        _fixedSize = new Size(16, 16); // Default fixed size
    }

    #endregion

    #region CopyFrom

    /// <summary>
    /// Value copy from the provided source to ourself.
    /// </summary>
    /// <param name="source">Source instance.</param>
    public void CopyFrom(OverlayImageValues source)
    {
        Image = source.Image;
        ImageTransparentColor = source.ImageTransparentColor;
        Position = source.Position;
        ScaleMode = source.ScaleMode;
        ScaleFactor = source.ScaleFactor;
        FixedSize = source.FixedSize;
    }

    #endregion

    public override string ToString() => !IsDefault ? @"Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;
}
