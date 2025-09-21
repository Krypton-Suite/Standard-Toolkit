using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace BackstageDemo.UserControls
{
    partial class SettingsPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.titleLabel = new Krypton.Toolkit.KryptonLabel();
            this.autoSaveLabel = new Krypton.Toolkit.KryptonLabel();
            this.autoSaveCheck = new Krypton.Toolkit.KryptonCheckBox();
            this.themeLabel = new Krypton.Toolkit.KryptonLabel();
            this.themeCombo = new Krypton.Toolkit.KryptonComboBox();
            this.languageLabel = new Krypton.Toolkit.KryptonLabel();
            this.languageCombo = new Krypton.Toolkit.KryptonComboBox();
            this.saveButton = new Krypton.Toolkit.KryptonButton();
            this.resetButton = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.themeCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.languageCombo)).BeginInit();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Location = new System.Drawing.Point(30, 30);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(400, 40);
            this.titleLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Application Settings";
            // 
            // autoSaveLabel
            // 
            this.autoSaveLabel.Location = new System.Drawing.Point(30, 90);
            this.autoSaveLabel.Name = "autoSaveLabel";
            this.autoSaveLabel.Size = new System.Drawing.Size(100, 25);
            this.autoSaveLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.autoSaveLabel.TabIndex = 1;
            this.autoSaveLabel.Text = "Auto Save:";
            // 
            // autoSaveCheck
            // 
            this.autoSaveCheck.Checked = true;
            this.autoSaveCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoSaveCheck.Location = new System.Drawing.Point(30, 120);
            this.autoSaveCheck.Name = "autoSaveCheck";
            this.autoSaveCheck.Size = new System.Drawing.Size(250, 25);
            this.autoSaveCheck.TabIndex = 2;
            this.autoSaveCheck.Text = "Enable auto save every 5 minutes";
            this.autoSaveCheck.CheckedChanged += new System.EventHandler(this.AutoSaveCheck_CheckedChanged);
            // 
            // themeLabel
            // 
            this.themeLabel.Location = new System.Drawing.Point(30, 170);
            this.themeLabel.Name = "themeLabel";
            this.themeLabel.Size = new System.Drawing.Size(150, 25);
            this.themeLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.themeLabel.TabIndex = 3;
            this.themeLabel.Text = "Application Theme:";
            // 
            // themeCombo
            // 
            this.themeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.themeCombo.DropDownWidth = 200;
            this.themeCombo.Items.AddRange(new object[] {
            "Microsoft365Blue",
            "Office2010Blue",
            "Office2010Silver",
            "Office2010Black",
            "SparkleBlue"});
            this.themeCombo.Location = new System.Drawing.Point(30, 200);
            this.themeCombo.Name = "themeCombo";
            this.themeCombo.Size = new System.Drawing.Size(200, 25);
            this.themeCombo.TabIndex = 4;
            this.themeCombo.SelectedIndexChanged += new System.EventHandler(this.ThemeCombo_SelectedIndexChanged);
            // 
            // languageLabel
            // 
            this.languageLabel.Location = new System.Drawing.Point(300, 170);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(100, 25);
            this.languageLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.languageLabel.TabIndex = 5;
            this.languageLabel.Text = "Language:";
            // 
            // languageCombo
            // 
            this.languageCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageCombo.DropDownWidth = 150;
            this.languageCombo.Items.AddRange(new object[] {
            "English",
            "Spanish",
            "French",
            "German",
            "Chinese"});
            this.languageCombo.Location = new System.Drawing.Point(300, 200);
            this.languageCombo.Name = "languageCombo";
            this.languageCombo.Size = new System.Drawing.Size(150, 25);
            this.languageCombo.TabIndex = 6;
            this.languageCombo.SelectedIndexChanged += new System.EventHandler(this.LanguageCombo_SelectedIndexChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(30, 280);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(120, 35);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save Settings";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(170, 280);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(130, 35);
            this.resetButton.TabIndex = 8;
            this.resetButton.Text = "Reset to Defaults";
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // SettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.languageCombo);
            this.Controls.Add(this.languageLabel);
            this.Controls.Add(this.themeCombo);
            this.Controls.Add(this.themeLabel);
            this.Controls.Add(this.autoSaveCheck);
            this.Controls.Add(this.autoSaveLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "SettingsPage";
            this.Size = new System.Drawing.Size(600, 400);
            ((System.ComponentModel.ISupportInitialize)(this.themeCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.languageCombo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonLabel titleLabel;
        private KryptonLabel autoSaveLabel;
        private KryptonCheckBox autoSaveCheck;
        private KryptonLabel themeLabel;
        private KryptonComboBox themeCombo;
        private KryptonLabel languageLabel;
        private KryptonComboBox languageCombo;
        private KryptonButton saveButton;
        private KryptonButton resetButton;
    }
}
