#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Implementation for the minimize ribbon button.
/// </summary>
public class ButtonSpecMinimizeRibbon : ButtonSpec
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecMinimizeRibbon class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    public ButtonSpecMinimizeRibbon([DisallowNull] KryptonRibbon ribbon)
    {
        Debug.Assert(ribbon is not null);
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));

        // Fix the type
        ProtectedType = PaletteButtonSpecStyle.RibbonMinimize;
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

    #region ButtonSpecStype
    /// <summary>
    /// Gets and sets the actual type of the button.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteButtonSpecStyle ButtonSpecType
    {
        get => ProtectedType;
        set => ProtectedType = value;
    }
    #endregion

    #region IButtonSpecValues
    /// <summary>
    /// Gets the button visible value.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button visibiliy.</returns>
    public override bool GetVisible(PaletteBase palette) => _ribbon is { ShowMinimizeButton: true, MinimizedMode: false };

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
    public override ButtonCheckState GetChecked(PaletteBase? palette) =>
        // Close button is never shown as checked
        ButtonCheckState.NotCheckButton;

    /// <summary>
    /// Gets the button style.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button style.</returns>
    public override ButtonStyle GetStyle(PaletteBase? palette) => ButtonStyle.ButtonSpec;

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
            if (!_ribbon.InDesignMode)
            {
                _ribbon.MinimizedMode = true;
            }
        }
    }
    #endregion
}