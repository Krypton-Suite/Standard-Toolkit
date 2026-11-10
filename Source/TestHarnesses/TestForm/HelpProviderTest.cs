#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class HelpProviderTest : KryptonForm
{
    public HelpProviderTest()
    {
        InitializeComponent();
        InitializeHelpProvider();
    }

    private void InitializeHelpProvider()
    {
        // Configure the help provider
        kryptonHelpProvider1.HelpNamespace = string.Empty; // Empty for pop-up help strings
        
        // Set container control for tooltip functionality
        kryptonHelpProvider1.ContainerControl = this;
        
        // Configure Krypton tooltip settings
        kryptonHelpProvider1.ToolTipValues.EnableToolTips = true;
        kryptonHelpProvider1.ToolTipValues.ToolTipStyle = LabelStyle.ToolTip;
        kryptonHelpProvider1.ToolTipValues.ToolTipShadow = true;
        kryptonHelpProvider1.ToolTipValues.ShowIntervalDelay = 500;
        kryptonHelpProvider1.ToolTipValues.CloseIntervalDelay = 5000;
        
        // Set up pop-up help strings for various controls
        kryptonHelpProvider1.SetShowHelp(kbtnTestButton, true);
        kryptonHelpProvider1.SetHelpString(kbtnTestButton, "This is a test button. Click it to perform a test action.");
        
        kryptonHelpProvider1.SetShowHelp(ktxtName, true);
        kryptonHelpProvider1.SetHelpString(ktxtName, "Enter your full name in this field. This is a required field.");
        
        kryptonHelpProvider1.SetShowHelp(ktxtEmail, true);
        kryptonHelpProvider1.SetHelpString(ktxtEmail, "Enter your email address. Must be in a valid email format (e.g., user@example.com).");
        
        kryptonHelpProvider1.SetShowHelp(ktxtPhone, true);
        kryptonHelpProvider1.SetHelpString(ktxtPhone, "Enter your phone number. Include area code if applicable.");
        
        kryptonHelpProvider1.SetShowHelp(kcmbCountry, true);
        kryptonHelpProvider1.SetHelpString(kcmbCountry, "Select your country from the dropdown list.");
        
        kryptonHelpProvider1.SetShowHelp(kchkAgree, true);
        kryptonHelpProvider1.SetHelpString(kchkAgree, "Check this box to agree to the terms and conditions.");
        
        kryptonHelpProvider1.SetShowHelp(knudAge, true);
        kryptonHelpProvider1.SetHelpString(knudAge, "Enter your age. Must be between 18 and 120.");
        
        kryptonHelpProvider1.SetShowHelp(kdtpBirthDate, true);
        kryptonHelpProvider1.SetHelpString(kdtpBirthDate, "Select your date of birth from the calendar.");
        
        kryptonHelpProvider1.SetShowHelp(krtbComments, true);
        kryptonHelpProvider1.SetHelpString(krtbComments, "Enter any additional comments or notes in this text area.");
        
        // Set up help with keywords for HTML help (if HelpNamespace is set)
        kryptonHelpProvider1.SetShowHelp(kbtnHtmlHelp, true);
        kryptonHelpProvider1.SetHelpKeyword(kbtnHtmlHelp, "html_help_button");
        kryptonHelpProvider1.SetHelpNavigator(kbtnHtmlHelp, HelpNavigator.Topic);
        
        // Set up help with different navigators
        kryptonHelpProvider1.SetShowHelp(kbtnTableOfContents, true);
        kryptonHelpProvider1.SetHelpKeyword(kbtnTableOfContents, "");
        kryptonHelpProvider1.SetHelpNavigator(kbtnTableOfContents, HelpNavigator.TableOfContents);
        
        kryptonHelpProvider1.SetShowHelp(kbtnIndex, true);
        kryptonHelpProvider1.SetHelpKeyword(kbtnIndex, "");
        kryptonHelpProvider1.SetHelpNavigator(kbtnIndex, HelpNavigator.Index);
        
        kryptonHelpProvider1.SetShowHelp(kbtnFind, true);
        kryptonHelpProvider1.SetHelpKeyword(kbtnFind, "");
        kryptonHelpProvider1.SetHelpNavigator(kbtnFind, HelpNavigator.Find);
        
        // Hook up the HelpRequested event
        kryptonHelpProvider1.HelpRequested += KryptonHelpProvider1_HelpRequested;
        
        // Initialize display
        UpdateHelpInfo();
        UpdatePaletteModeDisplay();
    }

    private void KryptonHelpProvider1_HelpRequested(object? sender, HelpEventArgs hlpevent)
    {
        // Custom help handling - you can override the default behavior here
        // The sender is the HelpProvider, so we need to find the control that has focus
        Control? control = ActiveControl;
        
        if (control != null)
        {
            string? helpString = kryptonHelpProvider1.GetHelpString(control);
            
            if (!string.IsNullOrEmpty(helpString))
            {
                // Show custom help dialog instead of default popup
                KryptonMessageBox.Show(this, 
                    helpString, 
                    $"Help: {control.Name}", 
                    KryptonMessageBoxButtons.OK, 
                    KryptonMessageBoxIcon.Information);
                
                hlpevent.Handled = true; // Mark as handled to prevent default behavior
            }
        }
        
        // Log the help request
        AddLogEntry($"Help requested for: {control?.GetType().Name ?? "Unknown"}");
    }

    private void kbtnTestButton_Click(object sender, EventArgs e)
    {
        KryptonMessageBox.Show(this, "Test button clicked!", "Test", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        AddLogEntry("Test button clicked");
    }

    private void kbtnToggleHelp_Click(object sender, EventArgs e)
    {
        bool currentState = kryptonHelpProvider1.GetShowHelp(ktxtName);
        kryptonHelpProvider1.SetShowHelp(ktxtName, !currentState);
        UpdateHelpInfo();
        AddLogEntry($"Help for Name field: {(!currentState ? "Enabled" : "Disabled")}");
    }

    private void kbtnSetHelpNamespace_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "Help Files (*.chm;*.hlp)|*.chm;*.hlp|All Files (*.*)|*.*",
            Title = "Select Help File"
        };
        
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            kryptonHelpProvider1.HelpNamespace = dialog.FileName;
            klblHelpNamespaceValue.Text = dialog.FileName;
            AddLogEntry($"Help namespace set to: {dialog.FileName}");
        }
    }

    private void kbtnClearHelpNamespace_Click(object sender, EventArgs e)
    {
        kryptonHelpProvider1.HelpNamespace = string.Empty;
        klblHelpNamespaceValue.Text = "(none - using pop-up help strings)";
        AddLogEntry("Help namespace cleared");
    }

    private void kbtnGetHelpInfo_Click(object sender, EventArgs e)
    {
        var info = new System.Text.StringBuilder();
        info.AppendLine("=== Help Provider Information ===");
        info.AppendLine($"Help Namespace: {kryptonHelpProvider1.HelpNamespace ?? "(empty)"}");
        info.AppendLine();
        info.AppendLine("Control Help Status:");
        
        foreach (Control control in kpnlControls.Controls)
        {
            if (kryptonHelpProvider1.GetShowHelp(control))
            {
                info.AppendLine($"  {control.Name}:");
                info.AppendLine($"    Help String: {kryptonHelpProvider1.GetHelpString(control) ?? "(none)"}");
                info.AppendLine($"    Help Keyword: {kryptonHelpProvider1.GetHelpKeyword(control) ?? "(none)"}");
                info.AppendLine($"    Help Navigator: {kryptonHelpProvider1.GetHelpNavigator(control)}");
            }
        }
        
        KryptonMessageBox.Show(this, info.ToString(), "Help Information", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
    }

    private void kcmbPaletteMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (kcmbPaletteMode.SelectedItem != null)
        {
            var selectedMode = (PaletteMode)kcmbPaletteMode.SelectedItem;
            kryptonHelpProvider1.PaletteMode = selectedMode;
            UpdatePaletteModeDisplay();
            AddLogEntry($"Palette mode changed to: {selectedMode}");
        }
    }

    private void kbtnUpdateHelpString_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ktxtNewHelpString.Text))
        {
            KryptonMessageBox.Show(this, "Please enter a help string.", "Input Required", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
            return;
        }
        
        kryptonHelpProvider1.SetShowHelp(ktxtName, true);
        kryptonHelpProvider1.SetHelpString(ktxtName, ktxtNewHelpString.Text);
        UpdateHelpInfo();
        AddLogEntry($"Help string updated for Name field: {ktxtNewHelpString.Text}");
    }

    private void kbtnSetHelpKeyword_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ktxtHelpKeyword.Text))
        {
            KryptonMessageBox.Show(this, "Please enter a help keyword.", "Input Required", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
            return;
        }
        
        kryptonHelpProvider1.SetShowHelp(ktxtName, true);
        kryptonHelpProvider1.SetHelpKeyword(ktxtName, ktxtHelpKeyword.Text);
        kryptonHelpProvider1.SetHelpNavigator(ktxtName, HelpNavigator.Topic);
        UpdateHelpInfo();
        AddLogEntry($"Help keyword set for Name field: {ktxtHelpKeyword.Text}");
    }

    private void kbtnClearAllHelp_Click(object sender, EventArgs e)
    {
        foreach (Control control in kpnlControls.Controls)
        {
            kryptonHelpProvider1.SetShowHelp(control, false);
        }
        
        UpdateHelpInfo();
        AddLogEntry("All help cleared for controls");
    }

    private void kbtnRestoreHelp_Click(object sender, EventArgs e)
    {
        InitializeHelpProvider();
        AddLogEntry("Help configuration restored");
    }

    private void UpdateHelpInfo()
    {
        bool showHelp = kryptonHelpProvider1.GetShowHelp(ktxtName);
        string? helpString = kryptonHelpProvider1.GetHelpString(ktxtName);
        string? helpKeyword = kryptonHelpProvider1.GetHelpKeyword(ktxtName);
        HelpNavigator navigator = kryptonHelpProvider1.GetHelpNavigator(ktxtName);
        
        klblShowHelpValue.Text = showHelp ? "Yes" : "No";
        klblHelpStringValue.Text = helpString ?? "(none)";
        klblHelpKeywordValue.Text = helpKeyword ?? "(none)";
        klblHelpNavigatorValue.Text = navigator.ToString();
    }

    private void UpdatePaletteModeDisplay()
    {
        klblPaletteModeValue.Text = kryptonHelpProvider1.PaletteMode.ToString();
    }

    private void AddLogEntry(string message)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        klbLog.Items.Add($"[{timestamp}] {message}");
        klbLog.SelectedIndex = klbLog.Items.Count - 1;
    }

    private void kbtnClearLog_Click(object sender, EventArgs e)
    {
        klbLog.Items.Clear();
    }

    private void kbtnShowHelp_Click(object sender, EventArgs e)
    {
        // Programmatically trigger help for the Name field
        Help.ShowHelp(ktxtName, kryptonHelpProvider1.HelpNamespace, kryptonHelpProvider1.GetHelpNavigator(ktxtName), kryptonHelpProvider1.GetHelpKeyword(ktxtName));
        AddLogEntry("Help shown programmatically for Name field");
    }

    private void kbtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}

