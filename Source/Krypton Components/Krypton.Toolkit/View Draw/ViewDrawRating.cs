#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Draw and operate a rating glyph strip.
/// </summary>
public class ViewDrawRating : ViewLeaf
{
    #region Static Fields

    internal const int DefaultMaximum = 5;
    internal const int MinimumMaximum = 1;
    internal const int MaximumMaximum = 32;

    #endregion

    #region Instance Fields

    private readonly NeedPaintHandler? _needPaint;
    private readonly KryptonRating _owner;
    private decimal _value;
    private decimal _hoverValue;
    private bool _hovering;
    private int _maximum;
    private KryptonRatingPrecision _precision;
    private bool _readOnly;
    private bool _allowClear;
    private bool _focused;
    private Orientation _orientation;
    private RightToLeft _rightToLeft;
    private Rectangle[] _glyphRects = Array.Empty<Rectangle>();

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the value of the Value property changes.
    /// </summary>
    public event EventHandler? ValueChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawRating"/> class.
    /// </summary>
    /// <param name="owner">Owning rating control.</param>
    /// <param name="needPaint">Delegate used to request repainting.</param>
    public ViewDrawRating(KryptonRating owner, NeedPaintHandler needPaint)
    {
        _owner = owner;
        _needPaint = needPaint;
        _maximum = DefaultMaximum;
        _precision = KryptonRatingPrecision.Full;
        _allowClear = true;
        _orientation = Orientation.Horizontal;
        _rightToLeft = RightToLeft.No;

        var controller = new RatingController(this);
        MouseController = controller;
        KeyController = controller;
        SourceController = controller;
    }

    /// <inheritdoc />
    public override string ToString() => $"ViewDrawRating:{Id}";

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the current rating.
    /// </summary>
    public decimal Value
    {
        get => _value;
        set => SetValue(value, true);
    }

    /// <summary>
    /// Gets the hover preview rating. Equals <see cref="Value"/> when the mouse is not over the control.
    /// </summary>
    public decimal HoverValue => _hovering ? _hoverValue : _value;

    /// <summary>
    /// Gets a value indicating whether the mouse is tracking a preview rating.
    /// </summary>
    public bool IsHovering => _hovering;

    /// <summary>
    /// Gets or sets the number of glyphs.
    /// </summary>
    public int Maximum
    {
        get => _maximum;
        set
        {
            value = Math.Max(MinimumMaximum, Math.Min(MaximumMaximum, value));
            if (_maximum != value)
            {
                _maximum = value;
                SetValue(_value, false);
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets how values snap and fill.
    /// </summary>
    public KryptonRatingPrecision Precision
    {
        get => _precision;
        set
        {
            if (_precision != value)
            {
                _precision = value;
                SetValue(_value, true);
            }
        }
    }

    /// <summary>
    /// Gets or sets whether the user can change the rating.
    /// </summary>
    public bool ReadOnly
    {
        get => _readOnly;
        set
        {
            if (_readOnly != value)
            {
                _readOnly = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets whether clicking the current rating (or before the first glyph) clears to zero.
    /// </summary>
    public bool AllowClear
    {
        get => _allowClear;
        set => _allowClear = value;
    }

    /// <summary>
    /// Gets or sets the glyph strip orientation.
    /// </summary>
    public Orientation Orientation
    {
        get => _orientation;
        set
        {
            if (_orientation != value)
            {
                _orientation = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets right-to-left glyph order.
    /// </summary>
    public RightToLeft RightToLeft
    {
        get => _rightToLeft;
        set
        {
            if (_rightToLeft != value)
            {
                _rightToLeft = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether the owning control is focused.
    /// </summary>
    public bool Focused => _focused;

    /// <summary>
    /// Discover the preferred size of the glyph strip.
    /// </summary>
    public Size GetContentSize()
    {
        int item = _owner.RatingValues.ItemSize;
        int spacing = _owner.RatingValues.ItemSpacing;
        int length = _maximum * item + Math.Max(0, _maximum - 1) * spacing;
        return _orientation == Orientation.Horizontal
            ? new Size(length, item)
            : new Size(item, length);
    }

    /// <summary>
    /// Convert a control-relative point to a snapped rating.
    /// </summary>
    /// <param name="pt">Point in control coordinates.</param>
    /// <returns>Snapped rating in 0..<see cref="Maximum"/>.</returns>
    public decimal ValueFromPoint(Point pt)
    {
        if (_glyphRects.Length == 0)
        {
            return _value;
        }

        bool reverse = IsReversed();
        int hitIndex = -1;
        for (int i = 0; i < _glyphRects.Length; i++)
        {
            if (_glyphRects[i].Contains(pt))
            {
                hitIndex = i;
                break;
            }
        }

        if (hitIndex < 0)
        {
            if (IsBeforeFirst(pt))
            {
                return 0m;
            }

            if (IsAfterLast(pt))
            {
                return _maximum;
            }

            hitIndex = NearestGlyphIndex(pt);
        }

        Rectangle glyph = _glyphRects[hitIndex];
        float along = GetAlong(pt, glyph);
        if (reverse)
        {
            along = 1f - along;
        }

        along = Math.Max(0f, Math.Min(1f, along));
        decimal raw = _precision switch
        {
            KryptonRatingPrecision.Full => hitIndex + 1m,
            KryptonRatingPrecision.Half => along < 0.5f ? hitIndex + 0.5m : hitIndex + 1m,
            _ => hitIndex + (decimal)along
        };
        return RatingGlyphPainter.Snap(raw, _precision, _maximum);
    }

    #endregion

    #region Layout

    /// <inheritdoc />
    public override Size GetPreferredSize(ViewLayoutContext context) => GetContentSize();

    /// <inheritdoc />
    public override void Layout(ViewLayoutContext context)
    {
        if (context == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(context));
        }

        ClientRectangle = context.DisplayRectangle;
        LayoutGlyphs();
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

        LayoutGlyphs();

        PaletteBase? palette = _owner.GetResolvedPalette();
        RatingGlyphPainter.ResolveColors(_owner, palette, Enabled, _hovering,
            out Color fill, out Color empty, out Color outline);

        Color activeFill = fill;
        Color activeEmpty = empty;
        Color activeOutline = outline;
        decimal display = HoverValue;
        bool reverseFill = IsReversed();

        for (int i = 0; i < _glyphRects.Length; i++)
        {
            float fraction = RatingGlyphPainter.GlyphFill(display, i + 1, _precision);
            RatingGlyphPainter.DrawGlyph(context.Graphics, _glyphRects[i], _owner.RatingValues.Glyph,
                _owner.RatingValues, fraction, reverseFill, activeFill, activeEmpty, activeOutline, Enabled);
        }

        if (_focused && _owner.DrawFocusCues)
        {
            ControlPaint.DrawFocusRectangle(context.Graphics, ClientRectangle);
        }
    }

    #endregion

    #region Interaction

    internal void OnMouseMove(Point pt)
    {
        decimal hover = ValueFromPoint(pt);
        if (!_hovering || hover != _hoverValue)
        {
            _hovering = true;
            _hoverValue = hover;
            PerformNeedPaint(false);
        }
    }

    internal void OnMouseDown(Point pt)
    {
        if (_readOnly)
        {
            return;
        }

        decimal next = ValueFromPoint(pt);
        if (_allowClear && next == _value)
        {
            next = 0m;
        }

        SetValue(next, true);
    }

    internal void OnMouseLeave()
    {
        if (_hovering)
        {
            _hovering = false;
            PerformNeedPaint(false);
        }
    }

    internal void OnMouseWheel(int delta)
    {
        if (_readOnly || delta == 0)
        {
            return;
        }

        decimal step = RatingGlyphPainter.GetStep(_precision);
        decimal next = delta > 0 ? _value + step : _value - step;
        SetValue(next, true);
    }

    internal void OnKeyDown(KeyEventArgs e)
    {
        if (_readOnly)
        {
            return;
        }

        decimal step = RatingGlyphPainter.GetStep(_precision);
        bool rtl = IsReversed() && _orientation == Orientation.Horizontal;
        switch (e.KeyCode)
        {
            case Keys.Left:
                SetValue(rtl ? _value + step : _value - step, true);
                break;
            case Keys.Right:
                SetValue(rtl ? _value - step : _value + step, true);
                break;
            case Keys.Down:
                SetValue(_value - step, true);
                break;
            case Keys.Up:
                SetValue(_value + step, true);
                break;
            case Keys.Home:
                SetValue(0m, true);
                break;
            case Keys.End:
                SetValue(_maximum, true);
                break;
            case Keys.PageDown:
                SetValue(_value - Math.Max(1m, step * 2m), true);
                break;
            case Keys.PageUp:
                SetValue(_value + Math.Max(1m, step * 2m), true);
                break;
        }
    }

    internal void OnKeyPress(KeyPressEventArgs e)
    {
        if (_readOnly)
        {
            return;
        }

        if (e.KeyChar == '0' && _allowClear)
        {
            SetValue(0m, true);
            e.Handled = true;
            return;
        }

        if (e.KeyChar >= '1' && e.KeyChar <= '9')
        {
            int digit = e.KeyChar - '0';
            if (digit <= _maximum)
            {
                SetValue(digit, true);
                e.Handled = true;
            }
        }
    }

    internal void OnGotFocus()
    {
        _focused = true;
        PerformNeedPaint(false);
    }

    internal void OnLostFocus()
    {
        _focused = false;
        _hovering = false;
        PerformNeedPaint(false);
    }

    #endregion

    #region Implementation

    private void SetValue(decimal value, bool raise)
    {
        decimal snapped = RatingGlyphPainter.Snap(value, _precision, _maximum);
        if (_value == snapped)
        {
            return;
        }

        _value = snapped;
        if (!_hovering)
        {
            _hoverValue = _value;
        }

        if (raise)
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        PerformNeedPaint(false);
    }

    private void LayoutGlyphs()
    {
        int count = _maximum;
        if (_glyphRects.Length != count)
        {
            _glyphRects = new Rectangle[count];
        }

        if (count == 0)
        {
            return;
        }

        Size content = GetContentSize();
        int originX = ClientRectangle.X + Math.Max(0, (ClientRectangle.Width - content.Width) / 2);
        int originY = ClientRectangle.Y + Math.Max(0, (ClientRectangle.Height - content.Height) / 2);
        int item = _owner.RatingValues.ItemSize;
        int spacing = _owner.RatingValues.ItemSpacing;
        bool reverse = IsReversed();

        for (int visual = 0; visual < count; visual++)
        {
            int index = reverse ? count - 1 - visual : visual;
            int offset = visual * (item + spacing);
            _glyphRects[index] = _orientation == Orientation.Horizontal
                ? new Rectangle(originX + offset, originY, item, item)
                : new Rectangle(originX, originY + offset, item, item);
        }
    }

    private bool IsReversed() =>
        _rightToLeft == RightToLeft.Yes;

    private float GetAlong(Point pt, Rectangle glyph)
    {
        if (_orientation == Orientation.Horizontal)
        {
            if (glyph.Width <= 0)
            {
                return 0f;
            }

            return (pt.X - glyph.X) / (float)glyph.Width;
        }

        if (glyph.Height <= 0)
        {
            return 0f;
        }

        return (pt.Y - glyph.Y) / (float)glyph.Height;
    }

    private bool IsBeforeFirst(Point pt)
    {
        if (_glyphRects.Length == 0)
        {
            return false;
        }

        Rectangle first = _glyphRects[0];
        if (_orientation == Orientation.Horizontal)
        {
            return IsReversed() ? pt.X > first.Right : pt.X < first.X;
        }

        return IsReversed() ? pt.Y > first.Bottom : pt.Y < first.Y;
    }

    private bool IsAfterLast(Point pt)
    {
        if (_glyphRects.Length == 0)
        {
            return false;
        }

        Rectangle last = _glyphRects[_glyphRects.Length - 1];
        if (_orientation == Orientation.Horizontal)
        {
            return IsReversed() ? pt.X < last.X : pt.X > last.Right;
        }

        return IsReversed() ? pt.Y < last.Y : pt.Y > last.Bottom;
    }

    private int NearestGlyphIndex(Point pt)
    {
        int best = 0;
        int bestDist = int.MaxValue;
        for (int i = 0; i < _glyphRects.Length; i++)
        {
            int cx = _glyphRects[i].X + _glyphRects[i].Width / 2;
            int cy = _glyphRects[i].Y + _glyphRects[i].Height / 2;
            int dist = (pt.X - cx) * (pt.X - cx) + (pt.Y - cy) * (pt.Y - cy);
            if (dist < bestDist)
            {
                bestDist = dist;
                best = i;
            }
        }

        return best;
    }

    private void PerformNeedPaint(bool needLayout) =>
        _needPaint?.Invoke(this, new NeedLayoutEventArgs(needLayout));

    #endregion
}
