#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Visual display of tooltip information.
/// </summary>
public class VisualPopupToolTip : VisualPopup
{
    #region Instance Fields
    private readonly PaletteTripleMetricRedirect _palette;
    private readonly ViewDrawDocker _drawDocker;
    private readonly ViewDrawContent _drawContent;
    private readonly IContentValues _contentValues;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the VisualPopupTooltip class.
    /// </summary>
    /// <param name="redirector">Redirector for recovering palette values.</param>
    /// <param name="contentValues">Source of content values.</param>
    /// <param name="renderer">Drawing renderer.</param>
    /// <param name="shadow">Does the Tooltip need a shadow effect.</param>
    public VisualPopupToolTip(PaletteRedirect redirector,
        IContentValues contentValues,
        IRenderer renderer,
        bool shadow)
        : this(redirector, contentValues, renderer,
            PaletteBackStyle.ControlToolTip,
            PaletteBorderStyle.ControlToolTip,
            PaletteContentStyle.LabelToolTip,
            shadow)
    {
    }

    /// <summary>
    /// Initialize a new instance of the VisualPopupTooltip class.
    /// </summary>
    /// <param name="redirector">Redirector for recovering palette values.</param>
    /// <param name="contentValues">Source of content values.</param>
    /// <param name="renderer">Drawing renderer.</param>
    /// <param name="backStyle">Style for the tooltip background.</param>
    /// <param name="borderStyle">Style for the tooltip border.</param>
    /// <param name="contentStyle">Style for the tooltip content.</param>
    /// <param name="shadow">Does the Tooltip need a shadow effect.</param>
    public VisualPopupToolTip([DisallowNull] PaletteRedirect redirector,
        [DisallowNull] IContentValues contentValues,
        IRenderer renderer,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        PaletteContentStyle contentStyle,
        bool shadow)
        : base(renderer, shadow)
    {
        Debug.Assert(contentValues is not null);

        // Remember references needed later
        _contentValues = contentValues ?? throw new NullReferenceException(GlobalStaticFunctions.VariableCannotBeNull(nameof(contentValues)));

        // Create the triple redirector needed by view elements
        _palette = new PaletteTripleMetricRedirect(redirector, backStyle, borderStyle, contentStyle, NeedPaintDelegate);

        // Our view contains background and border with content inside
        _drawDocker = new ViewDrawDocker(_palette.Back, _palette.Border, null);
        _drawContent = new ViewDrawContent(_palette.Content, _contentValues, VisualOrientation.Top);
        _drawDocker.Add(_drawContent, ViewDockStyle.Fill);

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawDocker);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets a value indicating if the keyboard is passed to this popup.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public override bool KeyboardInert => true;

    /// <summary>
    /// Should the mouse move at provided screen point be allowed.
    /// </summary>
    /// <param name="m">Original message.</param>
    /// <param name="pt">Client coordinates point.</param>
    /// <returns>True to allow; otherwise false.</returns>
    public override bool AllowMouseMove(Message m, Point pt) =>
        // We allow all mouse moves when we are showing
        true;

    /// <summary>
    /// Use the setting from the Positioning to display the tooltip
    /// </summary>
    /// <param name="target"></param>
    /// <param name="controlMousePosition"></param>
    public void ShowRelativeTo(ViewBase target, Point controlMousePosition)
    {
        PopupPositionValues position;
        if (_contentValues is ToolTipValues toolTipValues)
        {
            position = toolTipValues.ToolTipPosition;
        }
        else
        {
            position = new PopupPositionValues();
        }

        Control? owning = target.OwningControl;
        if (owning is null)
        {
            ShowCalculatingSize(controlMousePosition);
            return;
        }

        ApplyPlacementAndShow(controlMousePosition, position, owning, target.ClientRectangle);
    }

    /// <summary>
    /// Positions the tooltip using <paramref name="position"/> and the rectangle of <paramref name="placementControl"/>
    /// when <see cref="PopupPositionValues.PlacementRectangle"/> is empty and <see cref="PopupPositionValues.PlacementTarget"/> is not set.
    /// Use this when content is not <see cref="ToolTipValues"/> but placement should still follow <see cref="ToolTipValues.ToolTipPosition"/> (e.g. <see cref="KryptonToolTip"/>).
    /// </summary>
    /// <param name="placementControl">Hovered control supplying default placement bounds (<see cref="Control.ClientRectangle"/>).</param>
    /// <param name="screenMousePosition">Screen-space cursor position.</param>
    /// <param name="position">Placement resolved from tooltip settings.</param>
    public void ShowRelativeTo([DisallowNull] Control placementControl, Point screenMousePosition,
        [DisallowNull] PopupPositionValues position) =>
        ApplyPlacementAndShow(screenMousePosition, position, placementControl,
            placementControl.ClientRectangle);

    /// <summary>
    /// Shared placement aligned with WPF Popup behaviour (same rules as <see cref="ShowRelativeTo(ViewBase, Point)"/>).
    /// </summary>
    /// <param name="controlMousePosition">Screen-space mouse/cursor coordinates.</param>
    /// <param name="position">Placement configuration.</param>
    /// <param name="fallbackOwningControl">Owning control used when placement does not bind to <see cref="PopupPositionValues.PlacementTarget"/>.</param>
    /// <param name="fallbackPlacementRectInOwningClient">Rectangle in <paramref name="fallbackOwningControl"/> client coordinates.</param>
    private void ApplyPlacementAndShow(Point controlMousePosition, PopupPositionValues position,
        Control fallbackOwningControl, Rectangle fallbackPlacementRectInOwningClient)
    {
        Rectangle cursorBounds = CommonHelper.GetCursorScreenBounds(controlMousePosition);
        const int cursorMargin = 2;

        Rectangle positionPlacementRectangle = position.PlacementRectangle;
        switch (position.PlacementMode)
        {
            case PlacementMode.Absolute:
            case PlacementMode.AbsolutePoint:
                // The screen, or PlacementRectangle if it is set.
                // So do nothing !
                break;
            case PlacementMode.Mouse:
            case PlacementMode.MousePoint:
                // The bounds of the mouse pointer. PlacementRectangle is ignored
                positionPlacementRectangle = cursorBounds;
                break;
            default:
                // The screen, or PlacementRectangle if it is set. The PlacementRectangle is relative to the screen.
                if (positionPlacementRectangle.IsEmpty)
                {
                    Control? ctrl = position.PlacementTarget?.OwningControl ?? fallbackOwningControl;
                    if (ctrl is not null)
                    {
                        Rectangle rectInOwnerClient = position.PlacementTarget?.ClientRectangle ?? fallbackPlacementRectInOwningClient;
                        positionPlacementRectangle = ctrl.RectangleToScreen(rectInOwnerClient);
                    }
                    else
                    {
                        positionPlacementRectangle = cursorBounds;
                    }
                }
                else
                {
                    positionPlacementRectangle = Screen.GetWorkingArea(controlMousePosition);
                }
                break;
        }

        // Get the size the popup would like to be
        Size popupSize = ViewManager!.GetPreferredSize(Renderer, new Size(100, 10));
        Point popupLocation;

        switch (position.PlacementMode)
        {
            case PlacementMode.Absolute:
            case PlacementMode.AbsolutePoint:
            case PlacementMode.MousePoint:
            case PlacementMode.Relative:
            case PlacementMode.RelativePoint:
                // The top-left corner of the target area.     The top-left corner of the Popup.
                popupLocation = positionPlacementRectangle.Location;
                if (positionPlacementRectangle.IntersectsWith(cursorBounds))
                {
                    popupLocation.X = cursorBounds.Right + cursorMargin;
                }
                break;
            case PlacementMode.Bottom:
            case PlacementMode.Mouse:
                // The bottom-left corner of the target area.     The top-left corner of the Popup.
                popupLocation = new Point(positionPlacementRectangle.Left, positionPlacementRectangle.Bottom);
                break;
            case PlacementMode.Center:
                // The center of the target area.     The center of the Popup.
                popupLocation = positionPlacementRectangle.Location;
                popupLocation.Offset(popupSize.Width / 2, -popupSize.Height / 2);
                if (positionPlacementRectangle.IntersectsWith(cursorBounds))
                {
                    popupLocation.X = cursorBounds.Right + cursorMargin;
                }
                break;
            case PlacementMode.Left:
                // The top-left corner of the target area.     The top-right corner of the Popup.
                popupLocation = new Point(positionPlacementRectangle.Left - popupSize.Width, positionPlacementRectangle.Top);
                break;
            case PlacementMode.Right:
                // The top-right corner of the target area.     The top-left corner of the Popup.
                popupLocation = new Point(positionPlacementRectangle.Right, positionPlacementRectangle.Top);
                break;
            case PlacementMode.Top:
                // The top-left corner of the target area.     The bottom-left corner of the Popup.
                popupLocation = new Point(positionPlacementRectangle.Left, positionPlacementRectangle.Top - popupSize.Height);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(position.PlacementMode));
        }
        // Show it now!
        Show(popupLocation, popupSize);
    }

    /// <summary>
    /// Show the tooltip popup relative to the provided screen position.
    /// </summary>
    /// <param name="controlMousePosition">Screen point of cursor.</param>
    public void ShowCalculatingSize(Point controlMousePosition)
    {
        // Get the size the popup would like to be
        Size popupSize = ViewManager!.GetPreferredSize(Renderer, Size.Empty);

        // Anchor below-right of the full cursor image so the hotspot is not covered
        Rectangle cursorBounds = CommonHelper.GetCursorScreenBounds(controlMousePosition);
        const int cursorMargin = 2;
        var popupLocation = new Point(cursorBounds.Right + cursorMargin, cursorBounds.Bottom + cursorMargin);
        // Show it now!
        Show(popupLocation, popupSize);
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="lEvent">An EventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs lEvent)
    {
        // Let base class calculate fill rectangle
        base.OnLayout(lEvent);

        // Need a render context for accessing the renderer
        Rectangle rect = ClientRectangle;
        rect.Inflate(1, 1); // Make sure bottom and left borders are visible
        using var context = new RenderContext(this, null, rect, Renderer);
        using var gh = new GraphicsHint(context.Graphics, _palette.Border.GetBorderGraphicsHint(PaletteState.Normal));
        // Grab a path that is the outside edge of the border
        Rectangle borderRect = rect;
        GraphicsPath borderPath1 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _palette.Border, VisualOrientation.Top, PaletteState.Normal);
        borderRect.Inflate(-1, -1);
        GraphicsPath borderPath2 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _palette.Border, VisualOrientation.Top, PaletteState.Normal);
        borderRect.Inflate(-1, -1);
        GraphicsPath borderPath3 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _palette.Border, VisualOrientation.Top, PaletteState.Normal);

        // Update the region of the popup to be the border path
        Region = new Region(borderPath1);

        // Inform the shadow to use the same paths for drawing the shadow
        DefineShadowPaths(borderPath1, borderPath2, borderPath3);
    }
    #endregion
}