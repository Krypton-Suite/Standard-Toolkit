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
/// Navigator view element for drawing a stack check button for a krypton page.
/// </summary>
internal class ViewDrawNavCheckButtonStack : ViewDrawNavCheckButtonBase
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawNavCheckButtonStack class.
    /// </summary>
    /// <param name="navigator">Owning navigator instance.</param>
    /// <param name="page">Page this check button represents.</param>
    /// <param name="orientation">Orientation for the check button.</param>
    public ViewDrawNavCheckButtonStack(KryptonNavigator navigator,
        KryptonPage? page,
        VisualOrientation orientation)
        : base(navigator, page, orientation)
    {
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawNavCheckButtonStack:{Id}";

    #endregion

    #region UpdateButtonSpecMapping
    /// <summary>
    /// Update the button spec manager mapping to reflect current settings.
    /// </summary>
    public override void UpdateButtonSpecMapping()
    {
        // Define a default mapping for text color and recreate to use that new setting
        ButtonSpecManager?.SetRemapTarget(Navigator.Stack.CheckButtonStyle);
        ButtonSpecManager?.RecreateButtons();
    }
    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public override Image? GetImage(PaletteState state) => Page?.GetImageMapping(Navigator.Stack.StackMapImage);

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public override string GetShortText() => Page?.GetTextMapping(Navigator.Stack.StackMapText)!;

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public override string GetLongText() => Page?.GetTextMapping(Navigator.Stack.StackMapExtraText)!;

    #endregion
}