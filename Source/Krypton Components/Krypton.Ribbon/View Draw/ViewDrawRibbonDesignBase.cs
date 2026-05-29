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
/// Draws a design time item.
/// </summary>
internal class ViewDrawRibbonDesignBase : ViewComposite,
    IContentValues
{
    #region Instance Fields

    private readonly NeedPaintHandler _needPaint;
    private readonly DesignTextToContent _contentProvider;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonDesignBase class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonDesignBase([DisallowNull] KryptonRibbon ribbon,
        [DisallowNull] NeedPaintHandler needPaint)
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(needPaint != null);

        // Cache incoming values
        Ribbon = ribbon!;
        _needPaint = needPaint!;

        // Create and add the draw content for display inside the tab
        _contentProvider = new DesignTextToContent(ribbon!);
        Add(new ViewDrawContent(_contentProvider, this, VisualOrientation.Top));

        // Use a controller to change state because of mouse movement
        var controller = new ViewHightlightController(this, needPaint!);
        controller.Click += OnClick;
        MouseController = controller;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonDesignBase:{Id}";

    #endregion

    #region Ribbon
    /// <summary>
    /// Gets access to the ribbon control instance.
    /// </summary>
    public KryptonRibbon Ribbon { get; }

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        Size retSize = base.GetPreferredSize(context);

        // Apply the padding around the child elements
        retSize = CommonHelper.ApplyPadding(Orientation.Horizontal, retSize, PreferredPadding);

        return retSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Reduce our size by a padding around the element
        ClientRectangle = new Rectangle(ClientLocation.X + OuterPadding.Left,
            ClientLocation.Y + OuterPadding.Top,
            ClientWidth - OuterPadding.Horizontal,
            ClientHeight - OuterPadding.Vertical);

        // Reduce again to arrive at size available for child elements
        context.DisplayRectangle = new Rectangle(ClientLocation.X + LayoutPadding.Left,
            ClientLocation.Y + LayoutPadding.Top,
            ClientWidth - LayoutPadding.Horizontal,
            ClientHeight - LayoutPadding.Vertical);

        // Let contained content element we laid out
        base.Layout(context);

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        // Ensure the child text view has same state as us
        this[0]!.ElementState = ElementState;

        // Draw background using the design time colors
        DesignTimeDraw.DrawArea(Ribbon, context, ClientRectangle, State);

        base.RenderBefore(context);
    }
    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the image used for the ribbon tab.
    /// </summary>
    /// <param name="state">Tab state.</param>
    /// <returns>Image.</returns>
    public Image? GetImage(PaletteState state) => null;

    /// <summary>
    /// Gets the image color that should be interpreted as transparent.
    /// </summary>
    /// <param name="state">Tab state.</param>
    /// <returns>Transparent Color.</returns>
    public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the short text used as the main ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public virtual string GetShortText() => "Unknown";

    /// <summary>
    /// Gets the long text used as the secondary ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public string GetLongText() => string.Empty;

    #endregion

    #region Protected
    /// <summary>
    /// Gets the padding to use when calculating the preferred size.
    /// </summary>
    protected virtual Padding PreferredPadding => Padding.Empty;

    /// <summary>
    /// Gets the padding to use when laying out the view.
    /// </summary>
    protected virtual Padding LayoutPadding => Padding.Empty;

    /// <summary>
    /// Gets the padding to shrink the client area by when laying out.
    /// </summary>
    protected virtual Padding OuterPadding => Padding.Empty;

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnClick(object? sender, EventArgs e)
    {
    }
    #endregion
}