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
/// Implementation for the fixed previous button for navigator.
/// </summary>
public class ButtonSpecNavPrevious : ButtonSpecNavFixed
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecNavPrevious class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    public ButtonSpecNavPrevious(KryptonNavigator navigator)
        : base(navigator, PaletteButtonSpecStyle.Previous)
    {
    }
    #endregion

    #region IButtonSpecValues
    /// <summary>
    /// Gets the button visible value.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button visibility.</returns>
    public override bool GetVisible(PaletteBase palette)
    {
        switch (Navigator.Button.PreviousButtonDisplay)
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
                    case ButtonDisplayLogic.Context:
                        return false;
                    case ButtonDisplayLogic.NextPrevious:
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
                DebugTools.NotImplemented(Navigator.Button.PreviousButtonDisplay.ToString());
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
        switch (Navigator.Button.PreviousButtonDisplay)
        {
            case ButtonDisplay.Hide:
            case ButtonDisplay.ShowDisabled:
                // Always disabled
                return ButtonEnabled.False;
            case ButtonDisplay.ShowEnabled:
                // Always enabled
                return ButtonEnabled.True;
            case ButtonDisplay.Logic:
                return Navigator.ViewBuilder!.PreviousActionEnabled(Navigator.Button.PreviousButtonAction);
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(Navigator.Button.PreviousButtonDisplay.ToString());
                return ButtonEnabled.False;
        }
    }

    /// <summary>
    /// Gets the button checked state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button checked state.</returns>
    public override ButtonCheckState GetChecked(PaletteBase? palette) =>
        // Previous button is never shown as checked
        ButtonCheckState.NotCheckButton;

    #endregion    
}