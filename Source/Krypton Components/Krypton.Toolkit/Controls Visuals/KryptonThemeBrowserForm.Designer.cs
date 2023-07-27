namespace Krypton.Toolkit
{
    partial class KryptonThemeBrowserForm
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
            this.kcpbCustom = new Krypton.Toolkit.KryptonCustomPaletteBase(this.components);
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kbtnImport = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.klbThemeList = new Krypton.Toolkit.KryptonListBox();
            this.kbtnOK = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kcpbCustom
            // 
            this.kcpbCustom.BaseFont = new System.Drawing.Font("Segoe UI", 9F);
            this.kcpbCustom.BaseFontSize = 9F;
            this.kcpbCustom.BasePaletteType = Krypton.Toolkit.BasePaletteType.Custom;
            this.kcpbCustom.ThemeName = null;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnOK);
            this.kryptonPanel1.Controls.Add(this.kbtnCancel);
            this.kryptonPanel1.Controls.Add(this.kbtnImport);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 400);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.klbThemeList);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(800, 400);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderSecondary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(800, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kbtnImport
            // 
            this.kbtnImport.Location = new System.Drawing.Point(13, 13);
            this.kbtnImport.Name = "kbtnImport";
            this.kbtnImport.Size = new System.Drawing.Size(90, 25);
            this.kbtnImport.TabIndex = 1;
            this.kbtnImport.Values.Text = "&Import...";
            this.kbtnImport.Click += new System.EventHandler(this.kbtnImport_Click);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(698, 13);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(90, 25);
            this.kbtnCancel.TabIndex = 2;
            this.kbtnCancel.UseAsADialogButton = true;
            this.kbtnCancel.Values.Text = "Cance&l";
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCancel_Click);
            // 
            // klbThemeList
            // 
            this.klbThemeList.Location = new System.Drawing.Point(13, 13);
            this.klbThemeList.Name = "klbThemeList";
            this.klbThemeList.Size = new System.Drawing.Size(775, 381);
            this.klbThemeList.TabIndex = 0;
            this.klbThemeList.SelectedIndexChanged += new System.EventHandler(this.klbThemeList_SelectedIndexChanged);
            // 
            // kbtnOK
            // 
            this.kbtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnOK.Location = new System.Drawing.Point(602, 13);
            this.kbtnOK.Name = "kbtnOK";
            this.kbtnOK.Size = new System.Drawing.Size(90, 25);
            this.kbtnOK.TabIndex = 3;
            this.kbtnOK.UseAsADialogButton = true;
            this.kbtnOK.Values.Text = "O&K";
            this.kbtnOK.Click += new System.EventHandler(this.kbtnOK_Click);
            // 
            // KryptonThemeBrowserForm
            // 
            this.AcceptButton = this.kbtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnCancel;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonThemeBrowserForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Select a Theme";
            this.Load += new System.EventHandler(this.KryptonThemeBrowserForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonCustomPaletteBase kcpbCustom;
        private KryptonPanel kryptonPanel1;
        private KryptonPanel kryptonPanel2;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonButton kbtnImport;
        private KryptonButton kbtnCancel;
        private KryptonListBox klbThemeList;
        private KryptonButton kbtnOK;
    }
}