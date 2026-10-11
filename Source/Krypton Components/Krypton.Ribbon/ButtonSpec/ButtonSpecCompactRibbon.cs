#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Optional arrow that switches the ribbon between classic and compact layout.
/// </summary>
/// <remarks>
/// <see cref="KryptonRibbon.CompactButtonLocation"/> chooses under the groups, at the end of the groups, or on the tab row.
/// </remarks>
public class ButtonSpecCompactRibbon : ButtonSpec
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly bool _enterCompact;
    private readonly bool _onTabRow;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="ButtonSpecCompactRibbon"/> class.
    /// </summary>
    /// <param name="ribbon">Owning ribbon.</param>
    /// <param name="enterCompact">True when this button turns compact mode on. False when it restores the classic ribbon.</param>
    /// <param name="onTabRow">True when this button is hosted on the tab row. False when it is hosted with the groups.</param>
    public ButtonSpecCompactRibbon([DisallowNull] KryptonRibbon ribbon, bool enterCompact, bool onTabRow = false)
    {
        Debug.Assert(ribbon is not null);
        _ribbon = ribbon ?? ThrowHelper.ThrowArgumentNullException(ribbon);
        _enterCompact = enterCompact;
        _onTabRow = onTabRow;

        ProtectedType = enterCompact ? PaletteButtonSpecStyle.RibbonMinimize : PaletteButtonSpecStyle.RibbonExpand;
        AllowInheritToolTipTitle = false;
        ToolTipTitle = enterCompact ? @"Compact ribbon" : @"Classic ribbon";
    }
    #endregion

    #region AllowComponent
    /// <summary>
    /// Gets a value indicating if the component is allowed to be selected at design time.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool AllowComponent => false;

    #endregion

    #region IButtonSpecValues
    /// <summary>
    /// Gets the button visible value.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button visibility.</returns>
    public override bool GetVisible(PaletteBase palette) =>
        _ribbon.ShowCompactButton
        && (_ribbon.CompactMode != _enterCompact)
        && (_onTabRow == (_ribbon.CompactButtonLocation == CompactButtonLocation.Tabs));

    /// <summary>
    /// Gets the button enabled state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button enabled state.</returns>
    public override ButtonEnabled GetEnabled(PaletteBase palette) => ButtonEnabled.True;

    /// <summary>
    /// Gets the button checked state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button checked state.</returns>
    public override ButtonCheckState GetChecked(PaletteBase? palette) => ButtonCheckState.NotCheckButton;

    /// <summary>
    /// Gets the button style.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button style.</returns>
    public override ButtonStyle GetStyle(PaletteBase? palette) => ButtonStyle.ButtonSpec;

    #endregion

    #region Protected Overrides
    /// <summary>
    /// Raises the Click event and switches <see cref="KryptonRibbon.CompactMode"/>.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnClick(EventArgs e)
    {
        if (GetViewEnabled() && !_ribbon.InDesignMode)
        {
            _ribbon.CompactMode = _enterCompact;
        }
    }
    #endregion
}
