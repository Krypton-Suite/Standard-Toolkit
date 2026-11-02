#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class CommandLinkButtons : KryptonForm
{
    public CommandLinkButtons()
    {
        InitializeComponent();
    }

    private void kcmdElevated_ElevatedClick(object sender, EventArgs e)
    {
        bool administrator;
        using (System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent())
        {
            administrator = new System.Security.Principal.WindowsPrincipal(identity)
                .IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        AppendElevatedLog(administrator
            ? "ElevatedClick (already Administrator)"
            : "ElevatedClick (RunAs launch requested)");
    }

    private void kcmdElevated_Click(object sender, EventArgs e) =>
        AppendElevatedLog("Click");

    private void kbtnTryHideShield_Click(object sender, EventArgs e)
    {
        kcmdElevated.UACShieldIcon.DisplayUACShield = false;
        kcmdElevated.CommandLinkTextValues.UseDefaultImage = false;

        bool shieldStillShown = (kcmdElevated.UACShieldIcon.DisplayUACShield
                                 && kcmdElevated.UACShieldIcon.Image != null)
                                || kcmdElevated.CommandLinkTextValues.GetOverlayImage(PaletteState.Normal) != null;
        AppendElevatedLog(shieldStillShown
            ? "Hide ignored: shield still shown."
            : "Hide changed the shield.");
    }

    private void AppendElevatedLog(string line) =>
        ktxtElevatedLog.AppendText(line + Environment.NewLine);
}