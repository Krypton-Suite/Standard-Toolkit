namespace TestForm
{
    partial class FoldableDialogDemo
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
            this.kcmbStartPosition = new Krypton.Toolkit.KryptonComboBox();
            this.klblStartPosition = new Krypton.Toolkit.KryptonLabel();
            this.kcmbDefault = new Krypton.Toolkit.KryptonComboBox();
            this.klblDefault = new Krypton.Toolkit.KryptonLabel();
            this.kcmbButtons = new Krypton.Toolkit.KryptonComboBox();
            this.klblButtons = new Krypton.Toolkit.KryptonLabel();
            this.kcmbIcon = new Krypton.Toolkit.KryptonComboBox();
            this.klblIcon = new Krypton.Toolkit.KryptonLabel();
            this.ktxtDetails = new Krypton.Toolkit.KryptonTextBox();
            this.klblDetails = new Krypton.Toolkit.KryptonLabel();
            this.ktxtMessage = new Krypton.Toolkit.KryptonTextBox();
            this.klblMessage = new Krypton.Toolkit.KryptonLabel();
            this.ktxtHeading = new Krypton.Toolkit.KryptonTextBox();
            this.klblHeading = new Krypton.Toolkit.KryptonLabel();
            this.ktxtCaption = new Krypton.Toolkit.KryptonTextBox();
            this.klblCaption = new Krypton.Toolkit.KryptonLabel();
            this.klblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbStartPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbDefault)).BeginInit();
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
            this.kpnlMain.Controls.Add(this.kcmbStartPosition);
            this.kpnlMain.Controls.Add(this.klblStartPosition);
            this.kpnlMain.Controls.Add(this.kcmbDefault);
            this.kpnlMain.Controls.Add(this.klblDefault);
            this.kpnlMain.Controls.Add(this.kcmbButtons);
            this.kpnlMain.Controls.Add(this.klblButtons);
            this.kpnlMain.Controls.Add(this.kcmbIcon);
            this.kpnlMain.Controls.Add(this.klblIcon);
            this.kpnlMain.Controls.Add(this.ktxtDetails);
            this.kpnlMain.Controls.Add(this.klblDetails);
            this.kpnlMain.Controls.Add(this.ktxtMessage);
            this.kpnlMain.Controls.Add(this.klblMessage);
            this.kpnlMain.Controls.Add(this.ktxtHeading);
            this.kpnlMain.Controls.Add(this.klblHeading);
            this.kpnlMain.Controls.Add(this.ktxtCaption);
            this.kpnlMain.Controls.Add(this.klblCaption);
            this.kpnlMain.Controls.Add(this.klblInstructions);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(12);
            this.kpnlMain.Size = new System.Drawing.Size(644, 593);
            this.kpnlMain.TabIndex = 0;
            // 
            // klblResult
            // 
            this.klblResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.klblResult.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblResult.Location = new System.Drawing.Point(12, 496);
            this.klblResult.Name = "klblResult";
            this.klblResult.Size = new System.Drawing.Size(83, 20);
            this.klblResult.TabIndex = 20;
            this.klblResult.Values.Text = "Last result: -";
            // 
            // kbtnJitPreset
            // 
            this.kbtnJitPreset.Location = new System.Drawing.Point(312, 448);
            this.kbtnJitPreset.Name = "kbtnJitPreset";
            this.kbtnJitPreset.Size = new System.Drawing.Size(190, 30);
            this.kbtnJitPreset.TabIndex = 19;
            this.kbtnJitPreset.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnJitPreset.Values.Text = "Show JIT-style Preset";
            this.kbtnJitPreset.Click += new System.EventHandler(this.kbtnJitPreset_Click);
            // 
            // kbtnShow
            // 
            this.kbtnShow.Location = new System.Drawing.Point(160, 448);
            this.kbtnShow.Name = "kbtnShow";
            this.kbtnShow.Size = new System.Drawing.Size(140, 30);
            this.kbtnShow.TabIndex = 18;
            this.kbtnShow.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShow.Values.Text = "Show Dialog";
            this.kbtnShow.Click += new System.EventHandler(this.kbtnShow_Click);
            // 
            // kchkExpanded
            // 
            this.kchkExpanded.Location = new System.Drawing.Point(160, 416);
            this.kchkExpanded.Name = "kchkExpanded";
            this.kchkExpanded.Size = new System.Drawing.Size(215, 20);
            this.kchkExpanded.TabIndex = 17;
            this.kchkExpanded.Values.Text = "Show details expanded on open";
            // 
            // kcmbStartPosition
            // 
            this.kcmbStartPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbStartPosition.DropDownWidth = 200;
            this.kcmbStartPosition.IntegralHeight = false;
            this.kcmbStartPosition.Location = new System.Drawing.Point(160, 382);
            this.kcmbStartPosition.Name = "kcmbStartPosition";
            this.kcmbStartPosition.Size = new System.Drawing.Size(200, 22);
            this.kcmbStartPosition.TabIndex = 16;
            // 
            // klblStartPosition
            // 
            this.klblStartPosition.Location = new System.Drawing.Point(12, 382);
            this.klblStartPosition.Name = "klblStartPosition";
            this.klblStartPosition.Size = new System.Drawing.Size(90, 20);
            this.klblStartPosition.TabIndex = 15;
            this.klblStartPosition.Values.Text = "Start position:";
            // 
            // kcmbDefault
            // 
            this.kcmbDefault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbDefault.DropDownWidth = 200;
            this.kcmbDefault.IntegralHeight = false;
            this.kcmbDefault.Location = new System.Drawing.Point(160, 350);
            this.kcmbDefault.Name = "kcmbDefault";
            this.kcmbDefault.Size = new System.Drawing.Size(200, 22);
            this.kcmbDefault.TabIndex = 14;
            // 
            // klblDefault
            // 
            this.klblDefault.Location = new System.Drawing.Point(12, 350);
            this.klblDefault.Name = "klblDefault";
            this.klblDefault.Size = new System.Drawing.Size(96, 20);
            this.klblDefault.TabIndex = 13;
            this.klblDefault.Values.Text = "Default button:";
            // 
            // kcmbButtons
            // 
            this.kcmbButtons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbButtons.DropDownWidth = 200;
            this.kcmbButtons.IntegralHeight = false;
            this.kcmbButtons.Location = new System.Drawing.Point(160, 318);
            this.kcmbButtons.Name = "kcmbButtons";
            this.kcmbButtons.Size = new System.Drawing.Size(200, 22);
            this.kcmbButtons.TabIndex = 12;
            // 
            // klblButtons
            // 
            this.klblButtons.Location = new System.Drawing.Point(12, 318);
            this.klblButtons.Name = "klblButtons";
            this.klblButtons.Size = new System.Drawing.Size(56, 20);
            this.klblButtons.TabIndex = 11;
            this.klblButtons.Values.Text = "Buttons:";
            // 
            // kcmbIcon
            // 
            this.kcmbIcon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbIcon.DropDownWidth = 200;
            this.kcmbIcon.IntegralHeight = false;
            this.kcmbIcon.Location = new System.Drawing.Point(160, 286);
            this.kcmbIcon.Name = "kcmbIcon";
            this.kcmbIcon.Size = new System.Drawing.Size(200, 22);
            this.kcmbIcon.TabIndex = 10;
            // 
            // klblIcon
            // 
            this.klblIcon.Location = new System.Drawing.Point(12, 286);
            this.klblIcon.Name = "klblIcon";
            this.klblIcon.Size = new System.Drawing.Size(38, 20);
            this.klblIcon.TabIndex = 9;
            this.klblIcon.Values.Text = "Icon:";
            // 
            // ktxtDetails
            // 
            this.ktxtDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtDetails.Location = new System.Drawing.Point(160, 182);
            this.ktxtDetails.Multiline = true;
            this.ktxtDetails.Name = "ktxtDetails";
            this.ktxtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ktxtDetails.Size = new System.Drawing.Size(472, 92);
            this.ktxtDetails.TabIndex = 8;
            // 
            // klblDetails
            // 
            this.klblDetails.Location = new System.Drawing.Point(12, 182);
            this.klblDetails.Name = "klblDetails";
            this.klblDetails.Size = new System.Drawing.Size(96, 20);
            this.klblDetails.TabIndex = 7;
            this.klblDetails.Values.Text = "Details (folded):";
            // 
            // ktxtMessage
            // 
            this.ktxtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtMessage.Location = new System.Drawing.Point(160, 126);
            this.ktxtMessage.Multiline = true;
            this.ktxtMessage.Name = "ktxtMessage";
            this.ktxtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ktxtMessage.Size = new System.Drawing.Size(472, 48);
            this.ktxtMessage.TabIndex = 6;
            // 
            // klblMessage
            // 
            this.klblMessage.Location = new System.Drawing.Point(12, 126);
            this.klblMessage.Name = "klblMessage";
            this.klblMessage.Size = new System.Drawing.Size(64, 20);
            this.klblMessage.TabIndex = 5;
            this.klblMessage.Values.Text = "Message:";
            // 
            // ktxtHeading
            // 
            this.ktxtHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtHeading.Location = new System.Drawing.Point(160, 94);
            this.ktxtHeading.Name = "ktxtHeading";
            this.ktxtHeading.Size = new System.Drawing.Size(472, 23);
            this.ktxtHeading.TabIndex = 4;
            // 
            // klblHeading
            // 
            this.klblHeading.Location = new System.Drawing.Point(12, 94);
            this.klblHeading.Name = "klblHeading";
            this.klblHeading.Size = new System.Drawing.Size(63, 20);
            this.klblHeading.TabIndex = 3;
            this.klblHeading.Values.Text = "Heading:";
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
            this.klblInstructions.Text = "Configure the content below and click \"Show Dialog\". When the details text is set" +
    ", an expander (Show/Hide details) folds the details region in and out and resize" +
    "s the dialog. \"Show JIT-style Preset\" recreates a Visual Studio Just-In-Time debugger dialog.";
            // 
            // FoldableDialogDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 593);
            this.Controls.Add(this.kpnlMain);
            this.MinimumSize = new System.Drawing.Size(560, 512);
            this.Name = "FoldableDialogDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Foldable Dialog Demo (Issue #3840)";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.kpnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbStartPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbDefault)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbButtons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private Krypton.Toolkit.KryptonWrapLabel klblInstructions;
        private Krypton.Toolkit.KryptonLabel klblCaption;
        private Krypton.Toolkit.KryptonTextBox ktxtCaption;
        private Krypton.Toolkit.KryptonLabel klblHeading;
        private Krypton.Toolkit.KryptonTextBox ktxtHeading;
        private Krypton.Toolkit.KryptonLabel klblMessage;
        private Krypton.Toolkit.KryptonTextBox ktxtMessage;
        private Krypton.Toolkit.KryptonLabel klblDetails;
        private Krypton.Toolkit.KryptonTextBox ktxtDetails;
        private Krypton.Toolkit.KryptonLabel klblIcon;
        private Krypton.Toolkit.KryptonComboBox kcmbIcon;
        private Krypton.Toolkit.KryptonLabel klblButtons;
        private Krypton.Toolkit.KryptonComboBox kcmbButtons;
        private Krypton.Toolkit.KryptonLabel klblDefault;
        private Krypton.Toolkit.KryptonComboBox kcmbDefault;
        private Krypton.Toolkit.KryptonLabel klblStartPosition;
        private Krypton.Toolkit.KryptonComboBox kcmbStartPosition;
        private Krypton.Toolkit.KryptonCheckBox kchkExpanded;
        private Krypton.Toolkit.KryptonButton kbtnShow;
        private Krypton.Toolkit.KryptonButton kbtnJitPreset;
        private Krypton.Toolkit.KryptonLabel klblResult;
    }
}
