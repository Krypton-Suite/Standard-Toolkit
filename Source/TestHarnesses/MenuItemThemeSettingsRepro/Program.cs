using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace MenuItemThemeSettingsRepro
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ReproForm());
        }
    }

    internal sealed class ReproForm : Form
    {
        private readonly KryptonCustomPaletteBase _palette;
        private readonly KryptonManager _manager;
        private readonly MenuStrip _menuStrip;
        private readonly ContextMenuStrip _contextMenuStrip;
        private readonly Panel _contextPanel;

        public ReproForm()
        {
            _palette = new KryptonCustomPaletteBase();
            _manager = new KryptonManager
            {
                GlobalCustomPalette = _palette,
                GlobalPaletteMode = PaletteMode.Custom
            };

            Text = @"MenuItem Theme Settings Repro";
            StartPosition = FormStartPosition.CenterScreen;
            Width = 760;
            Height = 420;

            _menuStrip = CreateMenuStrip();
            _contextMenuStrip = CreateContextMenuStrip();
            _contextPanel = CreateContextPanel();

            var buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(8),
                FlowDirection = FlowDirection.LeftToRight
            };

            var fullButton = new Button
            {
                Text = @"Full colors",
                AutoSize = true
            };
            fullButton.Click += delegate { ApplyFullColors(); };

            var borderOnlyButton = new Button
            {
                Text = @"Border only",
                AutoSize = true
            };
            borderOnlyButton.Click += delegate { ApplyBorderOnly(); };

            buttonsPanel.Controls.Add(fullButton);
            buttonsPanel.Controls.Add(borderOnlyButton);

            Controls.Add(_contextPanel);
            Controls.Add(buttonsPanel);
            Controls.Add(_menuStrip);
            MainMenuStrip = _menuStrip;

            ApplyFullColors();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _manager.Dispose();
                _palette.Dispose();
            }

            base.Dispose(disposing);
        }

        private static MenuStrip CreateMenuStrip()
        {
            var menuStrip = new MenuStrip();
            var menu = new ToolStripMenuItem(@"Menu");
            var nested = new ToolStripMenuItem(@"Nested");

            nested.DropDownItems.Add(new ToolStripMenuItem(@"Nested child"));
            menu.DropDownItems.Add(new ToolStripMenuItem(@"Drop-down item"));
            menu.DropDownItems.Add(nested);
            menu.DropDownItems.Add(new ToolStripSeparator());
            menu.DropDownItems.Add(new ToolStripMenuItem(@"Disabled item") { Enabled = false });

            menuStrip.Items.Add(menu);
            menuStrip.Items.Add(new ToolStripMenuItem(@"Second menu")
            {
                DropDownItems =
                {
                    new ToolStripMenuItem(@"Second item")
                }
            });

            return menuStrip;
        }

        private static ContextMenuStrip CreateContextMenuStrip()
        {
            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add(new ToolStripMenuItem(@"Context item"));
            contextMenuStrip.Items.Add(new ToolStripMenuItem(@"Context submenu")
            {
                DropDownItems =
                {
                    new ToolStripMenuItem(@"Context child")
                }
            });

            return contextMenuStrip;
        }

        private Panel CreateContextPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                ContextMenuStrip = _contextMenuStrip
            };

            var label = new Label
            {
                Text = @"Hover menu items; right-click this area for context menu.",
                AutoSize = true,
                Location = new Point(16, 24)
            };

            panel.Controls.Add(label);
            return panel;
        }

        private void ApplyFullColors()
        {
            ResetMenuItemColors();

            _palette.ToolMenuStatus.Menu.MenuItemText = Color.FromArgb(20, 20, 20);
            _palette.ToolMenuStatus.Menu.MenuItemBorder = Color.FromArgb(192, 32, 48);
            _palette.ToolMenuStatus.Menu.MenuItemSelected = Color.FromArgb(255, 238, 128);
            _palette.ToolMenuStatus.Menu.MenuItemSelectedGradientBegin = Color.FromArgb(103, 220, 255);
            _palette.ToolMenuStatus.Menu.MenuItemSelectedGradientEnd = Color.FromArgb(0, 96, 160);
            _palette.ToolMenuStatus.Menu.MenuItemPressedGradientBegin = Color.FromArgb(246, 169, 255);
            _palette.ToolMenuStatus.Menu.MenuItemPressedGradientMiddle = Color.FromArgb(180, 74, 220);
            _palette.ToolMenuStatus.Menu.MenuItemPressedGradientEnd = Color.FromArgb(74, 20, 140);

            RefreshRenderers();
        }

        private void ApplyBorderOnly()
        {
            ResetMenuItemColors();
            _palette.ToolMenuStatus.Menu.MenuItemBorder = Color.FromArgb(192, 32, 48);
            RefreshRenderers();
        }

        private void ResetMenuItemColors()
        {
            _palette.ToolMenuStatus.Menu.MenuItemText = Color.Empty;
            _palette.ToolMenuStatus.Menu.MenuItemBorder = Color.Empty;
            _palette.ToolMenuStatus.Menu.MenuItemSelected = Color.Empty;
            _palette.ToolMenuStatus.Menu.MenuItemSelectedGradientBegin = Color.Empty;
            _palette.ToolMenuStatus.Menu.MenuItemSelectedGradientEnd = Color.Empty;
            _palette.ToolMenuStatus.Menu.MenuItemPressedGradientBegin = Color.Empty;
            _palette.ToolMenuStatus.Menu.MenuItemPressedGradientMiddle = Color.Empty;
            _palette.ToolMenuStatus.Menu.MenuItemPressedGradientEnd = Color.Empty;
        }

        private void RefreshRenderers()
        {
            var renderer = _palette.GetRenderer().RenderToolStrip(_palette);
            _menuStrip.Renderer = renderer;
            _contextMenuStrip.Renderer = renderer;
            _menuStrip.Invalidate(true);
            _contextMenuStrip.Invalidate(true);
        }
    }
}
