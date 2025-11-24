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

// ReSharper disable RedundantNullableFlowAttribute
namespace Krypton.Docking;

/// <summary>
/// View element that can draw an auto hidden tab based on a KryptonPage as the source.
/// </summary>
internal class ViewDrawAutoHiddenTab : ViewDrawButton,
    IContentValues
{
    #region Instance Fields

    private readonly VisualOrientation _orientation;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawAutoHiddenTab class.
    /// </summary>
    /// <param name="page">Reference to the page this tab represents.</param>
    /// <param name="orientation">Visual orientation used for drawing the tab.</param>
    public ViewDrawAutoHiddenTab([DisallowNull] KryptonPage page,
        VisualOrientation orientation)
        : base(page.StateDisabled.CheckButton,
            page.StateNormal.CheckButton,
            page.StateTracking.CheckButton,
            page.StatePressed.CheckButton,
            null, null, orientation, false)
    {
        Page = page;
        _orientation = orientation;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the page associated with the view.
    /// </summary>
    public KryptonPage Page { get; }

    #endregion

    #region Public IContentValues
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state) => Page.ImageSmall;

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => Page.Text;

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => string.Empty;

    #endregion
}