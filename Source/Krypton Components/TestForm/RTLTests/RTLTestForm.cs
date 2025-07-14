using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm;

public partial class RTLTestForm : KryptonForm
{
    public RTLTestForm()
    {
        InitializeComponent();
        
        // Set initial RTL state
        UpdateRtlDisplay();
    }

    private void kbtnToggleRTL_Click(object sender, EventArgs e)
    {
        // Toggle RTL settings
        RightToLeft = RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;
        RightToLeftLayout = !RightToLeftLayout;

        // Update display
        UpdateRtlDisplay();
    }

    /// <summary>
    /// Updates the display to show current RTL state and test KryptonPanel functionality.
    /// </summary>
    private void UpdateRtlDisplay()
    {
        // Update button text
        kbtnToggleRTL.Text = $@"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}";
        
        // Update labels to show RTL state
        kryptonLabel1.Values.Text = $"RTL Mode: {(RightToLeft == RightToLeft.Yes ? "Enabled" : "Disabled")} - Test KryptonPanel RTL Support";
        kryptonLabel2.Values.Text = $"Bottom Label - RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")} - Notice how docked controls mirror in RTL mode";
        
        // Update button texts to show RTL awareness
        kbtnLeft.Values.Text = RightToLeft == RightToLeft.Yes ? "Right Button (Docked Left)" : "Left Button (Docked Left)";
        kbtnRight.Values.Text = RightToLeft == RightToLeft.Yes ? "Left Button (Docked Right)" : "Right Button (Docked Right)";
        kbtnCenter.Values.Text = $"Center Button - RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}";
    }
}