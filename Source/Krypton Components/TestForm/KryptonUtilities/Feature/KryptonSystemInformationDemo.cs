#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

public partial class KryptonSystemInformationDemo : KryptonForm
{
    public KryptonSystemInformationDemo()
    {
        InitializeComponent();
    }

    private void kbtnShow_Click(object sender, EventArgs e)
    {
        var data = new KryptonSystemInformationData
        {
            ShowWindowsSystemInformation = kchkWindowsMsinfo.Checked,
            UseRtlLayout = kchkRtl.Checked ? KryptonUseRTLLayout.Yes : KryptonUseRTLLayout.No,
            InitialCategoryId = SystemInformationCategoryId.SystemSummary
        };

        if (kchkModal.Checked)
        {
            KryptonSystemInformation.ShowDialog(this, data);
        }
        else
        {
            KryptonSystemInformation.Show(this, data);
        }
    }

    private void kbtnClose_Click(object sender, EventArgs e) => Close();
}
