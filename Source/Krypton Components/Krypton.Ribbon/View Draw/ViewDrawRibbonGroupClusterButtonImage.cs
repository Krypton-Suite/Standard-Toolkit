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
/// Draws a small image from a group cluster button.
/// </summary>
internal class ViewDrawRibbonGroupClusterButtonImage : ViewDrawRibbonGroupImageBase

{
    #region Instance Fields
    private readonly Size _smallSize;// = new Size(16, 16);
    private readonly KryptonRibbonGroupClusterButton _ribbonButton;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupClusterButtonImage class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="ribbonButton">Reference to ribbon group button definition.</param>
    public ViewDrawRibbonGroupClusterButtonImage([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroupClusterButton? ribbonButton)
        : base(ribbon)
    {
        Debug.Assert(ribbonButton is not null);

        _ribbonButton = ribbonButton ?? throw new ArgumentNullException(nameof(ribbonButton));
        _smallSize = new Size((int)(16 * FactorDpiX), (int)(16 * FactorDpiY));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupClusterButtonImage:{Id}";

    #endregion

    #region Protected
    /// <summary>
    /// Gets the size to draw the image.
    /// </summary>
    protected override Size DrawSize => _smallSize;

    /// <summary>
    /// Gets the image to be drawn.
    /// </summary>
    protected override Image? DrawImage => _ribbonButton.KryptonCommand != null ? _ribbonButton.KryptonCommand.ImageSmall : _ribbonButton.ImageSmall;

    #endregion
}