#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, Lesandro and tobitege et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>Extends the KryptonForm to act as a floating window for a detached ribbon.</summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
public class VisualRibbonFloatingWindow : KryptonForm
{
    #region Instance Fields
    private KryptonRibbon? _ribbon;
    private VisualRibbonDropSolidWindow? _dropSolidWindow;
    private bool _isDragging;
    private bool _isOverSnapTarget;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the window is closing and the ribbon needs to be reattached.
    /// </summary>
    public event EventHandler? WindowClosing;

    /// <summary>
    /// Occurs when the title bar is double-clicked and the ribbon should be reattached.
    /// </summary>
    public event EventHandler? TitleBarDoubleClick;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the VisualRibbonFloatingWindow class.
    /// </summary>
    /// <param name="owner">Reference to form that will own the floating window.</param>
    /// <param name="ribbon">Reference to the ribbon control to host.</param>
    public VisualRibbonFloatingWindow(Form owner, KryptonRibbon ribbon)
    {
        _ribbon = ribbon ?? ThrowHelper.ThrowArgumentNullException(ribbon);

        // IMPORTANT: Set inherited control override for proper control handling
        // This is required for KryptonForm to properly manage controls
        SetInheritedControlOverride();

        // Set the owner of the window so that minimizing the owner will do the same to this
        Owner = owner;

        // Set correct form settings for a floating window
        TopLevel = true;
        ShowIcon = false;
        ShowInTaskbar = false;
        MinimizeBox = false;
        MaximizeBox = false;
        StartPosition = FormStartPosition.Manual;
        Text = _ribbon.FloatingWindowText ?? @"Ribbon";

        // Set border style to fixed tool window after initial sizing
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
    }

    #endregion

    #region Public
    /// <summary>
    /// Gets access to the contained ribbon control.
    /// </summary>
    public KryptonRibbon? Ribbon => _ribbon;

    #endregion

    #region Protected
    /// <summary>
    /// Raises the Load event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        // Ensure ribbon is properly sized and window is sized to fit
        if (_ribbon != null && _ribbon.Parent == this)
        {
            // Ensure ribbon is visible, properly docked, and at front of z-order
            _ribbon.Visible = true;
            _ribbon.Dock = DockStyle.Top;
            _ribbon.BringToFront();

            // Force layout first to get accurate measurements
            SuspendLayout();
            _ribbon.SuspendLayout();

            _ribbon.PerformLayout();
            PerformLayout();

            _ribbon.ResumeLayout(true);
            ResumeLayout(true);

            // Get the actual ribbon height after layout
            var ribbonHeight = _ribbon.Height;

            // If height is still invalid, use preferred size calculation
            if (ribbonHeight <= 0 || ribbonHeight < 100)
            {
                var preferredSize = _ribbon.GetPreferredSize(new Size(Math.Max(400, Width), 0));
                ribbonHeight = Math.Max(100, preferredSize.Height > 0 ? preferredSize.Height : 100);

                _ribbon.Size = new Size(Math.Max(400, Width), ribbonHeight);
                _ribbon.PerformLayout();
            }

            // Set client size based on ribbon's actual size
            ClientSize = new Size(Math.Max(400, _ribbon.Width), ribbonHeight);
            MinimumSize = Size;

            // Force a refresh to ensure ribbon is painted
            _ribbon.Invalidate(true);
            Invalidate(true);
            Update();
            _ribbon.Update();
        }
    }

    /// <summary>
    /// Raises the LocationChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnLocationChanged(EventArgs e)
    {
        base.OnLocationChanged(e);

        if (_isDragging)
        {
            UpdateDragFeedback();
        }
    }

#if NET10_0_OR_GREATER
    /// <summary>Raises the Form Closing event.</summary>
    /// <param name="e">An FormClosingEventArgs that contains the event data.</param>
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        // Notify that the window is closing so ribbon can be reattached
        OnWindowClosing(EventArgs.Empty);
        base.OnFormClosing(e);
    }
#else
    /// <summary>
    /// Raises the Closing event.
    /// </summary>
    /// <param name="e">An CancelEventArgs that contains the event data.</param>
    protected override void OnClosing(CancelEventArgs e)
    {
        // Notify that the window is closing so ribbon can be reattached
        OnWindowClosing(EventArgs.Empty);
        base.OnClosing(e);
    }
#endif

    /// <summary>
    /// Raises the WindowClosing event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnWindowClosing(EventArgs e) => WindowClosing?.Invoke(this, e);

    /// <summary>
    /// Raises the TitleBarDoubleClick event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnTitleBarDoubleClick(EventArgs e) => TitleBarDoubleClick?.Invoke(this, e);

    /// <summary>
    /// Process the WM_NCLBUTTONDBLCLK message when double-clicking the title bar.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected override bool OnWM_NCLBUTTONDBLCLK(ref Message m)
    {
        // Check if the double-click is on the caption/title bar area
        // SendMessage with int Msg returns uint
        var result = PI.SendMessage(Handle, (int)PI.WM_.NCHITTEST, IntPtr.Zero, m.LParam);
        
        if (result == (uint)PI.HT.CAPTION)
        {
            // Double-click on title bar - trigger reattach
            OnTitleBarDoubleClick(EventArgs.Empty);
            return true; // Message handled
        }

        // Let base class handle other cases (like icon double-click)
        return base.OnWM_NCLBUTTONDBLCLK(ref m);
    }

    /// <summary>
    /// Processes Windows messages.
    /// </summary>
    /// <param name="m">The Windows Message to process.</param>
    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case (int)PI.WM_.ENTERSIZEMOVE:
                StartDragFeedback();
                break;

            case (int)PI.WM_.MOVING:
                UpdateDragFeedback();
                break;

            case (int)PI.WM_.EXITSIZEMOVE:
                EndDragFeedback();
                break;
        }

        base.WndProc(ref m);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_dropSolidWindow != null)
            {
                _dropSolidWindow.Dispose();
                _dropSolidWindow = null;
            }

            // Remove ribbon reference but don't dispose it - it will be reattached
            _ribbon = null;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Implementation
    private void StartDragFeedback()
    {
        if (_isDragging || _ribbon == null || !_ribbon.AllowDragReattach)
        {
            return;
        }

        _isDragging = true;
        _isOverSnapTarget = false;

        if (_dropSolidWindow == null)
        {
            var palette = new PaletteDragDrop(_ribbon.GetResolvedPalette(), null);
            _dropSolidWindow = new VisualRibbonDropSolidWindow(palette, _ribbon.Renderer);
            _dropSolidWindow.SetBounds(SharedStaticConstants.OFF_SCREEN_POSITION, SharedStaticConstants.OFF_SCREEN_POSITION, 1, 1, BoundsSpecified.All);
            _dropSolidWindow.ShowWithoutActivate();
        }
    }

    private void UpdateDragFeedback()
    {
        if (!_isDragging || _ribbon == null || !_ribbon.AllowDragReattach || _dropSolidWindow == null)
        {
            return;
        }

        Control? targetParent = _ribbon.OriginalParent ?? Owner;
        if (targetParent == null || targetParent.IsDisposed || !targetParent.Visible)
        {
            _isOverSnapTarget = false;
            _dropSolidWindow.SolidRect = Rectangle.Empty;
            return;
        }

        // Calculate the dock target rectangle in screen coordinates
        Point targetScreenTopLeft = targetParent.PointToScreen(Point.Empty);
        var ribbonHeight = _ribbon.Height > 0 ? _ribbon.Height : 115;
        var ribbonWidth = targetParent.ClientSize.Width;
        var dockRect = new Rectangle(targetScreenTopLeft.X, targetScreenTopLeft.Y, ribbonWidth, ribbonHeight);

        // Snap zone: cursor position is within dockRect with a generous margin,
        // or the floating window itself overlaps the upper area of the parent form
        Point cursorPos = Cursor.Position;
        var expandedDockRect = new Rectangle(
            dockRect.Left - 30,
            dockRect.Top - 30,
            dockRect.Width + 60,
            dockRect.Height + 60);

        var targetParentScreenBounds = new Rectangle(
            targetScreenTopLeft.X,
            targetScreenTopLeft.Y,
            targetParent.ClientSize.Width,
            targetParent.ClientSize.Height);

        var inSnapZone = expandedDockRect.Contains(cursorPos)
                         || (targetParentScreenBounds.Contains(cursorPos) && cursorPos.Y <= targetParentScreenBounds.Top + Math.Max(120, ribbonHeight + 30));

        if (inSnapZone)
        {
            _isOverSnapTarget = true;
            _dropSolidWindow.SolidRect = dockRect;
        }
        else
        {
            _isOverSnapTarget = false;
            _dropSolidWindow.SolidRect = Rectangle.Empty;
        }
    }

    private void EndDragFeedback()
    {
        if (!_isDragging)
        {
            return;
        }

        _isDragging = false;

        if (_dropSolidWindow != null)
        {
            _dropSolidWindow.Dispose();
            _dropSolidWindow = null;
        }

        if (_isOverSnapTarget && _ribbon != null)
        {
            _isOverSnapTarget = false;
            _ribbon.Reattach();
        }
    }
    #endregion

    private void InitializeComponent()
    {
        this.SuspendLayout();
        // 
        // VisualRibbonFloatingWindow
        // 
        this.ClientSize = new System.Drawing.Size(1099, 293);
        this.Name = "VisualRibbonFloatingWindow";
        this.ResumeLayout(false);
    }
}
