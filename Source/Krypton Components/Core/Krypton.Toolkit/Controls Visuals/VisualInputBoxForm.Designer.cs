namespace Krypton.Toolkit
{
    partial class VisualInputBoxForm
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
            this._panelMessage = new Krypton.Toolkit.KryptonPanel();
            this._tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._labelPrompt = new Krypton.Toolkit.KryptonWrapLabel();
            this._textBoxResponse = new Krypton.Toolkit.KryptonTextBox();
            this._kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this._kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this._buttonOk = new Krypton.Toolkit.KryptonButton();
            this._buttonCancel = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this._panelMessage)).BeginInit();
            this._panelMessage.SuspendLayout();
            this._tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonPanel1)).BeginInit();
            this._kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            //
            // _panelMessage
            //
            this._panelMessage.Controls.Add(this._tableLayoutPanel1);
            this._panelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelMessage.Location = new System.Drawing.Point(0, 0);
            this._panelMessage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._panelMessage.Name = "_panelMessage";
            this._panelMessage.Size = new System.Drawing.Size(487, 131);
            this._panelMessage.TabIndex = 1;
            //
            // _tableLayoutPanel1
            //
            this._tableLayoutPanel1.AutoSize = true;
            this._tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._tableLayoutPanel1.ColumnCount = 1;
            this._tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tableLayoutPanel1.Controls.Add(this._labelPrompt, 0, 0);
            this._tableLayoutPanel1.Controls.Add(this._textBoxResponse, 0, 1);
            this._tableLayoutPanel1.Controls.Add(this._kryptonBorderEdge1, 0, 2);
            this._tableLayoutPanel1.Controls.Add(this._kryptonPanel1, 0, 3);
            this._tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this._tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this._tableLayoutPanel1.Name = "_tableLayoutPanel1";
            this._tableLayoutPanel1.RowCount = 4;
            this._tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutPanel1.Size = new System.Drawing.Size(467, 137);
            this._tableLayoutPanel1.TabIndex = 3;
            //
            // _labelPrompt
            //
            this._labelPrompt.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this._labelPrompt.Location = new System.Drawing.Point(5, 5);
            this._labelPrompt.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this._labelPrompt.Name = "_labelPrompt";
            this._labelPrompt.Size = new System.Drawing.Size(58, 20);
            this._labelPrompt.Text = "Prompt";
            //
            // _textBoxResponse
            //
            this._textBoxResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textBoxResponse.Location = new System.Drawing.Point(11, 34);
            this._textBoxResponse.Margin = new System.Windows.Forms.Padding(11, 4, 11, 4);
            this._textBoxResponse.MinimumSize = new System.Drawing.Size(444, 27);
            this._textBoxResponse.Name = "_textBoxResponse";
            this._textBoxResponse.Size = new System.Drawing.Size(445, 27);
            this._textBoxResponse.TabIndex = 0;
            this._textBoxResponse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Response_KeyDown);
            //
            // _kryptonBorderEdge1
            //
            this._kryptonBorderEdge1.AutoSize = false;
            this._kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._kryptonBorderEdge1.Location = new System.Drawing.Point(0, 75);
            this._kryptonBorderEdge1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this._kryptonBorderEdge1.Name = "_kryptonBorderEdge1";
            this._kryptonBorderEdge1.Size = new System.Drawing.Size(467, 1);
            this._kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            //
            // _kryptonPanel1
            //
            this._kryptonPanel1.Controls.Add(this._buttonOk);
            this._kryptonPanel1.Controls.Add(this._buttonCancel);
            this._kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._kryptonPanel1.Location = new System.Drawing.Point(3, 88);
            this._kryptonPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 10);
            this._kryptonPanel1.Name = "_kryptonPanel1";
            this._kryptonPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this._kryptonPanel1.Size = new System.Drawing.Size(461, 39);
            this._kryptonPanel1.TabIndex = 2;
            //
            // _buttonOk
            //
            this._buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonOk.AutoSize = true;
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(235, 1);
            this._buttonOk.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this._buttonOk.MinimumSize = new System.Drawing.Size(100, 32);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(100, 32);
            this._buttonOk.TabIndex = 1;
            this._buttonOk.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._buttonOk.Values.Text = "&OK";
            //
            // _buttonCancel
            //
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.AutoSize = true;
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(353, 1);
            this._buttonCancel.Margin = new System.Windows.Forms.Padding(0);
            this._buttonCancel.MinimumSize = new System.Drawing.Size(100, 32);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(100, 32);
            this._buttonCancel.TabIndex = 2;
            this._buttonCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._buttonCancel.Values.Text = "Cance&l";
            //
            // VisualInputBoxForm
            //
            this.AcceptButton = this._buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(487, 131);
            this.Controls.Add(this._panelMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualInputBoxForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            ((System.ComponentModel.ISupportInitialize)(this._panelMessage)).EndInit();
            this._panelMessage.ResumeLayout(false);
            this._panelMessage.PerformLayout();
            this._tableLayoutPanel1.ResumeLayout(false);
            this._tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonPanel1)).EndInit();
            this._kryptonPanel1.ResumeLayout(false);
            this._kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private KryptonPanel _panelMessage;
        private TableLayoutPanel _tableLayoutPanel1;
        private KryptonWrapLabel _labelPrompt;
        private KryptonTextBox _textBoxResponse;
        private KryptonBorderEdge _kryptonBorderEdge1;
        private KryptonPanel _kryptonPanel1;
        private KryptonButton _buttonOk;
        private KryptonButton _buttonCancel;
    }
}