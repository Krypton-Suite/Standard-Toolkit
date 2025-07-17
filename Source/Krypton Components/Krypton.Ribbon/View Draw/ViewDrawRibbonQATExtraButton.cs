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
/// Draws the extra quick access button used for customization or overflowing.
/// </summary>
internal class ViewDrawRibbonQATExtraButton : ViewLeaf
{
    // TODO: Needs to be scaled
    private static readonly Size _contentSize = new Size(-2, -5);

    #region Instance Fields
    private readonly Size _viewSize; // = new(13, 22);
    private readonly KryptonRibbon _ribbon;
    private IDisposable? _mementoBack;
    private readonly EventHandler? _finishDelegate;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the quick access toolbar button has been clicked.
    /// </summary>
    public event ClickAndFinishHandler? ClickAndFinish;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonQATExtraButton class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonQATExtraButton([DisallowNull] KryptonRibbon ribbon,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(ribbon != null);

        // Remember incoming references
        _ribbon = ribbon!;

        // Create delegate used to process end of click action
        _finishDelegate = ClickFinished;

        // Attach a controller to this element for the pressing of the button
        var controller = new QATExtraButtonController(ribbon!, this, needPaint);
        controller.Click += OnClick;
        MouseController = controller;
        SourceController = controller;
        KeyController = controller;
        _viewSize = new Size((int)(13 * FactorDpiX), (int)(22 * FactorDpiY));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonQATExtraButton:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_mementoBack != null)
            {
                _mementoBack.Dispose();
                _mementoBack = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region KeyTipTarget
    /// <summary>
    /// Gets the key tip target for this view.
    /// </summary>
    public IRibbonKeyTipTarget? KeyTipTarget => SourceController as IRibbonKeyTipTarget;

    #endregion

    #region Overflow
    /// <summary>
    /// Gets and sets a value indicating if the button should be drawn as an overflow or context arrow.
    /// </summary>
    public bool Overflow { get; set; }

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

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context) => _viewSize;

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;
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

        // Update the enabled state of the button
        Enabled = _ribbon.Enabled;

        IPaletteBack paletteBack = _ribbon.StateCommon.RibbonGroupDialogButton.PaletteBack;
        IPaletteBorder paletteBorder = _ribbon.StateCommon.RibbonGroupDialogButton.PaletteBorder;
        IPaletteRibbonGeneral paletteGeneral = _ribbon.StateCommon.RibbonGeneral;

        // Do we need to draw the background?
        if (paletteBack.GetBackDraw(State) == InheritBool.True)
        {
            // Get the border path which the background is clipped to drawing within
            using GraphicsPath borderPath = context.Renderer.RenderStandardBorder.GetBackPath(context, ClientRectangle, paletteBorder, VisualOrientation.Top, State);
            Padding borderPadding = context.Renderer.RenderStandardBorder.GetBorderRawPadding(paletteBorder, State, VisualOrientation.Top);

            // Apply the padding depending on the orientation
            Rectangle enclosingRect = CommonHelper.ApplyPadding(VisualOrientation.Top, ClientRectangle, borderPadding);

            // Render the background inside the border path
            using var gh = new GraphicsHint(context.Graphics, paletteBorder.GetBorderGraphicsHint(PaletteState.Normal));
            _mementoBack = context.Renderer.RenderStandardBack.DrawBack(context, enclosingRect, borderPath,
                paletteBack, VisualOrientation.Top,
                State, _mementoBack);
        }

        // Do we need to draw the border?
        if (paletteBorder.GetBorderDraw(State) == InheritBool.True)
        {
            context.Renderer.RenderStandardBorder.DrawBorder(context, ClientRectangle, paletteBorder, VisualOrientation.Top, State);
        }

        // Find the content area inside the button rectangle
        Rectangle contentRect = ClientRectangle;
        contentRect.Inflate(_contentSize);

        // Decide if we are drawing an overflow or context arrow image
        if (Overflow)
        {
            context.Renderer.RenderGlyph.DrawRibbonOverflow(_ribbon.RibbonShape, context, contentRect, paletteGeneral, State);
        }
        else
        {
            context.Renderer.RenderGlyph.DrawRibbonContextArrow(_ribbon.RibbonShape, context, contentRect, paletteGeneral, State);
        }
    }
    #endregion

    #region Implementation
    private void ClickFinished(object? sender, EventArgs e)
    {
        // Get access to our mouse controller
        var controller = MouseController as LeftDownButtonController;

        // Remove the fixed pressed appearance
        controller?.RemoveFixed();
    }

    private void OnClick(object? sender, MouseEventArgs e)
    {
        Form? ownerForm = _ribbon.FindForm();

        // Ensure the form we are inside is active
        ownerForm?.Activate();
        if ((ClickAndFinish != null))
        {
            if (!_ribbon.InDesignMode)
            {
                ClickAndFinish(this, _finishDelegate);
            }
            else
            {
                ClickFinished(this, EventArgs.Empty);
            }
        }
    }
    #endregion
}