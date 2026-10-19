#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Bug3661KryptonContextMenuOverflowDemo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.lblInstruction = new Krypton.Toolkit.KryptonWrapLabel();
            this.lblSectionConfig = new Krypton.Toolkit.KryptonLabel();
            this.klblItemCount = new Krypton.Toolkit.KryptonLabel();
            this.knumItemCount = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kchkOverflowArrows = new Krypton.Toolkit.KryptonCheckBox();
            this.lblSectionProgrammatic = new Krypton.Toolkit.KryptonLabel();
            this.kbtnShowMouse = new Krypton.Toolkit.KryptonButton();
            this.kbtnShowScreenTop = new Krypton.Toolkit.KryptonButton();
            this.kbtnShowScreenCenter = new Krypton.Toolkit.KryptonButton();
            this.kbtnShowScreenBottom = new Krypton.Toolkit.KryptonButton();
            this.kbtnKeyboardOpen = new Krypton.Toolkit.KryptonButton();
            this.lblSectionAttached = new Krypton.Toolkit.KryptonLabel();
            this.kbtnLongList = new Krypton.Toolkit.KryptonButton();
            this.kbtnMixedList = new Krypton.Toolkit.KryptonButton();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kbtnAnchorTarget = new Krypton.Toolkit.KryptonButton();
            this.lblSectionAnchor = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.AutoScroll = true;
            this.kryptonPanelMain.Controls.Add(this.kbtnAnchorTarget);
            this.kryptonPanelMain.Controls.Add(this.lblSectionAnchor);
            this.kryptonPanelMain.Controls.Add(this.klblStatus);
            this.kryptonPanelMain.Controls.Add(this.kbtnMixedList);
            this.kryptonPanelMain.Controls.Add(this.kbtnLongList);
            this.kryptonPanelMain.Controls.Add(this.lblSectionAttached);
            this.kryptonPanelMain.Controls.Add(this.kbtnKeyboardOpen);
            this.kryptonPanelMain.Controls.Add(this.kbtnShowScreenBottom);
            this.kryptonPanelMain.Controls.Add(this.kbtnShowScreenCenter);
            this.kryptonPanelMain.Controls.Add(this.kbtnShowScreenTop);
            this.kryptonPanelMain.Controls.Add(this.kbtnShowMouse);
            this.kryptonPanelMain.Controls.Add(this.lblSectionProgrammatic);
            this.kryptonPanelMain.Controls.Add(this.kchkOverflowArrows);
            this.kryptonPanelMain.Controls.Add(this.knumItemCount);
            this.kryptonPanelMain.Controls.Add(this.klblItemCount);
            this.kryptonPanelMain.Controls.Add(this.lblSectionConfig);
            this.kryptonPanelMain.Controls.Add(this.lblInstruction);
            this.kryptonPanelMain.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(684, 561);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // kryptonThemeComboBox1
            //
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Global;
            this.kryptonThemeComboBox1.DropDownWidth = 220;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(15, 15);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(220, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 0;
            //
            // lblInstruction
            //
            this.lblInstruction.AutoSize = false;
            this.lblInstruction.Location = new System.Drawing.Point(15, 48);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(640, 130);
            this.lblInstruction.Text = "Issue #3661: When a KryptonContextMenu has more items than fit on screen, Scroll Up " +
    "and Scroll Down rows appear instead of clipping. Verify: (1) hover a scroll row — " +
    "the list should advance while the pointer stays on the row; (2) mouse wheel over the " +
    "menu; (3) arrow keys past the first/last visible item; (4) keyboard-open places focus " +
    "on the first item. Use \"Near bottom of screen\" or shrink the window height to force overflow.";
            //
            // lblSectionConfig
            //
            this.lblSectionConfig.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblSectionConfig.Location = new System.Drawing.Point(15, 188);
            this.lblSectionConfig.Name = "lblSectionConfig";
            this.lblSectionConfig.Size = new System.Drawing.Size(200, 27);
            this.lblSectionConfig.TabIndex = 2;
            this.lblSectionConfig.Values.Text = "Configuration";
            //
            // klblItemCount
            //
            this.klblItemCount.Location = new System.Drawing.Point(15, 222);
            this.klblItemCount.Name = "klblItemCount";
            this.klblItemCount.Size = new System.Drawing.Size(140, 20);
            this.klblItemCount.TabIndex = 3;
            this.klblItemCount.Values.Text = "Menu item count: 65";
            //
            // knumItemCount
            //
            this.knumItemCount.Location = new System.Drawing.Point(200, 220);
            this.knumItemCount.Maximum = new decimal(new int[] { 150, 0, 0, 0 });
            this.knumItemCount.Minimum = new decimal(new int[] { 15, 0, 0, 0 });
            this.knumItemCount.Name = "knumItemCount";
            this.knumItemCount.Size = new System.Drawing.Size(80, 22);
            this.knumItemCount.TabIndex = 4;
            this.knumItemCount.Value = new decimal(new int[] { 65, 0, 0, 0 });
            //
            // kchkOverflowArrows
            //
            this.kchkOverflowArrows.Location = new System.Drawing.Point(300, 220);
            this.kchkOverflowArrows.Name = "kchkOverflowArrows";
            this.kchkOverflowArrows.Size = new System.Drawing.Size(220, 20);
            this.kchkOverflowArrows.TabIndex = 5;
            this.kchkOverflowArrows.Values.Text = "Overflow rows use arrows";
            //
            // lblSectionProgrammatic
            //
            this.lblSectionProgrammatic.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblSectionProgrammatic.Location = new System.Drawing.Point(15, 258);
            this.lblSectionProgrammatic.Name = "lblSectionProgrammatic";
            this.lblSectionProgrammatic.Size = new System.Drawing.Size(280, 27);
            this.lblSectionProgrammatic.TabIndex = 5;
            this.lblSectionProgrammatic.Values.Text = "Programmatic placement (long list)";
            //
            // kbtnShowMouse
            //
            this.kbtnShowMouse.Location = new System.Drawing.Point(15, 292);
            this.kbtnShowMouse.Name = "kbtnShowMouse";
            this.kbtnShowMouse.Size = new System.Drawing.Size(200, 28);
            this.kbtnShowMouse.TabIndex = 6;
            this.kbtnShowMouse.Values.Text = "Show below anchor button";
            this.kbtnShowMouse.Click += new System.EventHandler(this.kbtnShowMouse_Click);
            //
            // kbtnShowScreenTop
            //
            this.kbtnShowScreenTop.Location = new System.Drawing.Point(225, 292);
            this.kbtnShowScreenTop.Name = "kbtnShowScreenTop";
            this.kbtnShowScreenTop.Size = new System.Drawing.Size(200, 28);
            this.kbtnShowScreenTop.TabIndex = 7;
            this.kbtnShowScreenTop.Values.Text = "Near top of screen";
            this.kbtnShowScreenTop.Click += new System.EventHandler(this.kbtnShowScreenTop_Click);
            //
            // kbtnShowScreenCenter
            //
            this.kbtnShowScreenCenter.Location = new System.Drawing.Point(435, 292);
            this.kbtnShowScreenCenter.Name = "kbtnShowScreenCenter";
            this.kbtnShowScreenCenter.Size = new System.Drawing.Size(200, 28);
            this.kbtnShowScreenCenter.TabIndex = 8;
            this.kbtnShowScreenCenter.Values.Text = "Screen center";
            this.kbtnShowScreenCenter.Click += new System.EventHandler(this.kbtnShowScreenCenter_Click);
            //
            // kbtnShowScreenBottom
            //
            this.kbtnShowScreenBottom.Location = new System.Drawing.Point(15, 328);
            this.kbtnShowScreenBottom.Name = "kbtnShowScreenBottom";
            this.kbtnShowScreenBottom.Size = new System.Drawing.Size(200, 28);
            this.kbtnShowScreenBottom.TabIndex = 9;
            this.kbtnShowScreenBottom.Values.Text = "Near bottom of screen";
            this.kbtnShowScreenBottom.Click += new System.EventHandler(this.kbtnShowScreenBottom_Click);
            //
            // kbtnKeyboardOpen
            //
            this.kbtnKeyboardOpen.Location = new System.Drawing.Point(225, 328);
            this.kbtnKeyboardOpen.Name = "kbtnKeyboardOpen";
            this.kbtnKeyboardOpen.Size = new System.Drawing.Size(200, 28);
            this.kbtnKeyboardOpen.TabIndex = 10;
            this.kbtnKeyboardOpen.Values.Text = "Keyboard-activated open";
            this.kbtnKeyboardOpen.Click += new System.EventHandler(this.kbtnKeyboardOpen_Click);
            //
            // lblSectionAttached
            //
            this.lblSectionAttached.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblSectionAttached.Location = new System.Drawing.Point(15, 368);
            this.lblSectionAttached.Name = "lblSectionAttached";
            this.lblSectionAttached.Size = new System.Drawing.Size(320, 27);
            this.lblSectionAttached.TabIndex = 11;
            this.lblSectionAttached.Values.Text = "Attached menus (right-click)";
            //
            // kbtnLongList
            //
            this.kbtnLongList.Location = new System.Drawing.Point(15, 402);
            this.kbtnLongList.Name = "kbtnLongList";
            this.kbtnLongList.Size = new System.Drawing.Size(280, 32);
            this.kbtnLongList.TabIndex = 12;
            this.kbtnLongList.Values.Text = "Long list — right-click here";
            //
            // kbtnMixedList
            //
            this.kbtnMixedList.Location = new System.Drawing.Point(310, 402);
            this.kbtnMixedList.Name = "kbtnMixedList";
            this.kbtnMixedList.Size = new System.Drawing.Size(325, 32);
            this.kbtnMixedList.TabIndex = 13;
            this.kbtnMixedList.Values.Text = "Mixed headings / separators / extras — right-click";
            //
            // klblStatus
            //
            this.klblStatus.Location = new System.Drawing.Point(15, 448);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(620, 40);
            this.klblStatus.TabIndex = 14;
            this.klblStatus.Values.Text = "Status: open a menu to begin.";
            //
            // lblSectionAnchor
            //
            this.lblSectionAnchor.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblSectionAnchor.Location = new System.Drawing.Point(15, 498);
            this.lblSectionAnchor.Name = "lblSectionAnchor";
            this.lblSectionAnchor.Size = new System.Drawing.Size(200, 27);
            this.lblSectionAnchor.TabIndex = 15;
            this.lblSectionAnchor.Values.Text = "Anchor for placement";
            //
            // kbtnAnchorTarget
            //
            this.kbtnAnchorTarget.Location = new System.Drawing.Point(15, 532);
            this.kbtnAnchorTarget.Name = "kbtnAnchorTarget";
            this.kbtnAnchorTarget.Size = new System.Drawing.Size(240, 28);
            this.kbtnAnchorTarget.TabIndex = 16;
            this.kbtnAnchorTarget.Values.Text = "Anchor button (also has long list menu)";
            //
            // Bug3661KryptonContextMenuOverflowDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.Controls.Add(this.kryptonPanelMain);
            this.Name = "Bug3661KryptonContextMenuOverflowDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Issue #3661: KryptonContextMenu Overflow Demo";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.kryptonPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonWrapLabel lblInstruction;
        private Krypton.Toolkit.KryptonLabel lblSectionConfig;
        private Krypton.Toolkit.KryptonLabel klblItemCount;
        private Krypton.Toolkit.KryptonNumericUpDown knumItemCount;
        private Krypton.Toolkit.KryptonCheckBox kchkOverflowArrows;
        private Krypton.Toolkit.KryptonLabel lblSectionProgrammatic;
        private Krypton.Toolkit.KryptonButton kbtnShowMouse;
        private Krypton.Toolkit.KryptonButton kbtnShowScreenTop;
        private Krypton.Toolkit.KryptonButton kbtnShowScreenCenter;
        private Krypton.Toolkit.KryptonButton kbtnShowScreenBottom;
        private Krypton.Toolkit.KryptonButton kbtnKeyboardOpen;
        private Krypton.Toolkit.KryptonLabel lblSectionAttached;
        private Krypton.Toolkit.KryptonButton kbtnLongList;
        private Krypton.Toolkit.KryptonButton kbtnMixedList;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private Krypton.Toolkit.KryptonLabel lblSectionAnchor;
        private Krypton.Toolkit.KryptonButton kbtnAnchorTarget;
    }
}
