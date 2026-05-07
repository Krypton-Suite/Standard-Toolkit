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
/// Implementation for internal calendar buttons.
/// </summary>
public class ButtonSpecCalendar : ButtonSpec
{
    #region Instance Fields

    private readonly RelativeEdgeAlign _edge;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecCalendar class.
    /// </summary>
    /// <param name="month">Reference to owning view.</param>
    /// <param name="fixedStyle">Fixed style to use.</param>
    /// <param name="edge">Alignment edge.</param>
    public ButtonSpecCalendar(ViewDrawMonth month,
        PaletteButtonSpecStyle fixedStyle,
        RelativeEdgeAlign edge)
    {
        // Remember back reference to owning navigator.
        _edge = edge;
        Enabled = true;
        Visible = true;

        // Fix the type
        ProtectedType = fixedStyle;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the visible state.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Visible { get; set; }

    /// <summary>
    /// Gets and sets the enabled state.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Enabled { get; set; }

    /// <summary>
    /// Can a component be associated with the view.
    /// </summary>
    public override bool AllowComponent => false;

    /// <summary>
    /// Gets the button visible value.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button visibility.</returns>
    public override bool GetVisible(PaletteBase palette) => Visible;

    /// <summary>
    /// Gets the button enabled state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button enabled state.</returns>
    public override ButtonEnabled GetEnabled(PaletteBase palette) => Enabled ? ButtonEnabled.Container : ButtonEnabled.False;

    /// <summary>
    /// Gets the button checked state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button checked state.</returns>
    public override ButtonCheckState GetChecked(PaletteBase? palette) => ButtonCheckState.Unchecked;

    /// <summary>
    /// Gets the button edge to position against.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Edge position.</returns>
    public override RelativeEdgeAlign GetEdge(PaletteBase? palette) => _edge;

    #endregion
}