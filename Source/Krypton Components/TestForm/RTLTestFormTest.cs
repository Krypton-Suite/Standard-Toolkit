#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

/// <summary>
/// A simple test form to demonstrate and verify RTL (Right-To-Left) support in KryptonForm.
/// - Contains a label explaining the test.
/// - Contains a button to toggle RTL mode (RightToLeft and RightToLeftLayout).
/// - Dynamically updates the button text to indicate the current RTL state.
/// - Shows how the window buttons and icon are mirrored in RTL.
/// </summary>
public partial class RTLTestFormTest : KryptonForm
{
    public RTLTestFormTest()
    {
        //InitializeComponent();
        
        // --- RTL TEST: Set up the form for demonstration ---
        Text = "RTL Test Form";
        Size = new Size(600, 400);
        StartPosition = FormStartPosition.CenterScreen;
        
        // Create a simple panel with some controls
        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10)
        };
        
        // Add a label
        var label = new KryptonLabel
        {
            Text = "This is a test form to demonstrate RTL support in KryptonForm.",
            Dock = DockStyle.Top,
            Height = 30
        };
        
        // Add a button to toggle RTL
        var toggleButton = new KryptonButton
        {
            Text = "Toggle RTL",
            Dock = DockStyle.Top,
            Height = 30
        };
        
        // --- RTL TEST: Toggle RTL mode on button click ---
        toggleButton.Click += (sender, e) =>
        {
            // Toggle RTL settings
            RightToLeft = RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;
            RightToLeftLayout = !RightToLeftLayout;
            
            // Update button text
            toggleButton.Text = $"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}";
        };
        
        // Add controls to panel
        panel.Controls.Add(label);
        panel.Controls.Add(toggleButton);
        
        // Add panel to form
        Controls.Add(panel);
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // RTLTestForm
            // 
            this.ClientSize = new System.Drawing.Size(288, 249);
            this.Name = "RTLTestForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ResumeLayout(false);

    }
}