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
/// Extends the ViewDrawDocker by drawing the ribbon app menu area.
/// </summary>
internal class ViewDrawRibbonAppMenu : ViewDrawDocker
{
    #region Instance Fields
    private readonly ViewBase _fixedElement;
    private readonly Rectangle _fixedScreenRect;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonAppMenu class.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    /// <param name="paletteBorder">Palette source for the border.</param>
    /// <param name="fixedElement">Element to display at provided screen rect.</param>
    /// <param name="fixedScreenRect">Screen rectangle for showing the element at.</param>
    public ViewDrawRibbonAppMenu(IPaletteBack paletteBack,
        IPaletteBorder paletteBorder,
        ViewBase fixedElement,
        Rectangle fixedScreenRect)
        : base(paletteBack, paletteBorder)
    {
        _fixedElement = fixedElement;
        _fixedScreenRect = fixedScreenRect;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonAppMenu:{Id}";

    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering after child elements are rendered.
    /// </summary>
    /// <param name="renderContext">Rendering context.</param>
    public override void RenderAfter([DisallowNull] RenderContext renderContext)
    {
        if (renderContext.Renderer is null)
        {
            throw new ArgumentNullException(nameof(renderContext.Renderer));
        }

        if (renderContext.TopControl is null)
        {
            throw new ArgumentNullException(nameof(renderContext.TopControl));
        }

        base.RenderAfter(renderContext);

        // Convert our rectangle to the screen
        Rectangle screenRect = renderContext.TopControl.RectangleToScreen(renderContext.TopControl.ClientRectangle);

        // If the fixed rectangle is in our showing area and at the top
        if (screenRect.Contains(_fixedScreenRect) && (screenRect.Y == _fixedScreenRect.Y))
        {
            // Position the element appropriately
            using (var layoutContext =
                   new ViewLayoutContext(renderContext.Control!, renderContext.Renderer))
            {
                layoutContext.DisplayRectangle = renderContext.TopControl.RectangleToClient(_fixedScreenRect);
                _fixedElement.Layout(layoutContext);
            }

            // Now draw
            _fixedElement.Render(renderContext);
        }
    }
    #endregion
}