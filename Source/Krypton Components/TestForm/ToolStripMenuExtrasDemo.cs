#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demonstrates the menu-related ToolStrip items in <c>Krypton.Toolkit.Utilities</c>: <see cref="KryptonEnhancedToolStripMenuItem"/>
/// (radio groups and check-box style), <see cref="KryptonEnhancedToolStripSeparator"/> (<see cref="KryptonEnhancedToolStripSeparator.ShowSeparatorLine"/>),
/// <see cref="KryptonToolStripMarqueeMenuItem"/>, <see cref="KryptonExpandingMenuItem"/> (<see cref="KryptonExpandingMenuItem.IsStandardItem"/> /
/// <see cref="KryptonExpandingMenuItem.AlwaysHidden"/>), <see cref="KryptonToolStripMenuItemUACShield"/> (elevation disabled for the demo),
/// <see cref="KryptonClearClipboard"/>, and <see cref="KryptonLoadingCircleToolStripMenuItem"/>. Both a <see cref="MenuStrip"/> and a
/// <see cref="ContextMenuStrip"/> are exercised.
/// </summary>
public class ToolStripMenuExtrasDemo : KryptonForm
{
    private readonly KryptonLoadingCircleToolStripMenuItem _loadingCircleItem;
    private readonly KryptonCheckBox _chkLoadingCircleActive;
    private readonly KryptonLabel _lblStatus;

    public ToolStripMenuExtrasDemo()
    {
        Text = @"ToolStrip Menu Extras Demo";
        Size = new Size(900, 600);
        StartPosition = FormStartPosition.CenterParent;

        var menuStrip = new MenuStrip { Dock = DockStyle.Top };

        // ----- Edit menu: UAC shield, ClearClipboard, LoadingCircle spinner -----
        var editMenu = new ToolStripMenuItem(@"&Edit");

        var uacItem = new KryptonToolStripMenuItemUACShield
        {
            Text = @"Restart as &Administrator",
            // Elevation is disabled for this demo so clicking does not spawn a UAC prompt / relaunch.
            ElevateApplicationOnClick = false
        };
        uacItem.Click += (_, _) => UpdateStatus(@"UACShield clicked (ElevateApplicationOnClick = false, so nothing elevates).");

        var clearClipboardItem = new KryptonClearClipboard();
        clearClipboardItem.Click += (_, _) => UpdateStatus(@"KryptonClearClipboard clicked - clipboard text cleared (if any).");

        _loadingCircleItem = new KryptonLoadingCircleToolStripMenuItem();
        _loadingCircleItem.LoadingCircleControl!.StylePreset = StylePresets.MacOSX;

        editMenu.DropDownItems.Add(uacItem);
        editMenu.DropDownItems.Add(clearClipboardItem);
        editMenu.DropDownItems.Add(new ToolStripSeparator());
        editMenu.DropDownItems.Add(new ToolStripLabel(@"Loading spinner:"));
        editMenu.DropDownItems.Add(_loadingCircleItem);

        // ----- View menu: radio group, checkbox item, separator with text, marquee -----
        var viewMenu = new ToolStripMenuItem(@"&View");

        var radioSmall = new KryptonEnhancedToolStripMenuItem { Text = @"Small icons", RadioButtonGroupName = @"IconSize" };
        var radioMedium = new KryptonEnhancedToolStripMenuItem { Text = @"Medium icons", RadioButtonGroupName = @"IconSize", Checked = true };
        var radioLarge = new KryptonEnhancedToolStripMenuItem { Text = @"Large icons", RadioButtonGroupName = @"IconSize" };
        radioSmall.Click += (_, _) => UpdateStatus(@"IconSize radio group -> Small icons");
        radioMedium.Click += (_, _) => UpdateStatus(@"IconSize radio group -> Medium icons");
        radioLarge.Click += (_, _) => UpdateStatus(@"IconSize radio group -> Large icons");

        var separator = new KryptonEnhancedToolStripSeparator { Text = @"Toggles", ShowSeparatorLine = true };

        var checkboxItem = new KryptonEnhancedToolStripMenuItem
        {
            Text = @"Show toolbar",
            DisplayStyle = CheckMarkDisplayStyle.CheckBox,
            Checked = true
        };
        checkboxItem.Click += (_, _) => UpdateStatus($@"Show toolbar = {checkboxItem.Checked}");

        var marqueeItem = new KryptonToolStripMarqueeMenuItem
        {
            Text = @"Live ticker: KryptonToolStripMarqueeMenuItem scrolls this text - hover to pause (StopScrollOnMouseOver).",
            MinimumTextWidth = 260
        };
        marqueeItem.Click += (_, _) => UpdateStatus(@"Marquee item clicked.");

        viewMenu.DropDownItems.Add(radioSmall);
        viewMenu.DropDownItems.Add(radioMedium);
        viewMenu.DropDownItems.Add(radioLarge);
        viewMenu.DropDownItems.Add(separator);
        viewMenu.DropDownItems.Add(checkboxItem);
        viewMenu.DropDownItems.Add(new ToolStripSeparator());
        viewMenu.DropDownItems.Add(marqueeItem);

        // ----- Expanding menu: standard vs collapsible vs always-hidden children -----
        var expandingMenu = new KryptonExpandingMenuItem { Text = @"&Recent (Expanding)" };

        var std1 = new KryptonExpandingMenuItem { Text = @"Document1.txt (IsStandardItem)", IsStandardItem = true };
        var std2 = new KryptonExpandingMenuItem { Text = @"Document2.txt (IsStandardItem)", IsStandardItem = true };
        var more1 = new KryptonExpandingMenuItem { Text = @"Document3.txt (collapsible)" };
        var more2 = new KryptonExpandingMenuItem { Text = @"Document4.txt (collapsible)" };
        var alwaysHidden = new KryptonExpandingMenuItem { Text = @"SecretDraft.txt (AlwaysHidden)", AlwaysHidden = true };

        foreach (var item in new[] { std1, std2, more1, more2, alwaysHidden })
        {
            item.Click += (sender, _) => UpdateStatus($@"Expanding menu item clicked: {((ToolStripItem)sender!).Text}");
        }

        expandingMenu.DropDownItems.Add(std1);
        expandingMenu.DropDownItems.Add(std2);
        expandingMenu.DropDownItems.Add(more1);
        expandingMenu.DropDownItems.Add(more2);
        expandingMenu.DropDownItems.Add(alwaysHidden);

        menuStrip.Items.Add(editMenu);
        menuStrip.Items.Add(viewMenu);
        menuStrip.Items.Add(expandingMenu);

        Controls.Add(menuStrip);

        // ----- ContextMenuStrip: a second, independent set of the same item types -----
        var contextMenu = new ContextMenuStrip();
        var ctxRadio1 = new KryptonEnhancedToolStripMenuItem { Text = @"Sort by name", RadioButtonGroupName = @"CtxSort", Checked = true };
        var ctxRadio2 = new KryptonEnhancedToolStripMenuItem { Text = @"Sort by date", RadioButtonGroupName = @"CtxSort" };
        ctxRadio1.Click += (_, _) => UpdateStatus(@"Context menu: Sort by name");
        ctxRadio2.Click += (_, _) => UpdateStatus(@"Context menu: Sort by date");

        var ctxClearClipboard = new KryptonClearClipboard();
        ctxClearClipboard.Click += (_, _) => UpdateStatus(@"Context menu: KryptonClearClipboard clicked.");

        var ctxUac = new KryptonToolStripMenuItemUACShield { Text = @"Run as administrator", ElevateApplicationOnClick = false };
        ctxUac.Click += (_, _) => UpdateStatus(@"Context menu: UACShield clicked (elevation disabled).");

        contextMenu.Items.Add(ctxRadio1);
        contextMenu.Items.Add(ctxRadio2);
        contextMenu.Items.Add(new ToolStripSeparator());
        contextMenu.Items.Add(ctxClearClipboard);
        contextMenu.Items.Add(ctxUac);

        // ----- Main content -----
        var mainPanel = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(12), ContextMenuStrip = contextMenu };

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 96,
            Text = @"Edit menu: UAC shield (elevation disabled for safety), KryptonClearClipboard, and a KryptonLoadingCircleToolStripMenuItem " +
                   @"spinner (toggle Active below). View menu: radio-styled KryptonEnhancedToolStripMenuItem group, a checkbox-styled item, a " +
                   @"separator with text and a visible line, and a scrolling KryptonToolStripMarqueeMenuItem. Recent (Expanding) menu: open it - " +
                   @"the two IsStandardItem entries always show; collapsible items appear after a short delay or via the '>>' expander; " +
                   @"AlwaysHidden never shows. Right-click this panel for an equivalent ContextMenuStrip."
        };
        mainPanel.Controls.Add(instructions);

        var loadingRow = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Top = 100 };
        _chkLoadingCircleActive = new KryptonCheckBox { Values = { Text = @"LoadingCircleToolStripMenuItem.Active" } };
        loadingRow.Controls.Add(_chkLoadingCircleActive);
        mainPanel.Controls.Add(loadingRow);

        _lblStatus = new KryptonLabel { Dock = DockStyle.Bottom, Values = { Text = @"Ready. Try the menus above or right-click here." } };
        mainPanel.Controls.Add(_lblStatus);

        Controls.Add(mainPanel);

        _chkLoadingCircleActive.CheckedChanged += (_, _) =>
        {
            _loadingCircleItem.LoadingCircleControl!.Active = _chkLoadingCircleActive.Checked;
            UpdateStatus($@"LoadingCircleToolStripMenuItem.Active = {_chkLoadingCircleActive.Checked}");
        };
    }

    private void UpdateStatus(string message) => _lblStatus.Values.Text = message;
}
