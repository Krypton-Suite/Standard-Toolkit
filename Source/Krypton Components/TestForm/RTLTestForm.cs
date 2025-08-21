using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace Krypton.Toolkit.TestForm;

/// <summary>
/// Test form for RTL functionality testing.
/// </summary>
public partial class RTLTestForm : KryptonForm
{
    private KryptonButton _testButton;
    private KryptonLabel _statusLabel;
    private KryptonPanel _controlPanel;
    private KryptonButton _btnRTLToggle;
    private KryptonButton _btnRTLLayoutToggle;
    private KryptonButton _btnReset;

    public RTLTestForm()
    {
        InitializeComponent();
        SetupRTLTestControls();
        UpdateStatusDisplay();
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
            
        // Form properties
        this.Text = "RTL Test Form";
        this.Size = new Size(800, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.Sizable;
            
        // Create main panel
        _controlPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20)
        };

        // Create test button (this is the button we want to test positioning)
        _testButton = new KryptonButton
        {
            Text = "Test Button",
            Size = new Size(120, 40),
            Location = new Point(50, 100),
            ButtonStyle = ButtonStyle.ButtonSpec
        };

        // Create status label
        _statusLabel = new KryptonLabel
        {
            Text = "RTL Status: ",
            Size = new Size(400, 30),
            Location = new Point(50, 200),
            StateCommon = { ShortText = { Font = new Font("Segoe UI", 10, FontStyle.Bold) } }
        };

        // Create RTL toggle button
        _btnRTLToggle = new KryptonButton
        {
            Text = "Toggle RTL",
            Size = new Size(120, 40),
            Location = new Point(50, 250),
            ButtonStyle = ButtonStyle.ButtonSpec
        };
        _btnRTLToggle.Click += BtnRTLToggle_Click;

        // Create RTL Layout toggle button
        _btnRTLLayoutToggle = new KryptonButton
        {
            Text = "Toggle RTL Layout",
            Size = new Size(120, 40),
            Location = new Point(200, 250),
            ButtonStyle = ButtonStyle.ButtonSpec
        };
        _btnRTLLayoutToggle.Click += BtnRTLLayoutToggle_Click;

        // Create reset button
        _btnReset = new KryptonButton
        {
            Text = "Reset to LTR",
            Size = new Size(120, 40),
            Location = new Point(350, 250),
            ButtonStyle = ButtonStyle.ButtonSpec
        };
        _btnReset.Click += BtnReset_Click;

        // Add controls to panel
        _controlPanel.Controls.Add(_testButton);
        _controlPanel.Controls.Add(_statusLabel);
        _controlPanel.Controls.Add(_btnRTLToggle);
        _controlPanel.Controls.Add(_btnRTLLayoutToggle);
        _controlPanel.Controls.Add(_btnReset);

        // Add panel to form
        this.Controls.Add(_controlPanel);
            
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private void SetupRTLTestControls()
    {
        // Add some additional test controls to demonstrate RTL behavior
        var testLabel = new KryptonLabel
        {
            Text = "This is a test label for RTL positioning",
            Size = new Size(300, 30),
            Location = new Point(50, 300),
            StateCommon = { ShortText = { Font = new Font("Segoe UI", 9) } }
        };

        var testTextBox = new KryptonTextBox
        {
            Text = "Test text input",
            Size = new Size(200, 30),
            Location = new Point(50, 350)
        };

        var testCheckBox = new KryptonCheckBox
        {
            Text = "Test checkbox",
            Size = new Size(150, 30),
            Location = new Point(50, 400)
        };

        _controlPanel.Controls.Add(testLabel);
        _controlPanel.Controls.Add(testTextBox);
        _controlPanel.Controls.Add(testCheckBox);
    }

    private void BtnRTLToggle_Click(object sender, EventArgs e)
    {
        // Toggle between RTL.Yes and RTL.No
        if (this.RightToLeft == RightToLeft.Yes)
        {
            this.RightToLeft = RightToLeft.No;
        }
        else
        {
            this.RightToLeft = RightToLeft.Yes;
        }
            
        UpdateStatusDisplay();
    }

    private void BtnRTLLayoutToggle_Click(object sender, EventArgs e)
    {
        // Toggle RTL Layout
        this.RightToLeftLayout = !this.RightToLeftLayout;
        UpdateStatusDisplay();
    }

    private void BtnReset_Click(object sender, EventArgs e)
    {
        // Reset to LTR mode
        this.RightToLeft = RightToLeft.No;
        this.RightToLeftLayout = false;
        UpdateStatusDisplay();
    }

    private void UpdateStatusDisplay()
    {
        string rtlStatus = this.RightToLeft == RightToLeft.Yes ? "Yes" : "No";
        string rtlLayoutStatus = this.RightToLeftLayout ? "Yes" : "No";
            
        string behavior = GetRTLBehaviorDescription();
            
        _statusLabel.Text = $"RTL: {rtlStatus} | RTL Layout: {rtlLayoutStatus}\nBehavior: {behavior}";
            
        // Update button texts to show current state
        _btnRTLToggle.Text = $"RTL: {rtlStatus}";
        _btnRTLLayoutToggle.Text = $"Layout: {rtlLayoutStatus}";
    }

    private string GetRTLBehaviorDescription()
    {
        if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
        {
            return "Full RTL - Controls aligned RTL, layout mirrored RTL, title bar mirrored";
        }
        else if (this.RightToLeft == RightToLeft.Yes && !this.RightToLeftLayout)
        {
            return "RTL Controls - Controls aligned RTL, layout unchanged, title bar unchanged";
        }
        else
        {
            return "LTR Normal - Standard left-to-right layout";
        }
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
        base.OnRightToLeftChanged(e);
        UpdateStatusDisplay();
    }

    protected override void OnRightToLeftLayoutChanged(EventArgs e)
    {
        base.OnRightToLeftLayoutChanged(e);
        UpdateStatusDisplay();
    }
}