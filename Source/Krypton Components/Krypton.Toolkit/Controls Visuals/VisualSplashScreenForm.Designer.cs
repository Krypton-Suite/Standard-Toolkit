namespace Krypton.Toolkit
{
    partial class VisualSplashScreenForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            this.kbtnMinimize = new Krypton.Toolkit.KryptonButton();
            this.pbxApplicationIcon = new System.Windows.Forms.PictureBox();
            this.klblCopyright = new Krypton.Toolkit.KryptonLabel();
            this.klblVersion = new Krypton.Toolkit.KryptonLabel();
            this.kpbProgress = new Krypton.Toolkit.KryptonProgressBar();
            this.tmrCountdown = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxApplicationIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 450);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.kbtnClose, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.kbtnMinimize, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbxApplicationIcon, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.klblCopyright, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.klblVersion, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.kpbProgress, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // kbtnClose
            // 
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.AutoSize = true;
            this.kbtnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnClose.ButtonStyle = Krypton.Toolkit.ButtonStyle.FormClose;
            this.kbtnClose.Location = new System.Drawing.Point(783, 3);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(14, 20);
            this.kbtnClose.TabIndex = 0;
            this.kbtnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClose.Values.Text = "X";
            this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
            // 
            // kbtnMinimize
            // 
            this.kbtnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnMinimize.AutoSize = true;
            this.kbtnMinimize.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnMinimize.ButtonStyle = Krypton.Toolkit.ButtonStyle.FormClose;
            this.kbtnMinimize.Location = new System.Drawing.Point(765, 3);
            this.kbtnMinimize.Name = "kbtnMinimize";
            this.kbtnMinimize.Size = new System.Drawing.Size(11, 20);
            this.kbtnMinimize.TabIndex = 1;
            this.kbtnMinimize.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnMinimize.Values.Text = "-";
            this.kbtnMinimize.Click += new System.EventHandler(this.kbtnMinimize_Click);
            // 
            // pbxApplicationIcon
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pbxApplicationIcon, 4);
            this.pbxApplicationIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxApplicationIcon.Location = new System.Drawing.Point(3, 29);
            this.pbxApplicationIcon.Name = "pbxApplicationIcon";
            this.pbxApplicationIcon.Size = new System.Drawing.Size(794, 334);
            this.pbxApplicationIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxApplicationIcon.TabIndex = 4;
            this.pbxApplicationIcon.TabStop = false;
            // 
            // klblCopyright
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.klblCopyright, 4);
            this.klblCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblCopyright.Location = new System.Drawing.Point(3, 369);
            this.klblCopyright.Name = "klblCopyright";
            this.klblCopyright.Size = new System.Drawing.Size(794, 20);
            this.klblCopyright.TabIndex = 5;
            this.klblCopyright.Values.Text = "kryptonLabel1";
            // 
            // klblVersion
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.klblVersion, 4);
            this.klblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblVersion.Location = new System.Drawing.Point(3, 395);
            this.klblVersion.Name = "klblVersion";
            this.klblVersion.Size = new System.Drawing.Size(794, 20);
            this.klblVersion.TabIndex = 6;
            this.klblVersion.Values.Text = "kryptonLabel2";
            // 
            // kpbProgress
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.kpbProgress, 4);
            this.kpbProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpbProgress.Location = new System.Drawing.Point(3, 421);
            this.kpbProgress.Name = "kpbProgress";
            this.kpbProgress.Size = new System.Drawing.Size(794, 26);
            this.kpbProgress.StateCommon.Back.Color1 = System.Drawing.Color.Green;
            this.kpbProgress.StateDisabled.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kpbProgress.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kpbProgress.TabIndex = 7;
            this.kpbProgress.Values.Text = "";
            // 
            // tmrCountdown
            // 
            this.tmrCountdown.Enabled = true;
            this.tmrCountdown.Interval = 1000;
            this.tmrCountdown.Tick += new System.EventHandler(this.tmrCountdown_Tick);
            // 
            // KryptonSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "KryptonSplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VisualSplashScreenForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VisualSplashScreenForm_FormClosed);
            this.Load += new System.EventHandler(this.VisualSplashScreenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxApplicationIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonButton kbtnClose;
        private KryptonButton kbtnMinimize;
        private PictureBox pbxApplicationIcon;
        private KryptonLabel klblCopyright;
        private KryptonLabel klblVersion;
        private KryptonProgressBar kpbProgress;
        private System.Windows.Forms.Timer tmrCountdown;
    }
}