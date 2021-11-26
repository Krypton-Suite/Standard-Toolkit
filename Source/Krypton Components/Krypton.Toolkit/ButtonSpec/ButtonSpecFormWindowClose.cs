#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    /// <summary>
    /// Implementation for the fixed close button for krypton form.
    /// </summary>
    public class ButtonSpecFormWindowClose : ButtonSpecFormFixed
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecFormWindowClose class.
        /// </summary>
        /// <param name="form">Reference to owning krypton form instance.</param>
        public ButtonSpecFormWindowClose(KryptonForm form)
            : base(form, PaletteButtonSpecStyle.FormClose)
        {
        }         
        #endregion

        #region IButtonSpecValues
        /// <summary>
        /// Gets the button visible value.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button visibility.</returns>
        public override bool GetVisible(IPalette palette)
        {
            // We do not show if the custom chrome is combined with composition,
            // in which case the form buttons are handled by the composition
            if (KryptonForm.ApplyComposition && KryptonForm.ApplyCustomChrome)
            {
                return false;
            }

            // Have all buttons been turned off?
            return KryptonForm.ControlBox;
        }

        /// <summary>
        /// Gets the button enabled state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button enabled state.</returns>
        public override ButtonEnabled GetEnabled(IPalette palette) => ButtonEnabled.True;

        /// <summary>
        /// Gets the button checked state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button checked state.</returns>
        public override ButtonCheckState GetChecked(IPalette palette) =>
            // Close button is never shown as checked
            ButtonCheckState.NotCheckButton;

        #endregion    

        #region Protected Overrides
        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            // Only if associated view is enabled to we perform an action
            if (GetViewEnabled())
            {
                // If we do not provide an inert form
                if (!KryptonForm.InertForm)
                {
                    // Only if the mouse is still within the button bounds do we perform action
                    MouseEventArgs mea = (MouseEventArgs)e;
                    if (GetView().ClientRectangle.Contains(mea.Location))
                    {
                        PropertyInfo pi = typeof(Form).GetProperty("CloseReason",
                                                                    BindingFlags.Instance |
                                                                    BindingFlags.SetProperty |
                                                                    BindingFlags.NonPublic);

                        // Update form with the reason for the close
                        pi.SetValue(KryptonForm, CloseReason.UserClosing, null);

                        // Convert screen position to LPARAM format of WM_SYSCOMMAND message
                        Point screenPos = Control.MousePosition;
                        IntPtr lParam = (IntPtr)(PI.MAKELOWORD(screenPos.X) |
                                                 PI.MAKEHIWORD(screenPos.Y));

                        // Request the form be closed down
                        KryptonForm.SendSysCommand(PI.SC_.CLOSE, lParam);

                        // Let base class fire any other attached events
                        base.OnClick(e);
                    }
                }
            }
        }
        #endregion
    }
}
