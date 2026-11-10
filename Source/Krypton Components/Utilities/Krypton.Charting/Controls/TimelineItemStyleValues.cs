#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// Colours for one <see cref="TimelineItemStyle"/> overlay.
/// An empty colour keeps the built-in colour for that role, or draws nothing when the style has no built-in for it.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TimelineItemStyleColors : Storage
{
    #region Instance Fields

    private Color _stripe;
    private Color _hatch;
    private Color _mark;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="TimelineItemStyleColors"/> class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public TimelineItemStyleColors(NeedPaintHandler? needPaint) => NeedPaint = needPaint;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the left status band.
    /// Empty keeps the built-in stripe, or draws no stripe when the style has none.
    /// </summary>
    [KryptonDefaultColor]
    [Category(@"Appearance")]
    [Description(@"Left status band. Empty keeps the built-in stripe, or draws no stripe when the style has none.")]
    public Color Stripe
    {
        get => _stripe;
        set
        {
            if (_stripe != value)
            {
                _stripe = value;
                PerformNeedPaint();
            }
        }
    }

    /// <summary>
    /// Gets or sets the hatch colour.
    /// Empty keeps the built-in hatch, or draws no hatch when the style has none.
    /// </summary>
    [KryptonDefaultColor]
    [Category(@"Appearance")]
    [Description(@"Hatch colour. Empty keeps the built-in hatch, or draws no hatch when the style has none.")]
    public Color Hatch
    {
        get => _hatch;
        set
        {
            if (_hatch != value)
            {
                _hatch = value;
                PerformNeedPaint();
            }
        }
    }

    /// <summary>
    /// Gets or sets the check, strike, or outline colour.
    /// Empty keeps the built-in mark. On <see cref="TimelineItemStyle.Hatched"/>, a colour here draws an outline.
    /// </summary>
    [KryptonDefaultColor]
    [Category(@"Appearance")]
    [Description(@"Check, strike, or outline. Empty keeps the built-in mark. On Hatched, a colour here draws an outline.")]
    public Color Mark
    {
        get => _mark;
        set
        {
            if (_mark != value)
            {
                _mark = value;
                PerformNeedPaint();
            }
        }
    }

    /// <summary>
    /// Restore the built-in colours.
    /// </summary>
    public void Reset()
    {
        bool changed = !_stripe.IsEmpty || !_hatch.IsEmpty || !_mark.IsEmpty;
        _stripe = Color.Empty;
        _hatch = Color.Empty;
        _mark = Color.Empty;
        if (changed)
        {
            PerformNeedPaint();
        }
    }

    /// <summary>
    /// Gets a value indicating if every colour is empty.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => _stripe.IsEmpty && _hatch.IsEmpty && _mark.IsEmpty;

    #endregion
}

/// <summary>
/// Colours for each <see cref="TimelineItemStyle"/> overlay.
/// Expand this in the property grid to edit Hatched, Completed, Delayed, and Cancelled.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TimelineItemStyleValues : Storage
{
    private static readonly TimelineItemStyleColors _unused = new TimelineItemStyleColors(null);

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="TimelineItemStyleValues"/> class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public TimelineItemStyleValues(NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;
        Hatched = new TimelineItemStyleColors(needPaint);
        Completed = new TimelineItemStyleColors(needPaint);
        Delayed = new TimelineItemStyleColors(needPaint);
        Cancelled = new TimelineItemStyleColors(needPaint);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the hatch colour for <see cref="TimelineItemStyle.Hatched"/>.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Colours for the hatched style.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TimelineItemStyleColors Hatched { get; }

    private bool ShouldSerializeHatched() => !Hatched.IsDefault;

    private void ResetHatched() => Hatched.Reset();

    /// <summary>
    /// Gets the stripe and check colours for <see cref="TimelineItemStyle.Completed"/>.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Colours for the completed style.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TimelineItemStyleColors Completed { get; }

    private bool ShouldSerializeCompleted() => !Completed.IsDefault;

    private void ResetCompleted() => Completed.Reset();

    /// <summary>
    /// Gets the stripe, hatch, and outline colours for <see cref="TimelineItemStyle.Delayed"/>.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Colours for the delayed style.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TimelineItemStyleColors Delayed { get; }

    private bool ShouldSerializeDelayed() => !Delayed.IsDefault;

    private void ResetDelayed() => Delayed.Reset();

    /// <summary>
    /// Gets the stripe, hatch, and strike colours for <see cref="TimelineItemStyle.Cancelled"/>.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Colours for the cancelled style.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TimelineItemStyleColors Cancelled { get; }

    private bool ShouldSerializeCancelled() => !Cancelled.IsDefault;

    private void ResetCancelled() => Cancelled.Reset();

    /// <summary>
    /// Restore every style to its built-in colours.
    /// </summary>
    public void Reset()
    {
        Hatched.Reset();
        Completed.Reset();
        Delayed.Reset();
        Cancelled.Reset();
    }

    /// <summary>
    /// Gets a value indicating if every style still uses its built-in colours.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Hatched.IsDefault && Completed.IsDefault && Delayed.IsDefault && Cancelled.IsDefault;

    #endregion

    #region Internal

    internal TimelineItemStyleColors For(TimelineItemStyle style)
    {
        switch (style)
        {
            case TimelineItemStyle.Completed:
                return Completed;
            case TimelineItemStyle.Delayed:
                return Delayed;
            case TimelineItemStyle.Cancelled:
                return Cancelled;
            case TimelineItemStyle.Hatched:
                return Hatched;
            default:
                // Normal has no overlay colours. Keep a throwaway instance so a miss does not paint Hatched overrides.
                return _unused;
        }
    }

    #endregion
}
