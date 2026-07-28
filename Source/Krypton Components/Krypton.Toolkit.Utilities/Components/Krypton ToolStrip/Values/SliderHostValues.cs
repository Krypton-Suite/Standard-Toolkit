#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Expandable configuration for the <see cref="KryptonSlider"/> tool strip host. Mirrors the settings of
/// the hosted <see cref="KryptonToolbarSlider"/> (<see cref="KryptonSlider.Tracker"/>).
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class SliderHostValues : Storage
{
    #region Constants

    private static readonly Size DEFAULT_TRACKER_SIZE = new Size(140, 16);

    #endregion

    #region Instance Fields

    private readonly KryptonSlider _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="SliderHostValues"/> class.
    /// </summary>
    /// <param name="owner">Owning slider host.</param>
    public SliderHostValues(KryptonSlider owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        (_owner.Tracker is null || _owner.Tracker.SliderValues.IsDefault) &&
        (_owner.Tracker is null || _owner.Tracker.Size == DEFAULT_TRACKER_SIZE);

    #endregion

    #region Public

    /// <summary>
    /// Determines if the slider buttons are single click or machine gun fire.
    /// </summary>
    [Category(@"Slider Values")]
    [DefaultValue(false)]
    public bool SingleClick
    {
        get => _owner.Tracker?.SingleClick ?? false;
        set
        {
            if (_owner.Tracker is { } tracker)
            {
                tracker.SingleClick = value;
            }
        }
    }

    /// <summary>
    /// The interval at which the slider buttons fire events.
    /// </summary>
    [Category(@"Slider Values")]
    [DefaultValue(200)]
    public int FireInterval
    {
        get => _owner.Tracker?.FireInterval ?? 200;
        set
        {
            if (_owner.Tracker is { } tracker)
            {
                tracker.FireInterval = value;
            }
        }
    }

    /// <summary>
    /// The size of the hosted slider tracker control.
    /// </summary>
    [Category(@"Slider Values")]
    [DefaultValue(typeof(Size), "140, 16")]
    public Size TrackerSize
    {
        get => _owner.Tracker?.Size ?? DEFAULT_TRACKER_SIZE;
        set
        {
            if (_owner.Tracker is { } tracker)
            {
                tracker.Size = value;
            }
        }
    }

    /// <summary>
    /// The current value of the slider.
    /// </summary>
    [Category(@"Slider Values")]
    [DefaultValue(0)]
    public int Value
    {
        get => _owner.Tracker?.Value ?? 0;
        set
        {
            if (_owner.Tracker is { } tracker)
            {
                tracker.Value = value;
            }
        }
    }

    /// <summary>
    /// The range of the slider.
    /// </summary>
    [Category(@"Slider Values")]
    [DefaultValue(100)]
    public int Range
    {
        get => _owner.Tracker?.Range ?? 100;
        set
        {
            if (_owner.Tracker is { } tracker)
            {
                tracker.Range = value;
            }
        }
    }

    /// <summary>
    /// The step size of the slider.
    /// </summary>
    [Category(@"Slider Values")]
    [DefaultValue(5)]
    public int Steps
    {
        get => _owner.Tracker?.Steps ?? 5;
        set
        {
            if (_owner.Tracker is { } tracker)
            {
                tracker.Steps = value;
            }
        }
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        if (_owner.Tracker is { } tracker)
        {
            tracker.SliderValues.Reset();
            tracker.Size = DEFAULT_TRACKER_SIZE;
        }
    }

    #endregion
}
