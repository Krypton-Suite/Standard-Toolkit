#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Krypton.Toolkit;
using Krypton.Utilities;

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of KryptonFloatingToolbars features including:
/// - Basic drag-and-drop floating/docking
/// - Programmatic Float() and Dock() methods
/// - FloatingStateChanged events
/// - Animation controls (EnableAnimation, AnimationDuration)
/// - Window styles (FloatingWindowStyle)
/// - Docking preview indicators
/// - Custom window themes
/// - State persistence (SaveState/LoadState)
/// - Multi-monitor support
/// - Toolbar grouping
/// </summary>
public partial class FloatingToolbarsDemo : KryptonForm
{
    #region Instance Fields

    private readonly List<FloatingToolbarState> _savedStates = [];
    private readonly string _stateFilePath;

    #endregion

    #region Identity

    public FloatingToolbarsDemo()
    {
        InitializeComponent();
        
        _stateFilePath = Path.Combine(
            FloatingToolbarStateManager.GetUserPreferencesPath("FloatingToolbarsDemo"),
            "toolbar_states.xml");

        InitializeDemo();
    }

    #endregion

    #region Implementation

    private void InitializeDemo()
    {
        // Setup toolbar panels
        SetupToolbarPanels();
        
        // Setup menu strip panels
        SetupMenuStripPanels();
        
        // Setup toolbars
        SetupToolbars();
        
        // Setup menu strips
        SetupMenuStrips();
        
        // Setup event handlers
        SetupEventHandlers();
        
        // Setup controls panel
        SetupControlsPanel();
        
        // Load saved states if available
        LoadSavedStates();
    }

    private void SetupToolbarPanels()
    {
        // Top panel
        ktspTop.Dock = DockStyle.Top;
        ktspTop.Name = "TopPanel";
        
        // Bottom panel
        ktspBottom.Dock = DockStyle.Bottom;
        ktspBottom.Name = "BottomPanel";
        
        // Left panel
        ktspLeft.Dock = DockStyle.Left;
        ktspLeft.Name = "LeftPanel";
        
        // Right panel
        ktspRight.Dock = DockStyle.Right;
        ktspRight.Name = "RightPanel";
    }

    private void SetupMenuStripPanels()
    {
        // Top menu panel
        kmspTop.Dock = DockStyle.Top;
        kmspTop.Name = "TopMenuPanel";
        
        // Bottom menu panel
        kmspBottom.Dock = DockStyle.Bottom;
        kmspBottom.Name = "BottomMenuPanel";
    }

    private void SetupToolbars()
    {
        // Toolbar 1: Basic toolbar
        kftsToolbar1.Name = "Toolbar1";
        kftsToolbar1.FloatingToolBarWindowText = "Standard Toolbar";
        kftsToolbar1.KryptonToolStripPanelExtendedList.Add(ktspTop);
        kftsToolbar1.KryptonToolStripPanelExtendedList.Add(ktspBottom);
        kftsToolbar1.KryptonToolStripPanelExtendedList.Add(ktspLeft);
        kftsToolbar1.KryptonToolStripPanelExtendedList.Add(ktspRight);
        
        // Add buttons to toolbar 1
        var btnNew = new ToolStripButton("New");
        var btnOpen = new ToolStripButton("Open");
        var btnSave = new ToolStripButton("Save");
        kftsToolbar1.Items.AddRange(new ToolStripItem[] { btnNew, btnOpen, btnSave });
        
        ktspTop.Controls.Add(kftsToolbar1);

        // Toolbar 2: Formatting toolbar
        kftsToolbar2.Name = "Toolbar2";
        kftsToolbar2.FloatingToolBarWindowText = "Formatting Toolbar";
        kftsToolbar2.KryptonToolStripPanelExtendedList.Add(ktspTop);
        kftsToolbar2.KryptonToolStripPanelExtendedList.Add(ktspBottom);
        kftsToolbar2.EnableAnimation = true;
        kftsToolbar2.AnimationDuration = 300;
        kftsToolbar2.FloatingWindowStyle = FloatingWindowStyle.ToolWindow;
        
        var btnBold = new ToolStripButton("B") { Font = new Font("Arial", 9, FontStyle.Bold) };
        var btnItalic = new ToolStripButton("I") { Font = new Font("Arial", 9, FontStyle.Italic) };
        var btnUnderline = new ToolStripButton("U") { Font = new Font("Arial", 9, FontStyle.Underline) };
        kftsToolbar2.Items.AddRange(new ToolStripItem[] { btnBold, btnItalic, btnUnderline });
        
        ktspTop.Controls.Add(kftsToolbar2);

        // Toolbar 3: Custom themed toolbar
        kftsToolbar3.Name = "Toolbar3";
        kftsToolbar3.FloatingToolBarWindowText = "Themed Toolbar";
        kftsToolbar3.KryptonToolStripPanelExtendedList.Add(ktspTop);
        kftsToolbar3.KryptonToolStripPanelExtendedList.Add(ktspBottom);
        kftsToolbar3.WindowTheme = FloatingWindowTheme.Dark;
        kftsToolbar3.FloatingWindowStyle = FloatingWindowStyle.Minimal;
        
        var btnCut = new ToolStripButton("Cut");
        var btnCopy = new ToolStripButton("Copy");
        var btnPaste = new ToolStripButton("Paste");
        kftsToolbar3.Items.AddRange(new ToolStripItem[] { btnCut, btnCopy, btnPaste });
        
        ktspBottom.Controls.Add(kftsToolbar3);
    }

    private void SetupMenuStrips()
    {
        // Menu Strip 1: File menu
        kfmsMenu1.Name = "MenuStrip1";
        kfmsMenu1.FloatingWindowText = "File Menu";
        kfmsMenu1.MenuStripPanelExtendedList?.Add(kmspTop);
        kfmsMenu1.MenuStripPanelExtendedList?.Add(kmspBottom);
        
        var fileMenuItem = new ToolStripMenuItem("File");
        fileMenuItem.DropDownItems.Add("New");
        fileMenuItem.DropDownItems.Add("Open");
        fileMenuItem.DropDownItems.Add("Save");
        fileMenuItem.DropDownItems.Add(new ToolStripSeparator());
        fileMenuItem.DropDownItems.Add("Exit");
        
        kfmsMenu1.Items.Add(fileMenuItem);
        kmspTop.Controls.Add(kfmsMenu1);

        // Menu Strip 2: Edit menu with custom theme
        kfmsMenu2.Name = "MenuStrip2";
        kfmsMenu2.FloatingWindowText = "Edit Menu";
        kfmsMenu2.MenuStripPanelExtendedList?.Add(kmspTop);
        kfmsMenu2.MenuStripPanelExtendedList?.Add(kmspBottom);
        kfmsMenu2.WindowTheme = FloatingWindowTheme.BlueAccent;
        kfmsMenu2.EnableAnimation = true;
        kfmsMenu2.AnimationDuration = 250;
        
        var editMenuItem = new ToolStripMenuItem("Edit");
        editMenuItem.DropDownItems.Add("Undo");
        editMenuItem.DropDownItems.Add("Redo");
        editMenuItem.DropDownItems.Add(new ToolStripSeparator());
        editMenuItem.DropDownItems.Add("Cut");
        editMenuItem.DropDownItems.Add("Copy");
        editMenuItem.DropDownItems.Add("Paste");
        
        kfmsMenu2.Items.Add(editMenuItem);
        kmspTop.Controls.Add(kfmsMenu2);
    }

    private void SetupEventHandlers()
    {
        // Toolbar 1 events
        kftsToolbar1.FloatingStateChanged += (sender, e) =>
        {
            LogEvent($"Toolbar1: {(e.IsFloating ? "Floated" : "Docked")}");
        };

        // Toolbar 2 events
        kftsToolbar2.FloatingStateChanged += (sender, e) =>
        {
            LogEvent($"Toolbar2: {(e.IsFloating ? "Floated" : "Docked")}");
        };

        // Toolbar 3 events
        kftsToolbar3.FloatingStateChanged += (sender, e) =>
        {
            LogEvent($"Toolbar3: {(e.IsFloating ? "Floated" : "Docked")}");
        };

        // Menu Strip 1 events
        kfmsMenu1.FloatingStateChanged += (sender, e) =>
        {
            LogEvent($"MenuStrip1: {(e.IsFloating ? "Floated" : "Docked")}");
        };

        // Menu Strip 2 events
        kfmsMenu2.FloatingStateChanged += (sender, e) =>
        {
            LogEvent($"MenuStrip2: {(e.IsFloating ? "Floated" : "Docked")}");
        };
    }

    private void SetupControlsPanel()
    {
        // Animation controls
        kchkEnableAnimation.Checked = true;
        knudAnimationDuration.Value = 200;
        kchkEnableAnimation.CheckedChanged += (s, e) =>
        {
            bool enabled = kchkEnableAnimation.Checked;
            kftsToolbar1.EnableAnimation = enabled;
            kftsToolbar2.EnableAnimation = enabled;
            kftsToolbar3.EnableAnimation = enabled;
            kfmsMenu1.EnableAnimation = enabled;
            kfmsMenu2.EnableAnimation = enabled;
        };
        
        knudAnimationDuration.ValueChanged += (s, e) =>
        {
            int duration = (int)knudAnimationDuration.Value;
            kftsToolbar1.AnimationDuration = duration;
            kftsToolbar2.AnimationDuration = duration;
            kftsToolbar3.AnimationDuration = duration;
            kfmsMenu1.AnimationDuration = duration;
            kfmsMenu2.AnimationDuration = duration;
        };

        // Window style controls
        kcmbWindowStyle.Items.AddRange(Enum.GetNames(typeof(FloatingWindowStyle)));
        kcmbWindowStyle.SelectedIndex = 0;
        kcmbWindowStyle.SelectedIndexChanged += (s, e) =>
        {
            if (kcmbWindowStyle.SelectedItem != null)
            {
                var style = (FloatingWindowStyle)Enum.Parse(typeof(FloatingWindowStyle), kcmbWindowStyle.SelectedItem.ToString()!);
                kftsToolbar1.FloatingWindowStyle = style;
                kftsToolbar2.FloatingWindowStyle = style;
                kftsToolbar3.FloatingWindowStyle = style;
                kfmsMenu1.FloatingWindowStyle = style;
                kfmsMenu2.FloatingWindowStyle = style;
            }
        };

        // Docking preview controls
        kchkShowDockingPreview.Checked = true;
        kchkShowDockingPreview.CheckedChanged += (s, e) =>
        {
            bool show = kchkShowDockingPreview.Checked;
            kftsToolbar1.ShowDockingPreview = show;
            kftsToolbar2.ShowDockingPreview = show;
            kftsToolbar3.ShowDockingPreview = show;
            kfmsMenu1.ShowDockingPreview = show;
            kfmsMenu2.ShowDockingPreview = show;
        };

        // Programmatic control buttons
        kbtnFloatToolbar1.Click += (s, e) => kftsToolbar1.Float();
        kbtnDockToolbar1.Click += (s, e) => kftsToolbar1.Dock();
        kbtnFloatToolbar2.Click += (s, e) => kftsToolbar2.Float();
        kbtnDockToolbar2.Click += (s, e) => kftsToolbar2.Dock();
        kbtnFloatMenu1.Click += (s, e) => kfmsMenu1.Float();
        kbtnDockMenu1.Click += (s, e) => kfmsMenu1.Dock();

        // State persistence buttons
        kbtnSaveStates.Click += (s, e) => SaveAllStates();
        kbtnLoadStates.Click += (s, e) => LoadSavedStates();
        kbtnClearStates.Click += (s, e) => ClearStates();

        // Theme selection
        kcmbTheme.Items.AddRange(new[] { "Default", "Dark", "Light", "Blue Accent" });
        kcmbTheme.SelectedIndex = 0;
        kcmbTheme.SelectedIndexChanged += (s, e) =>
        {
            ApplyThemeToToolbar3();
        };
    }

    private void ApplyThemeToToolbar3()
    {
        FloatingWindowTheme? theme = kcmbTheme.SelectedItem?.ToString() switch
        {
            "Dark" => FloatingWindowTheme.Dark,
            "Light" => FloatingWindowTheme.Light,
            "Blue Accent" => FloatingWindowTheme.BlueAccent,
            _ => FloatingWindowTheme.Default
        };
        
        kftsToolbar3.WindowTheme = theme;
    }

    private void LogEvent(string message)
    {
        if (ktbEventLog.InvokeRequired)
        {
            ktbEventLog.Invoke(new Action(() => LogEvent(message)));
            return;
        }

        ktbEventLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
        ktbEventLog.SelectionStart = ktbEventLog.Text.Length;
        ktbEventLog.ScrollToCaret();
    }

    private void SaveAllStates()
    {
        try
        {
            var states = new FloatingToolbarState?[]
            {
                kftsToolbar1.SaveState(),
                kftsToolbar2.SaveState(),
                kftsToolbar3.SaveState(),
                kfmsMenu1.SaveState(),
                kfmsMenu2.SaveState()
            }.Where(s => s != null).Cast<FloatingToolbarState>().ToList();

            if (states.Count > 0)
            {
                bool saved = FloatingToolbarStateManager.SaveStatesToUserPreferences(
                    states,
                    "FloatingToolbarsDemo",
                    "toolbar_states.xml");

                if (saved)
                {
                    LogEvent("States saved successfully!");
                    MessageBox.Show("Toolbar states saved successfully!", "Save States", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LogEvent("Failed to save states.");
                    MessageBox.Show("Failed to save toolbar states.", "Save States", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception ex)
        {
            LogEvent($"Error saving states: {ex.Message}");
            MessageBox.Show($"Error saving states: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadSavedStates()
    {
        try
        {
            var collection = FloatingToolbarStateManager.LoadStatesFromUserPreferences(
                "FloatingToolbarsDemo",
                "toolbar_states.xml");

            if (collection != null && collection.ToolbarStates.Count > 0)
            {
                kftsToolbar1.LoadState(collection.ToolbarStates.FirstOrDefault(s => s.Name == "Toolbar1"));
                kftsToolbar2.LoadState(collection.ToolbarStates.FirstOrDefault(s => s.Name == "Toolbar2"));
                kftsToolbar3.LoadState(collection.ToolbarStates.FirstOrDefault(s => s.Name == "Toolbar3"));
                kfmsMenu1.LoadState(collection.ToolbarStates.FirstOrDefault(s => s.Name == "MenuStrip1"));
                kfmsMenu2.LoadState(collection.ToolbarStates.FirstOrDefault(s => s.Name == "MenuStrip2"));

                LogEvent($"Loaded {collection.ToolbarStates.Count} toolbar states.");
                MessageBox.Show($"Loaded {collection.ToolbarStates.Count} toolbar states!", "Load States", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                LogEvent("No saved states found.");
            }
        }
        catch (Exception ex)
        {
            LogEvent($"Error loading states: {ex.Message}");
            MessageBox.Show($"Error loading states: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClearStates()
    {
        try
        {
            if (File.Exists(_stateFilePath))
            {
                File.Delete(_stateFilePath);
                LogEvent("Saved states cleared.");
                MessageBox.Show("Saved states cleared!", "Clear States", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                LogEvent("No saved states file found.");
            }
        }
        catch (Exception ex)
        {
            LogEvent($"Error clearing states: {ex.Message}");
            MessageBox.Show($"Error clearing states: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    #endregion
}
