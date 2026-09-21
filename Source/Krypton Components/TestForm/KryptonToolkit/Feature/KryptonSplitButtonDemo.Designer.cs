#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class KryptonSplitButtonDemo
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
            this.klblTheme = new Krypton.Toolkit.KryptonLabel();
            this.kcmbTheme = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kbtnPlain = new Krypton.Toolkit.KryptonButton();
            this.kbtnShowSplit = new Krypton.Toolkit.KryptonButton();
            this.kdropWhole = new Krypton.Toolkit.KryptonDropButton();
            this.ksplit = new Krypton.Toolkit.KryptonSplitButton();
            this.ksplitDisabled = new Krypton.Toolkit.KryptonSplitButton();
            this.ksplitMnemonic = new Krypton.Toolkit.KryptonSplitButton();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kcmMenu = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem1 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem2 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem3 = new Krypton.Toolkit.KryptonContextMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.klblInstructions);
            this.kryptonPanel1.Controls.Add(this.klblTheme);
            this.kryptonPanel1.Controls.Add(this.kcmbTheme);
            this.kryptonPanel1.Controls.Add(this.kbtnPlain);
            this.kryptonPanel1.Controls.Add(this.kbtnShowSplit);
            this.kryptonPanel1.Controls.Add(this.kdropWhole);
            this.kryptonPanel1.Controls.Add(this.ksplit);
            this.kryptonPanel1.Controls.Add(this.ksplitDisabled);
            this.kryptonPanel1.Controls.Add(this.ksplitMnemonic);
            this.kryptonPanel1.Controls.Add(this.klblStatus);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(16);
            this.kryptonPanel1.Size = new System.Drawing.Size(720, 420);
            this.kryptonPanel1.TabIndex = 0;
            //
            // klblInstructions
            //
            this.klblInstructions.Location = new System.Drawing.Point(19, 19);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(682, 72);
            this.klblInstructions.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.klblInstructions.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.klblInstructions.TabIndex = 0;
            this.klblInstructions.Values.Text = "Issue #4366: KryptonSplitButton is the Toolbox split control. Body click fires Click; chevron fires DropDown.\r\nKryptonButton + ShowSplitOption is the legacy opt-in. KryptonDropButton with Splitter off opens the menu from the whole button.\r\nAlt+S on &Save must Click (not open the menu). Switch the theme to check chevron contrast.";
            //
            // klblTheme
            //
            this.klblTheme.Location = new System.Drawing.Point(19, 100);
            this.klblTheme.Name = "klblTheme";
            this.klblTheme.Size = new System.Drawing.Size(50, 20);
            this.klblTheme.TabIndex = 1;
            this.klblTheme.Values.Text = "Theme:";
            //
            // kcmbTheme
            //
            this.kcmbTheme.DropDownWidth = 360;
            this.kcmbTheme.Location = new System.Drawing.Point(75, 98);
            this.kcmbTheme.Name = "kcmbTheme";
            this.kcmbTheme.Size = new System.Drawing.Size(360, 22);
            this.kcmbTheme.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbTheme.TabIndex = 2;
            //
            // kbtnPlain
            //
            this.kbtnPlain.Location = new System.Drawing.Point(19, 140);
            this.kbtnPlain.Name = "kbtnPlain";
            this.kbtnPlain.Size = new System.Drawing.Size(320, 32);
            this.kbtnPlain.TabIndex = 3;
            this.kbtnPlain.Values.Text = "KryptonButton";
            this.kbtnPlain.Click += new System.EventHandler(this.OnAnyClick);
            //
            // kbtnShowSplit
            //
            this.kbtnShowSplit.KryptonContextMenu = this.kcmMenu;
            this.kbtnShowSplit.Location = new System.Drawing.Point(355, 140);
            this.kbtnShowSplit.Name = "kbtnShowSplit";
            this.kbtnShowSplit.ShowSplitOption = true;
            this.kbtnShowSplit.Size = new System.Drawing.Size(320, 32);
            this.kbtnShowSplit.TabIndex = 4;
            this.kbtnShowSplit.Values.Text = "KryptonButton + ShowSplitOption";
            this.kbtnShowSplit.Click += new System.EventHandler(this.OnAnyClick);
            this.kbtnShowSplit.DropDown += new System.EventHandler<Krypton.Toolkit.ContextPositionMenuArgs>(this.OnAnyDropDown);
            //
            // kdropWhole
            //
            this.kdropWhole.KryptonContextMenu = this.kcmMenu;
            this.kdropWhole.Location = new System.Drawing.Point(19, 184);
            this.kdropWhole.Name = "kdropWhole";
            this.kdropWhole.Size = new System.Drawing.Size(320, 32);
            this.kdropWhole.Splitter = false;
            this.kdropWhole.TabIndex = 5;
            this.kdropWhole.Values.Text = "KryptonDropButton (Splitter = false)";
            this.kdropWhole.Click += new System.EventHandler(this.OnAnyClick);
            this.kdropWhole.DropDown += new System.EventHandler<Krypton.Toolkit.ContextPositionMenuArgs>(this.OnAnyDropDown);
            //
            // ksplit
            //
            this.ksplit.KryptonContextMenu = this.kcmMenu;
            this.ksplit.Location = new System.Drawing.Point(355, 184);
            this.ksplit.Name = "ksplit";
            this.ksplit.Size = new System.Drawing.Size(320, 32);
            this.ksplit.TabIndex = 6;
            this.ksplit.Values.Text = "KryptonSplitButton";
            this.ksplit.Click += new System.EventHandler(this.OnAnyClick);
            this.ksplit.DropDown += new System.EventHandler<Krypton.Toolkit.ContextPositionMenuArgs>(this.OnAnyDropDown);
            //
            // ksplitDisabled
            //
            this.ksplitDisabled.Enabled = false;
            this.ksplitDisabled.KryptonContextMenu = this.kcmMenu;
            this.ksplitDisabled.Location = new System.Drawing.Point(19, 228);
            this.ksplitDisabled.Name = "ksplitDisabled";
            this.ksplitDisabled.Size = new System.Drawing.Size(320, 32);
            this.ksplitDisabled.TabIndex = 7;
            this.ksplitDisabled.Values.Text = "KryptonSplitButton (disabled)";
            //
            // ksplitMnemonic
            //
            this.ksplitMnemonic.KryptonContextMenu = this.kcmMenu;
            this.ksplitMnemonic.Location = new System.Drawing.Point(355, 228);
            this.ksplitMnemonic.Name = "ksplitMnemonic";
            this.ksplitMnemonic.Size = new System.Drawing.Size(320, 32);
            this.ksplitMnemonic.TabIndex = 8;
            this.ksplitMnemonic.Values.Text = "&Save (mnemonic = Click)";
            this.ksplitMnemonic.Click += new System.EventHandler(this.OnAnyClick);
            this.ksplitMnemonic.DropDown += new System.EventHandler<Krypton.Toolkit.ContextPositionMenuArgs>(this.OnAnyDropDown);
            //
            // klblStatus
            //
            this.klblStatus.Location = new System.Drawing.Point(19, 280);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(656, 40);
            this.klblStatus.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.klblStatus.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.klblStatus.TabIndex = 9;
            this.klblStatus.Values.Text = "Status: click a button body or chevron. Last event appears here.";
            //
            // kcmMenu
            //
            this.kcmMenu.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
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
            this.kryptonContextMenuItem1.Text = "Open";
            //
            // kryptonContextMenuItem2
            //
            this.kryptonContextMenuItem2.Text = "Save As...";
            //
            // kryptonContextMenuItem3
            //
            this.kryptonContextMenuItem3.Text = "Exit";
            //
            // KryptonSplitButtonDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 360);
            this.Controls.Add(this.kryptonPanel1);
            this.MinimumSize = new System.Drawing.Size(640, 360);
            this.Name = "KryptonSplitButtonDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "KryptonSplitButton (#4366)";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonLabel klblInstructions;
        private Krypton.Toolkit.KryptonLabel klblTheme;
        private Krypton.Toolkit.KryptonThemeComboBox kcmbTheme;
        private Krypton.Toolkit.KryptonButton kbtnPlain;
        private Krypton.Toolkit.KryptonButton kbtnShowSplit;
        private Krypton.Toolkit.KryptonDropButton kdropWhole;
        private Krypton.Toolkit.KryptonSplitButton ksplit;
        private Krypton.Toolkit.KryptonSplitButton ksplitDisabled;
        private Krypton.Toolkit.KryptonSplitButton ksplitMnemonic;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private Krypton.Toolkit.KryptonContextMenu kcmMenu;
        private Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem1;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem2;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem3;
    }
}
