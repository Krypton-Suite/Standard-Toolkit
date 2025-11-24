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

// ReSharper disable VirtualMemberCallInConstructor
namespace Krypton.Navigator;

/// <summary>
/// Base class for storage and mapping of navigator header values.
/// </summary>
public abstract class HeaderGroupMappingBase : HeaderValuesBase
{
    #region Instance Fields
    private readonly KryptonNavigator _navigator;
    private MapKryptonPageText _mapHeading;
    private MapKryptonPageText _mapDescription;
    private MapKryptonPageImage _mapImage;
    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the HeaderGroupMappingBase class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="getDpiFactor"></param>
    protected HeaderGroupMappingBase([DisallowNull] KryptonNavigator navigator,
        NeedPaintHandler needPaint, GetDpiFactor getDpiFactor)
        : base(needPaint, getDpiFactor)
    {
        Debug.Assert(navigator is not null);

        // Remember back reference to owning control
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

        // Set initial values to the default
        _mapImage = GetMapImageDefault();
        _mapHeading = GetMapHeadingDefault();
        _mapDescription = GetMapDescriptionDefault();
    }
    #endregion

    #region Default Values
    /// <summary>
    /// Gets the default image value.
    /// </summary>
    /// <returns>Image reference.</returns>
    protected override Image? GetImageDefault() => null;

    /// <summary>
    /// Gets the default image mapping value.
    /// </summary>
    /// <returns>Image mapping enumeration.</returns>
    protected abstract MapKryptonPageImage GetMapImageDefault();

    /// <summary>
    /// Gets the default heading mapping value.
    /// </summary>
    /// <returns>Text mapping enumeration.</returns>
    protected abstract MapKryptonPageText GetMapHeadingDefault();

    /// <summary>
    /// Gets the default description mapping value.
    /// </summary>
    /// <returns>Text mapping enumeration.</returns>
    protected abstract MapKryptonPageText GetMapDescriptionDefault();
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (base.IsDefault &&
                                       (MapImage == GetMapImageDefault()) &&
                                       (MapHeading == GetMapHeadingDefault()) &&
                                       (MapDescription == GetMapDescriptionDefault()));

    #endregion

    #region GetImage
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">State for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public override Image? GetImage(PaletteState state)
    {
        // If there is no selected page to map from or if the
        // mapping indicates to always use the static image
        if ((_navigator.SelectedPage == null) ||
            (MapImage == MapKryptonPageImage.None))
        {
            return Image;
        }

        // Ask the page to provide the image mapping
        return _navigator.SelectedPage.GetImageMapping(MapImage);
    }
    #endregion

    #region GetShortText
    /// <summary>
    /// Gets the content short text.
    /// </summary>
    public override string GetShortText()
    {
        // If there is no selected page to map from or if the
        // mapping indicates to always use the static text
        if ((_navigator.SelectedPage == null) ||
            (MapHeading == MapKryptonPageText.None))
        {
            return Heading;
        }

        // Ask the page to provide the text mapping
        return _navigator.SelectedPage.GetTextMapping(MapHeading);
    }
    #endregion

    #region GetLongText
    /// <summary>
    /// Gets the content long text.
    /// </summary>
    public override string GetLongText()
    {
        // If there is no selected page to map from or if the
        // mapping indicates to always use the static text
        if ((_navigator.SelectedPage == null) ||
            (MapDescription == MapKryptonPageText.None))
        {
            return Description;
        }

        // Ask the page to provide the text mapping
        return _navigator.SelectedPage.GetTextMapping(MapDescription);
    }
    #endregion

    #region MapImage
    /// <summary>
    /// Gets and sets the mapping used for the Image property.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Mapping used for the image.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual MapKryptonPageImage MapImage
    {
        get => _mapImage;

        set
        {
            if (_mapImage != value)
            {
                _mapImage = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMapImage() => MapImage != GetMapImageDefault();

    /// <summary>
    /// Resets the MapImage property to its default value.
    /// </summary>
    public void ResetMapImage() => MapImage = GetMapImageDefault();
    #endregion

    #region MapHeading
    /// <summary>
    /// Gets and sets the mapping used for the Heading property.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Mapping used for the heading.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual MapKryptonPageText MapHeading
    {
        get => _mapHeading;

        set
        {
            if (_mapHeading != value)
            {
                _mapHeading = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMapHeading() => MapHeading != GetMapHeadingDefault();

    /// <summary>
    /// Resets the MapHeading property to its default value.
    /// </summary>
    public void ResetMapHeading() => MapHeading = GetMapHeadingDefault();
    #endregion

    #region MapDescription
    /// <summary>
    /// Gets and sets the mapping used for the Description property.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Mapping used for the description.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual MapKryptonPageText MapDescription
    {
        get => _mapDescription;

        set
        {
            if (_mapDescription != value)
            {
                _mapDescription = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMapDescription() => MapDescription != GetMapDescriptionDefault();

    /// <summary>
    /// Resets the MapDescription property to its default value.
    /// </summary>
    public void ResetMapDescription() => MapDescription = GetMapDescriptionDefault();
    #endregion
}