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

internal class ViewLayoutColorStack : ViewLayoutStack
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutStack class.
    /// </summary>
    public ViewLayoutColorStack()
        : base(false)
    {
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutColorStack:{Id}";

    #endregion

    #region Paint
    /// <summary>
    /// Perform a render of the elements.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void Render([DisallowNull] RenderContext context)
    {
        Debug.Assert(context != null);

        // Perform rendering before any children
        RenderBefore(context!);

        // Perform rendering after that of children
        RenderAfter(context!);
    }

    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        // Ask each child to render in turn
        foreach (ViewBase child in this)
        {
            // Only render visible children that are inside the clipping rectangle
            if (child.Visible && child.ClientRectangle.IntersectsWith(context.ClipRect))
            {
                child.RenderBefore(context);
            }
        }
    }

    /// <summary>
    /// Perform rendering after child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderAfter(RenderContext context)
    {
        // Ask each child to render in turn
        foreach (ViewBase child in this)
        {
            // Only render visible children that are inside the clipping rectangle
            if (child.Visible && child.ClientRectangle.IntersectsWith(context.ClipRect))
            {
                child.RenderAfter(context);
            }
        }
    }
    #endregion

}