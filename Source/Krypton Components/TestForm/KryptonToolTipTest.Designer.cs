namespace TestForm
{
    partial class KryptonToolTipTest
    {
        private System.ComponentModel.IContainer components;

        /// <inheritdoc />
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
            this.components = new System.ComponentModel.Container();
            this.kryptonToolTip1 = new Krypton.Toolkit.KryptonToolTip(this.components);
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
            this.pnlHoverRegion = new System.Windows.Forms.Panel();
            this.klblHoverPanelCaption = new Krypton.Toolkit.KryptonLabel();
            this.btnStandardWinFormsButton = new System.Windows.Forms.Button();
            this.kbtnSample = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)this.kpnlMain).BeginInit();
            this.kpnlMain.SuspendLayout();
            this.pnlHoverRegion.SuspendLayout();
            this.SuspendLayout();
            //
            // kpnlMain
            //
            this.kpnlMain.Controls.Add(this.klblInstructions);
            this.kpnlMain.Controls.Add(this.pnlHoverRegion);
            this.kpnlMain.Controls.Add(this.btnStandardWinFormsButton);
            this.kpnlMain.Controls.Add(this.kbtnSample);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(14);
            this.kpnlMain.Size = new System.Drawing.Size(534, 311);
            this.kpnlMain.TabIndex = 0;
            //
            // klblInstructions
            //
            this.klblInstructions.AutoSize = false;
            this.klblInstructions.Dock = System.Windows.Forms.DockStyle.Top;
            this.klblInstructions.Location = new System.Drawing.Point(14, 14);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(506, 72);
            this.klblInstructions.TabIndex = 0;
            this.klblInstructions.Values.Text =
                "Hover the controls below. Tooltips use the global Krypton palette, SuperTip styling, and standard show / auto-close delays.";
            //
            // pnlHoverRegion
            //
            this.pnlHoverRegion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHoverRegion.Controls.Add(this.klblHoverPanelCaption);
            this.pnlHoverRegion.Location = new System.Drawing.Point(17, 186);
            this.pnlHoverRegion.Name = "pnlHoverRegion";
            this.pnlHoverRegion.Size = new System.Drawing.Size(497, 100);
            this.pnlHoverRegion.TabIndex = 3;
            //
            // klblHoverPanelCaption
            //
            this.klblHoverPanelCaption.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.klblHoverPanelCaption.AutoSize = true;
            this.klblHoverPanelCaption.Location = new System.Drawing.Point(8, 8);
            this.klblHoverPanelCaption.Name = "klblHoverPanelCaption";
            this.klblHoverPanelCaption.Size = new System.Drawing.Size(255, 20);
            this.klblHoverPanelCaption.TabIndex = 0;
            this.klblHoverPanelCaption.Values.Text = @"Hover panel (System.Windows.Forms.Panel)";
            //
            // btnStandardWinFormsButton
            //
            this.btnStandardWinFormsButton.Location = new System.Drawing.Point(17, 100);
            this.btnStandardWinFormsButton.Name = "btnStandardWinFormsButton";
            this.btnStandardWinFormsButton.Size = new System.Drawing.Size(240, 32);
            this.btnStandardWinFormsButton.TabIndex = 1;
            this.btnStandardWinFormsButton.Text = "Standard WinForms.Button";
            this.btnStandardWinFormsButton.UseVisualStyleBackColor = true;
            //
            // kbtnSample
            //
            this.kbtnSample.Location = new System.Drawing.Point(274, 100);
            this.kbtnSample.Name = "kbtnSample";
            this.kbtnSample.Size = new System.Drawing.Size(240, 32);
            this.kbtnSample.TabIndex = 2;
            this.kbtnSample.Values.Text = "KryptonButton";
            //
            // KryptonToolTipTest
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 311);
            this.Controls.Add(this.kpnlMain);
            this.Name = "KryptonToolTipTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = @"KryptonToolTip demo (#3380)";
            ((System.ComponentModel.ISupportInitialize)this.kpnlMain).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.pnlHoverRegion.ResumeLayout(false);
            this.pnlHoverRegion.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonToolTip kryptonToolTip1;

        private Krypton.Toolkit.KryptonPanel kpnlMain;

        private Krypton.Toolkit.KryptonLabel klblInstructions;

        private System.Windows.Forms.Panel pnlHoverRegion;

        private Krypton.Toolkit.KryptonLabel klblHoverPanelCaption;

        private System.Windows.Forms.Button btnStandardWinFormsButton;

        private Krypton.Toolkit.KryptonButton kbtnSample;
    }
}
