namespace TestForm
{
    partial class RTLFormBorderTest
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
            this.buttonSpecAny1 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny2 = new Krypton.Toolkit.ButtonSpecAny();
            this.kchkbtnSwitchLayout = new Krypton.Toolkit.KryptonCheckButton();
            this.SuspendLayout();
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Next;
            this.buttonSpecAny1.UniqueName = "0f4b63b15d3f450d8a516cd20cf8228d";
            // 
            // buttonSpecAny2
            // 
            this.buttonSpecAny2.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Previous;
            this.buttonSpecAny2.UniqueName = "13edbb23e5f1489cb15b33880c16b496";
            // 
            // kchkbtnSwitchLayout
            // 
            this.kchkbtnSwitchLayout.Location = new System.Drawing.Point(317, 150);
            this.kchkbtnSwitchLayout.Name = "kchkbtnSwitchLayout";
            this.kchkbtnSwitchLayout.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.kchkbtnSwitchLayout.Size = new System.Drawing.Size(90, 25);
            this.kchkbtnSwitchLayout.TabIndex = 1;
            this.kchkbtnSwitchLayout.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kchkbtnSwitchLayout.Values.Text = "Switch Layout";
            this.kchkbtnSwitchLayout.Click += new System.EventHandler(this.kchkbtnSwitchLayout_Click);
            // 
            // RTLFormBorderTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ButtonSpecs.Add(this.buttonSpecAny1);
            this.ButtonSpecs.Add(this.buttonSpecAny2);
            this.ClientSize = new System.Drawing.Size(705, 414);
            this.Controls.Add(this.kchkbtnSwitchLayout);
            this.Name = "RTLFormBorderTest";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "RTLFormBorderTest";
            this.TextExtra = "Test Text";
            this.ResumeLayout(false);

        }

        #endregion

        private ButtonSpecAny buttonSpecAny1;
        private ButtonSpecAny buttonSpecAny2;
        private KryptonCheckButton kchkbtnSwitchLayout;
    }
}