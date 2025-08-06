using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace TestForm.RTLTests;

public partial class FormTitleAlignInheritTestOld : KryptonForm
{
    public FormTitleAlignInheritTestOld()
    {
        InitializeComponent();
        InitializeTest();
    }

    private void InitializeTest()
    {
        // Set the form title alignment to Inherit to test the fix
        FormTitleAlign = PaletteRelativeAlign.Inherit;
        
        // Set initial text
        Text = "FormTitleAlign Inherit Test";
        
        // Add some test controls
        var testPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        var testLabel = new KryptonLabel
        {
            Text = "Testing FormTitleAlign = Inherit\nThis should not cause assertion failures",
            Location = new Point(20, 20),
            Size = new Size(300, 60),
            //StateCommon. // .TextAlign = ContentAlignment.MiddleCenter
        };

        var testButton = new KryptonButton
        {
            Text = "Test RTL Toggle",
            Location = new Point(20, 100),
            Size = new Size(150, 30)
        };
        testButton.Click += TestButton_Click;

        testPanel.Controls.Add(testLabel);
        testPanel.Controls.Add(testButton);
        Controls.Add(testPanel);
    }

    private void TestButton_Click(object sender, EventArgs e)
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

    /// <summary>
    /// Test method to verify that FormTitleAlign = Inherit works correctly
    /// </summary>
    public void TestFormTitleAlignInherit()
    {
        try
        {
            // Test 1: Set FormTitleAlign to Inherit
            FormTitleAlign = PaletteRelativeAlign.Inherit;
            
            // Test 2: Enable RTL
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            
            // Test 3: Disable RTL
            RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            
            // Test 4: Enable RTL again
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            
            // Force repaint
            Invalidate();
            Update();
            
            System.Diagnostics.Debug.WriteLine("FormTitleAlign Inherit comprehensive test passed");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"FormTitleAlign Inherit comprehensive test failed: {ex.Message}");
            throw;
        }
    }
} 