#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class ErrorProviderTest : KryptonForm
{
    public ErrorProviderTest()
    {
        InitializeComponent();
        InitializeErrorProvider();
    }

    private void InitializeErrorProvider()
    {
        kryptonErrorProvider1.ContainerControl = this;
        kryptonErrorProvider1.BlinkStyle = KryptonErrorBlinkStyle.BlinkIfDifferentError;
        
        // Configure Krypton tooltip settings
        // Tooltips will automatically appear when hovering over error icons
        // The tooltips use Krypton styling and can be customized via ToolTipValues
        kryptonErrorProvider1.ToolTipValues.EnableToolTips = true;
        kryptonErrorProvider1.ToolTipValues.ToolTipStyle = LabelStyle.ToolTip;
        kryptonErrorProvider1.ToolTipValues.ToolTipShadow = true;
        kryptonErrorProvider1.ToolTipValues.ShowIntervalDelay = 500;
        kryptonErrorProvider1.ToolTipValues.CloseIntervalDelay = 5000;
        
        UpdateBlinkStyleDisplay();
        UpdateIconAlignmentDisplay();
        UpdateIconPaddingDisplay();
    }

    private void UpdateBlinkStyleDisplay()
    {
        klblBlinkStyleValue.Text = kryptonErrorProvider1.BlinkStyle.ToString();
    }

    private void UpdateIconAlignmentDisplay()
    {
        klblIconAlignmentValue.Text = kryptonErrorProvider1.IconAlignment.ToString();
    }

    private void UpdateIconPaddingDisplay()
    {
        knudIconPadding.Value = kryptonErrorProvider1.IconPadding;
    }

    private void kbtnSetError_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ktxtErrorText.Text))
        {
            kryptonErrorProvider1.SetError(ktxtTestControl, "This is a test error message");
        }
        else
        {
            kryptonErrorProvider1.SetError(ktxtTestControl, ktxtErrorText.Text);
        }
        
        UpdateErrorInfo();
    }

    private void kbtnClearError_Click(object sender, EventArgs e)
    {
        kryptonErrorProvider1.SetError(ktxtTestControl, string.Empty);
        UpdateErrorInfo();
    }

    private void kbtnClearAll_Click(object sender, EventArgs e)
    {
        kryptonErrorProvider1.Clear();
        UpdateErrorInfo();
    }

    private void kcmbBlinkStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (kcmbBlinkStyle.SelectedItem != null)
        {
            var selectedStyle = (KryptonErrorBlinkStyle)kcmbBlinkStyle.SelectedItem;
            kryptonErrorProvider1.BlinkStyle = selectedStyle;
            UpdateBlinkStyleDisplay();
        }
    }

    private void kcmbIconAlignment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (kcmbIconAlignment.SelectedItem != null)
        {
            var selectedAlignment = (KryptonErrorIconAlignment)kcmbIconAlignment.SelectedItem;
            kryptonErrorProvider1.IconAlignment = selectedAlignment;
            
            if (kryptonErrorProvider1.GetError(ktxtTestControl) != string.Empty)
            {
                kryptonErrorProvider1.SetIconAlignment(ktxtTestControl, selectedAlignment);
            }
            
            UpdateIconAlignmentDisplay();
        }
    }

    private void knudIconPadding_ValueChanged(object sender, EventArgs e)
    {
        int padding = (int)knudIconPadding.Value;
        kryptonErrorProvider1.IconPadding = padding;
        
        if (kryptonErrorProvider1.GetError(ktxtTestControl) != string.Empty)
        {
            kryptonErrorProvider1.SetIconPadding(ktxtTestControl, padding);
        }
        
        UpdateIconPaddingDisplay();
    }

    private void kbtnSetCustomIcon_Click(object sender, EventArgs e)
    {
        kryptonErrorProvider1.Icon = SystemIcons.Warning;
    }

    private void kbtnSetDefaultIcon_Click(object sender, EventArgs e)
    {
        kryptonErrorProvider1.Icon = SystemIcons.Error;
    }

    private void kbtnSetInfoIcon_Click(object sender, EventArgs e)
    {
        kryptonErrorProvider1.Icon = SystemIcons.Information;
    }

    private void kbtnSetQuestionIcon_Click(object sender, EventArgs e)
    {
        kryptonErrorProvider1.Icon = SystemIcons.Question;
    }

    private void kbtnSetIconSize16_Click(object sender, EventArgs e)
    {
        kryptonErrorProvider1.IconSize = new Size(16, 16);
    }

    private void kbtnSetIconSize32_Click(object sender, EventArgs e)
    {
        kryptonErrorProvider1.IconSize = new Size(32, 32);
    }

    private void kbtnSetIconSize48_Click(object sender, EventArgs e)
    {
        kryptonErrorProvider1.IconSize = new Size(48, 48);
    }

    private void kcmbPaletteMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (kcmbPaletteMode.SelectedItem != null)
        {
            var selectedMode = (PaletteMode)kcmbPaletteMode.SelectedItem;
            kryptonErrorProvider1.PaletteMode = selectedMode;
        }
    }

    private void kbtnSetMultipleErrors_Click(object sender, EventArgs e)
    {
        kryptonErrorProvider1.SetError(ktxtTestControl, "Error on test control");
        kryptonErrorProvider1.SetError(ktxtEmail, "Invalid email format");
        kryptonErrorProvider1.SetError(ktxtPhone, "Phone number required");
        kryptonErrorProvider1.SetError(ktxtAge, "Age must be between 18 and 100");
        kryptonErrorProvider1.SetError(kcbRequired, "This field is required");
        
        UpdateErrorInfo();
    }

    private void kbtnValidateForm_Click(object sender, EventArgs e)
    {
        bool hasErrors = false;
        
        if (string.IsNullOrWhiteSpace(ktxtEmail.Text))
        {
            kryptonErrorProvider1.SetError(ktxtEmail, "Email is required");
            hasErrors = true;
        }
        else if (!ktxtEmail.Text.Contains("@"))
        {
            kryptonErrorProvider1.SetError(ktxtEmail, "Invalid email format");
            hasErrors = true;
        }
        else
        {
            kryptonErrorProvider1.SetError(ktxtEmail, string.Empty);
        }
        
        if (string.IsNullOrWhiteSpace(ktxtPhone.Text))
        {
            kryptonErrorProvider1.SetError(ktxtPhone, "Phone number is required");
            hasErrors = true;
        }
        else
        {
            kryptonErrorProvider1.SetError(ktxtPhone, string.Empty);
        }
        
        if (string.IsNullOrWhiteSpace(ktxtAge.Text) || !int.TryParse(ktxtAge.Text, out int age) || age < 18 || age > 100)
        {
            kryptonErrorProvider1.SetError(ktxtAge, "Age must be a number between 18 and 100");
            hasErrors = true;
        }
        else
        {
            kryptonErrorProvider1.SetError(ktxtAge, string.Empty);
        }
        
        if (!kcbRequired.Checked)
        {
            kryptonErrorProvider1.SetError(kcbRequired, "You must accept the terms");
            hasErrors = true;
        }
        else
        {
            kryptonErrorProvider1.SetError(kcbRequired, string.Empty);
        }
        
        UpdateErrorInfo();
        
        if (!hasErrors)
        {
            KryptonMessageBox.Show(this, "Validation passed!", "Success", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        }
    }

    private void ktxtTestControl_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ktxtTestControl.Text))
        {
            kryptonErrorProvider1.SetError(ktxtTestControl, "This field cannot be empty");
            e.Cancel = true;
        }
        else
        {
            kryptonErrorProvider1.SetError(ktxtTestControl, string.Empty);
        }
        
        UpdateErrorInfo();
    }

    private void ktxtAge_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ktxtAge.Text) || !int.TryParse(ktxtAge.Text, out int age) || age < 18 || age > 100)
        {
            kryptonErrorProvider1.SetError(ktxtAge, "Age must be a number between 18 and 100");
            e.Cancel = true;
        }
        else
        {
            kryptonErrorProvider1.SetError(ktxtAge, string.Empty);
        }
        
        UpdateErrorInfo();
    }

    private void ktxtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ktxtEmail.Text) && !ktxtEmail.Text.Contains("@"))
        {
            kryptonErrorProvider1.SetError(ktxtEmail, "Invalid email format");
        }
        else
        {
            kryptonErrorProvider1.SetError(ktxtEmail, string.Empty);
        }
        
        UpdateErrorInfo();
    }

    private void UpdateErrorInfo()
    {
        string errorText = kryptonErrorProvider1.GetError(ktxtTestControl);
        klblErrorTextValue.Text = string.IsNullOrEmpty(errorText) ? "(none)" : errorText;
        
        var alignment = kryptonErrorProvider1.GetIconAlignment(ktxtTestControl);
        klblCurrentAlignmentValue.Text = alignment.ToString();
        
        var padding = kryptonErrorProvider1.GetIconPadding(ktxtTestControl);
        klblCurrentPaddingValue.Text = padding.ToString();
        
        int errorCount = 0;
        foreach (Control control in kpnlValidation.Controls)
        {
            if (!string.IsNullOrEmpty(kryptonErrorProvider1.GetError(control)))
            {
                errorCount++;
            }
        }
        
        klblErrorCountValue.Text = errorCount.ToString();
    }

    private void kbtnSetErrorWithAlignment_Click(object sender, EventArgs e)
    {
        if (kcmbIconAlignment.SelectedItem != null)
        {
            var alignment = (KryptonErrorIconAlignment)kcmbIconAlignment.SelectedItem;
            string errorText = string.IsNullOrWhiteSpace(ktxtErrorText.Text) 
                ? "Error with custom alignment" 
                : ktxtErrorText.Text;
            
            kryptonErrorProvider1.SetError(ktxtTestControl, errorText, alignment);
            UpdateErrorInfo();
        }
    }

    private void kbtnDemoAllAlignments_Click(object sender, EventArgs e)
    {
        kryptonErrorProvider1.SetError(ktxtTopLeft, "Top Left", KryptonErrorIconAlignment.TopLeft);
        kryptonErrorProvider1.SetError(ktxtTopRight, "Top Right", KryptonErrorIconAlignment.TopRight);
        kryptonErrorProvider1.SetError(ktxtMiddleLeft, "Middle Left", KryptonErrorIconAlignment.MiddleLeft);
        kryptonErrorProvider1.SetError(ktxtMiddleRight, "Middle Right", KryptonErrorIconAlignment.MiddleRight);
        kryptonErrorProvider1.SetError(ktxtBottomLeft, "Bottom Left", KryptonErrorIconAlignment.BottomLeft);
        kryptonErrorProvider1.SetError(ktxtBottomRight, "Bottom Right", KryptonErrorIconAlignment.BottomRight);
    }

    private void kbtnClearAlignmentDemo_Click(object sender, EventArgs e)
    {
        kryptonErrorProvider1.SetError(ktxtTopLeft, string.Empty);
        kryptonErrorProvider1.SetError(ktxtTopRight, string.Empty);
        kryptonErrorProvider1.SetError(ktxtMiddleLeft, string.Empty);
        kryptonErrorProvider1.SetError(ktxtMiddleRight, string.Empty);
        kryptonErrorProvider1.SetError(ktxtBottomLeft, string.Empty);
        kryptonErrorProvider1.SetError(ktxtBottomRight, string.Empty);
    }
}

