using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace TestForm.RTLTests;

public partial class RTLMirroringTest : KryptonForm
{
    private KryptonPanel _mainPanel;
    private KryptonButton _leftButton;
    private KryptonButton _rightButton;
    private KryptonCheckBox _leftCheckBox;
    private KryptonCheckBox _rightCheckBox;
    private KryptonToggleSwitch _leftToggle;
    private KryptonToggleSwitch _rightToggle;
    private KryptonLabel _statusLabel;

    public RTLMirroringTest()
    {
        InitializeComponent();
        InitializeTestControls();
    }

    private void InitializeTestControls()
    {
        // Set initial RTL state
        RightToLeft = RightToLeft.No;
        RightToLeftLayout = false;
        Text = "RTL Mirroring Test - LTR Mode";

        // Create main panel
        _mainPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        // Create left side controls
        _leftButton = new KryptonButton
        {
            Text = "Left Button",
            Location = new Point(20, 20),
            Size = new Size(100, 30)
        };

        _leftCheckBox = new KryptonCheckBox
        {
            Text = "Left CheckBox",
            Location = new Point(20, 60),
            Size = new Size(120, 20)
        };

        _leftToggle = new KryptonToggleSwitch
        {
            Text = "Left Toggle",
            Location = new Point(20, 100),
            Size = new Size(120, 30)
        };

        // Create right side controls
        _rightButton = new KryptonButton
        {
            Text = "Right Button",
            Location = new Point(200, 20),
            Size = new Size(100, 30)
        };

        _rightCheckBox = new KryptonCheckBox
        {
            Text = "Right CheckBox",
            Location = new Point(200, 60),
            Size = new Size(120, 20)
        };

        _rightToggle = new KryptonToggleSwitch
        {
            Text = "Right Toggle",
            Location = new Point(200, 100),
            Size = new Size(120, 30)
        };

        // Create status label
        _statusLabel = new KryptonLabel
        {
            Text = "Controls should mirror when RTL is enabled",
            Location = new Point(20, 150),
            Size = new Size(300, 20),
            //TextAlign = ContentAlignment.MiddleCenter
        };

        // Create RTL toggle button
        var rtlToggleButton = new KryptonButton
        {
            Text = "Toggle RTL",
            Location = new Point(20, 180),
            Size = new Size(100, 30)
        };
        rtlToggleButton.Click += RtlToggleButton_Click;

        // Add controls to panel
        _mainPanel.Controls.Add(_leftButton);
        _mainPanel.Controls.Add(_leftCheckBox);
        _mainPanel.Controls.Add(_leftToggle);
        _mainPanel.Controls.Add(_rightButton);
        _mainPanel.Controls.Add(_rightCheckBox);
        _mainPanel.Controls.Add(_rightToggle);
        _mainPanel.Controls.Add(_statusLabel);
        _mainPanel.Controls.Add(rtlToggleButton);

        Controls.Add(_mainPanel);
    }

    private void RtlToggleButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (RightToLeft == RightToLeft.No)
            {
                // Enable RTL
                RightToLeft = RightToLeft.Yes;
                RightToLeftLayout = true;
                Text = "RTL Mirroring Test - RTL Mode";
                _statusLabel.Text = "RTL Enabled - Controls should be mirrored";
            }
            else
            {
                // Disable RTL
                RightToLeft = RightToLeft.No;
                RightToLeftLayout = false;
                Text = "RTL Mirroring Test - LTR Mode";
                _statusLabel.Text = "LTR Mode - Controls in normal positions";
            }

            // Force layout update
            PerformLayout();
            Invalidate();

            System.Diagnostics.Debug.WriteLine($"RTL Toggle: RightToLeft={RightToLeft}, RightToLeftLayout={RightToLeftLayout}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"RTL Toggle failed: {ex.Message}");
            MessageBox.Show($"RTL Toggle failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Test RTL mirroring functionality
    /// </summary>
    public void TestRTLMirroring()
    {
        try
        {
            // Test 1: Start in LTR mode
            RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            System.Diagnostics.Debug.WriteLine("Test 1: LTR mode set");

            // Test 2: Enable RTL
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            System.Diagnostics.Debug.WriteLine("Test 2: RTL mode set");

            // Test 3: Verify child controls have RTL settings
            foreach (Control child in _mainPanel.Controls)
            {
                if (child.RightToLeft != RightToLeft.Yes || child.RightToLeftLayout != true)
                {
                    throw new Exception($"Child control {child.Name} RTL settings not applied correctly");
                }
            }
            System.Diagnostics.Debug.WriteLine("Test 3: Child control RTL settings verified");

            // Test 4: Disable RTL
            RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            System.Diagnostics.Debug.WriteLine("Test 4: RTL disabled");

            // Test 5: Re-enable RTL
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            System.Diagnostics.Debug.WriteLine("Test 5: RTL re-enabled");

            System.Diagnostics.Debug.WriteLine("RTL Mirroring test passed");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"RTL Mirroring test failed: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Get current control positions for verification
    /// </summary>
    public string GetControlPositions()
    {
        var positions = $"Form RTL: {RightToLeft}, RTL Layout: {RightToLeftLayout}\n";
        positions += $"Left Button: {_leftButton.Location}\n";
        positions += $"Right Button: {_rightButton.Location}\n";
        positions += $"Left CheckBox: {_leftCheckBox.Location}\n";
        positions += $"Right CheckBox: {_rightCheckBox.Location}\n";
        positions += $"Left Toggle: {_leftToggle.Location}\n";
        positions += $"Right Toggle: {_rightToggle.Location}\n";
        
        return positions;
    }
} 