using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm.RTLTests
{
    public partial class FormTitleAlignInheritTest : KryptonForm
    {
        public FormTitleAlignInheritTest()
        {
            InitializeComponent();
        }

        private void kbtnTest_Click(object sender, EventArgs e)
        {
            try
            {
                // Test toggling RTL with FormTitleAlign set to Inherit
                if (RightToLeft == RightToLeft.No)
                {
                    RightToLeft = RightToLeft.Yes;
                    RightToLeftLayout = true;
                    Text = "FormTitleAlign Inherit Test - RTL Enabled";
                }
                else
                {
                    RightToLeft = RightToLeft.No;
                    RightToLeftLayout = false;
                    Text = "FormTitleAlign Inherit Test - LTR Mode";
                }

                // Force a repaint to trigger the rendering code
                Invalidate();
                Update();

                System.Diagnostics.Debug.WriteLine("FormTitleAlign Inherit test passed - no assertion failures");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FormTitleAlign Inherit test failed: {ex.Message}");
                MessageBox.Show($"Test failed: {ex.Message}", "Test Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
