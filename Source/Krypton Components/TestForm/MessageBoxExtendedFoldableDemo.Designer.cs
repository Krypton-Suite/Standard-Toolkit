namespace TestForm
{
    partial class MessageBoxExtendedFoldableDemo
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
            this.klblResult = new Krypton.Toolkit.KryptonLabel();
            this.kbtnJitPreset = new Krypton.Toolkit.KryptonButton();
            this.kbtnShow = new Krypton.Toolkit.KryptonButton();
            this.kchkExpanded = new Krypton.Toolkit.KryptonCheckBox();
            this.knudRtbHeight = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblRtbHeight = new Krypton.Toolkit.KryptonLabel();
            this.kcmbContentType = new Krypton.Toolkit.KryptonComboBox();
            this.klblContentType = new Krypton.Toolkit.KryptonLabel();
            this.kcmbButtons = new Krypton.Toolkit.KryptonComboBox();
            this.klblButtons = new Krypton.Toolkit.KryptonLabel();
            this.kcmbIcon = new Krypton.Toolkit.KryptonComboBox();
            this.klblIcon = new Krypton.Toolkit.KryptonLabel();
            this.ktxtFooter = new Krypton.Toolkit.KryptonTextBox();
            this.klblFooter = new Krypton.Toolkit.KryptonLabel();
            this.ktxtMessage = new Krypton.Toolkit.KryptonTextBox();
            this.klblMessage = new Krypton.Toolkit.KryptonLabel();
            this.ktxtCaption = new Krypton.Toolkit.KryptonTextBox();
            this.klblCaption = new Krypton.Toolkit.KryptonLabel();
            this.klblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbContentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.klblResult);
            this.kpnlMain.Controls.Add(this.kbtnJitPreset);
            this.kpnlMain.Controls.Add(this.kbtnShow);
            this.kpnlMain.Controls.Add(this.kchkExpanded);
            this.kpnlMain.Controls.Add(this.knudRtbHeight);
            this.kpnlMain.Controls.Add(this.klblRtbHeight);
            this.kpnlMain.Controls.Add(this.kcmbContentType);
            this.kpnlMain.Controls.Add(this.klblContentType);
            this.kpnlMain.Controls.Add(this.kcmbButtons);
            this.kpnlMain.Controls.Add(this.klblButtons);
            this.kpnlMain.Controls.Add(this.kcmbIcon);
            this.kpnlMain.Controls.Add(this.klblIcon);
            this.kpnlMain.Controls.Add(this.ktxtFooter);
            this.kpnlMain.Controls.Add(this.klblFooter);
            this.kpnlMain.Controls.Add(this.ktxtMessage);
            this.kpnlMain.Controls.Add(this.klblMessage);
            this.kpnlMain.Controls.Add(this.ktxtCaption);
            this.kpnlMain.Controls.Add(this.klblCaption);
            this.kpnlMain.Controls.Add(this.klblInstructions);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(12);
            this.kpnlMain.Size = new System.Drawing.Size(644, 545);
            this.kpnlMain.TabIndex = 0;
            // 
            // klblResult
            // 
            this.klblResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.klblResult.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblResult.Location = new System.Drawing.Point(12, 500);
            this.klblResult.Name = "klblResult";
            this.klblResult.Size = new System.Drawing.Size(83, 20);
            this.klblResult.TabIndex = 18;
            this.klblResult.Values.Text = "Last result: -";
            // 
            // kbtnJitPreset
            // 
            this.kbtnJitPreset.Location = new System.Drawing.Point(312, 460);
            this.kbtnJitPreset.Name = "kbtnJitPreset";
            this.kbtnJitPreset.Size = new System.Drawing.Size(190, 30);
            this.kbtnJitPreset.TabIndex = 17;
            this.kbtnJitPreset.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnJitPreset.Values.Text = "JIT Preset (data model)";
            this.kbtnJitPreset.Click += new System.EventHandler(this.kbtnJitPreset_Click);
            // 
            // kbtnShow
            // 
            this.kbtnShow.Location = new System.Drawing.Point(160, 460);
            this.kbtnShow.Name = "kbtnShow";
            this.kbtnShow.Size = new System.Drawing.Size(140, 30);
            this.kbtnShow.TabIndex = 16;
            this.kbtnShow.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShow.Values.Text = "Show Message Box";
            this.kbtnShow.Click += new System.EventHandler(this.kbtnShow_Click);
            // 
            // kchkExpanded
            // 
            this.kchkExpanded.Location = new System.Drawing.Point(160, 428);
            this.kchkExpanded.Name = "kchkExpanded";
            this.kchkExpanded.Size = new System.Drawing.Size(215, 20);
            this.kchkExpanded.TabIndex = 15;
            this.kchkExpanded.Values.Text = "Show details expanded on open";
            // 
            // knudRtbHeight
            // 
            this.knudRtbHeight.Location = new System.Drawing.Point(160, 394);
            this.knudRtbHeight.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.knudRtbHeight.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.knudRtbHeight.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.knudRtbHeight.Name = "knudRtbHeight";
            this.knudRtbHeight.Size = new System.Drawing.Size(90, 22);
            this.knudRtbHeight.TabIndex = 14;
            this.knudRtbHeight.Value = new decimal(new int[] {
            160,
            0,
            0,
            0});
            // 
            // klblRtbHeight
            // 
            this.klblRtbHeight.Location = new System.Drawing.Point(12, 394);
            this.klblRtbHeight.Name = "klblRtbHeight";
            this.klblRtbHeight.Size = new System.Drawing.Size(142, 20);
            this.klblRtbHeight.TabIndex = 13;
            this.klblRtbHeight.Values.Text = "RichTextBox height (px):";
            // 
            // kcmbContentType
            // 
            this.kcmbContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbContentType.DropDownWidth = 200;
            this.kcmbContentType.IntegralHeight = false;
            this.kcmbContentType.Location = new System.Drawing.Point(160, 362);
            this.kcmbContentType.Name = "kcmbContentType";
            this.kcmbContentType.Size = new System.Drawing.Size(200, 22);
            this.kcmbContentType.TabIndex = 12;
            // 
            // klblContentType
            // 
            this.klblContentType.Location = new System.Drawing.Point(12, 362);
            this.klblContentType.Name = "klblContentType";
            this.klblContentType.Size = new System.Drawing.Size(126, 20);
            this.klblContentType.TabIndex = 11;
            this.klblContentType.Values.Text = "Footer content type:";
            // 
            // kcmbButtons
            // 
            this.kcmbButtons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbButtons.DropDownWidth = 200;
            this.kcmbButtons.IntegralHeight = false;
            this.kcmbButtons.Location = new System.Drawing.Point(160, 330);
            this.kcmbButtons.Name = "kcmbButtons";
            this.kcmbButtons.Size = new System.Drawing.Size(200, 22);
            this.kcmbButtons.TabIndex = 10;
            // 
            // klblButtons
            // 
            this.klblButtons.Location = new System.Drawing.Point(12, 330);
            this.klblButtons.Name = "klblButtons";
            this.klblButtons.Size = new System.Drawing.Size(56, 20);
            this.klblButtons.TabIndex = 9;
            this.klblButtons.Values.Text = "Buttons:";
            // 
            // kcmbIcon
            // 
            this.kcmbIcon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbIcon.DropDownWidth = 200;
            this.kcmbIcon.IntegralHeight = false;
            this.kcmbIcon.Location = new System.Drawing.Point(160, 298);
            this.kcmbIcon.Name = "kcmbIcon";
            this.kcmbIcon.Size = new System.Drawing.Size(200, 22);
            this.kcmbIcon.TabIndex = 8;
            // 
            // klblIcon
            // 
            this.klblIcon.Location = new System.Drawing.Point(12, 298);
            this.klblIcon.Name = "klblIcon";
            this.klblIcon.Size = new System.Drawing.Size(38, 20);
            this.klblIcon.TabIndex = 7;
            this.klblIcon.Values.Text = "Icon:";
            // 
            // ktxtFooter
            // 
            this.ktxtFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtFooter.Location = new System.Drawing.Point(160, 190);
            this.ktxtFooter.Multiline = true;
            this.ktxtFooter.Name = "ktxtFooter";
            this.ktxtFooter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ktxtFooter.Size = new System.Drawing.Size(472, 92);
            this.ktxtFooter.TabIndex = 6;
            // 
            // klblFooter
            // 
            this.klblFooter.Location = new System.Drawing.Point(12, 190);
            this.klblFooter.Name = "klblFooter";
            this.klblFooter.Size = new System.Drawing.Size(133, 20);
            this.klblFooter.TabIndex = 5;
            this.klblFooter.Values.Text = "Footer / details text:";
            // 
            // ktxtMessage
            // 
            this.ktxtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtMessage.Location = new System.Drawing.Point(160, 94);
            this.ktxtMessage.Multiline = true;
            this.ktxtMessage.Name = "ktxtMessage";
            this.ktxtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ktxtMessage.Size = new System.Drawing.Size(472, 88);
            this.ktxtMessage.TabIndex = 4;
            // 
            // klblMessage
            // 
            this.klblMessage.Location = new System.Drawing.Point(12, 94);
            this.klblMessage.Name = "klblMessage";
            this.klblMessage.Size = new System.Drawing.Size(64, 20);
            this.klblMessage.TabIndex = 3;
            this.klblMessage.Values.Text = "Message:";
            // 
            // ktxtCaption
            // 
            this.ktxtCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtCaption.Location = new System.Drawing.Point(160, 62);
            this.ktxtCaption.Name = "ktxtCaption";
            this.ktxtCaption.Size = new System.Drawing.Size(472, 23);
            this.ktxtCaption.TabIndex = 2;
            // 
            // klblCaption
            // 
            this.klblCaption.Location = new System.Drawing.Point(12, 62);
            this.klblCaption.Name = "klblCaption";
            this.klblCaption.Size = new System.Drawing.Size(60, 20);
            this.klblCaption.TabIndex = 1;
            this.klblCaption.Values.Text = "Caption:";
            // 
            // klblInstructions
            // 
            this.klblInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.klblInstructions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.klblInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.klblInstructions.Location = new System.Drawing.Point(12, 12);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(620, 40);
            this.klblInstructions.Text = "Configure the message box and its optional expandable footer, then click \"Show Me" +
    "ssage Box\". When footer text is set (or the content type is CheckBox), a Show/Hid" +
    "e details toggle folds the footer region in and out and resizes the message box.";
            // 
            // MessageBoxExtendedFoldableDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 545);
            this.Controls.Add(this.kpnlMain);
            this.MinimumSize = new System.Drawing.Size(560, 500);
            this.Name = "MessageBoxExtendedFoldableDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message Box Extended - Foldable Footer Demo";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.kpnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbContentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbButtons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private Krypton.Toolkit.KryptonWrapLabel klblInstructions;
        private Krypton.Toolkit.KryptonLabel klblCaption;
        private Krypton.Toolkit.KryptonTextBox ktxtCaption;
        private Krypton.Toolkit.KryptonLabel klblMessage;
        private Krypton.Toolkit.KryptonTextBox ktxtMessage;
        private Krypton.Toolkit.KryptonLabel klblFooter;
        private Krypton.Toolkit.KryptonTextBox ktxtFooter;
        private Krypton.Toolkit.KryptonLabel klblIcon;
        private Krypton.Toolkit.KryptonComboBox kcmbIcon;
        private Krypton.Toolkit.KryptonLabel klblButtons;
        private Krypton.Toolkit.KryptonComboBox kcmbButtons;
        private Krypton.Toolkit.KryptonLabel klblContentType;
        private Krypton.Toolkit.KryptonComboBox kcmbContentType;
        private Krypton.Toolkit.KryptonLabel klblRtbHeight;
        private Krypton.Toolkit.KryptonNumericUpDown knudRtbHeight;
        private Krypton.Toolkit.KryptonCheckBox kchkExpanded;
        private Krypton.Toolkit.KryptonButton kbtnShow;
        private Krypton.Toolkit.KryptonButton kbtnJitPreset;
        private Krypton.Toolkit.KryptonLabel klblResult;
    }
}
