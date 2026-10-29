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

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

/// <summary>
/// Manages when tooltips are Displayed in response to provided messages.
/// </summary>
public class ToolTipManager
{
    #region Instance Fields
    private readonly Timer _startTimer;
    private readonly Timer _detectMoveTimer;
    private readonly Timer _closeTimer;
    private readonly ToolTipValues _toolTipValues;
    private int _closeInterval;
    private ViewBase? _startTarget;
    private ViewBase? _currentTarget;
    private bool _showingToolTips;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a tooltip is required to be shown.
    /// </summary>
    public event EventHandler<ToolTipEventArgs>? ShowToolTip;

    /// <summary>
    /// Occurs when the showing tooltip is no longer required.
    /// </summary>
    public event EventHandler? CancelToolTip;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the TooltipManager class.
    /// </summary>
    public ToolTipManager(ToolTipValues toolTipValues)
    {
        _toolTipValues = toolTipValues;
        _startTimer = new Timer
        {
            Interval = toolTipValues.ShowIntervalDelay
        };
        _startTimer.Tick += OnStartTimerTick;

        // 0 = infinite display, but cannot have an interval less than 0
        _closeInterval = toolTipValues.CloseIntervalDelay < 0 ? 0 : toolTipValues.CloseIntervalDelay;

        _closeTimer = new Timer
        {
            // 0 = infinite display, but cannot have an interval less than 0
            Interval = _closeInterval > 0 ? _closeInterval : 1
        };
        _closeTimer.Tick += OnCloseTimerTick;

        _detectMoveTimer = new Timer
        {
            Interval = 100 // ReShowDelay
        };
     
        _detectMoveTimer.Tick += OnStopDetectMoveTimerTick;

        toolTipValues.ShowIntervalDelayChanged += OnShowIntervalDelayChanged;

        toolTipValues.CloseIntervalDelayChanged += OnCloseIntervalDelayChanged;
    }

    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the interval before a tooltip is shown.
    /// </summary>
    public int ShowInterval
    {
        get => _startTimer.Interval;

        set
        {
            // Cannot have an interval less than 1ms
            if (value < 0)
            {
                value = 1;
            }

            _startTimer.Interval = value;
        }
    }

    /// <summary>
    /// Gets and sets the interval before a tooltip is closed.
    /// Use 0 for infinite display (tooltip stays until the pointer leaves the control).
    /// </summary>
    public int CloseInterval
    {
        get => _closeInterval;

        set
        {
            // 0 = infinite display, but cannot have an interval less than 0
            if (value < 0)
            {
                value = 0;
            }

            _closeInterval = value;

            // Update the timer interval to match the new value, using 1ms if infinite display is specified
            _closeTimer.Interval = value > 0 ? value : 1;
        }
    }
    #endregion

    #region IMouseController Snooped Messages
    /// <summary>
    /// Mouse has entered the view.
    /// </summary>
    /// <param name="targetElement">Target element for the mouse message.</param>
    /// <param name="c">Reference to the source control instance.</param>
    public void MouseEnter(ViewBase targetElement, Control c)
    {
        // Remember the current target
        _currentTarget = targetElement;

        // If not currently showing a tooltip
        if (!_showingToolTips)
        {
            try
            {
                // If there is no start timer running
                if (_startTarget == null)
                {
                    // Start the timer and associate it with the target
                    _startTarget = targetElement;
                    _startTimer.Start();
                }
                else
                {
                    // If the running start timer is not for the new target
                    if (_startTarget != targetElement)
                    {
                        // Stop the currently running start timer
                        _startTimer.Stop();
                        _closeTimer.Stop();

                        // Restart the timer and associate it with the target
                        _startTarget = targetElement;
                        _startTimer.Start();
                    }
                }
            }
            catch
            {
                // ignored
            }
        }
    }

    /// <summary>
    /// Mouse has moved inside the view.
    /// </summary>
    /// <param name="targetElement">Target element for the mouse message.</param>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    public void MouseMove(ViewBase targetElement, Control c, Point pt)
    {
    }

    /// <summary>
    /// Mouse button has been pressed in the view.
    /// </summary>
    /// <param name="targetElement">Target element for the mouse message.</param>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    /// <param name="button">Mouse button pressed down.</param>
    public void MouseDown(ViewBase targetElement,
        Control c,
        Point pt,
        MouseButtons button)
    {
        // Stop hover-delay / linger timers. Keep the showing target when an interactive host stays up.
        _startTimer.Stop();
        _detectMoveTimer.Stop();

        bool keepInteractive = _showingToolTips
                               && _toolTipValues.HostedContent is not null
                               && !_toolTipValues.DismissInteractiveOnTargetMouseDown;

        if (!keepInteractive)
        {
            _closeTimer.Stop();
            _currentTarget = null;
            _startTarget = null;
        }

        if (_showingToolTips && !keepInteractive)
        {
            _showingToolTips = false;
            OnCancelToolTip();
        }
    }

    /// <summary>
    /// Mouse button has been released in the view.
    /// </summary>
    /// <param name="targetElement">Target element for the mouse message.</param>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    /// <param name="button">Mouse button released.</param>
    public void MouseUp(ViewBase targetElement,
        Control c,
        Point pt,
        MouseButtons button)
    {
    }

    /// <summary>
    /// Mouse has left the view.
    /// </summary>
    /// <param name="targetElement">Target element for the mouse message.</param>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="next">Reference to view that is next to have the mouse.</param>
    public void MouseLeave(ViewBase? targetElement, Control c, ViewBase? next)
    {
        // `next` is the view that will receive the mouse (sibling), or null when leaving the control.
        _currentTarget = next;

        if (_toolTipValues.HostedContent is not null && _showingToolTips)
        {
            return;
        }

        if (_showingToolTips)
        {
            if (next != null && next == _startTarget)
            {
                return;
            }

            try
            {
                // Brief linger so a move onto a sibling view can reshow without the hover delay.
                _detectMoveTimer.Stop();
                _detectMoveTimer.Start();
                _closeTimer.Stop();
            }
            catch
            {
                // ignored
            }

            return;
        }

        if (next == null)
        {
            _startTimer.Stop();
            _startTarget = null;
        }
        else if (next != _startTarget)
        {
            _startTimer.Stop();
            _startTarget = next;
            _startTimer.Start();
        }
    }

    /// <summary>
    /// Left mouse button double click.
    /// </summary>
    /// <param name="targetElement">Target element for the mouse message.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    public void DoubleClick(ViewBase targetElement, Point pt)
    {
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the ShowTooltip event.
    /// </summary>
    /// <param name="e">A TooltipEventArgs that contains the event data.</param>
    protected virtual void OnShowToolTip(ToolTipEventArgs e) => ShowToolTip?.Invoke(this, e);

    /// <summary>
    /// Raises the CancelTooltip event.
    /// </summary>
    protected virtual void OnCancelToolTip() => CancelToolTip?.Invoke(this, EventArgs.Empty);

    #endregion

    #region Implementation
    private void OnStartTimerTick(object? sender, EventArgs e)
    {
        // One tick timer, so always stop
        _startTimer.Stop();

        // Is the target the same as when the timer was kicked off?
        if (_currentTarget != null && _currentTarget == _startTarget)
        {
            // Enter showing tooltips mode
            _showingToolTips = true;

            // Raise event requesting the tooltip be shown
            OnShowToolTip(new ToolTipEventArgs(_startTarget, Control.MousePosition));

            RestartCloseTimerIfNeeded();
        }
        else
        {
            // Timer no longer valid, so reset the associated target
            _startTarget = null;
        }
    }

    private void OnStopDetectMoveTimerTick(object? sender, EventArgs e)
    {
        _detectMoveTimer.Stop();

        if (!_showingToolTips)
        {
            return;
        }

        // Still over the view that owns the open tooltip.
        if (_currentTarget != null && _currentTarget == _startTarget)
        {
            RestartCloseTimerIfNeeded();
            return;
        }

        _showingToolTips = false;
        _closeTimer.Stop();
        OnCancelToolTip();

        if (_currentTarget != null)
        {
            // Moved onto another view during the linger: show immediately (no hover delay).
            _startTarget = _currentTarget;
            _showingToolTips = true;
            OnShowToolTip(new ToolTipEventArgs(_startTarget, Control.MousePosition));
            RestartCloseTimerIfNeeded();
        }
        else
        {
            _startTarget = null;
        }
    }

    private void RestartCloseTimerIfNeeded()
    {
        bool interactive = _toolTipValues.HostedContent is not null;
        if (_closeInterval > 0 && (!interactive || _toolTipValues.UseCloseTimerForInteractive))
        {
            _closeTimer.Interval = _closeInterval;
            _closeTimer.Start();
        }
    }

    private void OnCloseTimerTick(object? sender, EventArgs e)
    {
        _closeTimer.Stop();
        _showingToolTips = false;
        _startTarget = null;
        OnCancelToolTip();
    }

    private void OnShowIntervalDelayChanged(object? sender, EventArgs e)
    {
        if (sender is ToolTipValues values)
        {
            ShowInterval = values.ShowIntervalDelay;
        }
    }

    private void OnCloseIntervalDelayChanged(object? sender, EventArgs e)
    {
        if (sender is ToolTipValues values)
        {
            CloseInterval = values.CloseIntervalDelay;
        }
    }

    #endregion
}
