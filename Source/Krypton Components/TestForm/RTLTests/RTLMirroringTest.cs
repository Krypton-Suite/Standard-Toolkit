using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm.RTLTests;

public partial class RTLMirroringTest : KryptonForm
{
    public RTLMirroringTest()
    {
        InitializeComponent();
    }

    private void ktsToggleRTL_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void kbtnToggleRTL_Click(object sender, EventArgs e)
    {
        try
        {
            if (RightToLeft == RightToLeft.No)
            {
                // Enable RTL
                RightToLeft = RightToLeft.Yes;
                RightToLeftLayout = true;
                Text = "RTL Mirroring Test - RTL Mode";
                kryptonLabel1.Text = "RTL Enabled - Controls should be mirrored";
            }
            else
            {
                // Disable RTL
                RightToLeft = RightToLeft.No;
                RightToLeftLayout = false;
                Text = "RTL Mirroring Test - LTR Mode";
                kryptonLabel1.Text = "LTR Mode - Controls in normal positions";
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
}