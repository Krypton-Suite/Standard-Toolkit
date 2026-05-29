namespace Krypton.Toolkit
{
    partial class VisualInformationBoxForm
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
            this.tmrAutoClose = new System.Windows.Forms.Timer(this.components);
            this.tlpBase = new System.Windows.Forms.TableLayoutPanel();
            this._panelContent = new Krypton.Toolkit.KryptonPanel();
            this._panelFooter = new Krypton.Toolkit.KryptonPanel();
            this.tlpContent = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._checkBox = new Krypton.Toolkit.KryptonCheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._panelButtons = new Krypton.Toolkit.KryptonPanel();
            this._borderEdge = new Krypton.Toolkit.KryptonBorderEdge();
            this.kpnlContentArea = new Krypton.Toolkit.KryptonPanel();
            this.kwlblMessageText = new Krypton.Toolkit.KryptonWrapLabel();
            this.klwlblMessageText = new Krypton.Toolkit.KryptonLinkWrapLabel();
            this.tlpBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelContent)).BeginInit();
            this._panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelFooter)).BeginInit();
            this._panelFooter.SuspendLayout();
            this.tlpContent.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelButtons)).BeginInit();
            this._panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContentArea)).BeginInit();
            this.kpnlContentArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpBase
            // 
            this.tlpBase.BackColor = System.Drawing.Color.Transparent;
            this.tlpBase.ColumnCount = 1;
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBase.Controls.Add(this._panelContent, 0, 0);
            this.tlpBase.Controls.Add(this._panelFooter, 0, 1);
            this.tlpBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBase.Location = new System.Drawing.Point(0, 0);
            this.tlpBase.Name = "tlpBase";
            this.tlpBase.RowCount = 2;
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpBase.Size = new System.Drawing.Size(267, 163);
            this.tlpBase.TabIndex = 0;
            // 
            // _panelContent
            // 
            this._panelContent.Controls.Add(this.tlpContent);
            this._panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelContent.Location = new System.Drawing.Point(0, 0);
            this._panelContent.Margin = new System.Windows.Forms.Padding(0);
            this._panelContent.Name = "_panelContent";
            this._panelContent.Size = new System.Drawing.Size(267, 98);
            this._panelContent.TabIndex = 3;
            // 
            // _panelFooter
            // 
            this._panelFooter.Controls.Add(this.tableLayoutPanel1);
            this._panelFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelFooter.Location = new System.Drawing.Point(0, 98);
            this._panelFooter.Margin = new System.Windows.Forms.Padding(0);
            this._panelFooter.Name = "_panelFooter";
            this._panelFooter.Size = new System.Drawing.Size(267, 65);
            this._panelFooter.TabIndex = 4;
            // 
            // tlpContent
            // 
            this.tlpContent.ColumnCount = 2;
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Controls.Add(this.kpnlContentArea, 1, 0);
            this.tlpContent.Controls.Add(this.pictureBox1, 0, 0);
            this.tlpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContent.Location = new System.Drawing.Point(0, 0);
            this.tlpContent.Name = "tlpContent";
            this.tlpContent.RowCount = 1;
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Size = new System.Drawing.Size(267, 98);
            this.tlpContent.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._panelButtons, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._checkBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(267, 65);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _checkBox
            // 
            this._checkBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._checkBox.Location = new System.Drawing.Point(3, 3);
            this._checkBox.Name = "_checkBox";
            this._checkBox.Size = new System.Drawing.Size(261, 20);
            this._checkBox.TabIndex = 1;
            this._checkBox.Values.Text = "checkBox";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 88);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // _panelButtons
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._panelButtons, 2);
            this._panelButtons.Controls.Add(this._borderEdge);
            this._panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelButtons.Location = new System.Drawing.Point(0, 26);
            this._panelButtons.Margin = new System.Windows.Forms.Padding(0);
            this._panelButtons.Name = "_panelButtons";
            this._panelButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this._panelButtons.Size = new System.Drawing.Size(267, 39);
            this._panelButtons.TabIndex = 2;
            // 
            // _borderEdge
            // 
            this._borderEdge.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this._borderEdge.Dock = System.Windows.Forms.DockStyle.Top;
            this._borderEdge.Location = new System.Drawing.Point(0, 0);
            this._borderEdge.Margin = new System.Windows.Forms.Padding(2);
            this._borderEdge.Name = "_borderEdge";
            this._borderEdge.Size = new System.Drawing.Size(267, 1);
            this._borderEdge.Text = "kryptonBorderEdge1";
            // 
            // kpnlContentArea
            // 
            this.kpnlContentArea.Controls.Add(this.kwlblMessageText);
            this.kpnlContentArea.Controls.Add(this.klwlblMessageText);
            this.kpnlContentArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlContentArea.Location = new System.Drawing.Point(61, 3);
            this.kpnlContentArea.Name = "kpnlContentArea";
            this.kpnlContentArea.Size = new System.Drawing.Size(203, 92);
            this.kpnlContentArea.TabIndex = 2;
            // 
            // kwlblMessageText
            // 
            this.kwlblMessageText.AutoSize = false;
            this.kwlblMessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblMessageText.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlblMessageText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlblMessageText.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblMessageText.Location = new System.Drawing.Point(0, 0);
            this.kwlblMessageText.Name = "kwlblMessageText";
            this.kwlblMessageText.Size = new System.Drawing.Size(203, 92);
            this.kwlblMessageText.Text = "Message Text";
            this.kwlblMessageText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // klwlblMessageText
            // 
            this.klwlblMessageText.AutoSize = false;
            this.klwlblMessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klwlblMessageText.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.klwlblMessageText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.klwlblMessageText.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.klwlblMessageText.Location = new System.Drawing.Point(0, 0);
            this.klwlblMessageText.Name = "klwlblMessageText";
            this.klwlblMessageText.Size = new System.Drawing.Size(203, 92);
            this.klwlblMessageText.Text = "Message Text";
            this.klwlblMessageText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VisualInformationBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 163);
            this.Controls.Add(this.tlpBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.FormTitleAlign = Krypton.Toolkit.PaletteRelativeAlign.Inherit;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualInformationBoxForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.tlpBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._panelContent)).EndInit();
            this._panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._panelFooter)).EndInit();
            this._panelFooter.ResumeLayout(false);
            this.tlpContent.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelButtons)).EndInit();
            this._panelButtons.ResumeLayout(false);
            this._panelButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContentArea)).EndInit();
            this.kpnlContentArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrAutoClose;
        private TableLayoutPanel tlpBase;
        private KryptonPanel _panelContent;
        private KryptonPanel _panelFooter;
        private TableLayoutPanel tlpContent;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonCheckBox _checkBox;
        private PictureBox pictureBox1;
        private KryptonPanel _panelButtons;
        private KryptonBorderEdge _borderEdge;
        private KryptonPanel kpnlContentArea;
        private KryptonWrapLabel kwlblMessageText;
        private KryptonLinkWrapLabel klwlblMessageText;
    }
}