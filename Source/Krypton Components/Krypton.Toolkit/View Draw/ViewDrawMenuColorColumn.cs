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
/// Positions the child within a border that is drawn as the column background color.
/// </summary>
public class ViewDrawMenuColorColumn : ViewComposite
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuColorColumn class.
    /// </summary>
    /// <param name="provider">Reference to provider.</param>
    /// <param name="colorColumns">Reference to owning color columns entry.</param>
    /// <param name="colors">Set of colors to initialize from.</param>\
    /// <param name="start">Stating index to use.</param>
    /// <param name="end">Ending index to use.</param>
    /// <param name="enabled">Is this column enabled</param>
    public ViewDrawMenuColorColumn(IContextMenuProvider provider,
        KryptonContextMenuColorColumns colorColumns,
        Color[] colors, 
        int start, 
        int end, 
        bool enabled)
    {
        var vertical = new ViewLayoutColorStack();

        for (var i = start; i < end; i++)
        {
            vertical.Add(new ViewDrawMenuColorBlock(provider, colorColumns, colors[i], 
                i == start, i == (end - 1), enabled));
        }

        Add(vertical);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawMenuColorColumn:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Let base class layout the children
        base.Layout(context);

        // Put back the original size before returning
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Paint

    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void RenderBefore([DisallowNull] RenderContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        using var brush = new SolidBrush(Color.FromArgb(197, 197, 197));
        context.Graphics.FillRectangle(brush, ClientRectangle);
    }
    #endregion
}