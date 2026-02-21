#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

[ToolboxBitmap(typeof(KryptonFloatableToolStrip), "ToolboxBitmaps.FloatableToolStrip.bmp")]
public class KryptonFloatableToolStrip : KryptonToolStrip
{
    #region Instance Fields

    private VisualToolStripContainerForm? _containerForm;

    private Control? _originalParent;

    private bool _aboutToFloat;

    private bool _isFloating;

    private bool _parentChanged;

    private List<KryptonToolStripPanelExtended> _toolStripPanelExtendedList = [];

    private string _floatingToolBarWindowText;

    private bool _enableAnimation = true;

    private int _animationDuration = 200; // milliseconds

    private FloatingWindowStyle _floatingWindowStyle = FloatingWindowStyle.Default;

    private bool _showDockingPreview = true;

    private Color _dockingPreviewColor = Color.FromArgb(100, 0, 120, 215);

    private Color _dockingPreviewBorderColor = Color.FromArgb(200, 0, 120, 215);

    private DockingPreviewIndicator? _previewIndicator;

    private FloatingWindowTheme? _windowTheme;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the floating state of the toolbar changes.
    /// </summary>
    public event EventHandler<FloatingStateChangedEventArgs>? FloatingStateChanged;

    #endregion

    #region Public

    internal Control? OriginalParent => _originalParent;

    /// <summary>
    /// Gets or sets the tool strip panel extended list.
    /// </summary>
    /// <value>
    /// The tool strip panel extended list.
    /// </value>
    [Editor(typeof(KryptonToolStripPanelCollectionEditor), typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<KryptonToolStripPanelExtended> KryptonToolStripPanelExtendedList { get => _toolStripPanelExtendedList; set => _toolStripPanelExtendedList = value; }

    /// <summary>
    /// Gets a value indicating whether this instance is floating.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is floating; otherwise, <c>false</c>.
    /// </value>
    public bool IsFloating => _isFloating;

    /// <summary>
    /// Gets or sets a value indicating whether the control and all its child controls are displayed.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool Visible
    {
        get => base.Visible;

        set
        {
            if (_isFloating)
            {
                _containerForm?.Visible = value;
            }
            else
            {
                base.Visible = value;
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string FloatingToolBarWindowText { get => _floatingToolBarWindowText; set => _floatingToolBarWindowText = value; }

    /// <summary>
    /// Gets or sets a value indicating whether animation is enabled during dock/float transitions.
    /// </summary>
    [DefaultValue(true)]
    public bool EnableAnimation { get => _enableAnimation; set => _enableAnimation = value; }

    /// <summary>
    /// Gets or sets the duration of dock/float animations in milliseconds.
    /// </summary>
    [DefaultValue(200)]
    public int AnimationDuration { get => _animationDuration; set => _animationDuration = Math.Max(0, value); }

    /// <summary>
    /// Gets or sets the style of the floating window.
    /// </summary>
    [DefaultValue(FloatingWindowStyle.Default)]
    public FloatingWindowStyle FloatingWindowStyle { get => _floatingWindowStyle; set => _floatingWindowStyle = value; }

    /// <summary>
    /// Gets or sets a value indicating whether docking preview indicators are shown when dragging over dock zones.
    /// </summary>
    [DefaultValue(true)]
    public bool ShowDockingPreview { get => _showDockingPreview; set => _showDockingPreview = value; }

    /// <summary>
    /// Gets or sets the color of the docking preview indicator fill.
    /// </summary>
    [DefaultValue(typeof(Color), "100, 0, 120, 215")]
    public Color DockingPreviewColor
    {
        get => _dockingPreviewColor;
        set
        {
            _dockingPreviewColor = value;
            if (_previewIndicator != null)
            {
                _previewIndicator.IndicatorColor = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the color of the docking preview indicator border.
    /// </summary>
    [DefaultValue(typeof(Color), "200, 0, 120, 215")]
    public Color DockingPreviewBorderColor
    {
        get => _dockingPreviewBorderColor;
        set
        {
            _dockingPreviewBorderColor = value;
            if (_previewIndicator != null)
            {
                _previewIndicator.BorderColor = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the theme for the floating window.
    /// </summary>
    public FloatingWindowTheme? WindowTheme
    {
        get => _windowTheme;
        set
        {
            _windowTheme = value;
            ApplyWindowTheme();
        }
    }

    /// <summary>
    /// Programmatically floats the toolbar at the specified location.
    /// </summary>
    /// <param name="location">The screen location where the floating window should appear. If null, uses current control location.</param>
    /// <returns>True if the toolbar was successfully floated; false if it was already floating or cannot be floated.</returns>
    public bool Float(Point? location = null)
    {
        if (_isFloating)
        {
            return false; // Already floating
        }

        if (_originalParent == null && Parent == null)
        {
            return false; // No parent to float from
        }

        // Store original parent if not already stored
        if (_originalParent == null && Parent != null)
        {
            _originalParent = Parent;
        }

        // Create container form if needed
        if (_containerForm == null)
        {
            _containerForm = new VisualToolStripContainerForm();
            _containerForm.NCLBUTTONDBLCLK += ContainerForm_NCLBUTTONDBLCLK;
            _containerForm.LocationChanged += ContainerForm_LocationChanged;
            _containerForm.FormClosing += ContainerForm_FormClosing;
            _containerForm.MouseUp += ContainerForm_MouseUp;
        }

        // Remove from current parent
        if (Parent != null)
        {
            Parent.Controls.Remove(this);
        }

        // Set up container form
        _containerForm.KryptonFloatableToolStrip = this;
        
        // Apply window style
        ApplyWindowStyle(_containerForm);
        
        // Apply window theme
        ApplyWindowTheme();
        
        Point targetLocation = location ?? PointToScreen(Point.Empty);
        
        // Ensure location is on screen (multi-monitor support)
        targetLocation = MultiMonitorHelper.EnsurePointOnScreen(targetLocation);
        
        // Show floating window (initially hidden for animation)
        if (_originalParent?.Parent != null)
        {
            _containerForm.Show(_originalParent.Parent);
        }
        else if (_originalParent != null)
        {
            _containerForm.Show(_originalParent);
        }
        else
        {
            _containerForm.Show();
        }

        // Animate if enabled
        if (_enableAnimation && _animationDuration > 0)
        {
            Point startLocation = PointToScreen(Point.Empty);
            _containerForm.Location = startLocation;
            _containerForm.Opacity = 0;
            
            FloatingAnimationHelper.AnimateForm(_containerForm, targetLocation, 1.0, _animationDuration);
        }
        else
        {
            _containerForm.Location = targetLocation;
        }

        // Update state
        _isFloating = true;
        _aboutToFloat = false;
        OnFloatingStateChanged(true);

        return true;
    }

    /// <summary>
    /// Programmatically docks the toolbar to the specified panel or original parent.
    /// </summary>
    /// <param name="targetPanel">The panel to dock to. If null, docks to original parent.</param>
    /// <returns>True if the toolbar was successfully docked; false if it was not floating or cannot be docked.</returns>
    public new bool Dock(KryptonToolStripPanelExtended? targetPanel = null)
    {
        if (!_isFloating)
        {
            return false; // Not floating
        }

        Control? dockTarget = null;

        // Determine dock target
        if (targetPanel != null)
        {
            // Verify panel is in the collection
            if (!_toolStripPanelExtendedList.Contains(targetPanel))
            {
                return false; // Panel not in collection
            }
            dockTarget = targetPanel;
        }
        else if (_originalParent != null)
        {
            dockTarget = _originalParent;
        }
        else
        {
            return false; // No valid dock target
        }

        // Remove from floating container (without animation delay for docking)
        _containerForm?.Controls.Remove(this);
        _containerForm?.Hide();
        
        if (_containerForm != null)
        {
            _containerForm.Opacity = 1.0; // Reset opacity
        }

        // Add to dock target
        dockTarget.SuspendLayout();
        base.Dock = DockStyle.None;
        Anchor = AnchorStyles.Top | AnchorStyles.Left;
        dockTarget.Controls.Add(this);
        dockTarget.ResumeLayout();

        // Hide preview indicator
        HideDockingPreview();

        // Update state
        _isFloating = false;
        _parentChanged = false;
        OnFloatingStateChanged(false);

        // Restore focus
        if (_originalParent?.Parent != null)
        {
            _originalParent.Parent.Focus();
        }
        else
        {
            _originalParent?.Focus();
        }

        return true;
    }

    /// <summary>
    /// Saves the current floating state to a FloatingToolbarState object.
    /// </summary>
    /// <returns>A FloatingToolbarState object containing the current state, or null if the control has no name.</returns>
    public FloatingToolbarState? SaveState()
    {
        if (string.IsNullOrEmpty(Name))
        {
            return null;
        }

        var state = new FloatingToolbarState
        {
            Name = Name,
            IsFloating = _isFloating
        };

        if (_isFloating && _containerForm != null)
        {
            state.Location = _containerForm.Location;
            state.Size = _containerForm.Size;
        }
        else if (Parent != null)
        {
            state.Location = Parent.PointToScreen(Location);
            state.Size = Size;
            state.DockStyle = base.Dock;
            
            if (Parent is KryptonToolStripPanelExtended panel)
            {
                state.DockedPanelName = panel.Name;
            }
        }

        return state;
    }

    /// <summary>
    /// Loads a floating state from a FloatingToolbarState object.
    /// </summary>
    /// <param name="state">The FloatingToolbarState object to load.</param>
    /// <returns>True if the state was loaded successfully; otherwise, false.</returns>
    public bool LoadState(FloatingToolbarState? state)
    {
        if (state == null || string.IsNullOrEmpty(state.Name) || state.Name != Name)
        {
            return false;
        }

        if (state.IsFloating)
        {
            // Float the toolbar at the saved location
            if (Float(state.Location))
            {
                if (_containerForm != null && !state.Size.IsEmpty)
                {
                    _containerForm.Size = state.Size;
                }
                return true;
            }
        }
        else
        {
            // Dock to the saved panel if specified
            if (!string.IsNullOrEmpty(state.DockedPanelName))
            {
                var targetPanel = _toolStripPanelExtendedList.FirstOrDefault(p => p.Name == state.DockedPanelName);
                if (targetPanel != null)
                {
                    // If currently floating, dock to the panel
                    if (_isFloating)
                    {
                        return Dock(targetPanel);
                    }
                    // If not floating, just ensure it's in the right panel
                    else if (Parent != targetPanel)
                    {
                        Parent?.Controls.Remove(this);
                        targetPanel.Controls.Add(this);
                        if (state.DockStyle != DockStyle.None)
                        {
                            base.Dock = state.DockStyle;
                        }
                        return true;
                    }
                }
            }
        }

        return false;
    }

    #endregion

    #region Identity

    public KryptonFloatableToolStrip()
    {
        FloatingToolBarWindowText = @"Tool Bar";
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override void OnParentChanged(EventArgs e)
    {
        base.OnParentChanged(e);

        if (Parent != null)
        {
            _originalParent = Parent;

            if (_aboutToFloat)
            {
                _parentChanged = true;
            }
        }
    }

    /// <inheritdoc />
    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);

        Focus();
    }

    /// <inheritdoc />
    protected override void OnMouseDown(MouseEventArgs mea)
    {
        base.OnMouseDown(mea);

        if (!_isFloating && GripRectangle.Contains(mea.Location))
        {
            _aboutToFloat = true;
        }
    }

    /// <inheritdoc />
    protected override void OnMouseUp(MouseEventArgs mea)
    {
        base.OnMouseUp(mea);

        if (_parentChanged)
        {
            _parentChanged = false;

            _aboutToFloat = false;

            return;
        }

        Point p0 = PointToScreen(mea.Location), p1 = _originalParent!.PointToClient(p0);

        if (_aboutToFloat && !_originalParent.ClientRectangle.Contains(p1))
        {
            if (_containerForm == null)
            {
                _containerForm = new VisualToolStripContainerForm();

                _containerForm.NCLBUTTONDBLCLK += ContainerForm_NCLBUTTONDBLCLK;

                _containerForm.LocationChanged += ContainerForm_LocationChanged;

                _containerForm.FormClosing += ContainerForm_FormClosing;
            }

            _originalParent.Controls.Remove(this);

            _containerForm.KryptonFloatableToolStrip = this;

            _containerForm.Location = p0;

            if (Parent != null)
            {
                _containerForm.Show(Parent.Parent);
            }

            _aboutToFloat = false;

            _isFloating = true;
            OnFloatingStateChanged(true);
        }
    }
    #endregion

    #region Runtime Methods
    [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void GetCursorPos(out Point point);
    #endregion

    #region Event Handlers
    private void ContainerForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            Dock(); // Dock to original parent
        }
    }

    private void ContainerForm_LocationChanged(object? sender, EventArgs e)
    {
        Point point;

        if (_parentChanged)
        {
            _parentChanged = false;
        }

        GetCursorPos(out point);

        bool foundDockZone = false;

        // Check for docking zones
        foreach (KryptonToolStripPanelExtended item in _toolStripPanelExtendedList)
        {
            Rectangle activeRect = item.ActiveRectangle;
            Rectangle screenRect = item.RectangleToScreen(activeRect);

            if (activeRect.Contains(item.PointToClient(point)))
            {
                // Show preview indicator
                if (_showDockingPreview)
                {
                    ShowDockingPreviewIndicator(screenRect);
                }

                foundDockZone = true;
                break;
            }
        }

        // Check for group drop zones (tabbed containers)
        if (!foundDockZone && _containerForm != null)
        {
            foreach (var group in FloatingToolbarGroupManager.Groups)
            {
                if (group.IsTabbed && group.TabbedContainerForm != null)
                {
                    var containerForm = group.TabbedContainerForm;
                    Rectangle containerRect = containerForm.RectangleToScreen(containerForm.ClientRectangle);

                    if (containerRect.Contains(point))
                    {
                        // Show group drop indicator
                        if (_showDockingPreview)
                        {
                            ShowDockingPreviewIndicator(containerRect);
                        }
                        foundDockZone = true;
                        break;
                    }
                }
            }
        }

        // Hide preview if not over any dock zone
        if (!foundDockZone)
        {
            HideDockingPreview();
        }
    }

    private void ContainerForm_MouseUp(object? sender, MouseEventArgs e)
    {
        // Check if we should dock when mouse is released
        Point point;
        GetCursorPos(out point);

        // Check docking zones first
        foreach (KryptonToolStripPanelExtended item in _toolStripPanelExtendedList)
        {
            if (item.ActiveRectangle.Contains(item.PointToClient(point)))
            {
                HideDockingPreview();
                Dock(item);
                return;
            }
        }

        // Check for group drop zones
        if (_containerForm != null)
        {
            foreach (var group in FloatingToolbarGroupManager.Groups)
            {
                if (group.IsTabbed && group.TabbedContainerForm != null)
                {
                    var containerForm = group.TabbedContainerForm;
                    Rectangle containerRect = containerForm.RectangleToScreen(containerForm.ClientRectangle);

                    if (containerRect.Contains(point))
                    {
                        HideDockingPreview();
                        
                        // Move toolbar to group
                        FloatingToolbarGroupManager.MoveToolbarToGroup(this, group);
                        
                        // If group is tabbed, ensure it's visible
                        if (group.IsTabbed && containerForm.Visible)
                        {
                            // Toolbar will be added to tabbed container automatically
                            return;
                        }
                    }
                }
            }
        }

        HideDockingPreview();
    }

    private void ShowDockingPreviewIndicator(Rectangle screenRect)
    {
        if (!_showDockingPreview)
        {
            return;
        }

        if (_previewIndicator == null)
        {
            _previewIndicator = new DockingPreviewIndicator
            {
                IndicatorColor = _dockingPreviewColor,
                BorderColor = _dockingPreviewBorderColor
            };
        }

        _previewIndicator.ShowIndicator(screenRect);
    }

    private void HideDockingPreview()
    {
        _previewIndicator?.HideIndicator();
    }

    private void ContainerForm_NCLBUTTONDBLCLK(object? sender, EventArgs e)
    {
        Dock(); // Dock to original parent
    }

    /// <summary>
    /// Raises the FloatingStateChanged event.
    /// </summary>
    /// <param name="isFloating">True if the toolbar is now floating; false if docked.</param>
    protected virtual void OnFloatingStateChanged(bool isFloating)
    {
        FloatingStateChanged?.Invoke(this, new FloatingStateChangedEventArgs(isFloating));
    }

    /// <summary>
    /// Applies the configured window style to the container form.
    /// </summary>
    /// <param name="form">The container form.</param>
    private void ApplyWindowStyle(VisualToolStripContainerForm form)
    {
        switch (_floatingWindowStyle)
        {
            case FloatingWindowStyle.Minimal:
                form.FormBorderStyle = FormBorderStyle.None;
                break;
            case FloatingWindowStyle.ToolWindow:
                form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                break;
            case FloatingWindowStyle.Default:
            default:
                form.FormBorderStyle = FormBorderStyle.Sizable;
                break;
        }
    }

    /// <summary>
    /// Applies the configured window theme to the container form.
    /// </summary>
    private void ApplyWindowTheme()
    {
        if (_containerForm == null || _windowTheme == null)
        {
            return;
        }

        // Apply theme colors
        _containerForm.BackColor = _windowTheme.BackColor;
        
        if (_windowTheme.Opacity >= 0.0 && _windowTheme.Opacity <= 1.0)
        {
            _containerForm.Opacity = _windowTheme.Opacity;
        }

        // If custom painter is set, it will handle painting in OnPaint
        // Otherwise, use default theme colors
        if (_windowTheme.CustomPainter == null)
        {
            _windowTheme.CustomPainter = new FloatingWindowDefaultPainter(_windowTheme);
        }

        // Force repaint to apply custom painting
        _containerForm.Invalidate();
    }
    #endregion
}

/// <summary>
/// Provides data for the FloatingStateChanged event.
/// </summary>
public class FloatingStateChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets a value indicating whether the control is now floating.
    /// </summary>
    public bool IsFloating { get; }

    /// <summary>
    /// Initializes a new instance of the FloatingStateChangedEventArgs class.
    /// </summary>
    /// <param name="isFloating">True if the control is floating; otherwise, false.</param>
    public FloatingStateChangedEventArgs(bool isFloating)
    {
        IsFloating = isFloating;
    }
}
