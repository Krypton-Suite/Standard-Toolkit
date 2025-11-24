#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    partial class InputBoxTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBoxTest));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.ktxtDefaultResponse = new Krypton.Toolkit.KryptonTextBox();
            this.krtxtPrompt = new Krypton.Toolkit.KryptonRichTextBox();
            this.ktxtCaption = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.ktxtCueText = new Krypton.Toolkit.KryptonTextBox();
            this.kcbtnCueTextColor = new Krypton.Toolkit.KryptonColorButton();
            this.kcbUsePasswordOption = new Krypton.Toolkit.KryptonCheckBox();
            this.kcbUseRTLOption = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kbtnTest = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnTest);
            this.kryptonPanel1.Controls.Add(this.kbtnCancel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 256);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(579, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 255);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(579, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kcbUseRTLOption);
            this.kryptonPanel2.Controls.Add(this.kcbUsePasswordOption);
            this.kryptonPanel2.Controls.Add(this.kcbtnCueTextColor);
            this.kryptonPanel2.Controls.Add(this.ktxtCueText);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel2.Controls.Add(this.ktxtCaption);
            this.kryptonPanel2.Controls.Add(this.krtxtPrompt);
            this.kryptonPanel2.Controls.Add(this.ktxtDefaultResponse);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(579, 255);
            this.kryptonPanel2.TabIndex = 2;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(59, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Caption:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel2.Location = new System.Drawing.Point(13, 127);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(123, 20);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "Default Respoonse:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel3.Location = new System.Drawing.Point(13, 41);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(58, 20);
            this.kryptonLabel3.TabIndex = 2;
            this.kryptonLabel3.Values.Text = "Prompt:";
            // 
            // ktxtDefaultResponse
            // 
            this.ktxtDefaultResponse.Location = new System.Drawing.Point(143, 127);
            this.ktxtDefaultResponse.Name = "ktxtDefaultResponse";
            this.ktxtDefaultResponse.Size = new System.Drawing.Size(424, 23);
            this.ktxtDefaultResponse.TabIndex = 3;
            this.ktxtDefaultResponse.Text = "Default Response";
            // 
            // krtxtPrompt
            // 
            this.krtxtPrompt.Location = new System.Drawing.Point(143, 41);
            this.krtxtPrompt.Name = "krtxtPrompt";
            this.krtxtPrompt.Size = new System.Drawing.Size(423, 80);
            this.krtxtPrompt.TabIndex = 4;
            this.krtxtPrompt.Text = "Your prompt here...";
            // 
            // ktxtCaption
            // 
            this.ktxtCaption.Location = new System.Drawing.Point(143, 13);
            this.ktxtCaption.Name = "ktxtCaption";
            this.ktxtCaption.Size = new System.Drawing.Size(422, 23);
            this.ktxtCaption.TabIndex = 5;
            this.ktxtCaption.Text = "Caption";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel4.Location = new System.Drawing.Point(71, 155);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(65, 20);
            this.kryptonLabel4.TabIndex = 6;
            this.kryptonLabel4.Values.Text = "Cue Text:";
            // 
            // ktxtCueText
            // 
            this.ktxtCueText.Location = new System.Drawing.Point(143, 157);
            this.ktxtCueText.Name = "ktxtCueText";
            this.ktxtCueText.Size = new System.Drawing.Size(424, 23);
            this.ktxtCueText.TabIndex = 7;
            // 
            // kcbtnCueTextColor
            // 
            this.kcbtnCueTextColor.Location = new System.Drawing.Point(143, 187);
            this.kcbtnCueTextColor.Name = "kcbtnCueTextColor";
            this.kcbtnCueTextColor.SelectedColor = System.Drawing.Color.Gray;
            this.kcbtnCueTextColor.Size = new System.Drawing.Size(153, 25);
            this.kcbtnCueTextColor.TabIndex = 8;
            this.kcbtnCueTextColor.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonColorButton1.Values.Image")));
            this.kcbtnCueTextColor.Values.Text = "Cue Text Color";
            // 
            // kcbUsePasswordOption
            // 
            this.kcbUsePasswordOption.Location = new System.Drawing.Point(143, 219);
            this.kcbUsePasswordOption.Name = "kcbUsePasswordOption";
            this.kcbUsePasswordOption.Size = new System.Drawing.Size(140, 20);
            this.kcbUsePasswordOption.TabIndex = 9;
            this.kcbUsePasswordOption.Values.Text = "Use Password Option";
            // 
            // kcbUseRTLOption
            // 
            this.kcbUseRTLOption.Location = new System.Drawing.Point(289, 219);
            this.kcbUseRTLOption.Name = "kcbUseRTLOption";
            this.kcbUseRTLOption.Size = new System.Drawing.Size(108, 20);
            this.kcbUseRTLOption.TabIndex = 10;
            this.kcbUseRTLOption.Values.Text = "Use RTL Option";
            this.kcbUseRTLOption.CheckedChanged += new System.EventHandler(this.kcbUseRTLOption_CheckedChanged);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(477, 13);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(90, 25);
            this.kbtnCancel.TabIndex = 0;
            this.kbtnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCancel.Values.Text = "Cance&l";
            this.kbtnCancel.Values.UseAsADialogButton = true;
            // 
            // kbtnTest
            // 
            this.kbtnTest.Location = new System.Drawing.Point(381, 13);
            this.kbtnTest.Name = "kbtnTest";
            this.kbtnTest.Size = new System.Drawing.Size(90, 25);
            this.kbtnTest.TabIndex = 1;
            this.kbtnTest.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTest.Values.Text = "&Test";
            this.kbtnTest.Click += new System.EventHandler(this.kbtnTest_Click);
            // 
            // InputBoxTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 306);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonBorderEdge1);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "InputBoxTest";
            this.Text = "InputBoxTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kryptonPanel2;
        private KryptonLabel kryptonLabel3;
        private KryptonLabel kryptonLabel2;
        private KryptonLabel kryptonLabel1;
        private KryptonColorButton kcbtnCueTextColor;
        private KryptonTextBox ktxtCueText;
        private KryptonLabel kryptonLabel4;
        private KryptonTextBox ktxtCaption;
        private KryptonRichTextBox krtxtPrompt;
        private KryptonTextBox ktxtDefaultResponse;
        private KryptonCheckBox kcbUseRTLOption;
        private KryptonCheckBox kcbUsePasswordOption;
        private KryptonButton kbtnTest;
        private KryptonButton kbtnCancel;
    }
}