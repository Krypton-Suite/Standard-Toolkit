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
/// Storage for header content value information.
/// </summary>
public abstract class HeaderValuesBase : Storage,
    IContentValues
{
    #region Static Fields

    private static readonly Image? _defaultImage = GenericImageResources.KryptonLogoGeneric;

    #endregion

    #region Instance Fields
    private Image? _image;
    private Color _transparent;
    private string? _heading;
    private string _description;
    private readonly GetDpiFactor _getDpiFactor;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the Text property changes.
    /// </summary>
    public event EventHandler? TextChanged;
    #endregion

    #region Delegates
    /// <summary>
    /// Signature of method that is called when scaling an image is required.
    /// </summary>
    public delegate float GetDpiFactor();
    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the HeaderValuesBase class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="getDpiFactor"></param>
    protected HeaderValuesBase(NeedPaintHandler? needPaint, GetDpiFactor getDpiFactor)
    {
        _getDpiFactor = getDpiFactor;
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Set initial values to the default
        _image = GetImageDefault();
        _transparent = GlobalStaticValues.EMPTY_COLOR;
        _heading = GetHeadingDefault();
        _description = GetDescriptionDefault();
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializeImage()
                                      && !ShouldSerializeImageTransparentColor()
                                      && !ShouldSerializeHeading()
                                      && !ShouldSerializeDescription();

    #endregion

    #region Default Values

    /// <summary>
    /// Gets the default image value.
    /// </summary>
    /// <returns>Image reference.</returns>
    protected virtual Image? GetImageDefault() => _defaultImage;

    /// <summary>
    /// Gets the default heading value.
    /// </summary>
    /// <returns>String reference.</returns>
    protected abstract string GetHeadingDefault();

    /// <summary>
    /// Gets the default description value.
    /// </summary>
    /// <returns>String reference.</returns>
    protected abstract string GetDescriptionDefault();
    #endregion

    #region Image
    /// <summary>
    /// Gets and sets the heading image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Heading image.")]
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

    private bool ShouldSerializeImage() => _image != GetImageDefault();
    protected internal void ResetImage() => _image = GetImageDefault();

    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public virtual Image? GetImage(PaletteState state)
    {
        float dpiFactor = _getDpiFactor();
        return (_image != null)
            ? CommonHelper.ScaleImageForSizedDisplay(_image, _image.Width * dpiFactor,
                _image.Height * dpiFactor, false)
            : null;
    }
    #endregion

    #region ImageTransparentColor
    /// <summary>
    /// Gets and sets the heading image transparent color.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Heading image transparent color.")]
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
    protected internal void ResetImageTransparentColor() => ImageTransparentColor = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the content image transparent color.
    /// </summary>
    /// <param name="state">The state for which the image color is needed.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetImageTransparentColor(PaletteState state) => ImageTransparentColor;

    #endregion

    #region Heading
    /// <summary>
    /// Gets and sets the heading text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Heading text.")]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [AllowNull]
    public virtual string Heading
    {
        get => _heading ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;

        set
        {
            if (_heading != value)
            {
                _heading = value;
                PerformNeedPaint(true);
                TextChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool ShouldSerializeHeading() => Heading != GetHeadingDefault();
    public /*internal*/ void ResetHeading() => Heading = GetHeadingDefault();

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    public virtual string GetShortText() => Heading;

    #endregion

    #region Description
    /// <summary>
    /// Gets and sets the header description text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Header description text.")]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    public virtual string Description
    {
        get => _description;

        set
        {
            if (_description != value)
            {
                _description = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeDescription() => Description != GetDescriptionDefault();
    protected internal void ResetDescription() => Description = GetDescriptionDefault();

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    public virtual string GetLongText() => Description;

    #endregion
}