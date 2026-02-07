#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class DropDownArrowsDemo
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
            this.pnlHeader = new Krypton.Toolkit.KryptonPanel();
            this.lblDpiInfo = new Krypton.Toolkit.KryptonLabel();
            this.btnRefresh = new Krypton.Toolkit.KryptonButton();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.lblTheme = new Krypton.Toolkit.KryptonLabel();
            this.lblDescription = new Krypton.Toolkit.KryptonLabel();
            this.grpMain = new Krypton.Toolkit.KryptonGroupBox();
            this.grpInputControls = new Krypton.Toolkit.KryptonGroupBox();
            this.numValue = new Krypton.Toolkit.KryptonNumericUpDown();
            this.dtpValue = new Krypton.Toolkit.KryptonDateTimePicker();
            this.cmbItems = new Krypton.Toolkit.KryptonComboBox();
            this.grpButtons = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonColorButton1 = new Krypton.Toolkit.KryptonColorButton();
            this.lblArrowColor = new Krypton.Toolkit.KryptonLabel();
            this.btnDisabled = new Krypton.Toolkit.KryptonButton();
            this.btnStandalone = new Krypton.Toolkit.KryptonButton();
            this.btnSplit = new Krypton.Toolkit.KryptonButton();
            this.btnDropDown = new Krypton.Toolkit.KryptonDropButton();
            this.kryptonContextMenu1 = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem1 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem2 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem3 = new Krypton.Toolkit.KryptonContextMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain.Panel)).BeginInit();
            this.grpMain.Panel.SuspendLayout();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpInputControls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInputControls.Panel)).BeginInit();
            this.grpInputControls.Panel.SuspendLayout();
            this.grpInputControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpButtons.Panel)).BeginInit();
            this.grpButtons.Panel.SuspendLayout();
            this.grpButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.Controls.Add(this.lblDpiInfo);
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Controls.Add(this.kryptonThemeComboBox1);
            this.pnlHeader.Controls.Add(this.lblTheme);
            this.pnlHeader.Controls.Add(this.lblDescription);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(12);
            this.pnlHeader.Size = new System.Drawing.Size(684, 130);
            this.pnlHeader.TabIndex = 0;
            //
            // lblDescription
            //
            this.lblDescription.Location = new System.Drawing.Point(15, 15);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(654, 36);
            this.lblDescription.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Values.Text = "Drop-down arrows are smaller (10px base @ 96 DPI) and DPI-aware. Move this window between monitors to verify scaling. Issue #2129.";
            //
            // lblTheme
            //
            this.lblTheme.Location = new System.Drawing.Point(15, 58);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(50, 20);
            this.lblTheme.TabIndex = 1;
            this.lblTheme.Values.Text = "Theme:";
            //
            // kryptonThemeComboBox1
            //
            this.kryptonThemeComboBox1.DropDownWidth = 250;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(71, 56);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(200, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 2;
            //
            // btnRefresh
            //
            this.btnRefresh.Location = new System.Drawing.Point(287, 55);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 25);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Values.Text = "Refresh";
            //
            // lblDpiInfo
            //
            this.lblDpiInfo.Location = new System.Drawing.Point(15, 90);
            this.lblDpiInfo.Name = "lblDpiInfo";
            this.lblDpiInfo.Size = new System.Drawing.Size(654, 20);
            this.lblDpiInfo.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblDpiInfo.TabIndex = 4;
            this.lblDpiInfo.Values.Text = "DPI: 96×96 | Scale: 100% | Drop-down arrow: 10×10px";
            //
            // grpMain
            //
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 130);
            this.grpMain.Name = "grpMain";
            this.grpMain.Panel.AutoScroll = true;
            this.grpMain.Size = new System.Drawing.Size(684, 331);
            this.grpMain.TabIndex = 1;
            this.grpMain.Values.Heading = "Controls with Drop-Down Arrows (Issue #2129, #1211)";
            //
            // grpMain.Panel
            //
            this.grpMain.Panel.Controls.Add(this.grpInputControls);
            this.grpMain.Panel.Controls.Add(this.grpButtons);
            //
            // grpButtons
            //
            this.grpButtons.Location = new System.Drawing.Point(15, 15);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(654, 180);
            this.grpButtons.TabIndex = 0;
            this.grpButtons.Values.Heading = "Buttons";
            //
            // grpButtons.Panel
            //
            this.grpButtons.Panel.Controls.Add(this.kryptonColorButton1);
            this.grpButtons.Panel.Controls.Add(this.lblArrowColor);
            this.grpButtons.Panel.Controls.Add(this.btnDisabled);
            this.grpButtons.Panel.Controls.Add(this.btnStandalone);
            this.grpButtons.Panel.Controls.Add(this.btnSplit);
            this.grpButtons.Panel.Controls.Add(this.btnDropDown);
            //
            // btnDropDown
            //
            this.btnDropDown.KryptonContextMenu = this.kryptonContextMenu1;
            this.btnDropDown.Location = new System.Drawing.Point(15, 35);
            this.btnDropDown.Name = "btnDropDown";
            this.btnDropDown.Size = new System.Drawing.Size(200, 28);
            this.btnDropDown.TabIndex = 0;
            this.btnDropDown.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnDropDown.Values.Text = "KryptonDropButton";
            //
            // btnSplit
            //
            this.btnSplit.KryptonContextMenu = this.kryptonContextMenu1;
            this.btnSplit.Location = new System.Drawing.Point(230, 35);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.ShowSplitOption = true;
            this.btnSplit.Size = new System.Drawing.Size(200, 28);
            this.btnSplit.TabIndex = 1;
            this.btnSplit.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSplit.Values.ShowSplitOption = true;
            this.btnSplit.Values.Text = "KryptonButton (Split)";
            //
            // btnStandalone
            //
            this.btnStandalone.Location = new System.Drawing.Point(445, 35);
            this.btnStandalone.Name = "btnStandalone";
            this.btnStandalone.Size = new System.Drawing.Size(200, 28);
            this.btnStandalone.TabIndex = 2;
            this.btnStandalone.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnStandalone.Values.Text = "KryptonButton (no drop-down)";
            //
            // btnDisabled
            //
            this.btnDisabled.Enabled = false;
            this.btnDisabled.KryptonContextMenu = this.kryptonContextMenu1;
            this.btnDisabled.Location = new System.Drawing.Point(15, 75);
            this.btnDisabled.Name = "btnDisabled";
            this.btnDisabled.Size = new System.Drawing.Size(200, 28);
            this.btnDisabled.TabIndex = 3;
            this.btnDisabled.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnDisabled.Values.Text = "Disabled Drop-Down";
            //
            // lblArrowColor
            //
            this.lblArrowColor.Location = new System.Drawing.Point(15, 118);
            this.lblArrowColor.Name = "lblArrowColor";
            this.lblArrowColor.Size = new System.Drawing.Size(120, 20);
            this.lblArrowColor.TabIndex = 4;
            this.lblArrowColor.Values.Text = "Drop-down arrow color:";
            //
            // kryptonColorButton1
            //
            this.kryptonColorButton1.Location = new System.Drawing.Point(145, 115);
            this.kryptonColorButton1.Name = "kryptonColorButton1";
            this.kryptonColorButton1.Size = new System.Drawing.Size(200, 28);
            this.kryptonColorButton1.TabIndex = 5;
            this.kryptonColorButton1.Values.Text = "Pick arrow color";
            //
            // kryptonContextMenu1
            //
            this.kryptonContextMenu1.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems1});
            //
            // kryptonContextMenuItems1
            //
            this.kryptonContextMenuItems1.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem1,
            this.kryptonContextMenuItem2,
            this.kryptonContextMenuItem3});
            //
            // kryptonContextMenuItem1
            //
            this.kryptonContextMenuItem1.Text = "Menu item 1";
            //
            // kryptonContextMenuItem2
            //
            this.kryptonContextMenuItem2.Text = "Menu item 2";
            //
            // kryptonContextMenuItem3
            //
            this.kryptonContextMenuItem3.Text = "Menu item 3";
            //
            // grpInputControls
            //
            this.grpInputControls.Location = new System.Drawing.Point(15, 210);
            this.grpInputControls.Name = "grpInputControls";
            this.grpInputControls.Size = new System.Drawing.Size(654, 110);
            this.grpInputControls.TabIndex = 1;
            this.grpInputControls.Values.Heading = "Input Controls with Drop-Down Arrows";
            //
            // grpInputControls.Panel
            //
            this.grpInputControls.Panel.Controls.Add(this.numValue);
            this.grpInputControls.Panel.Controls.Add(this.dtpValue);
            this.grpInputControls.Panel.Controls.Add(this.cmbItems);
            //
            // cmbItems
            //
            this.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItems.DropDownWidth = 200;
            this.cmbItems.Location = new System.Drawing.Point(15, 35);
            this.cmbItems.Name = "cmbItems";
            this.cmbItems.Size = new System.Drawing.Size(200, 25);
            this.cmbItems.TabIndex = 0;
            //
            // dtpValue
            //
            this.dtpValue.Location = new System.Drawing.Point(230, 35);
            this.dtpValue.Name = "dtpValue";
            this.dtpValue.Size = new System.Drawing.Size(200, 25);
            this.dtpValue.TabIndex = 1;
            //
            // numValue
            //
            this.numValue.Location = new System.Drawing.Point(445, 35);
            this.numValue.Name = "numValue";
            this.numValue.Size = new System.Drawing.Size(120, 25);
            this.numValue.TabIndex = 2;
            //
            // DropDownArrowsDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.pnlHeader);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "DropDownArrowsDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Drop-Down Arrows Demo (Issue #2129)";
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain.Panel)).EndInit();
            this.grpMain.Panel.ResumeLayout(false);
            this.grpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpInputControls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInputControls.Panel)).EndInit();
            this.grpInputControls.Panel.ResumeLayout(false);
            this.grpInputControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpButtons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpButtons.Panel)).EndInit();
            this.grpButtons.Panel.ResumeLayout(false);
            this.grpButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel pnlHeader;
        private Krypton.Toolkit.KryptonLabel lblDescription;
        private Krypton.Toolkit.KryptonLabel lblTheme;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonButton btnRefresh;
        private Krypton.Toolkit.KryptonLabel lblDpiInfo;
        private Krypton.Toolkit.KryptonGroupBox grpMain;
        private Krypton.Toolkit.KryptonGroupBox grpButtons;
        private Krypton.Toolkit.KryptonDropButton btnDropDown;
        private Krypton.Toolkit.KryptonButton btnSplit;
        private Krypton.Toolkit.KryptonButton btnStandalone;
        private Krypton.Toolkit.KryptonButton btnDisabled;
        private Krypton.Toolkit.KryptonLabel lblArrowColor;
        private Krypton.Toolkit.KryptonColorButton kryptonColorButton1;
        private Krypton.Toolkit.KryptonContextMenu kryptonContextMenu1;
        private Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem1;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem2;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem3;
        private Krypton.Toolkit.KryptonGroupBox grpInputControls;
        private Krypton.Toolkit.KryptonComboBox cmbItems;
        private Krypton.Toolkit.KryptonDateTimePicker dtpValue;
        private Krypton.Toolkit.KryptonNumericUpDown numValue;
    }
}
