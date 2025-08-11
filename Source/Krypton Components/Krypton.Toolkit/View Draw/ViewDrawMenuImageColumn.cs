#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class ViewDrawMenuImageColumn : ViewDrawDocker
{
    #region Instance Fields
    private readonly ViewLayoutSeparator _separator;
    private readonly IContextMenuProvider _provider;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuImageColumn class.
    /// </summary>
    /// <param name="provider">Provider used to access palette information.</param>
    /// <param name="items">Reference to the owning collection.</param>
    /// <param name="palette">Palette for obtaining drawing values.</param>
    public ViewDrawMenuImageColumn(IContextMenuProvider provider,
        KryptonContextMenuItems items,
        PaletteDoubleRedirect palette)
        : base(items.StateNormal.Back, items.StateNormal.Border)
    {
        _provider = provider;
        // Give the items collection the redirector to use when inheriting values
        items.SetPaletteRedirect(palette);

        _separator = new ViewLayoutSeparator(0);
        Add(_separator);

        // We draw the image-margin separator lines ourselves using the color table,
        // so suppress the standard single-edge border for this canvas.
        MaxBorderEdges = PaletteDrawBorders.None;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawMenuImageColumn:{Id}";

    #endregion

    #region Width
    /// <summary>
    /// Sets the width of the column.
    /// </summary>
    public int ColumnWidth
    {
        set => _separator.SeparatorSize = new Size(value, 0);
    }
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

        // Ensure all children are laid out in our total space
        base.Layout(context);
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering after child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderAfter([DisallowNull] RenderContext context)
    {
        base.RenderAfter(context);

        // Draw the light and dark separator lines to mirror ToolStripDropDownMenu rendering
        var rect = ClientRectangle;

        // Determine RTL so we know which edge to draw on
        bool rtl = CommonHelper.GetRightToLeftLayout(context.Control!) &&
                   context.Control!.RightToLeft == RightToLeft.Yes;

        // Acquire colors from the active color table
        var colorTable = _provider.ProviderRedirector.ColorTable;
        using var lightPen = new Pen(colorTable.ImageMarginGradientEnd);
        using var darkPen = new Pen(colorTable.ImageMarginGradientMiddle);

        if (!rtl)
        {
            // Draw the light and dark lines on the right-hand side
            context.Graphics.DrawLine(lightPen, rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom);
            context.Graphics.DrawLine(darkPen, rect.Right - 2, rect.Top, rect.Right - 2, rect.Bottom);
        }
        else
        {
            // Draw the light and dark lines on the left-hand side for RTL
            context.Graphics.DrawLine(lightPen, rect.Left, rect.Top, rect.Left, rect.Bottom);
            context.Graphics.DrawLine(darkPen, rect.Left + 1, rect.Top, rect.Left + 1, rect.Bottom);
        }
    }
    #endregion
}