#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;

namespace TestForm
{
    partial class Office2024RibbonDemo
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
            this._manager = new KryptonManager(this.components);
            this._ribbon = new KryptonRibbon();
            this._panel = new KryptonPanel();
            this._instructions = new KryptonWrapLabel();
            this._buttons = new FlowLayoutPanel();
            this._bevel = new KryptonCheckBox();
            this._contextTitles = new KryptonCheckBox();
            this._pills = new KryptonCheckBox();
            this._glow = new KryptonCheckBox();
            this._compact = new KryptonCheckBox();
            this._arrowLabel = new KryptonLabel();
            this._arrowLocation = new KryptonComboBox();
            this._bevelSizeLabel = new KryptonLabel();
            this._bevelSize = new KryptonNumericUpDown();
            this._gapLabel = new KryptonLabel();
            this._gap = new KryptonNumericUpDown();
            this._status = new KryptonWrapLabel();
            this._panel.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ribbon
            // 
            this._ribbon.Dock = System.Windows.Forms.DockStyle.Top;
            this._ribbon.Name = "office2024Ribbon";
            this._ribbon.RibbonFileAppTab.FileAppTabText = "File";
            this._ribbon.ShowCompactButton = true;
            this._ribbon.CompactModeChanged += new System.EventHandler(this.OnCompactModeChanged);
            this.CreateRibbonContent();
            // 
            // _instructions
            // 
            this._instructions.Dock = System.Windows.Forms.DockStyle.Top;
            this._instructions.Height = 220;
            this._instructions.Text = "Issue #4496: Office 2024 ribbon. The selected tab is an underline, groups have no boxes, and File is text." + System.Environment.NewLine + "Switch Blue, Silver, White, grays, and Black, including dark and light modes. Microsoft 365 Blue keeps the folder-tab chrome." + System.Environment.NewLine + "Home has two groups and a dialog launcher. Table Design is a contextual tab. File opens the backstage." + System.Environment.NewLine + "Compact ribbon (#4533) lays those same groups on one row for every ribbon shape: icon and text, then icons only as the form narrows. Group captions and group boxes hide. Each shape keeps its own tabs. Arrow place chooses under the groups, the end of the groups, or the tab row. Switching eases the groups area. The arrow is optional. Narrow the window until ... appears at the end of the row, then click it for the commands that did not fit." + "Issue #4552: double-click File closes the form. A single click only opens the backstage." + System.Environment.NewLine + "Beveled edges and gaps between groups are optional. Bevel size and group gap are pixels at 96 DPI. A gap of zero keeps one card." + System.Environment.NewLine + "Context titles in the title bar are optional for Office 2024 and stay off until you turn them on. Microsoft 365 still shows them." + System.Environment.NewLine + "Tab marks can be a straight line or a pill-shaped line under the label. An optional glow can be turned on.";
            // 
            // _buttons
            // 
            this._buttons.Controls.Add(this.CreateThemeButton("Office 2024 Blue", PaletteMode.Office2024Blue));
            this._buttons.Controls.Add(this.CreateThemeButton("Blue Dark", PaletteMode.Office2024BlueDarkMode));
            this._buttons.Controls.Add(this.CreateThemeButton("Blue Light", PaletteMode.Office2024BlueLightMode));
            this._buttons.Controls.Add(this.CreateThemeButton("Office 2024 Silver", PaletteMode.Office2024Silver));
            this._buttons.Controls.Add(this.CreateThemeButton("Silver Dark", PaletteMode.Office2024SilverDarkMode));
            this._buttons.Controls.Add(this.CreateThemeButton("Silver Light", PaletteMode.Office2024SilverLightMode));
            this._buttons.Controls.Add(this.CreateThemeButton("Office 2024 White", PaletteMode.Office2024White));
            this._buttons.Controls.Add(this.CreateThemeButton("Office 2024 Light Gray", PaletteMode.Office2024LightGray));
            this._buttons.Controls.Add(this.CreateThemeButton("Office 2024 Dark Gray", PaletteMode.Office2024DarkGray));
            this._buttons.Controls.Add(this.CreateThemeButton("Office 2024 Black", PaletteMode.Office2024Black));
            this._buttons.Controls.Add(this.CreateThemeButton("Black Dark", PaletteMode.Office2024BlackDarkMode));
            this._buttons.Controls.Add(this.CreateThemeButton("Black Dark Alternate", PaletteMode.Office2024BlackDarkModeAlternate));
            this._buttons.Controls.Add(this.CreateThemeButton("Microsoft 365 Blue", PaletteMode.Microsoft365Blue));
            this._buttons.Controls.Add(this.CreateThemeButton("Office 2007 Blue", PaletteMode.Office2007Blue));
            this._buttons.Controls.Add(this.CreateThemeButton("Office 2010 Blue", PaletteMode.Office2010Blue));
            this._buttons.Controls.Add(this.CreateThemeButton("Office 2013 White", PaletteMode.Office2013White));
            this._buttons.Controls.Add(this.CreateThemeButton("Sparkle Blue", PaletteMode.SparkleBlue));
            this._buttons.Controls.Add(this.CreateThemeButton("Visual Studio 2010", PaletteMode.VisualStudio2010Render2010));
            this._buttons.Controls.Add(this._bevel);
            this._buttons.Controls.Add(this._contextTitles);
            this._buttons.Controls.Add(this._pills);
            this._buttons.Controls.Add(this._glow);
            this._buttons.Controls.Add(this._compact);
            this._buttons.Controls.Add(this._arrowLabel);
            this._buttons.Controls.Add(this._arrowLocation);
            this._buttons.Controls.Add(this._bevelSizeLabel);
            this._buttons.Controls.Add(this._bevelSize);
            this._buttons.Controls.Add(this._gapLabel);
            this._buttons.Controls.Add(this._gap);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Top;
            this._buttons.Height = 440;
            this._buttons.Padding = new System.Windows.Forms.Padding(8, 8, 8, 0);
            // 
            // _bevel
            // 
            this._bevel.AutoSize = true;
            this._bevel.Margin = new System.Windows.Forms.Padding(8, 6, 8, 0);
            this._bevel.Text = "Beveled group edges";
            this._bevel.CheckedChanged += new System.EventHandler(this.OnBevelCheckedChanged);
            // 
            // _contextTitles
            // 
            this._contextTitles.AutoSize = true;
            this._contextTitles.Margin = new System.Windows.Forms.Padding(8, 6, 8, 0);
            this._contextTitles.Text = "Context titles in title bar";
            this._contextTitles.CheckedChanged += new System.EventHandler(this.OnContextTitlesCheckedChanged);
            // 
            // _pills
            // 
            this._pills.AutoSize = true;
            this._pills.Margin = new System.Windows.Forms.Padding(8, 6, 8, 0);
            this._pills.Text = "Pill shaped lines";
            this._pills.CheckedChanged += new System.EventHandler(this.OnPillsCheckedChanged);
            // 
            // _glow
            // 
            this._glow.AutoSize = true;
            this._glow.Margin = new System.Windows.Forms.Padding(8, 6, 8, 0);
            this._glow.Text = "Glow tab lines";
            this._glow.CheckedChanged += new System.EventHandler(this.OnGlowCheckedChanged);
            // 
            // _compact
            // 
            this._compact.AutoSize = true;
            this._compact.Margin = new System.Windows.Forms.Padding(8, 6, 8, 0);
            this._compact.Text = "Compact ribbon";
            this._compact.CheckedChanged += new System.EventHandler(this.OnCompactCheckedChanged);
            // 
            // _arrowLabel
            // 
            this._arrowLabel.AutoSize = true;
            this._arrowLabel.Margin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this._arrowLabel.Text = "Arrow place";
            // 
            // _arrowLocation
            // 
            this._arrowLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._arrowLocation.DropDownWidth = 140;
            this._arrowLocation.Items.AddRange(new object[] { "Below groups", "End of groups", "Tab row" });
            this._arrowLocation.Margin = new System.Windows.Forms.Padding(4, 4, 8, 0);
            this._arrowLocation.SelectedIndex = 0;
            this._arrowLocation.Width = 140;
            this._arrowLocation.SelectedIndexChanged += new System.EventHandler(this.OnArrowLocationChanged);
            // 
            // _bevelSizeLabel
            // 
            this._bevelSizeLabel.AutoSize = true;
            this._bevelSizeLabel.Margin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this._bevelSizeLabel.Text = "Bevel size";
            // 
            // _bevelSize
            // 
            this._bevelSize.Margin = new System.Windows.Forms.Padding(4, 4, 8, 0);
            this._bevelSize.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            this._bevelSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this._bevelSize.Value = new decimal(PaletteRibbonGeneral.GroupAreaBevelSizeDefault);
            this._bevelSize.Width = 64;
            this._bevelSize.ValueChanged += new System.EventHandler(this.OnBevelSizeChanged);
            // 
            // _gapLabel
            // 
            this._gapLabel.AutoSize = true;
            this._gapLabel.Margin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this._gapLabel.Text = "Group gap";
            // 
            // _gap
            // 
            this._gap.Margin = new System.Windows.Forms.Padding(4, 4, 8, 0);
            this._gap.Maximum = new decimal(new int[] { 48, 0, 0, 0 });
            this._gap.Width = 64;
            this._gap.ValueChanged += new System.EventHandler(this.OnGapChanged);
            // 
            // _status
            // 
            this._status.Dock = System.Windows.Forms.DockStyle.Fill;
            this._status.Text = "Select a theme.";
            // 
            // _panel
            // 
            this._panel.Controls.Add(this._status);
            this._panel.Controls.Add(this._buttons);
            this._panel.Controls.Add(this._instructions);
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // Office2024RibbonDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 760);
            this.Controls.Add(this._ribbon);
            this.Controls.Add(this._panel);
            this._panel.BringToFront();
            this.Name = "Office2024RibbonDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Office 2024 Ribbon — Issue #4496";
            this._panel.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this._buttons.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        /// <summary>
        /// Adds the Home, Insert, and Table Design tabs, the quick-access button, and the File backstage.
        /// </summary>
        private void CreateRibbonContent()
        {
            var home = new KryptonRibbonTab { Text = @"Home", KeyTip = @"H" };
            var insert = new KryptonRibbonTab { Text = @"Insert", KeyTip = @"N" };
            var table = new KryptonRibbonTab
            {
                Text = @"Table Design",
                KeyTip = @"JT",
                ContextName = @"Table"
            };

            home.Groups.Add(CreateClipboardGroup());
            home.Groups.Add(CreateFontGroup());
            insert.Groups.Add(CreateInsertGroup());
            table.Groups.Add(CreateTableGroup());

            this._ribbon.RibbonTabs.Add(home);
            this._ribbon.RibbonTabs.Add(insert);
            this._ribbon.RibbonTabs.Add(table);
            this._ribbon.RibbonContexts.Add(new KryptonRibbonContext
            {
                ContextName = @"Table",
                ContextTitle = @"Table Tools",
                ContextColor = Color.FromArgb(15, 108, 189)
            });
            this._ribbon.SelectedContext = @"Table";
            this._ribbon.QATButtons.Add(new KryptonRibbonQATButton { Text = @"Undo", ToolTipTitle = @"Undo" });
            this.CreateOffice2024Backstage();
        }

        /// <summary>
        /// Hosts an Office 2024 backstage on the File tab. Page content is a label so the rail can be checked.
        /// </summary>
        private void CreateOffice2024Backstage()
        {
            var backstage = new KryptonBackstageView
            {
                OverlayMode = BackstageOverlayMode.FullClient
            };
            backstage.Pages.Add(CreateBackstagePage(@"Home", 0, false, false));
            backstage.Pages.Add(CreateBackstagePage(@"New", 1, false, false));
            backstage.Pages.Add(CreateBackstagePage(@"Open", 2, false, false));
            backstage.Pages.Add(CreateBackstagePage(@"Info", 3, false, true));
            backstage.Pages.Add(CreateBackstagePage(@"Print", 4, false, false));
            backstage.Pages.Add(CreateBackstagePage(@"Options", 10, true, false));

            var close = new KryptonBackstageCommand(@"Close")
            {
                NavigationOrder = 5
            };
            close.Click += (_, _) => this._ribbon.CloseBackstageView();
            backstage.Commands.Add(close);

            this._ribbon.RibbonFileAppTab.UseBackstageView = true;
            this._ribbon.RibbonFileAppTab.BackstageView = backstage;
        }

        private static KryptonBackstagePage CreateBackstagePage(string text, int order, bool placeAtBottom, bool separatorBefore)
        {
            var page = new KryptonBackstagePage
            {
                Text = text,
                NavigationOrder = order,
                PlaceAtBottom = placeAtBottom,
                SeparatorBefore = separatorBefore
            };
            page.Controls.Add(new KryptonLabel
            {
                Text = text,
                Dock = DockStyle.Top,
                Padding = new Padding(24, 20, 24, 0)
            });
            return page;
        }

        private static KryptonRibbonGroup CreateClipboardGroup()
        {
            var paste = Command(@"Paste", @"V", Color.FromArgb(46, 125, 50));
            paste.ItemSizeMaximum = GroupItemSize.Large;
            paste.ItemSizeMinimum = GroupItemSize.Large;
            var cut = Command(@"Cut", @"X", Color.FromArgb(198, 40, 40));
            var copy = Command(@"Copy", @"C", Color.FromArgb(21, 101, 192));

            var lines = new KryptonRibbonGroupTriple();
            lines.Items!.Add(paste);
            lines.Items.Add(cut);
            lines.Items.Add(copy);

            var group = new KryptonRibbonGroup
            {
                TextLine1 = @"Clipboard",
                DialogBoxLauncher = true
            };
            group.Items.Add(lines);
            return group;
        }

        private static KryptonRibbonGroup CreateFontGroup()
        {
            var bold = Command(@"Bold", @"1", Color.FromArgb(66, 66, 66));
            var italic = Command(@"Italic", @"2", Color.FromArgb(106, 27, 154));
            var underline = Command(@"Underline", @"3", Color.FromArgb(0, 105, 92));
            var strike = Command(@"Strikethrough", @"4", Color.FromArgb(191, 54, 12));
            var highlight = Command(@"Highlight", @"I", Color.FromArgb(249, 168, 37));
            var fontColor = Command(@"Font Color", @"FC", Color.FromArgb(183, 28, 28));
            var alignLeft = Command(@"Align Left", @"AL", Color.FromArgb(2, 119, 189));
            var center = Command(@"Center", @"AC", Color.FromArgb(0, 131, 143));
            var alignRight = Command(@"Align Right", @"AR", Color.FromArgb(69, 90, 100));
            var styles = new KryptonRibbonGroupTriple();
            styles.Items!.Add(bold);
            styles.Items.Add(italic);
            styles.Items.Add(underline);
            var colors = new KryptonRibbonGroupTriple();
            colors.Items!.Add(strike);
            colors.Items.Add(highlight);
            colors.Items.Add(fontColor);
            var align = new KryptonRibbonGroupTriple();
            align.Items!.Add(alignLeft);
            align.Items.Add(center);
            align.Items.Add(alignRight);

            var group = new KryptonRibbonGroup { TextLine1 = @"Font" };
            group.Items.Add(styles);
            group.Items.Add(colors);
            group.Items.Add(align);
            return group;
        }

        private static KryptonRibbonGroup CreateInsertGroup()
        {
            var table = new KryptonRibbonGroupButton
            {
                TextLine1 = @"Table",
                KeyTip = @"T",
                ItemSizeMaximum = GroupItemSize.Large,
                ItemSizeMinimum = GroupItemSize.Large
            };
            var lines = new KryptonRibbonGroupTriple();
            lines.Items!.Add(table);
            var group = new KryptonRibbonGroup { TextLine1 = @"Tables" };
            group.Items.Add(lines);
            return group;
        }

        private static KryptonRibbonGroup CreateTableGroup()
        {
            var shading = new KryptonRibbonGroupButton { TextLine1 = @"Shading", KeyTip = @"SH" };
            var lines = new KryptonRibbonGroupTriple();
            lines.Items!.Add(shading);
            var group = new KryptonRibbonGroup { TextLine1 = @"Table Styles" };
            group.Items.Add(lines);
            return group;
        }

        private KryptonManager _manager;
        private KryptonRibbon _ribbon;
        private KryptonPanel _panel;
        private KryptonWrapLabel _instructions;
        private FlowLayoutPanel _buttons;
        private KryptonCheckBox _bevel;
        private KryptonCheckBox _contextTitles;
        private KryptonCheckBox _pills;
        private KryptonCheckBox _glow;
        private KryptonCheckBox _compact;
        private KryptonLabel _arrowLabel;
        private KryptonComboBox _arrowLocation;
        private KryptonLabel _bevelSizeLabel;
        private KryptonNumericUpDown _bevelSize;
        private KryptonLabel _gapLabel;
        private KryptonNumericUpDown _gap;
        private KryptonWrapLabel _status;
    }
}
