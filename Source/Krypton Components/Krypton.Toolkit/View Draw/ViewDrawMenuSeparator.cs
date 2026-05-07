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
/// Draw element for a context menu separator.
/// </summary>
public class ViewDrawMenuSeparator : ViewDrawDocker
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuSeparator class.
    /// </summary>
    /// <param name="separator">Reference to owning separator entry.</param>
    /// <param name="palette">Palette for obtaining drawing values.</param>
    public ViewDrawMenuSeparator([DisallowNull] KryptonContextMenuSeparator separator,
        PaletteDoubleRedirect palette)
        : base(separator.StateNormal.Back, separator.StateNormal.Border)
    {
        // Draw the separator by default
        Draw = true;

        // Give the separator object the redirector to use when inheriting values
        separator.SetPaletteRedirect(palette);

        Orientation = separator.Horizontal ? VisualOrientation.Top : VisualOrientation.Left;

        // We need to be big enough to contain 1 pixel square spacer
        Add(new ViewLayoutSeparator(1));
    }

    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuSeparator class.
    /// </summary>
    /// <param name="state">State specific source of palette values.</param>
    public ViewDrawMenuSeparator([DisallowNull] PaletteDouble state)
        : base(state.Back, state.Border)
    {
        // We need to be big enough to contain 1 pixel square spacer
        Orientation = VisualOrientation.Left;
        Add(new ViewLayoutSeparator(1));
    }
        
    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawMenuSeparator:{Id}";

    #endregion

    #region Draw
    /// <summary>
    /// Gets and sets the drawing of the separator.
    /// </summary>
    public bool Draw { get; set; }

    #endregion

    #region Paint
    /// <summary>
    /// Perform a render of the elements.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void Render([DisallowNull] RenderContext context)
    {
        Debug.Assert(context != null);

        if (Draw)
        {
            base.Render(context!);
        }
    }
    #endregion
}