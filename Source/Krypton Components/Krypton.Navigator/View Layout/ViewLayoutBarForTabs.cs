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

namespace Krypton.Navigator;

/// <summary>
/// Special version of the bar used for tabs, used to alter rendering order.
/// </summary>
internal class ViewLayoutBarForTabs : ViewLayoutBar
{
    #region Instance Fields
    private bool _drawChecked;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutBarForTabs class.
    /// </summary>
    /// <param name="itemSizing">Method used to calculate item size.</param>
    /// <param name="itemAlignment">Method used to align items within lines.</param>
    /// <param name="barMultiline">Multline showing of items.</param>
    /// <param name="itemMinimumSize">Maximum allowed item size.</param>
    /// <param name="itemMaximumSize">Minimum allowed item size.</param>
    /// <param name="barMinimumHeight">Minimum height of the bar.</param>
    /// <param name="tabBorderStyle">Tab border style.</param>
    /// <param name="reorderSelectedLine">Should line with selection be reordered.</param>
    public ViewLayoutBarForTabs(BarItemSizing itemSizing,
        RelativePositionAlign itemAlignment,
        BarMultiline barMultiline,
        Size itemMinimumSize,
        Size itemMaximumSize,
        int barMinimumHeight,
        TabBorderStyle tabBorderStyle,
        bool reorderSelectedLine)
        : base(itemSizing, itemAlignment, barMultiline, itemMinimumSize,
            itemMaximumSize, barMinimumHeight, tabBorderStyle, reorderSelectedLine)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewLayoutBarForTabs class.
    /// </summary>
    /// <param name="paletteMetric">Palette source for metric values.</param>
    /// <param name="metricGap">Metric for gap between each child item.</param>
    /// <param name="itemSizing">Method used to calculate item size.</param>
    /// <param name="itemAlignment">Method used to align items within lines.</param>
    /// <param name="barMultiline">Multline showing of items.</param>
    /// <param name="itemMinimumSize">Maximum allowed item size.</param>
    /// <param name="itemMaximumSize">Minimum allowed item size.</param>
    /// <param name="barMinimumHeight">Minimum height of the bar.</param>
    /// <param name="tabBorderStyle">Tab border style.</param>
    /// <param name="reorderSelectedLine">Should line with selection be reordered.</param>
    public ViewLayoutBarForTabs(IPaletteMetric paletteMetric,
        PaletteMetricInt metricGap,
        BarItemSizing itemSizing,
        RelativePositionAlign itemAlignment,
        BarMultiline barMultiline,
        Size itemMinimumSize,
        Size itemMaximumSize,
        int barMinimumHeight,
        TabBorderStyle tabBorderStyle,
        bool reorderSelectedLine)
        : base(paletteMetric, metricGap, itemSizing,
            itemAlignment, barMultiline, itemMinimumSize,
            itemMaximumSize, barMinimumHeight, tabBorderStyle,
            reorderSelectedLine)
    {
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutBarForTabs:{Id}";

    #endregion

    #region DrawChecked
    /// <summary>
    /// Sets the value indicating if the checked tab should be drawn.
    /// </summary>
    public bool DrawChecked
    {
        set => _drawChecked = value;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform a render of the elements.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void Render(RenderContext context)
    {
        // Perform rendering before any children
        RenderBefore(context);

        // Do not draw the checked item
        RenderChildren(context, _drawChecked);

        // Perform rendering after that of children
        RenderAfter(context);
    }
    #endregion

    #region Implementation
    private void RenderChildren([DisallowNull] RenderContext context, bool drawChecked)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Use tab style to decide what order the children are drawn in
        var orderedChildren = context.Renderer.RenderTabBorder.GetTabBorderLeftDrawing(TabBorderStyle) ? this : Reverse();

        // Ask each child to render in turn
        foreach (ViewBase child in orderedChildren)
        {
            // Only render visible children that are inside the clipping rectangle
            if (child.Visible && child.ClientRectangle.IntersectsWith(context.ClipRect))
            {
                // If this is a page representation that can overlap group border
                var buttonBar = child as ViewDrawNavCheckButtonBar;
                var tab = child as ViewDrawNavRibbonTab;
                if ((buttonBar != null) ||
                    (tab != null))
                {
                    var itemChecked = buttonBar?.Checked ?? tab!.Checked;

                    // Are we allowed to draw the checked item?
                    if ((!itemChecked && !drawChecked) ||
                        (itemChecked && drawChecked))
                    {
                        child.Render(context);
                    }
                }
                else
                {
                    if (!drawChecked)
                    {
                        child.Render(context);
                    }
                }
            }
        }
    }
    #endregion
}