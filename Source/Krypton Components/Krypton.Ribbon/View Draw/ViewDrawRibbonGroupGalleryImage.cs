#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Draws a large image from a gallery.
/// </summary>
internal class ViewDrawRibbonGroupGalleryImage : ViewDrawRibbonGroupImageBase
                                              
{
    #region Instance Fields
    private readonly Size _largeSize; // = new(32, 32);
    private readonly KryptonRibbonGroupGallery _ribbonGallery;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupGalleryImage class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="ribbonGallery">Reference to ribbon group gallery definition.</param>
    public ViewDrawRibbonGroupGalleryImage([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroupGallery? ribbonGallery)
        : base(ribbon)
    {
        Debug.Assert(ribbonGallery is not null);

        _ribbonGallery = ribbonGallery ?? throw new ArgumentNullException(nameof(ribbonGallery));
        _largeSize = new Size((int)(32 * FactorDpiX), (int)(32 * FactorDpiY));
    }        

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupGalleryImage:{Id}";

    #endregion

    #region Protected
    /// <summary>
    /// Gets the size to draw the image.
    /// </summary>
    protected override Size DrawSize => _largeSize;

    /// <summary>
    /// Gets the image to be drawn.
    /// </summary>
    protected override Image? DrawImage => _ribbonGallery.ImageLarge;

    #endregion
}