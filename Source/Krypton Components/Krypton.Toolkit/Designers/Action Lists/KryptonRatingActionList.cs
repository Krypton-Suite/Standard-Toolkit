#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonRatingActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonRating _rating;
    private readonly IComponentChangeService? _service;
    private string _action;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRatingActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonRatingActionList(KryptonRatingDesigner owner)
        : base(owner.Component)
    {
        _rating = (owner.Component as KryptonRating)!;
        _action = _rating.Orientation == Orientation.Vertical
            ? @"Horizontal orientation"
            : @"Vertical orientation";
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the current rating.
    /// </summary>
    public decimal Value
    {
        get => _rating.Value;
        set
        {
            if (_rating.Value != value)
            {
                _service?.OnComponentChanged(_rating, null, _rating.Value, value);
                _rating.Value = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the number of glyphs.
    /// </summary>
    public int Maximum
    {
        get => _rating.Maximum;
        set
        {
            if (_rating.Maximum != value)
            {
                _service?.OnComponentChanged(_rating, null, _rating.Maximum, value);
                _rating.Maximum = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets how values snap and fill.
    /// </summary>
    public KryptonRatingPrecision Precision
    {
        get => _rating.Precision;
        set
        {
            if (_rating.Precision != value)
            {
                _service?.OnComponentChanged(_rating, null, _rating.Precision, value);
                _rating.Precision = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets whether the user can change the rating.
    /// </summary>
    public bool ReadOnly
    {
        get => _rating.ReadOnly;
        set
        {
            if (_rating.ReadOnly != value)
            {
                _service?.OnComponentChanged(_rating, null, _rating.ReadOnly, value);
                _rating.ReadOnly = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the glyph drawn for each rating item.
    /// </summary>
    public KryptonRatingGlyph Glyph
    {
        get => _rating.RatingValues.Glyph;
        set
        {
            if (_rating.RatingValues.Glyph != value)
            {
                _service?.OnComponentChanged(_rating, null, _rating.RatingValues.Glyph, value);
                _rating.RatingValues.Glyph = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _rating.PaletteMode;
        set
        {
            if (_rating.PaletteMode != value)
            {
                _service?.OnComponentChanged(_rating, null, _rating.PaletteMode, value);
                _rating.PaletteMode = value;
            }
        }
    }

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();
        if (_rating != null)
        {
            actions.Add(new DesignerActionHeaderItem(@"Layout"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(_action, OnOrientationClick), @"Layout"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Value), nameof(Value), @"Behavior", @"Current rating"));
            actions.Add(new DesignerActionPropertyItem(nameof(Maximum), nameof(Maximum), @"Behavior", @"Number of glyphs"));
            actions.Add(new DesignerActionPropertyItem(nameof(Precision), nameof(Precision), @"Behavior", @"Snap precision"));
            actions.Add(new DesignerActionPropertyItem(nameof(ReadOnly), @"Read Only", @"Behavior", @"Prevent the user changing the rating"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(Glyph), nameof(Glyph), @"Visuals", @"Glyph shape"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }

    #endregion

    #region Implementation

    private void OnOrientationClick(object? sender, EventArgs e)
    {
        if (sender is DesignerVerb verb)
        {
            Orientation orientation = verb.Text.Equals(@"Horizontal orientation")
                ? Orientation.Horizontal
                : Orientation.Vertical;
            _action = orientation == Orientation.Vertical ? @"Horizontal orientation" : @"Vertical orientation";
            PropertyDescriptor? orientationProp = TypeDescriptor.GetProperties(_rating)[nameof(Orientation)];
            orientationProp?.SetValue(_rating, orientation);

            if (GetService(typeof(DesignerActionUIService)) is DesignerActionUIService service)
            {
                service.Refresh(_rating);
            }
        }
    }

    #endregion
}
