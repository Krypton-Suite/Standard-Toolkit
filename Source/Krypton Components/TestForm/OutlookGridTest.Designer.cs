namespace TestForm
{
    partial class OutlookGridTest
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
            Krypton.Toolkit.OutlookGridGroupCollection outlookGridGroupCollection2 = new Krypton.Toolkit.OutlookGridGroupCollection();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonHeaderGroup1 = new Krypton.Toolkit.KryptonHeaderGroup();
            this.bsahgLoad = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.bsahgSave = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.bsahgToggle = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.kryptonOutlookGrid1 = new Krypton.Toolkit.KryptonOutlookGrid();
            this.kryptonOutlookGridGroupBox1 = new Krypton.Toolkit.KryptonOutlookGridGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonHeaderGroup1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(834, 451);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.ButtonSpecs.Add(this.bsahgLoad);
            this.kryptonHeaderGroup1.ButtonSpecs.Add(this.bsahgSave);
            this.kryptonHeaderGroup1.ButtonSpecs.Add(this.bsahgToggle);
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(12, 12);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonOutlookGrid1);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonOutlookGridGroupBox1);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(798, 415);
            this.kryptonHeaderGroup1.TabIndex = 1;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Outlook Grid Test";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = null;
            // 
            // bsahgLoad
            // 
            this.bsahgLoad.Text = "Load Configuration";
            this.bsahgLoad.UniqueName = "1fed1aca43f5451fb5338a5e9b75a28a";
            this.bsahgLoad.Click += new System.EventHandler(this.bsahgLoad_Click);
            // 
            // bsahgSave
            // 
            this.bsahgSave.Text = "Save Configuration";
            this.bsahgSave.UniqueName = "45dd354dd9ef4a6b9cd6ec27f6106770";
            this.bsahgSave.Click += new System.EventHandler(this.bsahgSave_Click);
            // 
            // bsahgToggle
            // 
            this.bsahgToggle.Text = "Toggle all Nodes";
            this.bsahgToggle.UniqueName = "9e6b2c0dd34b42329857feecee3e1629";
            this.bsahgToggle.Click += new System.EventHandler(this.bsahgToggle_Click);
            // 
            // kryptonOutlookGrid1
            // 
            this.kryptonOutlookGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.kryptonOutlookGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonOutlookGrid1.FillMode = Krypton.Toolkit.GridFillMode.GroupsOnly;
            this.kryptonOutlookGrid1.GroupCollection = outlookGridGroupCollection2;
            this.kryptonOutlookGrid1.Location = new System.Drawing.Point(0, 46);
            this.kryptonOutlookGrid1.Name = "kryptonOutlookGrid1";
            this.kryptonOutlookGrid1.PreviousSelectedGroupRow = -1;
            this.kryptonOutlookGrid1.ShowLines = false;
            this.kryptonOutlookGrid1.Size = new System.Drawing.Size(796, 337);
            this.kryptonOutlookGrid1.TabIndex = 1;
            this.kryptonOutlookGrid1.GroupImageClick += new System.EventHandler<Krypton.Toolkit.OutlookGridGroupImageEventArgs>(this.kryptonOutlookGrid1_GroupImageClick);
            this.kryptonOutlookGrid1.Resize += new System.EventHandler(this.kryptonOutlookGrid1_Resize);
            // 
            // kryptonOutlookGridGroupBox1
            // 
            this.kryptonOutlookGridGroupBox1.AllowDrop = true;
            this.kryptonOutlookGridGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonOutlookGridGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.kryptonOutlookGridGroupBox1.Name = "kryptonOutlookGridGroupBox1";
            this.kryptonOutlookGridGroupBox1.Size = new System.Drawing.Size(796, 46);
            this.kryptonOutlookGridGroupBox1.TabIndex = 0;
            // 
            // OutlookGridTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 451);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "OutlookGridTest";
            this.Text = "OutlookGridTest";
            this.Load += new System.EventHandler(this.OutlookGridTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private Krypton.Toolkit.KryptonOutlookGridGroupBox kryptonOutlookGridGroupBox1;
        private Krypton.Toolkit.KryptonOutlookGrid kryptonOutlookGrid1;
        private Krypton.Toolkit.ButtonSpecHeaderGroup bsahgLoad;
        private Krypton.Toolkit.ButtonSpecHeaderGroup bsahgSave;
        private Krypton.Toolkit.ButtonSpecHeaderGroup bsahgToggle;
    }
}