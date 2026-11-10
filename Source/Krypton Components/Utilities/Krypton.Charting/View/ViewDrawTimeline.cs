#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// Draws a step timeline: start text, icon, end text, and a connector through the icons.
/// </summary>
internal sealed class ViewDrawTimeline : ViewLeaf
{
    #region Instance Fields

    private readonly KryptonTimeline _owner;
    private Slot[] _slots;
    private int _hotIndex;
    private int _dragIndex;
    private Cursor? _dragCursor;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawTimeline"/> class.
    /// </summary>
    /// <param name="owner">Owning timeline.</param>
    public ViewDrawTimeline(KryptonTimeline owner)
    {
        _owner = owner;
        _slots = Array.Empty<Slot>();
        _hotIndex = -1;
        _dragIndex = -1;
        var controller = new TimelineInputController(
            HitTest,
            SetHotIndex,
            _owner.SelectCore,
            () => { },
            _owner.ProcessKey,
            null,
            null,
            null,
            BeginMove,
            MoveItem,
            EndMove);
        MouseController = controller;
        KeyController = controller;
        SourceController = controller;
    }

    #endregion

    #region Layout

    /// <inheritdoc />
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        int count = Math.Max(1, _owner.Items.Count);
        int text = _owner.Font.Height + 8;
        int icon = TimelinePaint.IconSize;
        bool compact = _owner.Layout == TimelineLayout.Compact;
        if (_owner.Orientation == Orientation.Horizontal)
        {
            int across = compact
                ? TimelinePaint.Pad + icon + TimelinePaint.Gap + (text * 2) + TimelinePaint.Pad
                : TimelinePaint.Pad + text + TimelinePaint.Gap + icon + TimelinePaint.Gap + text + TimelinePaint.Pad;
            return new Size(Math.Max(160, count * 140), across);
        }

        int side = compact ? 140 : 120;
        int acrossV = compact
            ? TimelinePaint.Pad + icon + TimelinePaint.Gap + side
            : side + icon + side;
        int along = Math.Max(160, count * (text + icon + TimelinePaint.Gap));
        return new Size(acrossV, along);
    }

    /// <inheritdoc />
    public override void Layout(ViewLayoutContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        ClientRectangle = context.DisplayRectangle;
        BuildSlots();
    }

    #endregion

    #region Paint

    /// <inheritdoc />
    public override bool EvalTransparentPaint(ViewContext context) => true;

    /// <inheritdoc />
    public override void RenderBefore(RenderContext context)
    {
        if (context?.Graphics == null)
        {
            return;
        }

        BuildSlots();
        Graphics graphics = context.Graphics;
        DrawConnector(graphics);
        for (int i = 0; i < _slots.Length; i++)
        {
            DrawItem(context, graphics, i);
        }

        if (_owner.Focused && _owner.FocusCues)
        {
            ControlPaint.DrawFocusRectangle(graphics, ClientRectangle);
        }
    }

    #endregion

    #region Implementation

    private void SetHotIndex(int index)
    {
        if (index == _hotIndex)
        {
            return;
        }

        _hotIndex = index;
        _owner.PerformNeedPaint(false);
        if (_dragIndex < 0)
        {
            _owner.Cursor = index >= 0 && _owner.AllowMove ? Cursors.SizeAll : Cursors.Default;
        }
    }

    private bool BeginMove(int index, Point pt)
    {
        if (!_owner.AllowMove || _owner.Items.Count < 2)
        {
            return false;
        }

        _dragIndex = index;
        _dragCursor = _owner.Cursor;
        _owner.Cursor = Cursors.SizeAll;
        _owner.SelectCore(index, false);
        return true;
    }

    private void MoveItem(Point pt)
    {
        if (_dragIndex < 0)
        {
            return;
        }

        int target = HitTest(pt);
        if (target < 0 || target == _dragIndex)
        {
            return;
        }

        _owner.Reorder(_dragIndex, target);
        _dragIndex = target;
    }

    private void EndMove()
    {
        if (_dragIndex < 0)
        {
            return;
        }

        _dragIndex = -1;
        _owner.Cursor = _dragCursor ?? Cursors.Default;
        _dragCursor = null;
    }

    private int HitTest(Point pt)
    {
        for (int i = _slots.Length - 1; i >= 0; i--)
        {
            if (_slots[i].Bounds.Contains(pt))
            {
                return i;
            }
        }

        return -1;
    }

    private void BuildSlots()
    {
        int count = _owner.Items.Count;
        if (_slots.Length != count)
        {
            _slots = new Slot[count];
        }

        if (count == 0 || ClientRectangle.Width <= 0 || ClientRectangle.Height <= 0)
        {
            return;
        }

        Rectangle client = ClientRectangle;
        bool horizontal = _owner.Orientation == Orientation.Horizontal;
        bool rtl = _owner.RightToLeft == RightToLeft.Yes;
        bool compact = _owner.Layout == TimelineLayout.Compact;
        bool snap = _owner.Layout == TimelineLayout.SnapIcon;
        int textH = _owner.Font.Height + 6;
        int icon = TimelinePaint.IconSize;

        if (horizontal)
        {
            int slotW = Math.Max(1, client.Width / count);
            int axisY = compact
                ? client.Top + TimelinePaint.Pad + (icon / 2)
                : client.Top + TimelinePaint.Pad + textH + TimelinePaint.Gap + (icon / 2);
            for (int i = 0; i < count; i++)
            {
                int visual = rtl ? (count - 1 - i) : i;
                int left = client.Left + (visual * slotW);
                int iconX = snap
                    ? (rtl ? left + slotW - TimelinePaint.Pad - icon : left + TimelinePaint.Pad)
                    : left + Math.Max(0, (slotW - icon) / 2);
                var iconRect = new Rectangle(iconX, axisY - (icon / 2), icon, icon);
                Rectangle startRect;
                Rectangle endRect;
                if (compact)
                {
                    int textTop = iconRect.Bottom + TimelinePaint.Gap;
                    startRect = new Rectangle(left + 2, textTop, Math.Max(1, slotW - 4), textH);
                    endRect = new Rectangle(left + 2, startRect.Bottom + 2, Math.Max(1, slotW - 4), textH);
                }
                else
                {
                    startRect = new Rectangle(left + 2, client.Top + 2, Math.Max(1, slotW - 4), textH);
                    endRect = new Rectangle(left + 2, iconRect.Bottom + TimelinePaint.Gap, Math.Max(1, slotW - 4), textH);
                }

                _slots[i] = MakeSlot(startRect, iconRect, endRect);
            }

            return;
        }

        int slotH = Math.Max(1, client.Height / count);
        int axisX = compact
            ? client.Left + TimelinePaint.Pad + (icon / 2)
            : client.Left + (client.Width / 2);
        if (rtl && compact)
        {
            axisX = client.Right - TimelinePaint.Pad - (icon / 2);
        }

        for (int i = 0; i < count; i++)
        {
            int top = client.Top + (i * slotH);
            int iconY = snap
                ? top + TimelinePaint.Pad
                : top + Math.Max(0, (slotH - icon) / 2);
            var iconRect = new Rectangle(axisX - (icon / 2), iconY, icon, icon);
            Rectangle startRect;
            Rectangle endRect;
            if (compact)
            {
                int textLeft = rtl
                    ? client.Left + TimelinePaint.Pad
                    : iconRect.Right + TimelinePaint.Gap;
                int textRight = rtl
                    ? iconRect.Left - TimelinePaint.Gap
                    : client.Right - TimelinePaint.Pad;
                int textW = Math.Max(1, textRight - textLeft);
                startRect = new Rectangle(textLeft, top + 4, textW, textH);
                endRect = new Rectangle(textLeft, startRect.Bottom + 2, textW, textH);
            }
            else
            {
                int mid = iconRect.Top + ((icon - textH) / 2);
                int leftW = Math.Max(1, iconRect.Left - client.Left - TimelinePaint.Gap);
                int rightW = Math.Max(1, client.Right - iconRect.Right - TimelinePaint.Gap);
                startRect = new Rectangle(client.Left + 2, mid, leftW, textH);
                endRect = new Rectangle(iconRect.Right + TimelinePaint.Gap, mid, rightW, textH);
                if (rtl)
                {
                    Rectangle swap = startRect;
                    startRect = endRect;
                    endRect = swap;
                }
            }

            _slots[i] = MakeSlot(startRect, iconRect, endRect);
        }
    }

    private static Slot MakeSlot(Rectangle start, Rectangle icon, Rectangle end)
    {
        Rectangle bounds = Rectangle.Union(Rectangle.Union(start, icon), end);
        return new Slot { Start = start, Icon = icon, End = end, Bounds = bounds };
    }

    private void DrawConnector(Graphics graphics)
    {
        if (_slots.Length < 2)
        {
            return;
        }

        PaletteState state = _owner.Enabled ? PaletteState.Normal : PaletteState.Disabled;
        Color color = _owner.StateCommon.Border.GetBorderColor1(state);
        if (color.IsEmpty)
        {
            color = _owner.ForeColor;
        }

        int width = _owner.StateCommon.Border.GetBorderWidth(state);
        if (width < 2)
        {
            width = 2;
        }

        using (var pen = new Pen(color, width))
        {
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            for (int i = 0; i < _slots.Length - 1; i++)
            {
                // The line runs from icon centre to icon centre, so the first item has no
                // leading segment and the last item has no trailing segment.
                graphics.DrawLine(pen, Center(_slots[i].Icon), Center(_slots[i + 1].Icon));
            }
        }
    }

    private void DrawItem(RenderContext context, Graphics graphics, int index)
    {
        KryptonTimelineItem item = _owner.Items[index];
        Slot slot = _slots[index];
        PaletteState state = TimelinePaint.ResolveState(_owner.Enabled, index == _owner.SelectedIndex, index == _hotIndex);
        IPaletteTriple palette = _owner.PaletteFor(state);
        Font font = TimelinePaint.FontOr(palette.PaletteContent as PaletteContent ?? _owner.StateCommon.Content, state, _owner.Font);
        Color color = TimelinePaint.TextColor(_owner.StateCommon.Content, state, _owner.ForeColor);
        bool center = _owner.Orientation == Orientation.Horizontal;
        DrawText(context, graphics, item.StartText, item.StartUseBox, slot.Start, palette, state, font, color, center, item.Style, _owner.ItemStyles);
        DrawText(context, graphics, item.EndText, item.EndUseBox, slot.End, palette, state, font, color, center, item.Style, _owner.ItemStyles);
        IPaletteBorder borderPalette = palette.PaletteBorder ?? _owner.StateCommon.Border;
        Color fill = palette.PaletteBack.GetBackColor1(state);
        Color border = borderPalette.GetBorderColor1(state);
        if (fill.IsEmpty)
        {
            fill = color;
        }

        if (border.IsEmpty)
        {
            border = color;
        }

        TimelinePaint.DrawIcon(graphics, slot.Icon, item.Image, fill, border);
        TimelinePaint.DrawItemStyleFill(graphics, slot.Icon, item.Style, TimelinePaint.TimelineItemShape.Ellipse, border, _owner.ItemStyles);
        TimelinePaint.DrawItemStyleMark(graphics, slot.Icon, item.Style, TimelinePaint.TimelineItemShape.Ellipse, color, _owner.ItemStyles);
    }

    private static void DrawText(RenderContext context,
        Graphics graphics,
        string text,
        bool useBox,
        Rectangle bounds,
        IPaletteTriple palette,
        PaletteState state,
        Font font,
        Color color,
        bool center,
        TimelineItemStyle style,
        TimelineItemStyleValues styles)
    {
        if (string.IsNullOrEmpty(text) || bounds.Width <= 1 || bounds.Height <= 1)
        {
            return;
        }

        Rectangle textBounds = bounds;
        if (useBox)
        {
            TimelinePaint.DrawBox(context, bounds, palette.PaletteBack, palette.PaletteBorder, state);
            TimelinePaint.DrawItemStyleFill(graphics, bounds, style, TimelinePaint.TimelineItemShape.Box, color, styles);
            textBounds.Inflate(-4, -1);
            if (style == TimelineItemStyle.Completed && textBounds.Width > 16)
            {
                textBounds.Width -= 12;
            }
        }

        TimelinePaint.DrawLabel(graphics, text, font, color, textBounds, center);
        if (style == TimelineItemStyle.Cancelled)
        {
            TimelinePaint.DrawItemStyleMark(graphics, bounds, style, TimelinePaint.TimelineItemShape.Box, color, styles);
        }
    }

    private static Point Center(Rectangle rect) =>
        new Point(rect.Left + (rect.Width / 2), rect.Top + (rect.Height / 2));

    private struct Slot
    {
        public Rectangle Start;
        public Rectangle Icon;
        public Rectangle End;
        public Rectangle Bounds;
    }

    #endregion
}
