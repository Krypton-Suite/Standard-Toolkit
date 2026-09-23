#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class TextBoxInputModeDemo
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
        this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
        this.klblAny = new Krypton.Toolkit.KryptonLabel();
        this.ktbAny = new Krypton.Toolkit.KryptonTextBox();
        this.klblDigits = new Krypton.Toolkit.KryptonLabel();
        this.ktbDigits = new Krypton.Toolkit.KryptonTextBox();
        this.klblLetters = new Krypton.Toolkit.KryptonLabel();
        this.ktbLetters = new Krypton.Toolkit.KryptonTextBox();
        this.klblAlphanumeric = new Krypton.Toolkit.KryptonLabel();
        this.ktbAlphanumeric = new Krypton.Toolkit.KryptonTextBox();
        this.klblLive = new Krypton.Toolkit.KryptonLabel();
        this.kcmbMode = new Krypton.Toolkit.KryptonComboBox();
        this.ktbLive = new Krypton.Toolkit.KryptonTextBox();
        this.klblNative = new Krypton.Toolkit.KryptonLabel();
        this.tbNative = new System.Windows.Forms.TextBox();
        this.btnPasteSample = new Krypton.Toolkit.KryptonButton();
        this.btnClear = new Krypton.Toolkit.KryptonButton();
        this.klblLog = new Krypton.Toolkit.KryptonLabel();
        this.krtbLog = new Krypton.Toolkit.KryptonRichTextBox();
        this.klblTheme = new Krypton.Toolkit.KryptonLabel();
        this.kcmbTheme = new Krypton.Toolkit.KryptonThemeComboBox();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
        this.kryptonPanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbMode)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
        this.SuspendLayout();
        //
        // kryptonPanel1
        //
        this.kryptonPanel1.Controls.Add(this.klblInstructions);
        this.kryptonPanel1.Controls.Add(this.klblAny);
        this.kryptonPanel1.Controls.Add(this.ktbAny);
        this.kryptonPanel1.Controls.Add(this.klblDigits);
        this.kryptonPanel1.Controls.Add(this.ktbDigits);
        this.kryptonPanel1.Controls.Add(this.klblLetters);
        this.kryptonPanel1.Controls.Add(this.ktbLetters);
        this.kryptonPanel1.Controls.Add(this.klblAlphanumeric);
        this.kryptonPanel1.Controls.Add(this.ktbAlphanumeric);
        this.kryptonPanel1.Controls.Add(this.klblLive);
        this.kryptonPanel1.Controls.Add(this.kcmbMode);
        this.kryptonPanel1.Controls.Add(this.ktbLive);
        this.kryptonPanel1.Controls.Add(this.klblNative);
        this.kryptonPanel1.Controls.Add(this.tbNative);
        this.kryptonPanel1.Controls.Add(this.btnPasteSample);
        this.kryptonPanel1.Controls.Add(this.btnClear);
        this.kryptonPanel1.Controls.Add(this.klblLog);
        this.kryptonPanel1.Controls.Add(this.krtbLog);
        this.kryptonPanel1.Controls.Add(this.klblTheme);
        this.kryptonPanel1.Controls.Add(this.kcmbTheme);
        this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
        this.kryptonPanel1.Name = "kryptonPanel1";
        this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(16);
        this.kryptonPanel1.Size = new System.Drawing.Size(760, 560);
        this.kryptonPanel1.TabIndex = 0;
        //
        // klblInstructions
        //
        this.klblInstructions.Location = new System.Drawing.Point(19, 16);
        this.klblInstructions.Name = "klblInstructions";
        this.klblInstructions.Size = new System.Drawing.Size(720, 52);
        this.klblInstructions.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
        this.klblInstructions.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
        this.klblInstructions.TabIndex = 0;
        this.klblInstructions.Values.Text = "Issue #4417: KryptonTextBox.InputMode filters typing and paste (Digits / Letters / Alphanumeric). Editing keys still work. Programmatic Text is not filtered. Compare with the native TextBox (no filter).";
        //
        // klblAny
        //
        this.klblAny.Location = new System.Drawing.Point(19, 80);
        this.klblAny.Name = "klblAny";
        this.klblAny.Size = new System.Drawing.Size(120, 20);
        this.klblAny.TabIndex = 1;
        this.klblAny.Values.Text = "Any:";
        //
        // ktbAny
        //
        this.ktbAny.CueHint.CueHintText = "Any characters";
        this.ktbAny.InputMode = Krypton.Toolkit.KryptonTextBoxInputMode.Any;
        this.ktbAny.Location = new System.Drawing.Point(145, 78);
        this.ktbAny.Name = "ktbAny";
        this.ktbAny.Size = new System.Drawing.Size(280, 23);
        this.ktbAny.TabIndex = 2;
        //
        // klblDigits
        //
        this.klblDigits.Location = new System.Drawing.Point(19, 114);
        this.klblDigits.Name = "klblDigits";
        this.klblDigits.Size = new System.Drawing.Size(120, 20);
        this.klblDigits.TabIndex = 3;
        this.klblDigits.Values.Text = "Digits:";
        //
        // ktbDigits
        //
        this.ktbDigits.CueHint.CueHintText = "Digits only";
        this.ktbDigits.InputMode = Krypton.Toolkit.KryptonTextBoxInputMode.Digits;
        this.ktbDigits.Location = new System.Drawing.Point(145, 112);
        this.ktbDigits.Name = "ktbDigits";
        this.ktbDigits.Size = new System.Drawing.Size(280, 23);
        this.ktbDigits.TabIndex = 4;
        //
        // klblLetters
        //
        this.klblLetters.Location = new System.Drawing.Point(19, 148);
        this.klblLetters.Name = "klblLetters";
        this.klblLetters.Size = new System.Drawing.Size(120, 20);
        this.klblLetters.TabIndex = 5;
        this.klblLetters.Values.Text = "Letters:";
        //
        // ktbLetters
        //
        this.ktbLetters.CueHint.CueHintText = "Letters only";
        this.ktbLetters.InputMode = Krypton.Toolkit.KryptonTextBoxInputMode.Letters;
        this.ktbLetters.Location = new System.Drawing.Point(145, 146);
        this.ktbLetters.Name = "ktbLetters";
        this.ktbLetters.Size = new System.Drawing.Size(280, 23);
        this.ktbLetters.TabIndex = 6;
        //
        // klblAlphanumeric
        //
        this.klblAlphanumeric.Location = new System.Drawing.Point(19, 182);
        this.klblAlphanumeric.Name = "klblAlphanumeric";
        this.klblAlphanumeric.Size = new System.Drawing.Size(120, 20);
        this.klblAlphanumeric.TabIndex = 7;
        this.klblAlphanumeric.Values.Text = "Alphanumeric:";
        //
        // ktbAlphanumeric
        //
        this.ktbAlphanumeric.CueHint.CueHintText = "Letters and digits";
        this.ktbAlphanumeric.InputMode = Krypton.Toolkit.KryptonTextBoxInputMode.Alphanumeric;
        this.ktbAlphanumeric.Location = new System.Drawing.Point(145, 180);
        this.ktbAlphanumeric.Name = "ktbAlphanumeric";
        this.ktbAlphanumeric.Size = new System.Drawing.Size(280, 23);
        this.ktbAlphanumeric.TabIndex = 8;
        //
        // klblLive
        //
        this.klblLive.Location = new System.Drawing.Point(19, 226);
        this.klblLive.Name = "klblLive";
        this.klblLive.Size = new System.Drawing.Size(120, 20);
        this.klblLive.TabIndex = 9;
        this.klblLive.Values.Text = "Live mode:";
        //
        // kcmbMode
        //
        this.kcmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbMode.DropDownWidth = 160;
        this.kcmbMode.Location = new System.Drawing.Point(145, 224);
        this.kcmbMode.Name = "kcmbMode";
        this.kcmbMode.Size = new System.Drawing.Size(160, 22);
        this.kcmbMode.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
        this.kcmbMode.TabIndex = 10;
        this.kcmbMode.SelectedIndexChanged += new System.EventHandler(this.kcmbMode_SelectedIndexChanged);
        //
        // ktbLive
        //
        this.ktbLive.CueHint.CueHintText = "Mode from combo";
        this.ktbLive.Location = new System.Drawing.Point(315, 224);
        this.ktbLive.Name = "ktbLive";
        this.ktbLive.Size = new System.Drawing.Size(280, 23);
        this.ktbLive.TabIndex = 11;
        //
        // klblNative
        //
        this.klblNative.Location = new System.Drawing.Point(19, 268);
        this.klblNative.Name = "klblNative";
        this.klblNative.Size = new System.Drawing.Size(120, 20);
        this.klblNative.TabIndex = 12;
        this.klblNative.Values.Text = "Native TextBox:";
        //
        // tbNative
        //
        this.tbNative.Location = new System.Drawing.Point(145, 266);
        this.tbNative.Name = "tbNative";
        this.tbNative.Size = new System.Drawing.Size(280, 23);
        this.tbNative.TabIndex = 13;
        //
        // btnPasteSample
        //
        this.btnPasteSample.Location = new System.Drawing.Point(450, 264);
        this.btnPasteSample.Name = "btnPasteSample";
        this.btnPasteSample.Size = new System.Drawing.Size(145, 28);
        this.btnPasteSample.TabIndex = 14;
        this.btnPasteSample.Values.Text = "Paste sample -> live";
        this.btnPasteSample.Click += new System.EventHandler(this.btnPasteSample_Click);
        //
        // btnClear
        //
        this.btnClear.Location = new System.Drawing.Point(605, 264);
        this.btnClear.Name = "btnClear";
        this.btnClear.Size = new System.Drawing.Size(90, 28);
        this.btnClear.TabIndex = 15;
        this.btnClear.Values.Text = "Clear";
        this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
        //
        // klblLog
        //
        this.klblLog.Location = new System.Drawing.Point(19, 310);
        this.klblLog.Name = "klblLog";
        this.klblLog.Size = new System.Drawing.Size(40, 20);
        this.klblLog.TabIndex = 16;
        this.klblLog.Values.Text = "Log:";
        //
        // krtbLog
        //
        this.krtbLog.Location = new System.Drawing.Point(19, 334);
        this.krtbLog.Name = "krtbLog";
        this.krtbLog.ReadOnly = true;
        this.krtbLog.Size = new System.Drawing.Size(676, 140);
        this.krtbLog.TabIndex = 17;
        //
        // klblTheme
        //
        this.klblTheme.Location = new System.Drawing.Point(19, 492);
        this.klblTheme.Name = "klblTheme";
        this.klblTheme.Size = new System.Drawing.Size(50, 20);
        this.klblTheme.TabIndex = 18;
        this.klblTheme.Values.Text = "Theme:";
        //
        // kcmbTheme
        //
        this.kcmbTheme.DropDownWidth = 360;
        this.kcmbTheme.Location = new System.Drawing.Point(75, 490);
        this.kcmbTheme.Name = "kcmbTheme";
        this.kcmbTheme.Size = new System.Drawing.Size(360, 22);
        this.kcmbTheme.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
        this.kcmbTheme.TabIndex = 19;
        //
        // TextBoxInputModeDemo
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(760, 560);
        this.Controls.Add(this.kryptonPanel1);
        this.Name = "TextBoxInputModeDemo";
        this.Text = "TextBox InputMode (#4417)";
        this.Load += new System.EventHandler(this.TextBoxInputModeDemo_Load);
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
        this.kryptonPanel1.ResumeLayout(false);
        this.kryptonPanel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbMode)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
        this.ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonPanel kryptonPanel1;
    private Krypton.Toolkit.KryptonLabel klblInstructions;
    private Krypton.Toolkit.KryptonLabel klblAny;
    private Krypton.Toolkit.KryptonTextBox ktbAny;
    private Krypton.Toolkit.KryptonLabel klblDigits;
    private Krypton.Toolkit.KryptonTextBox ktbDigits;
    private Krypton.Toolkit.KryptonLabel klblLetters;
    private Krypton.Toolkit.KryptonTextBox ktbLetters;
    private Krypton.Toolkit.KryptonLabel klblAlphanumeric;
    private Krypton.Toolkit.KryptonTextBox ktbAlphanumeric;
    private Krypton.Toolkit.KryptonLabel klblLive;
    private Krypton.Toolkit.KryptonComboBox kcmbMode;
    private Krypton.Toolkit.KryptonTextBox ktbLive;
    private Krypton.Toolkit.KryptonLabel klblNative;
    private System.Windows.Forms.TextBox tbNative;
    private Krypton.Toolkit.KryptonButton btnPasteSample;
    private Krypton.Toolkit.KryptonButton btnClear;
    private Krypton.Toolkit.KryptonLabel klblLog;
    private Krypton.Toolkit.KryptonRichTextBox krtbLog;
    private Krypton.Toolkit.KryptonLabel klblTheme;
    private Krypton.Toolkit.KryptonThemeComboBox kcmbTheme;
}
