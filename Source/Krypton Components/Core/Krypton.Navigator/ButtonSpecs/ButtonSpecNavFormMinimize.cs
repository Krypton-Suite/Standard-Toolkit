#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator;

/// <summary>
/// Fixed minimize button specification for a navigator that owns a <see cref="KryptonForm"/>.
/// </summary>
public class ButtonSpecNavFormMinimize : ButtonSpecNavFixed
{
    #region Instance Fields

    private readonly KryptonNavigator _navigator;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonSpecNavFormMinimize"/> class.
    /// </summary>
    /// <param name="navigator">Owning navigator.</param>
    public ButtonSpecNavFormMinimize(KryptonNavigator navigator)
        : base(navigator, PaletteButtonSpecStyle.FormMin) =>
        _navigator = navigator;

    #endregion

    #region IButtonSpecValues

    /// <inheritdoc />
    public override bool GetVisible(PaletteBase palette)
    {
        var owner = _navigator.Owner;
        if (owner == null || _navigator.ControlKryptonFormFeatures)
        {
            return false;
        }

        switch (owner.FormBorderStyle)
        {
            case FormBorderStyle.FixedToolWindow:
            case FormBorderStyle.SizableToolWindow:
                return false;
        }

        return owner.MinimizeBox || owner.MaximizeBox;
    }

    /// <inheritdoc />
    public override ButtonEnabled GetEnabled(PaletteBase palette)
    {
        var owner = _navigator.Owner;
        if (owner == null)
        {
            return ButtonEnabled.False;
        }

        return owner.MinimizeBox ? ButtonEnabled.True : ButtonEnabled.False;
    }

    /// <inheritdoc />
    public override ButtonCheckState GetChecked(PaletteBase? palette) => ButtonCheckState.NotCheckButton;

    #endregion

    #region Protected Overrides

    /// <inheritdoc />
    protected override void OnClick(EventArgs e)
    {
        if (!GetViewEnabled())
        {
            return;
        }

        var owner = _navigator.Owner;
        if (owner is not { InertForm: false })
        {
            return;
        }

        var mea = (MouseEventArgs)e;
        if (!GetView().ClientRectangle.Contains(mea.Location))
        {
            return;
        }

        owner.SendSysCommand(owner.WindowState == FormWindowState.Minimized
            ? PI.SC_.RESTORE
            : PI.SC_.MINIMIZE);

        base.OnClick(e);
    }

    #endregion
}
