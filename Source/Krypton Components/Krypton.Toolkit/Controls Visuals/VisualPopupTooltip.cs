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
/// Visual display of tooltip information, optionally hosting an interactive <see cref="Control"/>.
/// </summary>
public class VisualPopupToolTip : VisualPopup
{
    #region Instance Fields
    private readonly PaletteTripleMetricRedirect _palette;
    private readonly ViewDrawDocker _drawDocker;
    private readonly ViewDrawContent? _drawContent;
    private readonly ViewLayoutHostedFill? _layoutFill;
    private readonly IContentValues? _contentValues;
    private readonly Control? _hostedControl;
    private readonly bool _keyboardInert;
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
        : this(redirector, renderer, backStyle, borderStyle, contentStyle, shadow, contentValues, null, true)
    {
    }

    /// <summary>
    /// Initialize a popup tooltip that hosts <paramref name="hostedControl"/> inside tooltip chrome.
    /// The hosted control is reparented for the lifetime of this popup and unparented on dispose; it is not disposed here.
    /// </summary>
    /// <param name="redirector">Redirector for recovering palette values.</param>
    /// <param name="hostedControl">Control shown inside the tooltip. Cannot be a <see cref="Form"/>.</param>
    /// <param name="renderer">Drawing renderer.</param>
    /// <param name="backStyle">Style for the tooltip background.</param>
    /// <param name="borderStyle">Style for the tooltip border.</param>
    /// <param name="contentStyle">Style for optional heading content.</param>
    /// <param name="shadow">Does the Tooltip need a shadow effect.</param>
    /// <param name="headingValues">Optional heading (short/long text and image) drawn above the hosted control.</param>
    /// <param name="keyboardInert">When false, keyboard input (including Escape) is delivered to this popup.</param>
    public VisualPopupToolTip(PaletteRedirect redirector,
        Control hostedControl,
        IRenderer renderer,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        PaletteContentStyle contentStyle,
        bool shadow,
        IContentValues? headingValues,
        bool keyboardInert = true)
        : this(redirector, renderer, backStyle, borderStyle, contentStyle, shadow, headingValues, hostedControl, keyboardInert)
    {
    }

    private VisualPopupToolTip([DisallowNull] PaletteRedirect redirector,
        IRenderer renderer,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        PaletteContentStyle contentStyle,
        bool shadow,
        IContentValues? contentValues,
        Control? hostedControl,
        bool keyboardInert)
        : base(renderer, shadow)
    {
        if (hostedControl is Form)
        {
            ThrowHelper.ThrowArgumentException(@"A Form cannot be hosted inside a tooltip.", nameof(hostedControl));
        }

        if (contentValues is null && hostedControl is null)
        {
            ThrowHelper.ThrowArgumentException(@"Tooltip requires heading content or a hosted control.");
        }

        _contentValues = contentValues;
        _hostedControl = hostedControl;
        _keyboardInert = keyboardInert;

        _palette = new PaletteTripleMetricRedirect(redirector, backStyle, borderStyle, contentStyle, NeedPaintDelegate);

        _drawDocker = new ViewDrawDocker(_palette.Back, _palette.Border, null);

        if (_contentValues is not null)
        {
            _drawContent = new ViewDrawContent(_palette.Content, _contentValues, VisualOrientation.Top);
            _drawDocker.Add(_drawContent, hostedControl is null ? ViewDockStyle.Fill : ViewDockStyle.Top);
        }

        ViewManager = new ViewManager(this, _drawDocker);

        if (_hostedControl is not null)
        {
            _layoutFill = new ViewLayoutHostedFill(_hostedControl)
            {
                DisplayPadding = new Padding(6)
            };
            _drawDocker.Add(_layoutFill, ViewDockStyle.Fill);

            if (_hostedControl.Parent is not null)
            {
                CommonHelper.RemoveControlFromParent(_hostedControl);
            }

            // ViewManager.Root must be assigned before parenting: Controls.Add triggers layout.
            Controls.Add(_hostedControl);
            _hostedControl.Visible = true;
        }
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing && _hostedControl is not null && !_hostedControl.IsDisposed)
        {
            // Unparent so ContainerControl.Dispose does not destroy caller-owned content.
            if (Controls.Contains(_hostedControl))
            {
                Controls.Remove(_hostedControl);
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the control hosted inside this tooltip, if any.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control? HostedControl => _hostedControl;

    /// <summary>
    /// Gets a value indicating whether this popup hosts interactive child controls.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsInteractive => _hostedControl is not null;

    /// <summary>
    /// Gets a value indicating if the keyboard is passed to this popup.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public override bool KeyboardInert => _keyboardInert;

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
        ShowRelativeTo(placementControl, screenMousePosition, position, placementControl.ClientRectangle);

    /// <summary>
    /// Positions the tooltip using <paramref name="position"/>, with <paramref name="fallbackPlacementRectInOwningClient"/>
    /// as the target when <see cref="PopupPositionValues.PlacementRectangle"/> is empty.
    /// </summary>
    /// <param name="placementControl">Hovered control used to convert client placement to screen coordinates.</param>
    /// <param name="screenMousePosition">Screen-space cursor position.</param>
    /// <param name="position">Placement resolved from tooltip settings.</param>
    /// <param name="fallbackPlacementRectInOwningClient">Rectangle in <paramref name="placementControl"/> client coordinates (for example a <see cref="ListViewItem"/> bounds).</param>
    public void ShowRelativeTo([DisallowNull] Control placementControl, Point screenMousePosition,
        [DisallowNull] PopupPositionValues position, Rectangle fallbackPlacementRectInOwningClient) =>
        ApplyPlacementAndShow(screenMousePosition, position, placementControl,
            fallbackPlacementRectInOwningClient);

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
                // PlacementRectangle is screen coordinates when set; otherwise the placement target / fallback control.
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
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(position.PlacementMode));
                return;
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

        if (_layoutFill is not null && _hostedControl is not null && !_hostedControl.IsDisposed)
        {
            Rectangle fillRect = _layoutFill.FillRect;
            if (!fillRect.IsEmpty)
            {
                _hostedControl.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
            }
        }

        if (!IsHandleCreated || ClientRectangle.IsEmpty)
        {
            return;
        }

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

    #region Private Types
    /// <summary>
    /// Fills the docker remainder and sizes from the hosted control's AutoSize preferred size or explicit Size.
    /// </summary>
    private sealed class ViewLayoutHostedFill : ViewLayoutFill
    {
        private readonly Control _hosted;

        public ViewLayoutHostedFill(Control hosted)
            : base(hosted) =>
            _hosted = hosted;

        /// <inheritdoc />
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Size size = _hosted.AutoSize
                ? _hosted.GetPreferredSize(context.DisplayRectangle.Size)
                : _hosted.Size;

            // First layout often has an empty proposed size, and an unshown control's Size can be 0,0.
            // Measure unconstrained so AutoSize content can still report a real preferred size.
            if (size.Width <= 0 || size.Height <= 0)
            {
                size = _hosted.GetPreferredSize(Size.Empty);
            }

            // Last resort: some controls still return empty until they have a handle. Use current
            // bounds with a 16px floor so the popup chrome does not collapse to zero.
            if (size.Width <= 0 || size.Height <= 0)
            {
                size = new Size(Math.Max(16, _hosted.Width), Math.Max(16, _hosted.Height));
            }

            return new Size(size.Width + DisplayPadding.Horizontal,
                size.Height + DisplayPadding.Vertical);
        }
    }
    #endregion
}
