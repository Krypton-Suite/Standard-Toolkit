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
/// Draws the background and border for a group button.
/// </summary>
internal class ViewDrawRibbonGroupButtonBackBorder : ViewComposite
{
    #region Instance Fields
    private readonly Size _viewSize; // = new(22, 22);
    private readonly KryptonRibbon _ribbon;
    private IDisposable? _mementoBack;
    private readonly IPaletteBack _paletteBack;
    private readonly PaletteBackInheritForced _paletteBackDraw;
    private readonly PaletteBackLightenColors _paletteBackLight;
    private readonly IPaletteBorder _paletteBorder;
    private readonly PaletteBorderInheritForced _paletteBorderAll;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the button is left clicked.
    /// </summary>
    public event EventHandler? Click;

    /// <summary>
    /// Occurs when the button is right clicked.
    /// </summary>
    public event MouseEventHandler? ContextClick;

    /// <summary>
    /// Occurs when the drop-down button is clicked.
    /// </summary>
    public event EventHandler? DropDown;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupButtonBackBorder class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="groupItem">Reference to owning group item.</param>
    /// <param name="paletteBack">Palette to use for the background.</param>
    /// <param name="paletteBorder">Palette to use for the border.</param>
    /// <param name="constantBorder">Should the border be a constant normal state.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonGroupButtonBackBorder([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroupItem? groupItem,
        [DisallowNull] IPaletteBack? paletteBack,
        [DisallowNull] IPaletteBorder? paletteBorder,
        bool constantBorder,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(groupItem != null);
        Debug.Assert(paletteBack != null);
        Debug.Assert(paletteBorder != null);

        // Remember incoming references
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        GroupItem = groupItem ?? throw new ArgumentNullException(nameof(groupItem));
        _paletteBack = paletteBack ?? throw new ArgumentNullException(nameof(paletteBack));
            
        _paletteBackDraw = new PaletteBackInheritForced(paletteBack)
        {
            ForceDraw = InheritBool.True
        };

        _paletteBackLight = new PaletteBackLightenColors(paletteBack);
        _paletteBorderAll = new PaletteBorderInheritForced(paletteBorder);
        _paletteBorderAll.ForceBorderEdges(PaletteDrawBorders.All);

        _paletteBorder = paletteBorder ?? throw new ArgumentNullException(nameof(paletteBorder));
        ConstantBorder = constantBorder;

        // Default other fields
        Checked = false;
        DrawNonTrackingAreas = true;

        // Create delegate used to process end of click action
        FinishDelegate = ActionFinished;

        // Attach a controller to this element for the pressing of the button
        Controller = new GroupButtonController(_ribbon, this, needPaint);
        Controller.Click += OnClick;
        Controller.ContextClick += OnContextClick;
        Controller.DropDown += OnDropDown;
        MouseController = Controller;
        SourceController = Controller;
        KeyController = Controller;
        _viewSize = new Size((int)(22 * FactorDpiX), (int)(22 * FactorDpiY));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupButtonBackBorder:{Id}";

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

    #region GroupItem
    /// <summary>
    /// Gets access to the associated ribbon group item.
    /// </summary>
    public KryptonRibbonGroupItem GroupItem { get; }

    #endregion

    #region Controller
    /// <summary>
    /// Gets access to the associated controller.
    /// </summary>
    public GroupButtonController? Controller { get; }

    #endregion

    #region SplitVertical
    /// <summary>
    /// Gets and sets if the split button is vertical or horizontal.
    /// </summary>
    public bool SplitVertical { get; set; }

    #endregion

    #region SplitRectangle
    /// <summary>
    /// Gets and sets the rectangle for the split area.
    /// </summary>
    public Rectangle SplitRectangle
    {
        get => Controller!.SplitRectangle;
        set => Controller!.SplitRectangle = value;
    }
    #endregion

    #region ButtonType
    /// <summary>
    /// Gets and sets the type of button the view represents.
    /// </summary>
    public GroupButtonType ButtonType
    {
        get => Controller!.ButtonType;
        set => Controller!.ButtonType = value;
    }
    #endregion

    #region Checked
    /// <summary>
    /// Gets and sets the checked state of the button background/border.
    /// </summary>
    public bool Checked { get; set; }

    #endregion

    #region ConstantBorder
    /// <summary>
    /// Gets and sets the drawing of a constant border.
    /// </summary>
    public bool ConstantBorder { get; set; }

    #endregion

    #region DrawNonTrackingAreas
    /// <summary>
    /// Gets and sets if the non tracking areas are drawn.
    /// </summary>
    public bool DrawNonTrackingAreas { get; set; }

    #endregion

    #region FinishDelegate
    /// <summary>
    /// Gets access to the associated finish delegate.
    /// </summary>
    public EventHandler? FinishDelegate { get; }

    #endregion

    #region Layout
    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext? context)
    {
        Debug.Assert(context is not null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Let child elements layout in given space
        base.Layout(context);
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        // Get that basic drawing state that does not reflect checked state
        PaletteState drawState = State;

        // We never draw background/border as disabled, let content show disabled state
        if (drawState == PaletteState.Disabled)
        {
            drawState = PaletteState.Normal;
        }
        else
        {
            // Do we need to modify this for the checked state?
            if (Checked && (ButtonType == GroupButtonType.Check))
            {
                switch (drawState)
                {
                    case PaletteState.Normal:
                        drawState = PaletteState.CheckedNormal;
                        break;
                    case PaletteState.Tracking:
                        drawState = PaletteState.CheckedTracking;
                        break;
                    case PaletteState.Pressed:
                        drawState = PaletteState.CheckedPressed;
                        break;
                }
            }
        }

        switch (ButtonType)
        {
            case GroupButtonType.Push:
            case GroupButtonType.Check:
            case GroupButtonType.DropDown:
                // Entire area is drawn using draw state
                DrawBackground(_paletteBack, context, ClientRectangle, drawState);

                DrawBorder(_paletteBorder, context, ClientRectangle,
                    ConstantBorder ? PaletteState.Normal : drawState);
                break;
            case GroupButtonType.Split:
                if (SplitVertical)
                {
                    DrawVerticalSplit(context, drawState);
                }
                else
                {
                    DrawHorizontalSplit(context, drawState);
                }
                break;
        }

        base.RenderBefore(context);
    }
    #endregion

    #region Implementation
    private void DrawVerticalSplit(RenderContext context, PaletteState drawState)
    {
        // We need the rectangle that represents just the split area
        var partialHeight = ClientHeight / 3 * 2;
        var partialRect = new Rectangle(ClientLocation, new Size(ClientWidth, partialHeight));
        var splitRectangle = Controller!.SplitRectangle;
        var aboveSplitRect = new Rectangle(ClientLocation, new Size(ClientWidth, splitRectangle.Y - ClientLocation.Y));
        var splitterRect = new Rectangle(splitRectangle.Location, new Size(ClientWidth, 1));
        var belowSplitRect = splitRectangle with { X = ClientLocation.X, Width = ClientWidth };

        var splitWithFading = SplitWithFading(drawState);
        switch (drawState)
        {
            case PaletteState.Normal:
                // Draw the entire border around the button
                if (ConstantBorder)
                {
                    DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Normal);
                    DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Normal);
                }
                break;

            case PaletteState.Tracking:
                // Draw the background for the click and split areas
                if (Controller.MouseInSplit)
                {
                    using (var clipToSplitter = new Clipping(context.Graphics, belowSplitRect))
                    {
                        if (splitWithFading)
                        {
                            if (DrawNonTrackingAreas)
                            {
                                DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                        }
                        else
                        {
                            DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                        }
                    }

                    var belowSplitRect1 = belowSplitRect with { Y = belowSplitRect.Y + 1, Height = belowSplitRect.Height - 1 };
                    using (var clipToSplitter = new Clipping(context.Graphics, belowSplitRect1))
                    {
                        DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Tracking);
                    }

                    using (var clipToSplitter = new Clipping(context.Graphics, aboveSplitRect))
                    {
                        if (splitWithFading)
                        {
                            if (DrawNonTrackingAreas)
                            {
                                DrawBackground(_paletteBackLight, context, partialRect, PaletteState.Tracking);
                            }
                        }
                        else
                        {
                            DrawBackground(_paletteBackDraw, context, partialRect, PaletteState.Normal);
                        }
                    }
                }
                else
                {
                    using (var clipToSplitter = new Clipping(context.Graphics, aboveSplitRect))
                    {
                        DrawBackground(_paletteBack, context, partialRect, PaletteState.Tracking);
                    }

                    using (var clipToSplitter = new Clipping(context.Graphics, belowSplitRect))
                    {
                        if (splitWithFading)
                        {
                            if (DrawNonTrackingAreas)
                            {
                                DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                        }
                        else
                        {
                            DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                        }
                    }
                }

                // Draw the single pixel splitter line
                using (var clipToSplitter = new Clipping(context.Graphics, splitterRect))
                {
                    DrawBorder(_paletteBorderAll, context, splitRectangle with { Height = 2 }, PaletteState.Tracking);
                }

                // Draw the entire border around the button
                DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Tracking);
                break;

            case PaletteState.Pressed:
                // Draw the background for the click and split areas
                if (Controller.MouseInSplit)
                {
                    using (var clipToSplitter = new Clipping(context.Graphics, belowSplitRect))
                    {
                        if (splitWithFading)
                        {
                            if (DrawNonTrackingAreas)
                            {
                                DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Pressed);
                            }
                        }
                        else
                        {
                            DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                        }
                    }

                    var belowSplitRect1 = belowSplitRect with { Y = belowSplitRect.Y + 1, Height = belowSplitRect.Height - 1 };
                    using (var clipToSplitter = new Clipping(context.Graphics, belowSplitRect1))
                    {
                        DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Pressed);
                    }

                    using (var clipToSplitter = new Clipping(context.Graphics, aboveSplitRect))
                    {
                        if (splitWithFading)
                        {
                            if (DrawNonTrackingAreas)
                            {
                                DrawBackground(_paletteBackLight, context, partialRect, PaletteState.Tracking);
                            }
                        }
                        else
                        {
                            DrawBackground(_paletteBackDraw, context, partialRect, PaletteState.Normal);
                        }
                    }
                }
                else
                {
                    using (var clipToSplitter = new Clipping(context.Graphics, aboveSplitRect))
                    {
                        DrawBackground(_paletteBack, context, partialRect, PaletteState.Pressed);
                    }

                    using (var clipToSplitter = new Clipping(context.Graphics, belowSplitRect))
                    {
                        if (splitWithFading)
                        {
                            if (DrawNonTrackingAreas)
                            {
                                DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                        }
                        else
                        {
                            DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                        }
                    }
                }

                // Draw the entire border around the button
                DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Tracking);

                // Draw the single pixel splitter line
                using (var clipToSplitter = new Clipping(context.Graphics, splitterRect))
                {
                    DrawBorder(_paletteBorderAll, context, splitRectangle with { Height = 2 }, PaletteState.Pressed);
                }

                // Draw the border for the click and split areas
                if (Controller.MouseInSplit)
                {
                    using var clipToSplitter = new Clipping(context.Graphics, belowSplitRect);
                    DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Pressed);
                }
                else
                {
                    using var clipToSplitter = new Clipping(context.Graphics, aboveSplitRect);
                    DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Pressed);
                }
                break;

            default:
                // Should never happen
                Debug.Assert(false);
                DebugTools.NotImplemented(drawState.ToString());
                break;
        }
    }

    private void DrawHorizontalSplit(RenderContext context, PaletteState drawState)
    {
        // We need the rectangle that represents just the split area
        var splitRectangle = Controller!.SplitRectangle;
        var beforeSplitRect = new Rectangle(ClientLocation, new Size(splitRectangle.X - ClientLocation.X, ClientHeight));
        var splitterRect = new Rectangle(splitRectangle.Location, new Size(1, ClientHeight));
        var afterSplitRect = splitRectangle with { Y = ClientLocation.Y, Height = ClientHeight };

        var splitWithFading = SplitWithFading(drawState);
        switch (drawState)
        {
            case PaletteState.Normal:
                // Draw the entire border around the button
                if (ConstantBorder)
                {
                    DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Normal);
                    DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Normal);
                }
                break;

            case PaletteState.Tracking:
                // Draw the background for the click and split areas
                if (Controller.MouseInSplit)
                {
                    using (var clipToSplitter = new Clipping(context.Graphics, afterSplitRect))
                    {
                        if (splitWithFading)
                        {
                            if (DrawNonTrackingAreas)
                            {
                                DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                        }
                        else
                        {
                            DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                        }
                    }

                    var afterSplitRect1 = afterSplitRect with { X = afterSplitRect.X + 1, Width = afterSplitRect.Width - 1 };
                    using (var clipToSplitter = new Clipping(context.Graphics, afterSplitRect1))
                    {
                        DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Tracking);
                    }

                    using (var clipToSplitter = new Clipping(context.Graphics, beforeSplitRect))
                    {
                        if (splitWithFading)
                        {
                            if (DrawNonTrackingAreas)
                            {
                                DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                        }
                        else
                        {
                            DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                        }
                    }
                }
                else
                {
                    using (var clipToSplitter = new Clipping(context.Graphics, beforeSplitRect))
                    {
                        DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Tracking);
                    }

                    using (var clipToSplitter = new Clipping(context.Graphics, afterSplitRect))
                    {
                        if (splitWithFading)
                        {
                            if (DrawNonTrackingAreas)
                            {
                                DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                        }
                        else
                        {
                            DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                        }
                    }
                }

                // Draw the single pixel splitter line
                using (var clipToSplitter = new Clipping(context.Graphics, splitterRect))
                {
                    DrawBorder(_paletteBorderAll, context, splitRectangle with { Width = 2 }, PaletteState.Tracking);
                }

                // Draw the entire border around the button
                if (ConstantBorder)
                {
                    DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Normal);
                }

                // If the border is not constant, drawn it now
                if (!ConstantBorder)
                {
                    // Draw the entire border around the button
                    DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Tracking);
                }
                break;

            case PaletteState.Pressed:
                // Draw the background for the click and split areas
                if (Controller.MouseInSplit)
                {
                    using (var clipToSplitter = new Clipping(context.Graphics, afterSplitRect))
                    {
                        if (splitWithFading)
                        {
                            if (DrawNonTrackingAreas)
                            {
                                DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Pressed);
                            }
                        }
                        else
                        {
                            DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                        }
                    }

                    var afterSplitRect1 = afterSplitRect with { X = afterSplitRect.X + 1, Width = afterSplitRect.Width - 1 };
                    using (var clipToSplitter = new Clipping(context.Graphics, afterSplitRect1))
                    {
                        DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Pressed);
                    }

                    using (var clipToSplitter = new Clipping(context.Graphics, beforeSplitRect))
                    {
                        if (splitWithFading)
                        {
                            if (DrawNonTrackingAreas)
                            {
                                DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                        }
                        else
                        {
                            DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                        }
                    }
                }
                else
                {
                    using (var clipToSplitter = new Clipping(context.Graphics, beforeSplitRect))
                    {
                        DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Pressed);
                    }

                    using (var clipToSplitter = new Clipping(context.Graphics, afterSplitRect))
                    {
                        if (splitWithFading)
                        {
                            if (DrawNonTrackingAreas)
                            {
                                DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                        }
                        else
                        {
                            DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                        }
                    }
                }

                // Draw the single pixel splitter line
                using (var clipToSplitter = new Clipping(context.Graphics, splitterRect))
                {
                    DrawBorder(_paletteBorderAll, context, splitRectangle with { Width = 2 }, PaletteState.Pressed);
                }

                // Draw the entire border around the button
                if (ConstantBorder)
                {
                    DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Normal);
                }

                // If the border is not constant, drawn it now
                if (!ConstantBorder)
                {
                    // Draw the entire border around the button
                    DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Tracking);

                    // Draw the border for the click and split areas
                    if (Controller.MouseInSplit)
                    {
                        using var clipToSplitter = new Clipping(context.Graphics, afterSplitRect);
                        DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Pressed);
                    }
                    else
                    {
                        using var clipToSplitter = new Clipping(context.Graphics, beforeSplitRect);
                        DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Pressed);
                    }
                }
                break;

            default:
                // Should never happen
                Debug.Assert(false);
                DebugTools.NotImplemented(drawState.ToString());
                break;
        }
    }

    private void DrawBackground(IPaletteBack paletteBack,
        [DisallowNull] RenderContext context,
        Rectangle rect,
        PaletteState state)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Do we need to draw the background?
        if (paletteBack.GetBackDraw(state) == InheritBool.True)
        {
            // Get the border path which the background is clipped to drawing within
            using GraphicsPath borderPath = context.Renderer.RenderStandardBorder.GetBackPath(context, rect, _paletteBorder, VisualOrientation.Top, state);
            Padding borderPadding = context.Renderer.RenderStandardBorder.GetBorderRawPadding(_paletteBorder, state, VisualOrientation.Top);

            // Apply the padding depending on the orientation
            Rectangle enclosingRect = CommonHelper.ApplyPadding(VisualOrientation.Top, rect, borderPadding);

            // Render the background inside the border path
            using var gh = new GraphicsHint(context.Graphics, _paletteBorder.GetBorderGraphicsHint(PaletteState.Normal));
            _mementoBack = context.Renderer.RenderStandardBack.DrawBack(context, enclosingRect, borderPath,
                paletteBack, VisualOrientation.Top,
                state, _mementoBack);
        }
    }

    private void DrawBorder(IPaletteBorder paletteBorder,
        [DisallowNull] RenderContext context,
        Rectangle rect,
        PaletteState state)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Do we need to draw the border?
        if (paletteBorder.GetBorderDraw(state) == InheritBool.True)
        {
            context.Renderer.RenderStandardBorder.DrawBorder(context, rect, paletteBorder,
                VisualOrientation.Top, state);
        }
    }

    private bool SplitWithFading(PaletteState drawState)
    {
        var palette = _ribbon.GetRedirector();
        return palette.GetMetricBool(drawState, PaletteMetricBool.SplitWithFading) == InheritBool.True;
    }

    private void ActionFinished(object? sender, EventArgs e)
    {
        var fireAction = true;

        if (e is ToolStripDropDownClosedEventArgs closedArgs)
        {
            if (closedArgs.CloseReason != ToolStripDropDownCloseReason.ItemClicked)
            {
                fireAction = false;
            }
        }

        // Remove any popups that result from an action occurring
        if ((_ribbon != null) && fireAction)
        {
            _ribbon.ActionOccurred();
        }

        // Remove the fixed pressed appearance
        Controller!.RemoveFixed();
    }

    private void OnClick(object? sender, EventArgs e) => Click?.Invoke(this, e);

    private void OnContextClick(object? sender, MouseEventArgs e) => ContextClick?.Invoke(this, e);

    private void OnDropDown(object? sender, EventArgs e) => DropDown?.Invoke(this, e);
    #endregion
}