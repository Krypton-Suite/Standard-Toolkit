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
/// Navigator view element for drawing a stack check button for the Outlook mode.
/// </summary>
internal class ViewDrawNavOutlookStack : ViewDrawNavCheckButtonBase
{
    #region Instance Fields
    private readonly bool _full;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawNavOutlookStack class.
    /// </summary>
    /// <param name="navigator">Owning navigator instance.</param>
    /// <param name="page">Page this check button represents.</param>
    /// <param name="orientation">Orientation for the check button.</param>
    public ViewDrawNavOutlookStack(KryptonNavigator navigator,
        KryptonPage? page,
        VisualOrientation orientation)
        : base(navigator, page, orientation) =>
        // Are we mapping for the full or the mini mode?
        _full = (navigator.NavigatorMode == NavigatorMode.OutlookFull);

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawNavOutlookStack:{Id} Text:{Page!.Text}";

    #endregion

    #region UpdateButtonSpecMapping
    /// <summary>
    /// Update the button spec manager mapping to reflect current settings.
    /// </summary>
    public override void UpdateButtonSpecMapping()
    {
        // Define a default mapping for text color and recreate to use that new setting
        ButtonSpecManager?.SetRemapTarget(Navigator.Outlook.CheckButtonStyle);
        ButtonSpecManager?.RecreateButtons();
    }
    #endregion

    #region AllowButtonSpecs
    /// <summary>
    /// Gets a value indicating if button specs are allowed on the button.
    /// </summary>
    public override bool AllowButtonSpecs => (Navigator.NavigatorMode == NavigatorMode.OutlookFull);

    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public override Image? GetImage(PaletteState state) =>
        Page?.GetImageMapping(_full ? Navigator.Outlook.Full.StackMapImage :
            Navigator.Outlook.Mini.StackMapImage);

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public override string GetShortText() =>
        Page!.GetTextMapping(_full ? Navigator.Outlook.Full.StackMapText :
            Navigator.Outlook.Mini.StackMapText);

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public override string GetLongText() =>
        Page!.GetTextMapping(_full ? Navigator.Outlook.Full.StackMapExtraText :
            Navigator.Outlook.Mini.StackMapExtraText);

    #endregion
}