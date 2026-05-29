namespace TestForm
{
    partial class RichTextBoxFormattingTest
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.krtbRichTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
            this.klblTitle = new Krypton.Toolkit.KryptonLabel();
            this.klblPalette = new Krypton.Toolkit.KryptonLabel();
            this.kcmbInputControlStyle = new Krypton.Toolkit.KryptonComboBox();
            this.klblInputControlStyle = new Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel3 = new Krypton.Toolkit.KryptonPanel();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kbtnClear = new Krypton.Toolkit.KryptonButton();
            this.kbtnVerifyFormatting = new Krypton.Toolkit.KryptonButton();
            this.kbtnLoadPlainText = new Krypton.Toolkit.KryptonButton();
            this.kbtnLoadSample = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbInputControlStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.krtbRichTextBox);
            this.kryptonPanel1.Controls.Add(this.kryptonPanel2);
            this.kryptonPanel1.Controls.Add(this.kryptonPanel3);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1000, 700);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // krtbRichTextBox
            // 
            this.krtbRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbRichTextBox.Location = new System.Drawing.Point(0, 130);
            this.krtbRichTextBox.Name = "krtbRichTextBox";
            this.krtbRichTextBox.Size = new System.Drawing.Size(1000, 500);
            this.krtbRichTextBox.TabIndex = 2;
            this.krtbRichTextBox.Text = "";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel2.Controls.Add(this.klblInstructions);
            this.kryptonPanel2.Controls.Add(this.klblTitle);
            this.kryptonPanel2.Controls.Add(this.klblPalette);
            this.kryptonPanel2.Controls.Add(this.kcmbInputControlStyle);
            this.kryptonPanel2.Controls.Add(this.klblInputControlStyle);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(1000, 130);
            this.kryptonPanel2.TabIndex = 0;
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DropDownWidth = 305;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(70, 45);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(305, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 6;
            // 
            // klblInstructions
            // 
            this.klblInstructions.Location = new System.Drawing.Point(400, 47);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(458, 20);
            this.klblInstructions.StateCommon.ShortText.Color1 = System.Drawing.Color.Gray;
            this.klblInstructions.TabIndex = 3;
            this.klblInstructions.Values.Text = "Instructions: Change the palette above and verify that RTF formatting is preserve" +
    "d.";
            // 
            // klblTitle
            // 
            this.klblTitle.Location = new System.Drawing.Point(12, 12);
            this.klblTitle.Name = "klblTitle";
            this.klblTitle.Size = new System.Drawing.Size(509, 26);
            this.klblTitle.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.klblTitle.TabIndex = 0;
            this.klblTitle.Values.Text = "KryptonRichTextBox Formatting Preservation Test (Issue #2832)";
            // 
            // klblPalette
            // 
            this.klblPalette.Location = new System.Drawing.Point(12, 47);
            this.klblPalette.Name = "klblPalette";
            this.klblPalette.Size = new System.Drawing.Size(51, 20);
            this.klblPalette.TabIndex = 2;
            this.klblPalette.Values.Text = "Palette:";
            // 
            // kcmbInputControlStyle
            // 
            this.kcmbInputControlStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbInputControlStyle.Location = new System.Drawing.Point(130, 75);
            this.kcmbInputControlStyle.Name = "kcmbInputControlStyle";
            this.kcmbInputControlStyle.Size = new System.Drawing.Size(200, 22);
            this.kcmbInputControlStyle.TabIndex = 4;
            this.kcmbInputControlStyle.SelectedIndexChanged += new System.EventHandler(this.KcmbInputControlStyle_SelectedIndexChanged);
            // 
            // klblInputControlStyle
            // 
            this.klblInputControlStyle.Location = new System.Drawing.Point(12, 77);
            this.klblInputControlStyle.Name = "klblInputControlStyle";
            this.klblInputControlStyle.Size = new System.Drawing.Size(109, 20);
            this.klblInputControlStyle.TabIndex = 5;
            this.klblInputControlStyle.Values.Text = "InputControlStyle:";
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.klblStatus);
            this.kryptonPanel3.Controls.Add(this.kbtnClear);
            this.kryptonPanel3.Controls.Add(this.kbtnVerifyFormatting);
            this.kryptonPanel3.Controls.Add(this.kbtnLoadPlainText);
            this.kryptonPanel3.Controls.Add(this.kbtnLoadSample);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 630);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(1000, 70);
            this.kryptonPanel3.TabIndex = 1;
            // 
            // klblStatus
            // 
            this.klblStatus.Location = new System.Drawing.Point(12, 41);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(83, 20);
            this.klblStatus.TabIndex = 4;
            this.klblStatus.Values.Text = "Status: Ready";
            // 
            // kbtnClear
            // 
            this.kbtnClear.Location = new System.Drawing.Point(480, 10);
            this.kbtnClear.Name = "kbtnClear";
            this.kbtnClear.Size = new System.Drawing.Size(100, 25);
            this.kbtnClear.TabIndex = 3;
            this.kbtnClear.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClear.Values.Text = "Clear";
            this.kbtnClear.Click += new System.EventHandler(this.KbtnClear_Click);
            // 
            // kbtnVerifyFormatting
            // 
            this.kbtnVerifyFormatting.Location = new System.Drawing.Point(324, 10);
            this.kbtnVerifyFormatting.Name = "kbtnVerifyFormatting";
            this.kbtnVerifyFormatting.Size = new System.Drawing.Size(150, 25);
            this.kbtnVerifyFormatting.TabIndex = 2;
            this.kbtnVerifyFormatting.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnVerifyFormatting.Values.Text = "Verify Formatting";
            this.kbtnVerifyFormatting.Click += new System.EventHandler(this.KbtnVerifyFormatting_Click);
            // 
            // kbtnLoadPlainText
            // 
            this.kbtnLoadPlainText.Location = new System.Drawing.Point(168, 10);
            this.kbtnLoadPlainText.Name = "kbtnLoadPlainText";
            this.kbtnLoadPlainText.Size = new System.Drawing.Size(150, 25);
            this.kbtnLoadPlainText.TabIndex = 1;
            this.kbtnLoadPlainText.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnLoadPlainText.Values.Text = "Load Plain Text";
            this.kbtnLoadPlainText.Click += new System.EventHandler(this.KbtnLoadPlainText_Click);
            // 
            // kbtnLoadSample
            // 
            this.kbtnLoadSample.Location = new System.Drawing.Point(12, 10);
            this.kbtnLoadSample.Name = "kbtnLoadSample";
            this.kbtnLoadSample.Size = new System.Drawing.Size(150, 25);
            this.kbtnLoadSample.TabIndex = 0;
            this.kbtnLoadSample.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnLoadSample.Values.Text = "Load Sample RTF";
            this.kbtnLoadSample.Click += new System.EventHandler(this.KbtnLoadSample_Click);
            // 
            // RichTextBoxFormattingTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.kryptonPanel1);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "RichTextBoxFormattingTest";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RichTextBox Formatting Preservation Test - Issue #2832";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbInputControlStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.kryptonPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonLabel klblTitle;
        private Krypton.Toolkit.KryptonLabel klblPalette;
        private Krypton.Toolkit.KryptonComboBox kcmbInputControlStyle;
        private Krypton.Toolkit.KryptonLabel klblInputControlStyle;
        private Krypton.Toolkit.KryptonRichTextBox krtbRichTextBox;
        private Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private Krypton.Toolkit.KryptonButton kbtnLoadSample;
        private Krypton.Toolkit.KryptonButton kbtnLoadPlainText;
        private Krypton.Toolkit.KryptonButton kbtnVerifyFormatting;
        private Krypton.Toolkit.KryptonButton kbtnClear;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private Krypton.Toolkit.KryptonLabel klblInstructions;
        private KryptonThemeComboBox kryptonThemeComboBox1;
    }
}