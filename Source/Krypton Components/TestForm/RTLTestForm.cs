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
    }

    private void kbtnToggleRTL_Click(object sender, EventArgs e)
    {
        // Toggle RTL settings
        RightToLeft = RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;
        RightToLeftLayout = !RightToLeftLayout;

        // Update button text
        kbtnToggleRTL.Text = $@"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}";
    }
}