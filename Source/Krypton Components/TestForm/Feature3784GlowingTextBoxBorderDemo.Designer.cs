#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Feature3784GlowingTextBoxBorderDemo
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
            this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.klblAnimatedHeader = new Krypton.Toolkit.KryptonLabel();
            this.ktxtAnimatedGlow = new Krypton.Toolkit.KryptonTextBox();
            this.klblStaticHeader = new Krypton.Toolkit.KryptonLabel();
            this.ktxtStaticGlow = new Krypton.Toolkit.KryptonTextBox();
            this.klblComboHeader = new Krypton.Toolkit.KryptonLabel();
            this.kcmbGlow = new Krypton.Toolkit.KryptonComboBox();
            this.klblRichTextHeader = new Krypton.Toolkit.KryptonLabel();
            this.krtbGlow = new Krypton.Toolkit.KryptonRichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            //
            // kwlblInfo
            //
            this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.kwlblInfo.Padding = new System.Windows.Forms.Padding(12, 12, 12, 8);
            this.kwlblInfo.Text =
                @"Issue #3784: Optional glowing border on KryptonTextBox, KryptonComboBox, and KryptonRichTextBox. " +
                @"Controls use the active theme colours; configure glow via GlowingBorderValues. Tab into each control to compare.";
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.Controls.Add(this.tableLayoutPanelMain);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(720, 420);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // tableLayoutPanelMain
            //
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.Controls.Add(this.klblAnimatedHeader, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.ktxtAnimatedGlow, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.klblStaticHeader, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.ktxtStaticGlow, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.klblComboHeader, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.kcmbGlow, 0, 5);
            this.tableLayoutPanelMain.Controls.Add(this.klblRichTextHeader, 0, 6);
            this.tableLayoutPanelMain.Controls.Add(this.krtbGlow, 0, 7);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 8;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(696, 396);
            this.tableLayoutPanelMain.TabIndex = 0;
            //
            // klblAnimatedHeader
            //
            this.klblAnimatedHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblAnimatedHeader.Location = new System.Drawing.Point(3, 0);
            this.klblAnimatedHeader.Name = "klblAnimatedHeader";
            this.klblAnimatedHeader.Size = new System.Drawing.Size(690, 20);
            this.klblAnimatedHeader.TabIndex = 0;
            this.klblAnimatedHeader.Text = @"KryptonTextBox — animated glow around full border";
            //
            // ktxtAnimatedGlow
            //
            this.ktxtAnimatedGlow.AlwaysActive = false;
            this.ktxtAnimatedGlow.CueHint.CueHintText = "Describe the app or website or idea that you want";
            this.ktxtAnimatedGlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtAnimatedGlow.GlowingBorderValues.Enable = true;
            this.ktxtAnimatedGlow.GlowingBorderValues.ShowWhen = Krypton.Toolkit.InputGlowingBorderShowWhen.Focused;
            this.ktxtAnimatedGlow.GlowingBorderValues.Style = Krypton.Toolkit.InputGlowingBorderStyle.All;
            this.ktxtAnimatedGlow.GlowingBorderValues.AnimationSpeed = 1.5F;
            this.ktxtAnimatedGlow.Location = new System.Drawing.Point(3, 23);
            this.ktxtAnimatedGlow.Name = "ktxtAnimatedGlow";
            this.ktxtAnimatedGlow.Size = new System.Drawing.Size(690, 30);
            this.ktxtAnimatedGlow.TabIndex = 1;
            //
            // klblStaticHeader
            //
            this.klblStaticHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblStaticHeader.Location = new System.Drawing.Point(3, 59);
            this.klblStaticHeader.Name = "klblStaticHeader";
            this.klblStaticHeader.Size = new System.Drawing.Size(690, 20);
            this.klblStaticHeader.TabIndex = 2;
            this.klblStaticHeader.Text = @"KryptonTextBox — static bottom-edge glow";
            //
            // ktxtStaticGlow
            //
            this.ktxtStaticGlow.AlwaysActive = false;
            this.ktxtStaticGlow.CueHint.CueHintText = "Static glow while focused";
            this.ktxtStaticGlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtStaticGlow.GlowingBorderValues.Animate = false;
            this.ktxtStaticGlow.GlowingBorderValues.Enable = true;
            this.ktxtStaticGlow.GlowingBorderValues.ShowWhen = Krypton.Toolkit.InputGlowingBorderShowWhen.Focused;
            this.ktxtStaticGlow.Location = new System.Drawing.Point(3, 82);
            this.ktxtStaticGlow.Name = "ktxtStaticGlow";
            this.ktxtStaticGlow.Size = new System.Drawing.Size(690, 30);
            this.ktxtStaticGlow.TabIndex = 3;
            //
            // klblComboHeader
            //
            this.klblComboHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblComboHeader.Location = new System.Drawing.Point(3, 118);
            this.klblComboHeader.Name = "klblComboHeader";
            this.klblComboHeader.Size = new System.Drawing.Size(690, 20);
            this.klblComboHeader.TabIndex = 4;
            this.klblComboHeader.Text = @"KryptonComboBox — animated glow";
            //
            // kcmbGlow
            //
            this.kcmbGlow.AlwaysActive = false;
            this.kcmbGlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbGlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbGlow.GlowingBorderValues.AnimationSpeed = 0.5F;
            this.kcmbGlow.GlowingBorderValues.Enable = true;
            this.kcmbGlow.Items.AddRange(new object[] {
            "Option Alpha",
            "Option Beta",
            "Option Gamma"});
            this.kcmbGlow.Location = new System.Drawing.Point(3, 141);
            this.kcmbGlow.Name = "kcmbGlow";
            this.kcmbGlow.Size = new System.Drawing.Size(690, 30);
            this.kcmbGlow.TabIndex = 5;
            //
            // klblRichTextHeader
            //
            this.klblRichTextHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblRichTextHeader.Location = new System.Drawing.Point(3, 177);
            this.klblRichTextHeader.Name = "klblRichTextHeader";
            this.klblRichTextHeader.Size = new System.Drawing.Size(690, 20);
            this.klblRichTextHeader.TabIndex = 6;
            this.klblRichTextHeader.Text = @"KryptonRichTextBox — animated glow";
            //
            // krtbGlow
            //
            this.krtbGlow.AlwaysActive = false;
            this.krtbGlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbGlow.GlowingBorderValues.Enable = true;
            this.krtbGlow.Location = new System.Drawing.Point(3, 200);
            this.krtbGlow.Name = "krtbGlow";
            this.krtbGlow.Size = new System.Drawing.Size(690, 66);
            this.krtbGlow.TabIndex = 7;
            this.krtbGlow.Text = "Rich text with glowing border";
            //
            // Feature3784GlowingTextBoxBorderDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 500);
            this.Controls.Add(this.kryptonPanelMain);
            this.Controls.Add(this.kwlblInfo);
            this.MinimumSize = new System.Drawing.Size(560, 420);
            this.Name = "Feature3784GlowingTextBoxBorderDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Feature 3784 - Glowing Input Borders";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private Krypton.Toolkit.KryptonLabel klblAnimatedHeader;
        private Krypton.Toolkit.KryptonTextBox ktxtAnimatedGlow;
        private Krypton.Toolkit.KryptonLabel klblStaticHeader;
        private Krypton.Toolkit.KryptonTextBox ktxtStaticGlow;
        private Krypton.Toolkit.KryptonLabel klblComboHeader;
        private Krypton.Toolkit.KryptonComboBox kcmbGlow;
        private Krypton.Toolkit.KryptonLabel klblRichTextHeader;
        private Krypton.Toolkit.KryptonRichTextBox krtbGlow;
    }
}
