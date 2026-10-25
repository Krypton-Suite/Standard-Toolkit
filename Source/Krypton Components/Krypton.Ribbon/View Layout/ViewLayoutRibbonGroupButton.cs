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
/// Layout area for the group button.
/// </summary>
internal class ViewLayoutRibbonGroupButton : ViewLayoutDocker
{
    #region Instance Fields
    private readonly ViewDrawRibbonGroupDialogButton _groupButton;
    private readonly ViewLayoutRibbonCenter _centerButton;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonGroupButton class.
    /// </summary>
    /// <param name="ribbon">Owning control instance.</param>
    /// <param name="ribbonGroup">Reference to ribbon group this represents.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewLayoutRibbonGroupButton(KryptonRibbon ribbon,
        KryptonRibbonGroup ribbonGroup,
        NeedPaintHandler needPaint)
    {
        _groupButton = new ViewDrawRibbonGroupDialogButton(ribbon, ribbonGroup, needPaint);
        _centerButton = new ViewLayoutRibbonCenter
        {

            // Fill remainder with the actual button, but centered within space
            _groupButton
        };
        Add(_centerButton, ViewDockStyle.Fill);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonGroupButton:{Id}";

    #endregion

    #region DialogButtonController
    /// <summary>
    /// Gets access to the controller used for the button.
    /// </summary>
    public DialogLauncherButtonController? DialogButtonController => _groupButton.DialogButtonController;

    #endregion

    #region GetFocusView
    /// <summary>
    /// Gets the view to use for the group dialog button.
    /// </summary>
    /// <returns>ViewBase if valid as a focus item; otherwise false.</returns>
    public ViewBase GetFocusView()
    {
        if (Visible && Enabled && _groupButton is { Visible: true, Enabled: true })
        {
            return _groupButton;
        }
        else
        {
            return null!;
        }
    }
    #endregion
}