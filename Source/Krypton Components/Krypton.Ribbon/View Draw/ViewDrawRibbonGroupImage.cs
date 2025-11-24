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
/// Draws the group image for a collapsed group.
/// </summary>
internal class ViewDrawRibbonGroupImage : ViewLeaf

{
    #region Instance Fields
    private readonly Size _viewSize2007; // = new(30, 31);
    private readonly Size _viewSize2010; // = new(31, 31);
    private readonly Size _imageSize; // = new(16, 16);
    private readonly int _imageOffsetX; // = 7;
    private readonly int _imageOffsetY2007; // = 4;
    private readonly int _imageOffsetY2010; // = 7;
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonRibbonGroup _ribbonGroup;
    private readonly ViewDrawRibbonGroup _viewGroup;
    private IDisposable? _memento1;
    private IDisposable? _memento2;
    private Size _viewSize;
    private int _offsetY;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupImage class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="ribbonGroup">Reference to ribbon group definition.</param>
    /// <param name="viewGroup">Reference to top level group element.</param>
    public ViewDrawRibbonGroupImage([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroup? ribbonGroup,
        [DisallowNull] ViewDrawRibbonGroup? viewGroup)
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(ribbonGroup != null);
        Debug.Assert(viewGroup != null);

        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        _ribbonGroup = ribbonGroup ?? throw new ArgumentNullException(nameof(ribbonGroup));
        _viewGroup = viewGroup ?? throw new ArgumentNullException(nameof(viewGroup));
        _viewSize2007 = new Size((int)(30 * FactorDpiX), (int)(31 * FactorDpiY));
        _viewSize2010 = new Size((int)(31 * FactorDpiX), (int)(31 * FactorDpiY));
        _imageSize = new Size((int)(16 * FactorDpiX), (int)(16 * FactorDpiY));
        _imageOffsetX = (int)(7 * FactorDpiX);
        _imageOffsetY2007 = (int)(4 * FactorDpiY);
        _imageOffsetY2010 = (int)(7 * FactorDpiY);  
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupImage:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_memento1 != null)
            {
                _memento1.Dispose();
                _memento1 = null;
            }

            if (_memento2 != null)
            {
                _memento2.Dispose();
                _memento2 = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        switch (_ribbon.RibbonShape)
        {
            default:
            case PaletteRibbonShape.Office2007:
                _viewSize = _viewSize2007;
                _offsetY = _imageOffsetY2007;
                break;
            case PaletteRibbonShape.Office2010:
                _viewSize = _viewSize2010;
                _offsetY = _imageOffsetY2010;
                break;
        }

        return _viewSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout(ViewLayoutContext context) =>
        // Take on all the provided area
        ClientRectangle = context.DisplayRectangle;
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore([DisallowNull] RenderContext context)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        IPaletteRibbonBack paletteBorder;
        IPaletteRibbonBack paletteBack;

        // Are we a group inside a context tab?
        if (!string.IsNullOrEmpty(_ribbon.SelectedTab?.ContextName))
        {
            ElementState = _viewGroup.Pressed ? PaletteState.Pressed : _viewGroup.Tracking ? PaletteState.ContextTracking : PaletteState.ContextNormal;
        }
        else
        {
            ElementState = _viewGroup.Pressed ? PaletteState.Pressed : _viewGroup.Tracking ? PaletteState.Tracking : PaletteState.Normal;
        }

        // Decide on the palette to use
        switch (State)
        {
            case PaletteState.Pressed:
                paletteBorder = _ribbon.StatePressed.RibbonGroupCollapsedFrameBorder;
                paletteBack = _ribbon.StatePressed.RibbonGroupCollapsedFrameBack;
                break;
            case PaletteState.ContextNormal:
                paletteBorder = _ribbon.StateContextNormal.RibbonGroupCollapsedFrameBorder;
                paletteBack = _ribbon.StateContextNormal.RibbonGroupCollapsedFrameBack;
                break;
            case PaletteState.ContextTracking:
                paletteBorder = _ribbon.StateContextTracking.RibbonGroupCollapsedFrameBorder;
                paletteBack = _ribbon.StateContextTracking.RibbonGroupCollapsedFrameBack;
                break;
            case PaletteState.Tracking:
                paletteBorder = _ribbon.StateTracking.RibbonGroupCollapsedFrameBorder;
                paletteBack = _ribbon.StateTracking.RibbonGroupCollapsedFrameBack;
                break;
            case PaletteState.Normal:
            default:
                paletteBorder = _ribbon.StateNormal.RibbonGroupCollapsedFrameBorder;
                paletteBack = _ribbon.StateNormal.RibbonGroupCollapsedFrameBack;
                break;
        }

        // The background is slightly inside the rounded border
        Rectangle backRect = ClientRectangle;
        backRect.Inflate(-1, -1);

        // Draw the background for the group image area
        _memento1 = context!.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, backRect, State, paletteBack, VisualOrientation.Top, _memento1);

        // Draw the border around the group image area
        _memento2 = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, ClientRectangle, State, paletteBorder, VisualOrientation.Top, _memento2);

        // If we have an image for drawing
        if (_ribbonGroup.Image != null)
        {
            // Determine the rectangle for the fixed size of image drawing
            var drawRect = new Rectangle(new Point(ClientLocation.X + _imageOffsetX, ClientLocation.Y + _offsetY), _imageSize);

            context.Graphics.DrawImage(_ribbonGroup.Image, drawRect);
        }
    }
    #endregion
}