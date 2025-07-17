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

namespace Krypton.Navigator;

/// <summary>
/// Storage and mapping for secondary header.
/// </summary>
public class HeaderGroupMappingSecondary : HeaderGroupMappingBase
{
    #region Static Fields
    private const string _defaultDescription = " ";
    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the HeaderGroupMappingSecondary class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="getDpiFactor"></param>
    public HeaderGroupMappingSecondary(KryptonNavigator navigator,
        NeedPaintHandler needPaint,
        GetDpiFactor getDpiFactor)
        : base(navigator, needPaint, getDpiFactor)
    {
    }
    #endregion

    #region Default Values
    /// <summary>
    /// Gets the default image value.
    /// </summary>
    /// <returns>Image reference.</returns>
    protected override Image? GetImageDefault() => null;

    /// <summary>
    /// Gets the default heading value.
    /// </summary>
    /// <returns>String reference.</returns>
    protected override string GetHeadingDefault() => _defaultDescription;

    /// <summary>
    /// Gets the default description value.
    /// </summary>
    /// <returns>String reference.</returns>
    protected override string GetDescriptionDefault() => string.Empty;

    /// <summary>
    /// Gets the default image mapping value.
    /// </summary>
    /// <returns>Image mapping enumeration.</returns>
    protected override MapKryptonPageImage GetMapImageDefault() => MapKryptonPageImage.None;

    /// <summary>
    /// Gets the default heading mapping value.
    /// </summary>
    /// <returns>Text mapping enumeration.</returns>
    protected override MapKryptonPageText GetMapHeadingDefault() => MapKryptonPageText.Description;

    /// <summary>
    /// Gets the default description mapping value.
    /// </summary>
    /// <returns>Text mapping enumeration.</returns>
    protected override MapKryptonPageText GetMapDescriptionDefault() => MapKryptonPageText.None;

    #endregion

    #region MapImage
    /// <summary>
    /// Gets and sets the mapping used for the Image property.
    /// </summary>
    //[DefaultValue(typeof(MapKryptonPageImage), "None (Null image)")]
    public override MapKryptonPageImage MapImage
    {
        get => base.MapImage;
        set => base.MapImage = value;
    }
    #endregion

    #region MapHeading
    /// <summary>
    /// Gets and sets the mapping used for the Heading property.
    /// </summary>
    //[DefaultValue(typeof(MapKryptonPageText), "Description")]
    public override MapKryptonPageText MapHeading
    {
        get => base.MapHeading;
        set => base.MapHeading = value;
    }
    #endregion

    #region MapDescription
    /// <summary>
    /// Gets and sets the mapping used for the Description property.
    /// </summary>
    //[DefaultValue(typeof(MapKryptonPageText), "None (Empty string)")]
    public override MapKryptonPageText MapDescription
    {
        get => base.MapDescription;
        set => base.MapDescription = value;
    }
    #endregion
}