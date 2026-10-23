#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Cursor-following overlay used to indicate tear-out/new-window drops while dragging a caption tab.
/// </summary>
internal sealed class TearOutFeedbackWindow : KryptonForm
{
    #region Static Values

    private const int FeedbackWidth = 120;
    private const int FeedbackHeight = 34;

    private const int WS_EX_TRANSPARENT = 0x00000020;
    private const int WS_EX_NOACTIVATE = 0x08000000;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="TearOutFeedbackWindow" /> class.</summary>
    public TearOutFeedbackWindow()
    {
        FormBorderStyle = FormBorderStyle.None;
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.Manual;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        TopMost = true;

        // Transparent background so the overlay does not affect hit-testing.
        BackColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
        TransparencyKey = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;

        Opacity = 0.65;
        Width = FeedbackWidth;
        Height = FeedbackHeight;

        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        UpdateStyles();

        Visible = false;
    }

    #endregion

    #region CreateParams

    /// <summary>
    /// Make the overlay click-through and prevent focus activation.
    /// </summary>
    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= WS_EX_TRANSPARENT;
            cp.ExStyle |= WS_EX_NOACTIVATE;
            return cp;
        }
    }

    #endregion

    #region Implementation

    public void ShowAtScreenPoint(Point screenPt)
    {
        var x = screenPt.X - (Width / 2);
        var y = screenPt.Y - (Height / 2);
        DesktopBounds = new Rectangle(x, y, Width, Height);

        if (!Visible)
        {
            Show();
        }
        else
        {
            Invalidate();
        }
    }

    public void HideFeedback()
    {
        if (IsDisposed)
        {
            return;
        }

        if (Visible)
        {
            Visible = false;
            Hide();
        }
    }

    #endregion

    #region Overrides

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using var pen = new Pen(Color.SteelBlue, 2);
        using var brush = new SolidBrush(Color.FromArgb(60, Color.SteelBlue));

        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        e.Graphics.FillRectangle(brush, ClientRectangle);
        e.Graphics.DrawRectangle(pen, new Rectangle(1, 1, Width - 3, Height - 3));

        using var font = new Font(Font.FontFamily, 10f, FontStyle.Bold);
        using var textBrush = new SolidBrush(Color.White);
        var text = $@"↗ {KryptonManager.Strings.NavigatorIntegrationStrings.NewWindow}";
        var textSize = e.Graphics.MeasureString(text, font);
        var textPt = new PointF(
            (Width - textSize.Width) / 2f,
            (Height - textSize.Height) / 2f);
        e.Graphics.DrawString(text, font, textBrush, textPt);
    }

    #endregion
}

