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
/// Draw the track for the track bar.
/// </summary>
public class ViewDrawTrackPosition : ViewLeaf
{
    #region Instance Fields
    private readonly ViewDrawTrackBar _drawTrackBar;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawTrackPosition class.
    /// </summary>
    /// <param name="drawTrackBar">Reference to owning track bar.</param>
    public ViewDrawTrackPosition(ViewDrawTrackBar drawTrackBar) => _drawTrackBar = drawTrackBar;

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawTrackPosition:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        return _drawTrackBar.PositionSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // We take on all the available display area
        ClientRectangle = context.DisplayRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore([DisallowNull] RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        IPaletteElementColor elementColors = State switch
        {
            PaletteState.Normal => _drawTrackBar.StateNormal.Position,
            PaletteState.Disabled => _drawTrackBar.StateDisabled.Position,
            PaletteState.Tracking => _drawTrackBar.StateTracking.Position,
            PaletteState.Pressed => _drawTrackBar.StatePressed.Position,
            _ => _drawTrackBar.StateNormal.Position
        };

        context.Renderer.RenderGlyph.DrawTrackPositionGlyph(context, State, elementColors, ClientRectangle,
            _drawTrackBar.Orientation,
            _drawTrackBar.TickStyle);
    }
    #endregion
}