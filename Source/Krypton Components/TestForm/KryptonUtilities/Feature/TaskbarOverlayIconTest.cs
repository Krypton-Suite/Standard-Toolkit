#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive test form demonstrating taskbar overlay icon functionality on KryptonForm.
/// </summary>
public partial class TaskbarOverlayIconTest : KryptonForm
{
    public TaskbarOverlayIconTest()
    {
        InitializeComponent();
        InitializeTaskbarOverlayIcons();
    }

    private void InitializeTaskbarOverlayIcons()
    {
        // Set form icon
        Icon = SystemIcons.Application;

        // Setup examples
        SetupBasicExamples();
        SetupInteractiveExamples();
        SetupNotificationExamples();
        SetupStatusExamples();

        // Setup property grid
        propertyGrid.SelectedObject = this;
    }

    private void SetupBasicExamples()
    {
        // Example 1: Simple overlay icon
        lblExample1.Text = "Example 1: Simple overlay icon (red notification badge)";
        btnExample1.Text = "Set Red Badge";
        btnExample1.Click += (s, e) =>
        {
            ShellValues.OverlayIconValues.Icon = CreateOverlayIcon(Color.Red, "!");
            ShellValues.OverlayIconValues.Description = "Red notification badge";
        };

        // Example 2: Different colors
        lblExample2.Text = "Example 2: Color variations";
        btnExample2Red.Text = "Red";
        btnExample2Red.Click += (s, e) =>
        {
            ShellValues.OverlayIconValues.Icon = CreateOverlayIcon(Color.Red, "!");
            ShellValues.OverlayIconValues.Description = "Red notification";
        };

        btnExample2Green.Text = "Green";
        btnExample2Green.Click += (s, e) =>
        {
            ShellValues.OverlayIconValues.Icon = CreateOverlayIcon(Color.Green, "✓");
            ShellValues.OverlayIconValues.Description = "Green success indicator";
        };

        btnExample2Blue.Text = "Blue";
        btnExample2Blue.Click += (s, e) =>
        {
            ShellValues.OverlayIconValues.Icon = CreateOverlayIcon(Color.Blue, "i");
            ShellValues.OverlayIconValues.Description = "Blue information";
        };

        btnExample2Orange.Text = "Orange";
        btnExample2Orange.Click += (s, e) =>
        {
            ShellValues.OverlayIconValues.Icon = CreateOverlayIcon(Color.Orange, "!");
            ShellValues.OverlayIconValues.Description = "Orange warning";
        };
    }

    private void SetupInteractiveExamples()
    {
        // Example 3: Notification counter
        lblExample3.Text = "Example 3: Notification counter";
        btnIncrementNotifications.Text = "Increment";
        btnIncrementNotifications.Click += BtnIncrementNotifications_Click;

        btnDecrementNotifications.Text = "Decrement";
        btnDecrementNotifications.Click += BtnDecrementNotifications_Click;

        btnClearNotifications.Text = "Clear";
        btnClearNotifications.Click += BtnClearNotifications_Click;

        UpdateNotificationDisplay();

        // Example 4: Toggle overlay
        lblExample4.Text = "Example 4: Toggle overlay visibility";
        btnToggleOverlay.Text = "Toggle Overlay";
        btnToggleOverlay.Click += BtnToggleOverlay_Click;
        UpdateToggleStatus();
    }

    private void SetupNotificationExamples()
    {
        // Example 5: System icons
        lblExample5.Text = "Example 5: System icons as overlays";
        btnSystemError.Text = "Error";
        btnSystemError.Click += (s, e) =>
        {
            ShellValues.OverlayIconValues.Icon = SystemIcons.Error;
            ShellValues.OverlayIconValues.Description = "Error notification";
        };

        btnSystemWarning.Text = "Warning";
        btnSystemWarning.Click += (s, e) =>
        {
            ShellValues.OverlayIconValues.Icon = SystemIcons.Warning;
            ShellValues.OverlayIconValues.Description = "Warning notification";
        };

        btnSystemInfo.Text = "Information";
        btnSystemInfo.Click += (s, e) =>
        {
            ShellValues.OverlayIconValues.Icon = SystemIcons.Information;
            ShellValues.OverlayIconValues.Description = "Information notification";
        };

        btnSystemQuestion.Text = "Question";
        btnSystemQuestion.Click += (s, e) =>
        {
            ShellValues.OverlayIconValues.Icon = SystemIcons.Question;
            ShellValues.OverlayIconValues.Description = "Question notification";
        };
    }

    private void SetupStatusExamples()
    {
        // Example 6: Status indicators
        lblExample6.Text = "Example 6: Status indicators";
        btnStatusOnline.Text = "Online";
        btnStatusOnline.Click += (s, e) =>
        {
            ShellValues.OverlayIconValues.Icon = CreateOverlayIcon(Color.Lime, "●");
            ShellValues.OverlayIconValues.Description = "Application is online";
        };

        btnStatusOffline.Text = "Offline";
        btnStatusOffline.Click += (s, e) =>
        {
            ShellValues.OverlayIconValues.Icon = CreateOverlayIcon(Color.Gray, "●");
            ShellValues.OverlayIconValues.Description = "Application is offline";
        };

        btnSyncing.Text = "Syncing";
        btnSyncing.Click += (s, e) =>
        {
            ShellValues.OverlayIconValues.Icon = CreateOverlayIcon(Color.Blue, "⟳");
            ShellValues.OverlayIconValues.Description = "Synchronizing data";
        };
    }

    private Icon CreateOverlayIcon(Color backgroundColor, string? text = null)
    {
        // Create a 16x16 icon for taskbar overlay
        var bitmap = new Bitmap(16, 16);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw circular badge
            using (var brush = new SolidBrush(backgroundColor))
            {
                g.FillEllipse(brush, 0, 0, 16, 16);
            }

            // Draw white border
            using (var pen = new Pen(Color.White, 1.5f))
            {
                g.DrawEllipse(pen, 0.75f, 0.75f, 14.5f, 14.5f);
            }

            // Draw text if provided
            if (!string.IsNullOrEmpty(text))
            {
                using (var font = new Font("Arial", 9, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.White))
                {
                    var textSize = g.MeasureString(text, font);
                    g.DrawString(text, font, brush,
                        (16 - textSize.Width) / 2,
                        (16 - textSize.Height) / 2);
                }
            }
        }

        return Icon.FromHandle(bitmap.GetHicon());
    }

    private int _notificationCount = 5;

    private void BtnIncrementNotifications_Click(object? sender, EventArgs e)
    {
        _notificationCount++;
        UpdateNotificationDisplay();
    }

    private void BtnDecrementNotifications_Click(object? sender, EventArgs e)
    {
        if (_notificationCount > 0)
        {
            _notificationCount--;
            UpdateNotificationDisplay();
        }
    }

    private void BtnClearNotifications_Click(object? sender, EventArgs e)
    {
        _notificationCount = 0;
        UpdateNotificationDisplay();
    }

    private void UpdateNotificationDisplay()
    {
        if (_notificationCount > 0)
        {
            ShellValues.OverlayIconValues.Icon = CreateOverlayIcon(Color.Orange, _notificationCount > 99 ? "99+" : _notificationCount.ToString());
            ShellValues.OverlayIconValues.Description = $"{_notificationCount} notifications";
            lblNotificationCount.Text = $"Notification Count: {_notificationCount}";
        }
        else
        {
            ShellValues.OverlayIconValues.Icon = null;
            ShellValues.OverlayIconValues.Description = string.Empty;
            lblNotificationCount.Text = "Notification Count: 0 (Overlay cleared)";
        }
    }

    private void BtnToggleOverlay_Click(object? sender, EventArgs e)
    {
        bool isVisible = ShellValues.OverlayIconValues.Icon != null;
        if (isVisible)
        {
            ShellValues.OverlayIconValues.Icon = null;
            ShellValues.OverlayIconValues.Description = string.Empty;
        }
        else
        {
            ShellValues.OverlayIconValues.Icon = CreateOverlayIcon(Color.Red, "!");
            ShellValues.OverlayIconValues.Description = "Overlay visible";
        }
        UpdateToggleStatus();
    }

    private void UpdateToggleStatus()
    {
        lblToggleStatus.Text = $"Overlay: {(ShellValues.OverlayIconValues.Icon != null ? "Visible" : "Hidden")}";
    }
}
