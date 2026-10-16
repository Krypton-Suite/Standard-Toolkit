#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Bug3451KryptonHeaderGroupPanelDemo
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
            this.khgDemo = new Krypton.Toolkit.KryptonHeaderGroup();
            this.btnHeaderGroupChild = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupDemo = new Krypton.Toolkit.KryptonGroup();
            this.lblGroupChild = new Krypton.Toolkit.KryptonLabel();
            this.kryptonGroupBoxDemo = new Krypton.Toolkit.KryptonGroupBox();
            this.lblGroupBoxChild = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.khgDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.khgDemo.Panel)).BeginInit();
            this.khgDemo.Panel.SuspendLayout();
            this.khgDemo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupDemo.Panel)).BeginInit();
            this.kryptonGroupDemo.Panel.SuspendLayout();
            this.kryptonGroupDemo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxDemo.Panel)).BeginInit();
            this.kryptonGroupBoxDemo.Panel.SuspendLayout();
            this.kryptonGroupBoxDemo.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.Controls.Add(this.kryptonGroupBoxDemo);
            this.kryptonPanelMain.Controls.Add(this.kryptonGroupDemo);
            this.kryptonPanelMain.Controls.Add(this.khgDemo);
            this.kryptonPanelMain.Controls.Add(this.lblInstruction);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(684, 461);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // lblInstruction
            //
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Location = new System.Drawing.Point(15, 15);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(640, 57);
            this.lblInstruction.Text = "Issue #3451: At design time, drop new controls into the content area below each " +
    "header/caption (not onto the header text). Designer code must use *.Panel.Controls.Add. " +
    "Open this form in the WinForms Designer and drag a control into each container to verify " +
    "no \"ReadOnly controls collection\" error.";
            //
            // khgDemo
            //
            this.khgDemo.HeaderVisibleSecondary = false;
            this.khgDemo.Location = new System.Drawing.Point(15, 85);
            this.khgDemo.Name = "khgDemo";
            //
            // khgDemo.Panel
            //
            this.khgDemo.Panel.Controls.Add(this.btnHeaderGroupChild);
            this.khgDemo.Size = new System.Drawing.Size(320, 160);
            this.khgDemo.TabIndex = 1;
            this.khgDemo.ValuesPrimary.Heading = "KryptonHeaderGroup";
            this.khgDemo.ValuesPrimary.Description = "Drop controls into the panel below this header";
            //
            // btnHeaderGroupChild
            //
            this.btnHeaderGroupChild.Location = new System.Drawing.Point(16, 16);
            this.btnHeaderGroupChild.Name = "btnHeaderGroupChild";
            this.btnHeaderGroupChild.Size = new System.Drawing.Size(160, 28);
            this.btnHeaderGroupChild.TabIndex = 0;
            this.btnHeaderGroupChild.Values.Text = "Panel child (runtime)";
            //
            // kryptonGroupDemo
            //
            this.kryptonGroupDemo.Location = new System.Drawing.Point(350, 85);
            this.kryptonGroupDemo.Name = "kryptonGroupDemo";
            //
            // kryptonGroupDemo.Panel
            //
            this.kryptonGroupDemo.Panel.Controls.Add(this.lblGroupChild);
            this.kryptonGroupDemo.Size = new System.Drawing.Size(300, 160);
            this.kryptonGroupDemo.TabIndex = 2;
            //
            // lblGroupChild
            //
            this.lblGroupChild.Location = new System.Drawing.Point(16, 16);
            this.lblGroupChild.Name = "lblGroupChild";
            this.lblGroupChild.Size = new System.Drawing.Size(140, 20);
            this.lblGroupChild.TabIndex = 0;
            this.lblGroupChild.Values.Text = "KryptonGroup panel child";
            //
            // kryptonGroupBoxDemo
            //
            this.kryptonGroupBoxDemo.Location = new System.Drawing.Point(15, 260);
            this.kryptonGroupBoxDemo.Name = "kryptonGroupBoxDemo";
            //
            // kryptonGroupBoxDemo.Panel
            //
            this.kryptonGroupBoxDemo.Panel.Controls.Add(this.lblGroupBoxChild);
            this.kryptonGroupBoxDemo.Size = new System.Drawing.Size(635, 170);
            this.kryptonGroupBoxDemo.TabIndex = 3;
            this.kryptonGroupBoxDemo.Values.Heading = "KryptonGroupBox";
            //
            // lblGroupBoxChild
            //
            this.lblGroupBoxChild.Location = new System.Drawing.Point(16, 16);
            this.lblGroupBoxChild.Name = "lblGroupBoxChild";
            this.lblGroupBoxChild.Size = new System.Drawing.Size(160, 20);
            this.lblGroupBoxChild.TabIndex = 0;
            this.lblGroupBoxChild.Values.Text = "KryptonGroupBox panel child";
            //
            // Bug3451KryptonHeaderGroupPanelDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.kryptonPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "Bug3451KryptonHeaderGroupPanelDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bug 3451 - HeaderGroup / Group panel parenting";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.kryptonPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.khgDemo.Panel)).EndInit();
            this.khgDemo.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.khgDemo)).EndInit();
            this.khgDemo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupDemo.Panel)).EndInit();
            this.kryptonGroupDemo.Panel.ResumeLayout(false);
            this.kryptonGroupDemo.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupDemo)).EndInit();
            this.kryptonGroupDemo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxDemo.Panel)).EndInit();
            this.kryptonGroupBoxDemo.Panel.ResumeLayout(false);
            this.kryptonGroupBoxDemo.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxDemo)).EndInit();
            this.kryptonGroupBoxDemo.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private Krypton.Toolkit.KryptonWrapLabel lblInstruction;
        private Krypton.Toolkit.KryptonHeaderGroup khgDemo;
        private Krypton.Toolkit.KryptonButton btnHeaderGroupChild;
        private Krypton.Toolkit.KryptonGroup kryptonGroupDemo;
        private Krypton.Toolkit.KryptonLabel lblGroupChild;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxDemo;
        private Krypton.Toolkit.KryptonLabel lblGroupBoxChild;
    }
}
