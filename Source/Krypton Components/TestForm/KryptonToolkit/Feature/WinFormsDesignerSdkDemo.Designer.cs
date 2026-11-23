namespace TestForm
{
    partial class WinFormsDesignerSdkDemo
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
            this.kryptonPanelRoot = new Krypton.Toolkit.KryptonPanel();
            this.klblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
            this.kbtnSample = new Krypton.Toolkit.KryptonButton();
            this.kchkSample = new Krypton.Toolkit.KryptonCheckBox();
            this.ktxtSample = new Krypton.Toolkit.KryptonTextBox();
            this.kcmbSample = new Krypton.Toolkit.KryptonComboBox();
            this.kscSample = new Krypton.Toolkit.KryptonSplitContainer();
            this.klblLeft = new Krypton.Toolkit.KryptonLabel();
            this.klblRight = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelRoot)).BeginInit();
            this.kryptonPanelRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kscSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kscSample.Panel1)).BeginInit();
            this.kscSample.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kscSample.Panel2)).BeginInit();
            this.kscSample.Panel2.SuspendLayout();
            this.kscSample.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbSample)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPanelRoot
            //
            this.kryptonPanelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelRoot.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelRoot.Name = "kryptonPanelRoot";
            this.kryptonPanelRoot.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelRoot.Size = new System.Drawing.Size(720, 420);
            this.kryptonPanelRoot.TabIndex = 0;
            this.kryptonPanelRoot.Controls.Add(this.kscSample);
            this.kryptonPanelRoot.Controls.Add(this.kcmbSample);
            this.kryptonPanelRoot.Controls.Add(this.ktxtSample);
            this.kryptonPanelRoot.Controls.Add(this.kchkSample);
            this.kryptonPanelRoot.Controls.Add(this.kbtnSample);
            this.kryptonPanelRoot.Controls.Add(this.klblInstructions);
            //
            // klblInstructions
            //
            this.klblInstructions.AutoSize = false;
            this.klblInstructions.Dock = System.Windows.Forms.DockStyle.Top;
            this.klblInstructions.Location = new System.Drawing.Point(12, 12);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(696, 88);
            this.klblInstructions.TabIndex = 0;
            this.klblInstructions.Text = "Issue #593 — WinForms Designer Extensibility SDK.\r\nThis form is a runtime host. Custom designers, smart tags, and Client image/folder editors load in the Visual Studio .NET designer only when the consuming project references a packed Krypton NuGet (local feed is fine). TestForm ProjectReference builds use the default ControlDesigner.";
            //
            // kbtnSample
            //
            this.kbtnSample.Location = new System.Drawing.Point(12, 112);
            this.kbtnSample.Name = "kbtnSample";
            this.kbtnSample.Size = new System.Drawing.Size(160, 32);
            this.kbtnSample.TabIndex = 1;
            this.kbtnSample.Values.Text = "KryptonButton";
            //
            // kchkSample
            //
            this.kchkSample.Location = new System.Drawing.Point(188, 116);
            this.kchkSample.Name = "kchkSample";
            this.kchkSample.Size = new System.Drawing.Size(160, 24);
            this.kchkSample.TabIndex = 2;
            this.kchkSample.Values.Text = "KryptonCheckBox";
            //
            // ktxtSample
            //
            this.ktxtSample.Location = new System.Drawing.Point(12, 156);
            this.ktxtSample.Name = "ktxtSample";
            this.ktxtSample.Size = new System.Drawing.Size(240, 23);
            this.ktxtSample.TabIndex = 3;
            this.ktxtSample.Text = "KryptonTextBox";
            //
            // kcmbSample
            //
            this.kcmbSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbSample.IntegralHeight = false;
            this.kcmbSample.Location = new System.Drawing.Point(268, 156);
            this.kcmbSample.Name = "kcmbSample";
            this.kcmbSample.Size = new System.Drawing.Size(200, 21);
            this.kcmbSample.TabIndex = 4;
            this.kcmbSample.Items.AddRange(new object[] { "First", "Second", "Third" });
            this.kcmbSample.Text = "First";
            //
            // kscSample
            //
            this.kscSample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kscSample.Location = new System.Drawing.Point(12, 196);
            this.kscSample.Name = "kscSample";
            this.kscSample.Size = new System.Drawing.Size(696, 200);
            this.kscSample.SplitterDistance = 340;
            this.kscSample.TabIndex = 5;
            this.kscSample.Panel1.Controls.Add(this.klblLeft);
            this.kscSample.Panel2.Controls.Add(this.klblRight);
            //
            // klblLeft
            //
            this.klblLeft.Location = new System.Drawing.Point(8, 8);
            this.klblLeft.Name = "klblLeft";
            this.klblLeft.Size = new System.Drawing.Size(180, 20);
            this.klblLeft.TabIndex = 0;
            this.klblLeft.Values.Text = "Split panel 1 (drop targets)";
            //
            // klblRight
            //
            this.klblRight.Location = new System.Drawing.Point(8, 8);
            this.klblRight.Name = "klblRight";
            this.klblRight.Size = new System.Drawing.Size(180, 20);
            this.klblRight.TabIndex = 0;
            this.klblRight.Values.Text = "Split panel 2 (drop targets)";
            //
            // WinFormsDesignerSdkDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 420);
            this.Controls.Add(this.kryptonPanelRoot);
            this.Name = "WinFormsDesignerSdkDemo";
            this.Text = "WinForms Designer SDK (#593)";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelRoot)).EndInit();
            this.kryptonPanelRoot.ResumeLayout(false);
            this.kryptonPanelRoot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kscSample.Panel1)).EndInit();
            this.kscSample.Panel1.ResumeLayout(false);
            this.kscSample.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kscSample.Panel2)).EndInit();
            this.kscSample.Panel2.ResumeLayout(false);
            this.kscSample.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kscSample)).EndInit();
            this.kscSample.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanelRoot;
        private Krypton.Toolkit.KryptonWrapLabel klblInstructions;
        private Krypton.Toolkit.KryptonButton kbtnSample;
        private Krypton.Toolkit.KryptonCheckBox kchkSample;
        private Krypton.Toolkit.KryptonTextBox ktxtSample;
        private Krypton.Toolkit.KryptonComboBox kcmbSample;
        private Krypton.Toolkit.KryptonSplitContainer kscSample;
        private Krypton.Toolkit.KryptonLabel klblLeft;
        private Krypton.Toolkit.KryptonLabel klblRight;
    }
}
