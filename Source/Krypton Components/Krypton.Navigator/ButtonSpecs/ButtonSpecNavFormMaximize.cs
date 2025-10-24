#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Navigator;

public class ButtonSpecNavFormMaximize : ButtonSpecNavFixed
{
    #region Instance Fields

    private readonly KryptonNavigator _navigator;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="ButtonSpecNavFormMaximize" /> class.</summary>
    /// <param name="navigator">The navigator.</param>
    public ButtonSpecNavFormMaximize(KryptonNavigator navigator) : base(navigator, PaletteButtonSpecStyle.FormMax)
    {
        _navigator = navigator;
    }

    #endregion

    #region ButtonSpecNavFixed Implementation

    public override bool GetVisible(PaletteBase palette)
    {
        // The maximize button is never present on tool windows
        switch (_navigator.Owner!.FormBorderStyle)
        {
            case FormBorderStyle.FixedToolWindow:
            case FormBorderStyle.SizableToolWindow:
                return false;
        }

        // Have all buttons been turned off?
        if (!_navigator.Owner!.ControlBox)
        {
            return false;
        }

        // Has the minimize/maximize buttons been turned off?
        return _navigator.Owner!.MinimizeBox || _navigator.Owner!.MaximizeBox;
    }

    public override ButtonCheckState GetChecked(PaletteBase? palette) => ButtonCheckState.NotCheckButton;

    public override ButtonEnabled GetEnabled(PaletteBase palette) =>
        // Has the maximize buttons been turned off?
        _navigator.Owner!.MaximizeBox ? ButtonEnabled.True : ButtonEnabled.False;

    #endregion

    #region Protected Overrides

    protected override void OnClick(EventArgs e)
    {
        // Only if associated view is enabled to we perform an action
        if (GetViewEnabled())
        {
            // If we do not provide an inert form
            if (!_navigator.Owner!.InertForm)
            {
                // Only if the mouse is still within the button bounds do we perform action
                var mea = (MouseEventArgs)e;
                if (GetView().ClientRectangle.Contains(mea.Location))
                {
                    // Toggle between maximized and restored
                    /*_navigator.Owner!.SendSysCommand(_navigator.Owner!.WindowState == FormWindowState.Maximized
                        ? PI.SC_.RESTORE
                        : PI.SC_.MAXIMIZE);*/

                    // Let base class fire any other attached events
                    base.OnClick(e);
                }
            }
        }
    }

    #endregion
}