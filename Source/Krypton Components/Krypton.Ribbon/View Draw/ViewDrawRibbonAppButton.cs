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
/// Draws half of an application button.
/// </summary>
internal class ViewDrawRibbonAppButton : ViewLeaf
{

    #region Instance Fields
    private IDisposable?[] _mementos;
    private readonly KryptonRibbon _ribbon;
    private readonly bool _bottomHalf;
    private Rectangle _clipRect;
    private readonly Size _size;
    private readonly Size _sizeFull; // = new(39, 39);
    private readonly Size _sizeBottom; // = new(39, 17);
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonAppButton class.
    /// </summary>
    /// <param name="ribbon">Owning control instance.</param>
    /// <param name="bottomHalf">Scroller orientation.</param>
    public ViewDrawRibbonAppButton([DisallowNull] KryptonRibbon ribbon, bool bottomHalf)
    {
        Debug.Assert(ribbon != null);

        _sizeFull = new Size((int)(39 * FactorDpiX), (int)(39 * FactorDpiY));
        var sizeTop = new Size((int)(39 * FactorDpiX), (int)(22 * FactorDpiY));
        _sizeBottom = new Size((int)(39 * FactorDpiX), (int)(17 * FactorDpiY));

        _ribbon = ribbon!;
        _bottomHalf = bottomHalf;
        _size = _bottomHalf ? _sizeBottom : sizeTop;
        _mementos = new IDisposable[3];
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonAppButton:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_mementos != null!)
            {
                foreach (IDisposable? memento in _mementos)
                {
                    memento?.Dispose();
                }

                _mementos = null!;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Visible
    /// <summary>
    /// Gets and sets the visible state of the element.
    /// </summary>
    public override bool Visible
    {
        get => base.Visible && (Parent?.Visible ?? true);
        set => base.Visible = value;
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context) => _size;

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;
        _clipRect = ClientRectangle;

        // Update to reflect full size of actual button
        if (_bottomHalf)
        {
            Rectangle client = ClientRectangle;
            client.Y -= _sizeFull.Height - _sizeBottom.Height;
            ClientRectangle = client;
        }

        ClientHeight = _sizeFull.Height;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore([DisallowNull] RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // New clipping region is at most our own client size
        using var combineRegion = new Region(_clipRect);
        // Remember the current clipping region
        Region clipRegion = context.Graphics.Clip.Clone();

        // Reduce clipping region down by the existing clipping region
        combineRegion.Intersect(clipRegion);

        // Use new region that restricts drawing to our client size only
        context.Graphics.Clip = combineRegion;

        IPaletteRibbonBack palette;
        int memento;

        // Find the correct palette to use that matches the button state
        switch (State)
        {
            default:
            case PaletteState.Normal:
                palette = _ribbon.StateNormal.RibbonAppButton;
                memento = 0;
                break;
            case PaletteState.Tracking:
                palette = _ribbon.StateTracking.RibbonAppButton;
                memento = 1;
                break;
            case PaletteState.Pressed:
                palette = _ribbon.StatePressed.RibbonAppButton;
                memento = 2;
                break;
        }

        // Draw the background
        _mementos[memento] = context.Renderer.RenderRibbon.DrawRibbonApplicationButton(_ribbon.RibbonShape, context, ClientRectangle, State, palette, _mementos[memento]);

        // If there is an application button image to be drawn
        Image? localImage = _ribbon.RibbonFileAppButton.AppButtonImage;
        if (localImage != null)
        {
            // We always draw the image a 24x24 image (if dpi = 1!)
            localImage = CommonHelper.ScaleImageForSizedDisplay(localImage, localImage.Width * FactorDpiX,
                localImage.Height * FactorDpiY, false);

            if (localImage != null)
            {
                var imageRect = new Rectangle(ClientLocation.X + (int)(7 * FactorDpiX),
                    ClientLocation.Y + (int)(6 * FactorDpiY), (int)(24 * FactorDpiX), (int)(24 * FactorDpiY));

                if (_ribbon.Enabled)
                {
                    context.Graphics.DrawImage(localImage, imageRect);
                }
                else
                {
                    // Use a color matrix to convert to black and white
                    using var attribs = new ImageAttributes();
                    attribs.SetColorMatrix(CommonHelper.MatrixDisabled);

                    context.Graphics.DrawImage(localImage,
                        imageRect, 0, 0,
                        localImage.Width,
                        localImage.Height,
                        GraphicsUnit.Pixel, attribs);
                }
            }
        }

        // Put clipping region back to original setting
        context.Graphics.Clip = clipRegion;
    }
    #endregion
}