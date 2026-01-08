#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class KryptonSplitterDemo : KryptonForm
{
    private KryptonSplitter? _verticalSplitter;
    private KryptonSplitter? _horizontalSplitter;
    private KryptonSplitter? _leftSplitter;
    private KryptonSplitter? _rightSplitter;
    private KryptonPanel? _leftPanel;
    private KryptonPanel? _middlePanel;
    private KryptonPanel? _rightPanel;
    private KryptonPanel? _topPanel;
    private KryptonPanel? _bottomPanel;

    public KryptonSplitterDemo()
    {
        InitializeComponent();
        InitializeSplitters();
        UpdateUIState();
    }

    private void InitializeSplitters()
    {
        // Create vertical splitter demo
        SetupVerticalSplitter();

        // Create horizontal splitter demo
        SetupHorizontalSplitter();

        // Create three-panel layout demo
        SetupThreePanelLayout();

        // Populate combo boxes
        InitializeComboBoxes();
    }

    private void SetupVerticalSplitter()
    {
        // Left panel
        _leftPanel = new KryptonPanel
        {
            Dock = DockStyle.Left,
            Width = 200,
            PanelBackStyle = PaletteBackStyle.PanelAlternate
        };
        _leftPanel.Controls.Add(new KryptonLabel
        {
            Text = "Left Panel",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.TitlePanel
        });
        kpnlVerticalDemo.Controls.Add(_leftPanel);

        // Vertical splitter
        _verticalSplitter = new KryptonSplitter
        {
            Dock = DockStyle.Left,
            MinExtra = 150,
            MinSize = 100,
            BorderStyle = BorderStyle.FixedSingle
        };
        _verticalSplitter.SplitterMoved += OnVerticalSplitterMoved;
        _verticalSplitter.SplitterMoving += OnVerticalSplitterMoving;
        kpnlVerticalDemo.Controls.Add(_verticalSplitter);

        // Right panel
        _rightPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };
        _rightPanel.Controls.Add(new KryptonLabel
        {
            Text = "Right Panel",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.TitlePanel
        });
        kpnlVerticalDemo.Controls.Add(_rightPanel);
    }

    private void SetupHorizontalSplitter()
    {
        // Top panel
        _topPanel = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 100,
            PanelBackStyle = PaletteBackStyle.PanelAlternate
        };
        _topPanel.Controls.Add(new KryptonLabel
        {
            Text = "Top Panel",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.TitlePanel
        });
        kpnlHorizontalDemo.Controls.Add(_topPanel);

        // Horizontal splitter
        _horizontalSplitter = new KryptonSplitter
        {
            Dock = DockStyle.Top,
            MinExtra = 100,
            MinSize = 50,
            BorderStyle = BorderStyle.FixedSingle
        };
        _horizontalSplitter.SplitterMoved += OnHorizontalSplitterMoved;
        _horizontalSplitter.SplitterMoving += OnHorizontalSplitterMoving;
        kpnlHorizontalDemo.Controls.Add(_horizontalSplitter);

        // Bottom panel
        _bottomPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };
        _bottomPanel.Controls.Add(new KryptonLabel
        {
            Text = "Bottom Panel",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.TitlePanel
        });
        kpnlHorizontalDemo.Controls.Add(_bottomPanel);
    }

    private void SetupThreePanelLayout()
    {
        // Left panel
        var leftPanel = new KryptonPanel
        {
            Dock = DockStyle.Left,
            Width = 150,
            PanelBackStyle = PaletteBackStyle.PanelAlternate
        };
        leftPanel.Controls.Add(new KryptonLabel
        {
            Text = "Left",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.TitlePanel
        });
        kpnlThreePanelDemo.Controls.Add(leftPanel);

        // First splitter
        _leftSplitter = new KryptonSplitter
        {
            Dock = DockStyle.Left,
            MinExtra = 200,
            MinSize = 100,
            BorderStyle = BorderStyle.FixedSingle
        };
        _leftSplitter.SplitterMoved += OnLeftSplitterMoved;
        kpnlThreePanelDemo.Controls.Add(_leftSplitter);

        // Middle panel
        _middlePanel = new KryptonPanel
        {
            Dock = DockStyle.Left,
            Width = 200,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };
        _middlePanel.Controls.Add(new KryptonLabel
        {
            Text = "Middle",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.TitlePanel
        });
        kpnlThreePanelDemo.Controls.Add(_middlePanel);

        // Second splitter
        _rightSplitter = new KryptonSplitter
        {
            Dock = DockStyle.Left,
            MinExtra = 200,
            MinSize = 100,
            BorderStyle = BorderStyle.FixedSingle
        };
        _rightSplitter.SplitterMoved += OnRightSplitterMoved;
        kpnlThreePanelDemo.Controls.Add(_rightSplitter);

        // Right panel
        var rightPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelAlternate
        };
        rightPanel.Controls.Add(new KryptonLabel
        {
            Text = "Right",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.TitlePanel
        });
        kpnlThreePanelDemo.Controls.Add(rightPanel);
    }

    private void InitializeComboBoxes()
    {
        // Populate Dock combo
        kcmbDock.Items.Clear();
        kcmbDock.Items.Add("Left");
        kcmbDock.Items.Add("Right");
        kcmbDock.Items.Add("Top");
        kcmbDock.Items.Add("Bottom");
        kcmbDock.SelectedIndex = 0; // "Left"

        // Populate PaletteMode combo
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
    }

    private void OnVerticalSplitterMoved(object? sender, SplitterEventArgs e)
    {
        var message = $"[{DateTime.Now:HH:mm:ss.fff}] Vertical Splitter Moved to ({e.SplitX}, {e.SplitY})";
        AddEventToList(message);
        UpdateSplitterInfo();
    }

    private void OnVerticalSplitterMoving(object? sender, SplitterEventArgs e)
    {
        klblVerticalPosition.Text = $"Position: {_verticalSplitter?.SplitPosition ?? 0}";
    }

    private void OnHorizontalSplitterMoved(object? sender, SplitterEventArgs e)
    {
        var message = $"[{DateTime.Now:HH:mm:ss.fff}] Horizontal Splitter Moved to ({e.SplitX}, {e.SplitY})";
        AddEventToList(message);
        UpdateSplitterInfo();
    }

    private void OnHorizontalSplitterMoving(object? sender, SplitterEventArgs e)
    {
        klblHorizontalPosition.Text = $"Position: {_horizontalSplitter?.SplitPosition ?? 0}";
    }

    private void OnLeftSplitterMoved(object? sender, SplitterEventArgs e)
    {
        var message = $"[{DateTime.Now:HH:mm:ss.fff}] Left Splitter Moved to ({e.SplitX}, {e.SplitY})";
        AddEventToList(message);
        UpdateSplitterInfo();
    }

    private void OnRightSplitterMoved(object? sender, SplitterEventArgs e)
    {
        var message = $"[{DateTime.Now:HH:mm:ss.fff}] Right Splitter Moved to ({e.SplitX}, {e.SplitY})";
        AddEventToList(message);
        UpdateSplitterInfo();
    }

    private void AddEventToList(string message)
    {
        if (klstEvents.InvokeRequired)
        {
            klstEvents.Invoke(new Action(() => AddEventToList(message)));
            return;
        }

        klstEvents.Items.Add(message);

        if (klstEvents.Items.Count > 0)
        {
            klstEvents.SelectedIndex = klstEvents.Items.Count - 1;
        }

        if (klstEvents.Items.Count > 500)
        {
            klstEvents.Items.RemoveAt(0);
        }
    }

    private void UpdateSplitterInfo()
    {
        if (_verticalSplitter != null)
        {
            klblVerticalPosition.Text = $"Position: {_verticalSplitter.SplitPosition}";
            klblVerticalMinSize.Text = $"MinSize: {_verticalSplitter.MinSize}";
            klblVerticalMinExtra.Text = $"MinExtra: {_verticalSplitter.MinExtra}";
            klblVerticalDock.Text = $"Dock: {_verticalSplitter.Dock}";
        }

        if (_horizontalSplitter != null)
        {
            klblHorizontalPosition.Text = $"Position: {_horizontalSplitter.SplitPosition}";
            klblHorizontalMinSize.Text = $"MinSize: {_horizontalSplitter.MinSize}";
            klblHorizontalMinExtra.Text = $"MinExtra: {_horizontalSplitter.MinExtra}";
            klblHorizontalDock.Text = $"Dock: {_horizontalSplitter.Dock}";
        }

        if (_leftSplitter != null)
        {
            klblLeftSplitterPosition.Text = $"Position: {_leftSplitter.SplitPosition}";
        }

        if (_rightSplitter != null)
        {
            klblRightSplitterPosition.Text = $"Position: {_rightSplitter.SplitPosition}";
        }
    }

    private void UpdateUIState()
    {
        UpdateSplitterInfo();
    }

    private void knudMinSize_ValueChanged(object sender, EventArgs e)
    {
        var minSize = (int)knudMinSize.Value;
        var targetSplitter = GetSelectedSplitter();

        if (targetSplitter != null)
        {
            targetSplitter.MinSize = minSize;
            UpdateSplitterInfo();
        }
    }

    private void knudMinExtra_ValueChanged(object sender, EventArgs e)
    {
        var minExtra = (int)knudMinExtra.Value;
        var targetSplitter = GetSelectedSplitter();

        if (targetSplitter != null)
        {
            targetSplitter.MinExtra = minExtra;
            UpdateSplitterInfo();
        }
    }

    private void kcmbDock_SelectedIndexChanged(object sender, EventArgs e)
    {
        // This is for demonstration - in real scenarios, changing Dock at runtime
        // would require recreating the layout
        var selected = kcmbDock.SelectedItem?.ToString();
        klblSelectedDock.Text = $"Selected Dock: {selected}";
    }

    private void kcmbPaletteMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = kcmbPaletteMode.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        var paletteMode = selected switch
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

        // Apply to all splitters
        if (_verticalSplitter != null)
        {
            _verticalSplitter.PaletteMode = paletteMode;
        }
        if (_horizontalSplitter != null)
        {
            _horizontalSplitter.PaletteMode = paletteMode;
        }
        if (_leftSplitter != null)
        {
            _leftSplitter.PaletteMode = paletteMode;
        }
        if (_rightSplitter != null)
        {
            _rightSplitter.PaletteMode = paletteMode;
        }
    }

    private KryptonSplitter? GetSelectedSplitter()
    {
        // Return the vertical splitter as default for property editing
        return _verticalSplitter;
    }

    private void kbtnSavePositions_Click(object sender, EventArgs e)
    {
        var positions = new
        {
            Vertical = _verticalSplitter?.SplitPosition ?? 0,
            Horizontal = _horizontalSplitter?.SplitPosition ?? 0,
            Left = _leftSplitter?.SplitPosition ?? 0,
            Right = _rightSplitter?.SplitPosition ?? 0
        };

        klblSavedPositions.Text = $"Saved - V:{positions.Vertical}, H:{positions.Horizontal}, L:{positions.Left}, R:{positions.Right}";
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Positions saved");
    }

    private void kbtnRestorePositions_Click(object sender, EventArgs e)
    {
        // Restore to default positions
        if (_verticalSplitter != null)
        {
            _verticalSplitter.SplitPosition = 200;
        }
        if (_horizontalSplitter != null)
        {
            _horizontalSplitter.SplitPosition = 100;
        }
        if (_leftSplitter != null)
        {
            _leftSplitter.SplitPosition = 150;
        }
        if (_rightSplitter != null)
        {
            _rightSplitter.SplitPosition = 200;
        }

        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Positions restored to defaults");
        UpdateSplitterInfo();
    }

    private void kbtnClearEvents_Click(object sender, EventArgs e)
    {
        klstEvents.Items.Clear();
    }

    private void kbtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}

