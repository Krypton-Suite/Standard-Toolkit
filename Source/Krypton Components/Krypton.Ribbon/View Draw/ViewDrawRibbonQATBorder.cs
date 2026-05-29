#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

// ReSharper disable InconsistentNaming
namespace Krypton.Ribbon;

/// <summary>
/// Draws the border around the quick access toolbar.
/// </summary>
internal class ViewDrawRibbonQATBorder : ViewComposite
{
    #region Instance Fields
    private readonly int QAT_BUTTON_WIDTH; // = 22;
    private readonly int QAT_HEIGHT_MINI; // = 26;
    private readonly int QAT_HEIGHT_FULL; // = 27;
    private readonly int QAT_MINIBAR_LEFT; // = 6;
    private readonly Padding _minibarBorderPaddingOverlap; // = new(8, 2, 11, 2);
    private readonly Padding _minibarBorderPaddingNoOverlap; // = new(17, 2, 11, 2);
    private readonly Padding _fullbarBorderPadding_2007; // = new(1, 3, 2, 2);
    private readonly Padding _fullbarBorderPadding_2010; // = new(2);
    private readonly Padding _noBorderPadding; // = new(1, 0, 1, 0);
    private readonly KryptonRibbon _ribbon;
    private IDisposable? _memento;
    private readonly bool _minibar;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonQATBorder class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="needPaintDelegate">Delegate for notifying paint/layout changes.</param>
    /// <param name="minibar">Minibar or full bar drawing.</param>
    public ViewDrawRibbonQATBorder([DisallowNull] KryptonRibbon ribbon,
        [DisallowNull] NeedPaintHandler needPaintDelegate,
        bool minibar)
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(needPaintDelegate != null);

        QAT_BUTTON_WIDTH = (int)(22 * FactorDpiX);
        QAT_HEIGHT_MINI = (int)(26 * FactorDpiY);
        QAT_HEIGHT_FULL = (int)(27 * FactorDpiY);
        QAT_MINIBAR_LEFT = (int)(6 * FactorDpiX);
        _minibarBorderPaddingOverlap = new Padding((int)(8 * FactorDpiX), (int)(2 * FactorDpiY), (int)(11 * FactorDpiX), (int)(2 * FactorDpiY));
        _minibarBorderPaddingNoOverlap = new Padding((int)(17 * FactorDpiX), (int)(2 * FactorDpiY), (int)(11 * FactorDpiX), (int)(2 * FactorDpiY));
        _fullbarBorderPadding_2007 = new Padding((int)(1 * FactorDpiX), (int)(3 * FactorDpiY), (int)(2 * FactorDpiX), (int)(2 * FactorDpiY));
        _fullbarBorderPadding_2010 = new Padding((int)(2 * FactorDpiX), (int)(2 * FactorDpiY), (int)(2 * FactorDpiX), (int)(2 * FactorDpiY));
        _noBorderPadding = new Padding((int)(1 * FactorDpiX), 0, (int)(1 * FactorDpiX), 0);
        // Remember incoming references
        _ribbon = ribbon!;
        _minibar = minibar;
        OverlapAppButton = true;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonQATBorder:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_memento != null)
            {
                _memento.Dispose();
                _memento = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region OwnerForm
    /// <summary>
    /// Gets and sets the associated form instance.
    /// </summary>
    public KryptonForm? OwnerForm { get; set; }

    #endregion

    #region Visible
    /// <summary>
    /// Gets and sets the visible state of the element.
    /// </summary>
    public override bool Visible
    {
        get => _ribbon.Visible && base.Visible;
        set => base.Visible = value;
    }
    #endregion

    #region OverlapAppButton
    /// <summary>
    /// Should the element overlap the app button to the left.
    /// </summary>
    public bool OverlapAppButton { get; set; }

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        // Get size of the contained items
        Size preferredSize = base.GetPreferredSize(context);

        // Add on the border padding
        preferredSize = CommonHelper.ApplyPadding(Orientation.Horizontal, preferredSize, BarPadding);
        preferredSize.Height = Math.Max(preferredSize.Height, BarHeight);

        // If we are inside the custom chrome area
        if (OwnerForm != null)
        {
            // Calculate the maximum width allowed
            var maxWidth = (OwnerForm.Width - 100) / 3 * 2;

            // Adjust so the width is a multiple of a button size
            var buttons = (maxWidth - BarPadding.Horizontal) / QAT_BUTTON_WIDTH;
            maxWidth = (buttons * QAT_BUTTON_WIDTH) + BarPadding.Horizontal;

            preferredSize.Width = Math.Min(maxWidth, preferredSize.Width);
        }

        return preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        Rectangle clientRect = context!.DisplayRectangle;

        // For the minibar we have to position ourself at bottom of available area
        if (_minibar)
        {
            clientRect.Y = clientRect.Bottom - 1 - QAT_HEIGHT_MINI;
            clientRect.Height = QAT_HEIGHT_MINI;
        }

        ClientRectangle = clientRect;

        // Remove QAT border for positioning children
        context.DisplayRectangle = CommonHelper.ApplyPadding(Orientation.Horizontal, ClientRectangle, BarPadding);

        // Let children be laid out inside border area
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
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // We never draw the background/border for Office 2010 shape QAT
        if (_minibar && (_ribbon.RibbonShape == PaletteRibbonShape.Office2010))
        {
            return;
        }

        IPaletteRibbonBack palette;
        var state = PaletteState.Normal;
        Rectangle drawRect = ClientRectangle;

        // Get the correct drawing palette
        if (_minibar)
        {
            if (Active)
            {
                palette = _ribbon.StateCommon.RibbonQATMinibarActive;
                if (!OverlapAppButton)
                {
                    state = PaletteState.CheckedNormal;
                }
            }
            else
            {
                palette = _ribbon.StateCommon.RibbonQATMinibarInactive;
                state = PaletteState.Disabled;
            }

            if (OverlapAppButton)
            {
                // If we need to overlap the app button, then shift left
                drawRect.X -= QAT_MINIBAR_LEFT;
                drawRect.Width += QAT_MINIBAR_LEFT;
            }
            else
            {
                // Otherwise shift right so there is a small gap on the left
                drawRect.X += QAT_MINIBAR_LEFT;
                drawRect.Width -= QAT_MINIBAR_LEFT;
            }
        }
        else
        {
            palette = _ribbon.StateCommon.RibbonQATFullbar;
        }

        // Perform actual drawing
        _memento = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, drawRect, state, palette, VisualOrientation.Top, _memento);
    }
    #endregion

    #region Implementation
    private bool Active => OwnerForm == null || OwnerForm.WindowActive;

    private Padding BarPadding
    {
        get
        {
            if (_minibar)
            {
                if (_ribbon.RibbonShape == PaletteRibbonShape.Office2010)
                {
                    return _noBorderPadding;
                }
                else
                {
                    return OverlapAppButton ? _minibarBorderPaddingOverlap : _minibarBorderPaddingNoOverlap;
                }
            }
            else
            {
                return _ribbon.RibbonShape == PaletteRibbonShape.Office2010 ? _fullbarBorderPadding_2010 : _fullbarBorderPadding_2007;
            }
        }
    }

    private int BarHeight => _minibar ? QAT_HEIGHT_MINI : QAT_HEIGHT_FULL;

    #endregion
}