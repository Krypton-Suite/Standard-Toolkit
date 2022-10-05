﻿namespace Krypton.Navigator
{
    /// <summary>
    /// Implementation for the fixed next button for navigator.
    /// </summary>
    public class ButtonSpecNavNext : ButtonSpecNavFixed
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecNavNext class.
        /// </summary>
        /// <param name="navigator">Reference to owning navigator instance.</param>
        public ButtonSpecNavNext(KryptonNavigator navigator)
            : base(navigator, PaletteButtonSpecStyle.Next)
        {
        }
        #endregion

        #region IButtonSpecValues
        /// <summary>
        /// Gets the button visible value.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button visibiliy.</returns>
        public override bool GetVisible(IPalette palette)
        {
            switch (Navigator.Button.NextButtonDisplay)
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
                            return false;
                    }
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return false;
            }
        }

        /// <summary>
        /// Gets the button enabled state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button enabled state.</returns>
        public override ButtonEnabled GetEnabled(IPalette palette)
        {
            switch (Navigator.Button.NextButtonDisplay)
            {
                case ButtonDisplay.Hide:
                case ButtonDisplay.ShowDisabled:
                    // Always disabled
                    return ButtonEnabled.False;
                case ButtonDisplay.ShowEnabled:
                    // Always enabled
                    return ButtonEnabled.True;
                case ButtonDisplay.Logic:
                    return Navigator.ViewBuilder.NextActionEnabled(Navigator.Button.NextButtonAction);
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return ButtonEnabled.False;
            }

        }

        /// <summary>
        /// Gets the button checked state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button checked state.</returns>
        public override ButtonCheckState GetChecked(IPalette palette) =>
            // Next button is never shown as checked
            ButtonCheckState.NotCheckButton;

        #endregion    
    }
}
