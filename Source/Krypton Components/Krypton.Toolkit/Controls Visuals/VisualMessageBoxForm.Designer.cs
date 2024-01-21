#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2024. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    partial class VisualMessageBoxForm
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
            this._messageIcon = new System.Windows.Forms.PictureBox();
            this._panelButtons = new Krypton.Toolkit.KryptonPanel();
            this._borderEdge = new Krypton.Toolkit.KryptonBorderEdge();
            this._button4 = new Krypton.Toolkit.MessageButton();
            this._button3 = new Krypton.Toolkit.MessageButton();
            this._button1 = new Krypton.Toolkit.MessageButton();
            this._button2 = new Krypton.Toolkit.MessageButton();
            this._button5 = new Krypton.Toolkit.MessageButton();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpContentArea = new System.Windows.Forms.TableLayoutPanel();
            this.kchkMessageboxCheckBox = new Krypton.Toolkit.KryptonCheckBox();
            this.kpnlContentArea = new Krypton.Toolkit.KryptonPanel();
            this.kwlblMessageText = new Krypton.Toolkit.KryptonWrapLabel();
            this.klwlblMessageText = new Krypton.Toolkit.KryptonLinkWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this._messageIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelButtons)).BeginInit();
            this._panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpContentArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContentArea)).BeginInit();
            this.kpnlContentArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // _messageIcon
            // 
            this._messageIcon.BackColor = System.Drawing.Color.Transparent;
            this._messageIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this._messageIcon.Location = new System.Drawing.Point(8, 4);
            this._messageIcon.Margin = new System.Windows.Forms.Padding(8, 4, 4, 4);
            this._messageIcon.Name = "_messageIcon";
            this._messageIcon.Size = new System.Drawing.Size(33, 67);
            this._messageIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._messageIcon.TabIndex = 0;
            this._messageIcon.TabStop = false;
            // 
            // _panelButtons
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._panelButtons, 2);
            this._panelButtons.Controls.Add(this._borderEdge);
            this._panelButtons.Controls.Add(this._button4);
            this._panelButtons.Controls.Add(this._button3);
            this._panelButtons.Controls.Add(this._button1);
            this._panelButtons.Controls.Add(this._button2);
            this._panelButtons.Controls.Add(this._button5);
            this._panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelButtons.Location = new System.Drawing.Point(0, 75);
            this._panelButtons.Margin = new System.Windows.Forms.Padding(0);
            this._panelButtons.Name = "_panelButtons";
            this._panelButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this._panelButtons.Size = new System.Drawing.Size(191, 21);
            this._panelButtons.TabIndex = 0;
            // 
            // _borderEdge
            // 
            this._borderEdge.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this._borderEdge.Dock = System.Windows.Forms.DockStyle.Top;
            this._borderEdge.Location = new System.Drawing.Point(0, 0);
            this._borderEdge.Margin = new System.Windows.Forms.Padding(2);
            this._borderEdge.Name = "_borderEdge";
            this._borderEdge.Size = new System.Drawing.Size(191, 1);
            this._borderEdge.Text = "kryptonBorderEdge1";
            // 
            // _button4
            // 
            this._button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._button4.AutoSize = true;
            this._button4.Enabled = false;
            this._button4.IgnoreAltF4 = false;
            this._button4.Location = new System.Drawing.Point(191, 0);
            this._button4.Margin = new System.Windows.Forms.Padding(0);
            this._button4.MinimumSize = new System.Drawing.Size(38, 21);
            this._button4.Name = "_button4";
            this._button4.Size = new System.Drawing.Size(38, 23);
            this._button4.TabIndex = 2;
            this._button4.Values.Text = "B4";
            this._button4.Visible = false;
            // 
            // _button3
            // 
            this._button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._button3.AutoSize = true;
            this._button3.Enabled = false;
            this._button3.IgnoreAltF4 = false;
            this._button3.Location = new System.Drawing.Point(154, 0);
            this._button3.Margin = new System.Windows.Forms.Padding(0);
            this._button3.MinimumSize = new System.Drawing.Size(38, 21);
            this._button3.Name = "_button3";
            this._button3.Size = new System.Drawing.Size(38, 23);
            this._button3.TabIndex = 2;
            this._button3.Values.Text = "B3";
            this._button3.Visible = false;
            // 
            // _button1
            // 
            this._button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._button1.AutoSize = true;
            this._button1.Enabled = false;
            this._button1.IgnoreAltF4 = false;
            this._button1.Location = new System.Drawing.Point(78, 0);
            this._button1.Margin = new System.Windows.Forms.Padding(0);
            this._button1.MinimumSize = new System.Drawing.Size(38, 21);
            this._button1.Name = "_button1";
            this._button1.Size = new System.Drawing.Size(38, 23);
            this._button1.TabIndex = 0;
            this._button1.Values.Text = "B1";
            this._button1.Visible = false;
            // 
            // _button2
            // 
            this._button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._button2.AutoSize = true;
            this._button2.Enabled = false;
            this._button2.IgnoreAltF4 = false;
            this._button2.Location = new System.Drawing.Point(116, 0);
            this._button2.Margin = new System.Windows.Forms.Padding(0);
            this._button2.MinimumSize = new System.Drawing.Size(38, 21);
            this._button2.Name = "_button2";
            this._button2.Size = new System.Drawing.Size(38, 23);
            this._button2.TabIndex = 1;
            this._button2.Values.Text = "B2";
            this._button2.Visible = false;
            // 
            // _button5
            // 
            this._button5.AutoSize = true;
            this._button5.Enabled = false;
            this._button5.IgnoreAltF4 = false;
            this._button5.Location = new System.Drawing.Point(0, 0);
            this._button5.Margin = new System.Windows.Forms.Padding(0);
            this._button5.MinimumSize = new System.Drawing.Size(38, 21);
            this._button5.Name = "_button5";
            this._button5.Size = new System.Drawing.Size(38, 23);
            this._button5.TabIndex = 3;
            this._button5.Values.Text = "B5";
            this._button5.Visible = false;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(191, 96);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._panelButtons, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._messageIcon, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tlpContentArea, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(191, 96);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tlpContentArea
            // 
            this.tlpContentArea.ColumnCount = 1;
            this.tlpContentArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContentArea.Controls.Add(this.kchkMessageboxCheckBox, 0, 1);
            this.tlpContentArea.Controls.Add(this.kpnlContentArea, 0, 0);
            this.tlpContentArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContentArea.Location = new System.Drawing.Point(48, 3);
            this.tlpContentArea.Name = "tlpContentArea";
            this.tlpContentArea.RowCount = 2;
            this.tlpContentArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContentArea.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContentArea.Size = new System.Drawing.Size(140, 69);
            this.tlpContentArea.TabIndex = 1;
            // 
            // kchkMessageboxCheckBox
            // 
            this.kchkMessageboxCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kchkMessageboxCheckBox.Location = new System.Drawing.Point(3, 46);
            this.kchkMessageboxCheckBox.Name = "kchkMessageboxCheckBox";
            this.kchkMessageboxCheckBox.Size = new System.Drawing.Size(134, 20);
            this.kchkMessageboxCheckBox.TabIndex = 0;
            this.kchkMessageboxCheckBox.Values.Text = "kryptonCheckBox1";
            // 
            // kpnlContentArea
            // 
            this.kpnlContentArea.Controls.Add(this.kwlblMessageText);
            this.kpnlContentArea.Controls.Add(this.klwlblMessageText);
            this.kpnlContentArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlContentArea.Location = new System.Drawing.Point(3, 3);
            this.kpnlContentArea.Name = "kpnlContentArea";
            this.kpnlContentArea.Size = new System.Drawing.Size(134, 37);
            this.kpnlContentArea.TabIndex = 1;
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
            this.kwlblMessageText.Size = new System.Drawing.Size(134, 37);
            this.kwlblMessageText.Text = "kryptonWrapLabel1";
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
            this.klwlblMessageText.Size = new System.Drawing.Size(134, 37);
            this.klwlblMessageText.Text = "kryptonLinkWrapLabel1";
            this.klwlblMessageText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VisualMessageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 96);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualMessageBoxForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this._messageIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelButtons)).EndInit();
            this._panelButtons.ResumeLayout(false);
            this._panelButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tlpContentArea.ResumeLayout(false);
            this.tlpContentArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContentArea)).EndInit();
            this.kpnlContentArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private PictureBox _messageIcon;
        private KryptonPanel _panelButtons;
        private MessageButton _button1;
        private MessageButton _button2;
        private MessageButton _button3;
        private MessageButton _button4;
        private KryptonBorderEdge _borderEdge;
        private KryptonPanel kryptonPanel1;
        private TableLayoutPanel tableLayoutPanel1;
        private MessageButton _button5;
        private TableLayoutPanel tlpContentArea;
        private KryptonCheckBox kchkMessageboxCheckBox;
        private KryptonPanel kpnlContentArea;
        private KryptonLinkWrapLabel klwlblMessageText;
        private KryptonWrapLabel kwlblMessageText;
    }
}