﻿#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    partial class RibbonTest
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RibbonTest));
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kcmdClose = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuItems2 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem2 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonCustomPaletteBase1 = new Krypton.Toolkit.KryptonCustomPaletteBase(this.components);
            this.kryptonRibbon1 = new Krypton.Ribbon.KryptonRibbon();
            this.kryptonRibbonQATButton1 = new Krypton.Ribbon.KryptonRibbonQATButton();
            this.kryptonRibbonQATButton2 = new Krypton.Ribbon.KryptonRibbonQATButton();
            this.kryptonRibbonQATButton3 = new Krypton.Ribbon.KryptonRibbonQATButton();
            this.kryptonRibbonQATButton4 = new Krypton.Ribbon.KryptonRibbonQATButton();
            this.kryptonRibbonQATButton5 = new Krypton.Ribbon.KryptonRibbonQATButton();
            this.kryptonRibbonContext1 = new Krypton.Ribbon.KryptonRibbonContext();
            this.kryptonRibbonContext2 = new Krypton.Ribbon.KryptonRibbonContext();
            this.kryptonRibbonContext3 = new Krypton.Ribbon.KryptonRibbonContext();
            this.kryptonRibbonTab1 = new Krypton.Ribbon.KryptonRibbonTab();
            this.kryptonRibbonGroup1 = new Krypton.Ribbon.KryptonRibbonGroup();
            this.kryptonRibbonGroupTriple1 = new Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.kryptonRibbonGroupButton1 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupButton2 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupButton3 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupLines1 = new Krypton.Ribbon.KryptonRibbonGroupLines();
            this.kryptonRibbonGroupButton4 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupButton5 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupGallery1 = new Krypton.Ribbon.KryptonRibbonGroupGallery();
            this.kryptonRibbonGroupTriple2 = new Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.kryptonRibbonGroupButton6 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupButton7 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupThemeComboBox1 = new Krypton.Ribbon.KryptonRibbonGroupThemeComboBox();
            this.kryptonRibbonTab2 = new Krypton.Ribbon.KryptonRibbonTab();
            this.kryptonRibbonTab3 = new Krypton.Ribbon.KryptonRibbonTab();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 115);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(838, 373);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Office2007BlackDarkMode;
            this.kryptonThemeComboBox1.DropDownWidth = 394;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(34, 82);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(394, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 2;
            // 
            // kcmdClose
            // 
            this.kcmdClose.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kcmdClose.ImageLarge")));
            this.kcmdClose.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kcmdClose.ImageSmall")));
            this.kcmdClose.Text = "Close";
            // 
            // kryptonContextMenuItem2
            // 
            this.kryptonContextMenuItem2.Text = "Menu Item";
            // 
            // kryptonCustomPaletteBase1
            // 
            this.kryptonCustomPaletteBase1.BaseFont = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonCustomPaletteBase1.BasePaletteType = Krypton.Toolkit.BasePaletteType.Custom;
            this.kryptonCustomPaletteBase1.Ribbon.RibbonTab.StateNormal.BackColor1 = System.Drawing.Color.Empty;
            this.kryptonCustomPaletteBase1.Ribbon.RibbonTab.StateNormal.BackColor2 = System.Drawing.Color.Empty;
            this.kryptonCustomPaletteBase1.Ribbon.RibbonTab.StateNormal.BackColor3 = System.Drawing.Color.Empty;
            this.kryptonCustomPaletteBase1.Ribbon.RibbonTab.StateNormal.BackColor4 = System.Drawing.Color.Empty;
            this.kryptonCustomPaletteBase1.Ribbon.RibbonTab.StateNormal.BackColor5 = System.Drawing.Color.Empty;
            this.kryptonCustomPaletteBase1.Ribbon.RibbonTab.StateNormal.TextColor = System.Drawing.Color.White;
            this.kryptonCustomPaletteBase1.ThemeName = null;
            this.kryptonCustomPaletteBase1.UseThemeFormChromeBorderWidth = Krypton.Toolkit.InheritBool.True;
            // 
            // kryptonRibbon1
            // 
            this.kryptonRibbon1.InDesignHelperMode = true;
            this.kryptonRibbon1.Name = "kryptonRibbon1";
            this.kryptonRibbon1.QATButtons.AddRange(new System.ComponentModel.Component[] {
            this.kryptonRibbonQATButton1,
            this.kryptonRibbonQATButton2,
            this.kryptonRibbonQATButton3,
            this.kryptonRibbonQATButton4,
            this.kryptonRibbonQATButton5});
            this.kryptonRibbon1.RibbonContexts.AddRange(new Krypton.Ribbon.KryptonRibbonContext[] {
            this.kryptonRibbonContext1,
            this.kryptonRibbonContext2,
            this.kryptonRibbonContext3});
            this.kryptonRibbon1.RibbonFileAppButton.AppButtonMenuItems.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems1});
            this.kryptonRibbon1.RibbonFileAppButton.AppButtonToolTipStyle = Krypton.Toolkit.LabelStyle.SuperTip;
            this.kryptonRibbon1.RibbonTabs.AddRange(new Krypton.Ribbon.KryptonRibbonTab[] {
            this.kryptonRibbonTab1,
            this.kryptonRibbonTab2,
            this.kryptonRibbonTab3});
            this.kryptonRibbon1.SelectedTab = this.kryptonRibbonTab1;
            this.kryptonRibbon1.Size = new System.Drawing.Size(838, 115);
            this.kryptonRibbon1.StateCommon.RibbonGeneral.TabRowBackgroundGradientFirstColor = System.Drawing.Color.Empty;
            this.kryptonRibbon1.TabIndex = 0;
            // 
            // kryptonRibbonQATButton1
            // 
            this.kryptonRibbonQATButton1.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            // 
            // kryptonRibbonQATButton2
            // 
            this.kryptonRibbonQATButton2.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            // 
            // kryptonRibbonQATButton3
            // 
            this.kryptonRibbonQATButton3.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            // 
            // kryptonRibbonQATButton4
            // 
            this.kryptonRibbonQATButton4.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            // 
            // kryptonRibbonQATButton5
            // 
            this.kryptonRibbonQATButton5.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            // 
            // kryptonRibbonContext1
            // 
            this.kryptonRibbonContext1.ContextTitle = "Context Tools";
            // 
            // kryptonRibbonContext2
            // 
            this.kryptonRibbonContext2.ContextTitle = "Context Tools";
            // 
            // kryptonRibbonContext3
            // 
            this.kryptonRibbonContext3.ContextTitle = "Context Tools";
            // 
            // kryptonRibbonTab1
            // 
            this.kryptonRibbonTab1.Groups.AddRange(new Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup1});
            // 
            // kryptonRibbonGroup1
            // 
            this.kryptonRibbonGroup1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple1,
            this.kryptonRibbonGroupLines1,
            this.kryptonRibbonGroupGallery1,
            this.kryptonRibbonGroupTriple2});
            // 
            // kryptonRibbonGroupTriple1
            // 
            this.kryptonRibbonGroupTriple1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButton1,
            this.kryptonRibbonGroupButton2,
            this.kryptonRibbonGroupButton3});
            // 
            // kryptonRibbonGroupLines1
            // 
            this.kryptonRibbonGroupLines1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButton4,
            this.kryptonRibbonGroupButton5});
            // 
            // kryptonRibbonGroupGallery1
            // 
            this.kryptonRibbonGroupGallery1.ImageList = null;
            // 
            // kryptonRibbonGroupTriple2
            // 
            this.kryptonRibbonGroupTriple2.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButton6,
            this.kryptonRibbonGroupButton7,
            this.kryptonRibbonGroupThemeComboBox1});
            // 
            // kryptonRibbonGroupThemeComboBox1
            // 
            this.kryptonRibbonGroupThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            this.kryptonRibbonGroupThemeComboBox1.DisplayMember = "Key";
            this.kryptonRibbonGroupThemeComboBox1.DropDownWidth = 500;
            this.kryptonRibbonGroupThemeComboBox1.FormattingEnabled = false;
            this.kryptonRibbonGroupThemeComboBox1.ItemHeight = 16;
            this.kryptonRibbonGroupThemeComboBox1.MaximumSize = new System.Drawing.Size(500, 0);
            this.kryptonRibbonGroupThemeComboBox1.MinimumSize = new System.Drawing.Size(400, 0);
            this.kryptonRibbonGroupThemeComboBox1.ValueMember = "Value";
            // 
            // RibbonTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 488);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kryptonRibbon1);
            this.Name = "RibbonTest";
            this.Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Ribbon.KryptonRibbon kryptonRibbon1;
        private Krypton.Ribbon.KryptonRibbonTab kryptonRibbonTab1;
        private Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup1;
        private Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple1;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton1;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton2;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton3;
        private Krypton.Ribbon.KryptonRibbonGroupLines kryptonRibbonGroupLines1;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton4;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton5;
        private Krypton.Ribbon.KryptonRibbonGroupGallery kryptonRibbonGroupGallery1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple2;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton6;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton7;
        private Krypton.Ribbon.KryptonRibbonGroupThemeComboBox kryptonRibbonGroupThemeComboBox1;
        private Krypton.Ribbon.KryptonRibbonTab kryptonRibbonTab2;
        private Krypton.Ribbon.KryptonRibbonTab kryptonRibbonTab3;
        private Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private Krypton.Toolkit.KryptonCommand kcmdClose;
        private Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems2;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem2;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Ribbon.KryptonRibbonQATButton kryptonRibbonQATButton1;
        private Krypton.Ribbon.KryptonRibbonQATButton kryptonRibbonQATButton2;
        private Krypton.Ribbon.KryptonRibbonQATButton kryptonRibbonQATButton3;
        private Krypton.Ribbon.KryptonRibbonQATButton kryptonRibbonQATButton4;
        private Krypton.Ribbon.KryptonRibbonQATButton kryptonRibbonQATButton5;
        private Krypton.Ribbon.KryptonRibbonContext kryptonRibbonContext1;
        private Krypton.Ribbon.KryptonRibbonContext kryptonRibbonContext2;
        private Krypton.Ribbon.KryptonRibbonContext kryptonRibbonContext3;
        private KryptonCustomPaletteBase kryptonCustomPaletteBase1;
    }
}