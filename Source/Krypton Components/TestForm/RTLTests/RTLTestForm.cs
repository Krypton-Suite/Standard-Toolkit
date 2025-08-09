using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace TestForm.RTLTests;

public partial class RTLTestForm : KryptonForm
{
    public RTLTestForm()
    {
        InitializeComponent();
            
        // Set initial text to indicate current state
        Text = "RTL Test Form - State: LTR Normal";
            
        // Set title alignment to center to test the problematic case
        //FormTitleAlign = PaletteRelativeAlign.Center;
    }

    private void ktsRTL_CheckedChanged(object sender, EventArgs e)
    {
        // Cycle through the four RTL states
        if (RightToLeft == RightToLeft.No && !RightToLeftLayout)
        {
            // State 1 -> State 2: RTL = false, RTL Layout = true
            RightToLeft = RightToLeft.No;
            RightToLeftLayout = true;
            Text = "RTL Test Form - State: LTR Normal (RTL Layout = true)";
        }
        else if (RightToLeft == RightToLeft.No && RightToLeftLayout)
        {
            // State 2 -> State 3: RTL = true, RTL Layout = false
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = false;
            Text = "RTL Test Form - State: RTL Aligned, Layout Unchanged";
        }
        else if (RightToLeft == RightToLeft.Yes && !RightToLeftLayout)
        {
            // State 3 -> State 4: RTL = true, RTL Layout = true
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "RTL Test Form - State: RTL Aligned, Layout Mirrored";
        }
        else
        {
            // State 4 -> State 1: RTL = false, RTL Layout = false
            RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            Text = "RTL Test Form - State: LTR Normal";
        }
    }

    /// <summary>
    /// Test method to verify that RTL properties can be set without causing InvalidOperationException
    /// </summary>
    public void TestRTLPropertySetting()
    {
        try
        {
            // Test setting RTL properties during initialization
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
                
            // Test cycling through states
            RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = false;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
                
            // If we get here without exception, the fix is working
            System.Diagnostics.Debug.WriteLine("RTL property setting test passed - no InvalidOperationException");
        }
        catch (InvalidOperationException ex)
        {
            System.Diagnostics.Debug.WriteLine($"RTL property setting test failed: {ex.Message}");
            throw;
        }
    }

    private void kryptonButton1_Click(object sender, EventArgs e)
    {
        new KryptonPanelRTLTest().ShowDialog();
    }
}