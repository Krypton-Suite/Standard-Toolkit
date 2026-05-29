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
/// Navigator view element for drawing a selected check button for the Outlook mini mode.
/// </summary>
internal class ViewDrawNavOutlookMini : ViewDrawNavCheckButtonBase
{
    #region Instance Fields
    private OutlookMiniController _controller;
    private readonly EventHandler _finishDelegate;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawNavOutlookSelected class.
    /// </summary>
    /// <param name="navigator">Owning navigator instance.</param>
    /// <param name="page">Page this check button represents.</param>
    /// <param name="orientation">Orientation for the check button.</param>
    public ViewDrawNavOutlookMini(KryptonNavigator navigator,
        KryptonPage? page,
        VisualOrientation orientation)
        : base(navigator, page, orientation,
            navigator.StateDisabled.MiniButton,
            navigator.StateNormal.MiniButton,
            navigator.StateTracking.MiniButton,
            navigator.StatePressed.MiniButton,
            navigator.StateSelected.MiniButton,
            navigator.OverrideFocus.MiniButton) =>
        // Create the finish handler for when popup is removed
        _finishDelegate = OnPopupFinished;

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawNavOutlookMini:{Id} Text:{Page!.Text}";

    #endregion

    #region Page
    /// <summary>
    /// Gets the page this view represents.
    /// </summary>
    public override KryptonPage? Page
    {
        set
        {
            base.Page = value;

            if (Page != null)
            {
                _overrideDisabled.SetPalettes(Page!.OverrideFocus.MiniButton, Page!.StateDisabled.MiniButton);
                _overrideNormal.SetPalettes(Page!.OverrideFocus.MiniButton, Page!.StateNormal.MiniButton);
                _overrideTracking.SetPalettes(Page!.OverrideFocus.MiniButton, Page!.StateTracking.MiniButton);
                _overridePressed.SetPalettes(Page!.OverrideFocus.MiniButton, Page!.StatePressed.MiniButton);
                _overrideSelected.SetPalettes(Page!.OverrideFocus.MiniButton, Page!.StateSelected.MiniButton);
            }
            else
            {
                _overrideDisabled.SetPalettes(Navigator.OverrideFocus.MiniButton, Navigator.StateDisabled.MiniButton);
                _overrideNormal.SetPalettes(Navigator.OverrideFocus.MiniButton, Navigator.StateNormal.MiniButton);
                _overrideTracking.SetPalettes(Navigator.OverrideFocus.MiniButton, Navigator.StateTracking.MiniButton);
                _overridePressed.SetPalettes(Navigator.OverrideFocus.MiniButton, Navigator.StatePressed.MiniButton);
                _overrideSelected.SetPalettes(Navigator.OverrideFocus.MiniButton, Navigator.StateSelected.MiniButton);
            }
        }
    }
    #endregion

    #region AllowButtonSpecs
    /// <summary>
    /// Gets a value indicating if button specs are allowed on the button.
    /// </summary>
    public override bool AllowButtonSpecs => false;

    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public override Image? GetImage(PaletteState state) => Page?.GetImageMapping(Navigator.Outlook.Mini.MiniMapImage);

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public override string GetShortText() => Page?.GetTextMapping(Navigator.Outlook.Mini.MiniMapText) ?? string.Empty;

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public override string GetLongText() => Page?.GetTextMapping(Navigator.Outlook.Mini.MiniMapExtraText) ?? string.Empty;

    #endregion

    #region CreateMouseController
    /// <summary>
    /// Create a mouse controller appropriate for operating this button.
    /// </summary>
    /// <returns>Reference to IMouseController interface.</returns>
    protected override IMouseController CreateMouseController()
    {
        _controller = new OutlookMiniController(this, OnNeedPaint);
        _controller.Click += OnMiniClick;
        return _controller;
    }
    #endregion

    #region Implementation
    private void OnMiniClick(object? sender, EventArgs e) =>
        // Ask the navigator to show the specified page as a popup window 
        // relative to our location as an element and firing the provided
        // delegate when the popup is dismissed.
        Navigator.ShowPopupPage(Page!, this, _finishDelegate);

    private void OnPopupFinished(object? sender, EventArgs e) =>
        // Remove the fixed display of the button, now the associated popup has been removed
        _controller.RemoveFixed();
    #endregion
}