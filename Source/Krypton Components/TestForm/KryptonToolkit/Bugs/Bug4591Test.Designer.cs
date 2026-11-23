namespace TestForm
{
    partial class Bug4591Test
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonSplitContainer1 = new Krypton.Toolkit.KryptonSplitContainer();
            this.kryptonWrapLabel1 = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kcbShowEventsButton = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnSelectSample = new Krypton.Toolkit.KryptonButton();
            this.kbtnClearSelection = new Krypton.Toolkit.KryptonButton();
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).BeginInit();
            this.kryptonSplitContainer1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).BeginInit();
            this.kryptonSplitContainer1.Panel2.SuspendLayout();
            this.kryptonSplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonSplitContainer1
            // 
            this.kryptonSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(0, 0);
            // 
            // kryptonSplitContainer1.Panel1
            // 
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.kbtnClearSelection);
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.kbtnSelectSample);
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.kcbShowEventsButton);
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.kryptonButton1);
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.kryptonWrapLabel1);
            // 
            // kryptonSplitContainer1.Panel2
            // 
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.kryptonPropertyGrid1);
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(1025, 620);
            this.kryptonSplitContainer1.SplitterDistance = 520;
            this.kryptonSplitContainer1.TabIndex = 1;
            // 
            // kryptonWrapLabel1
            // 
            this.kryptonWrapLabel1.AutoSize = false;
            this.kryptonWrapLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonWrapLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonWrapLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonWrapLabel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonWrapLabel1.Name = "kryptonWrapLabel1";
            this.kryptonWrapLabel1.Size = new System.Drawing.Size(520, 72);
            this.kryptonWrapLabel1.Text = "Issue #4591: with a control selected, the property grid toolbar should show the Events (lightning bolt) button. Use Clear selection to hide it via auto-sync, or toggle the checkbox to exercise SetEventsButtonVisible.";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(24, 96);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(140, 32);
            this.kryptonButton1.TabIndex = 0;
            this.kryptonButton1.Values.Text = "Sample button";
            // 
            // kcbShowEventsButton
            // 
            this.kcbShowEventsButton.Location = new System.Drawing.Point(24, 152);
            this.kcbShowEventsButton.Name = "kcbShowEventsButton";
            this.kcbShowEventsButton.Size = new System.Drawing.Size(280, 20);
            this.kcbShowEventsButton.TabIndex = 1;
            this.kcbShowEventsButton.Values.Text = "Show Events button (API)";
            this.kcbShowEventsButton.Checked = true;
            this.kcbShowEventsButton.CheckedChanged += new System.EventHandler(this.kcbShowEventsButton_CheckedChanged);
            // 
            // kbtnSelectSample
            // 
            this.kbtnSelectSample.Location = new System.Drawing.Point(24, 196);
            this.kbtnSelectSample.Name = "kbtnSelectSample";
            this.kbtnSelectSample.Size = new System.Drawing.Size(140, 32);
            this.kbtnSelectSample.TabIndex = 2;
            this.kbtnSelectSample.Values.Text = "Select sample";
            this.kbtnSelectSample.Click += new System.EventHandler(this.kbtnSelectSample_Click);
            // 
            // kbtnClearSelection
            // 
            this.kbtnClearSelection.Location = new System.Drawing.Point(180, 196);
            this.kbtnClearSelection.Name = "kbtnClearSelection";
            this.kbtnClearSelection.Size = new System.Drawing.Size(140, 32);
            this.kbtnClearSelection.TabIndex = 3;
            this.kbtnClearSelection.Values.Text = "Clear selection";
            this.kbtnClearSelection.Click += new System.EventHandler(this.kbtnClearSelection_Click);
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.Padding = new System.Windows.Forms.Padding(1);
            this.kryptonPropertyGrid1.SelectedObject = this.kryptonButton1;
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(500, 620);
            this.kryptonPropertyGrid1.TabIndex = 0;
            this.kryptonPropertyGrid1.Text = "kryptonPropertyGrid1";
            // 
            // Bug4591Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 620);
            this.Controls.Add(this.kryptonSplitContainer1);
            this.Name = "Bug4591Test";
            this.Text = "Bug #4591 - PropertyGrid Events button";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).EndInit();
            this.kryptonSplitContainer1.Panel1.ResumeLayout(false);
            this.kryptonSplitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).EndInit();
            this.kryptonSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).EndInit();
            this.kryptonSplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonSplitContainer kryptonSplitContainer1;
        private KryptonWrapLabel kryptonWrapLabel1;
        private KryptonButton kryptonButton1;
        private KryptonCheckBox kcbShowEventsButton;
        private KryptonButton kbtnSelectSample;
        private KryptonButton kbtnClearSelection;
        private KryptonPropertyGrid kryptonPropertyGrid1;
    }
}
