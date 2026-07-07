namespace TestForm
{
    partial class CheckBoxExtendedDemo
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.kcbxAgreement = new Krypton.Toolkit.Utilities.KryptonCheckBoxExtended();
            this.kcbxStandard = new Krypton.Toolkit.KryptonCheckBox();
            this.kwlblStatus = new Krypton.Toolkit.KryptonWrapLabel();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kwlblInfo);
            this.kryptonPanel1.Controls.Add(this.kcbxAgreement);
            this.kryptonPanel1.Controls.Add(this.kcbxStandard);
            this.kryptonPanel1.Controls.Add(this.kwlblStatus);
            this.kryptonPanel1.Controls.Add(this.kbtnClose);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.kryptonPanel1.Size = new System.Drawing.Size(684, 361);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kwlblInfo
            // 
            this.kwlblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kwlblInfo.AutoSize = false;
            this.kwlblInfo.Location = new System.Drawing.Point(23, 23);
            this.kwlblInfo.Name = "kwlblInfo";
            this.kwlblInfo.Size = new System.Drawing.Size(638, 40);
            this.kwlblInfo.Text = "Compare Krypton CheckBox Extended (word-wrapped text, subtext, and optional subtext links) with a standard KryptonCheckBox at the same width.";
            // 
            // kcbxAgreement
            // 
            this.kcbxAgreement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kcbxAgreement.AutoSize = true;
            this.kcbxAgreement.Location = new System.Drawing.Point(23, 79);
            this.kcbxAgreement.MaximumSize = new System.Drawing.Size(0, 0);
            this.kcbxAgreement.Name = "kcbxAgreement";
            this.kcbxAgreement.Size = new System.Drawing.Size(638, 0);
            this.kcbxAgreement.Values.Subtext = "Please read the full agreement before continuing. Subtext uses a separate font and color.";
            this.kcbxAgreement.TabIndex = 0;
            this.kcbxAgreement.Text = "I agree to the terms and conditions that govern use of this software product and any associated services.";
            this.kcbxAgreement.CheckedChanged += new System.EventHandler(this.kcbxAgreement_CheckedChanged);
            // 
            // kcbxStandard
            // 
            this.kcbxStandard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kcbxStandard.AutoSize = false;
            this.kcbxStandard.Location = new System.Drawing.Point(23, 200);
            this.kcbxStandard.Name = "kcbxStandard";
            this.kcbxStandard.Size = new System.Drawing.Size(638, 25);
            this.kcbxStandard.TabIndex = 1;
            this.kcbxStandard.Values.Text = "Standard KryptonCheckBox with the same main text (single line).";
            // 
            // kwlblStatus
            // 
            this.kwlblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kwlblStatus.AutoSize = false;
            this.kwlblStatus.Location = new System.Drawing.Point(23, 241);
            this.kwlblStatus.Name = "kwlblStatus";
            this.kwlblStatus.Size = new System.Drawing.Size(638, 23);
            this.kwlblStatus.Text = "Agreement accepted: False";
            // 
            // kbtnClose
            // 
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnClose.Location = new System.Drawing.Point(561, 316);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(100, 25);
            this.kbtnClose.TabIndex = 2;
            this.kbtnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClose.Values.Text = "Close";
            // 
            // CheckBoxExtendedDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnClose;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckBoxExtendedDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Krypton CheckBox Extended Demo";
            this.Load += new System.EventHandler(this.CheckBoxExtendedDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
        private Krypton.Toolkit.Utilities.KryptonCheckBoxExtended kcbxAgreement;
        private Krypton.Toolkit.KryptonCheckBox kcbxStandard;
        private Krypton.Toolkit.KryptonWrapLabel kwlblStatus;
        private Krypton.Toolkit.KryptonButton kbtnClose;
    }
}
