#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Implementation for the fixed help button for krypton form.
/// </summary>
public class ButtonSpecFormWindowHelp : ButtonSpecFormFixed
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecFormWindowHelp class.
    /// </summary>
    /// <param name="form">Reference to owning krypton form instance.</param>
    public ButtonSpecFormWindowHelp(KryptonForm form)
        : base(form, PaletteButtonSpecStyle.FormHelp)
    {
    }
    #endregion

    #region IButtonSpecValues
    /// <summary>
    /// Gets the button visible value.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button visibility.</returns>
    public override bool GetVisible(PaletteBase palette) =>
        KryptonForm.HelpButton;

    /// <summary>
    /// Gets the button enabled state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button enabled state.</returns>
    public override ButtonEnabled GetEnabled(PaletteBase palette) =>
        KryptonForm.HelpButton ? ButtonEnabled.True : ButtonEnabled.False;

    /// <summary>
    /// Gets the button checked state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button checked state.</returns>
    public override ButtonCheckState GetChecked(PaletteBase? palette) =>
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
                    KryptonForm.SendSysCommand(PI.SC_.CONTEXTHELP);

                    // Let base class fire any other attached events
                    base.OnClick(e);
                }
            }
        }
    }
    #endregion
}