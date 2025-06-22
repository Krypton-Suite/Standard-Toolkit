#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class ControlsTest : KryptonForm
{
    public ControlsTest()
    {
        InitializeComponent();
    }

    private void ControlsTest_Load(object sender, EventArgs e)
    {
        kryptonRibbonGroupComboBox1.SelectedIndex = 1;

        kryptonRibbonGroupComboBox2.SelectedIndex = 1;
    }

    private void krgbBug833Test_Click(object sender, EventArgs e)
    {
        var bug833Test = new Bug833Test();

        bug833Test.Show();
    }

    private void kryptonButton17_Click(object sender, EventArgs e)
    {
        new CheckedListBoxDemo().ShowDialog();
    }
}