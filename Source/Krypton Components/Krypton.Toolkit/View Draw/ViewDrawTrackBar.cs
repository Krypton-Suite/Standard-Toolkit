﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Draw and operate a track bar.
    /// </summary>
    public class ViewDrawTrackBar : ViewDrawPanel
    {
        #region Static Fields
        private static readonly Size _positionSizeSmallH = new(11, 15);
        private static readonly Size _positionSizeSmallV = new(15, 11);
        private static readonly Size _positionSizeMediumH = new(13, 21);
        private static readonly Size _positionSizeMediumV = new(21, 13);
        private static readonly Size _positionSizeLargeH = new(17, 27);
        private static readonly Size _positionSizeLargeV = new(27, 17);
        private static readonly Size _trackSizeSmall = new(2, 2);
        private static readonly Size _trackSizeSmallV = new(6, 6);
        private static readonly Size _trackSizeMedium = new(4, 4);
        private static readonly Size _trackSizeMediumV = new(11, 11);
        private static readonly Size _trackSizeLarge = new(5, 5);
        private static readonly Size _trackSizeLargeV = new(16, 16);
        private static readonly Size _tickSizeSmall = new(5, 5);
        private static readonly Size _tickSizeMedium = new(6, 6);
        private static readonly Size _tickSizeLarge = new(7, 7);
        #endregion

        #region Instance Fields

        private Orientation _orientation;
        private TickStyle _tickStyle;
        private int _tickFreq;
        private int _value;
        private int _minimum;
        private int _maximum;
        private int _smallChange;
        private int _largeChange;
        private readonly ViewLayoutDocker? _layoutTop;
        private readonly ViewDrawTrackTicks? _ticksTop;
        private readonly ViewDrawTrackTicks? _ticksBottom;
        private readonly NeedPaintHandler _needPaint;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the Value property changes.
        /// </summary>
        public event EventHandler ValueChanged;

        /// <summary>
        /// Occurs when the value has changed because of a user change.
        /// </summary>
        public event EventHandler Scroll;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawTrackBar class.
        /// </summary>
        /// <param name="stateNormal">Referenece to normal state values.</param>
        /// <param name="stateDisabled">Referenece to disabled state values.</param>
        /// <param name="stateTracking">Referenece to tracking state values.</param>
        /// <param name="statePressed">Referenece to pressed state values.</param>
        /// <param name="needPaint">Delegate used to request repainting.</param>
        public ViewDrawTrackBar(PaletteTrackBarStatesOverride stateNormal,
                                PaletteTrackBarStates stateDisabled,
                                PaletteTrackBarPositionStatesOverride stateTracking,
                                PaletteTrackBarPositionStatesOverride statePressed,
                                NeedPaintHandler needPaint)
            : base(stateNormal.Back)
        {
            // Default state
            StateNormal = stateNormal;
            StateDisabled = stateDisabled;
            StateTracking = stateTracking;
            StatePressed = statePressed;
            Padding = Padding.Empty;
            _orientation = Orientation.Horizontal;
            _value = 0;
            _minimum = 0;
            _maximum = 10;
            _smallChange = 1;
            _largeChange = 5;
            _tickFreq = 1;
            _tickStyle = TickStyle.BottomRight;
            TrackBarSize = PaletteTrackBarSize.Medium;
            VolumeControl = false;
            _needPaint = needPaint;

            // Create drawing/layout elements
            TrackPosition = new ViewDrawTP(this);
            _ticksTop = new ViewDrawTrackTicks(this, true);
            _ticksBottom = new ViewDrawTrackTicks(this, false);
            _ticksTop.Visible = false;
            _ticksBottom.Visible = true;

            // Connect up layout structure
            _layoutTop = new ViewLayoutDocker
            {
                { _ticksTop, ViewDockStyle.Top },
                { TrackPosition, ViewDockStyle.Top },
                { _ticksBottom, ViewDockStyle.Top }
            };
            _layoutTop.Padding = Padding;
            Add(_layoutTop);
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            "ViewDrawTrackBar:" + Id;

        #endregion

        #region Public
        /// <summary>
        /// Gets the track position element.
        /// </summary>
        public ViewDrawTP? TrackPosition { get; }

        /// <summary>
        /// Gets and sets the track bar size.
        /// </summary>
        public PaletteTrackBarSize TrackBarSize { get; set; }

        /// <summary>
        /// Gets and sets if the track bar displays like a volume control.
        /// </summary>
        public bool VolumeControl { get; set; }

        /// <summary>
        /// Gets and sets the internal padding space.
        /// </summary>
        public Padding Padding { get; set; }

        /// <summary>
        /// Gets and sets the right to left setting.
        /// </summary>
        public RightToLeft RightToLeft { get; set; }

        /// <summary>
        /// Gets or sets a value indicating how to display the tick marks on the track bar.
        /// </summary>
        public TickStyle TickStyle
        {
            get => _tickStyle;

            set
            {
                if (value != _tickStyle)
                {
                    _tickStyle = value;

                    // Decide which of the tick tracks are needed
                    var topLeft = false;
                    var bottomRight = false;
                    switch (_tickStyle)
                    {
                        case TickStyle.TopLeft:
                            topLeft = true;
                            break;
                        case TickStyle.BottomRight:
                            bottomRight = true;
                            break;
                        case TickStyle.Both:
                            topLeft = true;
                            bottomRight = true;
                            break;
                    }

                    _ticksTop.Visible = topLeft;
                    _ticksBottom.Visible = bottomRight;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that specifies the delta between ticks drawn on the control.
        /// </summary>
        public int TickFrequency
        {
            get => _tickFreq;

            set
            {
                if (value != _tickFreq)
                {
                    _tickFreq = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the horizontal or vertical orientation of the track bar.
        /// </summary>
        public Orientation Orientation
        {
            get => _orientation;

            set
            {
                if (value != _orientation)
                {
                    _orientation = value;
                    VisualOrientation visual = _orientation == Orientation.Horizontal ? VisualOrientation.Top : VisualOrientation.Right;
                    _layoutTop.Orientation = visual;
                }
            }
        }

        /// <summary>
        /// Gets or sets the upper limit of the range this trackbar is working with.
        /// </summary>
        public int Maximum
        {
            get => _maximum;

            set
            {
                if (value != _maximum)
                {
                    if (value < _minimum)
                    {
                        _minimum = value;
                    }

                    SetRange(Minimum, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the lower limit of the range this trackbar is working with.
        /// </summary>
        public int Minimum
        {
            get => _minimum;

            set
            {
                if (value != _minimum)
                {
                    if (value > _maximum)
                    {
                        _maximum = value;
                    }

                    SetRange(value, Maximum);
                }
            }
        }

        /// <summary>
        /// Gets or sets a numeric value that represents the current position of the scroll box on the track bar.
        /// </summary>
        public int Value
        {
            get => _value;

            set
            {
                if (value != _value)
                {
                    if ((value < Minimum) || (value > Maximum))
                    {
                        throw new ArgumentOutOfRangeException(nameof(value),
                            @"Provided value is out of the Minimum to Maximum range of values.");
                    }

                    _value = value;
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Sets a numeric value that represents the current position because of a user change.
        /// </summary>
        public int ScrollValue
        {
            set
            {
                if (value != _value)
                {
                    if ((value < Minimum) || (value > Maximum))
                    {
                        throw new ArgumentOutOfRangeException(nameof(Value), @"Provided value is out of the Minimum to Maximum range of values.");
                    }

                    _value = value;
                    OnScroll(EventArgs.Empty);
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value added to or subtracted from the Value property when the scroll box is moved a small distance.
        /// </summary>
        public int SmallChange
        {
            get => _smallChange;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(SmallChange), @"SmallChange cannot be less than zero.");
                }

                _smallChange = value;
            }
        }

        /// <summary>
        /// Gets or sets a value to be added to or subtracted from the Value property when the scroll box is moved a large distance.
        /// </summary>
        public int LargeChange
        {
            get => _largeChange;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(LargeChange), @"LargeChange cannot be less than zero.");
                }

                _largeChange = value;
            }
        }

        /// <summary>
        /// Sets the minimum and maximum values for a TrackBar.
        /// </summary>
        /// <param name="minValue">The lower limit of the range of the track bar.</param>
        /// <param name="maxValue">The upper limit of the range of the track bar.</param>
        public void SetRange(int minValue, int maxValue)
        {
            if ((Minimum != minValue) || (Maximum != maxValue))
            {
                if (minValue > maxValue)
                {
                    minValue = maxValue;
                }

                _minimum = minValue;
                _maximum = maxValue;

                var beforeValue = _value;
                if (_value < _minimum)
                {
                    _value = _minimum;
                }

                if (_value > _maximum)
                {
                    _value = _maximum;
                }

                if (beforeValue != _value)
                {
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Fix the control to a particular palette state.
        /// </summary>
        /// <param name="state">Palette state to fix.</param>
        public virtual void SetFixedState(PaletteState state)
        {
            if (state is PaletteState.Normal or PaletteState.Disabled)
            {
                _ticksTop.FixedState = state;
                _ticksBottom.FixedState = state;
            }

            TrackPosition.SetFixedState(state);
        }

        /// <summary>
        /// Gets and sets the enabled state of the element.
        /// </summary>
        public override bool Enabled
        {
            get => base.Enabled;

            set
            {
                base.Enabled = value;

                // Update with latest enabled state
                _layoutTop.Enabled = value;
                TrackPosition.Enabled = value;
                _ticksTop.Enabled = value;
                _ticksBottom.Enabled = value;
            }
        }

        /// <summary>
        /// Processes the MouseWheel event.
        /// </summary>
        /// <param name="e">Event arguments for the event.</param>
        public void OnMouseWheel(MouseEventArgs e)
        {
            var change = (e.Delta > 0) ? -SmallChange : SmallChange;
            var detents = Math.Abs(e.Delta) / SystemInformation.MouseWheelScrollDelta;
            for (var i = 0; i < detents; i++)
            {
                ScrollValue = Math.Max(Minimum, Math.Min(Value - change, Maximum));
            }
        }

        /// <summary>
        /// Gets the size of the position indicator.
        /// </summary>
        public Size PositionSize
        {
            get
            {
                return TrackBarSize switch
                {
                    PaletteTrackBarSize.Small => _orientation == Orientation.Horizontal ? _positionSizeSmallH : _positionSizeSmallV,
                    PaletteTrackBarSize.Large => _orientation == Orientation.Horizontal ? _positionSizeLargeH : _positionSizeLargeV,
                    _ => _orientation == Orientation.Horizontal ? _positionSizeMediumH : _positionSizeMediumV
                };
            }
        }

        /// <summary>
        /// Gets the size of the track.
        /// </summary>
        public Size TrackSize
        {
            get
            {
                return TrackBarSize switch
                {
                    PaletteTrackBarSize.Small => VolumeControl ? _trackSizeSmallV : _trackSizeSmall,
                    PaletteTrackBarSize.Large => VolumeControl ? _trackSizeLargeV : _trackSizeLarge,
                    _ => VolumeControl ? _trackSizeMediumV : _trackSizeMedium
                };
            }
        }

        /// <summary>
        /// Gets the size of the tick area.
        /// </summary>
        public Size TickSize
        {
            get
            {
                return TrackBarSize switch
                {
                    PaletteTrackBarSize.Small => _tickSizeSmall,
                    PaletteTrackBarSize.Medium => _tickSizeMedium,
                    PaletteTrackBarSize.Large => _tickSizeLarge,
                    _ => _tickSizeMedium
                };
            }
        }

        /// <summary>
        /// Gets access to the normal state.
        /// </summary>
        public PaletteTrackBarStatesOverride StateNormal { get; }

        /// <summary>
        /// Gets access to the disabled state.
        /// </summary>
        public PaletteTrackBarStates StateDisabled { get; }

        /// <summary>
        /// Gets access to the tracking state.
        /// </summary>
        public PaletteTrackBarPositionStatesOverride StateTracking { get; }

        /// <summary>
        /// Gets access to the pressed state.
        /// </summary>
        public PaletteTrackBarPositionStatesOverride StatePressed { get; }

        /// <summary>
        /// Raises a need paint event.
        /// </summary>
        /// <param name="needLayout">Does the layout need recalculating.</param>
        public void PerformNeedPaint(bool needLayout) => _needPaint?.Invoke(this, new NeedLayoutEventArgs(needLayout));

        #endregion

        #region Protected
        /// <summary>
        /// Raises the ValueChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnValueChanged(EventArgs e) => ValueChanged?.Invoke(this, e);

        /// <summary>
        /// Raises the Scroll event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnScroll(EventArgs e) => Scroll?.Invoke(this, e);

        #endregion
    }
}
