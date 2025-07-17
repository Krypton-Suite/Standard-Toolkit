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

namespace Krypton.Toolkit;

/// <summary>
/// Implementation for the fixed close button for krypton form.
/// </summary>
public class ButtonSpecFormWindowClose : ButtonSpecFormFixed
{
    private bool _enabled = true;

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecFormWindowClose class.
    /// </summary>
    /// <param name="form">Reference to owning krypton form instance.</param>
    public ButtonSpecFormWindowClose(KryptonForm form)
        : base(form, PaletteButtonSpecStyle.FormClose)
    {
    }

    /// <summary>
    /// Form Close Button Enabled: This will also Disable the System Menu `Close` BUT NOT the `Alt+F4` key action
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(true)]
    [Description("Form Close Button Enabled: This will also Disable the System Menu `Close` BUT NOT the `Alt+F4` key action")]
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                var hSystemMenu = PI.GetSystemMenu(KryptonForm.Handle, false);
                if (hSystemMenu != IntPtr.Zero)
                {
                    PI.EnableMenuItem(hSystemMenu, PI.SC_.CLOSE, _enabled ? PI.MF_.ENABLED : PI.MF_.DISABLED);
                }
            }
        }
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
        // Have all buttons been turned off?
        return KryptonForm is { ControlBox: true, CloseBox: true };
    }

    /// <summary>
    /// Gets the button enabled state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button enabled state.</returns>
    public override ButtonEnabled GetEnabled(PaletteBase palette) => KryptonForm.CloseBox && Enabled ? ButtonEnabled.True : ButtonEnabled.False;

    /// <summary>
    /// Gets the button checked state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button checked state.</returns>
    public override ButtonCheckState GetChecked(PaletteBase? palette) =>
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
                var mea = (MouseEventArgs)e;
                if (GetView().ClientRectangle.Contains(mea.Location))
                {
                    PropertyInfo pi = typeof(Form).GetProperty(nameof(CloseReason),
                        BindingFlags.Instance |
                        BindingFlags.SetProperty |
                        BindingFlags.NonPublic)!;

                    // Update form with the reason for the close
                    pi.SetValue(KryptonForm, CloseReason.UserClosing, null);

                    // Convert screen position to LPARAM format of WM_SYSCOMMAND message
                    Point screenPos = Control.MousePosition;
                    var lParam = (IntPtr)(PI.MAKELOWORD(screenPos.X) |
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