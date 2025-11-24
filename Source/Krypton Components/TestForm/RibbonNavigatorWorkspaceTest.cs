#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

using Krypton.Navigator;
using Krypton.Workspace;

namespace TestForm;

public partial class RibbonNavigatorWorkspaceTest : KryptonForm
{
    KryptonManager _manager = new KryptonManager();

    public RibbonNavigatorWorkspaceTest()
    {
        InitializeComponent();
    }

    private void buttonSpecExpandCollapse_Click(object sender, EventArgs e)
    {
        // Are we currently showing fully expanded?
        if (navigatorOutlook.NavigatorMode == NavigatorMode.OutlookFull)
        {
            // Switch to mini mode and reverse direction of arrow
            navigatorOutlook.NavigatorMode = NavigatorMode.OutlookMini;
            buttonSpecExpandCollapse.TypeRestricted = PaletteNavButtonSpecStyle.ArrowRight;
        }
        else
        {
            // Switch to full mode and reverse direction of arrow
            navigatorOutlook.NavigatorMode = NavigatorMode.OutlookFull;
            buttonSpecExpandCollapse.TypeRestricted = PaletteNavButtonSpecStyle.ArrowLeft;
        }
    }

    private void radioOffice2010Blue_CheckedChanged(object sender, EventArgs e)
    {
        if (radioOffice2010Blue.Checked)
        {
            navigatorOutlook.DismissPopups();
            _manager.GlobalPaletteMode = PaletteMode.Office2010Blue;
        }
    }

    private void radioOffice2010Silver_CheckedChanged(object sender, EventArgs e)
    {
        if (radioOffice2010Silver.Checked)
        {
            navigatorOutlook.DismissPopups();
            _manager.GlobalPaletteMode = PaletteMode.Office2010Silver;
        }
    }

    private void radioOffice2010Black_CheckedChanged(object sender, EventArgs e)
    {
        if (radioOffice2010Black.Checked)
        {
            navigatorOutlook.DismissPopups();
            _manager.GlobalPaletteMode = PaletteMode.Office2010Black;
        }
    }

    private void radioOffice2007Blue_CheckedChanged(object sender, EventArgs e)
    {
        if (radioOffice2007Blue.Checked)
        {
            navigatorOutlook.DismissPopups();
            _manager.GlobalPaletteMode = PaletteMode.Office2007Blue;
        }
    }

    private void radioOffice2007Silver_CheckedChanged(object sender, EventArgs e)
    {
        if (radioOffice2007Silver.Checked)
        {
            navigatorOutlook.DismissPopups();
            _manager.GlobalPaletteMode = PaletteMode.Office2007Silver;
        }
    }

    private void radioOffice2007Black_CheckedChanged(object sender, EventArgs e)
    {
        if (radioOffice2007Black.Checked)
        {
            navigatorOutlook.DismissPopups();
            _manager.GlobalPaletteMode = PaletteMode.Office2007Black;
        }
    }

    private void radioOffice2003_CheckedChanged(object sender, EventArgs e)
    {
        if (radioOffice2003.Checked)
        {
            navigatorOutlook.DismissPopups();
            _manager.GlobalPaletteMode = PaletteMode.ProfessionalOffice2003;
        }
    }

    private void radioSparkleBlue_CheckedChanged(object sender, EventArgs e)
    {
        if (radioSparkleBlue.Checked)
        {
            navigatorOutlook.DismissPopups();
            _manager.GlobalPaletteMode = PaletteMode.SparkleBlue;
        }
    }

    private void radioSparkleOrange_CheckedChanged(object sender, EventArgs e)
    {
        if (radioSparkleOrange.Checked)
        {
            navigatorOutlook.DismissPopups();
            _manager.GlobalPaletteMode = PaletteMode.SparkleOrange;
        }
    }

    private void radioSparklePurple_CheckedChanged(object sender, EventArgs e)
    {
        if (radioSparklePurple.Checked)
        {
            navigatorOutlook.DismissPopups();
            _manager.GlobalPaletteMode = PaletteMode.SparklePurple;
        }
    }

    private void radioSystem_CheckedChanged(object sender, EventArgs e)
    {
        if (radioSystem.Checked)
        {
            navigatorOutlook.DismissPopups();
            _manager.GlobalPaletteMode = PaletteMode.ProfessionalSystem;
        }
    }

    private void exit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void buttonTabs_Click(object sender, EventArgs e)
    {

    }

    private void buttonRibbonTabs_Click(object sender, EventArgs e)
    {

    }

    private void buttonCheckButtons_Click(object sender, EventArgs e)
    {

    }

    private void buttonHeaderGroup_Click(object sender, EventArgs e)
    {

    }

    private void buttonHeaderBar_Click(object sender, EventArgs e)
    {

    }

    private void buttonStack_Click(object sender, EventArgs e)
    {

    }

    private void kryptonWorkspace_WorkspaceCellAdding(object sender, WorkspaceCellEventArgs e)
    {
        // Remove the close and context menu buttons from the navigator cell
        e.Cell.Button.CloseButtonDisplay = ButtonDisplay.Hide;
        e.Cell.Button.ContextButtonDisplay = ButtonDisplay.Hide;
        e.Cell.Button.CloseButtonAction = CloseButtonAction.None;

        // Update with currently requested mode
        UpdateCell(e.Cell);
    }

    private void UpdateCell(KryptonWorkspaceCell eCell)
    {
        NavigatorMode newMode = NavigatorMode.BarTabGroup;

        if (checkSetDocMode.CheckedButton == buttonTabs)
        {
            newMode = NavigatorMode.BarTabGroup;
        }
        else if (checkSetDocMode.CheckedButton == buttonRibbonTabs)
        {
            newMode = NavigatorMode.BarRibbonTabGroup;
        }
        else if (checkSetDocMode.CheckedButton == buttonCheckButtons)
        {
            newMode = NavigatorMode.BarCheckButtonGroupOutside;
        }
        else if (checkSetDocMode.CheckedButton == buttonHeaderGroup)
        {
            newMode = NavigatorMode.HeaderGroup;
        }
        else if (checkSetDocMode.CheckedButton == buttonHeaderBar)
        {
            newMode = NavigatorMode.HeaderBarCheckButtonHeaderGroup;
        }
        else if (checkSetDocMode.CheckedButton == buttonStack)
        {
            newMode = NavigatorMode.StackCheckButtonGroup;
        }

        eCell.NavigatorMode = newMode;

        // Set mode specific properties
        switch (newMode)
        {
            case NavigatorMode.BarRibbonTabGroup:
            case NavigatorMode.BarRibbonTabOnly:
                eCell.PageBackStyle = PaletteBackStyle.ControlRibbon;
                eCell.Group.GroupBackStyle = PaletteBackStyle.ControlRibbon;
                eCell.Group.GroupBorderStyle = PaletteBorderStyle.ControlRibbon;
                break;
            default:
                eCell.PageBackStyle = PaletteBackStyle.PanelClient;
                eCell.Group.GroupBackStyle = PaletteBackStyle.PanelClient;
                eCell.Group.GroupBorderStyle = PaletteBorderStyle.ControlClient;
                break;
        }
    }

    private void checkSetDocMode_CheckedButtonChanged(object sender, EventArgs e)
    {
        // Kill any showing outlook popup page
        navigatorOutlook.DismissPopups();

        // Update each workspace cell
        KryptonWorkspaceCell? cell = kryptonWorkspace.FirstCell();
        while (cell != null)
        {
            UpdateCell(cell);
            cell = kryptonWorkspace.NextCell(cell);
        }
    }
}