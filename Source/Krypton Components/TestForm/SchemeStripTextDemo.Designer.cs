#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class SchemeStripTextDemo
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
            this.kryptonMenuStrip1 = new Krypton.Toolkit.KryptonMenuStrip();
            this.kryptonFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.kryptonFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.kryptonFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.kryptonEditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.kryptonEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.kryptonToolStrip1 = new Krypton.Toolkit.KryptonToolStrip();
            this.ktsbNew = new System.Windows.Forms.ToolStripButton();
            this.ktsbOpen = new System.Windows.Forms.ToolStripButton();
            this.ktslReady = new System.Windows.Forms.ToolStripLabel();
            this.kryptonStatusStrip1 = new Krypton.Toolkit.KryptonStatusStrip();
            this.ksslKrypton = new System.Windows.Forms.ToolStripStatusLabel();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kbtnResetAll = new Krypton.Toolkit.KryptonButton();
            this.kbtnContrastDemo = new Krypton.Toolkit.KryptonButton();
            this.kbtnKryptonContext = new Krypton.Toolkit.KryptonButton();
            this.kgrpPickers = new Krypton.Toolkit.KryptonGroupBox();
            this.klblMenuItemSlot = new Krypton.Toolkit.KryptonLabel();
            this.kcbtnMenuItem = new Krypton.Toolkit.KryptonColorButton();
            this.klblStatusSlot = new Krypton.Toolkit.KryptonLabel();
            this.kcbtnStatusStrip = new Krypton.Toolkit.KryptonColorButton();
            this.klblToolSlot = new Krypton.Toolkit.KryptonLabel();
            this.kcbtnToolStrip = new Krypton.Toolkit.KryptonColorButton();
            this.klblMenuSlot = new Krypton.Toolkit.KryptonLabel();
            this.kcbtnMenuStrip = new Krypton.Toolkit.KryptonColorButton();
            this.kgrpNative = new Krypton.Toolkit.KryptonGroupBox();
            this.nativeStatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.nsslNative = new System.Windows.Forms.ToolStripStatusLabel();
            this.nativeToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ntsbNative = new System.Windows.Forms.ToolStripButton();
            this.nativeMenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nativeFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.nativeFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.klblNativeHint = new Krypton.Toolkit.KryptonLabel();
            this.klblSchemeReadout = new Krypton.Toolkit.KryptonLabel();
            this.klblDescription = new Krypton.Toolkit.KryptonLabel();
            this.klblTheme = new Krypton.Toolkit.KryptonLabel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonContextMenu1 = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem1 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem2 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.nativeContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nativeContextItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nativeContextItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.kryptonMenuStrip1.SuspendLayout();
            this.kryptonToolStrip1.SuspendLayout();
            this.kryptonStatusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpPickers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpPickers.Panel)).BeginInit();
            this.kgrpPickers.Panel.SuspendLayout();
            this.kgrpPickers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpNative)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpNative.Panel)).BeginInit();
            this.kgrpNative.Panel.SuspendLayout();
            this.kgrpNative.SuspendLayout();
            this.nativeStatusStrip1.SuspendLayout();
            this.nativeToolStrip1.SuspendLayout();
            this.nativeMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.nativeContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonManager1
            //
            this.kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            //
            // kryptonMenuStrip1
            //
            this.kryptonMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kryptonFileMenu,
            this.kryptonEditMenu});
            this.kryptonMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.kryptonMenuStrip1.Name = "kryptonMenuStrip1";
            this.kryptonMenuStrip1.Size = new System.Drawing.Size(984, 24);
            this.kryptonMenuStrip1.TabIndex = 0;
            this.kryptonMenuStrip1.Text = "kryptonMenuStrip1";
            //
            // kryptonFileMenu
            //
            this.kryptonFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kryptonFileOpen,
            this.kryptonFileSave});
            this.kryptonFileMenu.Name = "kryptonFileMenu";
            this.kryptonFileMenu.Size = new System.Drawing.Size(37, 20);
            this.kryptonFileMenu.Text = "&File";
            //
            // kryptonFileOpen
            //
            this.kryptonFileOpen.Name = "kryptonFileOpen";
            this.kryptonFileOpen.Size = new System.Drawing.Size(180, 22);
            this.kryptonFileOpen.Text = "&Open";
            //
            // kryptonFileSave
            //
            this.kryptonFileSave.Name = "kryptonFileSave";
            this.kryptonFileSave.Size = new System.Drawing.Size(180, 22);
            this.kryptonFileSave.Text = "&Save";
            //
            // kryptonEditMenu
            //
            this.kryptonEditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kryptonEditCopy});
            this.kryptonEditMenu.Name = "kryptonEditMenu";
            this.kryptonEditMenu.Size = new System.Drawing.Size(39, 20);
            this.kryptonEditMenu.Text = "&Edit";
            //
            // kryptonEditCopy
            //
            this.kryptonEditCopy.Name = "kryptonEditCopy";
            this.kryptonEditCopy.Size = new System.Drawing.Size(180, 22);
            this.kryptonEditCopy.Text = "&Copy";
            //
            // kryptonToolStrip1
            //
            this.kryptonToolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ktsbNew,
            this.ktsbOpen,
            this.ktslReady});
            this.kryptonToolStrip1.Location = new System.Drawing.Point(0, 24);
            this.kryptonToolStrip1.Name = "kryptonToolStrip1";
            this.kryptonToolStrip1.Size = new System.Drawing.Size(984, 25);
            this.kryptonToolStrip1.TabIndex = 1;
            this.kryptonToolStrip1.Text = "kryptonToolStrip1";
            //
            // ktsbNew
            //
            this.ktsbNew.Name = "ktsbNew";
            this.ktsbNew.Size = new System.Drawing.Size(43, 22);
            this.ktsbNew.Text = "New";
            //
            // ktsbOpen
            //
            this.ktsbOpen.Name = "ktsbOpen";
            this.ktsbOpen.Size = new System.Drawing.Size(40, 22);
            this.ktsbOpen.Text = "Open";
            //
            // ktslReady
            //
            this.ktslReady.Name = "ktslReady";
            this.ktslReady.Size = new System.Drawing.Size(122, 22);
            this.ktslReady.Text = "Krypton tool strip text";
            //
            // kryptonStatusStrip1
            //
            this.kryptonStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ksslKrypton});
            this.kryptonStatusStrip1.Location = new System.Drawing.Point(0, 676);
            this.kryptonStatusStrip1.Name = "kryptonStatusStrip1";
            this.kryptonStatusStrip1.Size = new System.Drawing.Size(984, 22);
            this.kryptonStatusStrip1.TabIndex = 2;
            this.kryptonStatusStrip1.Text = "kryptonStatusStrip1";
            //
            // ksslKrypton
            //
            this.ksslKrypton.Name = "ksslKrypton";
            this.ksslKrypton.Size = new System.Drawing.Size(129, 17);
            this.ksslKrypton.Text = "Krypton status strip text";
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.klblStatus);
            this.kryptonPanel1.Controls.Add(this.kbtnResetAll);
            this.kryptonPanel1.Controls.Add(this.kbtnContrastDemo);
            this.kryptonPanel1.Controls.Add(this.kbtnKryptonContext);
            this.kryptonPanel1.Controls.Add(this.kgrpPickers);
            this.kryptonPanel1.Controls.Add(this.kgrpNative);
            this.kryptonPanel1.Controls.Add(this.klblSchemeReadout);
            this.kryptonPanel1.Controls.Add(this.klblDescription);
            this.kryptonPanel1.Controls.Add(this.klblTheme);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 49);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanel1.Size = new System.Drawing.Size(984, 627);
            this.kryptonPanel1.TabIndex = 3;
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
            // kbtnKryptonContext
            //
            this.kbtnKryptonContext.Location = new System.Drawing.Point(360, 12);
            this.kbtnKryptonContext.Name = "kbtnKryptonContext";
            this.kbtnKryptonContext.Size = new System.Drawing.Size(170, 28);
            this.kbtnKryptonContext.TabIndex = 2;
            this.kbtnKryptonContext.Values.Text = "KryptonContextMenu";
            this.kbtnKryptonContext.Click += new System.EventHandler(this.kbtnKryptonContext_Click);
            //
            // klblDescription
            //
            this.klblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.klblDescription.Location = new System.Drawing.Point(15, 48);
            this.klblDescription.Name = "klblDescription";
            this.klblDescription.Size = new System.Drawing.Size(954, 52);
            this.klblDescription.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.klblDescription.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.klblDescription.TabIndex = 3;
            this.klblDescription.Values.Text = "Independent strip text colours.";
            //
            // klblSchemeReadout
            //
            this.klblSchemeReadout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.klblSchemeReadout.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblSchemeReadout.Location = new System.Drawing.Point(15, 104);
            this.klblSchemeReadout.Name = "klblSchemeReadout";
            this.klblSchemeReadout.Size = new System.Drawing.Size(954, 40);
            this.klblSchemeReadout.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.klblSchemeReadout.TabIndex = 4;
            this.klblSchemeReadout.Values.Text = "Scheme colors:";
            //
            // kgrpPickers
            //
            this.kgrpPickers.Location = new System.Drawing.Point(15, 154);
            this.kgrpPickers.Name = "kgrpPickers";
            this.kgrpPickers.Size = new System.Drawing.Size(954, 150);
            this.kgrpPickers.TabIndex = 5;
            this.kgrpPickers.Values.Heading = "Scheme slots (SetSchemeColor)";
            //
            // kgrpPickers.Panel
            //
            this.kgrpPickers.Panel.Controls.Add(this.klblMenuSlot);
            this.kgrpPickers.Panel.Controls.Add(this.kcbtnMenuStrip);
            this.kgrpPickers.Panel.Controls.Add(this.klblToolSlot);
            this.kgrpPickers.Panel.Controls.Add(this.kcbtnToolStrip);
            this.kgrpPickers.Panel.Controls.Add(this.klblStatusSlot);
            this.kgrpPickers.Panel.Controls.Add(this.kcbtnStatusStrip);
            this.kgrpPickers.Panel.Controls.Add(this.klblMenuItemSlot);
            this.kgrpPickers.Panel.Controls.Add(this.kcbtnMenuItem);
            //
            // klblMenuSlot
            //
            this.klblMenuSlot.Location = new System.Drawing.Point(12, 12);
            this.klblMenuSlot.Name = "klblMenuSlot";
            this.klblMenuSlot.Size = new System.Drawing.Size(150, 20);
            this.klblMenuSlot.TabIndex = 0;
            this.klblMenuSlot.Values.Text = "MenuStripText";
            //
            // kcbtnMenuStrip
            //
            this.kcbtnMenuStrip.Location = new System.Drawing.Point(168, 8);
            this.kcbtnMenuStrip.Name = "kcbtnMenuStrip";
            this.kcbtnMenuStrip.SelectedColor = System.Drawing.Color.Empty;
            this.kcbtnMenuStrip.Size = new System.Drawing.Size(250, 28);
            this.kcbtnMenuStrip.TabIndex = 1;
            this.kcbtnMenuStrip.Values.Text = "Menu strip";
            this.kcbtnMenuStrip.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnMenuStrip_SelectedColorChanged);
            //
            // klblToolSlot
            //
            this.klblToolSlot.Location = new System.Drawing.Point(450, 12);
            this.klblToolSlot.Name = "klblToolSlot";
            this.klblToolSlot.Size = new System.Drawing.Size(140, 20);
            this.klblToolSlot.TabIndex = 2;
            this.klblToolSlot.Values.Text = "ToolStripText";
            //
            // kcbtnToolStrip
            //
            this.kcbtnToolStrip.Location = new System.Drawing.Point(596, 8);
            this.kcbtnToolStrip.Name = "kcbtnToolStrip";
            this.kcbtnToolStrip.SelectedColor = System.Drawing.Color.Empty;
            this.kcbtnToolStrip.Size = new System.Drawing.Size(250, 28);
            this.kcbtnToolStrip.TabIndex = 3;
            this.kcbtnToolStrip.Values.Text = "Tool strip";
            this.kcbtnToolStrip.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnToolStrip_SelectedColorChanged);
            //
            // klblStatusSlot
            //
            this.klblStatusSlot.Location = new System.Drawing.Point(12, 52);
            this.klblStatusSlot.Name = "klblStatusSlot";
            this.klblStatusSlot.Size = new System.Drawing.Size(150, 20);
            this.klblStatusSlot.TabIndex = 4;
            this.klblStatusSlot.Values.Text = "StatusStripText";
            //
            // kcbtnStatusStrip
            //
            this.kcbtnStatusStrip.Location = new System.Drawing.Point(168, 48);
            this.kcbtnStatusStrip.Name = "kcbtnStatusStrip";
            this.kcbtnStatusStrip.SelectedColor = System.Drawing.Color.Empty;
            this.kcbtnStatusStrip.Size = new System.Drawing.Size(250, 28);
            this.kcbtnStatusStrip.TabIndex = 5;
            this.kcbtnStatusStrip.Values.Text = "Status strip";
            this.kcbtnStatusStrip.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnStatusStrip_SelectedColorChanged);
            //
            // klblMenuItemSlot
            //
            this.klblMenuItemSlot.Location = new System.Drawing.Point(450, 52);
            this.klblMenuItemSlot.Name = "klblMenuItemSlot";
            this.klblMenuItemSlot.Size = new System.Drawing.Size(140, 20);
            this.klblMenuItemSlot.TabIndex = 6;
            this.klblMenuItemSlot.Values.Text = "MenuItemText";
            //
            // kcbtnMenuItem
            //
            this.kcbtnMenuItem.Location = new System.Drawing.Point(596, 48);
            this.kcbtnMenuItem.Name = "kcbtnMenuItem";
            this.kcbtnMenuItem.SelectedColor = System.Drawing.Color.Empty;
            this.kcbtnMenuItem.Size = new System.Drawing.Size(250, 28);
            this.kcbtnMenuItem.TabIndex = 7;
            this.kcbtnMenuItem.Values.Text = "Menu / context items";
            this.kcbtnMenuItem.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnMenuItem_SelectedColorChanged);
            //
            // kgrpNative
            //
            this.kgrpNative.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.kgrpNative.Location = new System.Drawing.Point(15, 318);
            this.kgrpNative.Name = "kgrpNative";
            this.kgrpNative.Size = new System.Drawing.Size(954, 220);
            this.kgrpNative.TabIndex = 6;
            this.kgrpNative.Values.Heading = "Native WinForms (same ColorTable)";
            //
            // kgrpNative.Panel
            //
            this.kgrpNative.Panel.Controls.Add(this.nativeStatusStrip1);
            this.kgrpNative.Panel.Controls.Add(this.nativeToolStrip1);
            this.kgrpNative.Panel.Controls.Add(this.nativeMenuStrip1);
            this.kgrpNative.Panel.Controls.Add(this.klblNativeHint);
            //
            // klblNativeHint
            //
            this.klblNativeHint.Dock = System.Windows.Forms.DockStyle.Top;
            this.klblNativeHint.Location = new System.Drawing.Point(0, 71);
            this.klblNativeHint.Name = "klblNativeHint";
            this.klblNativeHint.Size = new System.Drawing.Size(952, 40);
            this.klblNativeHint.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.klblNativeHint.TabIndex = 3;
            this.klblNativeHint.Values.Text = "Right-click this group for a native ContextMenuStrip. Dropdown File items use MenuItemText. Native and Krypton strips share the palette ColorTable.";
            //
            // nativeMenuStrip1
            //
            this.nativeMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nativeFileMenu});
            this.nativeMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.nativeMenuStrip1.Name = "nativeMenuStrip1";
            this.nativeMenuStrip1.Size = new System.Drawing.Size(952, 24);
            this.nativeMenuStrip1.TabIndex = 0;
            this.nativeMenuStrip1.Text = "nativeMenuStrip1";
            //
            // nativeFileMenu
            //
            this.nativeFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nativeFileOpen});
            this.nativeFileMenu.Name = "nativeFileMenu";
            this.nativeFileMenu.Size = new System.Drawing.Size(37, 20);
            this.nativeFileMenu.Text = "&File";
            //
            // nativeFileOpen
            //
            this.nativeFileOpen.Name = "nativeFileOpen";
            this.nativeFileOpen.Size = new System.Drawing.Size(180, 22);
            this.nativeFileOpen.Text = "&Open";
            //
            // nativeToolStrip1
            //
            this.nativeToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ntsbNative});
            this.nativeToolStrip1.Location = new System.Drawing.Point(0, 24);
            this.nativeToolStrip1.Name = "nativeToolStrip1";
            this.nativeToolStrip1.Size = new System.Drawing.Size(952, 25);
            this.nativeToolStrip1.TabIndex = 1;
            //
            // ntsbNative
            //
            this.ntsbNative.Name = "ntsbNative";
            this.ntsbNative.Size = new System.Drawing.Size(116, 22);
            this.ntsbNative.Text = "Native tool strip text";
            //
            // nativeStatusStrip1
            //
            this.nativeStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nsslNative});
            this.nativeStatusStrip1.Location = new System.Drawing.Point(0, 176);
            this.nativeStatusStrip1.Name = "nativeStatusStrip1";
            this.nativeStatusStrip1.Size = new System.Drawing.Size(952, 22);
            this.nativeStatusStrip1.TabIndex = 2;
            //
            // nsslNative
            //
            this.nsslNative.Name = "nsslNative";
            this.nsslNative.Size = new System.Drawing.Size(123, 17);
            this.nsslNative.Text = "Native status strip text";
            //
            // kbtnContrastDemo
            //
            this.kbtnContrastDemo.Location = new System.Drawing.Point(15, 552);
            this.kbtnContrastDemo.Name = "kbtnContrastDemo";
            this.kbtnContrastDemo.Size = new System.Drawing.Size(130, 28);
            this.kbtnContrastDemo.TabIndex = 7;
            this.kbtnContrastDemo.Values.Text = "Contrast demo";
            this.kbtnContrastDemo.Click += new System.EventHandler(this.kbtnContrastDemo_Click);
            //
            // kbtnResetAll
            //
            this.kbtnResetAll.Location = new System.Drawing.Point(155, 552);
            this.kbtnResetAll.Name = "kbtnResetAll";
            this.kbtnResetAll.Size = new System.Drawing.Size(90, 28);
            this.kbtnResetAll.TabIndex = 8;
            this.kbtnResetAll.Values.Text = "Reset all";
            this.kbtnResetAll.Click += new System.EventHandler(this.kbtnResetAll_Click);
            //
            // klblStatus
            //
            this.klblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.klblStatus.Location = new System.Drawing.Point(15, 592);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(954, 20);
            this.klblStatus.TabIndex = 9;
            this.klblStatus.Values.Text = "Use Contrast demo, then change one picker at a time. Right-click the native group for ContextMenuStrip.";
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
            this.kryptonContextMenuItem2});
            //
            // kryptonContextMenuItem1
            //
            this.kryptonContextMenuItem1.Text = "Krypton context item A";
            //
            // kryptonContextMenuItem2
            //
            this.kryptonContextMenuItem2.Text = "Krypton context item B";
            //
            // nativeContextMenuStrip1
            //
            this.nativeContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nativeContextItem1,
            this.nativeContextItem2});
            this.nativeContextMenuStrip1.Name = "nativeContextMenuStrip1";
            this.nativeContextMenuStrip1.Size = new System.Drawing.Size(181, 48);
            //
            // nativeContextItem1
            //
            this.nativeContextItem1.Name = "nativeContextItem1";
            this.nativeContextItem1.Size = new System.Drawing.Size(180, 22);
            this.nativeContextItem1.Text = "Native context item A";
            //
            // nativeContextItem2
            //
            this.nativeContextItem2.Name = "nativeContextItem2";
            this.nativeContextItem2.Size = new System.Drawing.Size(180, 22);
            this.nativeContextItem2.Text = "Native context item B";
            //
            // SchemeStripTextDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 698);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kryptonStatusStrip1);
            this.Controls.Add(this.kryptonToolStrip1);
            this.Controls.Add(this.kryptonMenuStrip1);
            this.MainMenuStrip = this.kryptonMenuStrip1;
            this.MinimumSize = new System.Drawing.Size(900, 640);
            this.Name = "SchemeStripTextDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scheme Strip Text Colors (Issue #1100)";
            this.kryptonMenuStrip1.ResumeLayout(false);
            this.kryptonMenuStrip1.PerformLayout();
            this.kryptonToolStrip1.ResumeLayout(false);
            this.kryptonToolStrip1.PerformLayout();
            this.kryptonStatusStrip1.ResumeLayout(false);
            this.kryptonStatusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpPickers.Panel)).EndInit();
            this.kgrpPickers.Panel.ResumeLayout(false);
            this.kgrpPickers.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpPickers)).EndInit();
            this.kgrpPickers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgrpNative.Panel)).EndInit();
            this.kgrpNative.Panel.ResumeLayout(false);
            this.kgrpNative.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpNative)).EndInit();
            this.kgrpNative.ResumeLayout(false);
            this.nativeStatusStrip1.ResumeLayout(false);
            this.nativeStatusStrip1.PerformLayout();
            this.nativeToolStrip1.ResumeLayout(false);
            this.nativeToolStrip1.PerformLayout();
            this.nativeMenuStrip1.ResumeLayout(false);
            this.nativeMenuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.nativeContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonMenuStrip kryptonMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem kryptonFileMenu;
        private System.Windows.Forms.ToolStripMenuItem kryptonFileOpen;
        private System.Windows.Forms.ToolStripMenuItem kryptonFileSave;
        private System.Windows.Forms.ToolStripMenuItem kryptonEditMenu;
        private System.Windows.Forms.ToolStripMenuItem kryptonEditCopy;
        private Krypton.Toolkit.KryptonToolStrip kryptonToolStrip1;
        private System.Windows.Forms.ToolStripButton ktsbNew;
        private System.Windows.Forms.ToolStripButton ktsbOpen;
        private System.Windows.Forms.ToolStripLabel ktslReady;
        private Krypton.Toolkit.KryptonStatusStrip kryptonStatusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ksslKrypton;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonLabel klblTheme;
        private Krypton.Toolkit.KryptonLabel klblDescription;
        private Krypton.Toolkit.KryptonLabel klblSchemeReadout;
        private Krypton.Toolkit.KryptonButton kbtnKryptonContext;
        private Krypton.Toolkit.KryptonGroupBox kgrpPickers;
        private Krypton.Toolkit.KryptonLabel klblMenuSlot;
        private Krypton.Toolkit.KryptonColorButton kcbtnMenuStrip;
        private Krypton.Toolkit.KryptonLabel klblToolSlot;
        private Krypton.Toolkit.KryptonColorButton kcbtnToolStrip;
        private Krypton.Toolkit.KryptonLabel klblStatusSlot;
        private Krypton.Toolkit.KryptonColorButton kcbtnStatusStrip;
        private Krypton.Toolkit.KryptonLabel klblMenuItemSlot;
        private Krypton.Toolkit.KryptonColorButton kcbtnMenuItem;
        private Krypton.Toolkit.KryptonGroupBox kgrpNative;
        private Krypton.Toolkit.KryptonLabel klblNativeHint;
        private System.Windows.Forms.MenuStrip nativeMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nativeFileMenu;
        private System.Windows.Forms.ToolStripMenuItem nativeFileOpen;
        private System.Windows.Forms.ToolStrip nativeToolStrip1;
        private System.Windows.Forms.ToolStripButton ntsbNative;
        private System.Windows.Forms.StatusStrip nativeStatusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel nsslNative;
        private Krypton.Toolkit.KryptonButton kbtnContrastDemo;
        private Krypton.Toolkit.KryptonButton kbtnResetAll;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private Krypton.Toolkit.KryptonContextMenu kryptonContextMenu1;
        private Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem1;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem2;
        private System.Windows.Forms.ContextMenuStrip nativeContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nativeContextItem1;
        private System.Windows.Forms.ToolStripMenuItem nativeContextItem2;
    }
}
