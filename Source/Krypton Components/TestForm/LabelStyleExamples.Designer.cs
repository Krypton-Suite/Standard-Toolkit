namespace TestForm
{
    partial class LabelStyleExamples
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabelStyleExamples));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.label1Visited = new Krypton.Toolkit.KryptonLinkLabel();
            this.label1Pressed = new Krypton.Toolkit.KryptonLinkLabel();
            this.label1Live = new Krypton.Toolkit.KryptonLinkLabel();
            this.label1LinkDisabled = new Krypton.Toolkit.KryptonLinkLabel();
            this.label1NotVisited = new Krypton.Toolkit.KryptonLinkLabel();
            this.label1Normal = new Krypton.Toolkit.KryptonLabel();
            this.label1Disabled = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.label1Visited);
            this.kryptonPanel1.Controls.Add(this.label1Pressed);
            this.kryptonPanel1.Controls.Add(this.label1Live);
            this.kryptonPanel1.Controls.Add(this.label1LinkDisabled);
            this.kryptonPanel1.Controls.Add(this.label1NotVisited);
            this.kryptonPanel1.Controls.Add(this.label1Normal);
            this.kryptonPanel1.Controls.Add(this.label1Disabled);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(664, 200);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // label1Visited
            // 
            this.label1Visited.Enabled = false;
            this.label1Visited.LinkVisited = true;
            this.label1Visited.Location = new System.Drawing.Point(334, 99);
            this.label1Visited.Name = "label1Visited";
            this.label1Visited.Size = new System.Drawing.Size(128, 20);
            this.label1Visited.TabIndex = 35;
            this.label1Visited.Values.ExtraText = "(LinkLabel)";
            this.label1Visited.Values.Image = ((System.Drawing.Image)(resources.GetObject("label1Visited.Values.Image")));
            this.label1Visited.Values.Text = "Visited";
            // 
            // label1Pressed
            // 
            this.label1Pressed.Enabled = false;
            this.label1Pressed.Location = new System.Drawing.Point(334, 73);
            this.label1Pressed.Name = "label1Pressed";
            this.label1Pressed.Size = new System.Drawing.Size(133, 20);
            this.label1Pressed.TabIndex = 34;
            this.label1Pressed.Values.ExtraText = "(LinkLabel)";
            this.label1Pressed.Values.Image = ((System.Drawing.Image)(resources.GetObject("label1Pressed.Values.Image")));
            this.label1Pressed.Values.Text = "Pressed";
            // 
            // label1Live
            // 
            this.label1Live.Location = new System.Drawing.Point(334, 47);
            this.label1Live.Name = "label1Live";
            this.label1Live.Size = new System.Drawing.Size(113, 20);
            this.label1Live.TabIndex = 33;
            this.label1Live.Values.ExtraText = "(LinkLabel)";
            this.label1Live.Values.Image = ((System.Drawing.Image)(resources.GetObject("label1Live.Values.Image")));
            this.label1Live.Values.Text = "Live";
            // 
            // label1LinkDisabled
            // 
            this.label1LinkDisabled.Enabled = false;
            this.label1LinkDisabled.Location = new System.Drawing.Point(171, 99);
            this.label1LinkDisabled.Name = "label1LinkDisabled";
            this.label1LinkDisabled.Size = new System.Drawing.Size(139, 20);
            this.label1LinkDisabled.TabIndex = 32;
            this.label1LinkDisabled.Values.ExtraText = "(LinkLabel)";
            this.label1LinkDisabled.Values.Image = ((System.Drawing.Image)(resources.GetObject("label1LinkDisabled.Values.Image")));
            this.label1LinkDisabled.Values.Text = "Disabled";
            // 
            // label1NotVisited
            // 
            this.label1NotVisited.Enabled = false;
            this.label1NotVisited.Location = new System.Drawing.Point(171, 125);
            this.label1NotVisited.Name = "label1NotVisited";
            this.label1NotVisited.Size = new System.Drawing.Size(149, 20);
            this.label1NotVisited.TabIndex = 31;
            this.label1NotVisited.Values.ExtraText = "(LinkLabel)";
            this.label1NotVisited.Values.Image = ((System.Drawing.Image)(resources.GetObject("label1NotVisited.Values.Image")));
            this.label1NotVisited.Values.Text = "NotVisited";
            // 
            // label1Normal
            // 
            this.label1Normal.Enabled = false;
            this.label1Normal.Location = new System.Drawing.Point(171, 73);
            this.label1Normal.Name = "label1Normal";
            this.label1Normal.Size = new System.Drawing.Size(93, 20);
            this.label1Normal.TabIndex = 30;
            this.label1Normal.Values.ExtraText = "(Label)";
            this.label1Normal.Values.Text = "Normal";
            // 
            // label1Disabled
            // 
            this.label1Disabled.Enabled = false;
            this.label1Disabled.Location = new System.Drawing.Point(171, 47);
            this.label1Disabled.Name = "label1Disabled";
            this.label1Disabled.Size = new System.Drawing.Size(100, 20);
            this.label1Disabled.TabIndex = 29;
            this.label1Disabled.Values.ExtraText = "(Label)";
            this.label1Disabled.Values.Text = "Disabled";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(12, 12);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(138, 29);
            this.kryptonLabel1.TabIndex = 28;
            this.kryptonLabel1.Values.Text = "kryptonLabel1";
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            this.kryptonThemeComboBox1.DropDownWidth = 282;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(191, 163);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(282, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 43;
            this.kryptonThemeComboBox1.SelectedIndexChanged += new System.EventHandler(this.kryptonThemeComboBox1_SelectedIndexChanged);
            // 
            // LabelStyleExamples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 200);
            this.Controls.Add(this.kryptonThemeComboBox1);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "LabelStyleExamples";
            this.Text = "LabelStyleExamples";
            this.Load += new System.EventHandler(this.LabelStyleExamples_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonLabel kryptonLabel1;
        private KryptonLabel label1Disabled;
        private KryptonLabel label1Normal;
        private KryptonLinkLabel label1LinkDisabled;
        private KryptonLinkLabel label1NotVisited;
        private KryptonLinkLabel label1Live;
        private KryptonLinkLabel label1Pressed;
        private KryptonLinkLabel label1Visited;
        private KryptonThemeComboBox kryptonThemeComboBox1;
    }
}