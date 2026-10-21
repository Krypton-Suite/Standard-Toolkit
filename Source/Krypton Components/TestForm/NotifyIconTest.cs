#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class NotifyIconTest : KryptonForm
{
    private KryptonNotifyIcon? _notifyIcon;
    private System.Windows.Forms.ContextMenuStrip? _contextMenu;
    private int _clickCount;
    private int _doubleClickCount;
    private int _balloonTipShownCount;

    public NotifyIconTest()
    {
        InitializeComponent();
        InitializeNotifyIcon();
        InitializeContextMenu();
        UpdateUIState();
    }

    private void InitializeNotifyIcon()
    {
        _notifyIcon = new KryptonNotifyIcon(this.components)
        {
            Icon = SystemIcons.Application,
            Text = "Krypton NotifyIcon Test",
            Visible = false
        };

        // Hook up all events
        _notifyIcon.Click += OnNotifyIconClick;
        _notifyIcon.DoubleClick += OnNotifyIconDoubleClick;
        _notifyIcon.MouseClick += OnNotifyIconMouseClick;
        _notifyIcon.MouseDoubleClick += OnNotifyIconMouseDoubleClick;
        _notifyIcon.MouseMove += OnNotifyIconMouseMove;
        _notifyIcon.MouseDown += OnNotifyIconMouseDown;
        _notifyIcon.MouseUp += OnNotifyIconMouseUp;
        _notifyIcon.BalloonTipClicked += OnBalloonTipClicked;
        _notifyIcon.BalloonTipClosed += OnBalloonTipClosed;
        _notifyIcon.BalloonTipShown += OnBalloonTipShown;

        // Set default values
        ktxtToolTipText.Text = "Krypton NotifyIcon Test";
        ktxtBalloonTipTitle.Text = "Notification";
        ktxtBalloonTipText.Text = "This is a balloon tip notification";
        knudBalloonTipTimeout.Value = 5000;

        // Initialize combo boxes
        InitializeComboBoxes();
    }

    private void InitializeComboBoxes()
    {
        // Palette mode combo
        kcmbPaletteMode.Items.Clear();
        kcmbPaletteMode.Items.Add("Global");
        kcmbPaletteMode.Items.Add("Professional System");
        kcmbPaletteMode.Items.Add("Office 2003");
        kcmbPaletteMode.Items.Add("Office 2007");
        kcmbPaletteMode.Items.Add("Office 2010");
        kcmbPaletteMode.Items.Add("Office 2013");
        kcmbPaletteMode.Items.Add("Sparkle Blue");
        kcmbPaletteMode.Items.Add("Sparkle Orange");
        kcmbPaletteMode.Items.Add("Sparkle Purple");
        kcmbPaletteMode.SelectedIndex = 0; // "Global"

        // Balloon tip icon combo
        kcmbBalloonTipIcon.Items.Clear();
        kcmbBalloonTipIcon.Items.Add("None");
        kcmbBalloonTipIcon.Items.Add("Info");
        kcmbBalloonTipIcon.Items.Add("Warning");
        kcmbBalloonTipIcon.Items.Add("Error");
        kcmbBalloonTipIcon.SelectedIndex = 0; // "None"
    }

    private void InitializeContextMenu()
    {
        _contextMenu = new System.Windows.Forms.ContextMenuStrip();

        var showItem = new System.Windows.Forms.ToolStripMenuItem("Show Form");
        showItem.Click += (s, e) => { WindowState = FormWindowState.Normal; Show(); Activate(); };
        _contextMenu.Items.Add(showItem);

        var hideItem = new System.Windows.Forms.ToolStripMenuItem("Hide Form");
        hideItem.Click += (s, e) => { Hide(); };
        _contextMenu.Items.Add(hideItem);

        _contextMenu.Items.Add(new System.Windows.Forms.ToolStripSeparator());

        var showBalloonItem = new System.Windows.Forms.ToolStripMenuItem("Show Balloon Tip");
        showBalloonItem.Click += (s, e) => ShowBalloonTip();
        _contextMenu.Items.Add(showBalloonItem);

        _contextMenu.Items.Add(new System.Windows.Forms.ToolStripSeparator());

        var exitItem = new System.Windows.Forms.ToolStripMenuItem("Exit");
        exitItem.Click += (s, e) => Close();
        _contextMenu.Items.Add(exitItem);

        if (_notifyIcon?.NotifyIcon != null)
        {
            _notifyIcon.NotifyIcon.ContextMenuStrip = _contextMenu;
        }
    }

    private void OnNotifyIconClick(object? sender, EventArgs e)
    {
        _clickCount++;
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Click event");
        UpdateClickCounts();
    }

    private void OnNotifyIconDoubleClick(object? sender, EventArgs e)
    {
        _doubleClickCount++;
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] DoubleClick event");
        UpdateClickCounts();

        // Show form on double-click
        if (WindowState == FormWindowState.Minimized || !Visible)
        {
            WindowState = FormWindowState.Normal;
            Show();
            Activate();
        }
    }

    private void OnNotifyIconMouseClick(object? sender, MouseEventArgs e)
    {
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] MouseClick: Button={e.Button}, Clicks={e.Clicks}, X={e.X}, Y={e.Y}");
    }

    private void OnNotifyIconMouseDoubleClick(object? sender, MouseEventArgs e)
    {
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] MouseDoubleClick: Button={e.Button}, X={e.X}, Y={e.Y}");
    }

    private void OnNotifyIconMouseMove(object? sender, MouseEventArgs e)
    {
        // Only log occasionally to avoid flooding
        if (kchkLogMouseMove.Checked && DateTime.Now.Millisecond % 100 == 0)
        {
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] MouseMove: X={e.X}, Y={e.Y}");
        }
    }

    private void OnNotifyIconMouseDown(object? sender, MouseEventArgs e)
    {
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] MouseDown: Button={e.Button}, X={e.X}, Y={e.Y}");
    }

    private void OnNotifyIconMouseUp(object? sender, MouseEventArgs e)
    {
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] MouseUp: Button={e.Button}, X={e.X}, Y={e.Y}");
    }

    private void OnBalloonTipClicked(object? sender, EventArgs e)
    {
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] BalloonTipClicked");
        KryptonMessageBox.Show(this, "Balloon tip was clicked!", "Balloon Tip Clicked",
            KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
    }

    private void OnBalloonTipClosed(object? sender, EventArgs e)
    {
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] BalloonTipClosed");
    }

    private void OnBalloonTipShown(object? sender, EventArgs e)
    {
        _balloonTipShownCount++;
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] BalloonTipShown");
        UpdateClickCounts();
    }

    private void UpdateClickCounts()
    {
        klblClickCount.Text = $"Click Count: {_clickCount}";
        klblDoubleClickCount.Text = $"DoubleClick Count: {_doubleClickCount}";
        klblBalloonTipShownCount.Text = $"Balloon Tips Shown: {_balloonTipShownCount}";
    }

    private void AddEventToList(string message)
    {
        if (klstEvents.InvokeRequired)
        {
            klstEvents.Invoke(new Action(() => AddEventToList(message)));
            return;
        }

        klstEvents.Items.Add(message);

        // Auto-scroll to bottom
        if (klstEvents.Items.Count > 0)
        {
            klstEvents.SelectedIndex = klstEvents.Items.Count - 1;
        }

        // Limit to 1000 items
        if (klstEvents.Items.Count > 1000)
        {
            klstEvents.Items.RemoveAt(0);
        }
    }

    private void UpdateUIState()
    {
        var isVisible = _notifyIcon?.Visible ?? false;

        kbtnShow.Enabled = !isVisible;
        kbtnHide.Enabled = isVisible;
        kbtnShowBalloonTip.Enabled = isVisible;

        klblVisibleStatus.Text = isVisible ? "Visible: Yes" : "Visible: No";
        klblVisibleStatus.StateCommon.ShortText.Color1 = isVisible ? Color.Green : Color.Gray;

        if (_notifyIcon != null)
        {
            klblTextStatus.Text = $"Text: {_notifyIcon.Text}";
            klblIconStatus.Text = $"Icon: {(_notifyIcon.Icon != null ? "Set" : "Not Set")}";
        }
    }

    private void kbtnShow_Click(object sender, EventArgs e)
    {
        if (_notifyIcon != null)
        {
            _notifyIcon.Visible = true;
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] NotifyIcon shown");
            UpdateUIState();
        }
    }

    private void kbtnHide_Click(object sender, EventArgs e)
    {
        if (_notifyIcon != null)
        {
            _notifyIcon.Visible = false;
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] NotifyIcon hidden");
            UpdateUIState();
        }
    }

    private void ktxtToolTipText_TextChanged(object sender, EventArgs e)
    {
        if (_notifyIcon != null)
        {
            _notifyIcon.Text = ktxtToolTipText.Text;
            UpdateUIState();
        }
    }

    private void kbtnSetIcon_Click(object sender, EventArgs e)
    {
        if (_notifyIcon == null)
        {
            return;
        }

        using var dialog = new OpenFileDialog
        {
            Filter = "Icon Files (*.ico)|*.ico|All Files (*.*)|*.*",
            Title = "Select Icon File"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var icon = new Icon(dialog.FileName);
                _notifyIcon.Icon?.Dispose();
                _notifyIcon.Icon = icon;
                AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Icon changed to: {dialog.FileName}");
                UpdateUIState();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(this,
                    $"Error loading icon:\n{ex.Message}",
                    "Error",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
            }
        }
    }

    private void kbtnSetSystemIcon_Click(object sender, EventArgs e)
    {
        if (_notifyIcon == null)
        {
            return;
        }

        // Cycle through system icons
        var currentIcon = _notifyIcon.Icon;

        if (currentIcon == SystemIcons.Application)
        {
            _notifyIcon.Icon = SystemIcons.Information;
        }
        else if (currentIcon == SystemIcons.Information)
        {
            _notifyIcon.Icon = SystemIcons.Warning;
        }
        else if (currentIcon == SystemIcons.Warning)
        {
            _notifyIcon.Icon = SystemIcons.Error;
        }
        else if (currentIcon == SystemIcons.Error)
        {
            _notifyIcon.Icon = SystemIcons.Question;
        }
        else
        {
            _notifyIcon.Icon = SystemIcons.Application;
        }

        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] System icon changed");
        UpdateUIState();
    }

    private void kbtnShowBalloonTip_Click(object sender, EventArgs e)
    {
        ShowBalloonTip();
    }

    private void ShowBalloonTip()
    {
        if (_notifyIcon == null)
        {
            return;
        }

        var title = ktxtBalloonTipTitle.Text;
        var text = ktxtBalloonTipText.Text;
        var timeout = (int)knudBalloonTipTimeout.Value;
        var icon = kcmbBalloonTipIcon.SelectedIndex switch
        {
            1 => ToolTipIcon.Info,
            2 => ToolTipIcon.Warning,
            3 => ToolTipIcon.Error,
            _ => ToolTipIcon.None
        };

        // Set balloon tip properties
        _notifyIcon.BalloonTipTitle = title;
        _notifyIcon.BalloonTipText = text;
        _notifyIcon.BalloonTipIcon = icon;

        // Show the balloon tip
        _notifyIcon.ShowBalloonTip(timeout, title, text, icon);

        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Balloon tip shown: Title=\"{title}\", Text=\"{text}\", Timeout={timeout}ms, Icon={icon}");
    }

    private void kbtnClearEvents_Click(object sender, EventArgs e)
    {
        klstEvents.Items.Clear();
        _clickCount = 0;
        _doubleClickCount = 0;
        _balloonTipShownCount = 0;
        UpdateClickCounts();
    }

    private void kcmbPaletteMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_notifyIcon == null)
        {
            return;
        }

        var selected = kcmbPaletteMode.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        _notifyIcon.PaletteMode = selected switch
        {
            "Global" => PaletteMode.Global,
            "Professional System" => PaletteMode.ProfessionalSystem,
            "Office 2003" => PaletteMode.ProfessionalOffice2003,
            "Office 2007" => PaletteMode.Office2007Blue,
            "Office 2010" => PaletteMode.Office2010Blue,
            "Office 2013" => PaletteMode.Office2013White,
            "Sparkle Blue" => PaletteMode.SparkleBlue,
            "Sparkle Orange" => PaletteMode.SparkleOrange,
            "Sparkle Purple" => PaletteMode.SparklePurple,
            _ => PaletteMode.Global
        };
    }

    private void kbtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        // Hide notify icon before closing
        if (_notifyIcon != null)
        {
            _notifyIcon.Visible = false;
        }

        _notifyIcon?.Dispose();
        _contextMenu?.Dispose();
        base.OnFormClosing(e);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        // Minimize to tray
        if (WindowState == FormWindowState.Minimized && _notifyIcon?.Visible == true)
        {
            Hide();
        }
    }
}
