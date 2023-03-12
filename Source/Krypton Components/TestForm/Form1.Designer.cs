namespace TestForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonButton2 = new Krypton.Toolkit.KryptonButton();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonListBox1 = new Krypton.Toolkit.KryptonListBox();
            this.kryptonTextBox1 = new Krypton.Toolkit.KryptonTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.buttonSpecAny2 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny1 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny3 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny4 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny5 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny6 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny7 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny8 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny9 = new Krypton.Toolkit.ButtonSpecAny();
            this.bsHelp = new Krypton.Toolkit.ButtonSpecAny();
            this.kryptonHelpCommand1 = new Krypton.Toolkit.KryptonHelpCommand();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonButton2);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Controls.Add(this.kryptonListBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonTextBox1);
            this.kryptonPanel1.Controls.Add(this.textBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(600, 366);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(55, 263);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton2.TabIndex = 6;
            this.kryptonButton2.Values.Text = "Ribbon";
            this.kryptonButton2.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DropDownWidth = 121;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(25, 190);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(161, 21);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom)
            | Krypton.Toolkit.PaletteDrawBorders.Left)
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Border.ImageStyle = Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 4;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(55, 231);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 5;
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonListBox1
            // 
            this.kryptonListBox1.Location = new System.Drawing.Point(202, 10);
            this.kryptonListBox1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonListBox1.Name = "kryptonListBox1";
            this.kryptonListBox1.Size = new System.Drawing.Size(388, 346);
            this.kryptonListBox1.TabIndex = 2;
            // 
            // kryptonTextBox1
            // 
            this.kryptonTextBox1.Location = new System.Drawing.Point(58, 104);
            this.kryptonTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonTextBox1.Name = "kryptonTextBox1";
            this.kryptonTextBox1.Size = new System.Drawing.Size(75, 23);
            this.kryptonTextBox1.TabIndex = 1;
            this.kryptonTextBox1.Text = "kryptonTextBox1";
            this.kryptonTextBox1.Click += new System.EventHandler(this.kryptonTextBox1_Click);
            this.kryptonTextBox1.DoubleClick += new System.EventHandler(this.kryptonTextBox1_DoubleClick);
            this.kryptonTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.kryptonTextBox1_KeyDown);
            this.kryptonTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.kryptonTextBox1_KeyPress);
            this.kryptonTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.kryptonTextBox1_KeyUp);
            this.kryptonTextBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.kryptonTextBox1_MouseClick);
            this.kryptonTextBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.kryptonTextBox1_MouseDoubleClick);
            this.kryptonTextBox1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.kryptonTextBox1_PreviewKeyDown);
            this.kryptonTextBox1.Validating += new System.ComponentModel.CancelEventHandler(this.kryptonTextBox1_Validating);
            this.kryptonTextBox1.Validated += new System.EventHandler(this.kryptonTextBox1_Validated);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(55, 49);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            this.textBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseClick);
            this.textBox1.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            this.textBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseDoubleClick);
            this.textBox1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox1_PreviewKeyDown);
            this.textBox1.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            this.textBox1.Validated += new System.EventHandler(this.textBox1_Validated);
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.Office2013White;
            // 
            // buttonSpecAny2
            // 
            this.buttonSpecAny2.Style = Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.buttonSpecAny2.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecAny2.Type = Krypton.Toolkit.PaletteButtonSpecStyle.New;
            this.buttonSpecAny2.UniqueName = "18ecc139f2254e42a53c2028cfca8f5c";
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Style = Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.buttonSpecAny1.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecAny1.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Open;
            this.buttonSpecAny1.UniqueName = "3751d4a69d0d420586c1567d0b9adfa3";
            // 
            // buttonSpecAny3
            // 
            this.buttonSpecAny3.Style = Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.buttonSpecAny3.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecAny3.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Save;
            this.buttonSpecAny3.UniqueName = "5378f91fbfe24ef29e5f7c28dcd4ef83";
            // 
            // buttonSpecAny4
            // 
            this.buttonSpecAny4.Style = Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.buttonSpecAny4.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecAny4.Type = Krypton.Toolkit.PaletteButtonSpecStyle.SaveAs;
            this.buttonSpecAny4.UniqueName = "8c65e58b191240039ac9f199a50ffeef";
            // 
            // buttonSpecAny5
            // 
            this.buttonSpecAny5.Style = Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.buttonSpecAny5.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecAny5.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Cut;
            this.buttonSpecAny5.UniqueName = "1a4a214557e443e18dd4e4dce0d6362a";
            // 
            // buttonSpecAny6
            // 
            this.buttonSpecAny6.Style = Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.buttonSpecAny6.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecAny6.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Copy;
            this.buttonSpecAny6.UniqueName = "4fc58d985a6c45d5b5151f9be3e19e23";
            // 
            // buttonSpecAny7
            // 
            this.buttonSpecAny7.Style = Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.buttonSpecAny7.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecAny7.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Paste;
            this.buttonSpecAny7.UniqueName = "0f2faa1862a949a7a609272d435898db";
            // 
            // buttonSpecAny8
            // 
            this.buttonSpecAny8.Style = Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.buttonSpecAny8.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecAny8.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Undo;
            this.buttonSpecAny8.UniqueName = "cac73c524b924a8db102dde24f44995a";
            // 
            // buttonSpecAny9
            // 
            this.buttonSpecAny9.Style = Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.buttonSpecAny9.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecAny9.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Redo;
            this.buttonSpecAny9.UniqueName = "7d671b8ea94f44419983e71c70353e7f";
            // 
            // bsHelp
            // 
            this.bsHelp.Enabled = Krypton.Toolkit.ButtonEnabled.True;
            this.bsHelp.Style = Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.bsHelp.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.bsHelp.Type = Krypton.Toolkit.PaletteButtonSpecStyle.FormHelp;
            this.bsHelp.UniqueName = "bcd86d4cc522416687d43465380b044a";
            // 
            // kryptonHelpCommand1
            // 
            this.kryptonHelpCommand1.HelpButton = this.bsHelp;
            this.kryptonHelpCommand1.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonHelpCommand1.ImageSmall")));
            this.kryptonHelpCommand1.Execute += new System.EventHandler(this.kryptonHelpCommand1_Execute);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ButtonSpecs.AddRange(new Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecAny2,
            this.buttonSpecAny1,
            this.buttonSpecAny3,
            this.buttonSpecAny4,
            this.buttonSpecAny5,
            this.buttonSpecAny6,
            this.buttonSpecAny7,
            this.buttonSpecAny8,
            this.buttonSpecAny9,
            this.bsHelp});
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.kryptonPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Controls.SetChildIndex(this.kryptonPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonTextBox kryptonTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private Krypton.Toolkit.KryptonListBox kryptonListBox1;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
        private Krypton.Toolkit.KryptonButton kryptonButton2;
        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny2;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny3;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny4;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny5;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny6;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny7;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny8;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny9;
        private Krypton.Toolkit.ButtonSpecAny bsHelp;
        private Krypton.Toolkit.KryptonHelpCommand kryptonHelpCommand1;
    }
}