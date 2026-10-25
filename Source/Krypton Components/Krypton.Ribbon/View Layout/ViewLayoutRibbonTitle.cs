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
/// View element that draws nothing and will center all children within itself.
/// </summary>
internal class ViewLayoutRibbonTitle: ViewLayoutDocker
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonTitle class.
    /// </summary>
    public ViewLayoutRibbonTitle()
    {
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonTitle:{Id}";

    #endregion

    #region VertOffset
    /// <summary>
    /// Gets and sets the vertial offset for bottom docked elements.
    /// </summary>
    public int VertOffset { get; set; }

    #endregion

    #region Layout
    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout(ViewLayoutContext context)
    {
        // Let base class perform simple layout
        base.Layout(context);

        // We adjust the vertical layout position of the bottom docked items
        Rectangle tempRect = context.DisplayRectangle;
        foreach(ViewBase view in this)
        {
            if (GetDock(view) == ViewDockStyle.Bottom)
            {
                // Ask the element to layout again but offset
                Rectangle layoutRect = view.ClientRectangle;
                layoutRect.Y += VertOffset;
                context.DisplayRectangle = layoutRect;

                view.Layout(context);
            }
        }
            
        // Must restore the original value
        context.DisplayRectangle = tempRect;
    }
    #endregion
}