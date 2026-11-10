#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class TextListItemExample
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
            this.components = new System.ComponentModel.Container();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kbtnResetAll = new Krypton.Toolkit.KryptonButton();
            this.kbtnContrastDemo = new Krypton.Toolkit.KryptonButton();
            this.kbtnApplyButtonColor = new Krypton.Toolkit.KryptonButton();
            this.kcbtnButtonColor = new Krypton.Toolkit.KryptonColorButton();
            this.klblButtonSlot = new Krypton.Toolkit.KryptonLabel();
            this.kbtnApplyListItemColor = new Krypton.Toolkit.KryptonButton();
            this.kcbtnListItemColor = new Krypton.Toolkit.KryptonColorButton();
            this.klblListItemSlot = new Krypton.Toolkit.KryptonLabel();
            this.kbtnApplyLabelColor = new Krypton.Toolkit.KryptonButton();
            this.kcbtnLabelColor = new Krypton.Toolkit.KryptonColorButton();
            this.klblLabelSlot = new Krypton.Toolkit.KryptonLabel();
            this.klblCustomizeHeader = new Krypton.Toolkit.KryptonLabel();
            this.kryptonSeparator1 = new Krypton.Toolkit.KryptonSeparator();
            this.kgrpListItems = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonListView1 = new Krypton.Toolkit.KryptonListView();
            this.kryptonCheckedListBox1 = new Krypton.Toolkit.KryptonCheckedListBox();
            this.kryptonListBoxDisabled = new Krypton.Toolkit.KryptonListBox();
            this.kryptonListBox1 = new Krypton.Toolkit.KryptonListBox();
            this.kryptonTreeView1 = new Krypton.Toolkit.KryptonTreeView();
            this.klblListHint = new Krypton.Toolkit.KryptonLabel();
            this.kgrpButton = new Krypton.Toolkit.KryptonGroupBox();
            this.klblButtonHint = new Krypton.Toolkit.KryptonLabel();
            this.kbtnSample = new Krypton.Toolkit.KryptonButton();
            this.kgrpLabels = new Krypton.Toolkit.KryptonGroupBox();
            this.klblNormalPanel = new Krypton.Toolkit.KryptonLabel();
            this.klblBoldControl = new Krypton.Toolkit.KryptonLabel();
            this.klblNormalControl = new Krypton.Toolkit.KryptonLabel();
            this.klblSchemeReadout = new Krypton.Toolkit.KryptonLabel();
            this.klblDescription = new Krypton.Toolkit.KryptonLabel();
            this.klblTheme = new Krypton.Toolkit.KryptonLabel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpListItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpListItems.Panel)).BeginInit();
            this.kgrpListItems.Panel.SuspendLayout();
            this.kgrpListItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpButton.Panel)).BeginInit();
            this.kgrpButton.Panel.SuspendLayout();
            this.kgrpButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpLabels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpLabels.Panel)).BeginInit();
            this.kgrpLabels.Panel.SuspendLayout();
            this.kgrpLabels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonManager1
            //
            this.kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.klblStatus);
            this.kryptonPanel1.Controls.Add(this.kbtnResetAll);
            this.kryptonPanel1.Controls.Add(this.kbtnContrastDemo);
            this.kryptonPanel1.Controls.Add(this.kbtnApplyButtonColor);
            this.kryptonPanel1.Controls.Add(this.kcbtnButtonColor);
            this.kryptonPanel1.Controls.Add(this.klblButtonSlot);
            this.kryptonPanel1.Controls.Add(this.kbtnApplyListItemColor);
            this.kryptonPanel1.Controls.Add(this.kcbtnListItemColor);
            this.kryptonPanel1.Controls.Add(this.klblListItemSlot);
            this.kryptonPanel1.Controls.Add(this.kbtnApplyLabelColor);
            this.kryptonPanel1.Controls.Add(this.kcbtnLabelColor);
            this.kryptonPanel1.Controls.Add(this.klblLabelSlot);
            this.kryptonPanel1.Controls.Add(this.klblCustomizeHeader);
            this.kryptonPanel1.Controls.Add(this.kryptonSeparator1);
            this.kryptonPanel1.Controls.Add(this.kgrpListItems);
            this.kryptonPanel1.Controls.Add(this.kgrpButton);
            this.kryptonPanel1.Controls.Add(this.kgrpLabels);
            this.kryptonPanel1.Controls.Add(this.klblSchemeReadout);
            this.kryptonPanel1.Controls.Add(this.klblDescription);
            this.kryptonPanel1.Controls.Add(this.klblTheme);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanel1.Size = new System.Drawing.Size(984, 701);
            this.kryptonPanel1.TabIndex = 0;
            //
            // klblTheme
            //
            this.klblTheme.Location = new System.Drawing.Point(15, 15);
            this.klblTheme.Name = "klblTheme";
            this.klblTheme.Size = new System.Drawing.Size(45, 20);
            this.klblTheme.TabIndex = 0;
            this.klblTheme.Values.Text = "Theme:";
            //
            // kryptonThemeComboBox1
            //
            this.kryptonThemeComboBox1.DropDownWidth = 280;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(66, 13);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(280, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 1;
            this.kryptonThemeComboBox1.SelectedIndexChanged += new System.EventHandler(this.kryptonThemeComboBox1_SelectedIndexChanged);
            //
            // klblDescription
            //
            this.klblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.klblDescription.Location = new System.Drawing.Point(15, 42);
            this.klblDescription.Name = "klblDescription";
            this.klblDescription.Size = new System.Drawing.Size(954, 52);
            this.klblDescription.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.klblDescription.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.klblDescription.TabIndex = 2;
            this.klblDescription.Values.Text = "TextListItem scheme color demonstration.";
            //
            // klblSchemeReadout
            //
            this.klblSchemeReadout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.klblSchemeReadout.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblSchemeReadout.Location = new System.Drawing.Point(15, 98);
            this.klblSchemeReadout.Name = "klblSchemeReadout";
            this.klblSchemeReadout.Size = new System.Drawing.Size(954, 20);
            this.klblSchemeReadout.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.klblSchemeReadout.TabIndex = 3;
            this.klblSchemeReadout.Values.Text = "Scheme colors:";
            //
            // kgrpLabels
            //
            this.kgrpLabels.Location = new System.Drawing.Point(15, 128);
            this.kgrpLabels.Name = "kgrpLabels";
            this.kgrpLabels.Size = new System.Drawing.Size(300, 148);
            this.kgrpLabels.TabIndex = 4;
            this.kgrpLabels.Values.Heading = "TextLabelControl (labels)";
            //
            // kgrpLabels.Panel
            //
            this.kgrpLabels.Panel.Controls.Add(this.klblNormalPanel);
            this.kgrpLabels.Panel.Controls.Add(this.klblBoldControl);
            this.kgrpLabels.Panel.Controls.Add(this.klblNormalControl);
            //
            // klblNormalControl
            //
            this.klblNormalControl.Location = new System.Drawing.Point(12, 8);
            this.klblNormalControl.Name = "klblNormalControl";
            this.klblNormalControl.Size = new System.Drawing.Size(260, 20);
            this.klblNormalControl.TabIndex = 0;
            this.klblNormalControl.Values.Text = "LabelNormalControl";
            //
            // klblBoldControl
            //
            this.klblBoldControl.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblBoldControl.Location = new System.Drawing.Point(12, 34);
            this.klblBoldControl.Name = "klblBoldControl";
            this.klblBoldControl.Size = new System.Drawing.Size(260, 20);
            this.klblBoldControl.TabIndex = 1;
            this.klblBoldControl.Values.Text = "LabelBoldControl";
            //
            // klblNormalPanel
            //
            this.klblNormalPanel.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
            this.klblNormalPanel.Location = new System.Drawing.Point(12, 60);
            this.klblNormalPanel.Name = "klblNormalPanel";
            this.klblNormalPanel.Size = new System.Drawing.Size(260, 20);
            this.klblNormalPanel.TabIndex = 2;
            this.klblNormalPanel.Values.Text = "LabelNormalPanel";
            //
            // kgrpButton
            //
            this.kgrpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kgrpButton.Location = new System.Drawing.Point(738, 128);
            this.kgrpButton.Name = "kgrpButton";
            this.kgrpButton.Size = new System.Drawing.Size(231, 148);
            this.kgrpButton.TabIndex = 5;
            this.kgrpButton.Values.Heading = "TextButtonNormal (buttons)";
            //
            // kgrpButton.Panel
            //
            this.kgrpButton.Panel.Controls.Add(this.klblButtonHint);
            this.kgrpButton.Panel.Controls.Add(this.kbtnSample);
            //
            // kbtnSample
            //
            this.kbtnSample.Location = new System.Drawing.Point(12, 12);
            this.kbtnSample.Name = "kbtnSample";
            this.kbtnSample.Size = new System.Drawing.Size(200, 32);
            this.kbtnSample.TabIndex = 0;
            this.kbtnSample.Values.Text = "KryptonButton sample";
            //
            // klblButtonHint
            //
            this.klblButtonHint.Location = new System.Drawing.Point(12, 52);
            this.klblButtonHint.Name = "klblButtonHint";
            this.klblButtonHint.Size = new System.Drawing.Size(200, 36);
            this.klblButtonHint.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.klblButtonHint.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.klblButtonHint.TabIndex = 1;
            this.klblButtonHint.Values.Text = "Hover to see tracking/checked text colors on supported themes.";
            //
            // kgrpListItems
            //
            this.kgrpListItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.kgrpListItems.Location = new System.Drawing.Point(15, 288);
            this.kgrpListItems.Name = "kgrpListItems";
            this.kgrpListItems.Size = new System.Drawing.Size(954, 248);
            this.kgrpListItems.TabIndex = 6;
            this.kgrpListItems.Values.Heading = "TextListItem (tree / list controls)";
            //
            // kgrpListItems.Panel
            //
            this.kgrpListItems.Panel.Controls.Add(this.kryptonListView1);
            this.kgrpListItems.Panel.Controls.Add(this.kryptonCheckedListBox1);
            this.kgrpListItems.Panel.Controls.Add(this.kryptonListBoxDisabled);
            this.kgrpListItems.Panel.Controls.Add(this.kryptonListBox1);
            this.kgrpListItems.Panel.Controls.Add(this.kryptonTreeView1);
            this.kgrpListItems.Panel.Controls.Add(this.klblListHint);
            //
            // klblListHint
            //
            this.klblListHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.klblListHint.Location = new System.Drawing.Point(12, 8);
            this.klblListHint.Name = "klblListHint";
            this.klblListHint.Size = new System.Drawing.Size(926, 20);
            this.klblListHint.TabIndex = 0;
            this.klblListHint.Values.Text = "KryptonTreeView, KryptonListBox, KryptonCheckedListBox, KryptonListView — normal item text uses TextListItem.";
            //
            // kryptonTreeView1
            //
            this.kryptonTreeView1.Location = new System.Drawing.Point(12, 34);
            this.kryptonTreeView1.Name = "kryptonTreeView1";
            this.kryptonTreeView1.Size = new System.Drawing.Size(220, 168);
            this.kryptonTreeView1.TabIndex = 1;
            //
            // kryptonListBox1
            //
            this.kryptonListBox1.Location = new System.Drawing.Point(242, 34);
            this.kryptonListBox1.Name = "kryptonListBox1";
            this.kryptonListBox1.Size = new System.Drawing.Size(220, 168);
            this.kryptonListBox1.TabIndex = 2;
            //
            // kryptonListBoxDisabled
            //
            this.kryptonListBoxDisabled.Enabled = false;
            this.kryptonListBoxDisabled.Location = new System.Drawing.Point(472, 34);
            this.kryptonListBoxDisabled.Name = "kryptonListBoxDisabled";
            this.kryptonListBoxDisabled.Size = new System.Drawing.Size(220, 80);
            this.kryptonListBoxDisabled.TabIndex = 3;
            //
            // kryptonCheckedListBox1
            //
            this.kryptonCheckedListBox1.CheckOnClick = true;
            this.kryptonCheckedListBox1.Location = new System.Drawing.Point(472, 122);
            this.kryptonCheckedListBox1.Name = "kryptonCheckedListBox1";
            this.kryptonCheckedListBox1.Size = new System.Drawing.Size(220, 80);
            this.kryptonCheckedListBox1.TabIndex = 4;
            //
            // kryptonListView1
            //
            this.kryptonListView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonListView1.HideSelection = false;
            this.kryptonListView1.Location = new System.Drawing.Point(702, 34);
            this.kryptonListView1.Name = "kryptonListView1";
            this.kryptonListView1.Size = new System.Drawing.Size(236, 168);
            this.kryptonListView1.TabIndex = 5;
            //
            // kryptonSeparator1
            //
            this.kryptonSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonSeparator1.Location = new System.Drawing.Point(15, 548);
            this.kryptonSeparator1.Name = "kryptonSeparator1";
            this.kryptonSeparator1.Size = new System.Drawing.Size(954, 10);
            this.kryptonSeparator1.TabIndex = 7;
            //
            // klblCustomizeHeader
            //
            this.klblCustomizeHeader.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.klblCustomizeHeader.Location = new System.Drawing.Point(15, 568);
            this.klblCustomizeHeader.Name = "klblCustomizeHeader";
            this.klblCustomizeHeader.Size = new System.Drawing.Size(320, 20);
            this.klblCustomizeHeader.TabIndex = 8;
            this.klblCustomizeHeader.Values.Text = "Customize active palette scheme colors:";
            //
            // klblLabelSlot
            //
            this.klblLabelSlot.Location = new System.Drawing.Point(15, 594);
            this.klblLabelSlot.Name = "klblLabelSlot";
            this.klblLabelSlot.Size = new System.Drawing.Size(200, 20);
            this.klblLabelSlot.TabIndex = 9;
            this.klblLabelSlot.Values.Text = "TextLabelControl";
            //
            // kcbtnLabelColor
            //
            this.kcbtnLabelColor.Location = new System.Drawing.Point(15, 620);
            this.kcbtnLabelColor.Name = "kcbtnLabelColor";
            this.kcbtnLabelColor.SelectedColor = System.Drawing.Color.Empty;
            this.kcbtnLabelColor.Size = new System.Drawing.Size(120, 28);
            this.kcbtnLabelColor.TabIndex = 10;
            this.kcbtnLabelColor.Values.Text = "Pick";
            this.kcbtnLabelColor.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnLabelColor_SelectedColorChanged);
            //
            // kbtnApplyLabelColor
            //
            this.kbtnApplyLabelColor.Location = new System.Drawing.Point(145, 620);
            this.kbtnApplyLabelColor.Name = "kbtnApplyLabelColor";
            this.kbtnApplyLabelColor.Size = new System.Drawing.Size(70, 28);
            this.kbtnApplyLabelColor.TabIndex = 11;
            this.kbtnApplyLabelColor.Values.Text = "Apply";
            this.kbtnApplyLabelColor.Click += new System.EventHandler(this.kbtnApplyLabelColor_Click);
            //
            // klblListItemSlot
            //
            this.klblListItemSlot.Location = new System.Drawing.Point(240, 594);
            this.klblListItemSlot.Name = "klblListItemSlot";
            this.klblListItemSlot.Size = new System.Drawing.Size(200, 20);
            this.klblListItemSlot.TabIndex = 12;
            this.klblListItemSlot.Values.Text = "TextListItem";
            //
            // kcbtnListItemColor
            //
            this.kcbtnListItemColor.Location = new System.Drawing.Point(240, 620);
            this.kcbtnListItemColor.Name = "kcbtnListItemColor";
            this.kcbtnListItemColor.SelectedColor = System.Drawing.Color.Empty;
            this.kcbtnListItemColor.Size = new System.Drawing.Size(120, 28);
            this.kcbtnListItemColor.TabIndex = 13;
            this.kcbtnListItemColor.Values.Text = "Pick";
            this.kcbtnListItemColor.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnListItemColor_SelectedColorChanged);
            //
            // kbtnApplyListItemColor
            //
            this.kbtnApplyListItemColor.Location = new System.Drawing.Point(370, 620);
            this.kbtnApplyListItemColor.Name = "kbtnApplyListItemColor";
            this.kbtnApplyListItemColor.Size = new System.Drawing.Size(70, 28);
            this.kbtnApplyListItemColor.TabIndex = 14;
            this.kbtnApplyListItemColor.Values.Text = "Apply";
            this.kbtnApplyListItemColor.Click += new System.EventHandler(this.kbtnApplyListItemColor_Click);
            //
            // klblButtonSlot
            //
            this.klblButtonSlot.Location = new System.Drawing.Point(465, 594);
            this.klblButtonSlot.Name = "klblButtonSlot";
            this.klblButtonSlot.Size = new System.Drawing.Size(200, 20);
            this.klblButtonSlot.TabIndex = 15;
            this.klblButtonSlot.Values.Text = "TextButtonNormal";
            //
            // kcbtnButtonColor
            //
            this.kcbtnButtonColor.Location = new System.Drawing.Point(465, 620);
            this.kcbtnButtonColor.Name = "kcbtnButtonColor";
            this.kcbtnButtonColor.SelectedColor = System.Drawing.Color.Empty;
            this.kcbtnButtonColor.Size = new System.Drawing.Size(120, 28);
            this.kcbtnButtonColor.TabIndex = 16;
            this.kcbtnButtonColor.Values.Text = "Pick";
            this.kcbtnButtonColor.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnButtonColor_SelectedColorChanged);
            //
            // kbtnApplyButtonColor
            //
            this.kbtnApplyButtonColor.Location = new System.Drawing.Point(595, 620);
            this.kbtnApplyButtonColor.Name = "kbtnApplyButtonColor";
            this.kbtnApplyButtonColor.Size = new System.Drawing.Size(70, 28);
            this.kbtnApplyButtonColor.TabIndex = 17;
            this.kbtnApplyButtonColor.Values.Text = "Apply";
            this.kbtnApplyButtonColor.Click += new System.EventHandler(this.kbtnApplyButtonColor_Click);
            //
            // kbtnContrastDemo
            //
            this.kbtnContrastDemo.Location = new System.Drawing.Point(690, 620);
            this.kbtnContrastDemo.Name = "kbtnContrastDemo";
            this.kbtnContrastDemo.Size = new System.Drawing.Size(130, 28);
            this.kbtnContrastDemo.TabIndex = 18;
            this.kbtnContrastDemo.Values.Text = "Contrast demo";
            this.kbtnContrastDemo.Click += new System.EventHandler(this.kbtnContrastDemo_Click);
            //
            // kbtnResetAll
            //
            this.kbtnResetAll.Location = new System.Drawing.Point(830, 620);
            this.kbtnResetAll.Name = "kbtnResetAll";
            this.kbtnResetAll.Size = new System.Drawing.Size(90, 28);
            this.kbtnResetAll.TabIndex = 19;
            this.kbtnResetAll.Values.Text = "Reset all";
            this.kbtnResetAll.Click += new System.EventHandler(this.kbtnResetAll_Click);
            //
            // klblStatus
            //
            this.klblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.klblStatus.Location = new System.Drawing.Point(15, 662);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(954, 20);
            this.klblStatus.TabIndex = 20;
            this.klblStatus.Values.Text = "Use Contrast demo, then change one picker at a time to confirm each control group responds independently.";
            //
            // TextListItemExample
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 701);
            this.Controls.Add(this.kryptonPanel1);
            this.MinimumSize = new System.Drawing.Size(900, 640);
            this.Name = "TextListItemExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TextListItem Scheme Colors (Issue #880)";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpListItems.Panel)).EndInit();
            this.kgrpListItems.Panel.ResumeLayout(false);
            this.kgrpListItems.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpListItems)).EndInit();
            this.kgrpListItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgrpButton.Panel)).EndInit();
            this.kgrpButton.Panel.ResumeLayout(false);
            this.kgrpButton.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpButton)).EndInit();
            this.kgrpButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgrpLabels.Panel)).EndInit();
            this.kgrpLabels.Panel.ResumeLayout(false);
            this.kgrpLabels.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpLabels)).EndInit();
            this.kgrpLabels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonLabel klblTheme;
        private Krypton.Toolkit.KryptonLabel klblDescription;
        private Krypton.Toolkit.KryptonLabel klblSchemeReadout;
        private Krypton.Toolkit.KryptonGroupBox kgrpLabels;
        private Krypton.Toolkit.KryptonLabel klblNormalControl;
        private Krypton.Toolkit.KryptonLabel klblBoldControl;
        private Krypton.Toolkit.KryptonLabel klblNormalPanel;
        private Krypton.Toolkit.KryptonGroupBox kgrpButton;
        private Krypton.Toolkit.KryptonButton kbtnSample;
        private Krypton.Toolkit.KryptonLabel klblButtonHint;
        private Krypton.Toolkit.KryptonGroupBox kgrpListItems;
        private Krypton.Toolkit.KryptonLabel klblListHint;
        private Krypton.Toolkit.KryptonTreeView kryptonTreeView1;
        private Krypton.Toolkit.KryptonListBox kryptonListBox1;
        private Krypton.Toolkit.KryptonListBox kryptonListBoxDisabled;
        private Krypton.Toolkit.KryptonCheckedListBox kryptonCheckedListBox1;
        private Krypton.Toolkit.KryptonListView kryptonListView1;
        private Krypton.Toolkit.KryptonSeparator kryptonSeparator1;
        private Krypton.Toolkit.KryptonLabel klblCustomizeHeader;
        private Krypton.Toolkit.KryptonLabel klblLabelSlot;
        private Krypton.Toolkit.KryptonColorButton kcbtnLabelColor;
        private Krypton.Toolkit.KryptonButton kbtnApplyLabelColor;
        private Krypton.Toolkit.KryptonLabel klblListItemSlot;
        private Krypton.Toolkit.KryptonColorButton kcbtnListItemColor;
        private Krypton.Toolkit.KryptonButton kbtnApplyListItemColor;
        private Krypton.Toolkit.KryptonLabel klblButtonSlot;
        private Krypton.Toolkit.KryptonColorButton kcbtnButtonColor;
        private Krypton.Toolkit.KryptonButton kbtnApplyButtonColor;
        private Krypton.Toolkit.KryptonButton kbtnContrastDemo;
        private Krypton.Toolkit.KryptonButton kbtnResetAll;
        private Krypton.Toolkit.KryptonLabel klblStatus;
    }
}
