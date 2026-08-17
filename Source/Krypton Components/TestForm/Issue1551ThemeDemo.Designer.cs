#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Issue1551ThemeDemo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support — do not modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            kryptonManager1 = new KryptonManager(components);
            kwlblInfo = new KryptonWrapLabel();
            kryptonPanelMain = new KryptonPanel();
            khgButtons = new KryptonHeaderGroup();
            tlpMain = new TableLayoutPanel();
            kbtnStandalone = new KryptonButton();
            kbtnCommand = new KryptonButton();
            kchkSelected = new KryptonCheckButton();
            kbtnDisabled = new KryptonButton();
            flpActions = new FlowLayoutPanel();
            klblFamily = new KryptonLabel();
            kcmbFamily = new KryptonComboBox();
            kbtnApply = new KryptonButton();
            kbtnResetTheme = new KryptonButton();
            kbtnExport = new KryptonButton();
            klblStatus = new KryptonLabel();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)kryptonPanelMain).BeginInit();
            kryptonPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)khgButtons).BeginInit();
            ((System.ComponentModel.ISupportInitialize)khgButtons.Panel).BeginInit();
            khgButtons.Panel.SuspendLayout();
            khgButtons.SuspendLayout();
            tlpMain.SuspendLayout();
            flpActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)kcmbFamily).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            //
            // kwlblInfo
            //
            kwlblInfo.AutoSize = false;
            kwlblInfo.Dock = DockStyle.Top;
            kwlblInfo.Height = 100;
            kwlblInfo.Padding = new Padding(12, 12, 12, 8);
            kwlblInfo.Text =
                @"Issue #1551 Materialize Blue, Materialize Light Blue, and Silver Dark Alternate across Office 2007 / 2010 / 2013 / Microsoft 365 / Material — light and dark bases. " +
                @"Same lime button accents; chrome follows the era and light/dark surface." + Environment.NewLine + Environment.NewLine +
                @"Pick a family (also listed in theme selectors as builtin PaletteMode entries), then compare title bar / panels. Reset restores the matching non-lime builtin.";
            //
            // kryptonPanelMain
            //
            kryptonPanelMain.Controls.Add(khgButtons);
            kryptonPanelMain.Controls.Add(flpActions);
            kryptonPanelMain.Controls.Add(klblStatus);
            kryptonPanelMain.Dock = DockStyle.Fill;
            kryptonPanelMain.Location = new Point(0, 0);
            kryptonPanelMain.Name = @"kryptonPanelMain";
            kryptonPanelMain.Padding = new Padding(12);
            kryptonPanelMain.PanelBackStyle = PaletteBackStyle.PanelClient;
            kryptonPanelMain.Size = new Size(760, 400);
            kryptonPanelMain.TabIndex = 0;
            //
            // khgButtons
            //
            khgButtons.Dock = DockStyle.Fill;
            khgButtons.Location = new Point(12, 12);
            khgButtons.Name = @"khgButtons";
            //
            // khgButtons.Panel
            //
            khgButtons.Panel.Controls.Add(tlpMain);
            khgButtons.Size = new Size(736, 260);
            khgButtons.TabIndex = 0;
            khgButtons.ValuesPrimary.Heading = @"Button states";
            khgButtons.ValuesPrimary.Description = @"Standalone · Command · Selected · Disabled";
            //
            // tlpMain
            //
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMain.Controls.Add(kbtnStandalone, 0, 0);
            tlpMain.Controls.Add(kbtnCommand, 1, 0);
            tlpMain.Controls.Add(kchkSelected, 0, 1);
            tlpMain.Controls.Add(kbtnDisabled, 1, 1);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = @"tlpMain";
            tlpMain.RowCount = 2;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpMain.Size = new Size(734, 210);
            tlpMain.TabIndex = 0;
            //
            // kbtnStandalone
            //
            kbtnStandalone.Dock = DockStyle.Fill;
            kbtnStandalone.Location = new Point(8, 8);
            kbtnStandalone.Margin = new Padding(8);
            kbtnStandalone.Name = @"kbtnStandalone";
            kbtnStandalone.Size = new Size(351, 89);
            kbtnStandalone.TabIndex = 0;
            kbtnStandalone.Values.Text = @"&Continue Online";
            //
            // kbtnCommand
            //
            kbtnCommand.ButtonStyle = ButtonStyle.Command;
            kbtnCommand.Dock = DockStyle.Fill;
            kbtnCommand.Location = new Point(375, 8);
            kbtnCommand.Margin = new Padding(8);
            kbtnCommand.Name = @"kbtnCommand";
            kbtnCommand.Size = new Size(351, 89);
            kbtnCommand.TabIndex = 1;
            kbtnCommand.Values.Text = @"Command style";
            //
            // kchkSelected
            //
            kchkSelected.Dock = DockStyle.Fill;
            kchkSelected.Location = new Point(8, 113);
            kchkSelected.Margin = new Padding(8);
            kchkSelected.Name = @"kchkSelected";
            kchkSelected.Size = new Size(351, 89);
            kchkSelected.TabIndex = 2;
            kchkSelected.Values.Text = @"Selected (check)";
            //
            // kbtnDisabled
            //
            kbtnDisabled.Dock = DockStyle.Fill;
            kbtnDisabled.Enabled = false;
            kbtnDisabled.Location = new Point(375, 113);
            kbtnDisabled.Margin = new Padding(8);
            kbtnDisabled.Name = @"kbtnDisabled";
            kbtnDisabled.Size = new Size(351, 89);
            kbtnDisabled.TabIndex = 3;
            kbtnDisabled.Values.Text = @"Disabled";
            //
            // flpActions
            //
            flpActions.AutoSize = true;
            flpActions.Controls.Add(klblFamily);
            flpActions.Controls.Add(kcmbFamily);
            flpActions.Controls.Add(kbtnApply);
            flpActions.Controls.Add(kbtnResetTheme);
            flpActions.Controls.Add(kbtnExport);
            flpActions.Dock = DockStyle.Bottom;
            flpActions.Location = new Point(12, 300);
            flpActions.Name = @"flpActions";
            flpActions.Padding = new Padding(0, 8, 0, 0);
            flpActions.Size = new Size(736, 41);
            flpActions.TabIndex = 1;
            //
            // klblFamily
            //
            klblFamily.Location = new Point(3, 14);
            klblFamily.Name = @"klblFamily";
            klblFamily.Size = new Size(48, 20);
            klblFamily.TabIndex = 0;
            klblFamily.Values.Text = @"Family:";
            //
            // kcmbFamily
            //
            kcmbFamily.DropDownStyle = ComboBoxStyle.DropDownList;
            kcmbFamily.DropDownWidth = 140;
            kcmbFamily.IntegralHeight = false;
            kcmbFamily.Location = new Point(57, 11);
            kcmbFamily.Name = @"kcmbFamily";
            kcmbFamily.Size = new Size(160, 25);
            kcmbFamily.TabIndex = 1;
            kcmbFamily.SelectedIndexChanged += kcmbFamily_SelectedIndexChanged;
            //
            // kbtnApply
            //
            kbtnApply.Location = new Point(203, 11);
            kbtnApply.Name = @"kbtnApply";
            kbtnApply.Size = new Size(140, 28);
            kbtnApply.TabIndex = 2;
            kbtnApply.Values.Text = @"Apply lime theme";
            kbtnApply.Click += kbtnApply_Click;
            //
            // kbtnResetTheme
            //
            kbtnResetTheme.Location = new Point(349, 11);
            kbtnResetTheme.Name = @"kbtnResetTheme";
            kbtnResetTheme.Size = new Size(150, 28);
            kbtnResetTheme.TabIndex = 3;
            kbtnResetTheme.Values.Text = @"Reset to base";
            kbtnResetTheme.Click += kbtnResetTheme_Click;
            //
            // kbtnExport
            //
            kbtnExport.Location = new Point(505, 11);
            kbtnExport.Name = @"kbtnExport";
            kbtnExport.Size = new Size(160, 28);
            kbtnExport.TabIndex = 4;
            kbtnExport.Values.Text = @"Export palette XML…";
            kbtnExport.Click += kbtnExport_Click;
            //
            // klblStatus
            //
            klblStatus.Dock = DockStyle.Bottom;
            klblStatus.Location = new Point(12, 341);
            klblStatus.Name = @"klblStatus";
            klblStatus.Padding = new Padding(0, 4, 0, 0);
            klblStatus.Size = new Size(736, 27);
            klblStatus.TabIndex = 2;
            klblStatus.Values.Text = @"Status";
            //
            // statusStrip
            //
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 500);
            statusStrip.Name = @"statusStrip";
            statusStrip.Size = new Size(760, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = @"statusStrip";
            //
            // statusLabel
            //
            statusLabel.Name = @"statusLabel";
            statusLabel.Size = new Size(42, 17);
            statusLabel.Text = @"Ready";
            //
            // Issue1551ThemeDemo
            //
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 522);
            Controls.Add(kryptonPanelMain);
            Controls.Add(kwlblInfo);
            Controls.Add(statusStrip);
            MinimumSize = new Size(720, 480);
            Name = @"Issue1551ThemeDemo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = @"Lime Green Theme";
            FormClosed += Issue1551ThemeDemo_FormClosed;
            Load += Issue1551ThemeDemo_Load;
            ((System.ComponentModel.ISupportInitialize)kryptonPanelMain).EndInit();
            kryptonPanelMain.ResumeLayout(false);
            kryptonPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)khgButtons.Panel).EndInit();
            khgButtons.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)khgButtons).EndInit();
            khgButtons.ResumeLayout(false);
            tlpMain.ResumeLayout(false);
            flpActions.ResumeLayout(false);
            flpActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)kcmbFamily).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private KryptonManager kryptonManager1;
        private KryptonWrapLabel kwlblInfo;
        private KryptonPanel kryptonPanelMain;
        private KryptonHeaderGroup khgButtons;
        private TableLayoutPanel tlpMain;
        private KryptonButton kbtnStandalone;
        private KryptonButton kbtnCommand;
        private KryptonCheckButton kchkSelected;
        private KryptonButton kbtnDisabled;
        private FlowLayoutPanel flpActions;
        private KryptonLabel klblFamily;
        private KryptonComboBox kcmbFamily;
        private KryptonButton kbtnApply;
        private KryptonButton kbtnResetTheme;
        private KryptonButton kbtnExport;
        private KryptonLabel klblStatus;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
    }
}
