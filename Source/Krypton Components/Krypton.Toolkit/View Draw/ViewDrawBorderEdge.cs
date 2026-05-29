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
/// View element that can draw a border edge by applying a size to a panel.
/// </summary>
public class ViewDrawBorderEdge : ViewDrawPanel
{
    #region Instance Fields
    private PaletteBorderEdge _palette;
    private readonly PaletteBackInheritForced _borderForced;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawBorderEdge class.
    /// </summary>
    /// <param name="palette">Palette source for drawing details.</param>
    /// <param name="orientation">Initial orientation of the border.</param>
    public ViewDrawBorderEdge([DisallowNull] PaletteBorderEdge palette,
        Orientation orientation)
        : base(palette)
    {
        Debug.Assert(palette != null);

        // Remember initial settings
        _palette = palette!;
        Orientation = orientation;

        // Create the forced border and override the graphics hint
        _borderForced = new PaletteBackInheritForced(palette!)
        {
            ForceGraphicsHint = PaletteGraphicsHint.None
        };
        base.SetPalettes(_borderForced);
    }
        
    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawBorderEdge:{Id}";

    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the orientation.
    /// </summary>
    public Orientation Orientation { get; set; }

    #endregion

    #region SetPalettes
    /// <summary>
    /// Update the source palette for drawing.
    /// </summary>
    /// <param name="palette">Palette source for drawing details.</param>
    public void SetPalettes([DisallowNull] PaletteBorderEdge palette)
    {
        Debug.Assert(palette != null);

        // Inherit from the newly provided palette
        _palette = palette!;
        _borderForced.SetInherit(palette!);

        // Give the forced palette to the base
        base.SetPalettes(_borderForced);
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We have no preferred size by default
        var preferredSize = Size.Empty;

        // Apply the border width in appropriate orientation
        if (Orientation == Orientation.Horizontal)
        {
            preferredSize.Height = _palette.GetBorderWidth(State);
        }
        else
        {
            preferredSize.Width = _palette.GetBorderWidth(State);
        }

        return preferredSize;
    }
    #endregion
}