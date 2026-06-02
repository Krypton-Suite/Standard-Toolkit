#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
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
    private RichTextBoxScrollBars? _originalRichTextBoxScrollBars;
    private ScrollBars? _originalTextBoxScrollBars;
    private bool _nativeThumbTracking;
    private IntPtr _hiddenNativeScrollbarsHandle;
    private Control? _scrollbarHostControl;
    private int _lastListBoxItemCount = -1;
    private int _lastListBoxVisibleItems = -1;
    private int _lastListBoxMaximumTopIndex = -1;
    private int _lastListBoxHorizontalContentWidth = -1;
    private int _lastListBoxHorizontalPageWidth = -1;
    private int _lastListBoxMaximumLeftOffset = -1;
    private int _trackedListBoxItemCount = -1;
    private int _trackedListBoxVisibleItems = -1;
    private int _trackedListBoxMaximumTopIndex = -1;
    private int _trackedListBoxHorizontalContentWidth = -1;
    private int _trackedListBoxHorizontalPageWidth = -1;
    private int _trackedListBoxMaximumLeftOffset = -1;

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
        _targetControl.ParentChanged += OnTargetControlParentChanged;
        UpdateScrollbarHostHook();

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
            _targetControl.ParentChanged -= OnTargetControlParentChanged;
            UnhookScrollbarHostControl();

            // Restore original ScrollBars values for RichTextBox/TextBox
            if (_targetControl is RichTextBox richTextBox && _originalRichTextBoxScrollBars.HasValue)
            {
                richTextBox.ScrollBars = _originalRichTextBoxScrollBars.Value;
                _originalRichTextBoxScrollBars = null;
            }
            else if (_targetControl is TextBox textBox && _originalTextBoxScrollBars.HasValue)
            {
                textBox.ScrollBars = _originalTextBoxScrollBars.Value;
                _originalTextBoxScrollBars = null;
            }

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
        InvalidateNativeScrollbarState();
    }

    /// <summary>
    /// Updates the scrollbars based on current content and visibility requirements.
    /// </summary>
    public void UpdateScrollbars()
    {
        if (_targetControl == null || !_enabled || _isUpdating || (_mode == ScrollbarManagerMode.NativeWrapper && _nativeThumbTracking))
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

        UpdateScrollbarHostHook();
        if (NativeScrollbarsAppearVisible())
        {
            InvalidateNativeScrollbarHiddenState();
        }

        EnsureNativeScrollbarsHidden();

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
        int hScrollableMaximum = GetNativeScrollableMaximum(hScrollInfo);
        int vScrollableMaximum = GetNativeScrollableMaximum(vScrollInfo);

        if (_targetControl is ListBox listBox)
        {
            UpdateListBoxHorizontalScrollbar(listBox, hScrollInfo, hasHScroll);
        }
        else if (hasHScroll && hScrollableMaximum > hScrollInfo.nMin)
        {
            if (_horizontalScrollBar == null)
            {
                CreateHorizontalScrollbar();
            }

            if (_horizontalScrollBar != null)
            {
                _suppressScrollEvents = true;
                try
                {
                    _horizontalScrollBar.Visible = true;
                    _horizontalScrollBar.Minimum = hScrollInfo.nMin;
                    _horizontalScrollBar.Maximum = hScrollInfo.nMax;
                    _horizontalScrollBar.LargeChange = hScrollInfo.nPage;
                    _horizontalScrollBar.SmallChange = 1;
                    _horizontalScrollBar.Value = Math.Min(hScrollInfo.nPos, hScrollableMaximum);
                }
                finally
                {
                    _suppressScrollEvents = false;
                }
            }
        }
        else
        {
            if (_horizontalScrollBar != null)
            {
                _horizontalScrollBar.Visible = false;
            }
        }

        if (_targetControl is ListBox listBoxForVertical)
        {
            UpdateListBoxVerticalScrollbar(listBoxForVertical);
        }
        else if (hasVScroll && vScrollableMaximum > vScrollInfo.nMin)
        {
            if (_verticalScrollBar == null)
            {
                CreateVerticalScrollbar();
            }

            if (_verticalScrollBar != null)
            {
                _suppressScrollEvents = true;
                try
                {
                    _verticalScrollBar.Visible = true;
                    _verticalScrollBar.Minimum = vScrollInfo.nMin;
                    _verticalScrollBar.Maximum = vScrollInfo.nMax;
                    _verticalScrollBar.LargeChange = vScrollInfo.nPage;
                    _verticalScrollBar.SmallChange = 1;
                    _verticalScrollBar.Value = Math.Min(vScrollInfo.nPos, vScrollableMaximum);
                }
                finally
                {
                    _suppressScrollEvents = false;
                }
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

    private void UpdateListBoxHorizontalScrollbar(ListBox listBox, WIN32ScrollBars.ScrollInfo hScrollInfo, bool hasHScroll)
    {
        int contentWidth = GetListBoxHorizontalContentWidth(listBox, hScrollInfo, hasHScroll);
        int pageWidth = GetListBoxHorizontalPageWidth(listBox);
        int maximumLeftOffset = Math.Max(0, contentWidth - pageWidth);

        if (contentWidth != _lastListBoxHorizontalContentWidth ||
            pageWidth != _lastListBoxHorizontalPageWidth ||
            maximumLeftOffset != _lastListBoxMaximumLeftOffset)
        {
            _lastListBoxHorizontalContentWidth = contentWidth;
            _lastListBoxHorizontalPageWidth = pageWidth;
            _lastListBoxMaximumLeftOffset = maximumLeftOffset;
            InvalidateNativeScrollbarHiddenState();
            EnsureNativeScrollbarsHidden();
        }

        if (maximumLeftOffset > 0)
        {
            if (_horizontalScrollBar == null)
            {
                CreateHorizontalScrollbar();
            }

            if (_horizontalScrollBar != null)
            {
                _suppressScrollEvents = true;
                try
                {
                    _horizontalScrollBar.Visible = true;
                    _horizontalScrollBar.Minimum = 0;
                    _horizontalScrollBar.Maximum = Math.Max(0, contentWidth - 1);
                    _horizontalScrollBar.LargeChange = pageWidth;
                    _horizontalScrollBar.SmallChange = Math.Max(1, Math.Min(16, pageWidth / 10));
                    _horizontalScrollBar.Value = Math.Min(Math.Max(0, hScrollInfo.nPos), maximumLeftOffset);
                }
                finally
                {
                    _suppressScrollEvents = false;
                }
            }
        }
        else if (_horizontalScrollBar != null)
        {
            _horizontalScrollBar.Visible = false;
        }
    }

    private void UpdateListBoxVerticalScrollbar(ListBox listBox)
    {
        int visibleItems = GetListBoxVisibleItemCount(listBox);
        int itemCount = listBox.Items.Count;
        int maximumTopIndex = Math.Max(0, itemCount - visibleItems);
        if (itemCount != _lastListBoxItemCount ||
            visibleItems != _lastListBoxVisibleItems ||
            maximumTopIndex != _lastListBoxMaximumTopIndex)
        {
            _lastListBoxItemCount = itemCount;
            _lastListBoxVisibleItems = visibleItems;
            _lastListBoxMaximumTopIndex = maximumTopIndex;
            InvalidateNativeScrollbarHiddenState();
            EnsureNativeScrollbarsHidden();
        }

        if (itemCount > visibleItems)
        {
            if (_verticalScrollBar == null)
            {
                CreateVerticalScrollbar();
            }

            if (_verticalScrollBar != null)
            {
                _suppressScrollEvents = true;
                try
                {
                    _verticalScrollBar.Visible = true;
                    _verticalScrollBar.Minimum = 0;
                    _verticalScrollBar.Maximum = Math.Max(0, itemCount - 1);
                    _verticalScrollBar.LargeChange = visibleItems;
                    _verticalScrollBar.SmallChange = 1;
                    _verticalScrollBar.Value = Math.Min(listBox.TopIndex, maximumTopIndex);
                }
                finally
                {
                    _suppressScrollEvents = false;
                }
            }
        }
        else if (_verticalScrollBar != null)
        {
            _verticalScrollBar.Visible = false;
        }
    }

    private static int GetListBoxVisibleItemCount(ListBox listBox)
    {
        if (listBox.Items.Count == 0)
        {
            return 1;
        }

        try
        {
            int visibleItems = 0;
            int topIndex = Math.Max(0, Math.Min(listBox.TopIndex, listBox.Items.Count - 1));
            int clientBottom = listBox.ClientSize.Height;

            for (int i = topIndex; i < listBox.Items.Count; i++)
            {
                Rectangle itemRect = listBox.GetItemRectangle(i);
                if (itemRect.Top >= clientBottom)
                {
                    break;
                }

                if (itemRect.Height <= 0 || itemRect.Bottom > clientBottom)
                {
                    break;
                }

                visibleItems++;
            }

            if (visibleItems > 0)
            {
                return visibleItems;
            }
        }
        catch
        {
            // Fall back to ItemHeight when item rectangles are unavailable.
        }

        int itemHeight = Math.Max(1, listBox.ItemHeight);
        try
        {
            itemHeight = Math.Max(itemHeight, listBox.GetItemRectangle(0).Height);
        }
        catch
        {
            // Use ItemHeight when the native item rectangle is not available.
        }

        return Math.Max(1, listBox.ClientSize.Height / itemHeight);
    }

    private static int GetListBoxHorizontalPageWidth(ListBox listBox) => Math.Max(1, listBox.ClientSize.Width);

    private static int GetListBoxHorizontalContentWidth(ListBox listBox, WIN32ScrollBars.ScrollInfo hScrollInfo, bool hasHScroll)
    {
        int contentWidth = Math.Max(0, listBox.HorizontalExtent);
        if (hasHScroll && hScrollInfo.nMax > hScrollInfo.nMin)
        {
            contentWidth = Math.Max(contentWidth, hScrollInfo.nMax - hScrollInfo.nMin + 1);
        }

        return contentWidth;
    }

    private static int GetNativeScrollableMaximum(WIN32ScrollBars.ScrollInfo scrollInfo)
    {
        int page = Math.Max(1, scrollInfo.nPage);
        long scrollableMaximum = (long)scrollInfo.nMax - page + 1;

        if (scrollableMaximum < scrollInfo.nMin)
        {
            return scrollInfo.nMin;
        }

        return scrollableMaximum > int.MaxValue ? int.MaxValue : (int)scrollableMaximum;
    }

    private void EnsureNativeScrollbarsHidden()
    {
        if (_targetControl == null || !_targetControl.IsHandleCreated || _hiddenNativeScrollbarsHandle == _targetControl.Handle)
        {
            return;
        }

        HideNativeScrollbars();
        _hiddenNativeScrollbarsHandle = _targetControl.Handle;
    }

    private void InvalidateNativeScrollbarState()
    {
        _hiddenNativeScrollbarsHandle = IntPtr.Zero;
        _lastListBoxItemCount = -1;
        _lastListBoxVisibleItems = -1;
        _lastListBoxMaximumTopIndex = -1;
        _lastListBoxHorizontalContentWidth = -1;
        _lastListBoxHorizontalPageWidth = -1;
        _lastListBoxMaximumLeftOffset = -1;
        ClearTrackedListBoxMetrics();
    }

    private void InvalidateNativeScrollbarHiddenState()
    {
        _hiddenNativeScrollbarsHandle = IntPtr.Zero;
    }

    private bool NativeScrollbarsAppearVisible()
    {
        if (_targetControl is not ListBox listBox)
        {
            return false;
        }

        int widthDifference = listBox.Width - listBox.ClientSize.Width;
        int heightDifference = listBox.Height - listBox.ClientSize.Height;

        return widthDifference >= SystemInformation.VerticalScrollBarWidth / 2 ||
               heightDifference >= SystemInformation.HorizontalScrollBarHeight / 2;
    }

    private void BeginNativeThumbTracking()
    {
        if (_nativeThumbTracking)
        {
            return;
        }

        _nativeThumbTracking = true;

        if (_targetControl is ListBox listBox)
        {
            int itemCount = listBox.Items.Count;
            int visibleItems = _lastListBoxVisibleItems > 0
                ? _lastListBoxVisibleItems
                : GetListBoxVisibleItemCount(listBox);

            _trackedListBoxItemCount = itemCount;
            _trackedListBoxVisibleItems = visibleItems;
            _trackedListBoxMaximumTopIndex = Math.Max(0, itemCount - visibleItems);
            _trackedListBoxHorizontalContentWidth = _lastListBoxHorizontalContentWidth;
            _trackedListBoxHorizontalPageWidth = _lastListBoxHorizontalPageWidth;
            _trackedListBoxMaximumLeftOffset = _lastListBoxMaximumLeftOffset;
        }
    }

    private void EndNativeThumbTracking()
    {
        _nativeThumbTracking = false;
        ClearTrackedListBoxMetrics();
    }

    private void ClearTrackedListBoxMetrics()
    {
        _trackedListBoxItemCount = -1;
        _trackedListBoxVisibleItems = -1;
        _trackedListBoxMaximumTopIndex = -1;
        _trackedListBoxHorizontalContentWidth = -1;
        _trackedListBoxHorizontalPageWidth = -1;
        _trackedListBoxMaximumLeftOffset = -1;
    }

    private int GetListBoxVisibleItemsForSync(ListBox listBox)
    {
        if (_nativeThumbTracking &&
            _trackedListBoxItemCount == listBox.Items.Count &&
            _trackedListBoxVisibleItems > 0)
        {
            return _trackedListBoxVisibleItems;
        }

        return GetListBoxVisibleItemCount(listBox);
    }

    private int GetListBoxMaximumTopIndexForSync(ListBox listBox, int visibleItems)
    {
        if (_nativeThumbTracking &&
            _trackedListBoxItemCount == listBox.Items.Count &&
            _trackedListBoxVisibleItems == visibleItems &&
            _trackedListBoxMaximumTopIndex >= 0)
        {
            return _trackedListBoxMaximumTopIndex;
        }

        return Math.Max(0, listBox.Items.Count - visibleItems);
    }

    private int GetListBoxHorizontalPageWidthForSync(ListBox listBox)
    {
        if (_nativeThumbTracking &&
            _trackedListBoxHorizontalPageWidth > 0)
        {
            return _trackedListBoxHorizontalPageWidth;
        }

        return GetListBoxHorizontalPageWidth(listBox);
    }

    private int GetListBoxMaximumLeftOffsetForSync(ListBox listBox, int pageWidth)
    {
        if (_nativeThumbTracking &&
            _trackedListBoxHorizontalPageWidth == pageWidth &&
            _trackedListBoxMaximumLeftOffset >= 0)
        {
            return _trackedListBoxMaximumLeftOffset;
        }

        var hScrollInfo = new WIN32ScrollBars.ScrollInfo
        {
            cbSize = Marshal.SizeOf(typeof(WIN32ScrollBars.ScrollInfo)),
            fMask = (int)PI.SIF_.ALL
        };
        bool hasHScroll = PI.GetScrollInfo(listBox.Handle, PI.SB_.HORZ, ref hScrollInfo);
        int contentWidth = GetListBoxHorizontalContentWidth(listBox, hScrollInfo, hasHScroll);

        return Math.Max(0, contentWidth - pageWidth);
    }

    private void HideNativeScrollbars()
    {
        if (_targetControl == null || !_targetControl.IsHandleCreated)
        {
            return;
        }

        try
        {
            // RichTextBox uses RichTextBoxScrollBars property
            if (_targetControl is RichTextBox richTextBox)
            {
                // Store original value if not already stored
                if (_originalRichTextBoxScrollBars == null)
                {
                    _originalRichTextBoxScrollBars = richTextBox.ScrollBars;
                }
                richTextBox.ScrollBars = RichTextBoxScrollBars.None;
                return;
            }

            // TextBox uses ScrollBars property
            if (_targetControl is TextBox textBox)
            {
                // Store original value if not already stored
                if (_originalTextBoxScrollBars == null)
                {
                    _originalTextBoxScrollBars = textBox.ScrollBars;
                }
                textBox.ScrollBars = ScrollBars.None;
                return;
            }

            // For other controls (ListBox, ListView, TreeView, PropertyGrid, etc.), hide native
            // scrollbars via ShowScrollBar so only Krypton scrollbars are visible.
            _ = PI.ShowScrollBar(_targetControl.Handle, (int)PI.SB_.BOTH, false);

            if (_targetControl is ListBox)
            {
                _targetControl.Invalidate();
                return;
            }

            // Also remove scrollbar window styles so they stay hidden; frame change is required
            // for style changes to take effect (see SetWindowLong / SetWindowPos docs).
            uint style = PI.GetWindowLong(_targetControl.Handle, PI.GWL_.STYLE);
            style &= ~(uint)PI.WS_.HSCROLL;
            style &= ~(uint)PI.WS_.VSCROLL;
            PI.SetWindowLong(_targetControl.Handle, PI.GWL_.STYLE, style);
            PI.SetWindowPos(_targetControl.Handle, IntPtr.Zero, 0, 0, 0, 0,
                PI.SWP_.NOMOVE | PI.SWP_.NOSIZE | PI.SWP_.NOZORDER | PI.SWP_.FRAMECHANGED);

            _targetControl.Invalidate();
        }
        catch
        {
            // If we can't hide scrollbars, continue anyway
            // The Krypton scrollbars will still work
        }
    }

    private void SyncNativeScrollPosition(bool horizontal, ScrollEventArgs e)
    {
        if (_targetControl == null || !_targetControl.IsHandleCreated || _suppressScrollEvents)
        {
            return;
        }

        try
        {
            if (horizontal && _targetControl is ListBox horizontalListBox)
            {
                int pageWidth = GetListBoxHorizontalPageWidthForSync(horizontalListBox);
                int maximumLeftOffset = GetListBoxMaximumLeftOffsetForSync(horizontalListBox, pageWidth);
                int requestedLeftOffset = Math.Max(0, Math.Min(e.NewValue, maximumLeftOffset));
                PI.SetScrollPos(horizontalListBox.Handle, PI.SB_.HORZ, requestedLeftOffset, true);

                int horizontalScrollRequest = e.Type == ScrollEventType.ThumbTrack
                    ? (int)PI.SB_.THUMBTRACK
                    : (int)PI.SB_.THUMBPOSITION;

                PI.SendMessage(horizontalListBox.Handle, PI.WM_.HSCROLL,
                    (IntPtr)(horizontalScrollRequest | (requestedLeftOffset << 16)), IntPtr.Zero);

                EnsureNativeScrollbarsHidden();
                horizontalListBox.Invalidate();
                SyncListBoxHorizontalScrollbarValue(horizontalListBox, requestedLeftOffset);
                return;
            }

            if (!horizontal && _targetControl is ListBox listBox)
            {
                if (listBox.Items.Count == 0)
                {
                    return;
                }

                int visibleItems = GetListBoxVisibleItemsForSync(listBox);
                int maximumTopIndex = GetListBoxMaximumTopIndexForSync(listBox, visibleItems);
                int requestedTopIndex = Math.Max(0, Math.Min(e.NewValue, maximumTopIndex));

                if (listBox.TopIndex != requestedTopIndex)
                {
                    listBox.TopIndex = requestedTopIndex;
                }

                EnsureNativeScrollbarsHidden();
                listBox.Invalidate();
                SyncListBoxVerticalScrollbarValue(listBox);
                return;
            }

            PI.SB_ scrollBar = horizontal ? PI.SB_.HORZ : PI.SB_.VERT;
            int value = e.NewValue;
            PI.SetScrollPos(_targetControl.Handle, scrollBar, value, true);
            int scrollRequest = e.Type == ScrollEventType.ThumbTrack
                ? (int)PI.SB_.THUMBTRACK
                : (int)PI.SB_.THUMBPOSITION;

            // Send scroll message to update the control
            PI.SendMessage(_targetControl.Handle, horizontal ? PI.WM_.HSCROLL : PI.WM_.VSCROLL,
                (IntPtr)(scrollRequest | (value << 16)), IntPtr.Zero);
        }
        catch
        {
            // Ignore errors
        }
    }

    private void SyncListBoxHorizontalScrollbarValue(ListBox listBox, int requestedLeftOffset)
    {
        if (_horizontalScrollBar == null)
        {
            return;
        }

        int pageWidth = GetListBoxHorizontalPageWidthForSync(listBox);
        int maximumLeftOffset = GetListBoxMaximumLeftOffsetForSync(listBox, pageWidth);
        int value = Math.Min(requestedLeftOffset, maximumLeftOffset);

        var hScrollInfo = new WIN32ScrollBars.ScrollInfo
        {
            cbSize = Marshal.SizeOf(typeof(WIN32ScrollBars.ScrollInfo)),
            fMask = (int)PI.SIF_.POS
        };
        if (PI.GetScrollInfo(listBox.Handle, PI.SB_.HORZ, ref hScrollInfo))
        {
            value = Math.Min(Math.Max(0, hScrollInfo.nPos), maximumLeftOffset);
        }

        _suppressScrollEvents = true;
        try
        {
            _horizontalScrollBar.Value = value;
        }
        finally
        {
            _suppressScrollEvents = false;
        }
    }

    private void SyncListBoxVerticalScrollbarValue(ListBox listBox)
    {
        if (_verticalScrollBar == null)
        {
            return;
        }

        int visibleItems = GetListBoxVisibleItemsForSync(listBox);
        int maximumTopIndex = GetListBoxMaximumTopIndexForSync(listBox, visibleItems);
        int value = Math.Min(listBox.TopIndex, maximumTopIndex);

        _suppressScrollEvents = true;
        try
        {
            _verticalScrollBar.Value = value;
        }
        finally
        {
            _suppressScrollEvents = false;
        }
    }

    private Control? GetScrollbarHostControl()
    {
        if (_targetControl == null)
        {
            return null;
        }

        if (_mode == ScrollbarManagerMode.NativeWrapper &&
            _targetControl is ListBox &&
            _targetControl.Parent != null)
        {
            return _targetControl.Parent;
        }

        return _targetControl;
    }

    private void UpdateScrollbarHostHook()
    {
        Control? host = GetScrollbarHostControl();
        if (_scrollbarHostControl == host)
        {
            MoveExistingScrollbarsToHost(host);
            return;
        }

        UnhookScrollbarHostControl();

        _scrollbarHostControl = host;
        if (_scrollbarHostControl != null && _scrollbarHostControl != _targetControl)
        {
            _scrollbarHostControl.Resize += OnScrollbarHostResize;
            _scrollbarHostControl.Layout += OnScrollbarHostLayout;
        }

        MoveExistingScrollbarsToHost(host);
    }

    private void UnhookScrollbarHostControl()
    {
        if (_scrollbarHostControl != null && _scrollbarHostControl != _targetControl)
        {
            _scrollbarHostControl.Resize -= OnScrollbarHostResize;
            _scrollbarHostControl.Layout -= OnScrollbarHostLayout;
        }

        _scrollbarHostControl = null;
    }

    private Rectangle GetTargetClientRectangleInHost()
    {
        if (_targetControl == null)
        {
            return Rectangle.Empty;
        }

        Control? host = GetScrollbarHostControl();
        if (host == null)
        {
            return Rectangle.Empty;
        }

        if (host == _targetControl)
        {
            return _targetControl.ClientRectangle;
        }

        Point location = host.PointToClient(_targetControl.PointToScreen(Point.Empty));
        return new Rectangle(location, _targetControl.Size);
    }

    private void MoveExistingScrollbarsToHost(Control? host)
    {
        MoveScrollbarToHost(_horizontalScrollBar, host);
        MoveScrollbarToHost(_verticalScrollBar, host);
    }

    private static void MoveScrollbarToHost(Control? scrollbar, Control? host)
    {
        if (scrollbar == null || scrollbar.Parent == host)
        {
            return;
        }

        if (scrollbar.Parent != null)
        {
            RemoveScrollbarFromHost(scrollbar);
        }

        if (host != null && host.IsHandleCreated)
        {
            AddScrollbarToHost(host, scrollbar);
            scrollbar.BringToFront();
        }
    }

    private static void AddScrollbarToHost(Control host, Control scrollbar)
    {
        if (host.Controls is KryptonControlCollection kryptonControls)
        {
            kryptonControls.AddInternal(scrollbar);
        }
        else
        {
            host.Controls.Add(scrollbar);
        }
    }

    private static void RemoveScrollbarFromHost(Control scrollbar)
    {
        Control? parent = scrollbar.Parent;
        if (parent == null)
        {
            return;
        }

        if (parent.Controls is KryptonControlCollection kryptonControls)
        {
            kryptonControls.RemoveInternal(scrollbar);
        }
        else
        {
            parent.Controls.Remove(scrollbar);
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
            TabStop = false
        };

        // Only use anchors for container mode; native wrapper mode uses manual positioning
        if (_mode == ScrollbarManagerMode.Container)
        {
            _horizontalScrollBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        _horizontalScrollBar.Scroll += OnHorizontalScroll;

        Control? host = GetScrollbarHostControl();
        if (host != null && host.IsHandleCreated)
        {
            AddScrollbarToHost(host, _horizontalScrollBar);
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
            TabStop = false
        };

        // Only use anchors for container mode; native wrapper mode uses manual positioning
        if (_mode == ScrollbarManagerMode.Container)
        {
            _verticalScrollBar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        }

        _verticalScrollBar.Scroll += OnVerticalScroll;

        Control? host = GetScrollbarHostControl();
        if (host != null && host.IsHandleCreated)
        {
            AddScrollbarToHost(host, _verticalScrollBar);
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
                RemoveScrollbarFromHost(_horizontalScrollBar);
            }

            _horizontalScrollBar.Dispose();
            _horizontalScrollBar = null;
        }

        if (_verticalScrollBar != null)
        {
            _verticalScrollBar.Scroll -= OnVerticalScroll;
            if (_verticalScrollBar.Parent != null)
            {
                RemoveScrollbarFromHost(_verticalScrollBar);
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

        Rectangle clientRect = GetTargetClientRectangleInHost();
        int scrollbarWidth = SystemInformation.VerticalScrollBarWidth;
        int scrollbarHeight = SystemInformation.HorizontalScrollBarHeight;

        // Position horizontal scrollbar
        if (_horizontalScrollBar != null && _horizontalScrollBar.Visible)
        {
            int hScrollY = clientRect.Bottom - scrollbarHeight;
            int hScrollWidth = clientRect.Width - (_verticalScrollBar?.Visible == true ? scrollbarWidth : 0);

            // Ensure scrollbar stays within bounds
            hScrollY = Math.Max(clientRect.Top, Math.Min(hScrollY, clientRect.Bottom - 1));
            hScrollWidth = Math.Max(0, Math.Min(hScrollWidth, clientRect.Width));

            _horizontalScrollBar.Location = new Point(clientRect.Left, hScrollY);
            _horizontalScrollBar.Width = hScrollWidth;
            _horizontalScrollBar.Height = scrollbarHeight;
        }

        // Position vertical scrollbar
        if (_verticalScrollBar != null && _verticalScrollBar.Visible)
        {
            int vScrollX = clientRect.Right - scrollbarWidth;
            int vScrollHeight = clientRect.Height - (_horizontalScrollBar?.Visible == true ? scrollbarHeight : 0);

            // Ensure scrollbar stays within bounds
            vScrollX = Math.Max(clientRect.Left, Math.Min(vScrollX, clientRect.Right - 1));
            vScrollHeight = Math.Max(0, Math.Min(vScrollHeight, clientRect.Height));

            _verticalScrollBar.Location = new Point(vScrollX, clientRect.Top);
            _verticalScrollBar.Width = scrollbarWidth;
            _verticalScrollBar.Height = vScrollHeight;
        }

        BringScrollbarsToFront();
    }

    private void BringScrollbarsToFront()
    {
        if (_horizontalScrollBar != null && _horizontalScrollBar.Visible)
        {
            _horizontalScrollBar.BringToFront();
        }

        if (_verticalScrollBar != null && _verticalScrollBar.Visible)
        {
            _verticalScrollBar.BringToFront();
        }
    }

    #endregion

    #region Event Handlers

    private void OnTargetControlHandleCreated(object? sender, EventArgs e)
    {
        InvalidateNativeScrollbarState();
        UpdateScrollbarHostHook();
        UpdateScrollbars();
    }

    private void OnTargetControlHandleDestroyed(object? sender, EventArgs e)
    {
        // Scrollbars will be cleaned up in Detach
        _nativeThumbTracking = false;
        InvalidateNativeScrollbarState();
    }

    private void OnTargetControlResize(object? sender, EventArgs e)
    {
        InvalidateNativeScrollbarHiddenState();
        UpdateScrollbars();
    }

    private void OnTargetControlLayout(object? sender, LayoutEventArgs e)
    {
        if (!_isUpdating)
        {
            InvalidateNativeScrollbarHiddenState();
            UpdateScrollbars();
        }
    }

    private void OnTargetControlParentChanged(object? sender, EventArgs e)
    {
        UpdateScrollbarHostHook();
        InvalidateNativeScrollbarHiddenState();
        UpdateScrollbars();
    }

    private void OnScrollbarHostResize(object? sender, EventArgs e)
    {
        InvalidateNativeScrollbarHiddenState();
        UpdateScrollbars();
    }

    private void OnScrollbarHostLayout(object? sender, LayoutEventArgs e)
    {
        if (!_isUpdating)
        {
            InvalidateNativeScrollbarHiddenState();
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
            if (e.Type == ScrollEventType.ThumbTrack)
            {
                BeginNativeThumbTracking();
            }

            SyncNativeScrollPosition(true, e);
            if (e.Type == ScrollEventType.EndScroll)
            {
                EndNativeThumbTracking();
                UpdateScrollbars();
            }
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
            if (e.Type == ScrollEventType.ThumbTrack)
            {
                BeginNativeThumbTracking();
            }

            SyncNativeScrollPosition(false, e);
            if (e.Type == ScrollEventType.EndScroll)
            {
                EndNativeThumbTracking();
                UpdateScrollbars();
            }
        }
    }

    private void OnScrollbarsChanged()
    {
        ScrollbarsChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SyncTimer_Tick(object? sender, EventArgs e)
    {
        if (_mode == ScrollbarManagerMode.NativeWrapper && _enabled && !_isUpdating && !_nativeThumbTracking)
        {
            UpdateNativeWrapperScrollbars();
        }
    }

    #endregion
}
