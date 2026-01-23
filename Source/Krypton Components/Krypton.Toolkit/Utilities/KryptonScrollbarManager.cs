#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

/// <summary>
/// Manages Krypton-themed scrollbars for scrollable controls.
/// Provides a unified solution for replacing native Windows scrollbars with Krypton scrollbars.
/// </summary>
/// <remarks>
/// This manager supports multiple integration modes:
/// - Container: For controls like Panel, GroupBox that use AutoScroll
/// - NativeWrapper: For controls like TextBox, RichTextBox with native scrollbars
/// - Custom: For controls with custom scrolling logic
/// </remarks>
public class KryptonScrollbarManager : IDisposable
{
    #region Instance Fields

    private Control? _targetControl;
    private KryptonHScrollBar? _horizontalScrollBar;
    private KryptonVScrollBar? _verticalScrollBar;
    private ScrollbarManagerMode _mode = ScrollbarManagerMode.Container;
    private bool _enabled = true;
    private bool _isUpdating;
    private bool _isDisposed;
    private Control? _contentContainer;
    private int _horizontalScrollValue;
    private int _verticalScrollValue;
    private bool _suppressScrollEvents;
    private readonly Timer _syncTimer;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when scrollbars are created, removed, or visibility changes.
    /// </summary>
    public event EventHandler? ScrollbarsChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonScrollbarManager"/> class.
    /// </summary>
    public KryptonScrollbarManager()
    {
        _syncTimer = new Timer { Interval = 50 }; // Update every 50ms for native wrapper mode
        _syncTimer.Tick += SyncTimer_Tick;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonScrollbarManager"/> class with a target control.
    /// </summary>
    /// <param name="targetControl">The control to attach scrollbars to.</param>
    /// <param name="mode">The integration mode to use.</param>
    public KryptonScrollbarManager(Control targetControl, ScrollbarManagerMode mode = ScrollbarManagerMode.Container)
    {
        _syncTimer = new Timer { Interval = 50 }; // Update every 50ms for native wrapper mode
        _syncTimer.Tick += SyncTimer_Tick;
        Attach(targetControl, mode);
    }

    /// <summary>
    /// Releases all resources used by the <see cref="KryptonScrollbarManager"/>.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="KryptonScrollbarManager"/> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            if (disposing)
            {
                _syncTimer?.Stop();
                _syncTimer?.Dispose();
                Detach();
            }

            _isDisposed = true;
        }
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets whether the scrollbar manager is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Gets or sets whether the scrollbar manager is enabled.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                UpdateScrollbars();
            }
        }
    }

    /// <summary>
    /// Gets the horizontal scrollbar, if created.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonHScrollBar? HorizontalScrollBar => _horizontalScrollBar;

    /// <summary>
    /// Gets the vertical scrollbar, if created.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonVScrollBar? VerticalScrollBar => _verticalScrollBar;

    /// <summary>
    /// Gets or sets the integration mode.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Gets or sets the integration mode (Container, NativeWrapper, or Custom).")]
    [DefaultValue(ScrollbarManagerMode.Container)]
    public ScrollbarManagerMode Mode
    {
        get => _mode;
        set
        {
            if (_mode != value)
            {
                _mode = value;
                if (_targetControl != null)
                {
                    Detach();
                    Attach(_targetControl, _mode);
                }
            }
        }
    }

    /// <summary>
    /// Gets the target control this manager is attached to.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control? TargetControl => _targetControl;

    #endregion

    #region Public Methods

    /// <summary>
    /// Attaches the manager to a control.
    /// </summary>
    /// <param name="targetControl">The control to attach to.</param>
    /// <param name="mode">The integration mode to use.</param>
    public void Attach(Control targetControl, ScrollbarManagerMode mode = ScrollbarManagerMode.Container)
    {
        if (targetControl == null)
        {
            throw new ArgumentNullException(nameof(targetControl));
        }

        if (_targetControl != null)
        {
            Detach();
        }

        _targetControl = targetControl;
        _mode = mode;

        // Hook into control events
        _targetControl.HandleCreated += OnTargetControlHandleCreated;
        _targetControl.HandleDestroyed += OnTargetControlHandleDestroyed;
        _targetControl.Resize += OnTargetControlResize;
        _targetControl.Layout += OnTargetControlLayout;

        // Initialize based on mode
        switch (_mode)
        {
            case ScrollbarManagerMode.Container:
                InitializeContainerMode();
                break;
            case ScrollbarManagerMode.NativeWrapper:
                InitializeNativeWrapperMode();
                _syncTimer.Start(); // Start periodic sync for native wrapper mode
                break;
            case ScrollbarManagerMode.Custom:
                // Custom mode - control manages its own scrolling
                break;
        }

        // Create scrollbars if handle is created
        if (_targetControl.IsHandleCreated)
        {
            UpdateScrollbars();
        }
    }

    /// <summary>
    /// Detaches the manager from the current control.
    /// </summary>
    public void Detach()
    {
        if (_targetControl != null)
        {
            // Unhook events
            _targetControl.HandleCreated -= OnTargetControlHandleCreated;
            _targetControl.HandleDestroyed -= OnTargetControlHandleDestroyed;
            _targetControl.Resize -= OnTargetControlResize;
            _targetControl.Layout -= OnTargetControlLayout;

            // Restore AutoScroll if it was disabled
            if (_mode == ScrollbarManagerMode.Container && _targetControl is Panel panel)
            {
                // AutoScroll will be restored when scrollbars are removed
            }

            _targetControl = null;
        }

        // Stop sync timer
        _syncTimer.Stop();

        // Remove and dispose scrollbars
        RemoveScrollbars();
        _contentContainer = null;
    }

    /// <summary>
    /// Updates the scrollbars based on current content and visibility requirements.
    /// </summary>
    public void UpdateScrollbars()
    {
        if (_targetControl == null || !_enabled || _isUpdating)
        {
            return;
        }

        if (!_targetControl.IsHandleCreated)
        {
            return;
        }

        _isUpdating = true;

        try
        {
            switch (_mode)
            {
                case ScrollbarManagerMode.Container:
                    UpdateContainerScrollbars();
                    break;
                case ScrollbarManagerMode.NativeWrapper:
                    UpdateNativeWrapperScrollbars();
                    break;
                case ScrollbarManagerMode.Custom:
                    // Custom mode - control manages its own scrolling
                    break;
            }
        }
        finally
        {
            _isUpdating = false;
        }
    }

    #endregion

    #region Implementation - Container Mode

    private void InitializeContainerMode()
    {
        if (_targetControl is Panel panel)
        {
            // Store original AutoScroll setting
            // We'll manage scrolling manually with Krypton scrollbars
            panel.AutoScroll = false;
            _contentContainer = panel;
        }
        else if (_targetControl is ContainerControl container)
        {
            _contentContainer = container;
        }
        else
        {
            _contentContainer = _targetControl;
        }

        // Store original locations of child controls
        if (_contentContainer != null)
        {
            foreach (Control child in _contentContainer.Controls)
            {
                if (child.Tag is not Point)
                {
                    child.Tag = child.Location;
                }
            }
        }
    }

    private void UpdateContainerScrollbars()
    {
        if (_contentContainer == null)
        {
            return;
        }

        // Calculate if scrollbars are needed
        bool needsHorizontal = false;
        bool needsVertical = false;
        int maxWidth = 0;
        int maxHeight = 0;

        // Find the maximum extent of child controls
        foreach (Control child in _contentContainer.Controls)
        {
            if (child.Visible)
            {
                int right = child.Right;
                int bottom = child.Bottom;

                if (right > maxWidth)
                {
                    maxWidth = right;
                }

                if (bottom > maxHeight)
                {
                    maxHeight = bottom;
                }
            }
        }

        // Check if scrollbars are needed
        int clientWidth = _contentContainer.ClientSize.Width;
        int clientHeight = _contentContainer.ClientSize.Height;
        int scrollbarWidth = SystemInformation.VerticalScrollBarWidth;
        int scrollbarHeight = SystemInformation.HorizontalScrollBarHeight;

        needsHorizontal = maxWidth > clientWidth;
        needsVertical = maxHeight > clientHeight;

        // If vertical scrollbar is needed, horizontal might need adjustment
        if (needsVertical && needsHorizontal)
        {
            needsHorizontal = maxWidth > (clientWidth - scrollbarWidth);
        }

        // Create or update horizontal scrollbar
        if (needsHorizontal)
        {
            if (_horizontalScrollBar == null)
            {
                CreateHorizontalScrollbar();
            }

            if (_horizontalScrollBar != null)
            {
                _horizontalScrollBar.Visible = true;
                _horizontalScrollBar.Minimum = 0;
                _horizontalScrollBar.Maximum = Math.Max(0, maxWidth - clientWidth + (needsVertical ? scrollbarWidth : 0));
                _horizontalScrollBar.LargeChange = clientWidth - (needsVertical ? scrollbarWidth : 0);
                _horizontalScrollBar.SmallChange = 10;
                _horizontalScrollBar.Value = Math.Min(_horizontalScrollBar.Value, _horizontalScrollBar.Maximum);
            }
        }
        else
        {
            if (_horizontalScrollBar != null)
            {
                _horizontalScrollBar.Visible = false;
            }
        }

        // Create or update vertical scrollbar
        if (needsVertical)
        {
            if (_verticalScrollBar == null)
            {
                CreateVerticalScrollbar();
            }

            if (_verticalScrollBar != null)
            {
                _verticalScrollBar.Visible = true;
                _verticalScrollBar.Minimum = 0;
                _verticalScrollBar.Maximum = Math.Max(0, maxHeight - clientHeight + (needsHorizontal ? scrollbarHeight : 0));
                _verticalScrollBar.LargeChange = clientHeight - (needsHorizontal ? scrollbarHeight : 0);
                _verticalScrollBar.SmallChange = 10;
                _verticalScrollBar.Value = Math.Min(_verticalScrollBar.Value, _verticalScrollBar.Maximum);
            }
        }
        else
        {
            if (_verticalScrollBar != null)
            {
                _verticalScrollBar.Visible = false;
            }
        }

        // Position scrollbars
        PositionScrollbars();

        // Update content position based on scroll values
        UpdateContentPosition();

        OnScrollbarsChanged();
    }

    private void UpdateContentPosition()
    {
        if (_contentContainer == null)
        {
            return;
        }

        int offsetX = _horizontalScrollBar?.Value ?? 0;
        int offsetY = _verticalScrollBar?.Value ?? 0;

        // For Panel controls, we can use a combination of approaches
        if (_contentContainer is Panel panel)
        {
            // Method 1: Use AutoScrollPosition (requires AutoScroll to be true, but we disabled it)
            // Method 2: Manually adjust child positions
            // We'll use method 2 for better control

            foreach (Control child in panel.Controls)
            {
                if (child != _horizontalScrollBar && child != _verticalScrollBar &&
                    child is not KryptonHScrollBar and not KryptonVScrollBar)
                {
                    // Store original location on first access
                    Point originalLocation;
                    if (child.Tag is Point storedLocation)
                    {
                        originalLocation = storedLocation;
                    }
                    else
                    {
                        originalLocation = child.Location;
                        child.Tag = originalLocation;
                    }

                    // Apply scroll offset
                    child.Location = new Point(originalLocation.X - offsetX, originalLocation.Y - offsetY);
                }
            }
        }
        else
        {
            // For other container controls, manually adjust child positions
            foreach (Control child in _contentContainer.Controls)
            {
                if (child != _horizontalScrollBar && child != _verticalScrollBar &&
                    child is not KryptonHScrollBar and not KryptonVScrollBar)
                {
                    // Store original location on first access
                    Point originalLocation;
                    if (child.Tag is Point storedLocation)
                    {
                        originalLocation = storedLocation;
                    }
                    else
                    {
                        originalLocation = child.Location;
                        child.Tag = originalLocation;
                    }

                    // Apply scroll offset
                    child.Location = new Point(originalLocation.X - offsetX, originalLocation.Y - offsetY);
                }
            }
        }
    }

    #endregion

    #region Implementation - Native Wrapper Mode

    private void InitializeNativeWrapperMode()
    {
        if (_targetControl == null)
        {
            return;
        }

        // Wait for handle to be created before initializing
        if (_targetControl.IsHandleCreated)
        {
            HideNativeScrollbars();
        }
    }

    private void UpdateNativeWrapperScrollbars()
    {
        if (_targetControl == null || !_targetControl.IsHandleCreated)
        {
            return;
        }

        // Hide native scrollbars
        HideNativeScrollbars();

        // Get scroll information from native control
        var hScrollInfo = new WIN32ScrollBars.ScrollInfo
        {
            cbSize = Marshal.SizeOf(typeof(WIN32ScrollBars.ScrollInfo)),
            fMask = (int)PI.SIF_.ALL
        };

        var vScrollInfo = new WIN32ScrollBars.ScrollInfo
        {
            cbSize = Marshal.SizeOf(typeof(WIN32ScrollBars.ScrollInfo)),
            fMask = (int)PI.SIF_.ALL
        };

        bool hasHScroll = PI.GetScrollInfo(_targetControl.Handle, PI.SB_.HORZ, ref hScrollInfo);
        bool hasVScroll = PI.GetScrollInfo(_targetControl.Handle, PI.SB_.VERT, ref vScrollInfo);

        // Update or create horizontal scrollbar
        if (hasHScroll && hScrollInfo.nMax >= hScrollInfo.nPage)
        {
            if (_horizontalScrollBar == null)
            {
                CreateHorizontalScrollbar();
            }

            if (_horizontalScrollBar != null)
            {
                _suppressScrollEvents = true;
                _horizontalScrollBar.Visible = true;
                _horizontalScrollBar.Minimum = hScrollInfo.nMin;
                _horizontalScrollBar.Maximum = hScrollInfo.nMax;
                _horizontalScrollBar.LargeChange = hScrollInfo.nPage;
                _horizontalScrollBar.SmallChange = 1;
                _horizontalScrollBar.Value = Math.Min(hScrollInfo.nPos, _horizontalScrollBar.Maximum);
                _suppressScrollEvents = false;
            }
        }
        else
        {
            if (_horizontalScrollBar != null)
            {
                _horizontalScrollBar.Visible = false;
            }
        }

        // Update or create vertical scrollbar
        if (hasVScroll && vScrollInfo.nMax >= vScrollInfo.nPage)
        {
            if (_verticalScrollBar == null)
            {
                CreateVerticalScrollbar();
            }

            if (_verticalScrollBar != null)
            {
                _suppressScrollEvents = true;
                _verticalScrollBar.Visible = true;
                _verticalScrollBar.Minimum = vScrollInfo.nMin;
                _verticalScrollBar.Maximum = vScrollInfo.nMax;
                _verticalScrollBar.LargeChange = vScrollInfo.nPage;
                _verticalScrollBar.SmallChange = 1;
                _verticalScrollBar.Value = Math.Min(vScrollInfo.nPos, _verticalScrollBar.Maximum);
                _suppressScrollEvents = false;
            }
        }
        else
        {
            if (_verticalScrollBar != null)
            {
                _verticalScrollBar.Visible = false;
            }
        }

        // Position scrollbars
        PositionScrollbars();

        OnScrollbarsChanged();
    }

    private void HideNativeScrollbars()
    {
        if (_targetControl == null || !_targetControl.IsHandleCreated)
        {
            return;
        }

        try
        {
            // Get current window style
            uint style = PI.GetWindowLong(_targetControl.Handle, PI.GWL_.STYLE);

            // Remove scrollbar styles
            style &= ~(uint)PI.WS_.HSCROLL;
            style &= ~(uint)PI.WS_.VSCROLL;

            // Set new style
            PI.SetWindowLong(_targetControl.Handle, PI.GWL_.STYLE, style);

            // Force redraw
            _targetControl.Invalidate();
        }
        catch
        {
            // If we can't hide scrollbars, continue anyway
            // The Krypton scrollbars will still work
        }
    }

    private void SyncNativeScrollPosition(bool horizontal, int value)
    {
        if (_targetControl == null || !_targetControl.IsHandleCreated || _suppressScrollEvents)
        {
            return;
        }

        try
        {
            PI.SB_ scrollBar = horizontal ? PI.SB_.HORZ : PI.SB_.VERT;
            PI.SetScrollPos(_targetControl.Handle, scrollBar, value, true);

            // Send scroll message to update the control
            PI.SendMessage(_targetControl.Handle, horizontal ? PI.WM_.HSCROLL : PI.WM_.VSCROLL,
                (IntPtr)((int)PI.SB_.THUMBPOSITION | (value << 16)), IntPtr.Zero);
        }
        catch
        {
            // Ignore errors
        }
    }

    #endregion

    #region Implementation - Scrollbar Creation

    private void CreateHorizontalScrollbar()
    {
        if (_targetControl == null || _horizontalScrollBar != null)
        {
            return;
        }

        _horizontalScrollBar = new KryptonHScrollBar
        {
            Visible = false,
            TabStop = false,
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
        };

        _horizontalScrollBar.Scroll += OnHorizontalScroll;

        if (_targetControl.IsHandleCreated)
        {
            _targetControl.Controls.Add(_horizontalScrollBar);
            _horizontalScrollBar.BringToFront();
        }
    }

    private void CreateVerticalScrollbar()
    {
        if (_targetControl == null || _verticalScrollBar != null)
        {
            return;
        }

        _verticalScrollBar = new KryptonVScrollBar
        {
            Visible = false,
            TabStop = false,
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right
        };

        _verticalScrollBar.Scroll += OnVerticalScroll;

        if (_targetControl.IsHandleCreated)
        {
            _targetControl.Controls.Add(_verticalScrollBar);
            _verticalScrollBar.BringToFront();
        }
    }

    private void RemoveScrollbars()
    {
        if (_horizontalScrollBar != null)
        {
            _horizontalScrollBar.Scroll -= OnHorizontalScroll;
            if (_horizontalScrollBar.Parent != null)
            {
                _horizontalScrollBar.Parent.Controls.Remove(_horizontalScrollBar);
            }

            _horizontalScrollBar.Dispose();
            _horizontalScrollBar = null;
        }

        if (_verticalScrollBar != null)
        {
            _verticalScrollBar.Scroll -= OnVerticalScroll;
            if (_verticalScrollBar.Parent != null)
            {
                _verticalScrollBar.Parent.Controls.Remove(_verticalScrollBar);
            }

            _verticalScrollBar.Dispose();
            _verticalScrollBar = null;
        }

        OnScrollbarsChanged();
    }

    private void PositionScrollbars()
    {
        if (_targetControl == null)
        {
            return;
        }

        int scrollbarWidth = SystemInformation.VerticalScrollBarWidth;
        int scrollbarHeight = SystemInformation.HorizontalScrollBarHeight;

        // Position horizontal scrollbar
        if (_horizontalScrollBar != null && _horizontalScrollBar.Visible)
        {
            _horizontalScrollBar.Location = new Point(0, _targetControl.ClientSize.Height - scrollbarHeight);
            _horizontalScrollBar.Width = _targetControl.ClientSize.Width - (_verticalScrollBar?.Visible == true ? scrollbarWidth : 0);
            _horizontalScrollBar.Height = scrollbarHeight;
        }

        // Position vertical scrollbar
        if (_verticalScrollBar != null && _verticalScrollBar.Visible)
        {
            _verticalScrollBar.Location = new Point(_targetControl.ClientSize.Width - scrollbarWidth, 0);
            _verticalScrollBar.Width = scrollbarWidth;
            _verticalScrollBar.Height = _targetControl.ClientSize.Height - (_horizontalScrollBar?.Visible == true ? scrollbarHeight : 0);
        }
    }

    #endregion

    #region Event Handlers

    private void OnTargetControlHandleCreated(object? sender, EventArgs e)
    {
        UpdateScrollbars();
    }

    private void OnTargetControlHandleDestroyed(object? sender, EventArgs e)
    {
        // Scrollbars will be cleaned up in Detach
    }

    private void OnTargetControlResize(object? sender, EventArgs e)
    {
        UpdateScrollbars();
    }

    private void OnTargetControlLayout(object? sender, LayoutEventArgs e)
    {
        if (!_isUpdating)
        {
            UpdateScrollbars();
        }
    }

    private void OnHorizontalScroll(object? sender, ScrollEventArgs e)
    {
        if (_suppressScrollEvents)
        {
            return;
        }

        _horizontalScrollValue = e.NewValue;

        if (_mode == ScrollbarManagerMode.Container)
        {
            UpdateContentPosition();
        }
        else if (_mode == ScrollbarManagerMode.NativeWrapper)
        {
            SyncNativeScrollPosition(true, e.NewValue);
        }
    }

    private void OnVerticalScroll(object? sender, ScrollEventArgs e)
    {
        if (_suppressScrollEvents)
        {
            return;
        }

        _verticalScrollValue = e.NewValue;

        if (_mode == ScrollbarManagerMode.Container)
        {
            UpdateContentPosition();
        }
        else if (_mode == ScrollbarManagerMode.NativeWrapper)
        {
            SyncNativeScrollPosition(false, e.NewValue);
        }
    }

    private void OnScrollbarsChanged()
    {
        ScrollbarsChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SyncTimer_Tick(object? sender, EventArgs e)
    {
        if (_mode == ScrollbarManagerMode.NativeWrapper && _enabled && !_isUpdating)
        {
            UpdateNativeWrapperScrollbars();
        }
    }

    #endregion
}
