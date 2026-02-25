#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Provides visual docking preview indicators when dragging toolbars over dock zones.
/// </summary>
internal class DockingPreviewIndicator : Form
{
    #region Instance Fields

    private Rectangle _targetRectangle;
    private Color _indicatorColor = Color.FromArgb(100, 0, 120, 215); // Semi-transparent blue
    private Color _borderColor = Color.FromArgb(200, 0, 120, 215);
    private bool _isActive;
    private float _animationPhase = 0f;
    private System.Windows.Forms.Timer? _animationTimer;

    #endregion

    #region Identity

    public DockingPreviewIndicator()
    {
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        ShowInTaskbar = false;
        TopMost = true;
        BackColor = Color.Magenta; // Transparent key color
        TransparencyKey = Color.Magenta;
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        
        // Start animation timer
        _animationTimer = new System.Windows.Forms.Timer { Interval = 16 }; // ~60 FPS
        _animationTimer.Tick += AnimationTimer_Tick;
    }

    private void AnimationTimer_Tick(object? sender, EventArgs e)
    {
        _animationPhase += 0.1f;
        if (_animationPhase > Math.PI * 2)
        {
            _animationPhase = 0f;
        }
        Invalidate();
    }

    #endregion

    #region Public

    /// <summary>
    /// Shows the docking preview indicator at the specified screen rectangle.
    /// </summary>
    /// <param name="screenRect">The screen rectangle to highlight.</param>
    public void ShowIndicator(Rectangle screenRect)
    {
        _targetRectangle = screenRect;
        _isActive = true;
        
        Location = screenRect.Location;
        Size = screenRect.Size;
        
        if (!Visible)
        {
            Show();
        }
        
        // Start animation
        _animationTimer?.Start();
        _animationPhase = 0f;
        
        Invalidate();
    }

    /// <summary>
    /// Hides the docking preview indicator.
    /// </summary>
    public void HideIndicator()
    {
        _isActive = false;
        _animationTimer?.Stop();
        Hide();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _animationTimer?.Stop();
            _animationTimer?.Dispose();
        }
        base.Dispose(disposing);
    }

    /// <summary>
    /// Gets or sets the indicator color.
    /// </summary>
    public Color IndicatorColor
    {
        get => _indicatorColor;
        set
        {
            _indicatorColor = value;
            Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the border color.
    /// </summary>
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            _borderColor = value;
            Invalidate();
        }
    }

    #endregion

    #region Overrides

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (!_isActive || _targetRectangle.IsEmpty)
        {
            return;
        }

        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle rect = ClientRectangle;

        // Draw semi-transparent fill
        using (var brush = new SolidBrush(_indicatorColor))
        {
            g.FillRectangle(brush, rect);
        }

        // Draw animated border with pulsing effect
        float pulseAlpha = (float)(0.5 + 0.5 * Math.Sin(_animationPhase));
        Color animatedBorderColor = Color.FromArgb((int)(255 * pulseAlpha), _borderColor);
        
        using (var pen = new Pen(animatedBorderColor, 2.0f))
        {
            pen.DashStyle = DashStyle.Dash;
            // Animate dash offset
            float dashOffset = (float)(_animationPhase * 10);
            pen.DashOffset = dashOffset;
            pen.DashPattern = new float[] { 5, 5 };
            g.DrawRectangle(pen, 1, 1, rect.Width - 2, rect.Height - 2);
        }

        // Draw corner indicators
        int cornerSize = 8;
        using (var cornerBrush = new SolidBrush(_borderColor))
        {
            // Top-left
            g.FillRectangle(cornerBrush, 0, 0, cornerSize, 3);
            g.FillRectangle(cornerBrush, 0, 0, 3, cornerSize);

            // Top-right
            g.FillRectangle(cornerBrush, rect.Width - cornerSize, 0, cornerSize, 3);
            g.FillRectangle(cornerBrush, rect.Width - 3, 0, 3, cornerSize);

            // Bottom-left
            g.FillRectangle(cornerBrush, 0, rect.Height - 3, cornerSize, 3);
            g.FillRectangle(cornerBrush, 0, rect.Height - cornerSize, 3, cornerSize);

            // Bottom-right
            g.FillRectangle(cornerBrush, rect.Width - cornerSize, rect.Height - 3, cornerSize, 3);
            g.FillRectangle(cornerBrush, rect.Width - 3, rect.Height - cornerSize, 3, cornerSize);
        }
    }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
            cp.ExStyle |= 0x00000080; // WS_EX_TOOLWINDOW
            return cp;
        }
    }

    #endregion
}
