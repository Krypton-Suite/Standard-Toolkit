#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class RibbonTest : KryptonForm
{
    public RibbonTest()
    {
        InitializeComponent();
        SetupBackstagePages();
    }

    private void krgbtnTest1715_Click(object sender, EventArgs e)
    {
        kryptonRibbon.SelectedTab!.ContextName = @"Testing";
    }

    private void SetupBackstagePages()
    {
        // Create some sample backstage pages using the Add method that takes string
        kryptonRibbon.BackstagePages.Add("New").TextTitle = "Create a new document";
        kryptonRibbon.BackstagePages.Add("Open").TextTitle = "Open an existing document";
        kryptonRibbon.BackstagePages.Add("Save").TextTitle = "Save your document";
        kryptonRibbon.BackstagePages.Add("Settings").TextTitle = "Application Settings";

        // For now, just add a simple menu item to test backstage
        // The backstage can be accessed through the app button menu
    }
}