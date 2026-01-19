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
//[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class VisualRibbonFloatingWindow : KryptonForm
{
    #region Instance Fields
    private KryptonRibbon? _ribbon;
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
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));

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

        // Ensure InternalPanel is visible by adding it to base.Controls
        // When SetInheritedControlOverride() is called, Controls property returns base.Controls,
        // but the InternalPanel itself needs to be added to base.Controls to be visible
        if (!base.Controls.Contains(InternalPanel))
        {
            InternalPanel.Dock = DockStyle.Fill;
            base.Controls.Add(InternalPanel);
            InternalPanel.BringToFront();
        }

        // Note: The ribbon will be added to this window by the Detach() method
        // after it's removed from its original parent. We just store the reference here.
        // The window size will be set in the Detach() method after the ribbon is added.

        // Set initial size - standard ribbon floating window size
        // This ensures the window is large enough to display the ribbon properly
        Size = new Size(1099, 293);

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
    /// Raises the HandleCreated event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        
        // Ensure InternalPanel is added to base.Controls when using SetInheritedControlOverride
        // This is similar to how MDI containers handle it
        if (!base.Controls.Contains(InternalPanel))
        {
            InternalPanel.Dock = DockStyle.Fill;
            base.Controls.Add(InternalPanel);
            InternalPanel.BringToFront();
        }
    }

    /// <summary>
    /// Raises the Load event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        
        // Ensure InternalPanel is visible
        if (!base.Controls.Contains(InternalPanel))
        {
            InternalPanel.Dock = DockStyle.Fill;
            base.Controls.Add(InternalPanel);
            InternalPanel.BringToFront();
        }

        // Ensure ribbon is properly sized and window is sized to fit
        // Check if ribbon is in base.Controls (when SetInheritedControlOverride is used)
        // or in InternalPanel.Controls
        var ribbonParent = _ribbon?.Parent;
        if (_ribbon != null && (ribbonParent == this || ribbonParent == InternalPanel))
        {
            // Ensure ribbon is visible and properly docked
            _ribbon.Visible = true;
            _ribbon.Dock = DockStyle.Top;
            
            // Force layout first to get accurate measurements
            SuspendLayout();
            _ribbon.SuspendLayout();
            
            // Perform layout to calculate actual size
            _ribbon.PerformLayout();
            PerformLayout();
            
            _ribbon.ResumeLayout(true);
            ResumeLayout(true);
            
            // Get the actual ribbon height after layout
            var ribbonHeight = _ribbon.Height;
            
            // If height is still invalid, use preferred size calculation
            if (ribbonHeight <= 0 || ribbonHeight < 100)
            {
                // Calculate preferred size with current width
                var preferredSize = _ribbon.GetPreferredSize(new Size(Math.Max(400, Width), 0));
                ribbonHeight = Math.Max(150, preferredSize.Height > 0 ? preferredSize.Height : 150);
                
                // Set ribbon size explicitly
                _ribbon.Size = new Size(Math.Max(400, Width), ribbonHeight);
                
                // Force another layout with the new size
                _ribbon.PerformLayout();
            }
            
            // Set window to standard size: 1099 x 293 pixels
            // This ensures the ribbon displays properly in the floating window
            Size = new Size(1099, 293);
            
            // Ensure minimum size is set
            MinimumSize = new Size(400, 150 + SystemInformation.CaptionHeight);
            
            // Force a refresh to ensure ribbon is painted
            _ribbon.Invalidate(true);
            Invalidate(true);
            Update();
            _ribbon.Update();
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
        uint result = PI.SendMessage(Handle, (int)PI.WM_.NCHITTEST, IntPtr.Zero, m.LParam);
        
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
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Remove ribbon reference but don't dispose it - it will be reattached
            _ribbon = null;
        }

        base.Dispose(disposing);
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
