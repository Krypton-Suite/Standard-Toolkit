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
/// Storage for check-button overlay image value information, including checked-state images.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class CheckOverlayImageValues : OverlayImageValues
{
    #region Instance Fields
    private CheckOverlayImageStates _imageStates = null!;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the CheckOverlayImageValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public CheckOverlayImageValues(NeedPaintHandler needPaint)
        : base(needPaint)
    {
    }
    #endregion

    #region CreateImageStates
    /// <summary>
    /// Create the storage for the overlay image states.
    /// </summary>
    /// <returns>Storage object.</returns>
    protected override OverlayImageStates CreateImageStates()
    {
        _imageStates = new CheckOverlayImageStates();
        return _imageStates;
    }
    #endregion

    #region GetImage
    /// <summary>
    /// Gets the overlay image for the specified palette state.
    /// </summary>
    /// <param name="state">The state for which the overlay image is needed.</param>
    /// <returns>Overlay image value, or null if no overlay image is set.</returns>
    public override Image? GetImage(PaletteState state)
    {
        // Try and get a checked-state specific image
        Image? image = state switch
        {
            PaletteState.CheckedNormal => _imageStates.ImageCheckedNormal,
            PaletteState.CheckedPressed => _imageStates.ImageCheckedPressed,
            PaletteState.CheckedTracking => _imageStates.ImageCheckedTracking,
            _ => null
        };

        return image ?? base.GetImage(state);
    }
    #endregion
}
