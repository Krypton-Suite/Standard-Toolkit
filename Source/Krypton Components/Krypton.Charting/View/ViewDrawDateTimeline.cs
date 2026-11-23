#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// Draws tasks and milestones on a horizontal time scale.
/// </summary>
internal sealed class ViewDrawDateTimeline : ViewLeaf
{
    private const int HeaderHeight = 40;
    private const int RowHeight = 48;
    private const int BarHeight = 30;
    private const int VisAxisHeight = 36;
    private const int VisBoxHeight = 22;
    private const int VisLaneGap = 8;

    private readonly KryptonDateTimeline _owner;
    private Mark[] _marks;
    private Rectangle _plot;
    private int _axisY;
    private int _hotIndex;
    private int _dragIndex;
    private Point _dragDown;
    private DateTime _dragStart;
    private DateTime _dragFinish;
    private int _dragRow;
    private DateTime _dragRangeStart;
    private DateTime _dragRangeFinish;
    private Cursor? _dragCursor;
    private bool _panArmed;
    private int _panX;
    private DateTime _panStart;
    private DateTime _panFinish;

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawDateTimeline"/> class.
    /// </summary>
    /// <param name="owner">Owning date timeline.</param>
    public ViewDrawDateTimeline(KryptonDateTimeline owner)
    {
        _owner = owner;
        _marks = Array.Empty<Mark>();
        _hotIndex = -1;
        _dragIndex = -1;
        var controller = new TimelineInputController(
            HitTest,
            SetHotIndex,
            _owner.SelectCore,
            () => { },
            _owner.ProcessKey,
            BeginPan,
            MovePan,
            EndPan,
            BeginMove,
            MoveItem,
            EndMove);
        MouseController = controller;
        KeyController = controller;
        SourceController = controller;
    }

    /// <inheritdoc />
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        if (_owner.Style == DateTimelineStyle.Vis)
        {
            int lanes = Math.Max(1, DatedCount());
            return new Size(560, VisAxisHeight + VisLaneGap + (lanes * (VisBoxHeight + VisLaneGap)));
        }

        int rows = RowCount();
        return new Size(560, HeaderHeight + (rows * RowHeight) + TimelinePaint.Pad);
    }

    /// <inheritdoc />
    public override void Layout(ViewLayoutContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        ClientRectangle = context.DisplayRectangle;
        BuildMarks();
    }

    /// <inheritdoc />
    public override bool EvalTransparentPaint(ViewContext context) => true;

    /// <inheritdoc />
    public override void RenderBefore(RenderContext context)
    {
        if (context?.Graphics == null)
        {
            return;
        }

        BuildMarks();
        _owner.GetVisibleRange(out DateTime start, out DateTime finish);
        if (_owner.Style == DateTimelineStyle.Vis)
        {
            DrawVisAxis(context, start, finish);
        }
        else
        {
            DrawHeader(context, start, finish);
        }

        DrawToday(context.Graphics, start, finish);
        for (int i = 0; i < _marks.Length; i++)
        {
            if (_marks[i].Index < 0)
            {
                continue;
            }

            DrawMark(context, i);
        }

        if (_owner.Focused && _owner.FocusCues)
        {
            ControlPaint.DrawFocusRectangle(context.Graphics, ClientRectangle);
        }
    }

    /// <summary>
    /// Zooms the visible range around a control point. Positive delta zooms in.
    /// </summary>
    /// <param name="pt">Cursor position in control coordinates.</param>
    /// <param name="delta">Mouse-wheel delta.</param>
    public void ZoomAt(Point pt, int delta)
    {
        if (_plot.Width <= 1 || delta == 0)
        {
            return;
        }

        _owner.GetVisibleRange(out DateTime start, out DateTime finish);
        DateTime anchor = DateFromX(pt.X, start, finish);
        double factor = delta > 0 ? 0.8 : 1.25;
        long before = (long)((anchor - start).Ticks * factor);
        long after = (long)((finish - anchor).Ticks * factor);
        if (before < 0)
        {
            before = 0;
        }

        if (after < 0)
        {
            after = 0;
        }

        var span = TimeSpan.FromTicks(before + after);
        if (span < TimeSpan.FromMinutes(1) || span > TimeSpan.FromDays(36500))
        {
            return;
        }

        _owner.SetVisibleRange(anchor - TimeSpan.FromTicks(before), anchor + TimeSpan.FromTicks(after));
    }

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
        if (!_owner.AllowMove || index < 0 || index >= _owner.Items.Count)
        {
            return false;
        }

        KryptonDateTimelineItem item = _owner.Items[index];
        if (!item.HasDate || _plot.Width <= 1)
        {
            return false;
        }

        _dragIndex = index;
        _dragDown = pt;
        _dragStart = item.Start;
        _dragFinish = item.Finish;
        _dragRow = item.Row;
        _owner.GetVisibleRange(out _dragRangeStart, out _dragRangeFinish);
        _owner.SetVisibleRange(_dragRangeStart, _dragRangeFinish);
        _dragCursor = _owner.Cursor;
        _owner.Cursor = Cursors.SizeAll;
        _owner.SelectCore(index, false);
        return true;
    }

    private void MoveItem(Point pt)
    {
        if (_dragIndex < 0 || _plot.Width <= 1)
        {
            return;
        }

        double seconds = (_dragRangeFinish - _dragRangeStart).TotalSeconds;
        double secondsPerPixel = seconds / _plot.Width;
        double direction = _owner.RightToLeft == RightToLeft.Yes ? -1d : 1d;
        var shift = TimeSpan.FromSeconds((pt.X - _dragDown.X) * secondsPerPixel * direction);
        int row = _dragRow;
        if (_owner.Style == DateTimelineStyle.Chart)
        {
            row = (pt.Y - _plot.Top) / RowHeight;
            if (row < 0)
            {
                row = 0;
            }
            else if (row > 32)
            {
                row = 32;
            }
        }

        _owner.Items[_dragIndex].Place(ShiftDate(_dragStart, shift), ShiftDate(_dragFinish, shift), row);
    }

    private static DateTime ShiftDate(DateTime value, TimeSpan shift)
    {
        if (value == DateTime.MinValue)
        {
            return value;
        }

        long ticks = value.Ticks + shift.Ticks;
        long min = DateTime.MinValue.Ticks + TimeSpan.TicksPerMinute;
        long max = DateTime.MaxValue.Ticks - TimeSpan.TicksPerMinute;
        if (ticks < min)
        {
            ticks = min;
        }
        else if (ticks > max)
        {
            ticks = max;
        }

        return new DateTime(ticks, value.Kind);
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
        for (int i = _marks.Length - 1; i >= 0; i--)
        {
            if (_marks[i].Index >= 0 && _marks[i].Hit.Contains(pt))
            {
                return _marks[i].Index;
            }
        }

        return -1;
    }

    private void BeginPan(Point pt)
    {
        _panArmed = true;
        _panX = pt.X;
        _owner.GetVisibleRange(out _panStart, out _panFinish);
    }

    private void MovePan(Point pt)
    {
        if (!_panArmed || _plot.Width <= 1)
        {
            return;
        }

        double seconds = (_panFinish - _panStart).TotalSeconds;
        double secondsPerPixel = seconds / _plot.Width;
        double direction = _owner.RightToLeft == RightToLeft.Yes ? 1d : -1d;
        var shift = TimeSpan.FromSeconds((pt.X - _panX) * secondsPerPixel * direction);
        _owner.SetVisibleRange(_panStart + shift, _panFinish + shift);
    }

    private void EndPan() => _panArmed = false;

    private int RowCount()
    {
        int rows = 1;
        foreach (KryptonDateTimelineItem item in _owner.Items)
        {
            if (item.HasDate)
            {
                rows = Math.Max(rows, item.Row + 1);
            }
        }

        return rows;
    }

    private void BuildMarks()
    {
        if (_owner.Style == DateTimelineStyle.Vis)
        {
            BuildVisMarks();
            return;
        }

        Rectangle client = ClientRectangle;
        _plot = new Rectangle(client.Left, client.Top + HeaderHeight, Math.Max(1, client.Width), Math.Max(1, client.Height - HeaderHeight));
        int count = _owner.Items.Count;
        if (_marks.Length != count)
        {
            _marks = new Mark[count];
        }

        _owner.GetVisibleRange(out DateTime start, out DateTime finish);
        for (int i = 0; i < count; i++)
        {
            KryptonDateTimelineItem item = _owner.Items[i];
            if (!item.HasDate)
            {
                _marks[i] = new Mark { Index = -1 };
                continue;
            }

            int y = _plot.Top + (item.Row * RowHeight) + Math.Max(0, (RowHeight - BarHeight) / 2);
            if (item.IsMilestone)
            {
                int x = X(item.Anchor, start, finish);
                var diamond = new Rectangle(x - 7, y + ((BarHeight - 14) / 2), 14, 14);
                var label = new Rectangle(diamond.Right + 4, y, Math.Max(1, _plot.Right - diamond.Right - 6), BarHeight);
                var hit = Rectangle.Union(diamond, label);
                _marks[i] = new Mark { Index = i, Bar = diamond, Label = label, Hit = hit, Milestone = true };
            }
            else
            {
                int x1 = X(item.Anchor, start, finish);
                int x2 = X(item.BarFinish, start, finish);
                if (x2 < x1)
                {
                    int swap = x1;
                    x1 = x2;
                    x2 = swap;
                }

                if (x2 - x1 < 8)
                {
                    x2 = x1 + 8;
                }

                var bar = new Rectangle(x1, y, x2 - x1, BarHeight);
                _marks[i] = new Mark { Index = i, Bar = bar, Label = bar, Hit = bar, Milestone = false };
            }
        }
    }

    private void DrawHeader(RenderContext context, DateTime start, DateTime finish)
    {
        Graphics graphics = context.Graphics!;
        var header = new Rectangle(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width, HeaderHeight);
        TimelinePaint.DrawBox(context, header, _owner.StateNormal.Back, _owner.StateNormal.Border, PaletteState.Normal);
        TimeSpan span = finish - start;
        ChooseScale(span, out ScaleUnit major, out ScaleUnit minor);
        DrawTicks(graphics, start, finish, minor, false);
        DrawTicks(graphics, start, finish, major, true);
    }

    private void DrawTicks(Graphics graphics, DateTime start, DateTime finish, ScaleUnit unit, bool major)
    {
        if (_plot.Width <= 1)
        {
            return;
        }

        PaletteState state = _owner.Enabled ? PaletteState.Normal : PaletteState.Disabled;
        Color color = TimelinePaint.TextColor(_owner.StateCommon.Content, state, _owner.ForeColor);
        Color line = _owner.StateCommon.Border.GetBorderColor1(state);
        if (line.IsEmpty)
        {
            line = color;
        }

        Font font = TimelinePaint.FontOr(_owner.StateCommon.Content, state, _owner.Font);
        int step = StepFor(finish - start, unit);
        DateTime cursor = AlignDown(start, unit);
        if (cursor < start)
        {
            cursor = AddUnit(cursor, unit, step);
        }

        int guard = 0;
        using (var pen = new Pen(line, major ? 1.5f : 1f))
        {
            while (cursor <= finish && guard < 400)
            {
                int x = X(cursor, start, finish);
        int top = ClientRectangle.Top;
        int y1 = major ? top + 2 : top + 20;
        int y2 = major ? top + 14 : top + HeaderHeight - 4;
        if (x >= _plot.Left && x <= _plot.Right)
        {
            graphics.DrawLine(pen, x, y1, x, y2);
                    if (major || unit != ScaleUnit.Year)
                    {
                        var label = new Rectangle(x + 2, major ? ClientRectangle.Top + 1 : ClientRectangle.Top + 18, 72, 16);
                        TimelinePaint.DrawLabel(graphics, FormatTick(cursor, unit), font, color, label, false);
                    }
                }

                DateTime next = AddUnit(cursor, unit, step);
                if (next <= cursor)
                {
                    break;
                }

                cursor = next;
                guard++;
            }
        }
    }

    private void DrawToday(Graphics graphics, DateTime start, DateTime finish)
    {
        if (!_owner.ShowTodayIndicator)
        {
            return;
        }

        DateTime today = DateTime.Today;
        if (today < start || today > finish)
        {
            return;
        }

        int x = X(today, start, finish);
        Color color = _owner.StatePressed.Border.GetBorderColor1(PaletteState.Pressed);
        if (color.IsEmpty)
        {
            color = _owner.StateCommon.Border.GetBorderColor1(PaletteState.Normal);
        }

        using (var pen = new Pen(color, 2f))
        {
            int top = _owner.Style == DateTimelineStyle.Vis ? _plot.Top : ClientRectangle.Top + HeaderHeight;
            int bottom = _owner.Style == DateTimelineStyle.Vis ? _axisY : ClientRectangle.Bottom - 2;
            graphics.DrawLine(pen, x, top, x, bottom);
        }

        int markerTop = _owner.Style == DateTimelineStyle.Vis ? _plot.Top : ClientRectangle.Top + HeaderHeight;
        Point[] marker =
        {
            new Point(x - 5, markerTop),
            new Point(x + 5, markerTop),
            new Point(x, markerTop + 8)
        };
        using (var brush = new SolidBrush(color))
        {
            graphics.FillPolygon(brush, marker);
        }
    }

    private void DrawMark(RenderContext context, int slot)
    {
        Mark mark = _marks[slot];
        if (_owner.Style == DateTimelineStyle.Vis)
        {
            DrawVisItem(context, mark);
            return;
        }

        KryptonDateTimelineItem item = _owner.Items[mark.Index];
        PaletteState state = TimelinePaint.ResolveState(_owner.Enabled, mark.Index == _owner.SelectedIndex, mark.Index == _hotIndex);
        IPaletteTriple palette = _owner.PaletteFor(state);
        Font font = TimelinePaint.FontOr(_owner.StateCommon.Content, state, _owner.Font);
        Color color = TimelinePaint.TextColor(_owner.StateCommon.Content, state, _owner.ForeColor);
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

        if (mark.Milestone)
        {
            if (item.Image != null)
            {
                TimelinePaint.DrawIcon(context.Graphics!, mark.Bar, item.Image, fill, border);
                TimelinePaint.DrawItemStyleFill(context.Graphics!, mark.Bar, item.Style, TimelinePaint.TimelineItemShape.Ellipse, border, _owner.ItemStyles);
            }
            else
            {
                TimelinePaint.DrawDiamond(context.Graphics!, mark.Bar, fill, border);
                TimelinePaint.DrawItemStyleFill(context.Graphics!, mark.Bar, item.Style, TimelinePaint.TimelineItemShape.Diamond, border, _owner.ItemStyles);
            }

            TimelinePaint.DrawItemStyleMark(context.Graphics!, mark.Bar, item.Style, TimelinePaint.TimelineItemShape.Diamond, color, _owner.ItemStyles);
            DrawCaption(context.Graphics!, item, font, color, mark.Label);
            if (item.Style == TimelineItemStyle.Cancelled)
            {
                TimelinePaint.DrawItemStyleMark(context.Graphics!, mark.Label, item.Style, TimelinePaint.TimelineItemShape.Box, color, _owner.ItemStyles);
            }

            return;
        }

        TimelinePaint.DrawBox(context, mark.Bar, palette.PaletteBack, palette.PaletteBorder, state);
        TimelinePaint.DrawItemStyleFill(context.Graphics!, mark.Bar, item.Style, TimelinePaint.TimelineItemShape.Box, border, _owner.ItemStyles);
        Rectangle inner = mark.Bar;
        inner.Inflate(-4, -2);
        if (item.Image != null && inner.Width > 18)
        {
            var imageRect = new Rectangle(inner.Left, inner.Top + Math.Max(0, (inner.Height - 16) / 2), 16, 16);
            context.Graphics!.DrawImage(item.Image, imageRect);
            inner.X += 18;
            inner.Width -= 18;
        }

        if (item.Style == TimelineItemStyle.Completed && inner.Width > 16)
        {
            inner.Width -= 14;
        }

        DrawCaption(context.Graphics!, item, font, color, inner);
        TimelinePaint.DrawItemStyleMark(context.Graphics!, mark.Bar, item.Style, TimelinePaint.TimelineItemShape.Box, color, _owner.ItemStyles);
    }

    private static void DrawCaption(Graphics graphics, KryptonDateTimelineItem item, Font font, Color color, Rectangle bounds)
    {
        if (bounds.Width <= 1 || bounds.Height <= 1)
        {
            return;
        }

        if (string.IsNullOrEmpty(item.Details) || bounds.Height < (font.Height * 2))
        {
            TimelinePaint.DrawLabel(graphics, item.Caption, font, color, bounds, false);
            return;
        }

        int half = bounds.Height / 2;
        var caption = new Rectangle(bounds.Left, bounds.Top, bounds.Width, half);
        var details = new Rectangle(bounds.Left, bounds.Top + half, bounds.Width, bounds.Height - half);
        TimelinePaint.DrawLabel(graphics, item.Caption, font, color, caption, false);
        TimelinePaint.DrawLabel(graphics, item.Details, font, color, details, false);
    }

    private int DatedCount()
    {
        int count = 0;
        foreach (KryptonDateTimelineItem item in _owner.Items)
        {
            if (item.HasDate)
            {
                count++;
            }
        }

        return count;
    }

    private void BuildVisMarks()
    {
        Rectangle client = ClientRectangle;
        _axisY = client.Bottom - VisAxisHeight;
        int plotBottom = Math.Max(client.Top + 1, _axisY);
        _plot = new Rectangle(client.Left, client.Top, Math.Max(1, client.Width), Math.Max(1, plotBottom - client.Top));
        int count = _owner.Items.Count;
        if (_marks.Length != count)
        {
            _marks = new Mark[count];
        }

        for (int i = 0; i < count; i++)
        {
            _marks[i] = new Mark { Index = -1 };
        }

        if (count == 0 || _plot.Width <= 1)
        {
            return;
        }

        _owner.GetVisibleRange(out DateTime start, out DateTime finish);
        var boxes = new List<VisBox>();
        for (int i = 0; i < count; i++)
        {
            KryptonDateTimelineItem item = _owner.Items[i];
            if (!item.HasDate)
            {
                continue;
            }

            int textWidth = MeasureBox(item.Caption);
            int left;
            int right;
            int axisX = X(item.Anchor, start, finish);
            if (item.IsMilestone)
            {
                left = axisX - (textWidth / 2);
                if (left < _plot.Left)
                {
                    left = _plot.Left;
                }

                if (left + textWidth > _plot.Right)
                {
                    left = Math.Max(_plot.Left, _plot.Right - textWidth);
                }

                right = left + textWidth;
            }
            else
            {
                int x1 = X(item.Anchor, start, finish);
                int x2 = X(item.BarFinish, start, finish);
                if (x2 < x1)
                {
                    int swap = x1;
                    x1 = x2;
                    x2 = swap;
                }

                left = x1;
                right = Math.Max(x2, x1 + textWidth);
            }

            boxes.Add(new VisBox
            {
                Index = i,
                Left = left,
                Right = right,
                Point = item.IsMilestone,
                AxisX = axisX
            });
        }

        boxes.Sort((a, b) => a.Left != b.Left ? a.Left.CompareTo(b.Left) : a.Index.CompareTo(b.Index));
        var laneRight = new int[Math.Max(1, boxes.Count)];
        int lanes = 0;
        for (int i = 0; i < boxes.Count; i++)
        {
            VisBox box = boxes[i];
            int lane = 0;
            while (lane < lanes && laneRight[lane] + VisLaneGap > box.Left)
            {
                lane++;
            }

            if (lane == lanes)
            {
                lanes++;
            }

            laneRight[lane] = box.Right;
            int y = _axisY - VisLaneGap - VisBoxHeight - (lane * (VisBoxHeight + VisLaneGap));
            var rect = new Rectangle(box.Left, y, Math.Max(1, box.Right - box.Left), VisBoxHeight);
            Rectangle hit = rect;
            if (box.Point)
            {
                var stem = new Rectangle(box.AxisX - 3, rect.Bottom, 6, Math.Max(1, _axisY - rect.Bottom));
                hit = Rectangle.Union(rect, stem);
            }

            _marks[box.Index] = new Mark
            {
                Index = box.Index,
                Bar = rect,
                Label = rect,
                Hit = hit,
                Milestone = box.Point,
                AxisX = box.AxisX
            };
        }
    }

    private int MeasureBox(string caption)
    {
        string text = caption.Length == 0 ? " " : caption;
        Size size = TextRenderer.MeasureText(text, _owner.Font, new Size(int.MaxValue, VisBoxHeight), TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
        return Math.Max(48, size.Width + 16);
    }

    private void DrawVisAxis(RenderContext context, DateTime start, DateTime finish)
    {
        Graphics graphics = context.Graphics!;
        PaletteState state = _owner.Enabled ? PaletteState.Normal : PaletteState.Disabled;
        Color color = TimelinePaint.TextColor(_owner.StateCommon.Content, state, _owner.ForeColor);
        Color line = _owner.StateCommon.Border.GetBorderColor1(state);
        if (line.IsEmpty)
        {
            line = color;
        }

        using (var pen = new Pen(line, 1f))
        {
            graphics.DrawLine(pen, _plot.Left, _axisY, _plot.Right, _axisY);
        }

        DrawVisTicks(graphics, start, finish, line, color);
    }

    private void DrawVisTicks(Graphics graphics, DateTime start, DateTime finish, Color line, Color text)
    {
        if (_plot.Width <= 1)
        {
            return;
        }

        TimeSpan span = finish - start;
        ChooseScale(span, out ScaleUnit major, out ScaleUnit minor);
        PaletteState state = _owner.Enabled ? PaletteState.Normal : PaletteState.Disabled;
        Font font = TimelinePaint.FontOr(_owner.StateCommon.Content, state, _owner.Font);
        using (var brush = new SolidBrush(line))
        {
            DrawVisTickRow(graphics, brush, font, text, start, finish, minor, false);
            DrawVisTickRow(graphics, brush, font, text, start, finish, major, true);
        }
    }

    private void DrawVisTickRow(Graphics graphics, Brush brush, Font font, Color text, DateTime start, DateTime finish, ScaleUnit unit, bool major)
    {
        int step = StepFor(finish - start, unit);
        DateTime cursor = AlignDown(start, unit);
        if (cursor < start)
        {
            cursor = AddUnit(cursor, unit, step);
        }

        int guard = 0;
        while (cursor <= finish && guard < 400)
        {
            int x = X(cursor, start, finish);
            if (x >= _plot.Left && x <= _plot.Right)
            {
                graphics.FillEllipse(brush, x - 3, _axisY - 3, 6, 6);
                if (major)
                {
                    const int labelWidth = 88;
                    int labelLeft = x + 4;
                    if (labelLeft + labelWidth > _plot.Right)
                    {
                        labelLeft = Math.Max(_plot.Left, x - labelWidth);
                    }

                    var label = new Rectangle(labelLeft, _axisY + 4, labelWidth, 16);
                    TimelinePaint.DrawLabel(graphics, FormatTick(cursor, unit), font, text, label, false);
                }
            }

            DateTime next = AddUnit(cursor, unit, step);
            if (next <= cursor)
            {
                break;
            }

            cursor = next;
            guard++;
        }
    }

    private void DrawVisItem(RenderContext context, Mark mark)
    {
        KryptonDateTimelineItem item = _owner.Items[mark.Index];
        PaletteState state = TimelinePaint.ResolveState(_owner.Enabled, mark.Index == _owner.SelectedIndex, mark.Index == _hotIndex);
        IPaletteTriple palette = _owner.PaletteFor(state);
        Font font = TimelinePaint.FontOr(_owner.StateCommon.Content, state, _owner.Font);
        Color color = TimelinePaint.TextColor(_owner.StateCommon.Content, state, _owner.ForeColor);
        Graphics graphics = context.Graphics!;
        if (mark.Milestone)
        {
            using (var pen = new Pen(color, 1f))
            {
                graphics.DrawLine(pen, mark.AxisX, mark.Bar.Bottom, mark.AxisX, _axisY);
            }

            using (var brush = new SolidBrush(color))
            {
                graphics.FillEllipse(brush, mark.AxisX - 3, _axisY - 3, 6, 6);
            }
        }

        TimelinePaint.DrawBox(context, mark.Bar, palette.PaletteBack, palette.PaletteBorder, state);
        TimelinePaint.DrawItemStyleFill(graphics, mark.Bar, item.Style, TimelinePaint.TimelineItemShape.Box, color, _owner.ItemStyles);
        Rectangle inner = mark.Bar;
        inner.Inflate(-4, -1);
        if (item.Style == TimelineItemStyle.Completed && inner.Width > 16)
        {
            inner.Width -= 14;
        }

        TimelinePaint.DrawLabel(graphics, item.Caption, font, color, inner, true);
        TimelinePaint.DrawItemStyleMark(graphics, mark.Bar, item.Style, TimelinePaint.TimelineItemShape.Box, color, _owner.ItemStyles);
    }

    private struct VisBox
    {
        public int Index;
        public int Left;
        public int Right;
        public bool Point;
        public int AxisX;
    }

    private int X(DateTime value, DateTime start, DateTime finish)
    {
        double span = (finish - start).TotalSeconds;
        if (span <= 0 || _plot.Width <= 1)
        {
            return _plot.Left;
        }

        double t = (value - start).TotalSeconds / span;
        if (_owner.RightToLeft == RightToLeft.Yes)
        {
            t = 1d - t;
        }

        if (t < 0)
        {
            t = 0;
        }
        else if (t > 1)
        {
            t = 1;
        }

        return _plot.Left + (int)(t * _plot.Width);
    }

    private DateTime DateFromX(int x, DateTime start, DateTime finish)
    {
        double span = (finish - start).TotalSeconds;
        if (span <= 0 || _plot.Width <= 1)
        {
            return start;
        }

        double t = (x - _plot.Left) / (double)_plot.Width;
        if (_owner.RightToLeft == RightToLeft.Yes)
        {
            t = 1d - t;
        }

        if (t < 0)
        {
            t = 0;
        }
        else if (t > 1)
        {
            t = 1;
        }

        return start + TimeSpan.FromSeconds(t * span);
    }

    private static void ChooseScale(TimeSpan span, out ScaleUnit major, out ScaleUnit minor)
    {
        double days = span.TotalDays;
        if (days <= 2)
        {
            major = ScaleUnit.Day;
            minor = ScaleUnit.Hour;
        }
        else if (days <= 70)
        {
            major = ScaleUnit.Month;
            minor = ScaleUnit.Day;
        }
        else if (days <= 800)
        {
            major = ScaleUnit.Year;
            minor = ScaleUnit.Month;
        }
        else
        {
            major = ScaleUnit.Year;
            minor = ScaleUnit.Year;
        }
    }

    private static int StepFor(TimeSpan span, ScaleUnit unit)
    {
        if (unit == ScaleUnit.Hour && span.TotalHours > 18)
        {
            return 6;
        }

        if (unit == ScaleUnit.Day && span.TotalDays > 21)
        {
            return 7;
        }

        if (unit == ScaleUnit.Month && span.TotalDays > 500)
        {
            return 3;
        }

        if (unit == ScaleUnit.Year && span.TotalDays > 3650)
        {
            return 5;
        }

        return 1;
    }

    private static DateTime AlignDown(DateTime value, ScaleUnit unit)
    {
        switch (unit)
        {
            case ScaleUnit.Hour:
                return new DateTime(value.Year, value.Month, value.Day, value.Hour, 0, 0);
            case ScaleUnit.Day:
                return value.Date;
            case ScaleUnit.Month:
                return new DateTime(value.Year, value.Month, 1);
            default:
                return new DateTime(value.Year, 1, 1);
        }
    }

    private static DateTime AddUnit(DateTime value, ScaleUnit unit, int step)
    {
        switch (unit)
        {
            case ScaleUnit.Hour:
                return value.AddHours(step);
            case ScaleUnit.Day:
                return value.AddDays(step);
            case ScaleUnit.Month:
                return value.AddMonths(step);
            default:
                return value.AddYears(step);
        }
    }

    private static string FormatTick(DateTime value, ScaleUnit unit)
    {
        switch (unit)
        {
            case ScaleUnit.Hour:
                return value.ToString("HH:mm");
            case ScaleUnit.Day:
                return value.ToString("d MMM");
            case ScaleUnit.Month:
                return value.ToString("MMM yyyy");
            default:
                return value.ToString("yyyy");
        }
    }

    private enum ScaleUnit
    {
        Hour,
        Day,
        Month,
        Year
    }

    private struct Mark
    {
        public int Index;
        public Rectangle Bar;
        public Rectangle Label;
        public Rectangle Hit;
        public bool Milestone;
        public int AxisX;
    }
}
