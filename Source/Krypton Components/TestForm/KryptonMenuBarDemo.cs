#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Side-by-side demo of native <see cref="MenuStrip"/>, <see cref="KryptonMenuStrip"/>, and <see cref="KryptonMenuBar"/>.
/// </summary>
public partial class KryptonMenuBarDemo : KryptonForm
{
    public KryptonMenuBarDemo()
    {
        InitializeComponent();
        MenuBar = kryptonMenuBar1;
        kryptonMenuBar1.InsertStandardItems();
        BuildComparisonStrips();
        WireMenuBarClicks(kryptonMenuBar1.Items);
        AddShortcutDemo();
        Log("Demo started. Alt or F10 activates the native KryptonMenuBar; Alt+letter uses mnemonics; hover-switch while a drop-down is open.");
    }

    private void BuildComparisonStrips()
    {
        kryptonMenuStrip1.Items.AddRange(
        [
            CreateToolStripMenu("&File", "&New", "&Open", "&Save", "-", "E&xit"),
            CreateToolStripMenu("&Edit", "&Undo", "&Redo", "-", "Cu&t", "&Copy", "&Paste")
        ]);

        menuStrip1.Items.AddRange(
        [
            CreateToolStripMenu("&File", "&New", "&Open", "&Save", "-", "E&xit"),
            CreateToolStripMenu("&Edit", "&Undo", "&Redo", "-", "Cu&t", "&Copy", "&Paste")
        ]);
    }

    private ToolStripMenuItem CreateToolStripMenu(string text, params string[] children)
    {
        var root = new ToolStripMenuItem(text);
        foreach (var child in children)
        {
            if (child == "-")
            {
                root.DropDownItems.Add(new ToolStripSeparator());
                continue;
            }

            var item = new ToolStripMenuItem(child);
            item.Click += (_, _) => Log($"ToolStrip '{root.Text}' / '{item.Text}' clicked.");
            root.DropDownItems.Add(item);
        }

        return root;
    }

    private void WireMenuBarClicks(IEnumerable<KryptonContextMenuItemBase> items)
    {
        foreach (var item in items)
        {
            if (item is KryptonContextMenuItem menuItem)
            {
                menuItem.Click += (_, _) => Log($"KryptonMenuBar '{menuItem.Text}' clicked.");
                WireMenuBarClicks(menuItem.Items);
                continue;
            }

            for (var i = 0; i < item.ItemChildCount; i++)
            {
                var child = item[i];
                if (child != null)
                {
                    WireMenuBarClicks(new[] { child });
                }
            }
        }
    }

    private void AddShortcutDemo()
    {
        foreach (KryptonContextMenuItemBase item in kryptonMenuBar1.Items)
        {
            if (item is not KryptonContextMenuItem { Text: var text } fileItem)
            {
                continue;
            }

            if (!text.Contains("File", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            foreach (KryptonContextMenuItemBase group in fileItem.Items)
            {
                for (var i = 0; i < group.ItemChildCount; i++)
                {
                    if (group[i] is KryptonContextMenuItem child
                        && child.Text.Contains("New", StringComparison.OrdinalIgnoreCase))
                    {
                        child.ShortcutKeys = Keys.Control | Keys.N;
                        Log("Assigned Ctrl+N to File > New on the KryptonMenuBar.");
                        return;
                    }
                }
            }
        }
    }

    private void kbtnInsertStandardItems_Click(object sender, EventArgs e)
    {
        kryptonMenuBar1.Items.Clear();
        kryptonMenuBar1.InsertStandardItems();
        WireMenuBarClicks(kryptonMenuBar1.Items);
        AddShortcutDemo();
        Log("Insert Standard Items replaced the KryptonMenuBar collection.");
    }

    private void kbtnClearLog_Click(object sender, EventArgs e) => klbLog.Items.Clear();

    private void Log(string message)
    {
        klbLog.Items.Insert(0, $"{DateTime.Now:HH:mm:ss}  {message}");
        klblStatus.Text = message;
    }
}
