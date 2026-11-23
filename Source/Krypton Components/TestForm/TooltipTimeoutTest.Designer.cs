namespace TestForm
{
    partial class TooltipTimeoutTest
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
            this.kpnlContent = new Krypton.Toolkit.KryptonPanel();
            this.kpgbDemoControls = new Krypton.Toolkit.KryptonGroupBox();
            this.kpnlDemoControls = new Krypton.Toolkit.KryptonPanel();
            this.kchkDemoCheck = new Krypton.Toolkit.KryptonCheckBox();
            this.ktxtDemoInput = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnBuiltInTooltip = new Krypton.Toolkit.KryptonButton();
            this.klblDemoInstructions = new Krypton.Toolkit.KryptonLabel();
            this.kpgbConfiguration = new Krypton.Toolkit.KryptonGroupBox();
            this.kpnlConfig = new Krypton.Toolkit.KryptonPanel();
            this.kbtnApply = new Krypton.Toolkit.KryptonButton();
            this.klblCurrentMode = new Krypton.Toolkit.KryptonLabel();
            this.klblCurrentModeLabel = new Krypton.Toolkit.KryptonLabel();
            this.kcmbTimeoutMode = new Krypton.Toolkit.KryptonComboBox();
            this.klblTimeoutMode = new Krypton.Toolkit.KryptonLabel();
            this.klblDescription = new Krypton.Toolkit.KryptonLabel();
            this.kpnlBottom = new Krypton.Toolkit.KryptonPanel();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContent)).BeginInit();
            this.kpnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbDemoControls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbDemoControls.Panel)).BeginInit();
            this.kpgbDemoControls.Panel.SuspendLayout();
            this.kpgbDemoControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlDemoControls)).BeginInit();
            this.kpnlDemoControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbConfiguration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbConfiguration.Panel)).BeginInit();
            this.kpgbConfiguration.Panel.SuspendLayout();
            this.kpgbConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlConfig)).BeginInit();
            this.kpnlConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTimeoutMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlBottom)).BeginInit();
            this.kpnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.kpnlContent);
            this.kpnlMain.Controls.Add(this.kpnlBottom);
            this.kpnlMain.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Size = new System.Drawing.Size(584, 370);
            this.kpnlMain.TabIndex = 0;
            // 
            // kpnlContent
            // 
            this.kpnlContent.AutoScroll = true;
            this.kpnlContent.Controls.Add(this.kpgbDemoControls);
            this.kpnlContent.Controls.Add(this.kpgbConfiguration);
            this.kpnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlContent.Location = new System.Drawing.Point(0, 0);
            this.kpnlContent.Name = "kpnlContent";
            this.kpnlContent.Padding = new System.Windows.Forms.Padding(10);
            this.kpnlContent.Size = new System.Drawing.Size(584, 318);
            this.kpnlContent.TabIndex = 1;
            // 
            // kpgbDemoControls
            // 
            this.kpgbDemoControls.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpgbDemoControls.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kpgbDemoControls.Location = new System.Drawing.Point(13, 180);
            // 
            // kpgbDemoControls.Panel
            // 
            this.kpgbDemoControls.Panel.Controls.Add(this.kpnlDemoControls);
            this.kpgbDemoControls.Panel.Controls.Add(this.klblDemoInstructions);
            this.kpgbDemoControls.Size = new System.Drawing.Size(558, 132);
            this.kpgbDemoControls.TabIndex = 1;
            this.kpgbDemoControls.Values.Heading = "Demo Controls – Hover to See Tooltip Behavior";
            // 
            // kpnlDemoControls
            // 
            this.kpnlDemoControls.Controls.Add(this.kchkDemoCheck);
            this.kpnlDemoControls.Controls.Add(this.ktxtDemoInput);
            this.kpnlDemoControls.Controls.Add(this.kbtnBuiltInTooltip);
            this.kpnlDemoControls.Location = new System.Drawing.Point(10, 45);
            this.kpnlDemoControls.Name = "kpnlDemoControls";
            this.kpnlDemoControls.Size = new System.Drawing.Size(536, 60);
            this.kpnlDemoControls.TabIndex = 1;
            // 
            // kchkDemoCheck
            // 
            this.kchkDemoCheck.Location = new System.Drawing.Point(309, 18);
            this.kchkDemoCheck.Name = "kchkDemoCheck";
            this.kchkDemoCheck.Size = new System.Drawing.Size(77, 20);
            this.kchkDemoCheck.TabIndex = 3;
            this.kchkDemoCheck.Values.Text = "Checkbox";
            // 
            // ktxtDemoInput
            // 
            this.ktxtDemoInput.Location = new System.Drawing.Point(109, 20);
            this.ktxtDemoInput.Name = "ktxtDemoInput";
            this.ktxtDemoInput.Size = new System.Drawing.Size(190, 23);
            this.ktxtDemoInput.TabIndex = 2;
            this.ktxtDemoInput.Text = "Text input – hover for tooltip";
            // 
            // kbtnBuiltInTooltip
            // 
            this.kbtnBuiltInTooltip.Location = new System.Drawing.Point(9, 18);
            this.kbtnBuiltInTooltip.Name = "kbtnBuiltInTooltip";
            this.kbtnBuiltInTooltip.Size = new System.Drawing.Size(90, 28);
            this.kbtnBuiltInTooltip.TabIndex = 1;
            this.kbtnBuiltInTooltip.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnBuiltInTooltip.Values.Text = "ToolTipValues";
            // 
            // klblDemoInstructions
            // 
            this.klblDemoInstructions.Location = new System.Drawing.Point(10, 10);
            this.klblDemoInstructions.Name = "klblDemoInstructions";
            this.klblDemoInstructions.Size = new System.Drawing.Size(241, 20);
            this.klblDemoInstructions.TabIndex = 0;
            this.klblDemoInstructions.Values.Text = "Hover over each control to see the tooltip.";
            // 
            // kpgbConfiguration
            // 
            this.kpgbConfiguration.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpgbConfiguration.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kpgbConfiguration.Location = new System.Drawing.Point(13, 13);
            // 
            // kpgbConfiguration.Panel
            // 
            this.kpgbConfiguration.Panel.Controls.Add(this.kpnlConfig);
            this.kpgbConfiguration.Size = new System.Drawing.Size(558, 154);
            this.kpgbConfiguration.TabIndex = 0;
            this.kpgbConfiguration.Values.Heading = "Tooltip Timeout Configuration (Issue #3075)";
            // 
            // kpnlConfig
            // 
            this.kpnlConfig.Controls.Add(this.kbtnApply);
            this.kpnlConfig.Controls.Add(this.klblCurrentMode);
            this.kpnlConfig.Controls.Add(this.klblCurrentModeLabel);
            this.kpnlConfig.Controls.Add(this.kcmbTimeoutMode);
            this.kpnlConfig.Controls.Add(this.klblTimeoutMode);
            this.kpnlConfig.Controls.Add(this.klblDescription);
            this.kpnlConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlConfig.Location = new System.Drawing.Point(0, 0);
            this.kpnlConfig.Name = "kpnlConfig";
            this.kpnlConfig.Size = new System.Drawing.Size(556, 131);
            this.kpnlConfig.TabIndex = 0;
            // 
            // kbtnApply
            // 
            this.kbtnApply.Location = new System.Drawing.Point(450, 38);
            this.kbtnApply.Name = "kbtnApply";
            this.kbtnApply.Size = new System.Drawing.Size(90, 28);
            this.kbtnApply.TabIndex = 5;
            this.kbtnApply.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnApply.Values.Text = "Apply";
            this.kbtnApply.Click += new System.EventHandler(this.kbtnApply_Click);
            // 
            // klblCurrentMode
            // 
            this.klblCurrentMode.AutoSize = false;
            this.klblCurrentMode.Location = new System.Drawing.Point(120, 75);
            this.klblCurrentMode.Name = "klblCurrentMode";
            this.klblCurrentMode.Size = new System.Drawing.Size(420, 35);
            this.klblCurrentMode.TabIndex = 4;
            this.klblCurrentMode.Values.Text = "Default – tooltip hides after 5 seconds.";
            // 
            // klblCurrentModeLabel
            // 
            this.klblCurrentModeLabel.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblCurrentModeLabel.Location = new System.Drawing.Point(10, 75);
            this.klblCurrentModeLabel.Name = "klblCurrentModeLabel";
            this.klblCurrentModeLabel.Size = new System.Drawing.Size(94, 20);
            this.klblCurrentModeLabel.TabIndex = 3;
            this.klblCurrentModeLabel.Values.Text = "Current mode:";
            // 
            // kcmbTimeoutMode
            // 
            this.kcmbTimeoutMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbTimeoutMode.Items.AddRange(new object[] {
            "Default (5000 ms)",
            "Extended (30000 ms)",
            "Infinite (0 – until pointer leaves)"});
            this.kcmbTimeoutMode.Location = new System.Drawing.Point(120, 40);
            this.kcmbTimeoutMode.Name = "kcmbTimeoutMode";
            this.kcmbTimeoutMode.Size = new System.Drawing.Size(320, 22);
            this.kcmbTimeoutMode.TabIndex = 2;
            this.kcmbTimeoutMode.SelectedIndexChanged += new System.EventHandler(this.kcmbTimeoutMode_SelectedIndexChanged);
            // 
            // klblTimeoutMode
            // 
            this.klblTimeoutMode.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblTimeoutMode.Location = new System.Drawing.Point(10, 43);
            this.klblTimeoutMode.Name = "klblTimeoutMode";
            this.klblTimeoutMode.Size = new System.Drawing.Size(99, 20);
            this.klblTimeoutMode.TabIndex = 1;
            this.klblTimeoutMode.Values.Text = "Timeout mode:";
            // 
            // klblDescription
            // 
            this.klblDescription.Location = new System.Drawing.Point(10, 10);
            this.klblDescription.Name = "klblDescription";
            this.klblDescription.Size = new System.Drawing.Size(573, 20);
            this.klblDescription.TabIndex = 0;
            this.klblDescription.Values.Text = "Krypton tooltips support extended timeouts (>5000ms) and infinite display (0) on " +
    "all Windows versions.";
            // 
            // kpnlBottom
            // 
            this.kpnlBottom.Controls.Add(this.kbtnClose);
            this.kpnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlBottom.Location = new System.Drawing.Point(0, 318);
            this.kpnlBottom.Name = "kpnlBottom";
            this.kpnlBottom.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlBottom.Size = new System.Drawing.Size(584, 51);
            this.kpnlBottom.TabIndex = 2;
            // 
            // kbtnClose
            // 
            this.kbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnClose.Location = new System.Drawing.Point(482, 13);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(90, 25);
            this.kbtnClose.TabIndex = 0;
            this.kbtnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClose.Values.Text = "Close";
            this.kbtnClose.Values.UseAsADialogButton = true;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 369);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(584, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // TooltipTimeoutTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnClose;
            this.ClientSize = new System.Drawing.Size(584, 370);
            this.Controls.Add(this.kpnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TooltipTimeoutTest";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tooltip Extended/Infinite Timeout Demo (Issue #3075)";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.kpnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContent)).EndInit();
            this.kpnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbDemoControls.Panel)).EndInit();
            this.kpgbDemoControls.Panel.ResumeLayout(false);
            this.kpgbDemoControls.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbDemoControls)).EndInit();
            this.kpgbDemoControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlDemoControls)).EndInit();
            this.kpnlDemoControls.ResumeLayout(false);
            this.kpnlDemoControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbConfiguration.Panel)).EndInit();
            this.kpgbConfiguration.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbConfiguration)).EndInit();
            this.kpgbConfiguration.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlConfig)).EndInit();
            this.kpnlConfig.ResumeLayout(false);
            this.kpnlConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTimeoutMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlBottom)).EndInit();
            this.kpnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private Krypton.Toolkit.KryptonPanel kpnlContent;
        private Krypton.Toolkit.KryptonGroupBox kpgbConfiguration;
        private Krypton.Toolkit.KryptonPanel kpnlConfig;
        private Krypton.Toolkit.KryptonLabel klblDescription;
        private Krypton.Toolkit.KryptonLabel klblTimeoutMode;
        private Krypton.Toolkit.KryptonComboBox kcmbTimeoutMode;
        private Krypton.Toolkit.KryptonLabel klblCurrentModeLabel;
        private Krypton.Toolkit.KryptonLabel klblCurrentMode;
        private Krypton.Toolkit.KryptonButton kbtnApply;
        private Krypton.Toolkit.KryptonGroupBox kpgbDemoControls;
        private Krypton.Toolkit.KryptonPanel kpnlDemoControls;
        private Krypton.Toolkit.KryptonButton kbtnBuiltInTooltip;
        private Krypton.Toolkit.KryptonTextBox ktxtDemoInput;
        private Krypton.Toolkit.KryptonCheckBox kchkDemoCheck;
        private Krypton.Toolkit.KryptonLabel klblDemoInstructions;
        private Krypton.Toolkit.KryptonPanel kpnlBottom;
        private Krypton.Toolkit.KryptonButton kbtnClose;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
    }
}