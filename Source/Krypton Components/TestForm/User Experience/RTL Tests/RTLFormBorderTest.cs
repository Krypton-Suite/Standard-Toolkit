#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class RTLFormBorderTest : KryptonForm
{
    public RTLFormBorderTest()
    {
        InitializeComponent();

        kchkbtnSwitchLayout.Checked = true;
    }

    private void kchkbtnSwitchLayout_Click(object sender, EventArgs e)
    {
        if (kchkbtnSwitchLayout.Checked)
        {
            RightToLeftLayout = true;

            RightToLeft = RightToLeft.Yes;
        }
        else
        {
            RightToLeftLayout = false;
            RightToLeft = RightToLeft.No;
        }
    }
}