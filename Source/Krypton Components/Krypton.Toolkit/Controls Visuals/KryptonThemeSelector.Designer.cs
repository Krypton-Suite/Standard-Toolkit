namespace Krypton.Toolkit
{
    partial class KryptonThemeSelector
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
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.ktcmbSelectedTheme = new Krypton.Toolkit.KryptonThemeComboBox();
            this.klblHeader = new Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnReset = new Krypton.Toolkit.KryptonButton();
            this.kbtnOK = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ktcmbSelectedTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.ktcmbSelectedTheme);
            this.kryptonPanel2.Controls.Add(this.klblHeader);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(615, 90);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // ktcmbSelectedTheme
            // 
            this.ktcmbSelectedTheme.DropDownWidth = 590;
            this.ktcmbSelectedTheme.IntegralHeight = false;
            this.ktcmbSelectedTheme.Location = new System.Drawing.Point(13, 40);
            this.ktcmbSelectedTheme.Name = "ktcmbSelectedTheme";
            this.ktcmbSelectedTheme.Size = new System.Drawing.Size(590, 21);
            this.ktcmbSelectedTheme.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.ktcmbSelectedTheme.TabIndex = 1;
            this.ktcmbSelectedTheme.SelectedIndexChanged += new System.EventHandler(this.ktcmbSelectedTheme_SelectedIndexChanged);
            // 
            // klblHeader
            // 
            this.klblHeader.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblHeader.Location = new System.Drawing.Point(13, 13);
            this.klblHeader.Name = "klblHeader";
            this.klblHeader.Size = new System.Drawing.Size(107, 20);
            this.klblHeader.TabIndex = 0;
            this.klblHeader.Values.Text = "Choose a theme:";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnReset);
            this.kryptonPanel1.Controls.Add(this.kbtnOK);
            this.kryptonPanel1.Controls.Add(this.kbtnCancel);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 90);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(615, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnReset
            // 
            this.kbtnReset.Enabled = false;
            this.kbtnReset.Location = new System.Drawing.Point(13, 13);
            this.kbtnReset.Name = "kbtnReset";
            this.kbtnReset.Size = new System.Drawing.Size(90, 25);
            this.kbtnReset.TabIndex = 3;
            this.kbtnReset.Values.Text = "kryptonButton3";
            this.kbtnReset.Click += new System.EventHandler(this.kbtnReset_Click);
            // 
            // kbtnOK
            // 
            this.kbtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnOK.Enabled = false;
            this.kbtnOK.Location = new System.Drawing.Point(416, 13);
            this.kbtnOK.Name = "kbtnOK";
            this.kbtnOK.Size = new System.Drawing.Size(90, 25);
            this.kbtnOK.TabIndex = 2;
            this.kbtnOK.Values.Text = "kryptonButton2";
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(512, 13);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(90, 25);
            this.kbtnCancel.TabIndex = 1;
            this.kbtnCancel.Values.Text = "kryptonButton1";
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderSecondary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(615, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // KryptonThemeSelector
            // 
            this.AcceptButton = this.kbtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnCancel;
            this.ClientSize = new System.Drawing.Size(615, 140);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonThemeSelector";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select a Theme";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ktcmbSelectedTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonButton kbtnReset;
        private KryptonButton kbtnOK;
        private KryptonButton kbtnCancel;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kryptonPanel2;
        private KryptonThemeComboBox ktcmbSelectedTheme;
        private KryptonLabel klblHeader;
    }
}