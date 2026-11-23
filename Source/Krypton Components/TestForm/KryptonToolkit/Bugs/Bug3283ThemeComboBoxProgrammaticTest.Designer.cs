#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class Bug3283ThemeComboBoxProgrammaticTest
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
        this.lblInstruction = new Krypton.Toolkit.KryptonWrapLabel();
        this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
        this.kryptonLabelSelection = new Krypton.Toolkit.KryptonLabel();
        this.kryptonLabelGlobalMode = new Krypton.Toolkit.KryptonLabel();
        this.kryptonButtonCycle = new Krypton.Toolkit.KryptonButton();
        this.kryptonButtonJumpLow = new Krypton.Toolkit.KryptonButton();
        this.kryptonButtonJumpHigh = new Krypton.Toolkit.KryptonButton();
        this.kryptonButtonPreHandle = new Krypton.Toolkit.KryptonButton();
        this.kryptonPanelHost = new Krypton.Toolkit.KryptonPanel();
        this.kryptonLabelPreHandleResult = new Krypton.Toolkit.KryptonLabel();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
        this.kryptonPanelMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelHost)).BeginInit();
        this.kryptonPanelHost.SuspendLayout();
        this.SuspendLayout();
        //
        // kryptonPanelMain
        //
        this.kryptonPanelMain.Controls.Add(this.kryptonLabelPreHandleResult);
        this.kryptonPanelMain.Controls.Add(this.kryptonPanelHost);
        this.kryptonPanelMain.Controls.Add(this.kryptonButtonPreHandle);
        this.kryptonPanelMain.Controls.Add(this.kryptonButtonJumpHigh);
        this.kryptonPanelMain.Controls.Add(this.kryptonButtonJumpLow);
        this.kryptonPanelMain.Controls.Add(this.kryptonButtonCycle);
        this.kryptonPanelMain.Controls.Add(this.kryptonLabelGlobalMode);
        this.kryptonPanelMain.Controls.Add(this.kryptonLabelSelection);
        this.kryptonPanelMain.Controls.Add(this.kryptonThemeComboBox1);
        this.kryptonPanelMain.Controls.Add(this.lblInstruction);
        this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
        this.kryptonPanelMain.Name = "kryptonPanelMain";
        this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
        this.kryptonPanelMain.Size = new System.Drawing.Size(600, 330);
        this.kryptonPanelMain.TabIndex = 0;
        //
        // lblInstruction
        //
        this.lblInstruction.AutoSize = false;
        this.lblInstruction.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblInstruction.Location = new System.Drawing.Point(12, 12);
        this.lblInstruction.Name = "lblInstruction";
        this.lblInstruction.Size = new System.Drawing.Size(576, 56);
        this.lblInstruction.Text = "Issue #3283: Use the buttons to change SelectedIndex in code. The global palette (see second line) must follow the combo selection. You can still change the theme from the drop-down as usual.";
        //
        // kryptonThemeComboBox1
        //
        this.kryptonThemeComboBox1.DropDownWidth = 400;
        this.kryptonThemeComboBox1.IntegralHeight = false;
        this.kryptonThemeComboBox1.Location = new System.Drawing.Point(12, 74);
        this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
        this.kryptonThemeComboBox1.Size = new System.Drawing.Size(400, 22);
        this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
        this.kryptonThemeComboBox1.TabIndex = 0;
        //
        // kryptonLabelSelection
        //
        this.kryptonLabelSelection.Location = new System.Drawing.Point(12, 104);
        this.kryptonLabelSelection.Name = "kryptonLabelSelection";
        this.kryptonLabelSelection.Size = new System.Drawing.Size(576, 24);
        this.kryptonLabelSelection.TabIndex = 1;
        this.kryptonLabelSelection.Values.Text = "SelectedIndex";
        //
        // kryptonLabelGlobalMode
        //
        this.kryptonLabelGlobalMode.Location = new System.Drawing.Point(12, 130);
        this.kryptonLabelGlobalMode.Name = "kryptonLabelGlobalMode";
        this.kryptonLabelGlobalMode.Size = new System.Drawing.Size(576, 24);
        this.kryptonLabelGlobalMode.TabIndex = 2;
        this.kryptonLabelGlobalMode.Values.Text = "Global palette";
        //
        // kryptonButtonCycle
        //
        this.kryptonButtonCycle.Location = new System.Drawing.Point(12, 164);
        this.kryptonButtonCycle.Name = "kryptonButtonCycle";
        this.kryptonButtonCycle.Size = new System.Drawing.Size(200, 32);
        this.kryptonButtonCycle.TabIndex = 3;
        this.kryptonButtonCycle.Values.Text = "Cycle SelectedIndex (+1)";
        //
        // kryptonButtonJumpLow
        //
        this.kryptonButtonJumpLow.Location = new System.Drawing.Point(220, 164);
        this.kryptonButtonJumpLow.Name = "kryptonButtonJumpLow";
        this.kryptonButtonJumpLow.Size = new System.Drawing.Size(170, 32);
        this.kryptonButtonJumpLow.TabIndex = 4;
        this.kryptonButtonJumpLow.Values.Text = "Jump to ~25% index";
        //
        // kryptonButtonJumpHigh
        //
        this.kryptonButtonJumpHigh.Location = new System.Drawing.Point(398, 164);
        this.kryptonButtonJumpHigh.Name = "kryptonButtonJumpHigh";
        this.kryptonButtonJumpHigh.Size = new System.Drawing.Size(190, 32);
        this.kryptonButtonJumpHigh.TabIndex = 5;
        this.kryptonButtonJumpHigh.Values.Text = "Jump to ~75% index";
        //
        // kryptonButtonPreHandle
        //
        this.kryptonButtonPreHandle.Location = new System.Drawing.Point(12, 204);
        this.kryptonButtonPreHandle.Name = "kryptonButtonPreHandle";
        this.kryptonButtonPreHandle.Size = new System.Drawing.Size(420, 32);
        this.kryptonButtonPreHandle.TabIndex = 6;
        this.kryptonButtonPreHandle.Values.Text = "Add fresh KryptonThemeComboBox (index set before handle)";
        //
        // kryptonPanelHost
        //
        this.kryptonPanelHost.Location = new System.Drawing.Point(12, 242);
        this.kryptonPanelHost.Name = "kryptonPanelHost";
        this.kryptonPanelHost.Size = new System.Drawing.Size(576, 28);
        this.kryptonPanelHost.TabIndex = 7;
        //
        // kryptonLabelPreHandleResult
        //
        this.kryptonLabelPreHandleResult.Location = new System.Drawing.Point(12, 274);
        this.kryptonLabelPreHandleResult.Name = "kryptonLabelPreHandleResult";
        this.kryptonLabelPreHandleResult.Size = new System.Drawing.Size(576, 44);
        this.kryptonLabelPreHandleResult.TabIndex = 8;
        this.kryptonLabelPreHandleResult.Values.Text = "";
        //
        // Bug3283ThemeComboBoxProgrammaticTest
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(600, 330);
        this.Controls.Add(this.kryptonPanelMain);
        this.Name = "Bug3283ThemeComboBoxProgrammaticTest";
        this.Text = "Issue #3283: KryptonThemeComboBox programmatic";
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
        this.kryptonPanelMain.ResumeLayout(false);
        this.kryptonPanelMain.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelHost)).EndInit();
        this.kryptonPanelHost.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
    private Krypton.Toolkit.KryptonWrapLabel lblInstruction;
    private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
    private Krypton.Toolkit.KryptonLabel kryptonLabelSelection;
    private Krypton.Toolkit.KryptonLabel kryptonLabelGlobalMode;
    private Krypton.Toolkit.KryptonButton kryptonButtonCycle;
    private Krypton.Toolkit.KryptonButton kryptonButtonJumpLow;
    private Krypton.Toolkit.KryptonButton kryptonButtonJumpHigh;
    private Krypton.Toolkit.KryptonButton kryptonButtonPreHandle;
    private Krypton.Toolkit.KryptonPanel kryptonPanelHost;
    private Krypton.Toolkit.KryptonLabel kryptonLabelPreHandleResult;
}
