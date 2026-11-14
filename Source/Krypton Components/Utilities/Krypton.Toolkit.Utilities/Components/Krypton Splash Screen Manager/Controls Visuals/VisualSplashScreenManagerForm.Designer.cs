namespace Krypton.Toolkit.Utilities
{
    partial class VisualSplashScreenManagerForm
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
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.tlpContent = new System.Windows.Forms.TableLayoutPanel();
            this.pbxLogo = new System.Windows.Forms.PictureBox();
            this.kwlblTitle = new Krypton.Toolkit.KryptonWrapLabel();
            this.kwlblStatus = new Krypton.Toolkit.KryptonWrapLabel();
            this.kpbProgress = new Krypton.Toolkit.KryptonProgressBar();
            this.kwlblVersion = new Krypton.Toolkit.KryptonWrapLabel();
            this.kwlblCopyright = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            this.tlpContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.tlpContent);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(16);
            this.kpnlMain.Size = new System.Drawing.Size(520, 320);
            this.kpnlMain.TabIndex = 0;
            // 
            // tlpContent
            // 
            this.tlpContent.BackColor = System.Drawing.Color.Transparent;
            this.tlpContent.ColumnCount = 1;
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Controls.Add(this.pbxLogo, 0, 0);
            this.tlpContent.Controls.Add(this.kwlblTitle, 0, 1);
            this.tlpContent.Controls.Add(this.kwlblStatus, 0, 2);
            this.tlpContent.Controls.Add(this.kpbProgress, 0, 3);
            this.tlpContent.Controls.Add(this.kwlblVersion, 0, 4);
            this.tlpContent.Controls.Add(this.kwlblCopyright, 0, 5);
            this.tlpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContent.Location = new System.Drawing.Point(16, 16);
            this.tlpContent.Name = "tlpContent";
            this.tlpContent.RowCount = 6;
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContent.Size = new System.Drawing.Size(488, 288);
            this.tlpContent.TabIndex = 0;
            // 
            // pbxLogo
            // 
            this.pbxLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxLogo.Location = new System.Drawing.Point(3, 3);
            this.pbxLogo.Name = "pbxLogo";
            this.pbxLogo.Size = new System.Drawing.Size(482, 148);
            this.pbxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxLogo.TabIndex = 0;
            this.pbxLogo.TabStop = false;
            // 
            // kwlblTitle
            // 
            this.kwlblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.kwlblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlblTitle.Location = new System.Drawing.Point(3, 154);
            this.kwlblTitle.Name = "kwlblTitle";
            this.kwlblTitle.Padding = new System.Windows.Forms.Padding(4);
            this.kwlblTitle.Size = new System.Drawing.Size(482, 33);
            this.kwlblTitle.Text = "Application";
            this.kwlblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kwlblStatus
            // 
            this.kwlblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlblStatus.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblStatus.Location = new System.Drawing.Point(3, 187);
            this.kwlblStatus.Name = "kwlblStatus";
            this.kwlblStatus.Padding = new System.Windows.Forms.Padding(4);
            this.kwlblStatus.Size = new System.Drawing.Size(482, 25);
            this.kwlblStatus.Text = "Starting…";
            this.kwlblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kpbProgress
            // 
            this.kpbProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpbProgress.Location = new System.Drawing.Point(3, 218);
            this.kpbProgress.Name = "kpbProgress";
            this.kpbProgress.Size = new System.Drawing.Size(482, 22);
            this.kpbProgress.TabIndex = 3;
            this.kpbProgress.Values.Text = "";
            // 
            // kwlblVersion
            // 
            this.kwlblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblVersion.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.kwlblVersion.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblVersion.Location = new System.Drawing.Point(3, 246);
            this.kwlblVersion.Name = "kwlblVersion";
            this.kwlblVersion.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.kwlblVersion.Size = new System.Drawing.Size(482, 19);
            this.kwlblVersion.Text = "Version";
            this.kwlblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kwlblCopyright
            // 
            this.kwlblCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblCopyright.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.kwlblCopyright.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblCopyright.Location = new System.Drawing.Point(3, 265);
            this.kwlblCopyright.Name = "kwlblCopyright";
            this.kwlblCopyright.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.kwlblCopyright.Size = new System.Drawing.Size(482, 19);
            this.kwlblCopyright.Text = "Copyright";
            this.kwlblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VisualSplashScreenManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 320);
            this.ControlBox = false;
            this.Controls.Add(this.kpnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualSplashScreenManagerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.tlpContent.ResumeLayout(false);
            this.tlpContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private System.Windows.Forms.TableLayoutPanel tlpContent;
        private System.Windows.Forms.PictureBox pbxLogo;
        private Krypton.Toolkit.KryptonWrapLabel kwlblTitle;
        private Krypton.Toolkit.KryptonWrapLabel kwlblStatus;
        private Krypton.Toolkit.KryptonProgressBar kpbProgress;
        private Krypton.Toolkit.KryptonWrapLabel kwlblVersion;
        private Krypton.Toolkit.KryptonWrapLabel kwlblCopyright;
    }
}
