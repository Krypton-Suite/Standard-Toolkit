using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace Krypton.Toolkit.TestForm;

/// <summary>
/// Launcher form for RTL testing.
/// </summary>
public partial class RTLTestLauncher : KryptonForm
{
    private KryptonButton _btnTestRTLForm;
    private KryptonLabel _descriptionLabel;
    private KryptonPanel _mainPanel;

    public RTLTestLauncher()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
            
        // Form properties
        this.Text = "RTL Test Launcher";
        this.Size = new Size(600, 400);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
            
        // Create main panel
        _mainPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(30)
        };

        // Create description label
        _descriptionLabel = new KryptonLabel
        {
            Text = "RTL (Right-to-Left) Testing Tool\n\n" +
                   "This tool allows you to test the RTL functionality of Krypton Toolkit.\n\n" +
                   "RTL Settings:\n" +
                   "• RTL = No, RTL Layout = No: Standard LTR layout\n" +
                   "• RTL = Yes, RTL Layout = No: Controls aligned RTL, layout unchanged\n" +
                   "• RTL = Yes, RTL Layout = Yes: Full RTL layout with mirrored title bar\n\n" +
                   "Click the button below to open the test form.",
            Size = new Size(500, 200),
            Location = new Point(30, 30),
            StateCommon = { ShortText = { Font = new Font("Segoe UI", 9) } }
        };

        // Create test button
        _btnTestRTLForm = new KryptonButton
        {
            Text = "Open RTL Test Form",
            Size = new Size(200, 50),
            Location = new Point(200, 250),
            ButtonStyle = ButtonStyle.ButtonSpec
        };
        _btnTestRTLForm.Click += BtnTestRTLForm_Click;

        // Add controls to panel
        _mainPanel.Controls.Add(_descriptionLabel);
        _mainPanel.Controls.Add(_btnTestRTLForm);

        // Add panel to form
        this.Controls.Add(_mainPanel);
            
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private void BtnTestRTLForm_Click(object sender, EventArgs e)
    {
        try
        {
            var testForm = new RTLTestForm();
            testForm.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error opening test form: {ex.Message}", "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}