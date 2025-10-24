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
/// Implementation for the fixed close button for navigator.
/// </summary>
public class ButtonSpecNavClose : ButtonSpecNavFixed
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecNavClose class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    public ButtonSpecNavClose(KryptonNavigator navigator)
        : base(navigator, PaletteButtonSpecStyle.Close)
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
        switch (Navigator.Button.CloseButtonDisplay)
        {
            case ButtonDisplay.Hide:
                // Always hide
                return false;
            case ButtonDisplay.ShowDisabled:
            case ButtonDisplay.ShowEnabled:
                // Always show
                return true;
            case ButtonDisplay.Logic:
                return true;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(Navigator.Button.CloseButtonDisplay.ToString());
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
        switch (Navigator.Button.CloseButtonDisplay)
        {
            case ButtonDisplay.Hide:
            case ButtonDisplay.ShowDisabled:
                // Always disabled
                return ButtonEnabled.False;
            case ButtonDisplay.ShowEnabled:
                // Always enabled
                return ButtonEnabled.True;
            case ButtonDisplay.Logic:
                // Only enabled if a page is selected
                return (Navigator.SelectedPage != null) ? ButtonEnabled.True : ButtonEnabled.False;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(Navigator.Button.CloseButtonDisplay.ToString());
                return ButtonEnabled.False;
        }
    }

    /// <summary>
    /// Gets the button checked state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button checked state.</returns>
    public override ButtonCheckState GetChecked(PaletteBase? palette) =>
        // Close button is never shown as checked
        ButtonCheckState.NotCheckButton;

    #endregion
}