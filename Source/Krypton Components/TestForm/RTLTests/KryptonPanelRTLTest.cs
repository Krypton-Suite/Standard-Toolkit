using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace TestForm.RTLTests;

public partial class KryptonPanelRTLTest : KryptonForm
{
    private KryptonPanel _mainPanel;
    private KryptonPanel _leftPanel;
    private KryptonPanel _rightPanel;
    private KryptonButton _testButton1;
    private KryptonButton _testButton2;
    private KryptonLabel _testLabel;

    public KryptonPanelRTLTest()
    {
        //InitializeComponent();
        InitializeTestControls();
    }

    private void InitializeTestControls()
    {
        // Create main panel
        _mainPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        // Create left panel
        _leftPanel = new KryptonPanel
        {
            Dock = DockStyle.Left,
            Width = 150,
            PanelBackStyle = PaletteBackStyle.PanelAlternate
        };

        // Create right panel
        _rightPanel = new KryptonPanel
        {
            Dock = DockStyle.Right,
            Width = 150,
            PanelBackStyle = PaletteBackStyle.PanelAlternate
        };

        // Create test controls
        _testButton1 = new KryptonButton
        {
            Text = "Left Button",
            Location = new Point(10, 10),
            Size = new Size(100, 30)
        };

        _testButton2 = new KryptonButton
        {
            Text = "Right Button",
            Location = new Point(10, 50),
            Size = new Size(100, 30)
        };

        _testLabel = new KryptonLabel
        {
            Text = "RTL Test Label",
            Location = new Point(10, 90),
            Size = new Size(120, 20)
        };

        // Add controls to panels
        _leftPanel.Controls.Add(_testButton1);
        _leftPanel.Controls.Add(_testButton2);
        _leftPanel.Controls.Add(_testLabel);

        _mainPanel.Controls.Add(_leftPanel);
        _mainPanel.Controls.Add(_rightPanel);

        Controls.Add(_mainPanel);

        // Set initial text
        Text = "KryptonPanel RTL Test - LTR Mode";
    }

    /// <summary>
    /// Test RTL functionality for KryptonPanel
    /// </summary>
    public void TestRTLFunctionality()
    {
        try
        {
            // Test 1: Enable RTL
            _mainPanel.RightToLeft = RightToLeft.Yes;
            _mainPanel.RightToLeftLayout = true;
            Text = "KryptonPanel RTL Test - RTL Mode Enabled";

            // Test 2: Disable RTL
            _mainPanel.RightToLeft = RightToLeft.No;
            _mainPanel.RightToLeftLayout = false;
            Text = "KryptonPanel RTL Test - LTR Mode";

            // Test 3: Enable RTL again
            _mainPanel.RightToLeft = RightToLeft.Yes;
            _mainPanel.RightToLeftLayout = true;
            Text = "KryptonPanel RTL Test - RTL Mode Enabled";

            System.Diagnostics.Debug.WriteLine("KryptonPanel RTL test passed - no exceptions thrown");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"KryptonPanel RTL test failed: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Toggle RTL mode for testing
    /// </summary>
    public void ToggleRTLMode()
    {
        if (_mainPanel.RightToLeft == RightToLeft.No)
        {
            _mainPanel.RightToLeft = RightToLeft.Yes;
            _mainPanel.RightToLeftLayout = true;
            Text = "KryptonPanel RTL Test - RTL Mode Enabled";
        }
        else
        {
            _mainPanel.RightToLeft = RightToLeft.No;
            _mainPanel.RightToLeftLayout = false;
            Text = "KryptonPanel RTL Test - LTR Mode";
        }
    }
} 