#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Drawing.Imaging;

namespace TestForm;

/// <summary>
/// Demo form for taskbar thumbnail toolbar buttons (quick actions in the taskbar preview).
/// </summary>
public partial class TaskbarThumbnailButtonsDemo : KryptonForm
{
    private Icon? _playIcon;
    private Icon? _pauseIcon;
    private Icon? _nextIcon;
    private Icon? _stopIcon;

    private const uint IdPlay = 1;
    private const uint IdPause = 2;
    private const uint IdNext = 3;
    private const uint IdStop = 4;

    /// <summary>
    /// Taskbar thumbnail toolbar requires 32-bit icons at SM_CXICON x SM_CYICON (typically 32x32).
    /// </summary>
    private const int ThumbnailIconSize = 32;

    public TaskbarThumbnailButtonsDemo()
    {
        InitializeComponent();
    }

    private void TaskbarThumbnailButtonsDemo_Load(object? sender, EventArgs e)
    {
        Icon = SystemIcons.Application;
        CreateDemoIcons();
        AddThumbnailButtons();
        ThumbnailButtonClick += OnThumbnailButtonClick;
        UpdateLastClicked(null);
    }

    private void CreateDemoIcons()
    {
        _playIcon = CreateTaskbarIcon(Color.DodgerBlue, DrawPlay);
        _pauseIcon = CreateTaskbarIcon(Color.Orange, DrawPause);
        _nextIcon = CreateTaskbarIcon(Color.MediumSeaGreen, DrawNext);
        _stopIcon = CreateTaskbarIcon(Color.Crimson, DrawStop);
    }

    private static Icon CreateTaskbarIcon(Color color, Action<Graphics, Brush> draw)
    {
        var bmp = new Bitmap(ThumbnailIconSize, ThumbnailIconSize, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(color))
            {
                draw(g, brush);
            }
        }

        return Icon.FromHandle(bmp.GetHicon());
    }

    private static void DrawPlay(Graphics g, Brush brush)
    {
        var pts = new[] { new Point(10, 8), new Point(10, 24), new Point(24, 16) };
        g.FillPolygon(brush, pts);
    }

    private static void DrawPause(Graphics g, Brush brush)
    {
        g.FillRectangle(brush, 10, 8, 6, 16);
        g.FillRectangle(brush, 20, 8, 6, 16);
    }

    private static void DrawNext(Graphics g, Brush brush)
    {
        var pts1 = new[] { new Point(8, 8), new Point(8, 24), new Point(18, 16) };
        var pts2 = new[] { new Point(18, 8), new Point(18, 24), new Point(28, 16) };
        g.FillPolygon(brush, pts1);
        g.FillPolygon(brush, pts2);
    }

    private static void DrawStop(Graphics g, Brush brush)
    {
        g.FillRectangle(brush, 8, 8, 16, 16);
    }

    private void AddThumbnailButtons()
    {
        var tbv = ShellValues.ThumbnailButtonValues;
        tbv.Clear();
        tbv.Buttons.Add(new ThumbnailButtonItem { Id = IdPlay, Icon = _playIcon, Tooltip = "Play", Enabled = true });
        tbv.Buttons.Add(new ThumbnailButtonItem { Id = IdPause, Icon = _pauseIcon, Tooltip = "Pause", Enabled = true });
        tbv.Buttons.Add(new ThumbnailButtonItem { Id = IdNext, Icon = _nextIcon, Tooltip = "Next", Enabled = true });
        tbv.Buttons.Add(new ThumbnailButtonItem { Id = IdStop, Icon = _stopIcon, Tooltip = "Stop", Enabled = true });
        tbv.Apply();
    }

    private void OnThumbnailButtonClick(object? sender, ThumbnailButtonClickEventArgs e)
    {
        var name = e.ButtonId switch
        {
            IdPlay => "Play",
            IdPause => "Pause",
            IdNext => "Next",
            IdStop => "Stop",
            _ => $"Button {e.ButtonId}"
        };

        UpdateLastClicked(name);
    }

    private void UpdateLastClicked(string? name)
    {
        lblLastClicked.Text = name != null
            ? $"Last clicked: {name}"
            : "Hover the taskbar button, then click a thumbnail toolbar button.";
    }

    private void BtnAddButtons_Click(object? sender, EventArgs e)
    {
        AddThumbnailButtons();
        UpdateLastClicked(null);
    }

    private void BtnClearButtons_Click(object? sender, EventArgs e)
    {
        ShellValues.ThumbnailButtonValues.Clear();
        UpdateLastClicked(null);
    }
}
