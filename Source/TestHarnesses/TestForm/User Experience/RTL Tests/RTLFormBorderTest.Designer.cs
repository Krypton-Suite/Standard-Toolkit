namespace TestForm
{
    partial class RTLFormBorderTest
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
            this.buttonSpecAny1 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny2 = new Krypton.Toolkit.ButtonSpecAny();
            this.kwlblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
            this.klblMode = new Krypton.Toolkit.KryptonLabel();
            this.kcmbRtlMode = new Krypton.Toolkit.KryptonComboBox();
            this.kchkbtnSwitchLayout = new Krypton.Toolkit.KryptonCheckButton();
            this.kbtnOpenNativeForm = new Krypton.Toolkit.KryptonButton();
            this.klblCaptionIconPadding = new Krypton.Toolkit.KryptonLabel();
            this.knudCaptionIconPadding = new Krypton.Toolkit.KryptonNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbRtlMode)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Next;
            this.buttonSpecAny1.UniqueName = "0f4b63b15d3f450d8a516cd20cf8228d";
            // 
            // buttonSpecAny2
            // 
            this.buttonSpecAny2.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Previous;
            this.buttonSpecAny2.UniqueName = "13edbb23e5f1489cb15b33880c16b496";
            // 
            // kwlblInstructions
            // 
            this.kwlblInstructions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlblInstructions.Location = new System.Drawing.Point(12, 12);
            this.kwlblInstructions.Name = "kwlblInstructions";
            this.kwlblInstructions.Size = new System.Drawing.Size(680, 120);
            this.kwlblInstructions.Text = "Issue #2103 — native WinForms: RightToLeft alone does not flip the title bar; RightToLeft + RightToLeftLayout moves min/max/close to the left and the icon to the right. Caption text must stay readable (not glyph-mirrored). Drag the left and right borders: the grabbed edge must move. Open a native Form to compare. CaptionIconPadding adds extra space around the caption icon (designer: Visuals).";
            // 
            // klblMode
            // 
            this.klblMode.Location = new System.Drawing.Point(12, 140);
            this.klblMode.Name = "klblMode";
            this.klblMode.Size = new System.Drawing.Size(90, 20);
            this.klblMode.TabIndex = 0;
            this.klblMode.Values.Text = "RTL mode";
            // 
            // kcmbRtlMode
            // 
            this.kcmbRtlMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbRtlMode.DropDownWidth = 280;
            this.kcmbRtlMode.IntegralHeight = false;
            this.kcmbRtlMode.Location = new System.Drawing.Point(12, 166);
            this.kcmbRtlMode.Name = "kcmbRtlMode";
            this.kcmbRtlMode.Size = new System.Drawing.Size(280, 25);
            this.kcmbRtlMode.TabIndex = 1;
            this.kcmbRtlMode.Items.AddRange(new object[] {
            "Left to right",
            "RightToLeft only (caption stays LTR)",
            "RightToLeft + RightToLeftLayout"});
            this.kcmbRtlMode.SelectedIndexChanged += new System.EventHandler(this.kcmbRtlMode_SelectedIndexChanged);
            // 
            // kchkbtnSwitchLayout
            // 
            this.kchkbtnSwitchLayout.AccessibleDescription = "Toggles the form between right-to-left layout and left-to-right.";
            this.kchkbtnSwitchLayout.AccessibleName = "Switch layout";
            this.kchkbtnSwitchLayout.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
            this.kchkbtnSwitchLayout.Location = new System.Drawing.Point(308, 166);
            this.kchkbtnSwitchLayout.Name = "kchkbtnSwitchLayout";
            this.kchkbtnSwitchLayout.Size = new System.Drawing.Size(120, 25);
            this.kchkbtnSwitchLayout.TabIndex = 2;
            this.kchkbtnSwitchLayout.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kchkbtnSwitchLayout.Values.Text = "Switch Layout";
            this.kchkbtnSwitchLayout.Click += new System.EventHandler(this.kchkbtnSwitchLayout_Click);
            // 
            // kbtnOpenNativeForm
            // 
            this.kbtnOpenNativeForm.Location = new System.Drawing.Point(444, 166);
            this.kbtnOpenNativeForm.Name = "kbtnOpenNativeForm";
            this.kbtnOpenNativeForm.Size = new System.Drawing.Size(140, 25);
            this.kbtnOpenNativeForm.TabIndex = 3;
            this.kbtnOpenNativeForm.Values.Text = "Open native Form";
            this.kbtnOpenNativeForm.Click += new System.EventHandler(this.kbtnOpenNativeForm_Click);
            // 
            // klblCaptionIconPadding
            // 
            this.klblCaptionIconPadding.Location = new System.Drawing.Point(12, 204);
            this.klblCaptionIconPadding.Name = "klblCaptionIconPadding";
            this.klblCaptionIconPadding.Size = new System.Drawing.Size(280, 20);
            this.klblCaptionIconPadding.TabIndex = 4;
            this.klblCaptionIconPadding.Values.Text = "CaptionIconPadding (all sides)";
            // 
            // knudCaptionIconPadding
            // 
            this.knudCaptionIconPadding.Location = new System.Drawing.Point(12, 230);
            this.knudCaptionIconPadding.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.knudCaptionIconPadding.Name = "knudCaptionIconPadding";
            this.knudCaptionIconPadding.Size = new System.Drawing.Size(80, 22);
            this.knudCaptionIconPadding.TabIndex = 5;
            this.knudCaptionIconPadding.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.knudCaptionIconPadding.ValueChanged += new System.EventHandler(this.knudCaptionIconPadding_ValueChanged);
            // 
            // RTLFormBorderTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ButtonSpecs.Add(this.buttonSpecAny1);
            this.ButtonSpecs.Add(this.buttonSpecAny2);
            this.ClientSize = new System.Drawing.Size(705, 414);
            this.Controls.Add(this.knudCaptionIconPadding);
            this.Controls.Add(this.klblCaptionIconPadding);
            this.Controls.Add(this.kbtnOpenNativeForm);
            this.Controls.Add(this.kchkbtnSwitchLayout);
            this.Controls.Add(this.kcmbRtlMode);
            this.Controls.Add(this.klblMode);
            this.Controls.Add(this.kwlblInstructions);
            this.Name = "RTLFormBorderTest";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "Caption Test ABC";
            this.TextExtra = "Test Text";
            ((System.ComponentModel.ISupportInitialize)(this.kcmbRtlMode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ButtonSpecAny buttonSpecAny1;
        private ButtonSpecAny buttonSpecAny2;
        private KryptonWrapLabel kwlblInstructions;
        private KryptonLabel klblMode;
        private KryptonComboBox kcmbRtlMode;
        private KryptonCheckButton kchkbtnSwitchLayout;
        private KryptonButton kbtnOpenNativeForm;
        private KryptonLabel klblCaptionIconPadding;
        private KryptonNumericUpDown knudCaptionIconPadding;
    }
}