#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit;

namespace AdministratorSuffixTest;

public partial class AdministratorSuffixTest : KryptonForm
{
    public AdministratorSuffixTest()
    {
        InitializeComponent();
    }

    private void AdministratorSuffixTest_Load(object sender, EventArgs e)
    {
        // Set the form title
        Text = "Administrator Suffix Test";
        
        // Initialize the checkbox to match the current setting
        chkShowAdminSuffix.Checked = KryptonManager.ShowAdministratorSuffix;
        
        // Display current administrator status
        lblAdminStatus.Text = IsInAdministratorMode ? "Running as Administrator" : "Running as Normal User";
    }

    private void chkShowAdminSuffix_CheckedChanged(object sender, EventArgs e)
    {
        KryptonManager.ShowAdministratorSuffix = chkShowAdminSuffix.Checked;
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        // Refresh the administrator status
        lblAdminStatus.Text = IsInAdministratorMode ? "Running as Administrator" : "Running as Normal User";
        
        // Force a repaint to update the title bar
        PerformNeedPaint(true);
    }
}
