namespace TestForm
{
    partial class Form5
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
            this.components = new System.ComponentModel.Container();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.buttonSpecAny1 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny2 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny3 = new Krypton.Toolkit.ButtonSpecAny();
            this.kryptonIntegratedToolBarManager1 = new Krypton.Toolkit.KryptonIntegratedToolBarManager();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonPropertyGrid1);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 450);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.kryptonPropertyGrid1.CategoryForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonPropertyGrid1.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.kryptonPropertyGrid1.CommandsForeColor = System.Drawing.Color.Black;
            this.kryptonPropertyGrid1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonPropertyGrid1.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.kryptonPropertyGrid1.HelpForeColor = System.Drawing.Color.Black;
            this.kryptonPropertyGrid1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(196)))), ((int)(((byte)(216)))));
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(13, 41);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(344, 397);
            this.kryptonPropertyGrid1.TabIndex = 1;
            this.kryptonPropertyGrid1.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.kryptonPropertyGrid1.ViewForeColor = System.Drawing.Color.Black;
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DropDownWidth = 344;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(13, 13);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(344, 21);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 0;
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Previous;
            this.buttonSpecAny1.UniqueName = "4cb7c96cbdba4cfebc468a3149d6cc4c";
            // 
            // buttonSpecAny2
            // 
            this.buttonSpecAny2.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Next;
            this.buttonSpecAny2.UniqueName = "229ede385c5b4d98ab13f3464707d87d";
            // 
            // buttonSpecAny3
            // 
            this.buttonSpecAny3.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Context;
            this.buttonSpecAny3.UniqueName = "0943ca091e624724a0d729320faa85a5";
            // 
            // kryptonIntegratedToolBarManager1
            // 
            this.kryptonIntegratedToolBarManager1.AllowFormIntegration = false;
            this.kryptonIntegratedToolBarManager1.IntegratedToolBarButtonAlignment = Krypton.Toolkit.PaletteRelativeEdgeAlign.Far;
            this.kryptonIntegratedToolBarManager1.IntegratedToolBarButtonOrientation = Krypton.Toolkit.PaletteButtonOrientation.FixedTop;
            this.kryptonIntegratedToolBarManager1.ParentForm = this;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonSpecs.Add(this.buttonSpecAny1);
            this.ButtonSpecs.Add(this.buttonSpecAny2);
            this.ButtonSpecs.Add(this.buttonSpecAny3);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "Form5";
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Microsoft365DarkGray;
            this.Text = "Form5";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonPropertyGrid kryptonPropertyGrid1;
        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny2;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny3;
        private Krypton.Toolkit.KryptonIntegratedToolBarManager kryptonIntegratedToolBarManager1;
    }
}