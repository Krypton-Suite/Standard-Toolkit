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
/// Implementation for the fixed context button for navigator.
/// </summary>
public class ButtonSpecNavContext : ButtonSpecNavFixed
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecNavContext class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    public ButtonSpecNavContext(KryptonNavigator navigator)
        : base(navigator, PaletteButtonSpecStyle.Context)
    {
        // Provide a context menu so that the button looks fixed when pressed
        // and the menu is shown. We add an items child so it has some content
        KryptonContextMenu = new KryptonContextMenu();
        KryptonContextMenu.Items.Add(new KryptonContextMenuItems());
    }
    #endregion

    #region IButtonSpecValues
    /// <summary>
    /// Gets the button visible value.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button visibiliy.</returns>
    public override bool GetVisible(PaletteBase palette)
    {
        switch (Navigator.Button.ContextButtonDisplay)
        {
            case ButtonDisplay.Hide:
                // Always hide
                return false;
            case ButtonDisplay.ShowDisabled:
            case ButtonDisplay.ShowEnabled:
                // Always show
                return true;

            case ButtonDisplay.Logic:
                // Use button display logic to determine actual operation
                switch (Navigator.Button.ButtonDisplayLogic)
                {
                    case ButtonDisplayLogic.None:
                    case ButtonDisplayLogic.NextPrevious:
                        return false;
                    case ButtonDisplayLogic.Context:
                    case ButtonDisplayLogic.ContextNextPrevious:
                        return true;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(Navigator.Button.ButtonDisplayLogic.ToString());
                        return false;
                }

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(Navigator.Button.ContextButtonDisplay.ToString());
                return false;
        }
    }

    /// <summary>
    /// Gets the button enabled state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button enabled state.</returns>
    public override ButtonEnabled GetEnabled(PaletteBase palette)
    {
        switch (Navigator.Button.ContextButtonDisplay)
        {
            case ButtonDisplay.Hide:
            case ButtonDisplay.ShowDisabled:
                // Always disabled
                return ButtonEnabled.False;
            case ButtonDisplay.ShowEnabled:
                // Always enabled
                return ButtonEnabled.True;
            case ButtonDisplay.Logic:
                // Only enabled if there is a selected page
                return (Navigator.SelectedPage != null ? ButtonEnabled.True : ButtonEnabled.False);
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(Navigator.Button.ContextButtonDisplay.ToString());
                return ButtonEnabled.False;
        }

    }

    /// <summary>
    /// Gets the button checked state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button checked state.</returns>
    public override ButtonCheckState GetChecked(PaletteBase? palette) =>
        // Context button is never shown as checked
        ButtonCheckState.NotCheckButton;

    #endregion    
}